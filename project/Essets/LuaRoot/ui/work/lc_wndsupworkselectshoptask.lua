--
-- @file    ui/work/lc_wndsupworkselectshoptask.lua
-- @authors cks
-- @date    2016-11-09 06:17:57
-- @desc    WNDsupWorkSelectShopTask
--

local ipairs, pairs
= ipairs, pairs
local libugui = require "libugui.cs"
local libunity = require "libunity.cs"
local UIMGR = MERequire "ui/uimgr"
local DY_DATA = MERequire "datamgr/dydata.lua"
local UI_DATA = MERequire "datamgr/uidata.lua"
local NW= MERequire "network/networkmgr"
local ProjectList 
local PeopleList
local Ref
local NewTaskList
local WeekList
local TaskList
--!*以下：自动生成的回调函数*--




local function on_subtop_btnback_click(btn)

	-- UIMGR.create_window("UI/WNDSupWorkSelectShopTaskSetSelPeople")
	UIMGR.close_window(Ref.root)
end


local function on_subtop_nextweek_click(btn)
	
end

local function on_subtasklist_grp_ent_btnbutton_click(btn)
	Ref.SubTaskList.Grp:dup(#NewTaskList,function (i,Ent,IsNew)
		Ent.TaskConcent.text = "无任务"
	end)
	UI_DATA.WNDSupWorkSelectShopTask.TaskList=NewTaskList
	print("点击按钮"..btn.transform.parent.name)
	UI_DATA.WNDSupWorkSelectShopTask.index=tonumber(btn.transform.parent.name:sub(4))
	--UI_DATA.WNDSupWorkSelectShopTaskSetSelPeople.PersonListForUpdate=TaskListUI_DATA.WNDSupWorkSelectShopTask.index
	-- print("UI_DATA.WNDSupWorkSelectShopTask.index"..tostring(UI_DATA.WNDSupWorkSelectShopTask.index))
	-- print("name"..UI_DATA.WNDSupWorkSelectShopTask.PersonList[1].name)
	UIMGR.create_window("UI/WNDSupWorkSelectShopTaskSet")
end



local function on_ui_init()
	local projectId= UI_DATA.WNDSelectStore.projectId
	local storeId = UI_DATA.WNDSubmitSchedule.storeId
	ProjectList = DY_DATA.ProjectList
	local StoreList = ProjectList[projectId].StoreList
	local Store =DY_DATA.get_store(StoreList, storeId) -- DY_DATA.get_store(StoreList, storeId)


	TaskList=Store.TaskList

	print("TaskList is :" .. JSON:encode(TaskList))
	-- if TaskList == nil or next(TaskList) == nil then
	-- 	if NW.connected() then
	-- 		local nm = NW.msg("WORK.CS.GETASSIGNMENT")
	-- 		--nm:writeU32(DY_DATA.User.id)
	-- 		nm:writeU32(storeId)
	-- 		NW.send(nm)

	-- 	end
	-- 	return
	-- end

print("<color=#0f0>on_ui_init---WORK.SC.GETASSIGNMENT--start</color>",projectId, storeId, JSON:encode(TaskList))

	for i=1,#TaskList do
		local day = tonumber(TaskList[i].starttime:sub(9,10))
		TaskList[i].day = day
		print("<color=#0f0>on_ui_init---WORK.SC.GETASSIGNMENT---TaskList[i].day = day--</color>",projectId, storeId, JSON:encode(TaskList))
	end

	local emptyTask = {

	    starttime = "",
	    endtime = "",
	    PersonList = {},

	}
	NewTaskList = {}
	for i=1,7 do
		table.insert(NewTaskList,emptyTask)
	end

	for i=1,#TaskList do
		for j=1,7 do
			if TaskList[i].day == WeekList[j].day then 
				NewTaskList[j] = TaskList[i]
				print("<color=#0f0>on_ui_init---WORK.SC.GETASSIGNMENT---TaskList[i].PersonList--</color>",projectId, storeId, JSON:encode(TaskList[i].PersonList))
				NewTaskList[j].PersonList = TaskList[i].PersonList
				-- print("______________________________" .. JSON:encode(TaskList[i].PersonList))
				-- print(JSON:encode(NewTaskList[j].PersonList))
			end
		end
	end



	Ref.SubTaskList.Grp:dup(#NewTaskList,function (i,Ent,IsNew)
		
		local task=NewTaskList[i]
		
		local  starttime,endtime=task.starttime,task.endtime
		-- print("Time Test : .." .. os.date("%w", string2time(starttime)))
		local namelist =""

		print("<color=#0f0>on_ui_init---WORK.SC.GETASSIGNMENT</color>",projectId, storeId, JSON:encode(TaskList))

		
		print("_______________________________" .. JSON:encode(task))
		if task.PersonList ~= nil then 
			 if task.PersonList ~= {} then 
				for i=1,#task.PersonList do
					namelist=namelist..task.PersonList[i].name..";"
				end
			end
		end
		print("Name in " .. i .. "  Ent is :" .. namelist)
		if starttime == "" then 
		else
			Ent.TaskConcent.text = starttime.."-"..endtime.."\n"..namelist
		end
	
	end)

end



local function on_date_init()
	--local WorkDay = DY_DATA.WorkDay
-- Ref.SubTaskList.Grp:dup(#WorkDay,function (i,Ent,IsNew)

-- 	end)
	-- body
end
local function init_view()
	Ref.SubTop.btnBack.onAction = on_subtop_btnback_click
	Ref.SubTop.NextWeek.onAction = on_subtop_nextweek_click
	Ref.SubTaskList.Grp.Ent.btnButton.onAction = on_subtasklist_grp_ent_btnbutton_click
	UIMGR.make_group(Ref.SubTaskList.Grp, function (New, Ent)
		New.btnButton.onAction = Ent.btnButton.onAction
		end)
	--!*以上：自动注册的回调函数*--
end

local function init_logic()
	UI_DATA.WNDSupWorkSelectShopTaskSetSelPeople.dateState=true
 --   	NW.subscribe("WORK.SC.GETSTARTDATE", on_ui_init)
 --   if DY_DATA.WorkDay == nil or next(DY_DATA.WorkDay) == nil then
	-- 	if NW.connected() then
	-- 		local nm = NW.msg("WORK.CS.GETSTARTDATE")
	-- 		nm:writeU32(DY_DATA.User.id)
	-- 		NW.send(nm)
	-- 	end
	-- 	return
	-- end
	--print("function init_logic---storeId-----------")
	-- print("WeekDay is :" .. os.date("%w",os.time()))
	WeekList = {}
	local todayweek = tonumber(os.date("%w",os.time()))
	if todayweek == 0 then todayweek =7 end
	local startday = tonumber(os.date("%d",os.time())) - todayweek + 1
	for i=1,7 do
		local day = startday
		table.insert(WeekList,{day = day})
		startday = startday + 1
		if startday > tonumber(os.date("%d",os.time({year=os.date("%Y"),month=os.date("%m")+1,day=0}))) then 
			startday = 1
		end
	end

	for i=1,7 do
		print(WeekList[i].day)
	end
	local projectId= UI_DATA.WNDSelectStore.projectId
	local storeId = UI_DATA.WNDSubmitSchedule.storeId
	print("projectId-----storeId:"..projectId.."/"..storeId)
	ProjectList = DY_DATA.ProjectList
	local StoreList = ProjectList[projectId].StoreList
	local Store =StoreList[storeId]   -- DY_DATA.get_store(StoreList, storeId)
	local TaskList=Store.TaskList
	TaskList = nil
	print("function init_logic---storeId-----------"..tostring(storeId))
	NW.subscribe("WORK.SC.GETASSIGNMENT",on_ui_init)	
	if TaskList == nil or next(TaskList) == nil then
		if NW.connected() then
			local nm = NW.msg("WORK.CS.GETASSIGNMENT")
			--nm:writeU32(DY_DATA.User.id)
			nm:writeU32(storeId)
			NW.send(nm)

		end
		return
	end
	on_ui_init()
	-- Ref.SubTaskList.Grp:dup(7,function (i,Ent,IsNew)
		-- body
	--end)
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

