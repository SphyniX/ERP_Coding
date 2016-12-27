--
-- @file    ui/schedule/lc_wndsetinfor.lua
-- @authors zl
-- @date    2016-10-17 08:32:37
-- @desc    WNDSetInfor
--

local ipairs, pairs
    = ipairs, pairs
local libugui = require "libugui.cs"
local libunity = require "libunity.cs"
local UIMGR = MERequire "ui/uimgr"
local UI_DATA = MERequire "datamgr/uidata.lua"
local DY_DATA = MERequire "datamgr/dydata.lua"
local Ref

--!*以下：自动生成的回调函数*--

local function on_submain_btnsave_click(btn)
	if DY_DATA.WNDSubmitSchedule.Infor == nil then DY_DATA.WNDSubmitSchedule.Infor = "" end
	DY_DATA.WNDSubmitSchedule.Infor = Ref.SubMain.inpInput.text
	UIMGR.close_window(Ref.root)
end

local function on_subtop_btnclear_click(btn)
	Ref.SubMain.inpInput.text = nil
end

local function on_subtop_btnback_click(btn)
	UIMGR.close_window(Ref.root)
end

local function init_view()
	Ref.SubMain.btnSave.onAction = on_submain_btnsave_click
	Ref.SubTop.btnClear.onAction = on_subtop_btnclear_click
	Ref.SubTop.btnBack.onAction = on_subtop_btnback_click

	-- if DY_DATA.WNDSubmitSchedule.Infor ~= nil then
	-- 	Ref.SubMain.inpInput.text = DY_DATA.WNDSubmitSchedule.Infor 
	-- end
	if DY_DATA.WNDSubmitScheduleData.Infor ~= nil then
		Ref.SubMain.inpInput.text = DY_DATA.WNDSubmitScheduleData.Infor
	end
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

