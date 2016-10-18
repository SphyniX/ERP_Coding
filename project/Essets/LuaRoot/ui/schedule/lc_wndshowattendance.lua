--
-- @file    ui/schedule/lc_wndshowattendance.lua
-- @authors zl
-- @date    2016-09-04 19:54:10
-- @desc    WNDShowAttendance
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

local on_day_init

local AttInfoList
--!*以下：自动生成的回调函数*--

local function on_submain_grpcontent_enthard_subon_btnphoto_click(btn)
	local index = tonumber(btn.transform.parent.parent.name:sub(8))
	local Att = AttInfoList[index]
	UI_DATA.WNDShowPhoto.title = "头像"
	UI_DATA.WNDShowPhoto.tip = ""
	UI_DATA.WNDShowPhoto.photolist = {
		[1] = { title = "上班", name = Att.image1},
	}

	UI_DATA.WNDShowPhoto.callback = nil
   	UIMGR.create_window("UI/WNDShowPhoto")
end

local function on_submain_grpcontent_enthard_suboff_btnphoto_click(btn)
	local index = tonumber(btn.transform.parent.parent.name:sub(8))	
	local Att = AttInfoList[index]
	UI_DATA.WNDShowPhoto.photolist = {
		[1] = { title = "下班", name = Att.image2},
		[1] = { title = "竞品", name = Att.image3},
		[1] = { title = "竞品", name = Att.image4},
		[1] = { title = "竞品", name = Att.image5},
		[1] = { title = "竞品", name = Att.image6},
	}

	UI_DATA.WNDShowPhoto.callback = nil
   	UIMGR.create_window("UI/WNDShowPhoto")
end

local function on_submain_grpcontent_enthard_btninfo_click(btn)
	local index = tonumber(btn.transform.parent.name:sub(8))
	local Att = AttInfoList[index]
	
end

local function on_subtop_btnback_click(btn)
	UIMGR.close_window(Ref.root)
end

local function on_subtop_subtime_click(btn)
	UI_DATA.WNDSetTime.on_call_back = function (Time)
		local strTime = string.format("%d/%d/%d", Time.year, Time.month, Time.day)
		UI_DATA.WNDSelectStore.day = strTime
		on_day_init()
	end
	UIMGR.create("UI/WNDSetDay")
end

local function on_attlist_init()
	local storeId = UI_DATA.WNDSelectStore.storeId
	if storeId == nil then return end
	local day = UI_DATA.WNDSelectStore.day
	if day == nil then 
		--设置时间
		Ref.SubTop.SubTime.lbTime.text = "选择时间"
		return
	end
	Ref.SubTop.SubTime.lbTime.text = day
	if DY_DATA.AttInfoList == nil or DY_DATA.AttInfoList[storeId] == nil or DY_DATA.AttInfoList[storeId][day] == nil then
		return
	end	
	AttInfoList = DY_DATA.AttInfoList[storeId][day]

	Ref.SubMain.GrpContent:dup(#AttInfoList, function (i, Ent, IsNew)
		local Att = AttInfoList[i]
		Ent.lbName.text = Att.personName
		Ent.lbId.text = Att.personId
		Ent.SubOn.lbTime.text = Att.starttime
		Ent.SubOff.lbTime.text = Att.endtime
	end)
end

on_day_init = function()
	local storeId = UI_DATA.WNDSelectStore.storeId
	if storeId == nil then return end
	local day = UI_DATA.WNDSelectStore.day
	if day == nil then 
		--设置时间
		Ref.SubTop.SubTime.lbTime.text = "选择时间"
		return 
	end
	Ref.SubTop.SubTime.lbTime.text = day
	if DY_DATA.AttInfoList == nil or DY_DATA.AttInfoList[storeId] == nil or DY_DATA.AttInfoList[storeId][day] == nil then
		local nm = NW.msg("REPORTED.CS.GETATTINFOR")
		nm:writeU32(storeId)
		nm:writeString(day)
		NW.send(nm)
		return
	end
	on_attlist_init()
end

local function init_view()
	Ref.SubMain.GrpContent.Ent.SubOn.btnPhoto.onAction = on_submain_grpcontent_enthard_subon_btnphoto_click
	Ref.SubMain.GrpContent.Ent.SubOff.btnPhoto.onAction = on_submain_grpcontent_enthard_suboff_btnphoto_click
	Ref.SubMain.GrpContent.Ent.btnInfo.onAction = on_submain_grpcontent_enthard_btninfo_click
	Ref.SubTop.btnBack.onAction = on_subtop_btnback_click
	Ref.SubTop.SubTime.btn.onAction = on_subtop_subtime_click
	UIMGR.make_group(Ref.SubMain.GrpContent, function (New, Ent)
		New.SubOn.btnPhoto.onAction = Ent.SubOn.btnPhoto.onAction
		New.SubOff.btnPhoto.onAction = Ent.SubOff.btnPhoto.onAction
		New.btnInfo.onAction = Ent.btnInfo.onAction
	end)
	--!*以上：自动注册的回调函数*--
end

local function init_logic()
	NW.subscribe("REPORTED.SC.GETATTINFOR", on_attlist_init)
	
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
	NW.unsubscribe("REPORTED.SC.GETATTINFOR", on_attlist_init)
end

local function on_recycle()
	
end

local P = {
	start = start,
	update_view = update_view,
	on_recycle = on_recycle,
}
return P

