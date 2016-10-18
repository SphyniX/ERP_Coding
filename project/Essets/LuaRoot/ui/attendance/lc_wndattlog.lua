--
-- @file    ui/attendance/lc_wndattlog.lua
-- @authors zl
-- @date    2016-10-08 17:01:52
-- @desc    WNDAttLog
--

local ipairs, pairs
    = ipairs, pairs
local libugui = require "libugui.cs"
local libunity = require "libunity.cs"
local UIMGR = MERequire "ui/uimgr"
local Ref
--!*以下：自动生成的回调函数*--

local function on_subtop_submouth_btnnew_click(btn)
	libunity.SetActive(Ref.SubTop.SubMouth.btnNew, false)
	libunity.SetActive(Ref.SubTop.SubMouth.btnLast, true)
end

local function on_subtop_submouth_btnlast_click(btn)
	libunity.SetActive(Ref.SubTop.SubMouth.btnNew, true)
	libunity.SetActive(Ref.SubTop.SubMouth.btnLast, false)
end

local function on_subtop_btnback_click(btn)
	UIMGR.close_window(Ref.root)
end

local function init_view()
	Ref.SubTop.SubMouth.btnNew.onAction = on_subtop_submouth_btnnew_click
	Ref.SubTop.SubMouth.btnLast.onAction = on_subtop_submouth_btnlast_click
	Ref.SubTop.btnBack.onAction = on_subtop_btnback_click
	UIMGR.make_group(Ref.SubLog.GrpLog)
	--!*以上：自动注册的回调函数*--
end

local function init_logic()
	libunity.SetActive(Ref.SubTop.SubMouth.btnNew, true)
	libunity.SetActive(Ref.SubTop.SubMouth.btnLast, false)
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

