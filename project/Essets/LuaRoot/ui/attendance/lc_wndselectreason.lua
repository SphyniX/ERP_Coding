--
-- @file    ui/attendance/lc_wndselectreason.lua
-- @authors zl
-- @date    2016-10-08 16:53:03
-- @desc    WNDSelectReason
--

local ipairs, pairs
    = ipairs, pairs
local libugui = require "libugui.cs"
local libunity = require "libunity.cs"
local UIMGR = MERequire "ui/uimgr"
local Ref
--!*以下：自动生成的回调函数*--

local function on_submain_btnback_click(btn)
	
end

local function on_submain_btnselect_click(btn)
	
end

local function on_submain_tgltoggle_change(tgl)
	
end

local function on_subtip_btncomfire_click(btn)
	
end

local function on_subtip_btncancle_click(btn)
	
end

local function init_view()
	Ref.SubMain.btnBack.onAction = on_submain_btnback_click
	Ref.SubMain.btnSelect.onAction = on_submain_btnselect_click
	Ref.SubMain.tglToggle.onAction = on_submain_tgltoggle_change
	Ref.SubTip.btnComfire.onAction = on_subtip_btncomfire_click
	Ref.SubTip.btnCancle.onAction = on_subtip_btncancle_click
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

