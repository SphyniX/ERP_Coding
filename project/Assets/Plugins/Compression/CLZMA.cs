using System;
using System.IO;

#pragma warning disable 0219,0414
public class CLZMA {

    static public long LenOfPackage = 0;

    static bool stdInMode = false;

    static Int32 dictionary = 1 << 23;
    static Int32 posStateBits = 2;
    static Int32 litContextBits = 3;
    static Int32 litPosBits = 0;
    static Int32 algorithm = 2;
    static Int32 numFastBytes = 128;
    static string mf = "bt4";
    static bool eos = stdInMode;

    static SevenZip.CoderPropID[] propIDs =
    {
        SevenZip.CoderPropID.DictionarySize,
        SevenZip.CoderPropID.PosStateBits,
        SevenZip.CoderPropID.LitContextBits,
        SevenZip.CoderPropID.LitPosBits,
        SevenZip.CoderPropID.Algorithm,
        SevenZip.CoderPropID.NumFastBytes,
        SevenZip.CoderPropID.MatchFinder,
        SevenZip.CoderPropID.EndMarker
    };
    static object[] properties =
    {
        (Int32)(dictionary),
        (Int32)(posStateBits),
        (Int32)(litContextBits),
        (Int32)(litPosBits),
        (Int32)(algorithm),
        (Int32)(numFastBytes),
        mf,
        eos
    };

    static long mLength;
    public static long targetLength { get { return mLength; } }

    public static byte[] Compress(byte[] inBytes, string outPath = "", SevenZip.ICodeProgress progress = null)
    {
        byte[] nbytes = null;
        Stream inStream = new MemoryStream(inBytes);
        Stream outStream = null;
        if (string.IsNullOrEmpty(outPath)) {
            outStream = new MemoryStream();
        } else {
            outStream = new FileStream(outPath, FileMode.Create, FileAccess.ReadWrite);
        }
        if (inStream != null && outStream != null) {
            SevenZip.Compression.LZMA.Encoder encoder = new SevenZip.Compression.LZMA.Encoder();
            encoder.SetCoderProperties(propIDs, properties);
            encoder.WriteCoderProperties(outStream);
            Int64 fileSize;
            if (eos || stdInMode) {
                fileSize = -1;
            } else {
                fileSize = inStream.Length;
            }
            for (int i = 0; i < 8; i++) {
                outStream.WriteByte((Byte)(fileSize >> (8 * i)));
            }
            mLength = inStream.Length;
            encoder.Code(inStream, outStream, -1, -1, progress);

            inStream.Close();

            outStream.Position = 0;//Seek(0, SeekOrigin.Begin);
            BinaryReader sr = new BinaryReader(outStream);
            nbytes = sr.ReadBytes((int)outStream.Length);
            sr.Close();
            outStream.Close();
        }
        return nbytes;
    }

    public static byte[] Decompress(byte[] inBytes, string outPath = "", SevenZip.ICodeProgress progress = null)
    {
        byte[] nbytes = null;
        Stream inStream = new MemoryStream(inBytes);
        Stream outStream = null;
        if (string.IsNullOrEmpty(outPath)) {
            outStream = new MemoryStream();
        } else {
            outStream = new FileStream(outPath, FileMode.Create, FileAccess.ReadWrite);
        }
        BinaryReader sr = new BinaryReader(inStream);

        if (inStream != null && outStream != null) {
            byte[] properties = new byte[5];
            if (sr.Read(properties, 0, 5) != 5) {
                throw (new Exception("input .lzma is too short"));
            }
            //inStream.Seek(5, SeekOrigin.Begin);
            
            SevenZip.Compression.LZMA.Decoder decoder = new SevenZip.Compression.LZMA.Decoder();
            decoder.SetDecoderProperties(properties);
            long outSize = 0;
            for (int i = 0; i < 8; i++) {
                int v = inStream.ReadByte();
                if (v < 0) {
                    throw (new Exception("Can't Read 1"));
                }
                outSize |= ((long)(byte)v) << (8 * i);
            }
            long compressedSize = inStream.Length - inStream.Position;
            mLength = compressedSize;
            decoder.Code(inStream, outStream, compressedSize, outSize, progress);

            sr.Close();
            inStream.Close();

            outStream.Position = 0;//Seek(0, SeekOrigin.Begin);
            sr = new BinaryReader(outStream);
            nbytes = sr.ReadBytes((int)outStream.Length);
            sr.Close();
            outStream.Close();
        }
        return nbytes;
    }

    public static long Package(string inDir, string pattern, string outFile)
    {
        System.IO.FileInfo[] files = new System.IO.DirectoryInfo(inDir).GetFiles(pattern);

        System.IO.FileStream fout = System.IO.File.Create(outFile);
        if (fout == null) return 0;
        System.IO.BinaryWriter fbw = new System.IO.BinaryWriter(fout);

        fbw.Write(files.Length);

        int n = 0;
        int max = files.Length;
        LenOfPackage = 0;
        foreach (System.IO.FileInfo f in files)
        {
            n++;
#if UNITY_EDITOR
            if (!UnityEngine.Application.isPlaying) {
                UnityEditor.EditorUtility.DisplayProgressBar("Compress with LZMA & Package",
                    "Compressing... " + f.Name + "[" + n + "/" + max + "]", (float)n / max);
            }
#endif
            fbw.Write(f.Name.Length);
            fbw.Write(System.Text.Encoding.UTF8.GetBytes(f.Name));
            byte[] nbytes = Compress(System.IO.File.ReadAllBytes(f.FullName));
            fbw.Write(nbytes.Length);
            fbw.Write(nbytes, 0, nbytes.Length);
            //UnityEngine.Debug.Log(f.Name.Length + "[" + f.Name + "]" + nbytes.Length);
            LenOfPackage += f.Length;
        }

        long size = fout.Length;
        fbw.Close();
        fout.Close();
#if UNITY_EDITOR
        if (!UnityEngine.Application.isPlaying) {
            UnityEditor.EditorUtility.ClearProgressBar();
        }
#endif
        return size;
    }

    // Editor only
    public static void Extract(string inFile, string outDir, System.Collections.Generic.List<string> skipList = null)
    {
        UnityEngine.Debug.Log(inFile + "->" + outDir);
        bool bFail = false;
        if (System.IO.File.Exists(inFile)) {
            System.IO.FileStream fin = System.IO.File.OpenRead(inFile);
            //UnityEngine.Debug.Log("Open " + inFile + " = " + fin);
            if (fin != null) {
                System.IO.BinaryReader fbr = new System.IO.BinaryReader(fin);
        
                if (!System.IO.Directory.Exists(outDir)) {
                    System.IO.Directory.CreateDirectory(outDir);
                }
                int nFiles = fbr.ReadInt32();
                for (int i = 0; i < nFiles; ++i)
                {
                    int nameLen = fbr.ReadInt32();
                    string fileName = System.Text.Encoding.UTF8.GetString(fbr.ReadBytes(nameLen));
                    int byteLen = fbr.ReadInt32();

                    if (skipList != null && skipList.Contains(fileName)) {
#if UNITY_EDITOR
                        UnityEditor.EditorUtility.DisplayProgressBar("Extract with LZMA",
                            "Skip: " + fileName + " Length: " + byteLen, (float)i / nFiles);
#endif
                        fbr.ReadBytes(byteLen);
                    } else {
#if UNITY_EDITOR
                        if (!UnityEngine.Application.isPlaying) {
                            UnityEditor.EditorUtility.DisplayProgressBar("Extract with LZMA",
                                "Extract: " + fileName + " Length: " + byteLen, (float)i / nFiles);
                        }
#endif
                        try {
                          if (byteLen > 0) {
                              Decompress(fbr.ReadBytes(byteLen), outDir + "/" + fileName);
                          } else {
                              throw new System.IndexOutOfRangeException("ZeroLength");
                          }
                        } catch (System.Exception e) {
                            UnityEngine.Debug.LogError(e.Message);
                            bFail = true;
                        }
                    }

                    if (bFail) break;
                }
                fbr.Close();
                fin.Close();
            } else {
                UnityEngine.Debug.LogError("Open " + inFile + " = " + fin);
            }
        }
#if UNITY_EDITOR
        if (!UnityEngine.Application.isPlaying) {
            UnityEditor.EditorUtility.ClearProgressBar();
        }
#endif
    }

    public delegate void DelegateProgress(long current, long total, string fileName, long fileSize);
    public delegate void DelegateFinish(string srcFile);
    public static System.Collections.IEnumerator Extract(string inFile, string outDir, DelegateProgress progress, DelegateFinish finish,
        System.Collections.Generic.List<string> skipList = null)
    {
        UnityEngine.Debug.Log(inFile + "->" + outDir);
        bool bFail = false;
        if (System.IO.File.Exists(inFile)) {
            System.IO.FileStream fin = System.IO.File.OpenRead(inFile);
            //UnityEngine.Debug.Log("Open " + inFile + " = " + fin);
            if (fin != null) {
                System.IO.BinaryReader fbr = new System.IO.BinaryReader(fin);
        
                if (!System.IO.Directory.Exists(outDir)) {
                    System.IO.Directory.CreateDirectory(outDir);
                }
                int nFiles = fbr.ReadInt32();
                for (int i = 0; i < nFiles; ++i)
                {
                    int nameLen = fbr.ReadInt32();
                    string fileName = System.Text.Encoding.UTF8.GetString(fbr.ReadBytes(nameLen));
                    int byteLen = fbr.ReadInt32();
                    long fileSize = 0;

                    yield return 1;
                    
                    if (skipList != null && skipList.Contains(fileName)) {
                        UnityEngine.Debug.Log("Skip: " + fileName + " Length: " + byteLen);
                        fbr.ReadBytes(byteLen);
                    } else {
                        UnityEngine.Debug.Log("Extract: " + fileName + " Length: " + byteLen);
                        byte[] nbytes = null;
                        try {
                          if (byteLen > 0) {
                              nbytes = Decompress(fbr.ReadBytes(byteLen), outDir + "/" + fileName);
                              fileSize = nbytes.Length;
                          } else {
                              throw new System.IndexOutOfRangeException("ZeroLength");
                          }
                        } catch (System.Exception e) {
                            UnityEngine.Debug.LogError(e.Message);
                            fileSize = -1;
                            bFail = true;
                        }

                        if (nbytes != null) {
                        //    fileSize = nbytes.LongLength;
                        //    System.IO.FileStream fs = System.IO.File.Create(outDir + "/" + fileName);
                        //    fs.Write(nbytes, 0, nbytes.Length);
                        //    fs.Close();
                        }
                    }
                    if (progress != null) {
                        progress(i + 1, nFiles, fileName, fileSize);
                    }
                    if (bFail) break;
                }
                fbr.Close();
                fin.Close();
            } else {
                UnityEngine.Debug.LogError("Open " + inFile + " = " + fin);
            }
        }
        if (finish != null && !bFail) finish(inFile);
    }

    public struct PackInfo {
        public string packName;
        public int nTotal;
        public int nCurrent;
        public int offset;
    }
    static public System.Collections.IEnumerator Unpack(PackInfo pack, string outDir, DelegateProgress progress, DelegateFinish finish, string record)
    {
        string inFile = pack.packName;
        UnityEngine.Debug.Log(inFile + "->" + outDir);
        bool bFail = false;
        if (System.IO.File.Exists(inFile)) {
            System.IO.FileStream fin = System.IO.File.OpenRead(inFile);
            if (fin != null) {
                System.IO.BinaryReader fbr = new System.IO.BinaryReader(fin);
        
                if (!System.IO.Directory.Exists(outDir)) {
                    System.IO.Directory.CreateDirectory(outDir);
                }
                int nFiles = Math.Max(fbr.ReadInt32(), pack.nTotal);
                fbr.ReadBytes(pack.offset);
                UnityEngine.Debug.Log(string.Format("Unpack Begin: {0}/{1}, Offset: {2}", pack.nCurrent, nFiles, pack.offset));
                for (int i = pack.nCurrent; i < nFiles; ++i) {
                    int nameLen = fbr.ReadInt32();
                    string fileName = System.Text.Encoding.UTF8.GetString(fbr.ReadBytes(nameLen));
                    int byteLen = fbr.ReadInt32();
                    long fileSize = 0;

                    yield return 1;

                    //UnityEngine.Debug.Log("Unpack: " + fileName + " Length: " + byteLen);
                    byte[] nbytes = fbr.ReadBytes(byteLen);
                    fileSize = nbytes.Length;
                    System.IO.FileStream fs = System.IO.File.Create(System.IO.Path.Combine(outDir, fileName));
                    fs.Write(nbytes, 0, nbytes.Length);
                    fs.Close();

                    pack.offset += sizeof(System.Int32) + nameLen + sizeof(System.Int32) + byteLen;
                    if (!string.IsNullOrEmpty(record)) {
                        System.IO.File.WriteAllText(record, string.Format("{0}|{1}|{2}", nFiles, i + 1, pack.offset));
                    }

                    if (progress != null) {
                        progress(i + 1, nFiles, fileName, fileSize);
                    }
                    if (bFail) break;
                }
                fbr.Close();
                fin.Close();
                UnityEngine.Debug.Log(string.Format("Unpack End: {0}, Size: {1}", nFiles, pack.offset));
            } else {
                UnityEngine.Debug.LogError("Open " + inFile + " = " + fin);
            }
        }
        if (finish != null && !bFail) finish(inFile);
    }
}
