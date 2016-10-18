using System.Collections.Generic;
using System.Text;
using UnityEditor;
using UnityEngine;
using vietlabs;

static internal class h2Reference {
    [PreferenceItem("Hierarchy2")]
    public static void Hierarchy2Reference() {
        EditorGUILayout.Space();
        
        EditorGUI.BeginChangeCheck();

        IconsGUI();
        EditorGUILayout.Space();
        ShortcutGUI();
        EditorGUILayout.Space();
        MiscGUI();

        if (EditorGUI.EndChangeCheck()) WindowX.Hierarchy.Repaint();
    }

    internal static string[] modeTitles;// = { "D", "1", "2", "3", "4", "5" };
    internal static readonly string[] iconTitles = {
            "Script",       "Lock",         "Active",
            "Static",       "ChildCount",   "Prefab",
            "Layer",        "Tag",          "Depth"
    };

    internal static h2StringBool iconModesDrawer;

    internal static void IconsGUI() {
        var useIcon = h2Settings.enableIcons;
        GuiX.xDrawTitleBar("ICONS SETTINGS", ref useIcon, -9f, -40f);
        h2Settings.enableIcons = useIcon;
        EditorGUI.BeginDisabledGroup(!useIcon);

        var idx     = Hierarchy2.currentIconMode;
        var rr      = GUILayoutUtility.GetLastRect();

        if (iconModesDrawer == null) {
            iconModesDrawer = new h2StringBool { source = new StringBuilder(h2Settings.iconModes), total = h2Settings.nIcons };
        }

        if (modeTitles == null) {
            var list = new List<string> {"D"};
            for (var i = 1; i < h2Settings.nModes; i++) {
                list.Add(""+i);
            }
            modeTitles = list.ToArray();
        }

        idx = GUI.Toolbar(rr.r(470f).l(470f - (10f + 20f * h2Settings.nModes)), idx, modeTitles);
        iconModesDrawer.offset = idx * (h2Settings.nIcons + 1);

        if (idx != Hierarchy2.currentIconMode) {
            Hierarchy2.currentIconMode = idx;
            EditorX.xDelayCall(WindowX.Hierarchy.Repaint);
        }

        const int nCols = 3;
        using (GuiX.hzLayout) {
            for (var i = 0; i < iconTitles.Length; i++)
            {
                if (i % nCols == 0) GUILayout.BeginVertical();
                iconModesDrawer.Draw(i, (j, v) => GuiX.GLToggle(v, iconTitles[j], 130f));
                if (i % nCols == nCols - 1 || i == iconTitles.Length - 1) GUILayout.EndVertical();
            }    
        }
        

        if (iconModesDrawer.changed) {
            //Debug.Log(h2Settings.iconModes + " ---> " + iconModesDrawer.source);
            h2Settings.iconModes = iconModesDrawer.source.ToString();
            //Debug.Log("after ---> " + h2Settings.iconModes);
        }

        EditorGUI.EndDisabledGroup();
    }
    internal static void ShortcutGUI() {
        var useShortcut = h2Settings.enableShortcut;
        GuiX.xDrawTitleBar("SHORTCUT", ref useShortcut, -9f, -40f);
        h2Settings.enableShortcut = useShortcut;

        using (GuiX.hzLayout)
        {
            EditorGUI.BeginDisabledGroup(!useShortcut);
            h2Settings.use_Alt_Shortcut = GuiX.GLToggle(h2Settings.use_Alt_Shortcut, "Alt + Key", 130);
            h2Settings.use_Shift_Shortcut = GuiX.GLToggle(h2Settings.use_Shift_Shortcut, "Shift + Key", 130);
            h2Settings.use_Single_Shortcut = GuiX.GLToggle(h2Settings.use_Single_Shortcut, "Key", 130);
            EditorGUI.EndDisabledGroup();
        }
    }

    internal static void IconColor() {
        
    }



    internal static void MiscGUI() {
        GuiX.xDrawTitleBar("MISC", -9f); 
    }

}
