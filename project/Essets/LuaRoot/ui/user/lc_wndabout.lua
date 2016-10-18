--
-- @file    ui/user/lc_wndabout.lua
-- @authors zl
-- @date    2016-10-09 15:57:50
-- @desc    WNDAbout
--

local ipairs, pairs
    = ipairs, pairs
local libugui = require "libugui.cs"
local libunity = require "libunity.cs"
local UIMGR = MERequire "ui/uimgr"
local UI_DATA = MERequire "datamgr/uidata.lua"
local Ref
--!*以下：自动生成的回调函数*--

local function on_btnback_click(btn)
	UIMGR.close_window(Ref.root)
end

local function init_view()
	Ref.btnBack.onAction = on_btnback_click
	--!*以上：自动注册的回调函数*--
end

local function init_logic()
	local LFL = UI_DATA.WNDLogin.LocalFileList
	Ref.lbVersion.text = string.format( "Version : %s" ,LFL.version)
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

