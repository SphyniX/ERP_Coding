--
-- @file    ui/login/lc_wndsetpassword.lua
-- @authors ckxz
-- @date    2016-07-04 19:51:52
-- @desc    WNDSetPassword
--

local ipairs, pairs
    = ipairs, pairs
local libugui = require "libugui.cs"
local libunity = require "libunity.cs"
local UIMGR = MERequire "ui/uimgr"
local UI_DATA = MERequire "datamgr/uidata.lua"
local LOGIN = MERequire "libmgr/login.lua"
local Ref

local function on_registed()
	UI_DATA.WNDMain.regist = true
	UIMGR.create_window("UI/WNDMain")
end
--!*以下：自动生成的回调函数*--

local function on_submain_tglshow_change(tgl)
	if tgl.value == true then
		local txt = Ref.SubMain.SubPassword.inpPassword.text
		Ref.SubMain.SubPassword.inpPassword.text = ""
		Ref.SubMain.SubPassword.inpPassword.contentType = "Standard"
		Ref.SubMain.SubPassword.inpPassword.text = txt

		txt = Ref.SubMain.SubPasswordTwo.inpPassword.text
		Ref.SubMain.SubPasswordTwo.inpPassword.text = ""
		Ref.SubMain.SubPasswordTwo.inpPassword.contentType = "Standard"
		Ref.SubMain.SubPasswordTwo.inpPassword.text = txt
	else
		local txt = Ref.SubMain.SubPassword.inpPassword.text
		Ref.SubMain.SubPassword.inpPassword.text = ""
		Ref.SubMain.SubPassword.inpPassword.contentType = "Password"
		Ref.SubMain.SubPassword.inpPassword.text = txt

		local txt = Ref.SubMain.SubPasswordTwo.inpPassword.text
		Ref.SubMain.SubPasswordTwo.inpPassword.text = ""
		Ref.SubMain.SubPasswordTwo.inpPassword.contentType = "Password"
		Ref.SubMain.SubPasswordTwo.inpPassword.text = txt
	end
end

local function on_submain_btnenter_click(btn)
	local password = Ref.SubMain.SubPassword.inpPassword.text
	local password2 = Ref.SubMain.SubPasswordTwo.inpPassword.text
	if password == "" then
		_G.UI.Toast:make(nil, "密码为空"):show()
		return
	end
	if password ~= password2 then
		_G.UI.Toast:make(nil, "密码不相同"):show()
		return
	end
	
	local UserInfo = UI_DATA.WNDRegist.UserInfo
	UserInfo.password = password
	LOGIN.try_regist(UserInfo, on_registed)
end

local function on_subtop_btnback_click(btn)
	UIMGR.close_window(Ref.root)
end

local function init_view()
	Ref.SubMain.tglShow.onAction = on_submain_tglshow_change
	Ref.SubMain.btnEnter.onAction = on_submain_btnenter_click
	Ref.SubTop.btnBack.onAction = on_subtop_btnback_click
	--!*以上：自动注册的回调函数*--
end

local function init_logic()
	
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

local P = {
	start = start,
	update_view = update_view,
}
return P

