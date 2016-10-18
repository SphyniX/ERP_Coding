--
-- @file    ui/schedule/lc_wndshowmechanism.lua
-- @authors zl
-- @date    2016-09-04 05:16:19
-- @desc    WNDShowMechanism
--

local ipairs, pairs
    = ipairs, pairs
local libugui = require "libugui.cs"
local libunity = require "libunity.cs"
local libsystem = require "libsystem.cs"
local UIMGR = MERequire "ui/uimgr"
local DY_DATA = MERequire "datamgr/dydata.lua"
local NW = MERequire "network/networkmgr"
local UI_DATA = MERequire "datamgr/uidata.lua"
local Ref
local on_day_init
--!*以下：自动生成的回调函数*--

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

local function on_mechanism_init()
	local storeId = UI_DATA.WNDSelectStore.storeId
	if storeId == nil then return end
	local day = UI_DATA.WNDSelectStore.day
	if day == nil then 
		--设置时间
		Ref.SubTop.SubTime.lbTime.text = "选择时间"
		return
	end
	Ref.SubTop.SubTime.lbTime.text = day

	if DY_DATA.MechanismList == nil or DY_DATA.MechanismList[storeId] == nil or DY_DATA.MechanismList[storeId][day] == nil then
		return
	end
	local MechanismList = DY_DATA.MechanismList[storeId][day]
	-- debug
	-- MechanismList = {
	-- 	{ id = 1, name = "aa", PersonList = {{id = 2, name = "AA", context = "asdf"},{id = 2, name = "AA", context = "asdf"},},},
	-- 	{ id = 1, name = "aa", PersonList = {{id = 2, name = "AA", context = "asdf"},{id = 2, name = "AA", context = "asdf"},},},
	-- 	{ id = 1, name = "aa", PersonList = {{id = 2, name = "AA", context = "asdf"},{id = 2, name = "AA", context = "asdf"},},},
	-- 	{ id = 1, name = "aa", PersonList = {{id = 2, name = "AA", context = "asdf"},{id = 2, name = "AA", context = "asdf"},},},
	-- }
	Ref.SubMain.Grp:dup( #MechanismList, function (i, Ent,  isNew)
		local Mechanism = MechanismList[i]
		Ent.lbName.text = Mechanism.name
		local PersonList = Mechanism.PersonList
		UIMGR.dup_new_group(Ent, Ent.go, "Grp", Ref.SubInfo.root, #PersonList, function (j, EntJ, isNewJ)
			local Person = PersonList[j]
			EntJ.lbName.text = Person.name
			EntJ.lbText.text = Person.context
		end)
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
	if DY_DATA.MechanismList == nil or DY_DATA.MechanismList[storeId] == nil or DY_DATA.MechanismList[storeId][day] == nil then
		local nm = NW.msg("REPORTED.CS.GETMECHANRE")
		nm:writeU32(storeId)
		nm:writeString(day)
		NW.send(nm)
		return
	end
	on_mechanism_init()
end

local function init_view()
	Ref.SubTop.btnBack.onAction = on_subtop_btnback_click
	Ref.SubTop.SubTime.btn.onAction = on_subtop_subtime_click
	UIMGR.make_group(Ref.SubInfo)
	UIMGR.make_group(Ref.SubMain.Grp)
	--!*以上：自动注册的回调函数*--
end

local function init_logic()
	NW.subscribe("REPORTED.SC.GETMECHANRE", on_mechanism_init)
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
	
end

local function on_recycle()
	NW.unsubscribe("REPORTED.SC.GETMECHANRE", on_mechanism_init)
end

local P = {
	start = start,
	update_view = update_view,
	on_recycle = on_recycle,
}
return P

