using UnityEngine;
using vietlabs;

public class h2Settings {

    public static bool enable {
        get { return EditorPersist.Get("h2.enable", true); }
        set { EditorPersist.Set("h2.enable", value); }
    }


    // SHOW / HIDE ICONS
    public const int nIcons        = 9;
    public const int nModes        = 4;
    const string defaultIM         = "101011000.001000000.000000000.000000000"; //"101011000.001000000.111111111.000000000.000000000.000000000";
    
    //in order : script, lock, visible, static, childCount, prefab, layer, tag, depth
    public static string iconModes {  
        get {
            var result = EditorPersist.Get("h2.iconModes", defaultIM);
            if (result.Length == defaultIM.Length) return result;

            //invalid saved data, return default
            EditorPersist.Set("h2.iconModes", defaultIM);
            return defaultIM;
        }

        set { EditorPersist.Set("h2.iconModes", value); }
    }

    public static bool enableIcons {
        get { return EditorPersist.Get("h2.enableIcons", true); }
        set { EditorPersist.Set("h2.enableIcons", value); }
    }
    public static bool showIco_Active {
        get { return EditorPersist.Get("h2.showIco_Active", true); }
        set { EditorPersist.Set("h2.showIco_Active", value); }
    }
    public static bool showIco_Script {
        get { return EditorPersist.Get("h2.showIco_Script", true); }
        set { EditorPersist.Set("h2.showIco_Script", value); }
    }
    public static bool showIco_Lock {
	    get { return EditorPersist.Get("h2.showIco_Lock", true); }
        set { EditorPersist.Set("h2.showIco_Lock", value); }
    }
    public static bool showIco_Children {
	    get { return EditorPersist.Get("h2.showIco_Children", true); }
        set { EditorPersist.Set("h2.showIco_Children", value); }
    }
    public static bool showIco_Static {
	    get { return EditorPersist.Get("h2.showIco_Static", true); }
        set { EditorPersist.Set("h2.showIco_Static", value); }
    }
    public static bool showIco_Depth {
        get { return EditorPersist.Get("h2.showIco_Depth", true); }
        set { EditorPersist.Set("h2.showIco_Depth", value); }
    }
    public static bool showIco_Prefab {
        get { return EditorPersist.Get("h2.showIco_Prefab", true); }
        set { EditorPersist.Set("h2.showIco_Prefab", value); }
    }

    public static bool showIco_Component {
        get { return EditorPersist.Get("h2.showIco_Component", true); }
        set { EditorPersist.Set("h2.showIco_Component", value); }
    }
    public static bool showIco_Layer {
        get { return EditorPersist.Get("h2.showIco_Layer", true); }
        set { EditorPersist.Set("h2.showIco_Layer", value); }
    }
    public static bool showIco_Tag {
        get { return EditorPersist.Get("h2.showIco_Tag", true); }
        set { EditorPersist.Set("h2.showIco_Tag", value); }
    }




    // ENABLE / DISABLE SHORTCUTS
    public static bool enableShortcut {
        get { return EditorPersist.Get("h2.enableShortcut", true); }
        set { EditorPersist.Set("h2.enableShortcut", value); }
    }

    public static bool use_Alt_Shortcut {
        get { return EditorPersist.Get("h2.use_Alt_Shortcut", true); }
        set { EditorPersist.Set("h2.use_Alt_Shortcut", value); }
    }
    public static bool use_Shift_Shortcut {
        get { return EditorPersist.Get("h2.use_Shift_Shortcut", true); }
        set { EditorPersist.Set("h2.use_Shift_Shortcut", value); }
    }
    public static bool use_Single_Shortcut {
        get { return EditorPersist.Get("h2.use_Single_Shortcut", true); }
        set { EditorPersist.Set("h2.use_Single_Shortcut", value); }
    }


    // COLOR SETTINGS
    const float ds = -0.5f; 
    const float da = -0.5f;

    public static ColorHSL ColorMissing = ColorHSL.red.dS(ds).dA(da);
    public static ColorHSL ColorValid = ColorHSL.green.dS(ds).dA(da); 

    /*public static Color color_HasScript {
        get { return EditorPersist.Get("h2.color_HasScript", (Color)(ColorHSL.green.dS(ds))); }
        set { EditorPersist.Set("h2.color_HasScript", value); }
    }
    public static Color color_MissingScript {
        get { return EditorPersist.Get("h2.color_MissingScript", (Color)(ColorHSL.red.dS(ds))); }
        set { EditorPersist.Set("h2.color_MissingScript", value); }
    }*/
    public static Color[] color_Depths {
        get {
            return EditorPersist.Get("h2.color_Depths", new Color[] {
                ColorHSL.red.dS(ds).dA(da),
                ColorHSL.yellow.dS(ds).dA(da),
                ColorHSL.green.dS(ds).dA(da),
                ColorHSL.blue.dS(ds).dA(da),
                ColorHSL.magenta.dS(ds).dA(da),
                ColorHSL.cyan.dS(ds).dA(da)

                //ColorX.xFromHSBA(0.0f, 0.65f, 1, 0.5f),
                //ColorX.xFromHSBA(0.1f, 0.65f, 1, 0.5f),
                //ColorX.xFromHSBA(0.2f, 0.65f, 1, 0.5f),
                //ColorX.xFromHSBA(0.3f, 0.65f, 1, 0.5f),
                //ColorX.xFromHSBA(0.4f, 0.65f, 1, 0.5f),
                //ColorX.xFromHSBA(0.5f, 0.65f, 1, 0.5f),
                //ColorX.xFromHSBA(0.6f, 0.65f, 1, 0.5f),
                //ColorX.xFromHSBA(0.7f, 0.65f, 1, 0.5f),
                //ColorX.xFromHSBA(0.8f, 0.65f, 1, 0.5f),
                //ColorX.xFromHSBA(0.9f, 0.65f, 1, 0.5f)
            });
        }
        set { EditorPersist.Set("h2.color_Depths", value); }
    }
    public static Color[] color_Layers {
        get
        {
            return EditorPersist.Get("h2.color_Layers", new Color[] {
                ColorHSL.red.dS(ds).dA(da),
                ColorHSL.yellow.dS(ds).dA(da),
                ColorHSL.green.dS(ds).dA(da),
                ColorHSL.blue.dS(ds).dA(da),
                ColorHSL.magenta.dS(ds).dA(da),
                ColorHSL.cyan.dS(ds).dA(da)
            });
        }
        set { EditorPersist.Set("h2.color_Layers", value); } 
    }
    public static Color[] color_Tags
    {
        get
        {
            return EditorPersist.Get("h2.color_Tags", new Color[] {
                ColorHSL.red.dS(ds).dA(da),
                ColorHSL.yellow.dS(ds).dA(da),
                ColorHSL.green.dS(ds).dA(da),
                ColorHSL.blue.dS(ds).dA(da),
                ColorHSL.magenta.dS(ds).dA(da),
                ColorHSL.cyan.dS(ds).dA(da)
            });
        }
        set { EditorPersist.Set("h2.color_Tags", value); }
    }

    static public Color color_Active { 
        get {
            //return ColorHSL.yellow.xProSkinAdjust(0.15f);
            return EditorPersist.Get("h2.color_Active", ColorHSL.yellow.xProSkinAdjust(0.15f));
        } 
        set { EditorPersist.Set("h2.color_Active", value); }
    }
    static public Color color_ActiveHalf {
        get {
            //return ColorHSL.yellow.xProSkinAdjust(0.25f);
            return EditorPersist.Get("h2.color_ActiveHalf", ColorHSL.yellow.xProSkinAdjust(0.25f));
        }
        set { EditorPersist.Set("h2.color_ActiveHalf", value); }
    }

    static public Color color_Deactive {
        get {
            //return ColorHSL.gray.xProSkinAdjust();
            return EditorPersist.Get("h2.color_Deactive", ColorHSL.gray.xProSkinAdjust());
        }
        set { EditorPersist.Set("h2.color_Deactive", value); }
    }


    // MISC SETTINGS
    private static readonly string[] _ignoreScriptPathsDefault = {
        ".dll",
        "Daikon Forge",
        "FlipbookGames",
        "iTween",
        "NGUI",
        "PlayMaker",
        "TK2DROOT",
        "VietLabs"
    };
    public static string[] ignoreScriptPaths {
        get {
            return EditorPersist.Get("h2.ignoreScriptPaths", _ignoreScriptPathsDefault); }
        set { EditorPersist.Set("h2.ignoreScriptPaths", value); }
    }
    public static int iconOffset {
        get { return EditorPersist.Get("h2.iconOffset", 0); }
        set { EditorPersist.Set("h2.iconOffset", value); }
    }
    public static bool useDKVisible {
        get { return EditorPersist.Get("h2.useDKVisible", true); }
        set { EditorPersist.Set("h2.useDKVisible", value); }
    }


    public static string[] icoActive {
        get { return EditorPersist.Get("h2.icoActive", new []{"eye", "eye_dis", "eye", "eye_dis"}); }
        set { EditorPersist.Set("h2.icoActive", value); }
    }








    //Visible icon
    //public h2Element h2_visible;
    //public h2Element h2_lock;
    //public h2Element h2_static;
    //public h2Element h2_prefab;
    //public h2Element h2_component;

    //special / common
    //public bool h2_enable;
    //public bool h2_children;
    //public bool h2_depth;
    //public bool useDKVisible;
    //public Color missingScriptColor;
    //public Color hasScriptColor;

    //public Color[] h2_depthColors;

    //public int iconOffset;
    //public string[] IgnoreScriptPaths;

    //public bool useShortCut;
    //public bool useSingleKeyShortcut;
    //public bool useAltShortcut;
    //public bool useShiftShortcut;

    /*static public h2Settings LoadConfig() {
        var defaultColors = new List<Color>{Color.white, Color.white, Color.white, Color.white};

        return new h2Settings {
            h2_enable = ,

            h2_visible = new h2Element {
                enabled = EditorPersist.Get("h2.visible_enable", true),
                colors  = EditorPersist.Get("h2.visible_colors", new [] {
                    ColorX.xFromHSBA(0.1f, 0.65f, 1, 1f),
                    ColorX.xFromHSBA(0.0f, 0f, 0.3f, 1f),
                    ColorX.xFromHSBA(0.1f, 0.65f, 0.5f, 1f),
                    ColorX.xFromHSBA(0.0f, 0f, 0.6f, 1f)
                }),
                icons   = EditorPersist.Get("h2.visible_icons", new []{"eye", "eye", "eye", "eye"})
            },

            h2_lock = new h2Element {
                enabled = EditorPersist.Get("h2.lock_enable", true),
                colors  = EditorPersist.Get("h2.lock_colors", defaultColors.ToArray()),
                icons   = EditorPersist.Get("h2.lock_icons", new []{"lock", "lock", "lock", "lock"})
            },
            
            h2_static = new h2Element {
                enabled = EditorPersist.Get("h2.static_enable", true),
                colors  = EditorPersist.Get("h2.static_colors", defaultColors.ToArray()),
                icons   = EditorPersist.Get("h2.static_icons", new []{"static", "static", "static", "static"})
            },

            h2_prefab = new h2Element {
                enabled = EditorPersist.Get("h2.prefab_enable", true),
                colors  = EditorPersist.Get("h2.prefab_colors", defaultColors.ToArray()),
                icons   = EditorPersist.Get("h2.prefab_icons", new []{"prefab", "prefab", "prefab", "prefab"})
            },

            h2_depth        = EditorPersist.Get("h2.depth_enable", true),
            h2_children     = EditorPersist.Get("h2.children_enable", true),
            h2_depthColors  = EditorPersist.Get("h2.depth_colors", new [] {
                ColorX.xFromHSBA(0.0f, 0.65f, 1, 0.5f),
                ColorX.xFromHSBA(0.1f, 0.65f, 1, 0.5f),
                ColorX.xFromHSBA(0.2f, 0.65f, 1, 0.5f),
                ColorX.xFromHSBA(0.3f, 0.65f, 1, 0.5f),
                ColorX.xFromHSBA(0.4f, 0.65f, 1, 0.5f),
                ColorX.xFromHSBA(0.5f, 0.65f, 1, 0.5f),
                ColorX.xFromHSBA(0.6f, 0.65f, 1, 0.5f),
                ColorX.xFromHSBA(0.7f, 0.65f, 1, 0.5f),
                ColorX.xFromHSBA(0.8f, 0.65f, 1, 0.5f),
                ColorX.xFromHSBA(0.9f, 0.65f, 1, 0.5f)
            }),

            iconOffset          = EditorPersist.Get("h2.icon_offset", 0),
            IgnoreScriptPaths   = EditorPersist.Get("h2.ignore_scripts_path", new [] {
                ".dll",
                "Daikon Forge",
                "FlipbookGames",
                "iTween",
                "NGUI",
                "PlayMaker",
                "TK2DROOT",
                "VietLabs"
            }),

            missingScriptColor      = EditorPersist.Get("h2.missing_script_color", ColorX.xFromHSBA(0,0.65f, 1f, 1f)),
            hasScriptColor          = EditorPersist.Get("h2.has_script_color", ColorX.xFromHSBA(0.3f,0.65f, 1f, 1f)),
            useDKVisible            = EditorPersist.Get("h2.use_dk_visible", true),
            useShortCut             = EditorPersist.Get("h2.enable_shortcut", true),
            useAltShortcut          = EditorPersist.Get("h2.alt_shortcut", true),
            useShiftShortcut        = EditorPersist.Get("h2.shift_shortcut", true),
            useSingleKeyShortcut    = EditorPersist.Get("h2.singlekey_shortcut", true),
        };
    }*/

 /*   //only for export / import settings
    public string encoded {
        get {
            return string.Format("h2s*{0}*{1}*{2}*{3}*{4}*{5}*{6}*{7}*{8}", 
                h2_visible.encoded,
                h2_lock.encoded,
                h2_static.encoded,
                h2_prefab.encoded,
                h2_component.encoded,
                h2_children     ? "1" : "0",
                h2_depth        ? "1" : "0",
                useDKVisible    ? "1" : "0", 
                h2_depthColors.xStringEncodeT()
            );
        }
    }
    public static h2Settings Decode(string source) {
        var arr = source.xSplit("*");
        if (arr.Length != 9) {
            Debug.LogWarning("Invalid h2Settings encoded string <" + source + ">");
            return null;
        }
        if (arr[0] != "h2s") {
            Debug.LogWarning("Invalid h2Settings signature <" + source + ">");
            return null;
        }

        return new h2Settings() {
            h2_visible      = h2Element.Decode(arr[0]),
            h2_lock         = h2Element.Decode(arr[1]),
            //h2_children     = h2Element.Decode(arr[2]),
            //h2_depth        = h2Element.Decode(arr[3]),
            h2_static       = h2Element.Decode(arr[4]),
            h2_prefab       = h2Element.Decode(arr[5]),
            h2_component    = h2Element.Decode(arr[6]),
            useDKVisible    = arr[7] == "1",
            h2_depthColors  = arr[8].xStringDecode<Color[]>()
        };
    }*/
}

/*public class h2Element {
    public bool enabled;
    public string[] icons; //dark-enabled, dark-disable, light-enable, light-disable
    public Color[] colors; //dark-enabled, dark-disable, light-enable, light-disable

    /*public string encoded {
        get {
            return string.Format("h2e|{0}|{1}|{2}",
                enabled ? 1 : 0,
                icons.xStringEncodeT(),
                colors.xStringEncodeT()
            );
        }
    }

    public static h2Element Decode(string source) {
        var arr = source.xSplit("|");
        if (arr.Length != 4) {
            Debug.LogWarning("Invalid h2Element encoded string <" + source + ">");
            return null;
        }

        if (arr[0] != "h2e") {
            Debug.LogWarning("Invalid h2Element signature <" + source + ">");
            return null;
        }

        return new h2Element() {
            enabled = arr[1]=="1",
            icons   = arr[2].xStringDecode<string[]>(),
            colors  = arr[3].xStringDecode<Color[]>()
        };
    }#1#
}*/