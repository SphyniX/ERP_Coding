--
-- @file    ui/attendance/lc_wndmainattendance.lua
-- @authors zl
-- @date    2016-09-26 10:17:31
-- @desc    WNDMainAttendance
--
local ipairs, pairs
    = ipairs, pairs
local libugui = require "libugui.cs"
local libunity = require "libunity.cs"
local libsystem = require "libsystem.cs"

local UIMGR = MERequire "ui/uimgr"
local DY_DATA = MERequire "datamgr/dydata.lua"
local UI_DATA = MERequire "datamgr/uidata.lua"
local TEXT = _G.ENV.TEXT
local NW = MERequire "network/networkmgr"
local LOGIN = MERequire "libmgr/login.lua"
local Attendance = MERequire "libmgr/attendance.lua"

local Ref

local undergoreason ,leavereason = nil, nil
local AttendanceProject = nil
local projectId = nil

local on_project_init

local punch_type -- 1 上班， 2 下班

local function time_to_string(Time)
	return string.format("%d-%d-%d %d:%d", Time.year, Time.month, Time.day, Time.hour, Time.minute)
end

local function on_select_project(id)
	projectId = id
	on_project_init()
end

local function on_try_punch(Ret)
	if Ret.ret == 1 then
		local workstate = DY_DATA.User.workstate -- 1 下班， 2， 上班中， 3 离岗
		Attendance.on_try_punch(punch_type, projectId)
	end
end

--!*以下：自动生成的回调函数*--

local function on_submain_subproject_click(btn)
	-- 选择项目
	UI_DATA.WNDSelectProject.on_call_back = on_select_project
	UIMGR.create_window("UI/WNDSelectProject")
end

local function on_submain_subatton_btnbutton_click(btn)
	-- 上班
	local workstate = DY_DATA.User.workstate -- 1 下班， 2， 上班中， 3 离岗
	if workstate == 1 then
		if projectId == nil then 
			_G.UI.Toast:make(nil, "请选择项目"):show()	
			return
		end

		punch_type = 1
		-- local nm = NW.msg("ATTENCE.CS.VERIFYLATLNG")
		-- local gps = libsystem.GetGps()
		-- nm:writeU32(projectId)
		-- nm:writeString(gps)
		-- NW.send(nm)
		
		on_try_punch({ret = 1})
	else
		_G.UI.Toast:make(nil, "当前不在下班状态"):show()	
	end
end

local function on_submain_subattoff_btnbutton_click(btn)
	-- 下班
	local workstate = DY_DATA.User.workstate -- 1 下班， 2， 上班中， 3 离岗
	if workstate == 2 then 
		punch_type = 2
		-- local nm = NW.msg("ATTENCE.CS.VERIFYLATLNG")
		-- local gps = libsystem.GetGps()
		-- nm:writeU32(projectId)
		-- nm:writeString(gps)
		-- NW.send(nm)
		on_try_punch({ret = 1})
	else
		_G.UI.Toast:make(nil, "当前不在上班状态"):show()
	end
end

local function on_submain_subleave_btnbutton_click(btn)
	UIMGR.create("UI/WNDAttLeave")
end

local function on_submain_subunder_btnbutton_click(btn)

	UIMGR.create("UI/WNDAttUnder")
end

local function on_subtop_btnlog_click(btn)
	UIMGR.create_window("UI/WNDAttLog")
end

local function on_subbtm_btnwork_click(btn)
	UIMGR.create_window("UI/WNDMainWork")
end

local function on_subbtm_btnsch_click(btn)
	UIMGR.create_window("UI/WNDMainSchedule")
end

local function on_subbtm_btnmsg_click(btn)
	UIMGR.create_window("UI/WNDMainMsg")
end

local function on_subbtm_btnuser_click(btn)
	UIMGR.create_window("UI/WNDMainUser")
end

on_project_init = function ()
	if projectId == nil then
		Ref.SubMain.SubProject.lbText.text = "请选择项目"
		Ref.SubMain.SubAttOn.lbText.text = ""
		Ref.SubMain.SubAttOff.lbText.text = ""
		return
	end
	local AttendanceProject = DY_DATA.AttendanceList[projectId]
	Ref.SubMain.SubProject.lbText.text = AttendanceProject.name
	Ref.SubMain.SubAttOn.lbText.text = AttendanceProject.starttime
	Ref.SubMain.SubAttOff.lbText.text = AttendanceProject.endtime
end

local function on_ui_init()
	-- Ref.SubMain.SubTime.lbTime.text = libsystem.DateTime()
	-- Ref.SubMain.SubTime.lbDay.text = ""

	local User = DY_DATA.User
	if User.workstate == 1 then
		Ref.SubMain.SubUnder.btnButton:SetInteractable(false)
		Ref.SubMain.SubLeave.lbText.text = "离岗"
	elseif User.workstate == 2 then
		Ref.SubMain.SubUnder.btnButton:SetInteractable(true)
		Ref.SubMain.SubLeave.lbText.text = "离岗"
	elseif User.workstate == 3 then
		Ref.SubMain.SubUnder.btnButton:SetInteractable(true)
		Ref.SubMain.SubLeave.lbText.text = "复岗"
	end

	on_project_init()
end

local function init_view()
	Ref.SubMain.SubProject.btn.onAction = on_submain_subproject_click
	Ref.SubMain.SubAttOn.btnButton.onAction = on_submain_subatton_btnbutton_click
	Ref.SubMain.SubAttOff.btnButton.onAction = on_submain_subattoff_btnbutton_click
	Ref.SubMain.SubLeave.btnButton.onAction = on_submain_subleave_btnbutton_click
	Ref.SubMain.SubUnder.btnButton.onAction = on_submain_subunder_btnbutton_click
	Ref.SubTop.btnLog.onAction = on_subtop_btnlog_click
	Ref.SubBtm.btnWork.onAction = on_subbtm_btnwork_click
	Ref.SubBtm.btnSch.onAction = on_subbtm_btnsch_click
	Ref.SubBtm.btnMsg.onAction = on_subbtm_btnmsg_click
	Ref.SubBtm.btnUser.onAction = on_subbtm_btnuser_click
	--!*以上：自动注册的回调函数*--
end

local function init_logic()
	NW.subscribe("ATTENCE.SC.VERIFYLATLNG", on_try_punch)
	NW.subscribe("USER.SC.GETUSERINFOR", on_ui_init)
	-- libsystem.StartGps()
	on_ui_init()
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
	NW.unsubscribe("ATTENCE.SC.VERIFYLATLNG", on_try_punch)
	NW.unsubscribe("USER.SC.GETUSERINFOR", on_ui_init)
	libsystem.StopGps()
end

local P = {
	start = start,
	update_view = update_view,
	on_recycle = on_recycle,
}
return P

