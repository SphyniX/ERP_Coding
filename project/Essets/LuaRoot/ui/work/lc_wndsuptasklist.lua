--
-- @file    ui/work/lc_wndsuptasklist.lua
-- @authors cks
-- @date    2016-11-07 06:50:50
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

local function on_substarttime_btnbutton_click(btn)
	
end

local function on_subendttime_btnbutton_click(btn)
	
end

local function on_subwork_btnbutton_click(btn)
	
end

local function on_submain_grp_ent_subbtnbutton_click(btn)
	
end

local function init_view()
	Ref.SubTop.btnBack.onAction = on_subtop_btnback_click
	Ref.SubTop.btnNew.onAction = on_subtop_btnnew_click
	Ref.SubStartTime.btnButton.onAction = on_substarttime_btnbutton_click
	Ref.SubEndtTime .btnButton.onAction = on_subendttime _btnbutton_click
	Ref.SubWork.btnButton.onAction = on_subwork_btnbutton_click
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

