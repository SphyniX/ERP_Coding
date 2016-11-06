--
-- @file    ui/work/lc_workpopup.lua
-- @authors cks
-- @date    2016-11-04 01:19:25
-- @desc    WorkPopup
--

local ipairs, pairs
    = ipairs, pairs
local libugui = require "libugui.cs"
local libunity = require "libunity.cs"
local UIMGR = MERequire "ui/uimgr"
local Ref
--!*以下：自动生成的回调函数*--

local function on_subbg_btndate_click(btn)
	
end

local function on_subbg_btntask_click(btn)
	
end

local function on_spimage_subpage_click(btn)
	
end

local function on_spimage_subtask_click(btn)
	
end

local function init_view()
	Ref.Subbg.btnDate.onAction = on_subbg_btndate_click
	Ref.Subbg.btnTask.onAction = on_subbg_btntask_click
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

