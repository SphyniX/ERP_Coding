--
-- @file    ui/attendance/lc_wndsupattunder.lua
-- @authors cks
-- @date    2016-11-30 15:18:10
-- @desc    WNDSupAttUnder
--

local ipairs, pairs
    = ipairs, pairs
local libugui = require "libugui.cs"
local libunity = require "libunity.cs"
local UIMGR = MERequire "ui/uimgr"
local Ref
--!*以下：自动生成的回调函数*--

local function on_submain_btnselect_click(btn)
	
end

local function on_submain_subreason_tgl1_change(tgl)
	
end

local function on_submain_subreason_tgl2_change(tgl)
	
end

local function on_submain_subreason_tgl3_change(tgl)
	
end

local function on_subtop_askofftabb_click(btn)
	UIMGR.create_window("UI/WNDSupAskOffTabb")
end

local function on_subtop_btnback_click(btn)
	UIMGR.close_window(Ref.root)
end

local function on_submain_tgl1_change(tgl)
	
end

local function on_submain_tgl2_change(tgl)
	
end

local function on_submain_tgl3_change(tgl)
	
end

local function init_view()
	Ref.SubMain.btnSelect.onAction = on_submain_btnselect_click
	Ref.SubMain.SubReason.tgl1.onAction = on_submain_subreason_tgl1_change
	Ref.SubMain.SubReason.tgl2.onAction = on_submain_subreason_tgl2_change
	Ref.SubMain.SubReason.tgl3.onAction = on_submain_subreason_tgl3_change
	Ref.SubTop.AskOffTabb.onAction = on_subtop_askofftabb_click
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

local function on_recycle()
	
end

local P = {
	start = start,
	update_view = update_view,
	on_recycle = on_recycle,
}
return P

