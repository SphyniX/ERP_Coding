--
-- @file    ui/schedule/lc_wndmainschedule.lua
-- @authors zl
-- @date    2016-08-12 10:27:57
-- @desc    WNDMainSchedule
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
local ProjectList
--!*以下：自动生成的回调函数*--

local function on_subproject_grpproject_entproject_click(btn)
	local index = tonumber(btn.name:sub(11))
	
	UI_DATA.WNDSelectStore.projectId = ProjectList[index].id
	UIMGR.create_window("UI/WNDSelectSchStore")
end

local function on_subbtm_btnatt_click(btn)
-- ##	UIMGR.WNDStack:pop()
	UIMGR.create_window("UI/WNDMainAttendance")
end

local function on_subbtm_btnwork_click(btn)
-- ##	UIMGR.WNDStack:pop()
	UIMGR.create_window("UI/WNDMainWork")
end

local function on_subbtm_btnmsg_click(btn)
-- ##	UIMGR.WNDStack:pop()
	UIMGR.create_window("UI/WNDMainMsg")
end

local function on_subbtm_btnuser_click(btn)
-- ##	UIMGR.WNDStack:pop()
	UIMGR.create_window("UI/WNDMainUser")
end

local function on_set_red()
	if DY_DATA.SetRed then
		libunity.SetActive(Ref.SubBtm.SetRed,true)
	else
		libunity.SetActive(Ref.SubBtm.SetRed,false)
	end
end

local function on_ui_init()
	ProjectList = DY_DATA.get_schproject_list()
	if ProjectList == nil then 
		print("ProjectList is nil")
		libunity.SetActive(Ref.SubProject.spNil, true)
		return 
	end

	libunity.SetActive(Ref.SubProject.spNil, #ProjectList == 0)
	print("ProjectList is "..#ProjectList)
	Ref.SubProject.GrpProject:dup(#ProjectList, function (i, Ent, isNew)
		local Project = ProjectList[i]
		Ent.lbText.text = Project.name
		UIMGR.get_photo(Ent.spIcon, Project.icon)
		local clr = i % 3
		libunity.SetActive(Ent.spRed, clr == 1)
		libunity.SetActive(Ent.spBlue, clr == 2)
		libunity.SetActive(Ent.spYellow, clr == 0)
	end)
end

local function init_view()
	Ref.SubProject.GrpProject.Ent.btn.onAction = on_subproject_grpproject_entproject_click
	Ref.SubBtm.btnAtt.onAction = on_subbtm_btnatt_click
	Ref.SubBtm.btnWork.onAction = on_subbtm_btnwork_click
	Ref.SubBtm.btnMsg.onAction = on_subbtm_btnmsg_click
	Ref.SubBtm.btnUser.onAction = on_subbtm_btnuser_click
	UIMGR.make_group(Ref.SubProject.GrpProject, function (New, Ent)
		New.btn.onAction = Ent.btn.onAction
	end)
	--!*以上：自动注册的回调函数*--
end

local function init_logic()
	
	NW.subscribe("REPORTED.SC.GETPROJECT", on_ui_init)
	NW.subscribe("MESSAGE.SC.GETMESSAGELIST", on_set_red)
	if DY_DATA.MsgList == nil or next(DY_DATA.MsgList) == nil then
		local nm = NW.msg("MESSAGE.CS.GETMESSAGELIST")
		nm:writeU32(DY_DATA.User.id)
		NW.send(nm)
	else
		on_set_red()
	end
	
	if DY_DATA.SchProjectList == nil or next(DY_DATA.SchProjectList) == nil then
		if NW.connected() then
			local nm = NW.msg("REPORTED.CS.GETPROJECT")
			nm:writeU32(DY_DATA.User.id)
			NW.send(nm)
		end
		return
	end
	
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
	NW.unsubscribe("REPORTED.SC.GETPROJECT", on_ui_init)
end

local P = {
	start = start,
	update_view = update_view,
	on_recycle = on_recycle,
}
return P

