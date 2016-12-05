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
local WeekList,NextWeekList,ThisWeek
local TaskList
--!*以下：自动生成的回调函数*--




local function on_subtop_btnback_click(btn)
	UI_DATA.WNDSupWorkSelectShopTask.ThisWeek = nil
	-- UIMGR.create_window("UI/WNDSupWorkSelectShopTaskSetSelPeople")
	UIMGR.close_window(Ref.root)
end




local function on_subtasklist_grp_ent_btnbutton_click(btn)
	local index = tonumber(btn.transform.parent.name:sub(4))

	Ref.SubTaskList.Grp:dup(#NewTaskList,function (i,Ent,IsNew)
		Ent.TaskConcent.text = "无任务"
	end)
	UI_DATA.WNDSupWorkSelectShopTask.TaskList=NewTaskList
	print("点击按钮"..btn.transform.parent.name)
	UI_DATA.WNDSupWorkSelectShopTask.index=tonumber(btn.transform.parent.name:sub(4))
	--UI_DATA.WNDSupWorkSelectShopTaskSetSelPeople.PersonListForUpdate=TaskListUI_DATA.WNDSupWorkSelectShopTask.index
	-- print("UI_DATA.WNDSupWorkSelectShopTask.index"..tostring(UI_DATA.WNDSupWorkSelectShopTask.index))
	-- print("name"..UI_DATA.WNDSupWorkSelectShopTask.PersonList[1].name)
	if ThisWeek then
		UI_DATA.WNDSupWorkSelectShopTaskSet.data = WeekList[index].data
	else
		UI_DATA.WNDSupWorkSelectShopTaskSet.data = NextWeekList[index].data
	end
	UI_DATA.WNDSupWorkSelectShopTask.ThisWeek = ThisWeek
	UIMGR.create_window("UI/WNDSupWorkSelectShopTaskSet")
end



local function on_ui_init()
	if ThisWeek then
		Ref.SubTop.lbWeek.text = "下周"
	else
		Ref.SubTop.lbWeek.text = "上周"
	end
	local projectId= UI_DATA.WNDSupWorkSelectShopTask.projectId
	local storeId = UI_DATA.WNDSupWorkSelectShopTask.storeIdtrue
	print("storeId is " .. storeId)
	ProjectList = DY_DATA.ProjectList
	local StoreList = ProjectList[projectId].StoreList
	print("StoreList is " .. JSON:encode(StoreList))
	local Store = DY_DATA.get_store(StoreList, storeId)


	TaskList = Store.TaskList

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
			if ThisWeek then
				if TaskList[i].day == WeekList[j].day then 
					NewTaskList[j] = TaskList[i]
					print("<color=#0f0>on_ui_init---WORK.SC.GETASSIGNMENT---TaskList[i].PersonList--</color>",projectId, storeId, JSON:encode(TaskList[i].PersonList))
					NewTaskList[j].PersonList = TaskList[i].PersonList
					-- print("______________________________" .. JSON:encode(TaskList[i].PersonList))
					-- print(JSON:encode(NewTaskList[j].PersonList))
				end
			else
				if TaskList[i].day == NextWeekList[j].day then 
					NewTaskList[j] = TaskList[i]
					print("<color=#0f0>on_ui_init---WORK.SC.GETASSIGNMENT---TaskList[i].PersonList--</color>",projectId, storeId, JSON:encode(TaskList[i].PersonList))
					NewTaskList[j].PersonList = TaskList[i].PersonList
					-- print("______________________________" .. JSON:encode(TaskList[i].PersonList))
					-- print(JSON:encode(NewTaskList[j].PersonList))
				end
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
					if i == 1 then
						namelist=namelist..task.PersonList[i].name
					else
						namelist=namelist.. "  、 " ..task.PersonList[i].name
					end
				end
			end
		end
		print("Name in " .. i .. "  Ent is :" .. namelist)
		if starttime == "" then
			Ent.TaskConcent.text = "无任务"
		else
			Ent.TaskConcent.text = starttime:sub(12).."  -  "..endtime:sub(12).."\n"..namelist
		end
		if ThisWeek then
			Ent.Time.text = WeekList[i].data
		else
			Ent.Time.text = NextWeekList[i].data
		end
	end)

end

local function tomorrow(today,bool)

	if bool then
		local day = today:sub(7,8)
		local month = today:sub(5,6)
		local year = today:sub(1,4)

		day = day + 1
		if day > tonumber(os.date("%d",os.time({year=tonumber(year),month=tonumber(month)+1,day=0}))) then 
			day = 1
			month = month + 1
		end

		if month == 13 then
			month = 1
			year = year + 1
		end
		if day < 10 then
			return year..month.."0"..day
		else
			return year..month..day
		end
	else
		local day = today:sub(7,8)
		local month = today:sub(5,6)
		local year = today:sub(1,4)

		day = day + 7
		if day > tonumber(os.date("%d",os.time({year=tonumber(year),month=tonumber(month)+1,day=0}))) then 
			day = day - tonumber(os.date("%d",os.time({year=tonumber(year),month=tonumber(month)+1,day=0})))
			month = month + 1
		end

		if month == 13 then
			month = 1
			year = year + 1
		end
		if day < 10 then
			return year..month.."0"..day
		else
			return year..month..day
		end
	end
	-- body
end


local function on_date_init()
	--local WorkDay = DY_DATA.WorkDay
-- Ref.SubTaskList.Grp:dup(#WorkDay,function (i,Ent,IsNew)

-- 	end)
	-- body
end

local function on_subtop_nextweek_click(btn)
	if ThisWeek then
		-- Ref.SubTop.lbWeek.text = "上周"
		ThisWeek = false
		on_ui_init()
	else
		-- Ref.SubTop.lbWeek.text = "下周"
		ThisWeek = true
		on_ui_init()
	end
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
	if UI_DATA.WNDSupWorkSelectShopTask.ThisWeek ~= nil then
		ThisWeek = UI_DATA.WNDSupWorkSelectShopTask.ThisWeek
		UI_DATA.WNDSupWorkSelectShopTask.ThisWeek = nil
	else
		ThisWeek = true
	end

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
	local startdata = DY_DATA.Work.NowTime.day:sub(1,4)..DY_DATA.Work.NowTime.day:sub(6,7)..DY_DATA.Work.NowTime.day:sub(9,10)
	-- local startdata = "20161128"
	for i=1,7 do
		table.insert(WeekList,{day = tonumber(startdata:sub(7,8)), data = startdata:sub(1,4).. "-" .. startdata:sub(5,6) .. "-" .. startdata:sub(7,8)})
		startdata = tomorrow(startdata,true)
	end
	NextWeekList = {}

	for i=1,7 do
		table.insert(NextWeekList,{day = tonumber(startdata:sub(7,8)), data = startdata:sub(1,4).. "-" .. startdata:sub(5,6) .. "-" .. startdata:sub(7,8)})
		startdata = tomorrow(startdata,true)
	end

	print("WeekList is :" .. JSON:encode(WeekList))
	print("NextWeekList is :" .. JSON:encode(NextWeekList))
	local projectId= UI_DATA.WNDSupWorkSelectShopTask.projectId
	local storeId = UI_DATA.WNDSupWorkSelectShopTask.storeIdtrue
	-- local 
	print("projectId-----storeId:"..projectId.."/"..storeId)
	ProjectList = DY_DATA.ProjectList
	local StoreList = ProjectList[projectId].StoreList
	local Store = DY_DATA.get_store(StoreList, storeId)
	local TaskList = Store.TaskList
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
	NW.unsubscribe("WORK.SC.GETASSIGNMENT",on_ui_init)
end

local P = {
start = start,
update_view = update_view,
on_recycle = on_recycle,
}
return P

