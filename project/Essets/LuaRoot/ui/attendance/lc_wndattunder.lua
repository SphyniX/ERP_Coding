--
-- @file    ui/attendance/lc_wndattunder.lua
-- @authors zl
-- @date    2016-10-09 14:46:00
-- @desc    WNDAttUnder
--

local ipairs, pairs
    = ipairs, pairs
local libugui = require "libugui.cs"
local libunity = require "libunity.cs"
local UIMGR = MERequire "ui/uimgr"
local UI_DATA = MERequire "datamgr/uidata.lua"
local Ref

local function time_to_string(Time)
	return string.format("%d-%d-%d %d:%d", Time.year, Time.month, Time.day, Time.hour, Time.minute)
end

--!*以下：自动生成的回调函数*--

local function on_submain_btnback_click(btn)
	UIMGR.close(Ref.root)
end

local function on_submain_substart_click(btn)
	-- 选择开始时间
	UI_DATA.WNDSetTime.on_call_back = function (Time)
		Ref.SubMain.SubStart.lbText.text = time_to_string(Time)
	end
	UIMGR.create("UI/WNDSetTime")
end

local function on_submain_subend_click(btn)
	-- 选择开始时间
	UI_DATA.WNDSetTime.on_call_back = function (Time)
		Ref.SubMain.SubEnd.lbText.text = time_to_string(Time)
	end
	UIMGR.create("UI/WNDSetTime")
end

local function on_submain_tgl1_change(tgl)
	
end

local function on_submain_tgl2_change(tgl)
	
end

local function on_submain_tgl3_change(tgl)
	
end

local function on_submain_btnselect_click(btn)
	libunity.SetActive(Ref.SubTip.root, true)
end

local function on_subtip_btncomfire_click(btn)
	UIMGR.close(Ref.root)
end

local function on_subtip_btncancle_click(btn)
	UIMGR.close(Ref.root)
end

local function init_view()
	Ref.SubMain.btnBack.onAction = on_submain_btnback_click
	Ref.SubMain.SubStart.btn.onAction = on_submain_substart_click
	Ref.SubMain.SubEnd.btn.onAction = on_submain_subend_click
	Ref.SubMain.tgl1.onAction = on_submain_tgl1_change
	Ref.SubMain.tgl2.onAction = on_submain_tgl2_change
	Ref.SubMain.tgl3.onAction = on_submain_tgl3_change
	Ref.SubMain.btnSelect.onAction = on_submain_btnselect_click
	Ref.SubTip.btnComfire.onAction = on_subtip_btncomfire_click
	Ref.SubTip.btnCancle.onAction = on_subtip_btncancle_click
	--!*以上：自动注册的回调函数*--
end

local function init_logic()
	
	libunity.SetActive(Ref.SubTip.root, false)
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

