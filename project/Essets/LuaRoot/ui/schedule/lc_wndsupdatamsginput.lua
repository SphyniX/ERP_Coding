--
-- @file    ui/schedule/lc_wndsupdatamsginput.lua
-- @authors zl
-- @date    2016-11-09 02:46:21
-- @desc    WNDSupDataMsgInput
--

local ipairs, pairs
    = ipairs, pairs
local libugui = require "libugui.cs"
local libunity = require "libunity.cs"
local UIMGR = MERequire "ui/uimgr"
local Ref
--!*以下：自动生成的回调函数*--

local function on_subtop_btnclean_click(btn)
	
end

local function on_subtop_btnback_click(btn)
	
end

local function on_btnsave_click(btn)
	
end

local function init_view()
	Ref.SubTop.btnClean.onAction = on_subtop_btnclean_click
	Ref.SubTop.btnBack.onAction = on_subtop_btnback_click
	Ref.btnSave.onAction = on_btnsave_click
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

local function on_recycle()
	
end

local P = {
	start = start,
	update_view = update_view,
	on_recycle = on_recycle,
}
return P

