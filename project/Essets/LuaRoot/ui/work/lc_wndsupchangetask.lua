--
-- @file    ui/work/lc_wndsupchangetask.lua
-- @authors zl
-- @date    2016-09-14 11:17:55
-- @desc    WNDSupChangeTask
--

local ipairs, pairs
    = ipairs, pairs
local libugui = require "libugui.cs"
local libunity = require "libunity.cs"
local UIMGR = MERequire "ui/uimgr"
local NW = MERequire "network/networkmgr"
local UI_DATA = MERequire "datamgr/uidata.lua"
local DY_DATA = MERequire "datamgr/dydata.lua"
local Ref

local Task, PersonList, AddList, RemoveList

local del_wait, cha_wait, new_wait

local function on_del_finish(Ret)
	if Ret.ret == 1 then
		del_wait = nil
		if del_wait == nil and cha_wait == nil and new_wait == nil then
			local storeId = UI_DATA.WNDSupTaskList.storeId
			local nm = NW.msg("WORK.CS.GETASSIGNMENT")
			libunity.LogI("WORK.CS.GETASSIGNMENT: storeId = {0}", storeId)
			nm:writeU32(storeId)
			NW.send(nm)
			UIMGR.close_window(Ref.root)
		end
	else
		
	end
end

local function on_cha_finish(Ret)
	if Ret.ret == 1 then
		cha_wait = nil
		if del_wait == nil and cha_wait == nil and new_wait == nil then
			local storeId = UI_DATA.WNDSupTaskList.storeId
			local nm = NW.msg("WORK.CS.GETASSIGNMENT")
			libunity.LogI("WORK.CS.GETASSIGNMENT: storeId = {0}", storeId)
			nm:writeU32(storeId)
			NW.send(nm)
			UIMGR.close_window(Ref.root)
		end
	else
	end
end

local function on_new_finish(Ret)
	if Ret.ret == 1 then
		new_wait = nil
		if del_wait == nil and cha_wait == nil and new_wait == nil then
			local storeId = UI_DATA.WNDSupTaskList.storeId
			local nm = NW.msg("WORK.CS.GETASSIGNMENT")
			libunity.LogI("WORK.CS.GETASSIGNMENT: storeId = {0}", storeId)
			nm:writeU32(storeId)
			NW.send(nm)
			UIMGR.close_window(Ref.root)
		end
	else
	end
end

local function time_to_string(Time)
	return string.format("%d-%d-%d %d:%d:00", Time.year, Time.month, Time.day, Time.hour, Time.minute)
end

local function on_person_init(type)
	print("on_person_init")
	if type == nil then
		type = Ref.SubTop.SubChange.lbText.text == "修改" and 2 or 1
	else
		Ref.SubTop.SubChange.lbText.text = type == 1 and "保存" or "修改"
	end
	libunity.SetActive(Ref.SubMain.SubContext.GrpPerson.btnAdd, type == 1)
	
	PersonList = table.toarray(Task.PersonList)
	AddList = UI_DATA.WNDSupChangeTask.AddList or {}
	RemoveList = UI_DATA.WNDSupChangeTask.RemoveList or {}
	for _,v in ipairs(AddList) do
		local _, Info = table.has(PersonList, v.id)
		if not Info then
			table.insert(PersonList, v)
		end
	end

	for _,v in ipairs(RemoveList) do
		local n, Info = table.has(PersonList, v.id)
		if n then
			table.remove(PersonList, n)
		end
	end

	if PersonList == nil then return end
	print("on_person_init "..JSON:encode(PersonList))
	Ref.SubMain.SubContext.GrpPerson:dup(#PersonList, function (i, Ent, isNew)
		local Person = PersonList[i]
		Ent.lbName.text = Person.name
		libunity.SetActive(Ent.btnDelete, type == 1)
	end)
end

local function on_save_finish(Ret)
	Ref.SubTop.SubChange.lbText.text = "修改"
	on_person_init(2)
end

local function on_save_data()
	local starttime = Ref.SubMain.SubContext.SubDay.SubStart.lbText.text
	local endtime = Ref.SubMain.SubContext.SubDay.SubEnd.lbText.text

	--判断是否存在修改
	local ChangeList = {}
	if starttime ~= Task.starttime or endtime ~= Task.endtime then
		for i,v in ipairs(Task.PersonList) do
			print("Person"..JSON:encode(v))
			local j, Info = table.has(RemoveList, v.id)
	 		if not Info then
	 			table.insert(ChangeList, v)
	 		end
		end
	end

	print("ChangeList"..JSON:encode(ChangeList))
	print("RemoveList"..JSON:encode(RemoveList))
	print("AddList"..JSON:encode(AddList))
	
	local nm
	-- 删除
	if #RemoveList ~= 0 then
		del_wait = true
		nm = NW.msg("WORK.CS.DELETEASS")
		nm:writeU32(#RemoveList)
		for i,v in ipairs(RemoveList) do
			nm:writeU32(v.taskId)
		end
		NW.send(nm)
	else
		del_wait = nil
	end
	-- 修改
	if #ChangeList ~= 0 then
		cha_wait = true
		nm = NW.msg("WORK.CS.UPDATEASS")
		print(#ChangeList)
		nm:writeU32(#ChangeList)
		for i,v in ipairs(ChangeList) do
			print(v.taskId, v.id, starttime, endtime)
			nm:writeU32(v.taskId)
			nm:writeU32(v.id)
			nm:writeString(starttime)
			nm:writeString(endtime)
		end
		NW.send(nm)
	else
		cha_wait = nil
	end
	-- 新建
	if #AddList ~= 0 then
		local storeId = UI_DATA.WNDSupTaskList.storeId
		new_wait = true
		nm = NW.msg("WORK.CS.ISSUED")
		nm:writeU32(storeId)
		nm:writeU32(#AddList)
		for i,v in ipairs(AddList) do
			nm:writeU32(v.id)
			nm:writeU32(1)
			nm:writeString(starttime)
			nm:writeString(endtime)
		end
		NW.send(nm)
	else
		new_wait = nil
	end

	if del_wait == nil and cha_wait == nil and new_wait == nil then
		on_person_init(2)
	end
end

local function on_remove(Info)

	local i, _ = table.has(UI_DATA.WNDSupChangeTask.AddList, Info.id)
	if i then
		table.remove(UI_DATA.WNDSupChangeTask.AddList, i) 
	else
		table.insert(UI_DATA.WNDSupChangeTask.RemoveList, Info)
	end
end

local function on_add(Info)
	local i, v = table.has(UI_DATA.WNDSupChangeTask.RemoveList, Info.id)
	if v then
		table.remove(UI_DATA.WNDSupChangeTask.RemoveList, i) 
	else
		table.insert(UI_DATA.WNDSupChangeTask.AddList, Info)
	end
end

--!*以下：自动生成的回调函数*--

local function on_submain_subcontext_subday_substart_click(btn)
	-- 修改开始时间
	if Ref.SubTop.SubChange.lbText.text == "修改" then return end
	UI_DATA.WNDSetTime.on_call_back = function (Time)
		Ref.SubMain.SubContext.SubDay.SubStart.lbText.text = time_to_string(Time)
	end
	UIMGR.create("UI/WNDSetTime")
end

local function on_submain_subcontext_subday_subend_click(btn)
	-- 修改结束时间
	if Ref.SubTop.SubChange.lbText.text == "修改" then return end
	UI_DATA.WNDSetTime.on_call_back = function (Time)
		Ref.SubMain.SubContext.SubDay.SubEnd.lbText.text = time_to_string(Time)
	end
	UIMGR.create("UI/WNDSetTime")
end

local function on_submain_subcontext_grpperson_btnadd_click(btn)
	-- 添加人员
	UI_DATA.WNDSelectPerson.SelectList = PersonList

	UI_DATA.WNDSelectPerson.callback = function (List)
		for i,v in ipairs(PersonList) do
			local n, Info = table.has(List, v.id)
			if not Info then
				on_remove(v)
			end
		end

		for i,v in ipairs(List) do
			local n, Info = table.has(PersonList, v.id)
			if not Info then
				on_add(v)
			end
		end
		on_person_init()
	end
	UIMGR.create_window("UI/WNDSelectPerson")
end

local function on_submain_subcontext_grpperson_entperson_btndelete_click(btn)
	-- 删除人员
	local index = tonumber(btn.transform.parent.name:sub(10))
	local Person = PersonList[index]
	on_remove(Person)
	on_person_init()
end

local function on_submain_subcontext_btndelete_click(btn)
	-- 删除此任务
	del_wait = true
	new_wait = nil
	cha_wait = nil

	local nm = NW.msg("WORK.CS.DELETEASS")
	nm:writeU32(#Task.PersonList)
	for i,v in ipairs(Task.PersonList) do
		nm:writeU32(v.taskId)
	end
	NW.send(nm)
end

local function on_subtop_btnback_click(btn)
	UIMGR.close_window(Ref.root)
end

local function on_subtop_subchange_click(btn)
	if Ref.SubTop.SubChange.lbText.text == "修改" then
		-- Ref.SubTop.SubChange.lbText.text = "保存"
		on_person_init(1)
	else
		on_save_data()
	end
end

local function on_ui_init()
	Task = UI_DATA.WNDSupChangeTask.Task
	if Task == nil then return end
	Ref.SubMain.SubContext.SubDay.SubStart.lbText.text = Task.starttime
	Ref.SubMain.SubContext.SubDay.SubEnd.lbText.text = Task.endtime
	local uitype = 1
	if UI_DATA.WNDSupChangeTask.AddList == nil then UI_DATA.WNDSupChangeTask.AddList = {} uitype = 2 end
	if UI_DATA.WNDSupChangeTask.RemoveList == nil then UI_DATA.WNDSupChangeTask.RemoveList = {} uitype = 2 end
	on_person_init(uitype)
end

local function init_view()
	Ref.SubMain.SubContext.SubDay.SubStart.btn.onAction = on_submain_subcontext_subday_substart_click
	Ref.SubMain.SubContext.SubDay.SubEnd.btn.onAction = on_submain_subcontext_subday_subend_click
	Ref.SubMain.SubContext.GrpPerson.btnAdd.onAction = on_submain_subcontext_grpperson_btnadd_click
	Ref.SubMain.SubContext.GrpPerson.Ent.btnDelete.onAction = on_submain_subcontext_grpperson_entperson_btndelete_click
	Ref.SubMain.SubContext.btnDelete.onAction = on_submain_subcontext_btndelete_click
	Ref.SubTop.btnBack.onAction = on_subtop_btnback_click
	Ref.SubTop.SubChange.btn.onAction = on_subtop_subchange_click
	UIMGR.make_group(Ref.SubMain.SubContext.GrpPerson, function (New, Ent)
		New.btnDelete.onAction = Ent.btnDelete.onAction
	end)
	--!*以上：自动注册的回调函数*--
end

local function init_logic()
	NW.subscribe("WORK.SC.DELETEASS", on_del_finish)
	NW.subscribe("WORK.SC.UPDATEASS", on_cha_finish)
	NW.subscribe("WORK.SC.ISSUED", on_new_finish)
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
	NW.unsubscribe("WORK.SC.DELETEASS", on_del_finish)
	NW.unsubscribe("WORK.SC.UPDATEASS", on_cha_finish)
	NW.unsubscribe("WORK.SC.ISSUED", on_new_finish)
end

local P = {
	start = start,
	update_view = update_view,
	on_recycle = on_recycle,
}
return P

