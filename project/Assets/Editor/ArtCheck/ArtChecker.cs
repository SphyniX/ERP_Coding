using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using System.IO;
using System.Text;
//using Battle.FX;

namespace Artwork {
	enum Method 
	{
		LessThan,
		LessEqual,
		GreatThan,
		GreatEqual,
		Equal,
		NotEqual,
		Same,
	}

	class Data
	{
		public Data( string d, Method m, int v )
		{
			desc = d;
			method = m;
			value = v;
		}
		public string desc;
		public Method method;
		public int value;
		
		public string methodDesc
		{
			get {
				switch (method) {
				case Method.LessThan:
					return "<";
				case Method.LessEqual:
					return "<=";
				case Method.GreatThan:
					return ">";
				case Method.GreatEqual:
					return ">=";
				case Method.Equal:
					return "==";
				case Method.NotEqual:
					return "!=";
				case Method.Same:
					return "≈";
				}
				return "?";
			}
		}
		
		public bool Satified(int v)
		{
			switch (method) {
			case Method.LessThan:
				return v < value;
			case Method.LessEqual:
				return v <= value;
			case Method.GreatThan:
				return v > value;
			case Method.GreatEqual:
				return v >= value;
			case Method.Equal:
				return v == value;
			case Method.NotEqual:
				return v!= value;
			case Method.Same:
				return v == value;
			}
			return false;
		}
	}
	public class ArtChecker : EditorWindow {
		
		[MenuItem("Custom/检查美术资源")]
		public static void ShowWindow(){ RefreshThis (); }
		
		//主角模型
		Dictionary<int, Data> _heroData = new Dictionary<int, Data>{
			{0, new Data("英雄三角面数", Method.LessEqual, 1500)},
			{1, new Data("骨骼数", Method.LessEqual, 40)},
		};

		//主角(头)模型
		Dictionary<int, Data> _headData = new Dictionary<int, Data>{
			{0, new Data("英雄(头)三角面数", Method.LessEqual, 800)},
			{1, new Data("骨骼数", Method.LessEqual, 7)},
		};

		//武器模型
		Dictionary<int, Data> _weaponData = new Dictionary<int, Data>{
			{0, new Data("武器三角面数", Method.LessEqual, 500)},
			{1, new Data("骨骼数", Method.GreatEqual, 1)},
		};

		//怪物模型
		Dictionary<int, Data> _monsterData = new Dictionary<int, Data>{
			{0, new Data("怪物三角面数", Method.LessEqual, 1000)},
			{1, new Data("骨骼数", Method.LessEqual, 26)},
		};

		// 主角贴图
		Dictionary<int, Data> _textureData = new Dictionary<int, Data>{
			{0, new Data("英雄贴图尺寸", Method.Equal, 512)},
		};

		// 主角(头)贴图
		Dictionary<int, Data> _headtexreData = new Dictionary<int, Data>{
			{0, new Data("英雄(头)贴图尺寸", Method.Equal, 128)},
		};

		//武器贴图
		Dictionary<int, Data> _weaptexreData = new Dictionary<int, Data>{
			{0, new Data("武器贴图尺寸", Method.Equal, 128)},
		};

		//怪物贴图
		Dictionary<int, Data> _monptexreData = new Dictionary<int, Data>{
			{0, new Data("怪物贴图尺寸", Method.Equal, 256)},
		};
		void OnGUI( )
		{
			RefreshThis();

			GUIMakeCheckEntry(_heroData, new Checker_Char(), "/RefAssets/Models/Body");
			GUIMakeCheckEntry(_textureData, new Checker_Texc(), "/RefAssets/Models/Body");
			GUIMakeCheckEntry(_headData, new Checker_Char(), "/RefAssets/Models/Head");
			GUIMakeCheckEntry(_headtexreData, new Checker_Texc(), "/RefAssets/Models/Head");
			GUIMakeCheckEntry(_monsterData, new Checker_Char(), "/Artwork/Model/Role");
			GUIMakeCheckEntry(_monptexreData, new Checker_Texc(), "/Artwork/Model/Role");
			GUIMakeCheckEntry(_weaponData, new Checker_Char(), "/Artwork/Model/weapon");
	        GUIMakeCheckEntry(_weaptexreData, new Checker_Texc(), "/Artwork/Model/weapon");

	        GUIMakeCheckEntry( new Checker_Fx(), "/RefAssets/FX" );
		}
		
		static ArtChecker _this;
		static void RefreshThis( )
		{
			if( _this != null ) {
				return;
			}
			_this = EditorWindow.GetWindow<ArtChecker>();
		}
		
		void GUIMakeCheckEntry(Dictionary<int, Data> data, Checker ckr, string path){
			SyncData(data);
			
			ckr.path = path;
			
			if (GUILayout.Button("检查：" + ckr.resType)) {
				_this.CheckEntry(data, ckr);
			}
		}

	    void GUIMakeCheckEntry( Checker ckr, string path )
	    {
	        ckr.path = path;
	        GUILayout.Space( 20 );
	        if (GUILayout.Button( ckr.resType ))
	        {
	            Lister ltr = new Lister();
	            ltr.ProcObjects( ckr );
	        }
	    }

		void CheckEntry( Dictionary<int, Data> data, Checker ckr )
		{ 
			Lister ltr = new Lister();
			ckr.SetData(data);
			ltr.ProcObjects(ckr);
		}
		
		void SyncData( Dictionary<int, Data> data )
		{
			foreach (var pair in data) {
				Data d = pair.Value;
				GUILayout.Label (d.desc + " " + d.methodDesc + " " + d.value.ToString ());
			}
		}
	}

	class AssetInfo
	{
		public string file;
		public List<Object> objs{ get; private set; }
		
		public string GetName(int index){
			if (index == -1)
				return GetName();
			return file + ":" + objs[index].name;
		}
		
		public string GetName(){
			return file;
		}
		
		public void InstObjs(List<Object> objList){
			objs = new List<Object>();
			foreach(var o in objList){
				
				objs.Add(o);
			}
		}
		
		public void DestoryObjs()
		{
			objs.Clear();
		}
		
	}
	class Log
	{
		public enum TriBool
		{
			Yes,
			No,
			Null,
		}
		
		public static void Info( string msg, Checker ckr = null, string fileName  = null, TriBool suc = TriBool.Null  )
		{
			string ckType = "";
			if (ckr != null) {
				ckType = "检查" + ckr.resType + ":";
			}
			if (fileName != null) {
				ckType += fileName;
			}
			
			string desc = "";
			if(suc != TriBool.Null){
				desc += (suc == TriBool.Yes) ? "成功" : " 失败";
			}
			desc += "：" + msg;
			
			string outMsg = "[CHK] " + ckType + desc;
			if (suc != TriBool.No) {
				Debug.Log(outMsg);
			} else {
				Debug.LogWarning(outMsg);
				ToFile(outMsg);
			}
			
		}
		
		public static void Warning(string msg, Checker ckr = null, string fileName = null)
		{
			Info(msg, ckr, fileName, TriBool.No);
		}
		
		public static void ToFile(string msg){
			string Path = Application.dataPath;
			string file = Path.Replace("project/Assets","tools");
			var wtr = File.AppendText(file + "/ART_CHK_LOG.txt"); //添加文本文件
			wtr.WriteLine(msg);
			wtr.Dispose();
		}
	}

	class Lister
	{
		public void ProcObjects( Checker ckr )
		{
			string resPath = Application.dataPath + ckr.path;
			int pathLen = Application.dataPath.IndexOf("Assets");
			bool ret = true;
			
			ckr.OnCheckBegin();
			
			ret = ChkFilesInDir(resPath, pathLen, ckr) && ret;
			
			var dirs = Directory.GetDirectories(resPath); //获取指定目录中的子目录的名称（包括其路径）
			foreach( var d in dirs ) {
				ret = ChkFilesInDir(d, pathLen, ckr) && ret;
			}
			
			if( ret ) {
				Log.Info("全部OK", ckr);
			} else {
				Log.Warning("请更新不合格", ckr);
			}
			
			ckr.OnCheckEnd();
		}
		
		bool ChkFilesInDir( string resPath, int pathLen, Checker ckr )
		{
			bool ret = true;
			var files = Directory.GetFiles(resPath); //返回指定目录中与指定的搜索模式匹配的文件的名称（包含它们的路径）
			foreach( var f in files ) {
				string file = f.Replace("\\", "/");
				string fileName = file.Substring(file.LastIndexOf("/")+1);
				
				if( !ckr.FileFilter(fileName))
					continue;
				
				string assetFile = file.Substring(pathLen);
				
				var objs = AssetDatabase.LoadAllAssetsAtPath(assetFile);
				List<Object> okObjs = new List<Object>();
				
				foreach( var obj in objs ) {
					if( ckr.CheckType(obj) )
						okObjs.Add(obj);
				}
				
				AssetInfo ai = new AssetInfo();
				ai.file = fileName;
				ai.InstObjs(okObjs);
				
				if(okObjs.Count == 0){
					//Log.Info("不包含Mesh (跳过检查）", ckr, ai.GetName());
					//ret = false;
				}else{
					ret = ckr.Check(ai) && ret;
				}
				ai.DestoryObjs();
			}
			
			return ret;
		}
	}
	// 检查基类
	class Checker
	{
		public string path;
		
		virtual public bool Check( AssetInfo ai ) { return true; }
		virtual public bool FileFilter( string file ) { return true; }
		virtual public bool CheckType( Object o ) { return true; }
		virtual public void OnCheckBegin(){}
		virtual public void OnCheckEnd(){}
		
		public string resType;
		public void SetData( Dictionary<int, Data> data ) { _data = data; }
		
		public Checker(string type){
			resType = type;
		}
		
		protected Dictionary<int, Data> _data;
		
		protected bool CheckInt(  AssetInfo ai, int objIdx, int dataIndex, int value )
		{
			bool ok = _data[dataIndex].Satified(value);
			if(!ok){
				string msg = _data[dataIndex].desc + "(" + value + ") 必须" + _data[dataIndex].methodDesc + _data[dataIndex].value;
				Log.Warning(msg, this, ai.GetName(objIdx));
			}
			return ok;
		}
		
		protected int GetTrianglesCount(Mesh mesh)
		{
			int count = 0;
			for (int i = 0; i < mesh.subMeshCount; i++) {
				count += mesh.GetIndices(i).Length / 3;
			}
			return count;
		}
		
		protected bool EndWith(string str, string end)
		{
			return str.EndsWith(end, System.StringComparison.OrdinalIgnoreCase);
		}
	}

	// 检查英雄
	class Checker_Char : Checker
	{
		public Checker_Char():base("英雄"){}
		
		override public bool FileFilter( string file )
		{
			if (!EndWith(file, ".FBX"))
				return false;
			return file.IndexOf("@") < 0;
		}
		
		override public bool CheckType( Object o )
		{
			return (o.GetType () == typeof(SkinnedMeshRenderer));
		}
		
		override public bool Check( AssetInfo ai )
		{
			bool ret = true;
			
			SkinnedMeshRenderer mr = ai.objs[0] as SkinnedMeshRenderer;
			Mesh mesh = mr.sharedMesh;
			
			//三角面数 0
			if( !CheckInt(ai, 0, 0, GetTrianglesCount(mesh)) )
				ret =  false;
			
			//骨骼 1
			//Unity里看到的数量会比3ds max中少1
			//PrintBones(ai, mr.bones);
			if( !CheckInt(ai, 0, 1, mr.bones.Length + 1) ){
				ret =  false;
			}
			
			return ret;
		}
		
	}

	// 检查贴图
	class Checker_Texc : Checker
	{
		public Checker_Texc():base("贴图"){}
		
		override public bool FileFilter( string file )
		{
			if (!EndWith(file, ".tga"))
				return false;		
			return file.IndexOf("a") > 0;
		}
		
		override public bool CheckType( Object o )
		{
			return o.GetType() == typeof(Texture2D);
		}
		
		override public bool Check (AssetInfo ai)
		{
			bool ret = true;
			// 贴图尺寸 0
			Texture2D texc = ai.objs[0] as Texture2D;
			if (!CheckInt(ai, -0, 0, texc.width))
				ret = false;
			
			return ret;
		}
	}

	//检查特效
	class Checker_Fx : Checker
	{
	    public Checker_Fx() : base("自动配置特效批次数"){}

	    public override bool FileFilter(string file)
	    {
	        if (!EndWith(file, ".prefab"))
	            return false;
	        return true;
	    }

	    public override bool CheckType(Object o)
	    {
	        if (o.GetType() == typeof(MeshRenderer))
	            return true;
	        if (o.GetType() == typeof(ParticleSystem))
	            return true;
	        if (o.GetType() == typeof(TrailRenderer))
	            return true;
	        //if (o.GetType() == typeof( FxCtrl ))
	        //    return true;
	        return false;
	    }

	    override public void OnCheckBegin()
	    {
	    }
	    override public void OnCheckEnd()
	    {
	    }
	    override public bool Check(AssetInfo ai)
	    {
	        int fx_batch = 0;
	        bool ret = true;
	        for (int i = 0; i < ai.objs.Count; i++)
	        {
	            var obj = ai.objs[i];
	            if (obj.GetType() == typeof( ParticleSystem ) || obj.GetType() == typeof( TrailRenderer ) || obj.GetType() == typeof( MeshRenderer ))
	            {
	                fx_batch++;
	            }
	        }
	        for (int i = 0; i < ai.objs.Count; i++)
	        {
	            var obj = ai.objs[i];
	            //if (obj.GetType() == typeof( FxCtrl ))
	            //{
	            //    var fx = obj as FxCtrl;
	            //    fx.roughBatches = fx_batch;
	            //}
	        }
	        //Debug.Log( "fx_batch:" + ai.file + " = " + fx_batch );
	        return ret;
	    }
	}
}