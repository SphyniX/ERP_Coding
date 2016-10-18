using System;
using UnityEngine;
using LuaInterface;

public class UnityEngine_UI_InputFieldWrap
{
	public static System.Type Register(IntPtr L)
	{
		LuaMethod[] regs = new LuaMethod[]
		{
			new LuaMethod("MoveTextEnd", MoveTextEnd),
			new LuaMethod("MoveTextStart", MoveTextStart),
			new LuaMethod("ScreenToLocal", ScreenToLocal),
			new LuaMethod("OnBeginDrag", OnBeginDrag),
			new LuaMethod("OnDrag", OnDrag),
			new LuaMethod("OnEndDrag", OnEndDrag),
			new LuaMethod("OnPointerDown", OnPointerDown),
			new LuaMethod("ProcessEvent", ProcessEvent),
			new LuaMethod("OnUpdateSelected", OnUpdateSelected),
			new LuaMethod("ForceLabelUpdate", ForceLabelUpdate),
			new LuaMethod("Rebuild", Rebuild),
			new LuaMethod("LayoutComplete", LayoutComplete),
			new LuaMethod("GraphicUpdateComplete", GraphicUpdateComplete),
			new LuaMethod("ActivateInputField", ActivateInputField),
			new LuaMethod("OnSelect", OnSelect),
			new LuaMethod("OnPointerClick", OnPointerClick),
			new LuaMethod("DeactivateInputField", DeactivateInputField),
			new LuaMethod("OnDeselect", OnDeselect),
			new LuaMethod("OnSubmit", OnSubmit),
			new LuaMethod("new", _CreateUnityEngine_UI_InputField),
			new LuaMethod("GetType", GetClassType),
		};

		LuaField[] fields = new LuaField[]
		{
			new LuaField("shouldHideMobileInput", get_shouldHideMobileInput, set_shouldHideMobileInput),
			new LuaField("text", get_text, set_text),
			new LuaField("isFocused", get_isFocused, null),
			new LuaField("caretBlinkRate", get_caretBlinkRate, set_caretBlinkRate),
			new LuaField("caretWidth", get_caretWidth, set_caretWidth),
			new LuaField("textComponent", get_textComponent, set_textComponent),
			new LuaField("placeholder", get_placeholder, set_placeholder),
			new LuaField("caretColor", get_caretColor, set_caretColor),
			new LuaField("customCaretColor", get_customCaretColor, set_customCaretColor),
			new LuaField("selectionColor", get_selectionColor, set_selectionColor),
			new LuaField("onEndEdit", get_onEndEdit, set_onEndEdit),
			new LuaField("onValueChanged", get_onValueChanged, set_onValueChanged),
			new LuaField("onValidateInput", get_onValidateInput, set_onValidateInput),
			new LuaField("characterLimit", get_characterLimit, set_characterLimit),
			new LuaField("contentType", get_contentType, set_contentType),
			new LuaField("lineType", get_lineType, set_lineType),
			new LuaField("inputType", get_inputType, set_inputType),
			new LuaField("keyboardType", get_keyboardType, set_keyboardType),
			new LuaField("characterValidation", get_characterValidation, set_characterValidation),
			new LuaField("readOnly", get_readOnly, set_readOnly),
			new LuaField("multiLine", get_multiLine, null),
			new LuaField("asteriskChar", get_asteriskChar, set_asteriskChar),
			new LuaField("wasCanceled", get_wasCanceled, null),
			new LuaField("caretPosition", get_caretPosition, set_caretPosition),
			new LuaField("selectionAnchorPosition", get_selectionAnchorPosition, set_selectionAnchorPosition),
			new LuaField("selectionFocusPosition", get_selectionFocusPosition, set_selectionFocusPosition),
		};

		var type = typeof(UnityEngine.UI.InputField);
		L.WrapCSharpObject(type, regs, fields);
		return type;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int _CreateUnityEngine_UI_InputField(IntPtr L)
	{
		LuaDLL.luaL_error(L, "UnityEngine.UI.InputField class does not have a constructor function");
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetClassType(IntPtr L)
	{
		int count = L.GetTop();
		if (count == 0) {
			L.PushLightUserData(typeof(UnityEngine.UI.InputField));
			return 1;
		} else {
			return MetaMethods.GetType(L);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_shouldHideMobileInput(IntPtr L)
	{
		object o = L.ToUserData(1);
		UnityEngine.UI.InputField obj = (UnityEngine.UI.InputField)o;

		if (obj == null)
		{
			LuaTypes types = L.Type(1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name shouldHideMobileInput");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index shouldHideMobileInput on a nil value");
			}
		}

		L.PushBoolean(obj.shouldHideMobileInput);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_text(IntPtr L)
	{
		object o = L.ToUserData(1);
		UnityEngine.UI.InputField obj = (UnityEngine.UI.InputField)o;

		if (obj == null)
		{
			LuaTypes types = L.Type(1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name text");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index text on a nil value");
			}
		}

		L.PushString(obj.text);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_isFocused(IntPtr L)
	{
		object o = L.ToUserData(1);
		UnityEngine.UI.InputField obj = (UnityEngine.UI.InputField)o;

		if (obj == null)
		{
			LuaTypes types = L.Type(1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name isFocused");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index isFocused on a nil value");
			}
		}

		L.PushBoolean(obj.isFocused);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_caretBlinkRate(IntPtr L)
	{
		object o = L.ToUserData(1);
		UnityEngine.UI.InputField obj = (UnityEngine.UI.InputField)o;

		if (obj == null)
		{
			LuaTypes types = L.Type(1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name caretBlinkRate");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index caretBlinkRate on a nil value");
			}
		}

		L.PushNumber(obj.caretBlinkRate);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_caretWidth(IntPtr L)
	{
		object o = L.ToUserData(1);
		UnityEngine.UI.InputField obj = (UnityEngine.UI.InputField)o;

		if (obj == null)
		{
			LuaTypes types = L.Type(1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name caretWidth");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index caretWidth on a nil value");
			}
		}

		L.PushInteger(obj.caretWidth);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_textComponent(IntPtr L)
	{
		object o = L.ToUserData(1);
		UnityEngine.UI.InputField obj = (UnityEngine.UI.InputField)o;

		if (obj == null)
		{
			LuaTypes types = L.Type(1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name textComponent");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index textComponent on a nil value");
			}
		}

		L.PushLightUserData(obj.textComponent);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_placeholder(IntPtr L)
	{
		object o = L.ToUserData(1);
		UnityEngine.UI.InputField obj = (UnityEngine.UI.InputField)o;

		if (obj == null)
		{
			LuaTypes types = L.Type(1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name placeholder");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index placeholder on a nil value");
			}
		}

		L.PushLightUserData(obj.placeholder);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_caretColor(IntPtr L)
	{
		object o = L.ToUserData(1);
		UnityEngine.UI.InputField obj = (UnityEngine.UI.InputField)o;

		if (obj == null)
		{
			LuaTypes types = L.Type(1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name caretColor");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index caretColor on a nil value");
			}
		}

		L.PushUData(obj.caretColor);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_customCaretColor(IntPtr L)
	{
		object o = L.ToUserData(1);
		UnityEngine.UI.InputField obj = (UnityEngine.UI.InputField)o;

		if (obj == null)
		{
			LuaTypes types = L.Type(1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name customCaretColor");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index customCaretColor on a nil value");
			}
		}

		L.PushBoolean(obj.customCaretColor);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_selectionColor(IntPtr L)
	{
		object o = L.ToUserData(1);
		UnityEngine.UI.InputField obj = (UnityEngine.UI.InputField)o;

		if (obj == null)
		{
			LuaTypes types = L.Type(1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name selectionColor");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index selectionColor on a nil value");
			}
		}

		L.PushUData(obj.selectionColor);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_onEndEdit(IntPtr L)
	{
		object o = L.ToUserData(1);
		UnityEngine.UI.InputField obj = (UnityEngine.UI.InputField)o;

		if (obj == null)
		{
			LuaTypes types = L.Type(1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name onEndEdit");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index onEndEdit on a nil value");
			}
		}

		L.PushLightUserData(obj.onEndEdit);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_onValueChanged(IntPtr L)
	{
		object o = L.ToUserData(1);
		UnityEngine.UI.InputField obj = (UnityEngine.UI.InputField)o;

		if (obj == null)
		{
			LuaTypes types = L.Type(1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name onValueChanged");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index onValueChanged on a nil value");
			}
		}

		L.PushLightUserData(obj.onValueChanged);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_onValidateInput(IntPtr L)
	{
		object o = L.ToUserData(1);
		UnityEngine.UI.InputField obj = (UnityEngine.UI.InputField)o;

		if (obj == null)
		{
			LuaTypes types = L.Type(1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name onValidateInput");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index onValidateInput on a nil value");
			}
		}

		L.PushUData(obj.onValidateInput);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_characterLimit(IntPtr L)
	{
		object o = L.ToUserData(1);
		UnityEngine.UI.InputField obj = (UnityEngine.UI.InputField)o;

		if (obj == null)
		{
			LuaTypes types = L.Type(1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name characterLimit");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index characterLimit on a nil value");
			}
		}

		L.PushInteger(obj.characterLimit);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_contentType(IntPtr L)
	{
		object o = L.ToUserData(1);
		UnityEngine.UI.InputField obj = (UnityEngine.UI.InputField)o;

		if (obj == null)
		{
			LuaTypes types = L.Type(1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name contentType");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index contentType on a nil value");
			}
		}

		L.PushUData(obj.contentType);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_lineType(IntPtr L)
	{
		object o = L.ToUserData(1);
		UnityEngine.UI.InputField obj = (UnityEngine.UI.InputField)o;

		if (obj == null)
		{
			LuaTypes types = L.Type(1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name lineType");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index lineType on a nil value");
			}
		}

		L.PushUData(obj.lineType);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_inputType(IntPtr L)
	{
		object o = L.ToUserData(1);
		UnityEngine.UI.InputField obj = (UnityEngine.UI.InputField)o;

		if (obj == null)
		{
			LuaTypes types = L.Type(1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name inputType");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index inputType on a nil value");
			}
		}

		L.PushUData(obj.inputType);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_keyboardType(IntPtr L)
	{
		object o = L.ToUserData(1);
		UnityEngine.UI.InputField obj = (UnityEngine.UI.InputField)o;

		if (obj == null)
		{
			LuaTypes types = L.Type(1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name keyboardType");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index keyboardType on a nil value");
			}
		}

		L.PushUData(obj.keyboardType);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_characterValidation(IntPtr L)
	{
		object o = L.ToUserData(1);
		UnityEngine.UI.InputField obj = (UnityEngine.UI.InputField)o;

		if (obj == null)
		{
			LuaTypes types = L.Type(1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name characterValidation");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index characterValidation on a nil value");
			}
		}

		L.PushUData(obj.characterValidation);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_readOnly(IntPtr L)
	{
		object o = L.ToUserData(1);
		UnityEngine.UI.InputField obj = (UnityEngine.UI.InputField)o;

		if (obj == null)
		{
			LuaTypes types = L.Type(1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name readOnly");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index readOnly on a nil value");
			}
		}

		L.PushBoolean(obj.readOnly);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_multiLine(IntPtr L)
	{
		object o = L.ToUserData(1);
		UnityEngine.UI.InputField obj = (UnityEngine.UI.InputField)o;

		if (obj == null)
		{
			LuaTypes types = L.Type(1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name multiLine");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index multiLine on a nil value");
			}
		}

		L.PushBoolean(obj.multiLine);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_asteriskChar(IntPtr L)
	{
		object o = L.ToUserData(1);
		UnityEngine.UI.InputField obj = (UnityEngine.UI.InputField)o;

		if (obj == null)
		{
			LuaTypes types = L.Type(1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name asteriskChar");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index asteriskChar on a nil value");
			}
		}

		L.PushLightUserData(obj.asteriskChar);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_wasCanceled(IntPtr L)
	{
		object o = L.ToUserData(1);
		UnityEngine.UI.InputField obj = (UnityEngine.UI.InputField)o;

		if (obj == null)
		{
			LuaTypes types = L.Type(1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name wasCanceled");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index wasCanceled on a nil value");
			}
		}

		L.PushBoolean(obj.wasCanceled);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_caretPosition(IntPtr L)
	{
		object o = L.ToUserData(1);
		UnityEngine.UI.InputField obj = (UnityEngine.UI.InputField)o;

		if (obj == null)
		{
			LuaTypes types = L.Type(1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name caretPosition");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index caretPosition on a nil value");
			}
		}

		L.PushInteger(obj.caretPosition);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_selectionAnchorPosition(IntPtr L)
	{
		object o = L.ToUserData(1);
		UnityEngine.UI.InputField obj = (UnityEngine.UI.InputField)o;

		if (obj == null)
		{
			LuaTypes types = L.Type(1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name selectionAnchorPosition");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index selectionAnchorPosition on a nil value");
			}
		}

		L.PushInteger(obj.selectionAnchorPosition);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_selectionFocusPosition(IntPtr L)
	{
		object o = L.ToUserData(1);
		UnityEngine.UI.InputField obj = (UnityEngine.UI.InputField)o;

		if (obj == null)
		{
			LuaTypes types = L.Type(1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name selectionFocusPosition");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index selectionFocusPosition on a nil value");
			}
		}

		L.PushInteger(obj.selectionFocusPosition);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_shouldHideMobileInput(IntPtr L)
	{
		object o = L.ToUserData(1);
		UnityEngine.UI.InputField obj = (UnityEngine.UI.InputField)o;

		if (obj == null)
		{
			LuaTypes types = L.Type(1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name shouldHideMobileInput");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index shouldHideMobileInput on a nil value");
			}
		}

		obj.shouldHideMobileInput = L.ChkBoolean(3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_text(IntPtr L)
	{
		object o = L.ToUserData(1);
		UnityEngine.UI.InputField obj = (UnityEngine.UI.InputField)o;

		if (obj == null)
		{
			LuaTypes types = L.Type(1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name text");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index text on a nil value");
			}
		}

		obj.text = L.ChkLuaString(3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_caretBlinkRate(IntPtr L)
	{
		object o = L.ToUserData(1);
		UnityEngine.UI.InputField obj = (UnityEngine.UI.InputField)o;

		if (obj == null)
		{
			LuaTypes types = L.Type(1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name caretBlinkRate");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index caretBlinkRate on a nil value");
			}
		}

		obj.caretBlinkRate = (float)L.ChkNumber(3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_caretWidth(IntPtr L)
	{
		object o = L.ToUserData(1);
		UnityEngine.UI.InputField obj = (UnityEngine.UI.InputField)o;

		if (obj == null)
		{
			LuaTypes types = L.Type(1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name caretWidth");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index caretWidth on a nil value");
			}
		}

		obj.caretWidth = (int)L.ChkNumber(3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_textComponent(IntPtr L)
	{
		object o = L.ToUserData(1);
		UnityEngine.UI.InputField obj = (UnityEngine.UI.InputField)o;

		if (obj == null)
		{
			LuaTypes types = L.Type(1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name textComponent");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index textComponent on a nil value");
			}
		}

		obj.textComponent = L.ToComponent(3, typeof(UnityEngine.UI.Text)) as UnityEngine.UI.Text;
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_placeholder(IntPtr L)
	{
		object o = L.ToUserData(1);
		UnityEngine.UI.InputField obj = (UnityEngine.UI.InputField)o;

		if (obj == null)
		{
			LuaTypes types = L.Type(1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name placeholder");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index placeholder on a nil value");
			}
		}

		obj.placeholder = L.ToComponent(3, typeof(UnityEngine.UI.Graphic)) as UnityEngine.UI.Graphic;
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_caretColor(IntPtr L)
	{
		object o = L.ToUserData(1);
		UnityEngine.UI.InputField obj = (UnityEngine.UI.InputField)o;

		if (obj == null)
		{
			LuaTypes types = L.Type(1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name caretColor");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index caretColor on a nil value");
			}
		}

		obj.caretColor = L.ToColor(3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_customCaretColor(IntPtr L)
	{
		object o = L.ToUserData(1);
		UnityEngine.UI.InputField obj = (UnityEngine.UI.InputField)o;

		if (obj == null)
		{
			LuaTypes types = L.Type(1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name customCaretColor");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index customCaretColor on a nil value");
			}
		}

		obj.customCaretColor = L.ChkBoolean(3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_selectionColor(IntPtr L)
	{
		object o = L.ToUserData(1);
		UnityEngine.UI.InputField obj = (UnityEngine.UI.InputField)o;

		if (obj == null)
		{
			LuaTypes types = L.Type(1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name selectionColor");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index selectionColor on a nil value");
			}
		}

		obj.selectionColor = L.ToColor(3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_onEndEdit(IntPtr L)
	{
		object o = L.ToUserData(1);
		UnityEngine.UI.InputField obj = (UnityEngine.UI.InputField)o;

		if (obj == null)
		{
			LuaTypes types = L.Type(1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name onEndEdit");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index onEndEdit on a nil value");
			}
		}

		obj.onEndEdit = (UnityEngine.UI.InputField.SubmitEvent)L.ChkUserData(3, typeof(UnityEngine.UI.InputField.SubmitEvent));
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_onValueChanged(IntPtr L)
	{
		object o = L.ToUserData(1);
		UnityEngine.UI.InputField obj = (UnityEngine.UI.InputField)o;

		if (obj == null)
		{
			LuaTypes types = L.Type(1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name onValueChanged");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index onValueChanged on a nil value");
			}
		}

		obj.onValueChanged = (UnityEngine.UI.InputField.OnChangeEvent)L.ChkUserData(3, typeof(UnityEngine.UI.InputField.OnChangeEvent));
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_onValidateInput(IntPtr L)
	{
		object o = L.ToUserData(1);
		UnityEngine.UI.InputField obj = (UnityEngine.UI.InputField)o;

		if (obj == null)
		{
			LuaTypes types = L.Type(1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name onValidateInput");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index onValidateInput on a nil value");
			}
		}

		LuaTypes funcType = L.Type(3);

		if (funcType != LuaTypes.LUA_TFUNCTION)
		{
			obj.onValidateInput = (UnityEngine.UI.InputField.OnValidateInput)L.ChkUserData(3, typeof(UnityEngine.UI.InputField.OnValidateInput));
		}
		else
		{
			LuaFunction func = L.ToLuaFunction(3);
			obj.onValidateInput = (param0, param1, param2) =>
			{
				int top = func.BeginPCall();
				L.PushString(param0);
				L.PushInteger(param1);
				L.PushLightUserData(param2);
				func.PCall(top, 3);
				object[] objs = func.PopValues(top);
				func.EndPCall(top);
				return (char)objs[0];
			};
		}
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_characterLimit(IntPtr L)
	{
		object o = L.ToUserData(1);
		UnityEngine.UI.InputField obj = (UnityEngine.UI.InputField)o;

		if (obj == null)
		{
			LuaTypes types = L.Type(1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name characterLimit");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index characterLimit on a nil value");
			}
		}

		obj.characterLimit = (int)L.ChkNumber(3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_contentType(IntPtr L)
	{
		object o = L.ToUserData(1);
		UnityEngine.UI.InputField obj = (UnityEngine.UI.InputField)o;

		if (obj == null)
		{
			LuaTypes types = L.Type(1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name contentType");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index contentType on a nil value");
			}
		}

		obj.contentType = (UnityEngine.UI.InputField.ContentType)L.ChkEnumValue(3, typeof(UnityEngine.UI.InputField.ContentType));
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_lineType(IntPtr L)
	{
		object o = L.ToUserData(1);
		UnityEngine.UI.InputField obj = (UnityEngine.UI.InputField)o;

		if (obj == null)
		{
			LuaTypes types = L.Type(1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name lineType");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index lineType on a nil value");
			}
		}

		obj.lineType = (UnityEngine.UI.InputField.LineType)L.ChkEnumValue(3, typeof(UnityEngine.UI.InputField.LineType));
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_inputType(IntPtr L)
	{
		object o = L.ToUserData(1);
		UnityEngine.UI.InputField obj = (UnityEngine.UI.InputField)o;

		if (obj == null)
		{
			LuaTypes types = L.Type(1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name inputType");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index inputType on a nil value");
			}
		}

		obj.inputType = (UnityEngine.UI.InputField.InputType)L.ChkEnumValue(3, typeof(UnityEngine.UI.InputField.InputType));
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_keyboardType(IntPtr L)
	{
		object o = L.ToUserData(1);
		UnityEngine.UI.InputField obj = (UnityEngine.UI.InputField)o;

		if (obj == null)
		{
			LuaTypes types = L.Type(1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name keyboardType");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index keyboardType on a nil value");
			}
		}

		obj.keyboardType = (TouchScreenKeyboardType)L.ChkEnumValue(3, typeof(TouchScreenKeyboardType));
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_characterValidation(IntPtr L)
	{
		object o = L.ToUserData(1);
		UnityEngine.UI.InputField obj = (UnityEngine.UI.InputField)o;

		if (obj == null)
		{
			LuaTypes types = L.Type(1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name characterValidation");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index characterValidation on a nil value");
			}
		}

		obj.characterValidation = (UnityEngine.UI.InputField.CharacterValidation)L.ChkEnumValue(3, typeof(UnityEngine.UI.InputField.CharacterValidation));
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_readOnly(IntPtr L)
	{
		object o = L.ToUserData(1);
		UnityEngine.UI.InputField obj = (UnityEngine.UI.InputField)o;

		if (obj == null)
		{
			LuaTypes types = L.Type(1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name readOnly");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index readOnly on a nil value");
			}
		}

		obj.readOnly = L.ChkBoolean(3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_asteriskChar(IntPtr L)
	{
		object o = L.ToUserData(1);
		UnityEngine.UI.InputField obj = (UnityEngine.UI.InputField)o;

		if (obj == null)
		{
			LuaTypes types = L.Type(1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name asteriskChar");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index asteriskChar on a nil value");
			}
		}

		obj.asteriskChar = (char)L.ChkNumber(3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_caretPosition(IntPtr L)
	{
		object o = L.ToUserData(1);
		UnityEngine.UI.InputField obj = (UnityEngine.UI.InputField)o;

		if (obj == null)
		{
			LuaTypes types = L.Type(1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name caretPosition");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index caretPosition on a nil value");
			}
		}

		obj.caretPosition = (int)L.ChkNumber(3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_selectionAnchorPosition(IntPtr L)
	{
		object o = L.ToUserData(1);
		UnityEngine.UI.InputField obj = (UnityEngine.UI.InputField)o;

		if (obj == null)
		{
			LuaTypes types = L.Type(1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name selectionAnchorPosition");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index selectionAnchorPosition on a nil value");
			}
		}

		obj.selectionAnchorPosition = (int)L.ChkNumber(3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_selectionFocusPosition(IntPtr L)
	{
		object o = L.ToUserData(1);
		UnityEngine.UI.InputField obj = (UnityEngine.UI.InputField)o;

		if (obj == null)
		{
			LuaTypes types = L.Type(1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name selectionFocusPosition");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index selectionFocusPosition on a nil value");
			}
		}

		obj.selectionFocusPosition = (int)L.ChkNumber(3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int MoveTextEnd(IntPtr L)
	{
		L.ChkArgsCount(2);
		UnityEngine.UI.InputField obj = (UnityEngine.UI.InputField)L.ChkUnityObjectSelf(1, "UnityEngine.UI.InputField");
		var arg0 = L.ChkBoolean(2);
		obj.MoveTextEnd(arg0);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int MoveTextStart(IntPtr L)
	{
		L.ChkArgsCount(2);
		UnityEngine.UI.InputField obj = (UnityEngine.UI.InputField)L.ChkUnityObjectSelf(1, "UnityEngine.UI.InputField");
		var arg0 = L.ChkBoolean(2);
		obj.MoveTextStart(arg0);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int ScreenToLocal(IntPtr L)
	{
		L.ChkArgsCount(2);
		UnityEngine.UI.InputField obj = (UnityEngine.UI.InputField)L.ChkUnityObjectSelf(1, "UnityEngine.UI.InputField");
		var arg0 = L.ToVector2(2);
		Vector2 o = obj.ScreenToLocal(arg0);
		L.PushUData(o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int OnBeginDrag(IntPtr L)
	{
		L.ChkArgsCount(2);
		UnityEngine.UI.InputField obj = (UnityEngine.UI.InputField)L.ChkUnityObjectSelf(1, "UnityEngine.UI.InputField");
		UnityEngine.EventSystems.PointerEventData arg0 = (UnityEngine.EventSystems.PointerEventData)L.ChkUserData(2, typeof(UnityEngine.EventSystems.PointerEventData));
		obj.OnBeginDrag(arg0);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int OnDrag(IntPtr L)
	{
		L.ChkArgsCount(2);
		UnityEngine.UI.InputField obj = (UnityEngine.UI.InputField)L.ChkUnityObjectSelf(1, "UnityEngine.UI.InputField");
		UnityEngine.EventSystems.PointerEventData arg0 = (UnityEngine.EventSystems.PointerEventData)L.ChkUserData(2, typeof(UnityEngine.EventSystems.PointerEventData));
		obj.OnDrag(arg0);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int OnEndDrag(IntPtr L)
	{
		L.ChkArgsCount(2);
		UnityEngine.UI.InputField obj = (UnityEngine.UI.InputField)L.ChkUnityObjectSelf(1, "UnityEngine.UI.InputField");
		UnityEngine.EventSystems.PointerEventData arg0 = (UnityEngine.EventSystems.PointerEventData)L.ChkUserData(2, typeof(UnityEngine.EventSystems.PointerEventData));
		obj.OnEndDrag(arg0);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int OnPointerDown(IntPtr L)
	{
		L.ChkArgsCount(2);
		UnityEngine.UI.InputField obj = (UnityEngine.UI.InputField)L.ChkUnityObjectSelf(1, "UnityEngine.UI.InputField");
		UnityEngine.EventSystems.PointerEventData arg0 = (UnityEngine.EventSystems.PointerEventData)L.ChkUserData(2, typeof(UnityEngine.EventSystems.PointerEventData));
		obj.OnPointerDown(arg0);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int ProcessEvent(IntPtr L)
	{
		L.ChkArgsCount(2);
		UnityEngine.UI.InputField obj = (UnityEngine.UI.InputField)L.ChkUnityObjectSelf(1, "UnityEngine.UI.InputField");
		Event arg0 = (Event)L.ChkUserData(2, typeof(Event));
		obj.ProcessEvent(arg0);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int OnUpdateSelected(IntPtr L)
	{
		L.ChkArgsCount(2);
		UnityEngine.UI.InputField obj = (UnityEngine.UI.InputField)L.ChkUnityObjectSelf(1, "UnityEngine.UI.InputField");
		UnityEngine.EventSystems.BaseEventData arg0 = (UnityEngine.EventSystems.BaseEventData)L.ChkUserData(2, typeof(UnityEngine.EventSystems.BaseEventData));
		obj.OnUpdateSelected(arg0);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int ForceLabelUpdate(IntPtr L)
	{
		L.ChkArgsCount(1);
		UnityEngine.UI.InputField obj = (UnityEngine.UI.InputField)L.ChkUnityObjectSelf(1, "UnityEngine.UI.InputField");
		obj.ForceLabelUpdate();
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int Rebuild(IntPtr L)
	{
		L.ChkArgsCount(2);
		UnityEngine.UI.InputField obj = (UnityEngine.UI.InputField)L.ChkUnityObjectSelf(1, "UnityEngine.UI.InputField");
		var arg0 = (UnityEngine.UI.CanvasUpdate)L.ChkEnumValue(2, typeof(UnityEngine.UI.CanvasUpdate));
		obj.Rebuild(arg0);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int LayoutComplete(IntPtr L)
	{
		L.ChkArgsCount(1);
		UnityEngine.UI.InputField obj = (UnityEngine.UI.InputField)L.ChkUnityObjectSelf(1, "UnityEngine.UI.InputField");
		obj.LayoutComplete();
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GraphicUpdateComplete(IntPtr L)
	{
		L.ChkArgsCount(1);
		UnityEngine.UI.InputField obj = (UnityEngine.UI.InputField)L.ChkUnityObjectSelf(1, "UnityEngine.UI.InputField");
		obj.GraphicUpdateComplete();
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int ActivateInputField(IntPtr L)
	{
		L.ChkArgsCount(1);
		UnityEngine.UI.InputField obj = (UnityEngine.UI.InputField)L.ChkUnityObjectSelf(1, "UnityEngine.UI.InputField");
		obj.ActivateInputField();
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int OnSelect(IntPtr L)
	{
		L.ChkArgsCount(2);
		UnityEngine.UI.InputField obj = (UnityEngine.UI.InputField)L.ChkUnityObjectSelf(1, "UnityEngine.UI.InputField");
		UnityEngine.EventSystems.BaseEventData arg0 = (UnityEngine.EventSystems.BaseEventData)L.ChkUserData(2, typeof(UnityEngine.EventSystems.BaseEventData));
		obj.OnSelect(arg0);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int OnPointerClick(IntPtr L)
	{
		L.ChkArgsCount(2);
		UnityEngine.UI.InputField obj = (UnityEngine.UI.InputField)L.ChkUnityObjectSelf(1, "UnityEngine.UI.InputField");
		UnityEngine.EventSystems.PointerEventData arg0 = (UnityEngine.EventSystems.PointerEventData)L.ChkUserData(2, typeof(UnityEngine.EventSystems.PointerEventData));
		obj.OnPointerClick(arg0);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int DeactivateInputField(IntPtr L)
	{
		L.ChkArgsCount(1);
		UnityEngine.UI.InputField obj = (UnityEngine.UI.InputField)L.ChkUnityObjectSelf(1, "UnityEngine.UI.InputField");
		obj.DeactivateInputField();
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int OnDeselect(IntPtr L)
	{
		L.ChkArgsCount(2);
		UnityEngine.UI.InputField obj = (UnityEngine.UI.InputField)L.ChkUnityObjectSelf(1, "UnityEngine.UI.InputField");
		UnityEngine.EventSystems.BaseEventData arg0 = (UnityEngine.EventSystems.BaseEventData)L.ChkUserData(2, typeof(UnityEngine.EventSystems.BaseEventData));
		obj.OnDeselect(arg0);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int OnSubmit(IntPtr L)
	{
		L.ChkArgsCount(2);
		UnityEngine.UI.InputField obj = (UnityEngine.UI.InputField)L.ChkUnityObjectSelf(1, "UnityEngine.UI.InputField");
		UnityEngine.EventSystems.BaseEventData arg0 = (UnityEngine.EventSystems.BaseEventData)L.ChkUserData(2, typeof(UnityEngine.EventSystems.BaseEventData));
		obj.OnSubmit(arg0);
		return 0;
	}
}

