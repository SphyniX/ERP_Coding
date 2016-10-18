using UnityEngine;
using UnityEditor;
using System.Collections;
using System.IO;

namespace ZFrame.UGUI 
{
	public class FontMakerWizard : ScriptableWizard
	{
		[MenuItem("UGUI/工具/创建字体...")]
		static void OpenWizard()
		{
			ScriptableWizard.DisplayWizard("创建字体向导", typeof(FontMakerWizard), "创建");
		}
		
		public Texture fntTex;
		public TextAsset fntTxt;
		
		protected override bool DrawWizardGUI ()
		{		
			var preTex = fntTex;
			var preTxt = fntTxt;
			fntTex = EditorGUILayout.ObjectField("字体图片文件: ", fntTex, typeof(Texture), false) as Texture;
			fntTxt = EditorGUILayout.ObjectField("字体信息文件: ", fntTxt, typeof(TextAsset), false) as TextAsset;
			return preTex != fntTex || preTxt != fntTxt;
		}
		
		void OnWizardUpdate()
		{		
			helpString = "";
			if (!fntTex) {
				errorString = "请选择一个字体图片文件。";
				isValid = false;
			} else if (!fntTxt) {
				errorString = "请选择一个字体信息文件。";
			} else {
				errorString = "";
				helpString = "马上点击！创建一个自定义的字体。";
			}
			isValid = fntTxt && fntTex;
		}
		
		void OnWizardCreate()
		{
			string fontName = fntTex.name;
			string path = AssetDatabase.GetAssetPath(fntTex);
			path = Path.GetDirectoryName(path);
			
			var matPath = Path.Combine(path, fontName + ".mat");
			var fntMat = AssetDatabase.LoadAssetAtPath<Material>(matPath);
			if (fntMat == null) {
				fntMat = new Material(Shader.Find("UI/Default"));
				fntMat.mainTexture = fntTex;
				AssetDatabase.CreateAsset(fntMat, matPath);
			}
			var fontPath = Path.Combine(path, fontName + ".fontsettings");
			var font = AssetDatabase.LoadAssetAtPath<Font>(fontPath);
			if (font == null) {
				font = new Font(fontName);
				AssetDatabase.CreateAsset(font, fontPath);
			}
			
			font.material = fntMat;
			
			var bmFont = new BMFont();
			BMFontReader.Load(bmFont, fntTxt.name, fntTxt.bytes);
			var charInfos = new CharacterInfo[bmFont.glyphs.Count];
			for (int i = 0; i < bmFont.glyphs.Count; ++i) {
				BMGlyph glyph = bmFont.glyphs[i];
                //var rect = new Rect(glyph.x, glyph.y, glyph.width, glyph.height);
                var uvRect = new Rect(glyph.x / (float)bmFont.texWidth, 1 - glyph.y / (float)bmFont.texHeight,
                    glyph.width / (float)bmFont.texWidth, -1 * glyph.height / (float)bmFont.texHeight);

				var info = new CharacterInfo();
				info.index = glyph.index;
                info.glyphWidth = glyph.width;
                info.glyphHeight = glyph.height;
                info.advance = glyph.advance;
                info.uvTopLeft = uvRect.min;// new Vector2(uvRect.xMin, uvRect.yMin);
                //info.uvBottomRight = new Vector2(uvRect.xMax, uvRect.yMin);
                //info.uvTopLeft = new Vector2(uvRect.xMin, uvRect.yMax);
                info.uvBottomRight = uvRect.max;// new Vector2(uvRect.xMax, uvRect.yMax);
	            charInfos[i] = info;
	        }
	        font.characterInfo = charInfos;
	        
	        //EditorUtility.DisplayDialog("提示", string.Format("功能未实现\nNot Implemented\n実装されていない\nKeine umsetzung"), "确定");
			EditorUtility.DisplayDialog("提示", string.Format("字体创建完成：{0}。\n注意：需要手动修改一下字体配置，否则退出后不会保存！", fontPath), "确定");
	        
	    }
	}
}
