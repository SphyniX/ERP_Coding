--
-- @file    ui/system/lc_wndsethour.lua
-- @authors zl
-- @date    2016-08-29 07:07:05
-- @desc    WNDSetHour
--

local ipairs, pairs
    = ipairs, pairs
local libugui = require "libugui.cs"
local libunity = require "libunity.cs"
local UIMGR = MERequire "ui/uimgr"
local UI_DATA = MERequire "datamgr/uidata.lua"
local DY_DATA = MERequire "datamgr/dydata.lua"
local Ref
local Time = {}
--!*以下：自动生成的回调函数*--

local function on_btnback_click(btn)
	UIMGR.close(Ref.root)
end

local function on_subbtm_btncancle_click(btn)
	UIMGR.close(Ref.root)
end

local function on_subbtm_btnconfirm_click(btn)
	local callback = UI_DATA.WNDSetTime.on_call_back
	
	Time.hour = Ref.SubMain.inpHour.text
	if Time.hour == nil or Time.hour == "" then
		_G.UI.Toast:make(nil, "请填写小时"):show()
		return
	end
	
	Time.minute = Ref.SubMain.inpMin.text
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
	Time = {
		hour = nil,
		minute = nil,
	}
	Ref.SubMain.inpHour.text = ""
	Ref.SubMain.inpMin.text = ""
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

