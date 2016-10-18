--
-- @file    ui/login/lc_wndlaunch.lua
-- @authors zhaole
-- @date    2015-12-18 13:36:30
-- @desc    WNDLaunch
--

local ipairs, pairs, tostring
    = ipairs, pairs, tostring
local libugui = require "libugui.cs"
local libunity = require "libunity.cs"
local libsystem = require "libsystem.cs"

local UIMGR = MERequire "ui/uimgr.lua"
local LOGIN = MERequire "libmgr/login.lua"
local UI_DATA = MERequire "datamgr/uidata.lua"
local TEXT = _G.ENV.TEXT
local NW = MERequire "network/networkmgr"
local Ref
--!*以下：自动生成的回调函数*--

local function on_tgltest_change(tgl)
	
end

local function on_btndebug_click(btn)
	local test = Ref.tglTest.value
	local LOGIN = MERequire "libmgr/login.lua"
	LOGIN.Channel.loginHost = test and LOGIN.IPSet.test or LOGIN.IPSet.productIP
	UIMGR.create_window("UI/WNDTest")
end

local function on_btnlogin_click(btn)
	local test = Ref.tglTest.value
	local LOGIN = MERequire "libmgr/login.lua"
	LOGIN.Channel.loginHost = test and LOGIN.IPSet.test or LOGIN.IPSet.productIP
	UIMGR.create_window("UI/WNDLogin")
end

local function on_subip_btnsend_click(btn)
	local test = Ref.SubIP.inpInput.text
	local LOGIN = MERequire "libmgr/login.lua"
	LOGIN.Channel.loginHost = test
	UIMGR.create_window("UI/WNDLogin")
end

local function init_view()
	Ref.tglTest.onAction = on_tgltest_change
	Ref.btnDebug.onAction = on_btndebug_click
	Ref.btnLogin.onAction = on_btnlogin_click
	Ref.SubIP.btnSend.onAction = on_subip_btnsend_click
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

