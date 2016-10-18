--
-- @file    ui/work/lc_wndsupnewtask.lua
-- @authors zl
-- @date    2016-09-13 19:31:59
-- @desc    WNDSupNewTask
--

local ipairs, pairs
    = ipairs, pairs
local libugui = require "libugui.cs"
local libunity = require "libunity.cs"
local UIMGR = MERequire "ui/uimgr"
local NW = MERequire "network/networkmgr"
local DY_DATA = MERequire "datamgr/dydata.lua"
local UI_DATA = MERequire "datamgr/uidata.lua"
local Ref

local PersonList = {}
local TimeList = {}

local function on_submit_finish(Ret)
	if Ret.ret == 1 then
		local storeId = UI_DATA.WNDSupTaskList.storeId
		local nm = NW.msg("WORK.CS.GETASSIGNMENT")
		libunity.LogI("WORK.CS.GETASSIGNMENT: storeId = {0}", storeId)
		nm:writeU32(storeId)
		NW.send(nm)
		UIMGR.close_window(Ref.root)
	else
		-- _G.UI.Toast:make(nil, "促销员时间冲突"):show()
	end
end

local function time_to_string(Time)
	return string.format("%d-%d-%d %d:%d:00", Time.year, Time.month, Time.day, Time.hour, Time.minute)
end

local function on_time_init()
	TimeList = UI_DATA.WNDSupNewTask.TimeList or {}
	Ref.SubMain.SubContext.GrpDay:dup(#TimeList, function (i, Ent, isNew)
		local Time = TimeList[i]
		Ent.SubStart.lbText.text = Time.starttime
		Ent.SubEnd.lbText.text = Time.endtime
	end)
end

local function on_person_init()
	PersonList = UI_DATA.WNDSupNewTask.PersonList or {}
	Ref.SubMain.SubContext.GrpPerson:dup(#PersonList, function (i, Ent, isNew)
		local Person = PersonList[i]
		Ent.lbName.text = Person.name
	end)
end
--!*以下：自动生成的回调函数*--

local function on_submain_subcontext_grpday_btnadd_click(btn)
	table.insert(UI_DATA.WNDSupNewTask.TimeList, {starttime = "", endtime = "" })
	on_time_init()
end

local function on_submain_subcontext_grpday_entday_btndelete_click(btn)
	local index = tonumber(btn.transform.parent.name:sub(7))
	table.remove(UI_DATA.WNDSupNewTask.TimeList, index)
	on_time_init()
end

local function on_submain_subcontext_grpday_entday_substart_click(btn)
	local index = tonumber(btn.transform.parent.name:sub(7))
	-- 修改开始时间
	UI_DATA.WNDSetTime.on_call_back = function (Time)
		local time = time_to_string(Time)
		UI_DATA.WNDSupNewTask.TimeList[index].starttime = time
		Ref.SubMain.SubContext.GrpDay.Ents[index].SubStart.lbText.text = time
	end
	UIMGR.create("UI/WNDSetTime")	
end

local function on_submain_subcontext_grpday_entday_subend_click(btn)
	local index = tonumber(btn.transform.parent.name:sub(7))
	-- 修改结束时间
	UI_DATA.WNDSetTime.on_call_back = function (Time)
		local time = time_to_string(Time)
		UI_DATA.WNDSupNewTask.TimeList[index].endtime = time
		Ref.SubMain.SubContext.GrpDay.Ents[index].SubEnd.lbText.text = time
	end
	UIMGR.create("UI/WNDSetTime")
end

local function on_submain_subcontext_grpperson_btnadd_click(btn)
		-- 添加人员
	-- table.insert(PersonList, {id = 3, name = "张五"})
	UI_DATA.WNDSelectPerson.SelectList = PersonList

	UI_DATA.WNDSelectPerson.callback = function (List)
		for i,v in ipairs(List) do
			local _, Info = table.has(PersonList, v.id)
			if not Info then
				table.insert(UI_DATA.WNDSupNewTask.PersonList, v)
			end
		end
		on_person_init()
	end
	UIMGR.create_window("UI/WNDSelectPerson")
end

local function on_submain_subcontext_grpperson_entperson_btndelete_click(btn)
	-- 删除人员
	local index = tonumber(btn.transform.parent.name:sub(10))
	table.remove(UI_DATA.WNDSupNewTask.PersonList, index)
	on_person_init()	
end

local function on_submain_subcontext_btnsubmit_click(btn)
	-- 下发新任务
	if PersonList == nil or #PersonList == 0 or TimeList == nil or #TimeList == 0 then
		_G.UI.Toast:make(nil, "任务不完整"):show()
		return
	end
	
	local storeId = UI_DATA.WNDSupTaskList.storeId
	local nm = NW.msg("WORK.CS.ISSUED")
	nm:writeU32(storeId)
	nm:writeU32(#PersonList)
	for i,v in ipairs(PersonList) do
		nm:writeU32(v.id)
		nm:writeU32(#TimeList)
		for j,w in ipairs(TimeList) do
			nm:writeString(w.starttime)
			nm:writeString(w.endtime)
		end
	end
	NW.send(nm)
end

local function on_subtop_btnback_click(btn)
	UIMGR.close_window(Ref.root)
end

local function init_view()
	Ref.SubMain.SubContext.GrpDay.btnAdd.onAction = on_submain_subcontext_grpday_btnadd_click
	Ref.SubMain.SubContext.GrpDay.Ent.btnDelete.onAction = on_submain_subcontext_grpday_entday_btndelete_click
	Ref.SubMain.SubContext.GrpDay.Ent.SubStart.btn.onAction = on_submain_subcontext_grpday_entday_substart_click
	Ref.SubMain.SubContext.GrpDay.Ent.SubEnd.btn.onAction = on_submain_subcontext_grpday_entday_subend_click
	Ref.SubMain.SubContext.GrpPerson.btnAdd.onAction = on_submain_subcontext_grpperson_btnadd_click
	Ref.SubMain.SubContext.GrpPerson.Ent.btnDelete.onAction = on_submain_subcontext_grpperson_entperson_btndelete_click
	Ref.SubMain.SubContext.btnSubmit.onAction = on_submain_subcontext_btnsubmit_click
	Ref.SubTop.btnBack.onAction = on_subtop_btnback_click
	UIMGR.make_group(Ref.SubMain.SubContext.GrpDay, function (New, Ent)
		New.btnDelete.onAction = Ent.btnDelete.onAction
		New.SubStart.btn.onAction = Ent.SubStart.btn.onAction
		New.SubEnd.btn.onAction = Ent.SubEnd.btn.onAction
	end)
	UIMGR.make_group(Ref.SubMain.SubContext.GrpPerson, function (New, Ent)
		New.btnDelete.onAction = Ent.btnDelete.onAction
	end)
	--!*以上：自动注册的回调函数*--
end

local function init_logic()
	NW.subscribe("WORK.SC.ISSUED", on_submit_finish)

	TimeList = UI_DATA.WNDSupNewTask.TimeList or {}
	PersonList = UI_DATA.WNDSupNewTask.PersonList or {}
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
	NW.unsubscribe("WORK.SC.ISSUED", on_submit_finish)
end

local P = {
	start = start,
	update_view = update_view,
	on_recycle = on_recycle,
}
return P

