--
-- @file    ui/work/lc_wndsuptasklist.lua
-- @authors zl
-- @date    2016-09-13 19:08:50
-- @desc    WNDSupTaskList
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

local projectId, storeId
local TaskList
--!*以下：自动生成的回调函数*--

local function on_subtask_grp_enttask_click(btn)
	local index = tonumber(btn.name:sub(8))
	local Task = TaskList[index]
	UI_DATA.WNDSupChangeTask.Task = Task
	UI_DATA.WNDSupChangeTask.AddList = nil
	UI_DATA.WNDSupChangeTask.RemoveList = nil
	
	UIMGR.create_window("UI/WNDSupChangeTask")
end

local function on_subtop_btnback_click(btn)
	UIMGR.close_window(Ref.root)
end

local function on_subtop_btnnew_click(btn)
	UI_DATA.WNDSupNewTask.TimeList = {}
	UI_DATA.WNDSupNewTask.PersonList = {}
	UIMGR.create_window("UI/WNDSupNewTask")
end

local function on_ui_init()
	if DY_DATA.ProjectList[projectId] == nil or DY_DATA.ProjectList[projectId].StoreList == nil then 
		libunity.LogW("UI Error: Project 或 StoreList 不存在: {0}", projectId)
		return 
	end
	local StoreList = DY_DATA.ProjectList[projectId].StoreList
	local Store = DY_DATA.get_store(StoreList, storeId)
	print(JSON:encode(Store))
	if Store == nil or Store.TaskList == nil then 
		libunity.LogW("UI Error: Store or TaskList 不存在: {0}", storeId)
		return
	end
	
	TaskList = Store.TaskList
	libunity.LogI("TaskList : {0}", JSON:encode(TaskList))
	Ref.SubTask.Grp:dup(#TaskList, function (i, Ent, isNew)
		local Task = TaskList[i]
		Ent.lbStart.text = Task.starttime
		Ent.lbEnd.text = Task.endtime
	end)
end

local function init_view()
	Ref.SubTask.Grp.Ent.btn.onAction = on_subtask_grp_enttask_click
	Ref.SubTop.btnBack.onAction = on_subtop_btnback_click
	Ref.SubTop.btnNew.onAction = on_subtop_btnnew_click
	UIMGR.make_group(Ref.SubTask.Grp, function (New, Ent)
		New.btn.onAction = Ent.btn.onAction
	end)
	--!*以上：自动注册的回调函数*--
end

local function init_logic()
	NW.subscribe("WORK.SC.GETASSIGNMENT", on_ui_init)

	projectId = UI_DATA.WNDSelectStore.projectId
	storeId = UI_DATA.WNDSupTaskList.storeId

	if DY_DATA.ProjectList[projectId] == nil or DY_DATA.ProjectList[projectId].StoreList == nil then return end
	local StoreList = DY_DATA.ProjectList[projectId].StoreList
	local Store = DY_DATA.get_store(StoreList, storeId)
	if Store == nil then return end
	if Store.TaskList == nil then
		local nm = NW.msg("WORK.CS.GETASSIGNMENT")
		libunity.LogI("WORK.CS.GETASSIGNMENT: storeId = {0}", storeId)
		nm:writeU32(storeId)
		NW.send(nm)
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
	NW.unsubscribe("WORK.SC.GETASSIGNMENT", on_ui_init)
end

local P = {
	start = start,
	update_view = update_view,
	on_recycle = on_recycle,
}
return P

