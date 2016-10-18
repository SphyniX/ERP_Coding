using UnityEngine;
using UnityEngine.EventSystems;
using UnityEditor;
using System.Collections;

// warning CS0168: 声明了变量，但从未使用
// warning CS0219: 给变量赋值，但从未使用
#pragma warning disable 0168, 0219, 0414
namespace ZFrame.UGUI
{
    public static class UGUICreateMenu
    {
        private const string kUILayerName = "UI";
        private const float kWidth = 160f;
        private const float kThickHeight = 60f;
        private const float kThinHeight = 40f;
        private const string kStandardSpritePath = "UI/Skin/UISprite.psd";
        private const string kBackgroundSpriteResourcePath = "UI/Skin/Background.psd";
        private const string kInputFieldBackgroundPath = "UI/Skin/InputFieldBackground.psd";
        private const string kKnobPath = "UI/Skin/Knob.psd";
        private const string kCheckmarkPath = "UI/Skin/Checkmark.psd";

        private static Vector2 s_ThickGUIElementSize = new Vector2(kWidth, kThickHeight);
        private static Vector2 s_ThinGUIElementSize = new Vector2(kWidth, kThinHeight);
        private static Vector2 s_ImageGUIElementSize = new Vector2(100f, 100f);
        private static Color s_DefaultSelectableColor = new Color(1f, 1f, 1f, 1f);
        private static Color s_PanelColor = new Color(1f, 1f, 1f, 0.392f);
        private static Color s_TextColor = Color.white;

        static RectTransform CreatUIBase(GameObject parent)
        {
            if (parent == null) parent = UGUITools.SelectedRoot(true);

            GameObject go = new GameObject("GameObject");
            go.SetParent(parent, false);
            var rectTrans = go.AddComponent<RectTransform>();
            rectTrans.anchoredPosition = Vector2.zero;
            go.AddComponent<CanvasRenderer>();
            return rectTrans;
        }

        static T CreateUIElm<T>(GameObject parent, params System.Type[] ComponentTypes) where T : UIBehaviour
        {
            RectTransform rect = CreatUIBase(parent);
            T c = rect.gameObject.AddComponent<T>();
            foreach (var comType in ComponentTypes) {
                rect.gameObject.AddComponent(comType);
            }
            return c;
        }

        static UILabel getUILabel(GameObject parent, string name, string text)
        {
            UILabel lb = CreateUIElm<UILabel>(parent);
            lb.name = name;
            lb.font = UGUITools.lastSelectedFont;
            //lb.fontRef = UGUITools.lastSelectedUIFont;
            lb.fontSize = 24;
            lb.text = text;
            lb.color = s_TextColor;
            return lb;
        }

        static void setUISprite(UISprite sp, string name, string resPath, UISprite.Type spType, Color color)
        {
            sp.name = name;
            sp.sprite = AssetDatabase.GetBuiltinExtraResource<Sprite>(resPath);
            sp.type = spType;
            sp.color = color;
        }

        [MenuItem("UGUI/控件/文本(Label) &#l")]
        static void CreateUILabel()
        {
            UILabel lb = getUILabel(null, "lbText", "文本");
            lb.rectTransform.sizeDelta = s_ThinGUIElementSize;
            lb.alignment = TextAnchor.MiddleCenter;
            Selection.activeGameObject = lb.gameObject;
        }

        [MenuItem("UGUI/控件/图片(Sprite) &#s")]
        static void CreateUISprite()
        {
            UISprite sp = CreateUIElm<UISprite>(null);
            sp.name = "spImage";
            sp.color = Color.white;
            sp.sprite = AssetDatabase.GetBuiltinExtraResource<Sprite>(kStandardSpritePath);
            Selection.activeGameObject = sp.gameObject;
        }

        [MenuItem("UGUI/控件/原始图片(Texture) &#t")]
        static void CreateUITexture()
        {
            UITexture sp = CreateUIElm<UITexture>(null);
            sp.name = "spTex";
            Selection.activeGameObject = sp.gameObject;
        }

        [MenuItem("UGUI/控件/按钮(Button) &#b")]
        static void CreateUIButton()
        {
            UIButton btn = CreateUIElm<UIButton>(null, typeof(UISprite));
            var sp = btn.GetComponent<UISprite>();
            setUISprite(sp, "btnButton", kStandardSpritePath, UISprite.Type.Sliced, s_DefaultSelectableColor);
            sp.rectTransform.sizeDelta = s_ThickGUIElementSize;
            UILabel lb = getUILabel(btn.gameObject, "lbText_", "按钮");
            lb.localize = true;
            lb.rectTransform.sizeDelta = s_ThinGUIElementSize;
            lb.alignment = TextAnchor.MiddleCenter;
            lb.color = Color.black;

            btn.targetGraphic = sp;

            Selection.activeGameObject = btn.gameObject;
        }

        [MenuItem("UGUI/控件/切换(Toggle) &#r")]
        static void CreateUIToggle()
        {
            UIToggle tgl = CreateUIElm<UIToggle>(null);
            tgl.name = "tglToggle";
            var rectTrans = tgl.GetComponent<RectTransform>();
            rectTrans.sizeDelta = s_ThickGUIElementSize;

            UISprite spBack = CreateUIElm<UISprite>(tgl.gameObject);
            setUISprite(spBack, "spBack_", kStandardSpritePath, UISprite.Type.Sliced, s_DefaultSelectableColor);

            spBack.rectTransform.anchorMin = new Vector2(0, 0.5f);
            spBack.rectTransform.anchorMax = new Vector2(0, 0.5f);
            spBack.rectTransform.sizeDelta = new Vector2(20, 20);
            spBack.rectTransform.anchoredPosition = new Vector2(10, 0);

            UISprite spChk = CreateUIElm<UISprite>(spBack.gameObject);
            setUISprite(spChk, "spChk_", kCheckmarkPath, UISprite.Type.Simple, Color.white);
            spChk.SetNativeSize();
            //spChk.rectTransform.anchorMin = new Vector2(0.5f, 0.5f);
            //spChk.rectTransform.anchorMax = new Vector2(0.5f, 0.5f);        

            UILabel lb = getUILabel(tgl.gameObject, "lbText_", "切换");
            lb.localize = true;
            lb.rectTransform.pivot = new Vector2(0, 0.5f);
            lb.rectTransform.anchorMin = new Vector2(0, 0.5f);
            lb.rectTransform.anchorMax = new Vector2(0, 0.5f);
            lb.rectTransform.offsetMin = new Vector2(25, 0);
            lb.rectTransform.offsetMax = new Vector2(0, 0);
            lb.rectTransform.sizeDelta = new Vector2(90, 24);
            tgl.targetGraphic = spBack;
            tgl.graphic = spChk;
            tgl.isOn = true;

            Selection.activeGameObject = tgl.gameObject;
        }

        [MenuItem("UGUI/控件/进度条(ProgressBar)")]
        static void CreateUIProgressBar()
        {
            UIProgressBar bar = CreateUIElm<UIProgressBar>(null);
            bar.name = "barProgress";
            var rectTrans = bar.GetComponent<RectTransform>();
            rectTrans.sizeDelta = new Vector2(160, 20);

            UISprite spBack = CreateUIElm<UISprite>(bar.gameObject);
            setUISprite(spBack, "spBack_", kBackgroundSpriteResourcePath, UISprite.Type.Sliced, s_DefaultSelectableColor);
            spBack.rectTransform.anchorMin = new Vector2(0, 0f);
            spBack.rectTransform.anchorMax = new Vector2(1, 1f);
            spBack.rectTransform.offsetMin = new Vector2(0, 0);
            spBack.rectTransform.offsetMax = new Vector2(0, 0);

            UISprite spFill = CreateUIElm<UISprite>(bar.gameObject);
            bar.m_CurrBar = spFill;
            setUISprite(spFill, "spFill_", kStandardSpritePath, UISprite.Type.Sliced, s_DefaultSelectableColor);
            spFill.rectTransform.offsetMin = new Vector2(0, 0);
            spFill.rectTransform.offsetMax = new Vector2(0, 0);

            Selection.activeGameObject = bar.gameObject;
        }

        [MenuItem("UGUI/控件/滑块(Slider)")]
        static void CreateUISlider()
        {
            UISlider sld = CreateUIElm<UISlider>(null);
            sld.name = "sldSlider";
            var rectTrans = sld.GetComponent<RectTransform>();
            rectTrans.sizeDelta = new Vector2(160, 20);

            UISprite spBack = CreateUIElm<UISprite>(sld.gameObject);
            sld.targetGraphic = spBack;
            setUISprite(spBack, "spBack_", kBackgroundSpriteResourcePath, UISprite.Type.Sliced, s_DefaultSelectableColor);
            spBack.rectTransform.anchorMin = new Vector2(0, 0f);
            spBack.rectTransform.anchorMax = new Vector2(1, 1f);
            spBack.rectTransform.offsetMin = new Vector2(0, 0);
            spBack.rectTransform.offsetMax = new Vector2(0, 0);

            UISprite spFill = CreateUIElm<UISprite>(sld.gameObject);
            sld.fillRect = spFill.rectTransform;
            setUISprite(spFill, "spFill_", kStandardSpritePath, UISprite.Type.Sliced, s_DefaultSelectableColor);
            spFill.rectTransform.offsetMin = new Vector2(0, 0);
            spFill.rectTransform.offsetMax = new Vector2(0, 0);

            UISprite spHandle = CreateUIElm<UISprite>(sld.gameObject);
            sld.handleRect = spHandle.rectTransform;
            setUISprite(spHandle, "spHandle_", kKnobPath, UISprite.Type.Simple, s_DefaultSelectableColor);
            spHandle.rectTransform.anchoredPosition = Vector2.zero;
            spHandle.rectTransform.sizeDelta = new Vector2(20, 0);

            Selection.activeGameObject = sld.gameObject;
        }

        static UIScrollbar CreateUIScrollbar(Vector2 sizeDelta)
        {
            UIScrollbar srb = CreateUIElm<UIScrollbar>(null, typeof(UISprite));
            var rectTrans = srb.GetComponent<RectTransform>();
            rectTrans.sizeDelta = sizeDelta;

            UISprite spBack = srb.GetComponent<UISprite>();
            setUISprite(spBack, "srbScroll", kBackgroundSpriteResourcePath, UISprite.Type.Sliced, s_DefaultSelectableColor);

            UISprite spHandle = CreateUIElm<UISprite>(srb.gameObject);
            srb.handleRect = spHandle.rectTransform;
            setUISprite(spHandle, "spHandle_", kStandardSpritePath, UISprite.Type.Sliced, s_DefaultSelectableColor);
            spHandle.rectTransform.offsetMin = new Vector2(0, 0);
            spHandle.rectTransform.offsetMax = new Vector2(0, 0);
            srb.size = 0.2f;

            Selection.activeGameObject = srb.gameObject;
            return srb;
        }

        [MenuItem("UGUI/控件/滚动条(Scrollbar)/从左到右(→)")]
        static void CreateUIScrollbar_L2R()
        {
            UIScrollbar srb = CreateUIScrollbar(new Vector2(kWidth, kThinHeight));
            srb.direction = UnityEngine.UI.Scrollbar.Direction.LeftToRight;
        }

        [MenuItem("UGUI/控件/滚动条(Scrollbar)/从右到左(←)")]
        static void CreateUIScrollbar_R2L()
        {
            UIScrollbar srb = CreateUIScrollbar(new Vector2(kWidth, kThinHeight));
            srb.direction = UnityEngine.UI.Scrollbar.Direction.RightToLeft;
        }

        [MenuItem("UGUI/控件/滚动条(Scrollbar)/从下到上(↑)")]
        static void CreateUIScrollbar_B2T()
        {
            UIScrollbar srb = CreateUIScrollbar(new Vector2(kThinHeight, kWidth));
            srb.direction = UnityEngine.UI.Scrollbar.Direction.BottomToTop;
        }

        [MenuItem("UGUI/控件/滚动条(Scrollbar)/从上到下(↓)")]
        static void CreateUIScrollbar_T2B()
        {
            UIScrollbar srb = CreateUIScrollbar(new Vector2(kThinHeight, kWidth));
            srb.direction = UnityEngine.UI.Scrollbar.Direction.TopToBottom;
        }

        [MenuItem("UGUI/控件/输入框(Input)")]
        static void CreateUIInput()
        {
            UIInput inp = CreateUIElm<UIInput>(null, typeof(UISprite));
            var rectTrans = inp.GetComponent<RectTransform>();
            rectTrans.sizeDelta = s_ThickGUIElementSize;

            UISprite spBack = inp.GetComponent<UISprite>();
            setUISprite(spBack, "inpInput", kInputFieldBackgroundPath, UISprite.Type.Sliced, s_DefaultSelectableColor);

            UILabel lbHold = getUILabel(inp.gameObject, "lbHold_", "输入文本...");
            lbHold.localize = true;
            inp.placeholder = lbHold;
            lbHold.rectTransform.anchorMin = new Vector2(0, 0f);
            lbHold.rectTransform.anchorMax = new Vector2(1, 1f);
            lbHold.rectTransform.offsetMin = new Vector2(10, 3f);
            lbHold.rectTransform.offsetMax = new Vector2(-10, -3f);
            lbHold.color = Color.gray;
            lbHold.fontStyle = FontStyle.Italic;

            UILabel lbText = getUILabel(inp.gameObject, "lbText_", "");
            inp.textComponent = lbText;
            lbText.supportRichText = false; // 输入框不支持富文本
            lbText.rectTransform.anchorMin = new Vector2(0, 0f);
            lbText.rectTransform.anchorMax = new Vector2(1, 1f);
            lbText.rectTransform.offsetMin = new Vector2(10, 3f);
            lbText.rectTransform.offsetMax = new Vector2(-10, -3f);
            lbText.color = Color.black;

            Selection.activeGameObject = inp.gameObject;
        }

        [MenuItem("UGUI/控件/滚动窗口(ScrollView)")]
        static void CreateUIScrollView()
        {
            UIScrollView view = CreateUIElm<UIScrollView>(null, typeof(UISprite), typeof(UnityEngine.UI.Mask));
            view.GetComponent<RectTransform>().sizeDelta = new Vector2(400, 400);

            UISprite spBack = view.GetComponent<UISprite>();
            setUISprite(spBack, "SubScroll", kBackgroundSpriteResourcePath, UISprite.Type.Sliced, s_PanelColor);

            RectTransform rect = CreatUIBase(view.gameObject);
            rect.name = "SubContent";
            rect.anchorMin = new Vector2(0, 0f);
            rect.anchorMax = new Vector2(1, 1f);
            rect.offsetMin = new Vector2(10, 10f);
            rect.offsetMax = new Vector2(-10, -10f);
            view.content = rect;

            Selection.activeGameObject = view.gameObject;
        }
    }
}

