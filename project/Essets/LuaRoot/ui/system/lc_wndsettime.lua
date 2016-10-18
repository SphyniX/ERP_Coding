--
-- @file    ui/system/lc_wndsettime.lua
-- @authors zl
-- @date    2016-08-14 16:49:50
-- @desc    WNDSetTime
--

local ipairs, pairs
    = ipairs, pairs
local libugui = require "libugui.cs"
local libunity = require "libunity.cs"
local UIMGR = MERequire "ui/uimgr"
local UI_DATA = MERequire "datamgr/uidata.lua"
local Ref

local Time = {
	month = nil,
	day = nil,
	hour = nil,
	minute = nil,
}
--!*以下：自动生成的回调函数*--

local function on_btnback_click(btn)
	UIMGR.close(Ref.root)
end

local function on_subbtm_btncancle_click(btn)
	UIMGR.close(Ref.root)
end

local function on_subbtm_btnconfirm_click(btn)
	local callback = UI_DATA.WNDSetTime.on_call_back

	Time.year = Ref.SubMain.inpYear.text
	if Time.year == nil or Time.year == "" then
		_G.UI.Toast:make(nil, "请填写年"):show()
		return
	end

	Time.month = Ref.SubMain.inpMonth.text
	if Time.month == nil or Time.month == "" then
		_G.UI.Toast:make(nil, "请填写月"):show()
		return
	end
	
	Time.day = Ref.SubMain.inpDay.text
	if Time.day == nil or Time.day == "" then
		_G.UI.Toast:make(nil, "请填写日"):show()
		return
	end
	
	Time.hour = Ref.SubMain.inpHour.text
	if Time.hour == nil or Time.hour == "" then
		_G.UI.Toast:make(nil, "请填写小时"):show()
		return
	end
	
	Time.minute = Ref.SubMain.inpMinute.text
	if Time.minute == nil or Time.minute == "" then
		_G.UI.Toast:make(nil, "请填写分钟"):show()
		return
	end
	
	if callback then callback(Time) end

	UIMGR.close(Ref.root)
end

local function init_view()
	Ref.btnBack.onAction = on_btnback_click
	Ref.SubBtm.btnCancle.onAction = on_subbtm_btncancle_click
	Ref.SubBtm.btnConfirm.onAction = on_subbtm_btnconfirm_click
	--!*以上：自动注册的回调函数*--
end

local function init_logic()
	local type = UIMGR.get_ui_type()
	libunity.SetActive(Ref.SubBtm.spRed, type == 1)
	libunity.SetActive(Ref.SubBtm.spBlue, type == 2)
	libunity.SetActive(Ref.SubBtm.spYellow, type == 3)

	Time = {
		year = nil,
		month = nil,
		day = nil,
		hour = nil,
		minute = nil,
	}
	Ref.SubMain.inpYear.text = ""
	Ref.SubMain.inpMonth.text = ""
	Ref.SubMain.inpDay.text = ""
	Ref.SubMain.inpHour.text = ""
	Ref.SubMain.inpMinute.text = ""
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

