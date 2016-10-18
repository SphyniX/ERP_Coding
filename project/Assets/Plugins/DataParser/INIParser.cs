using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class INIParser {

    struct Section {
        public string name;
        public Dictionary<string, string> kv;
    };
    List<Section> listSection;

    public INIParser(string fileContent)
    {
        string[] lines = fileContent.Split(new char[] {'\r', '\n'}, System.StringSplitOptions.RemoveEmptyEntries);
        if (lines != null && lines.Length > 0) {
            listSection = new List<Section>();
            Section section = new Section();
            foreach (string line in lines) {
                string trimLine = line.Trim(' ', '\t');
                if (trimLine.StartsWith("[") && trimLine.EndsWith("]")) {
                    if (!string.IsNullOrEmpty(section.name)) {
                        listSection.Add(section);
                    }
                    // Section Name
                    section.name = trimLine.Trim('[', ']');
                    section.kv = new Dictionary<string, string>();
                } else {
                    if (string.IsNullOrEmpty(section.name)) {
                        throw new System.FormatException("Error Section Name");
                    }
                    string[] segs = trimLine.Split(new char[] {'='}, System.StringSplitOptions.RemoveEmptyEntries);
                    if (segs == null || segs.Length > 2) {
                        throw new System.FormatException("Error Key & Val");
                    }
                    section.kv.Add(segs[0].Trim(' ', '\t'), segs[1].Trim(' ', '\t'));
                }
            }
            if (!string.IsNullOrEmpty(section.name)) {
                listSection.Add(section);
            }
        }
    }

    public override string ToString ()
    {
        string strout = "";
        if (listSection != null) {
            foreach (Section sect in listSection) {
                strout += sect.name + ":";
                foreach (KeyValuePair<string, string> kv in sect.kv) {
                    strout += "(" + kv.Key + "," + kv.Value + ")";
                }
                strout += "\n";
            }
        } else {
            strout = "NULL";
        }
        return strout;
    }

    public Dictionary<string, string> this[string name]
    {
        get {
            foreach (Section sec in listSection) {
                if (sec.name == name) {
                    return sec.kv;
                }
            }
            return null;
        }
    }

    public string this[string name, string key]
    {
        get {
            foreach (Section sec in listSection) {
                if (sec.name == name) {
                    string val;
                    if (sec.kv.TryGetValue(key, out val)) {
                        return val;
                    }
                }
            }
            return null;
        }
    }
}
