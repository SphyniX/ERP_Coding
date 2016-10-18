--
-- @file    ui/login/lc_wndverifyphone.lua
-- @authors ckxz
-- @date    2016-07-04 17:27:51
-- @desc    WNDVerifyPhone
--

local ipairs, pairs
    = ipairs, pairs
local libugui = require "libugui.cs"
local libunity = require "libunity.cs"
local UIMGR = MERequire "ui/uimgr"
local LOGIN = MERequire "libmgr/login.lua"
local UI_DATA = MERequire "datamgr/uidata.lua"
local Ref
local phone = ""

local function on_bind_phoned(Ret)
	if Ret.ret == 1 then
		UIMGR.create_window("UI/WNDSetUserInfo")
	end
end

local function on_get_verifyed(Ret)
	if Ret.ret == 1 then
		
	end
end
--!*以下：自动生成的回调函数*--

local function on_submain_subverifycode_btntime_click(btn)
	LOGIN.try_get_verify(phone, on_get_verifyed)
end

local function on_submain_btnenter_click(btn)
	local idf = Ref.SubMain.SubVerifyCode.inpCode.text
	LOGIN.try_bind_phone(phone, idf, on_bind_phoned)
end

local function on_subtop_btnback_click(btn)
	UIMGR.close_window(Ref.root)
end

local function init_view()
	Ref.SubMain.SubVerifyCode.btnTime.onAction = on_submain_subverifycode_btntime_click
	Ref.SubMain.btnEnter.onAction = on_submain_btnenter_click
	Ref.SubTop.btnBack.onAction = on_subtop_btnback_click
	--!*以上：自动注册的回调函数*--
end

local function init_logic()
	local UserInfo = UI_DATA.WNDRegist.UserInfo
	phone = UserInfo.phone
	Ref.SubMain.SubPhone.lbPhone.text = phone
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

