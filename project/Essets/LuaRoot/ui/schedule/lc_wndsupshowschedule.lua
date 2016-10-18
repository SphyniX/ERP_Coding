--
-- @file    ui/schedule/lc_wndsupshowschedule.lua
-- @authors zl
-- @date    2016-08-15 10:26:34
-- @desc    WNDSupShowSchedule
--

local ipairs, pairs
    = ipairs, pairs
local libugui = require "libugui.cs"
local libunity = require "libunity.cs"
local libsystem = require "libsystem.cs"
local UIMGR = MERequire "ui/uimgr"
local UI_DATA = MERequire "datamgr/uidata.lua"
local DY_DATA = MERequire "datamgr/dydata.lua"
local NW = MERequire "network/networkmgr"
local Ref

local ScheduleList, curScheduleIndex, titleType
local TitleList = {
	[1] = { title = "sales", name = "总销额" },
	[2] = { title = "volume", name = "总销量" },
	[3] = { title = "price", name = "促销价" },
	[4] = { title = "average_price", name = "通常价" },
}

local on_type_init, on_day_init, on_schedule_init
--!*以下：自动生成的回调函数*--

local function on_submain_subinfo_subtype_subselecttype_sub_grp_enttype_click(btn)
	local index = tonumber(btn.name:sub(8))
	titleType = index
	libunity.SetActive(Ref.SubMain.SubInfo.SubType.SubSelectType.root, false)
	on_type_init()
end

local function on_submain_subproduct_btnleft_click(btn)
	if curScheduleIndex == nil then curScheduleIndex = 1 end
	curScheduleIndex = curScheduleIndex - 1
	curScheduleIndex = curScheduleIndex < 1 and 1 or curScheduleIndex
	on_schedule_init()
end

local function on_submain_subproduct_btnright_click(btn)
	if curScheduleIndex == nil then curScheduleIndex = #ScheduleList end
	curScheduleIndex = curScheduleIndex - 1
	curScheduleIndex = curScheduleIndex > #ScheduleList and #ScheduleList or curScheduleIndex
	on_schedule_init()
end

local function on_submain_subinfo_subtype_click(btn)
	libunity.SetActive(Ref.SubMain.SubInfo.SubType.SubSelectType.root, true)
end

local function on_submain_subinfo_subtype_subselecttype_btnback_click(btn)
	libunity.SetActive(Ref.SubMain.SubInfo.SubType.SubSelectType.root, false)
end

local function on_subtop_subtime_click(btn)
	UI_DATA.WNDSetTime.on_call_back = function (Time)
		local strTime = string.format("%d/%d/%d", Time.year, Time.month, Time.day)
		UI_DATA.WNDSelectStore.day = strTime
		on_day_init()
	end
	UIMGR.create("UI/WNDSetDay")
end

local function on_subtop_btnback_click(btn)
	UIMGR.close_window(Ref.root)
end

local function on_person_init()
	local title = TitleList[titleType].title
	if ScheduleList == nil then return end
	local Schedule = ScheduleList[curScheduleIndex]
	if Schedule == nil then return end
	local PersonList = Schedule.PersonList
	if PersonList == nil then return end
	Ref.SubMain.SubInfo.SubPersons.Grp:dup( #PersonList, function (i, Ent, isNew)
		local Person = PersonList[i]
		Ent.lbName.text = Person.name
		Ent.lbValue.text = Person[title]
	end)
end

on_type_init = function ()
	if titleType == nil then titleType = 1 end
	if TitleList[titleType] == nil then return end
	Ref.SubMain.SubInfo.SubType.lbType.text = TitleList[titleType].name
	on_person_init()
end

on_schedule_init = function ()
	if curScheduleIndex == nil then curScheduleIndex = 1 end
	if ScheduleList[curScheduleIndex] == nil then return end
	local Schedule = ScheduleList[curScheduleIndex]

	-- 设置图片
	-- Ref.SubMain.SubProduct.spIcon
	Ref.SubMain.SubProduct.lbText.text = Schedule.name
	on_type_init()
end

local function on_schedulelist_init()
	local storeId = UI_DATA.WNDSelectStore.storeId
	-- local StoreList = Project.StoreList
	-- local Store = StoreList[storeId]
	local day = UI_DATA.WNDSelectStore.day

	if DY_DATA.ScheduleList == nil or DY_DATA.ScheduleList[storeId] == nil or DY_DATA.ScheduleList[storeId][day] == nil then	
		return
	end
	ScheduleList = DY_DATA.ScheduleList[storeId][day]
	if ScheduleList == nil then return end
	on_schedule_init()
end

on_day_init = function ()
	local storeId = UI_DATA.WNDSelectStore.storeId
	if storeId == nil then return end
	local day = UI_DATA.WNDSelectStore.day
	if day == nil then 
		--设置时间
		Ref.SubTop.SubTime.lbTime.text = "选择时间"
		return 
	end
	Ref.SubTop.SubTime.lbTime.text = day

	if DY_DATA.ScheduleList == nil or DY_DATA.ScheduleList[storeId] == nil or DY_DATA.ScheduleList[storeId][day] == nil then	
		local nm = NW.msg("REPORTED.CS.GETREP")
		nm:writeU32(storeId)
		nm:writeString(day)
		NW.send(nm)
		return
	end
	on_schedulelist_init()
end

local function init_view()
	Ref.SubMain.SubInfo.SubType.SubSelectType.Sub.Grp.Ent.btn.onAction = on_submain_subinfo_subtype_subselecttype_sub_grp_enttype_click
	Ref.SubMain.SubProduct.btnLeft.onAction = on_submain_subproduct_btnleft_click
	Ref.SubMain.SubProduct.btnRight.onAction = on_submain_subproduct_btnright_click
	Ref.SubMain.SubInfo.SubType.btn.onAction = on_submain_subinfo_subtype_click
	Ref.SubMain.SubInfo.SubType.SubSelectType.btnBack.onAction = on_submain_subinfo_subtype_subselecttype_btnback_click
	Ref.SubTop.SubTime.btn.onAction = on_subtop_subtime_click
	Ref.SubTop.btnBack.onAction = on_subtop_btnback_click
	UIMGR.make_group(Ref.SubMain.SubInfo.SubPersons.Grp)
	UIMGR.make_group(Ref.SubMain.SubInfo.SubType.SubSelectType.Sub.Grp, function (New, Ent)
		New.btn.onAction = Ent.btn.onAction
	end)
	--!*以上：自动注册的回调函数*--
end

local function init_logic()
	NW.subscribe("REPORTED.SC.GETREP", on_schedulelist_init)
	ScheduleList = nil
	curScheduleIndex = nil
	titleType = nil

	libunity.SetActive(Ref.SubMain.SubInfo.SubType.SubSelectType.root, true)
	Ref.SubMain.SubInfo.SubType.SubSelectType.Sub.Grp:dup(#TitleList, function (i, Ent, isNew)
		Ent.lbName.text = TitleList[i].name
	end)
	libunity.SetActive(Ref.SubMain.SubInfo.SubType.SubSelectType.root, false)

	local day = UI_DATA.WNDSelectStore.day
	if day == nil then
		local dateTime = libsystem.DateTime()
		print(dateTime)
		local strT = dateTime:split(' ')
		print(JSON:encode(dateTime))
		day = strT[1]
		UI_DATA.WNDSelectStore.day = day
		--设置时间
	end
	if titleType == nil then titleType = 1 end 
	if TitleList[titleType] then
		Ref.SubMain.SubInfo.SubType.lbType.text = TitleList[titleType].name
	else
		Ref.SubMain.SubInfo.SubType.lbType.text = "选择类型"
	end

	on_day_init()
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
	NW.unsubscribe("REPORTED.SC.GETREP", on_schedulelist_init)
end

local P = {
	start = start,
	update_view = update_view,
	on_recycle = on_recycle,
}
return P

