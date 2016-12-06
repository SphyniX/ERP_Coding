--
-- @file    ui/attendance/lc_wndselectproject.lua
-- @authors zl
-- @date    2016-08-15 02:39:42
-- @desc    WNDSelectProject
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
local AttendanceList
--!*以下：自动生成的回调函数*--

local function on_subproject_grpproject_entproject_click(btn)
	local index = tonumber(btn.name:sub(11))
	if DY_DATA.User.limit == 1 then
		local id = AttendanceList[index].Assignmentid
	end
	local projectId = AttendanceList[index].id
	local on_call_back = UI_DATA.WNDSelectProject.on_call_back
	if on_call_back ~= nil then on_call_back(projectId,DY_DATA.AttendanceList) end
	UIMGR.close_window(Ref.root)
end

local function on_subtop_btnback_click(btn)
	UIMGR.close_window(Ref.root)
end

local function on_ui_init()
	print("try init ui")
	AttendanceList = DY_DATA.get_attendance_list(false)
	if AttendanceList == nil then 
		libunity.SetActive(Ref.SubProject.spNil, true)
		return 
	end
	libunity.SetActive(Ref.SubProject.spNil, #AttendanceList == 0)
		
	-- print("init ui")
	Ref.SubProject.GrpProject:dup(#AttendanceList, function ( i, Ent, isNew)
		local Attendance = AttendanceList[i]
		Ent.lbName.text = Attendance.name
		if DY_DATA.User.limit == 1 then
			libunity.SetActive(Ent.SupName,true)
			Ent.SupName.text = Attendance.supervisor
		else
			libunity.SetActive(Ent.SupName,false)
		end
		UIMGR.get_photo(Ent.spIcon, Attendance.icon)

	end)
end

local function init_view()
	Ref.SubProject.GrpProject.Ent.btn.onAction = on_subproject_grpproject_entproject_click
	Ref.SubTop.btnBack.onAction = on_subtop_btnback_click
	UIMGR.make_group(Ref.SubProject.GrpProject, function (New, Ent)
		New.btn.onAction = Ent.btn.onAction
	end)
	--!*以上：自动注册的回调函数*--
end

local function init_logic()

	if DY_DATA.User.limit == 1 then
		print("______________________________Init Limit 1 ______________________________")
		NW.subscribe("ATTENCE.SC.GETWORK",on_ui_init)
		libunity.SetActive(Ref.SubLimit,true)
		if DY_DATA.AttendanceList == nil or next(DY_DATA.AttendanceList) == nil then
			print("AttendanceList is nil")
			if NW.connected() then
				local nm = NW.msg("ATTENCE.CS.GETWORK")
			    nm:writeU32(DY_DATA.User.id)
			    NW.send(nm)
			    return
			else
				DY_DATA.AttendanceList = {}
			end
		end
	end
	if DY_DATA.User.limit == 2 then 
		NW.subscribe("WORK.SC.GETPROJECT",on_ui_init)
		print("______________________________Init Limit 2 ______________________________")
		libunity.SetActive(Ref.SubLimit.root,false)
		if DY_DATA.AttendanceList == nil or next(DY_DATA.AttendanceList) == nil then
			print("AttendanceList is nil")
			if NW.connected() then
				local nm = NW.msg("WORK.CS.GETPROJECT")
			    nm:writeU32(DY_DATA.User.id)
			    NW.send(nm)
			    return
			end

		else
			on_ui_init()
		end
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
	
end

local function on_recycle()
	NW.unsubscribe("ATTENCE.SC.GETWORK",on_ui_init)
end

local P = {
	start = start,
	update_view = update_view,
	on_recycle = on_recycle,
}
return P

