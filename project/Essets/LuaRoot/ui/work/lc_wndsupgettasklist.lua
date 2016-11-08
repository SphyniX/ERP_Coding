--
-- @file    ui/work/lc_wndsupgettasklist.lua
-- @authors cks
-- @date    2016-11-07 07:06:40
-- @desc    WNDSupGetTaskList
--

local ipairs, pairs
    = ipairs, pairs
local libugui = require "libugui.cs"
local libunity = require "libunity.cs"
local UIMGR = MERequire "ui/uimgr"
local Ref
--!*以下：自动生成的回调函数*--

local function on_subtop_btnback_click(btn)
	
end

local function on_subtop_btnnew_click(btn)
	
end

local function on_submain_grp_ent _subbtnbutton_click(btn)
	UIMGR.create_window("UI/WNDSupTaskList")
end

local function init_view()
	Ref.SubTop.btnBack.onAction = on_subtop_btnback_click
	Ref.SubTop.btnNew.onAction = on_subtop_btnnew_click
	Ref.SubMain.Grp.Ent.SubbtnButton.btn.onAction = on_submain_grp_ent _subbtnbutton_click
	UIMGR.make_group(Ref.SubMain.Grp, function (New, Ent)
		New.SubbtnButton.btn.onAction = Ent.SubbtnButton.btn.onAction
	end)
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

