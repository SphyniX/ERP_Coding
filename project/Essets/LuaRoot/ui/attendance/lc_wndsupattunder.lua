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
local DY_DATA = MERequire "datamgr/dydata.lua"
local UI_DATA = MERequire "datamgr/uidata.lua"
local NW = MERequire "network/networkmgr"
local Ref

local Reason
local TEXT = _G.ENV.TEXT
--!*以下：自动生成的回调函数*--
local function time_to_string(Time)


	return string.format("%s-%s-%s %s:%s", Time.year, Time.month, Time.day, Time.hour, Time.minute)
end


local function on_submain_subendtime_btnbutton_click(btn)
	UI_DATA.WNDSetTime.on_call_back = function (Time)
		Ref.SubMain.SubEndTime.data.text = time_to_string(Time) .. ":00"
	end
	local SourceTime = {

		year = os.date("%Y"),
		month = os.date("%m"),
		day = os.date("%d"),
	}	
	UI_DATA.WNDSetTime.SourceTime = SourceTime
	UIMGR.create("UI/WNDSetTime")
end

local function on_submain_substarttime_btnbutton_click(btn)
	UI_DATA.WNDSetTime.on_call_back = function (Time)
		Ref.SubMain.SubStartTime.data.text = time_to_string(Time) .. ":00"
	end
	local SourceTime = {

		year = os.date("%Y"),
		month = os.date("%m"),
		day = os.date("%d"),
	}	
	UI_DATA.WNDSetTime.SourceTime = SourceTime
	UIMGR.create("UI/WNDSetTime")
end

local function on_submain_btnselect_click(btn)

	if Ref.SubMain.SubStartTime.data.text == nil or Ref.SubMain.SubStartTime.data.text == "请输入时间" or Ref.SubMain.SubEndTime.data.text == nil or Ref.SubMain.SubEndTime.data.text == "请输入时间" then
		_G.UI.Toast:make(nil,"请填写日期"):show()
	end

	local nm = NW.msg("ATTENCE.CS.LEAVE")
		nm:writeU32(DY_DATA.User.id)
		nm:writeString(Ref.SubMain.SubStartTime.data.text)
		nm:writeString(Ref.SubMain.SubEndTime.data.text)
		nm:writeString(Reason)
		if Ref.SubMain.inpInput.text ~= nil then
			nm:writeString(Ref.SubMain.inpInput.text)
		else
			nm:writeString("")
		end
		NW.send(nm)

	-- if callbackfunc ~= nil then callbackfunc() end
	-- UIMGR.close(Ref.root)
end

local function on_submain_subreason_tgl1_change(tgl)
	Reason = TEXT.Reason[1]
end

local function on_submain_subreason_tgl2_change(tgl)
	Reason = TEXT.Reason[2]
end

local function on_submain_subreason_tgl3_change(tgl)
	Reason = TEXT.Reason[3]
end

local function on_subtop_askofftabb_click(btn)
	UIMGR.create_window("UI/WNDSupAskOffTabb")
end

local function on_subtop_btnback_click(btn)
	UIMGR.close_window(Ref.root)
end


local function init_view()
	Ref.SubMain.SubEndTime.btnButton.onAction = on_submain_subendtime_btnbutton_click
	Ref.SubMain.SubStartTime.btnButton.onAction = on_submain_substarttime_btnbutton_click
	Ref.SubMain.btnSelect.onAction = on_submain_btnselect_click
	Ref.SubMain.SubReason.tgl1.onAction = on_submain_subreason_tgl1_change
	Ref.SubMain.SubReason.tgl2.onAction = on_submain_subreason_tgl2_change
	Ref.SubMain.SubReason.tgl3.onAction = on_submain_subreason_tgl3_change
	Ref.SubTop.AskOffTabb.onAction = on_subtop_askofftabb_click
	Ref.SubTop.btnBack.onAction = on_subtop_btnback_click
	--!*以上：自动注册的回调函数*--
end

local function init_logic()
	Reason = nil
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

