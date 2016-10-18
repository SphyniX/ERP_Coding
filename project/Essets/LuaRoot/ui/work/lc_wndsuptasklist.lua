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
local UI_DATA = MERequire "datamgr/uidata.lua"
local Ref

local storeId
local TaskList
--!*以下：自动生成的回调函数*--

local function on_subtask_grp_enttask_click(btn)
	local index = tonumber(btn.name:sub(8))
	UIMGR.create_window("UI/WNDSupChangeTask")
end

local function on_subtop_btnback_click(btn)
	UIMGR.close_window(Ref.root)
end

local function on_subtop_btnnew_click(btn)
	UIMGR.create_window("UI/WNDSupNewTask")
end

local function on_ui_init()
	TaskList = {
		{starttime = "2016/9/1 8:00", endtime = "2016/9/1 18:00",  PersonList = {{id = 1, name = "asdf",}, {id = 2, name = "zxcv",}, {id = 3, name = "qwer",},}},
		{starttime = "2016/9/2 8:00", endtime = "2016/9/2 18:00",  PersonList = {{id = 1, name = "asdf",}, {id = 2, name = "zxcv",}, {id = 3, name = "qwer",},}},
		{starttime = "2016/9/1 18:00", endtime = "2016/9/2 8:00",  PersonList = {{id = 1, name = "asdf",}, {id = 2, name = "zxcv",}, {id = 3, name = "qwer",},}},
		{starttime = "2016/9/3 8:00", endtime = "2016/9/3 18:00",  PersonList = {{id = 1, name = "asdf",}, {id = 2, name = "zxcv",}, {id = 3, name = "qwer",},}},
		{starttime = "2016/9/4 8:00", endtime = "2016/9/4 18:00",  PersonList = {{id = 1, name = "asdf",}, {id = 2, name = "zxcv",}, {id = 3, name = "qwer",},}},
	}

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
	storeId = UI_DATA.WNDSupTaskList.storeId

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
	
end

local P = {
	start = start,
	update_view = update_view,
	on_recycle = on_recycle,
}
return P

