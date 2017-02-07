--
-- @file    ui/login/lc_wndlogin.lua
-- @authors zhaole
-- @date    2015-12-31 13:57:50
-- @desc    WNDLogin
--

local ipairs, pairs, tostring
    = ipairs, pairs, tostring
local libugui = require "libugui.cs"
local libunity = require "libunity.cs"
local libasset = require "libasset.cs"
local libcsharpio = require "libcsharpio.cs"

local UIMGR = MERequire "ui/uimgr.lua"
local LOGIN = MERequire "libmgr/login.lua"
local UI_DATA = MERequire "datamgr/uidata.lua"
local Ref

local persistentDataPath = _G.ENV.app_persistentdata_path
local address, port

local debug_account = "45207"
local debug_password = "45207"

local function on_get_version(needUpdate)
	print("ui/login/lc_wndlogin.lua---xxx--on_get_version------x-----"..tostring(needUpdate))
	if needUpdate then return end
	LOGIN.enter_server(address, port)
end

local function on_logined(Ret)
	libunity.LogD("ui/login/lc_wndlogin.lua--xx--on_logined()--xxxxx---{0}",JSON:encode(Ret))     --输出：ui/login/lc_wndlogin.lua--xxxxx---{"ret":1,"id":75,"address":"106.14.17.160","port":6666}
	local LOGIN = MERequire "libmgr/login.lua"
	local DY_DATA = MERequire "datamgr/dydata.lua"
	DY_DATA.User = {
		id = Ret.id
	}
	UI_DATA.WNDLogin.id = Ret.id 
	UI_DATA.WNDLogin.Server = {address = Ret.address, port = Ret.port}
	address = Ret.address
	port = Ret.port
	LOGIN.try_getversion(address, port, on_get_version)
	-- if _G.Update then
	-- 	LOGIN.try_getversion(address, port, on_get_version)
	-- else
	-- 	LOGIN.enter_server(Ret.address, Ret.port)
	-- end
end

--!*以下：自动生成的回调函数*--

local function on_tglremamber_change(tgl)
	
end

local function on_btnenter_click(btn)
	print("lc_wndlogin------> on_btnenter_click")
	--UIMGR.create_window("UI/CamareTool")

	local inpAccount = Ref.SubAccount.inpAccount.text
	local inpPassword = Ref.SubPassword.inpPassword.text

		--判断是否使用端口登录
	if inpAccount == debug_account and inpPassword == debug_password then
		UIMGR.create_window("UI/WNDLaunch")
		return
	end

	local isSave = Ref.tglRemamber.value			--是否显示密码
	local LoginedAcc = {acc = inpAccount, pass = isSave and inpPassword or nil}
	UI_DATA.save_account(LoginedAcc)

	LOGIN.LoginedAcc = {acc = inpAccount, pass = inpPassword}
	LOGIN.try_login(inpAccount, inpPassword, on_logined)
end

local function on_btnregist_click(btn)
	--UIMGR.create("UI/CamareTool")
	UIMGR.create_window("UI/WNDRegist")
end

local function on_btnlostpassword_click(btn)
	UI_DATA.WNDBindPhone.on_changed = function ()
		UIMGR.create_window("UI/WNDChangePassword")
	end
	UI_DATA.WNDBindPhone.type = 2
	UIMGR.create_window("UI/WNDBindPhone")
end

local function init_view()
	Ref.tglRemamber.onAction = on_tglremamber_change
	Ref.btnEnter.onAction = on_btnenter_click
	Ref.btnRegist.onAction = on_btnregist_click
	Ref.btnLostPassword.onAction = on_btnlostpassword_click
	--!*以上：自动注册的回调函数*--
end

local function init_logic()
	-- 尝试删除apk文件

    local patchRoot = _G.ENV.app_persistentdata_path .. "/Updates/"
    libcsharpio.CreateDir(patchRoot)
    libcsharpio.DeleteFile(patchRoot.."Richer.apk")

	local filelist = libcsharpio.ReadAllText(persistentDataPath .. "/AssetBundles/filelist.bytes")
	local LFL
	if filelist == nil then
		LFL = {
			version = libasset.GetVersion(), --    /// 返回Resources资源下的资源的version文件/// 获取本地Application.dataPath + "/Issets/PersistentData/AssetBundles/+"filelist.bytes"的版本信息:var json = TinyJSON.JSON.Load(text);
		}
	else
	LFL = JSON:decode(filelist)
	end
	UI_DATA.WNDLogin.LocalFileList = LFL
	Ref.lbVersion.text = string.format( "Version : %s" ,LFL.version)

	local Acc = UI_DATA.load_account()
	if Acc then
		Ref.SubAccount.inpAccount.text = Acc.acc or ""
		Ref.SubPassword.inpPassword.text = Acc.pass or ""
	end
end

local function start(self)
	if Ref == nil or Ref.root ~= self then
		Ref = libugui.GenLuaTable(self, "root")
		init_view()
	end
	init_logic()
end

local function update_view()
	
end

local function on_recycle()
end

local P = {
	start = start,
	update_view = update_view,
	on_recycle = on_recycle,
}
return P

