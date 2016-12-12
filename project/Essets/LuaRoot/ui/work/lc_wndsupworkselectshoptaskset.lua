--
-- @file    ui/work/lc_wndsupworkselectshoptaskset.lua
-- @authors cks
-- @date    2016-11-09 06:48:31
-- @desc    WNDsupWorkSelectShopTaskSet
--

local ipairs, pairs
= ipairs, pairs
local libugui = require "libugui.cs"
local libunity = require "libunity.cs"
local UIMGR = MERequire "ui/uimgr"
local DY_DATA = MERequire "datamgr/dydata.lua"
local UI_DATA = MERequire "datamgr/uidata.lua"
local NW= MERequire "network/networkmgr"
local Ref

local storeId
local PersonListForUpdate
local PersonListForUpdateCallBack
local data
 --local dateState = true

--!*以下：自动生成的回调函数*--
local function time_to_string(Time)


	return string.format("%s-%s-%s %s:%s", Time.year, Time.month, Time.day, Time.hour, Time.minute)
end


local function on_set_sel_people_callback(PersonList)
	print("on_set_sel_people_callback-----------PersonList-------------------------------------------------"..JSON:encode(PersonList))
		print("on_set_sel_people_callback-----PersonListForUpdate-------------------------------------------------------" ..JSON:encode(PersonListForUpdate))
	PersonListForUpdateCallBack = PersonList
	local namelist = ""
	if PersonListForUpdateCallBack ~= nil then 
			 if PersonListForUpdateCallBack ~= {} then 
				for i=1,#PersonListForUpdateCallBack do
					if PersonListForUpdateCallBack[i].StateNumber ~= 2 then
						print("拼错字符串----------------------------------------------------------------------------")
						namelist = namelist..PersonListForUpdateCallBack[i].name..";"
					end
				end
			end
	end
	Ref.SubSelectPeople.data.text = namelist
end

local function on_subtop_btnback_click(btn)
	UIMGR.close_window(Ref.root)
end

local function on_subtop_nextweek_click(btn)	
	UI_DATA.WNDSupWorkSelectShopTaskSetSelPeople.dateState=true
	--UI_DATA.WNDSupWorkSelectShopTaskSetSelPeople.dateState=false												
	for i=1,#PersonListForUpdateCallBack do
		if PersonListForUpdateCallBack[i].StateNumber == 0 then
			if PersonListForUpdateCallBack[i].starttime ~= Ref.SubStartTime.data.text or PersonListForUpdateCallBack[i].endtime ~= Ref.SubEndTime.data.text then
				PersonListForUpdateCallBack[i].StateNumber = 3
			end
		end
	end

	print("_++++++++++++++++++++++++" .. JSON:encode(PersonListForUpdateCallBack))

	for i=1,#PersonListForUpdateCallBack do
		
		if PersonListForUpdateCallBack[i].StateNumber == 1 then	
			print("增加任务-------------------------------------"..JSON:encode(PersonListForUpdateCallBack))
			local nm = NW.msg("WORK.CS.ISSUED")
			nm:writeU32(tonumber(storeId))
			print("增加任务-----------storeId--------------------------"..storeId)
			nm:writeU32(1)
			nm:writeU32(PersonListForUpdateCallBack[i].id)
			print("增加任务-----------PersonListForUpdateCallBack[i].id--------------------------"..PersonListForUpdateCallBack[i].id)
			nm:writeU32(1)
			nm:writeString(Ref.SubStartTime.data.text)
			nm:writeString(Ref.SubEndTime.data.text)
			print("增加任务-----------time--------------------------"..PersonListForUpdateCallBack[i].id)
			NW.send(nm)
		end
		if PersonListForUpdateCallBack[i].StateNumber == 2 then	
			print("删除任务------------------------------------------"..Ref.SubStartTime.data.text.."/"..Ref.SubEndTime.data.text)
			local nm = NW.msg("WORK.CS.DELETEASS")
			nm:writeU32(1)
			nm:writeU32(PersonListForUpdateCallBack[i].taskId)
			NW.send(nm)
		end

		if PersonListForUpdateCallBack[i].StateNumber == 3 then	
			print("修改任务-------------------------------------------"..JSON.encode(PersonListForUpdateCallBack))
			local nm = NW.msg("WORK.CS.UPDATEASS")
			nm:writeU32(1)
			nm:writeU32(PersonListForUpdateCallBack[i].taskId)
			nm:writeU32(PersonListForUpdateCallBack[i].id)
			nm:writeString(Ref.SubStartTime.data.text)
			nm:writeString(Ref.SubEndTime.data.text)
			NW.send(nm)
		end



	end

	
		-- local nm = NW.msg("WORK.CS.UPDATEASS")
		
		-- nm:writeU32(#PersonListForUpdate)
		-- for i=1,#PersonListForUpdate do

		-- 	nm:writeU32(PersonListForUpdate[i].id)
		-- 	nm:writeU32(1)
		-- 	nm:writeString(Ref.SubStartTime.data.text)
		-- 	nm:writeString(Ref.SubEndTime.data.text)
		-- end
		-- NW.send(nm)
	PersonListForUpdateCallBack = nil
	UIMGR.close_window(Ref.root)

end

local function on_substarttime_btnbutton_click(btn)
	UI_DATA.WNDSetTime.on_call_back = function (Time)
		Ref.SubStartTime.data.text = time_to_string(Time) .. ":00"
	end

	local SourceTime = {

		year = data:sub(1,4),
		month = data:sub(6,7),
		day = data:sub(9,10),
	}	

	local SetInteractable = {

		year = 1,
		month = 1,
		day = 1,
	}
	UI_DATA.WNDSetTime.SourceTime = SourceTime
	UI_DATA.WNDSetTime.SetInteractable = SetInteractable
	UIMGR.create("UI/WNDSetTime")
end

local function on_subendtime_btnbutton_click(btn)
	UI_DATA.WNDSetTime.on_call_back = function (Time)
		Ref.SubEndTime.data.text = time_to_string(Time) .. ":00"
	end

		local SourceTime = {

		year = data:sub(1,4),
		month = data:sub(6,7),
		day = data:sub(9,10),
	}	

	local SetInteractable = {

		year = 1,
		month = 1,
	}
	UI_DATA.WNDSetTime.SourceTime = SourceTime
	UI_DATA.WNDSetTime.SetInteractable = SetInteractable
	UI_DATA.WNDSetTime.Task = 1
	UI_DATA.WNDSetTime.TaskData = {
		hour = tonumber(Ref.SubStartTime.data.text:sub(12,13)),
		minute = tonumber(Ref.SubStartTime.data.text:sub(15,16)),
	}

	UIMGR.create("UI/WNDSetTime")
end

local function on_subselectpeople_btnbutton_click(btn)

	UI_DATA.WNDSupWorkSelectShopTaskSetSelPeople.callbackfunc = on_set_sel_people_callback
	UI_DATA.WNDSupWorkSelectShopTaskSetSelPeople.PersonListForUpdate = PersonListForUpdateCallBack
	print("on_subselectpeople_btnbutton_click--------------------PersonListForUpdateCallBack"..JSON:encode(PersonListForUpdateCallBack))
	print("on_subselectpeople_btnbutton_click------------------111111--PersonListForUpdateCallBack"..JSON:encode(UI_DATA.WNDSupWorkSelectShopTaskSetSelPeople.PersonListForUpdate))
	UIMGR.create_window("UI/WNDSupWorkSelectShopTaskSetSelPeople")
end

local function init_view()
	Ref.SubTop.btnBack.onAction = on_subtop_btnback_click
	Ref.SubTop.NextWeek.onAction = on_subtop_nextweek_click
	Ref.SubStartTime.btnButton.onAction = on_substarttime_btnbutton_click
	Ref.SubEndTime.btnButton.onAction = on_subendtime_btnbutton_click
	Ref.SubSelectPeople.btnButton.onAction = on_subselectpeople_btnbutton_click
	--!*以上：自动注册的回调函数*--
end
local function init_logic()
		data = UI_DATA.WNDSupWorkSelectShopTaskSet.data
		--UI_DATA.WNDSupWorkSelectShopTaskSet.data = nil
		storeId = UI_DATA.WNDSupWorkSelectShopTask.storeIdtrue
	    local TaskList = UI_DATA.WNDSupWorkSelectShopTask.TaskList
	    local Tempn = UI_DATA.WNDSupWorkSelectShopTask.index
	    local n = tonumber(Tempn)
	    print("--------".. n)
	    print(JSON:encode(TaskList[n].PersonList))
	    local namelist = ""

		if UI_DATA.WNDSupWorkSelectShopTaskSetSelPeople.dateState then
			PersonListForUpdateCallBack = TaskList[n].PersonList--PersonListForUpdate
			print("第一次登录界面")
			PersonListForUpdate = TaskList[n].PersonList
			print("第一次登录界面------------PersonListForUpdateCallBack----"..JSON:encode( PersonListForUpdateCallBack).."///"..JSON:encode( PersonListForUpdate))
		    for i=1,#PersonListForUpdate do
				if PersonListForUpdate[i].StateNumber == nil then
					PersonListForUpdate[i].StateNumber = 0
				end
			end
		  	if TaskList[n].PersonList ~= nil then 
				 if TaskList[n].PersonList ~= {} then 
					for i=1,#TaskList[n].PersonList do
						namelist = namelist..TaskList[n].PersonList[i].name..";"
					end
				end
			end
			if TaskList[n].starttime ~= "" then
				print("初始化任务安排")
			    Ref.SubStartTime.data.text = TaskList[n].starttime
			    Ref.SubEndTime.data.text = TaskList[n].endtime
			    Ref.SubSelectPeople.data.text = namelist
			 else
			 	Ref.SubStartTime.data.text = "请输入时间"
			    Ref.SubEndTime.data.text = "请输入时间"
			    Ref.SubSelectPeople.data.text = "请选择人员"
			end
			print("function init_logic---storeId-----------")			
		else

			print("第二次登录界面")

			-- if UI_DATA.WNDSupWorkSelectShopTaskSetSelPeople.PersonListForUpdate ~= nil then
			-- 	local PersonList = UI_DATA.WNDSupWorkSelectShopTaskSetSelPeople.PersonListForUpdate
			-- 	if PersonList ~= nil then 
			-- 		 if PersonList ~= {} then 
			-- 			for i =1,#PersonList do
			-- 				namelist = namelist..PersonList[i].name..";"
			-- 			end
			-- 		end
			-- 	end
			-- 	Ref.SubStartTime.data.text = TaskList[n].starttime
			--     Ref.SubEndTime.data.text = TaskList[n].endtime
			--     Ref.SubSelectPeople.data.text = namelist
			-- end
		end

	-- NW.subscribe("WORK.SC.GETASSIGNMENT",on_store_init)


	-- local nm = NW.msg("WORK.CS.GETASSIGNMENT")
	-- nm:writeU32(storeId)
	-- NW.send(nm)
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

