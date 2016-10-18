-- File Name : framework/reflection.lua
local type, string_format, setmetatable
	= type, string.format, setmetatable

local System_Type = _G.System.Type
local System_Type_GetType = System_Type.GetType
local function check_type(typeName)
	local _type = System_Type_GetType(nil, typeName)
	 if _type then
		return _type
	else
		error(string_format("Type reflect fail: %s", typeName))
	end
end

setmetatable(_G.System,  {
	__index = function (t, n)
		local value = check_type(string_format("System.%s", n))
		t[n] = value
        return value
	end	
})

-- 开始注册反射
local LuaReflection = _G.LuaReflection

-- 生成反射管理器
local function get_reflector(typeName)
	local _type = typeName
	if type(_type) == "string" then
		_type = System_Type:GetType(typeName)
	end
	if _type then
		return LuaReflection:GetReflector(_type)
	else
		error("Type reflect fail: "..tostring(typeName))
	end
end

-- Type数组生成
local function type_array(Types)
	local Array = LuaReflection:GetTypeArray(#Types)
	for i,v in ipairs(Types) do
		Array:SetValue(v, i - 1)
	end	
	return Array
end

do
	local System = System
	-- 反射：System
	do 
		local reflector = get_reflector(System.Object)
		reflector:AddMethod("GetType", nil)
	end
	do 
		local reflector = get_reflector(System_Type)
		reflector:AddMethod("IsAssignableFrom", type_array({ System_Type }))
	end
	do
		local reflector = get_reflector("System.Collections.Generic.List`1[[System.String]]")
		reflector:AddMethod("Add", type_array({System.String}))
		reflector:AddMethod("Remove", type_array({System.String}))
		reflector:AddMethod("RemoveAt", type_array({System.Int32}))
		reflector:AddMethod("Clear", nil)
	end

	do
		local reflector = get_reflector(System.DateTime)
		reflector:AddCtor(type_array( { System.Int32, System.Int32, System.Int32, System.Int32, System.Int32, System.Int32 } ))
		reflector:AddMethod("AddSeconds", type_array( {System.Double} ))
	end
end

do
	-- 类型表，记录宿主语言的类型
	local HOST = setmetatable({}, {
			__index = function (t, n)
				local value = check_type(string_format(
					"%s, Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null", n))
				t[n] = value
		        return value
		    end,
		})

	_G.HOST = HOST
end

do
	local CMD5 = check_type("CMD5, Assembly-CSharp-firstpass, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null")
	-- 反射MD5方法
	do
		local reflector = get_reflector(CMD5)
		reflector:AddMethod("MD5File", type_array({ System.String }))
		reflector:AddMethod("MD5String", type_array({ System.String }))	
	end
	_G.CMD5 = CMD5
end

do
	local UnityEngine = setmetatable({}, {
			__index = function (t, n)
				local value = check_type(string_format(
					"UnityEngine.%s, UnityEngine, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null", n))
				t[n] = value
		        return value
		    end,
		})
	UnityEngine.UI = setmetatable({}, {
			__index = function (t, n)
				local value = check_type(string_format(
					"UnityEngine.UI.%s, UnityEngine.UI, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null", n))
				t[n] = value
		        return value
		    end,	
		})
	UnityEngine.Events = setmetatable({}, {
		__index = function (t, n)
				local value = check_type(string_format(
					"UnityEngine.Events.%s, UnityEngine, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null", n))
				t[n] = value
		        return value
		    end,	
		})

	-- 反射：UnityEngine
	do
		local reflector = get_reflector(UnityEngine.Object)
		reflector:AddMethod("Destroy", type_array({ UnityEngine.Object, System.Single }))
		reflector:AddMethod("DontDestroyOnLoad", type_array({ UnityEngine.Object }))
	end
	do
		local reflector = get_reflector(UnityEngine.Shader)
		reflector:AddMethod("Find", type_array({ System.String }))
	end
	do
		local reflector = get_reflector(UnityEngine.Material)
		reflector:AddMethod("SetColor", type_array({ System.String, UnityEngine.Color }))
		reflector:AddMethod("SetFloat", type_array({ System.String, System.Single }))
		reflector:AddMethod("SetInt", type_array({ System.String, System.Int32 }))
		reflector:AddMethod("SetTexture", type_array({ System.String, UnityEngine.Texture }))
		reflector:AddMethod("GetColor", type_array({ System.String }))
		reflector:AddMethod("GetFloat", type_array({ System.String }))
		reflector:AddMethod("GetInt", type_array({ System.String }))
		reflector:AddMethod("GetTexture", type_array({ System.String }))
	end
	do
		local reflector = get_reflector(UnityEngine.GameObject)
	    reflector:AddMethod("SetActive", type_array({ System.Boolean }))
	    reflector:AddMethod("AddComponent", type_array({ System_Type }))
	    reflector:AddMethod("GetComponent", type_array({ System.String }))
	    reflector:AddMethod("Find", type_array({ System.String }))
	end
	do
		local reflector = get_reflector(UnityEngine.Component)
	    reflector:AddMethod("GetComponent", type_array({ System.String }))
	end
	do
		local reflector = get_reflector(UnityEngine.Transform)
	    reflector:AddMethod("Find", type_array({ System.String }))
	end

	do
		local reflector = get_reflector(UnityEngine.Quaternion)
		reflector:AddCtor(type_array({ System.Single, System.Single, System.Single, System.Single }))
		reflector:AddMethod("Euler", type_array({ System.Single, System.Single, System.Single }))
	end

	do
		local reflector = get_reflector(UnityEngine.Vector2)
		reflector:AddCtor(type_array({ System.Single, System.Single }))
	end

	do
		local reflector = get_reflector(UnityEngine.Vector3)
		reflector:AddCtor(type_array({ System.Single, System.Single, System.Single }))
	end

	do
		local reflector = get_reflector(UnityEngine.Vector4)
		reflector:AddCtor(type_array({ System.Single, System.Single, System.Single, System.Single }))
	end

	do
		local reflector = get_reflector(UnityEngine.Color)
		reflector:AddCtor(type_array({ System.Single, System.Single, System.Single, System.Single }))
	end

	do 
		local reflector = get_reflector(UnityEngine.WWW)
		reflector:AddMethod("EscapeURL", type_array({ System.String }))
		reflector:AddMethod("UnEscapeURL", type_array({ System.String }))
	end

	do
		local reflector = get_reflector(UnityEngine.Debug)
		reflector:AddMethod("Log", type_array({ System.String, UnityEngine.Object }))
		reflector:AddMethod("LogWarning", type_array({ System.String, UnityEngine.Object }))
		reflector:AddMethod("LogError", type_array({ System.String, UnityEngine.Object }))
	end

	do
		local reflector = get_reflector(UnityEngine.PlayerPrefs)
		reflector:AddMethod("DeleteAll", nil)
		reflector:AddMethod("DeleteKey", type_array({ System.String }))
		reflector:AddMethod("GetFloat", type_array({ System.String, System.Single }))
		reflector:AddMethod("GetInt", type_array({ System.String, System.Int32 }))
		reflector:AddMethod("GetString", type_array({ System.String, System.String }))
		reflector:AddMethod("HasKey", type_array({ System.String }))
		reflector:AddMethod("Save", nil)
		reflector:AddMethod("SetFloat", type_array({ System.String, System.Single }))
		reflector:AddMethod("SetInt", type_array({ System.String, System.Int32 }))
		reflector:AddMethod("SetString", type_array({ System.String, System.String }))
	end

	do
		local reflector = get_reflector(UnityEngine.Animator)
		reflector:AddMethod("Play", type_array({ System.String }))
	end

	do
		local reflector = get_reflector(UnityEngine.Animation)
		reflector:AddMethod("Play", type_array({ System.String }))
	end

	do
		local reflector = get_reflector(UnityEngine.Application)
		reflector:AddMethod("OpenURL", type_array({ System.String }))
	end

	do 
		local reflector = get_reflector(UnityEngine.Screen)
		reflector:AddMethod("SetResolution", type_array({ System.Int32, System.Int32, System.Boolean }))
	end

	do
		local reflector = get_reflector(UnityEngine.Events.UnityEvent)
		reflector:AddMethod("AddListener", type_array({UnityEngine.Events.UnityAction}))
	end

	do
		local reflector = get_reflector(UnityEngine.WaitForSeconds)
		reflector:AddCtor(type_array({ System.Single }))
	end

	_G.UnityEngine = UnityEngine
end

do
	local ZFrame = setmetatable({}, {
			__index = function (t, n)
				local value = check_type(string_format(
					"ZFrame.%s, Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null", n))
				t[n] = value
		        return value
		    end,
		})

	-- 反射：NetEngine
	local NetEngine = setmetatable({}, {
			__index = function (t, n)
				local value = check_type(string_format(
					"clientlib.net.%s, Assembly-CSharp-firstpass, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null", n))
				t[n] = value
		        return value
		    end,
		})

	do
		local reflector = get_reflector(NetEngine.NetClient)
		reflector:AddMethod("Connect", type_array({ System.String, System.Int32 }))
		reflector:AddMethod("Close", nil)
		reflector:AddMethod("send", type_array( { NetEngine.INetMsg }))
	end

	do
		local reflector = get_reflector(NetEngine.NetMsg)
		reflector:AddMethod("createMsg", type_array({ System.Int16, System.Int32 }))
		reflector:AddMethod("createReadMsg", type_array({ System.Int16 }))
		reflector:AddMethod("read", nil)
		reflector:AddMethod("readU32", nil)
		reflector:AddMethod("readU64", nil)
		reflector:AddMethod("readFloat", nil)
		reflector:AddMethod("readDouble", nil)
		reflector:AddMethod("readString", nil)
		reflector:AddMethod("write", type_array({ System.Byte }))
		reflector:AddMethod("writeU32", type_array({ System.Int32 }))
		reflector:AddMethod("writeU64", type_array({ System.String }))
		reflector:AddMethod("writeString", type_array({ System.String }))	
	end
	ZFrame.NetEngine = NetEngine

	_G.ZFrame = ZFrame
end

do
-- 反射：UGUI
	local UGUI = setmetatable({}, {
			__index = function (t, n)
				local value = check_type(string_format(
					"ZFrame.UGUI.%s, Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null", n))
				t[n] = value
		        return value
		    end,
		})
	do
		local reflector = get_reflector(UGUI.UISprite)
		reflector:AddMethod("SetNativeSize", nil)
	end
	_G.UGUI = UGUI	
end

-- 战斗
_G.Battle = setmetatable({}, {
		__index = function (t, n)
			local value = check_type(string_format(
				"Battle.%s, Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null", n))
			t[n] = value
	        return value
		end	
	})
_G.Battle.Algorithm = setmetatable({}, {
		__index = function (t, n)
			local value = check_type(string_format(
				"Battle.Algorithm.%s, Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null", n))
			t[n] = value
	        return value
		end
	})
_G.Battle.UI = setmetatable({}, {
		__index = function (t, n)
			local value = check_type(string_format(
				"Battle.UI.%s, Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null", n))
			t[n] = value
	        return value
		end
	})

do
	local reflector = get_reflector(_G.Battle.Algorithm.SectorArea)
	reflector:AddCtor(type_array({UnityEngine.Vector3, UnityEngine.Vector3, System.Single, System.Single,}))
end

return {}



