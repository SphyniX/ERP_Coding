using UnityEngine;
using UnityEditor;
using UnityEditor.Callbacks;
using UnityEditor.XCodeEditor;
using System.Xml;
using System.IO;

public static class XCodePostProcess
{
	const string codeSign = "iPhone Developer: Andre Liu (Y5ARQNB3M4)";

    public static void OnPostProcessBuild (string pathToBuiltProject)
    {
        // Create a new project object from build target
        XCProject project = new XCProject (pathToBuiltProject);

        // Find and run through all projmods files to patch the project.
        // Please pay attention that ALL projmods files in your project folder will be excuted!
        //在这里面把frameworks添加在你的xcode工程里面
        //string[] files = Directory.GetFiles (Application.dataPath, "*.projmods", SearchOption.AllDirectories);
        //foreach (string file in files) {
        //    project.ApplyMod (file);
        //}

        //增加一个编译标记。。没有的话sharesdk会报错。。
        //project.AddOtherLinkerFlags("-licucore");

        //设置签名的证书， 第二个参数 你可以设置成你的证书
		project.overwriteBuildSetting ("CODE_SIGN_IDENTITY", codeSign, "Release");
		project.overwriteBuildSetting ("DEBUG_INFORMATION_FORMAT", "dwarf", "Release");

		project.overwriteBuildSetting ("CODE_SIGN_IDENTITY", codeSign, "Debug");
		project.overwriteBuildSetting ("DEBUG_INFORMATION_FORMAT", "dwarf", "Debug");


		//得到xcode工程的路径
		//string path = Path.GetFullPath (pathToBuiltProject);

        // 编辑plist 文件
        //EditorPlist(path);

        //编辑代码文件
        //EditorCode(path);

        // Finally save the xcode project
        project.Save ();
    }

    private static void EditorPlist(string filePath)
    {
     
        XCPlist list =new XCPlist(filePath);
        string bundle = "com.yusong.momo";

        string PlistAdd = @"  
            <key>CFBundleURLTypes</key>
            <array>
            <dict>
            <key>CFBundleTypeRole</key>
            <string>Editor</string>
            <key>CFBundleURLIconFile</key>
            <string>Icon@2x</string>
            <key>CFBundleURLName</key>
            <string>"+bundle+@"</string>
            <key>CFBundleURLSchemes</key>
            <array>
            <string>ww123456</string>
            </array>
            </dict>
            </array>";
        
        //在plist里面增加一行
        list.AddKey(PlistAdd);
        //在plist里面替换一行
        list.ReplaceKey("<string>com.yusong.${PRODUCT_NAME}</string>","<string>"+bundle+"</string>");
        //保存
        list.Save();

    }

    private static void EditorCode(string filePath)
    {
		//读取UnityAppController.mm文件
        XClass UnityAppController = new XClass(filePath + "/Classes/UnityAppController.mm");

        //在指定代码中替换一行
        //UnityAppController.Replace(
		//	"return YES;",
		//	"return [ShareSDK handleOpenURL:url sourceApplication:sourceApplication annotation:annotation wxDelegate:nil];"
		//	);

        //在指定代码后面增加一行
        UnityAppController.WriteBelow(
			"printf_console(\"WARNING -> applicationDidReceiveMemoryWarning()\\n\");",
			"\tUnitySendMessage(\"AppController\", \"OnReceiveMemoryWarning\", \"\");"
			);

    }
}
