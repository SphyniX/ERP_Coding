--
-- @file    ui/schedule/lc_wndareaschedule.lua
-- @authors zl
-- @date    2016-08-14 19:47:51
-- @desc    WNDAreaSchedule
--

local ipairs, pairs
    = ipairs, pairs
local libugui = require "libugui.cs"
local libunity = require "libunity.cs"
local UIMGR = MERequire "ui/uimgr"
local DY_DATA = MERequire "datamgr/dydata.lua"
local UI_DATA = MERequire "datamgr/uidata.lua"
local Ref
--!*以下：自动生成的回调函数*--

local function on_subproject_grpproject_entproject_btndata_click(btn)
	
end

local function on_subproject_grpproject_entproject_btntask_click(btn)
	
end

local function on_subbtm_btnwork_click(btn)
-- ##	UIMGR.WNDStack:pop()
	UIMGR.create_window("UI/WNDAreaWork")
end

local function on_subbtm_btnmsg_click(btn)
-- ##	UIMGR.WNDStack:pop()
	UIMGR.create_window("UI/WNDAreaMsg")
end

local function on_subbtm_btnuser_click(btn)
-- ##	UIMGR.WNDStack:pop()
	UIMGR.create_window("UI/WNDAreaUser")
end

local function init_view()
	Ref.SubProject.GrpProject.Ent.btnData.onAction = on_subproject_grpproject_entproject_btndata_click
	Ref.SubProject.GrpProject.Ent.btnTask.onAction = on_subproject_grpproject_entproject_btntask_click
	Ref.SubBtm.btnWork.onAction = on_subbtm_btnwork_click
	Ref.SubBtm.btnMsg.onAction = on_subbtm_btnmsg_click
	Ref.SubBtm.btnUser.onAction = on_subbtm_btnuser_click
	UIMGR.make_group(Ref.SubProject.GrpProject, function (New, Ent)
		New.btnData.onAction = Ent.btnData.onAction
		New.btnTask.onAction = Ent.btnTask.onAction
	end)
	--!*以上：自动注册的回调函数*--
end

local function init_logic()
	local ProjectList = DY_DATA.ProjectList
	
	Ref.SubProject.GrpProject:dup(#ProjectList, function (i, Ent, isNew)
		local Project = ProjectList[i]
		-- Ent.lbText.text = Project.name
	end)
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

