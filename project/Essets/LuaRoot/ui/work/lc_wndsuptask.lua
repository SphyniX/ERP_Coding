--
-- @file    ui/work/lc_wndsuptask.lua
-- @authors zl
-- @date    2016-08-28 19:36:01
-- @desc    WNDSupTask
--

local ipairs, pairs
    = ipairs, pairs
local libugui = require "libugui.cs"
local libunity = require "libunity.cs"
local UIMGR = MERequire "ui/uimgr"
local DY_DATA = MERequire "datamgr/dydata.lua"
local UI_DATA = MERequire "datamgr/uidata.lua"
local NW = MERequire "network/networkmgr"
local TEXT = _G.ENV.TEXT
local Ref

local PromoterList
local SelectedList = {}
local DayList = {}

local on_time_init

local function on_selected(index, value)
	SelectedList[index] = value
end

local function on_finish(Ret)
	if Ret.ret == 1 then
		UIMGR.close_window(Ref.root)
	end
end

local function time_to_string(Time)
	local h = tonumber(Time.hour)
	local m = tonumber(Time.minute)
	local strH = h < 10 and "0"..h or h
	local strM = m < 10 and "0"..m or m
	local strS = "00"
	return string.format("%s:%s:%s", strH, strM, strS)
end
--!*以下：自动生成的回调函数*--

local function on_submain_grpday_entproduct_click(btn)
	local index = tonumber(btn.name:sub(11))
	-- local WorkDay = DY_DATA.WorkDay
	-- local Day = WorkDay[index]

	if DayList[index] == nil then
		DayList[index] = true
	else
		DayList[index] = nil
	end
	on_time_init()
end

local function on_submain_subinput_subend_click(btn)
	UI_DATA.WNDSetTime.on_call_back = function (Timer)
		Ref.SubMain.SubInput.SubEnd.lbText.text = time_to_string(Timer)
	end
	UIMGR.create("UI/WNDSetHour")
end

local function on_submain_subinput_substart_click(btn)
	UI_DATA.WNDSetTime.on_call_back = function (Timer)
		Ref.SubMain.SubInput.SubStart.lbText.text = time_to_string(Timer)
	end
	UIMGR.create("UI/WNDSetHour")
end

local function on_submain_subinfo_subpersons_grp_entperson_tglselect_change(tgl)
	local index = tonumber(tgl.transform.parent.name:sub(10))
	on_selected(index, tgl.value)
end

local function on_submain_subinfo_btnsubmit_click(btn)
	local starttime = Ref.SubMain.SubInput.SubStart.lbText.text
	local endtime = Ref.SubMain.SubInput.SubEnd.lbText.text
	
	if next(DayList) == nil then
		_G.UI.Toast:make(nil, "上班日期未填写"):show()
		return
	end

	if starttime == "" or endtime == "" then
		_G.UI.Toast:make(nil, "上班时间未填写"):show()
		return
	end
	
	local PersonList = {}
	
	for k,v in pairs(SelectedList) do
		if v then table.insert( PersonList,PromoterList[k]) end
	end

	if #PersonList == 0 then
		_G.UI.Toast:make(nil, "未选择促销员"):show()
		return
	end

	local nm = NW.msg("WORK.CS.ISSUED")
	local storeId =  UI_DATA.WNDSupTask.storeId
	
	nm:writeU32(storeId)
	nm:writeU32(#PersonList)

	local nDay = 0
	for _,_ in pairs(DayList) do
		nDay = nDay + 1
	end
	local time = starttime.."-"..endtime
	local WorkDay = DY_DATA.WorkDay
	for i,v in ipairs(PersonList) do
		nm:writeU32(v.id)
		nm:writeU32(nDay)
		for k,_ in pairs(DayList) do
			local Day = WorkDay[k]
			local day = Day.year.."-"..Day.month.."-"..Day.day
			nm:writeString(day)
			nm:writeString(time)
		end
	end
	
	NW.send(nm)
end

local function on_subtop_btnall_click(btn)
	local Ents = Ref.SubMain.SubInfo.SubPersons.Grp.Ents
	for i,v in ipairs(Ents) do
		v.tglSelect.value = true
		on_selected(i, true)
	end
end

local function on_subtop_btnback_click(btn)
	UIMGR.close_window(Ref.root)
end

local function on_person_init()
	PromoterList = DY_DATA.get_promoter_List()
	print(PromoterList)
	if PromoterList == nil then return end
	print(JSON:encode(PromoterList))
	Ref.SubMain.SubInfo.SubPersons.Grp:dup(#PromoterList, function (i, Ent, isNew)
		local Promoter = PromoterList[i]
		Ent.lbName.text = Promoter.name
		local state = Promoter.state
		libunity.SetActive(Ent.spBack, state == 2)
		libunity.SetActive(Ent.tglSelect, state == 1)
	end)
end

on_time_init = function ()
	local WorkDay = DY_DATA.WorkDay
	if WorkDay == nil or #WorkDay == 0 then
		return
	end
	local WD = WorkDay[1]
	Ref.SubMain.lbTime.text = string.format("%s年%s月", WD.year, WD.month)
	Ref.SubMain.GrpDay:dup(7, function (i, Ent, isNew)
		local n = (i + 2) % 7 + 1
		Ent.lbWeek.text = TEXT.WeekNum[n]
		local Day = WorkDay[i]
		Ent.lbDay.text = Day.day
		libunity.SetActive(Ent.spSelect, DayList[i])
	end)
end

local function init_view()
	Ref.SubMain.GrpDay.Ent.btn.onAction = on_submain_grpday_entproduct_click
	Ref.SubMain.SubInput.SubEnd.btn.onAction = on_submain_subinput_subend_click
	Ref.SubMain.SubInput.SubStart.btn.onAction = on_submain_subinput_substart_click
	Ref.SubMain.SubInfo.SubPersons.Grp.Ent.tglSelect.onAction = on_submain_subinfo_subpersons_grp_entperson_tglselect_change
	Ref.SubMain.SubInfo.btnSubmit.onAction = on_submain_subinfo_btnsubmit_click
	Ref.SubTop.btnAll.onAction = on_subtop_btnall_click
	Ref.SubTop.btnBack.onAction = on_subtop_btnback_click
	UIMGR.make_group(Ref.SubMain.GrpDay, function (New, Ent)
		New.btn.onAction = Ent.btn.onAction
	end)
	UIMGR.make_group(Ref.SubMain.SubInfo.SubPersons.Grp, function (New, Ent)
		New.tglSelect.onAction = Ent.tglSelect.onAction
	end)
	--!*以上：自动注册的回调函数*--
end

local function init_logic()
	NW.subscribe("WORK.SC.GETSTARTDATE", on_time_init)
	NW.subscribe("WORK.SC.GETSALES", on_person_init)
	NW.subscribe("WORK.SC.ISSUED", on_finish)
	local WorkDay = DY_DATA.WorkDay
	if WorkDay == nil or #WorkDay == 0 then
		local nm = NW.msg("WORK.CS.GETSTARTDATE")
		NW.send(nm)

		Ref.SubMain.GrpDay:dup(7, function (i, Ent, isNew)
			local n = (i + 2) % 7 + 1
			Ent.lbWeek.text = TEXT.WeekNum[n]
		end)
	else
		on_time_init()
	end
	
	if DY_DATA.PromoterList == nil or next(DY_DATA.PromoterList) == nil then
		local nm = NW.msg("WORK.CS.GETSALES")
		nm:writeU32(DY_DATA.User.id)
		NW.send(nm)
	else
		on_person_init()
	end
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
	NW.unsubscribe("WORK.SC.GETSTARTDATE", on_time_init)
	NW.unsubscribe("WORK.SC.GETSALES", on_person_init)
	NW.unsubscribe("WORK.SC.ISSUED", on_finish)
end

local P = {
	start = start,
	update_view = update_view,
	on_recycle = on_recycle,
}
return P

