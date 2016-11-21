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
local x = 1

local TimeInfo = {}
local SameAsServer
local undergoreason ,leavereason = nil, nil
local AttendanceProject = nil
local projectId = nil
local Assignmentid = nil
local AttendanceList
local on_project_init

local punch_type -- 1 上班， 2 下班

local function time_to_string(Time)
	return string.format("%d-%d-%d %d:%d", Time.year, Time.month, Time.day, Time.hour, Time.minute)
end

local function on_select_project(id,inAttendanceList)
	print("Start Making Project :" .. id)
	projectId = id
	print(projectId)
	AttendanceList = inAttendanceList
	on_project_init()
end

local function on_try_punch(Ret)
	if Ret.ret == 1 then
		local workstate = DY_DATA.User.workstate -- 1 下班， 2， 上班中， 3 离岗
		Attendance.on_try_punch(punch_type, Assignmentid)
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

local function on_leave_back()
	-- body
end

local function on_submain_subleave_btnbutton_click(btn)
	if Ref.SubMain.SubLeave.lbText.text == "离岗" then
		UI_DATA.WNDAttLeave.Assignmentid = Assignmentid
		UIMGR.create("UI/WNDAttLeave")
	else
		local nm = NW.msg("ATTENCE.CS.FUGANG")
		nm:writeU32(tonumber(DY_DATA.User.taskid))
		NW.send(nm)
	end
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
	local AttendanceProject = AttendanceList[projectId]
	print("AttendanceProject in MainAttance :" .. JSON:encode(AttendanceProject))
	Assignmentid = AttendanceProject.Assignmentid
	Ref.SubMain.SubProject.lbText.text = AttendanceProject.name
	Ref.SubMain.SubAttOn.lbText.text = AttendanceProject.starttime
	Ref.SubMain.SubAttOff.lbText.text = AttendanceProject.endtime
end

local function on_ui_init()
	-- Ref.SubMain.SubTime.lbTime.text = libsystem.DateTime()
	-- Ref.SubMain.SubTime.lbDay.text = ""

	local User = DY_DATA.User
	if User.workstate == 1 then
		Ref.SubMain.SubLeave.btnButton:SetInteractable(false)
		Ref.SubMain.SubLeave.lbText.text = "离岗"
	elseif User.workstate == 2 then
		Ref.SubMain.SubLeave.btnButton:SetInteractable(true)
		Ref.SubMain.SubLeave.lbText.text = "离岗"
	elseif User.workstate == 3 then
		Ref.SubMain.SubLeave.btnButton:SetInteractable(true)
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

local function on_set_red()
	if DY_DATA.SetRed then
		libunity.SetActive(Ref.SubBtm.SetRed,true)
	else
		libunity.SetActive(Ref.SubBtm.SetRed,false)
	end
end

                      


local function refreshtime()
	TimeInfo = os.date("*t",os.time())
	
	if DY_DATA.Work.NowTime == nil then
		local nm = NW.msg("ATTENCE.CS.GETTIME")
		NW.send(nm)
		return
	end
	if DY_DATA.MsgList == nil or next(DY_DATA.MsgList) == nil then
		local nm = NW.msg("MESSAGE.CS.GETMESSAGELIST")
		nm:writeU32(DY_DATA.User.id)
		NW.send(nm)
		return
	end
	print("Work.NowTime is :" .. JSON:encode(DY_DATA.Work.NowTime))
	local TimeOfDay = DY_DATA.Work.NowTime
	print("TimeOfDay is :" .. JSON:encode(TimeOfDay))
	Ref.SubMain.SubTime.lbTime.text = TimeOfDay.time
	Ref.SubMain.SubTime.lbDay.text = string.sub(TimeOfDay.day,1,4) .. " 年 " .. string.sub(TimeOfDay.day,6,7) .. " 月 " .. string.sub(TimeOfDay.day,9,10) .. " 日" .. "     " .. TimeOfDay.week
	TimeInfo.hour = DY_DATA.Work.NowTime.time:sub(1,2)
	TimeInfo.min = DY_DATA.Work.NowTime.time:sub(4,5)
	TimeInfo.sec = DY_DATA.Work.NowTime.time:sub(7,8)
	-- print(DY_DATA.Work.NowTime.time:sub(1,2))
	-- print(DY_DATA.Work.NowTime.time:sub(4,5))
	-- print(DY_DATA.Work.NowTime.time:sub(7,8))
	-- print(libsystem.GetFps())

end

local function init_logic()
	print("Test DY_DATA.ProjectList is :"  .. JSON:encode(DY_DATA.ProjectList))
	DY_DATA.Work.NowTime = nil
	NW.subscribe("ATTENCE.SC.VERIFYLATLNG", on_try_punch)
	NW.subscribe("USER.SC.GETUSERINFOR", on_ui_init)
	NW.subscribe("ATTENCE.SC.BEDEMOBILIZED", on_leave_back)
	NW.subscribe("ATTENCE.SC.GETTIME",refreshtime)
	NW.subscribe("MESSAGE.SC.GETMESSAGELIST", on_set_red)

	-- libsystem.StartGps()
	on_ui_init()
	refreshtime()
end


local function refreshtime_onupdate()
	x = x + 1
	if (x % libsystem.GetFps()) == 0 then
		x = 0
		TimeInfo.sec = tostring(tonumber(TimeInfo.sec)+1)
		-- printf(TimeInfo.sec)

		if tonumber(TimeInfo.sec) == 60 then
			TimeInfo.min = tostring(tonumber(TimeInfo.min)+1)
			TimeInfo.sec = tonumber(0)
		end
		if tonumber(TimeInfo.sec) < 10 then TimeInfo.sec = "0" .. TimeInfo.sec end

		if tonumber(TimeInfo.min) == 60 then
			TimeInfo.hour = tostring(tonumber(TimeInfo.hour)+1)
			TimeInfo.min = tonumber(0)
		end
		if tonumber(TimeInfo.min) < 10 then TimeInfo.min = "0" .. tostring(tonumber(TimeInfo.min)) end

		if tonumber(TimeInfo.hour) == 24 then

			TimeInfo.hour = tonumber(0)
		end
		if tonumber(TimeInfo.hour) < 10 then TimeInfo.hour = "0" .. tostring(tonumber(TimeInfo.hour)) end
		
		Ref.SubMain.SubTime.lbTime.text = TimeInfo.hour .. ":" .. TimeInfo.min .. ":" .. TimeInfo.sec
	end

end 

local function start(self)
	if Ref == nil or Ref.root ~= self then
		Ref = libugui.GenLuaTable(self, "root")
		init_view()
	end
	init_logic()
end

local function update_view()
	print("1")
end

local function on_recycle()
	NW.unsubscribe("ATTENCE.SC.VERIFYLATLNG", on_try_punch)
	NW.unsubscribe("USER.SC.GETUSERINFOR", on_ui_init)
	libsystem.StopGps()
end

local function update()
	refreshtime_onupdate()
end

local P = {
	start = start,
	update_view = update_view,
	on_recycle = on_recycle,
}
return P

