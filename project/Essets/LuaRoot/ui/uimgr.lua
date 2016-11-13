--
-- @file    ui/uimgr.lua
-- @authors xingweizhen
-- @date    2015-04=08 19:19:23
-- @desc    UI管理库
--

local libugui = require "libugui.cs"
local libunity = require "libunity.cs"
local libasset = require "libasset.cs"
local libsystem = require "libsystem.cs"
local WindowDEF = _G.DEF.Window

local P = {
	WNDStack = _G.DEF.Stack:new(),
	back = "",
}

-- 窗口使用注意：

-- ===========================================================
-- 窗口深度定义
-- 底框=1
local DEPTH_FRM = WindowDEF.DEPTH_FRM
-- 全屏/默认=2
local DEPTH_WND = WindowDEF.DEPTH_WND

-- 创建/关闭独立的窗口
-- @depth:	0或者nil表示自动创建在最前面
function P.create(prefab, depth, instantly)
	return WindowDEF.u_create(prefab, depth, instantly)
end

function P.close(go, instantly)
	WindowDEF.u_close(go, instantly)
end

--!窗口管理，放到一个栈内
-- 在指定的深度打开一个新窗口并压入栈，关闭原栈顶窗口
function P.create_window(prefab, depth, instantly)
  -- print("P.create_window  创建界面")
	if depth == nil then depth = DEPTH_WND end
	-- 记录因本窗口打开而关闭的窗口数量
	local n = 0
	if depth == DEPTH_WND then
		-- 关闭栈中所有可见的界面，忽略窗口关闭动画
		while true do
			local TopWnd = P.WNDStack:peek(n)
			if TopWnd and TopWnd:is_opened() then
				TopWnd:close(true)
				n = n + 1
			else
				break
			end
		end
	else
		local TopWnd = P.WNDStack:peek()
		if TopWnd then
			n = n + 1
			-- 栈顶窗口的深度如果相同就关闭，忽略窗口关闭动画
			if TopWnd.depth == depth then
				TopWnd:close(true)
				n = n + TopWnd.prevN
			end
		end
	end
	print("设置深度 完成")
	
	local NewWnd = WindowDEF.new(prefab, depth)
	print(NewWnd)
	 -- print("创建物体"..tostring(n))
	NewWnd.prevN = n
	P.WNDStack:push(NewWnd)
	 -- print("创建物体1")
	--print("Push", NewWnd, NewWnd.prevN, #P.WNDStack)
	NewWnd:open(instantly)
	 -- print("创建物体2")
	
	return NewWnd.go
end

-- 关闭栈顶窗口，并弹出栈内的下一个窗口
function P.close_window(go, instantly)
	local n = 0
	local TopWnd = P.WNDStack:peek()
	if TopWnd and TopWnd:is_opened() then
		if go == nil then
			libunity.LogW("应该传入当前窗口的引用，避免误关闭。当前窗口是{0}", tostring(TopWnd))
		elseif go ~= TopWnd.go then
			libunity.LogD("[{0}]不是当前顶部窗口，可能已被关闭，忽略。", go)
			return
		end
		P.WNDStack:pop()
		n = TopWnd.prevN
		TopWnd:close(instantly)
		--print("Pop", TopWnd, TopWnd.prevN, #P.WNDStack)
	end

	for i=0, n - 1 do
		local OldWnd = P.WNDStack:peek(i)
		if OldWnd and not OldWnd:is_opened() then
			OldWnd:open()
		end
	end
end

-- 显示栈内的窗口(alpha -> 1)
function P.show_window(count)
	if count == nil then count = -1 end
	for _,v in ipairs(P.WNDStack) do
		count = count - 1
		if v:is_opened() then
			libugui.DOTween("TweenAlpha", v.go, nil, 1, 0.2)
		end
		if count == 0 then break end
	end
end

-- 隐藏栈内的窗口(alpha -> 0)
function P.hide_window(count)
	if count == nil then count = -1 end
	for _,v in ipairs(P.WNDStack) do
		count = count - 1
		if v:is_opened() then
			libugui.DOTween("TweenAlpha", v.go, nil, 0, 0.2)
		end
		if count == 0 then break end
	end
end

-- 加载公共背景图
function P.load_uiback(background)
	if background == nil then background = P.back end
	local go = libunity.FindGameObject(nil, "/UIROOT/BackCanvas/Background") 
		or libunity.AddChild("/UIROOT/BackCanvas", "UI/Background")
	if background ~= nil and #background > 0 then
		local back = libunity.AddChild("/UIROOT/BackCanvas", "UI/Background")
		libunity.SetSibling(back, 0)
		libugui.SetAlpha(back, 1)
		local imagePath = string.format("RawImage/%s/%s", background, background)
		libugui.SetTexture(back, imagePath, nil, function (o, p)
			libugui.DOTween("UITexture", p, 1, 0, 0.3)
			libunity.Destroy(p, 0.3)
		end, go)
	else
		libugui.SetTexture(go, "")
	end

	libasset.LimitAssetBundle("RawImage", 4)
end

function P.load_photo(go, name, callBack)
	if name == nil or name == "" then 
		if callBack then callBack(false, nil, nil) end 
		return 
	end
	print("load_photo",go, name, callBack)
	libunity.LogE("load_photo {0} -- {1}", go, name)
	if go == nil or name == nil then return end
	libugui.SetPhoto( go, name, function (o, p)
		if callBack ~= nil then callBack( p, name, o) end
	end)
end
function P.on_sdk_take_photo(name, tex, callBack)
	print("开始拍照")
	if name == nil or name == "" then 
		if callBack then callBack(false, nil, nil) end 
		return 
	end
	if _G.PhotoDebug then
		P.load_photo(tex, name, callBack)
		return
	end
		print("开始拍照1")
	local UI_DATA = MERequire "datamgr/uidata.lua"
	UI_DATA.WNDPhoto.on_get_photo_callback = function (name)
		P.load_photo(tex, name, callBack)
	end
	local Param = {
		method = "doTakePhoto",
		param =  {
			type = "takephoto",
			name = name,
		},
	}
	libsystem.SubmitGameData("com.rongygame.sdk.SDKApi", "OnGameMessageReturn", Param)
end

function P.update_photo(tex, name, callBack)
	if name == nil or name == "" then 
		if callBack then callBack(false, nil, nil) end 
		return 
	end
	local LOGIN = MERequire "libmgr/login.lua"
	local DY_DATA = MERequire "datamgr/dydata.lua"
	local NW = MERequire "network/networkmgr"
	local persistentDataPath = libasset.PersistentDataPath()
	local patchRoot = persistentDataPath .. "/Image/"
	local appUrl = LOGIN.HTTPSet.downloadPhotoInterface().."/"..name
	NW.http_download(appUrl, 0, patchRoot..name, function (url, current, totalm, err)
		if current == totalm then
			callBack()
		end
	end)
end

function P.get_photo(tex, name, callBack)
	if name == nil or name == "" then 
		if callBack then callBack(false, nil, nil) end 
		return 
	end
	P.load_photo(tex, name, function ( succ, name, image)
		if succ == true then
			if callBack then callBack(succ, name, image) end
		else
			P.update_photo( tex, name, function ()
				P.load_photo(tex, name, callBack)
			end)
		end
	end)
end

-- 隐藏公共背景图
function P.hide_uiback()
	local go = libunity.FindGameObject(nil, "/UIROOT/BackCanvas/Background") 
		or libunity.AddChild("/UIROOT/BackCanvas", "UI/Background")
	libugui.SetTexture(go, "")
end

-- 在UI界面中，加载3D模型
function P.load_map(map, on_loaded)
	local GameObject = import("UnityEngine.GameObject")
	libasset.LoadAsync(GameObject.GetType(), string.format("Maps/%s/%s", map, map), true, on_loaded)
end

-- 显示／隐藏版本号
function P.show_version(visible)
	if not libunity.IsObject(P.goVersion) then
		P.goVersion = libunity.Find("/UI Root/Debug/Version")
	end
	libunity.SetActive(P.goVersion, visible)
end

function P.get_ui_type()
	local DY_DATA = MERequire "datamgr/dydata.lua"
	return DY_DATA.User.limit or 1
end

-- 显示名称＋编号
function P.cfgname(Cfg)
	if _G.ENV.debug then
		return Cfg.name.."#"..Cfg.id
	else return Cfg.name end
end

-- 
-- 下面这些是Group界面容器的生成方法
-- @ Grp是容器的表，初始值
-- 		{ root = <GameObject>, Ent = { go = <GameObject>, ... }, Ents = {}, }
-- @ reg是为容器内每个控件的触发函数赋值的函数
-- 

local GroupDEF = {}
GroupDEF.__index = GroupDEF

do
	-- 生成模板Ent
	function GroupDEF:init(prefab, entName)
		local ent = libunity.NewChild(self.root, "UI/"..prefab, entName or "ent")	
		local Ent = libugui.GenLuaTable(ent, "go")
		if self.reg then self.reg(nil, Ent) end
		libunity.SetActive(ent, false)
		self.Ent = Ent
		return Ent
	end

	-- 生成一个Ent
	function GroupDEF:gen(i)
		local Ent = self.Ents[i]
		if Ent == nil then
			local ent = self.Ent.go
			local go = libunity.NewChild(self.root, ent, ent.name..i)
			Ent =  libugui.GenLuaTable(go, "go")
			self.Ents[i] = Ent
			if self.reg then self.reg(Ent, self.Ent) end
		else
			libunity.SetActive(Ent.go, true)
		end
		return Ent
	end

	-- 生成并初始化多个Ent
	function GroupDEF:dup(n, cbf)
		local Ents = self.Ents
		local nEnt = #Ents
		for i=1,n do 
			local Ent = self:gen(i)
	        libunity.SetActive(Ent.go, true)
			if cbf then cbf(i, Ent, i > nEnt) end
		end
	    for i=n+1,nEnt do
	        libunity.SetActive(Ents[i].go, false)
	    end
	end

	-- 初始化多个Ent
	function GroupDEF:view(List, cbf)
		for i,v in ipairs(self.Ents) do
			local Obj = List[i]
			if Obj then
				libunity.SetActive(v.go, true)
				cbf(Obj, v) 
			else
				libunity.SetActive(v.go, false)
			end
		end
	end

	-- 隐藏所有Ent
	function GroupDEF:hide()
		for _,v in ipairs(self.Ents) do
			libunity.SetActive(v.go, false)
		end
	end

	-- 生成和合并一个Ent
	function GroupDEF:gen_combine(i, prefab, subName)
		if subName == nil then subName = "Sub" end
	    local Ent = self:gen(i)
	    if Ent[subName] == nil then
	        local go = libunity.NewChild(Ent.go, "UI/"..prefab, subName)
	        libunity.DelComponent(go, "Selectable")
	        Ent[subName] = libugui.GenLuaTable(go, "root")
	    end
	    return Ent
	end

	-- 生成和合并并初始化多个Ent
	function GroupDEF:dup_combine(n, prefab, cbf, subName)
		if subName == nil then subName = "Sub" end
	    local GameObject = import("UnityEngine.GameObject")
	    local prefab = libasset.Load(GameObject.GetType(), "UI/"..prefab)
	    self:dup(n, function (i, Ent, isNew)
	        if Ent[subName] == nil then
	            local go = libunity.NewChild(Ent.go, prefab, subName)
	            libunity.DelComponent(go, "Selectable")
	            Ent[subName] = libugui.GenLuaTable(go, "root")
	        end
	        if cbf then cbf(i, Ent, isNew) end
	    end)
	end

end
-- 完成"UI组"类的定义 @

-- 生成组函数（自动），保存控件的生成方法在容器表内
function P.make_group(Grp, reg)
	Grp.reg = reg
	return setmetatable(Grp, GroupDEF)
end

-- 生成组函数（手动），保存控件的生成方法在容器表内
function P.complete_group(prefab, Grp, reg, entName)
	if Grp.Ents == nil then Grp.Ents = {} end
	P.make_group(Grp, reg)
	-- 生成一个初始Ent
	if prefab then Grp:init(prefab, entName) end
	return Grp
end

-- 在组内生成多个控件，不同于complete_group
-- 该接口不在组表内保存生成函数
function P.dup_group(prefab, Grp, reg, n, on_init, entName)
	if (not entName) and Grp.Ent then entName = Grp.Ent.go.name end
	if Grp.Ents == nil then Grp.Ents = {} end
	local Ent = Grp.Ent or GroupDEF.init(Grp, prefab, entName)	
	local root, Ents = Grp.root, Grp.Ents
    local nEnt, goEnt = #Ents, Ent.go
    for i = 1, n do
        local New = Ents[i]
        if New == nil then
            local go = libunity.NewChild(root, goEnt, entName .. i)
            New = libugui.GenLuaTable(go, "go")
            if reg then reg(New, Ent) end
            Ents[i] = New 
        end
        libunity.SetActive(New.go, true)
        if on_init then on_init(i, Ents[i], i > nEnt) end
    end
    for i=n+1,nEnt do
        libunity.SetActive(Ents[i].go, false)
    end
end

function P.dup_new_group(Root, GoRoot, GrpName, prefab, n, cbf)
	local Grp = Root[GrpName]
	if Grp == nil then
		local goGrp = libunity.NewChild(GoRoot, prefab, GrpName)
		Grp = libugui.GenLuaTable(goGrp,"root")
	end
	Root[GrpName] = Grp
	if Grp.Ents == nil then Grp.Ents = {} end
	local Ents = Grp.Ents
	local nEnt = #Ents
	for i=1,n do
		local Ent = Ents[i]
		if Ent == nil then
			local ent = Grp.Ent.go
			local go = libunity.NewChild(Grp.root, ent, ent.name..i)
			Ent = libugui.GenLuaTable(go,"go")
			Grp.Ents[i] = Ent
		end
		libunity.SetActive(Ent.go,true)
		if cbf then cbf(i,Ent, i > nEnt) end
	end
	for i = n + 1,nEnt do
		libunity.SetActive(Ents[i].go,false)
	end 
end


-- 动态创建一个UI控件 
function P.gen_prefab(prefab, Root, subName)
    if subName == nil then subName = "Sub" end

    local Sub = Root[subName]
    if Sub == nil then
        local root = Root.root or Root.go
        local go = libunity.NewChild(root, "UI/"..prefab, subName)
        Sub = libugui.GenLuaTable(go, "root")
        Root[subName] = Sub
    end
    return Sub
end

return P
 