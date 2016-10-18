--
-- @file    ui/schedule/lc_wndsupselectstore.lua
-- @authors zl
-- @date    2016-08-09 15:24:13
-- @desc    WNDSupSelectStore
--

local ipairs, pairs
    = ipairs, pairs
local libugui = require "libugui.cs"
local libunity = require "libunity.cs"
local UIMGR = MERequire "ui/uimgr"
local DY_DATA = MERequire "datamgr/dydata.lua"
local UI_DATA = MERequire "datamgr/uidata.lua"
local NW = MERequire "network/networkmgr"
local Ref

local Project, StoreList
--!*以下：自动生成的回调函数*--

local function on_substore_grpstore_entstore_btnschedule_click(btn)
	local index = tonumber(btn.transform.parent.name:sub(9))
	local Store = StoreList[index]
	UI_DATA.WNDSelectStore.storeId = Store.id
	UI_DATA.WNDSelectStore.day = nil
	UIMGR.create_window("UI/WNDSupShowSchedule")
end

local function on_substore_grpstore_entstore_btnmechanism_click(btn)
	
	local index = tonumber(btn.transform.parent.name:sub(9))
	local Store = StoreList[index]
	UI_DATA.WNDSelectStore.storeId = Store.id
	UI_DATA.WNDSelectStore.day = nil
	UIMGR.create_window("UI/WNDShowMechanism")
end

local function on_substore_grpstore_entstore_btnperson_click(btn)
	local index = tonumber(btn.transform.parent.name:sub(9))
	local Store = StoreList[index]
	UI_DATA.WNDSelectStore.storeId = Store.id
	UI_DATA.WNDSelectStore.day = nil
	UIMGR.create_window("UI/WNDShowAttendance")
end

local function on_substore_grpstore_entstore_btnshowcompare_click(btn)
	local index = tonumber(btn.transform.parent.name:sub(9))
	local Store = StoreList[index]
	UI_DATA.WNDSelectStore.storeId = Store.id
	UI_DATA.WNDSelectStore.day = nil
	UIMGR.create_window("UI/WNDShowCompeteProduct")
end

local function on_substore_grpstore_entstore_btnsubmitcompare_click(btn)
	local index = tonumber(btn.transform.parent.name:sub(9))
	local Store = StoreList[index]
	UI_DATA.WNDSubmitSchedule.projectId = Store.projectId
	UI_DATA.WNDSubmitSchedule.storeId = Store.id
	UI_DATA.WNDSetCompeteProduct.type = 2
	UI_DATA.WNDSetCompeteProduct.callback = function (SubmitList)
		local nm = NW.msg("REPORTED.CS.COM")

		nm:writeU32(1)
	end
	UIMGR.create_window("UI/WNDSetCompeteProduct")
end

local function on_substore_grpstore_entstore_btninfo_click(btn)
	local index = tonumber(btn.transform.parent.name:sub(9))
	local Store = StoreList[index]
	UI_DATA.WNDSelectStore.storeId = Store.id
	UI_DATA.WNDSelectStore.day = nil
	UIMGR.create_window("UI/WNDShowInfo")
end

local function on_subtop_btnback_click(btn)
	UIMGR.close_window(Ref.root)
end

local function on_ui_init()
	local projectId = UI_DATA.WNDSelectStore.projectId
	local Project = DY_DATA.ProjectList[projectId]
	StoreList = Project.StoreList
	if StoreList == nil then
		libunity.SetActive(Ref.SubStore.spNil, true)
		return 
	end
	libunity.SetActive(Ref.SubStore.spNil, #StoreList == 0)
	print(JSON:encode(StoreList))
	Ref.SubStore.GrpStore:dup(#StoreList, function ( i, Ent, isNew)
		local Store = StoreList[i]
		Ent.lbName.text = Store.name
		UIMGR.get_photo(Ent.spIcon, Store.icon)
		local clr = i % 3
		libunity.SetActive(Ent.spRed, clr == 1)
		libunity.SetActive(Ent.spBlue, clr == 2)
		libunity.SetActive(Ent.spYellow, clr == 0)
	end)
end

local function init_view()
	Ref.SubStore.GrpStore.Ent.btnSchedule.onAction = on_substore_grpstore_entstore_btnschedule_click
	Ref.SubStore.GrpStore.Ent.btnMechanism.onAction = on_substore_grpstore_entstore_btnmechanism_click
	Ref.SubStore.GrpStore.Ent.btnPerson.onAction = on_substore_grpstore_entstore_btnperson_click
	Ref.SubStore.GrpStore.Ent.btnShowCompare.onAction = on_substore_grpstore_entstore_btnshowcompare_click
	Ref.SubStore.GrpStore.Ent.btnSubmitCompare.onAction = on_substore_grpstore_entstore_btnsubmitcompare_click
	Ref.SubStore.GrpStore.Ent.btnInfo.onAction = on_substore_grpstore_entstore_btninfo_click
	Ref.SubTop.btnBack.onAction = on_subtop_btnback_click
	UIMGR.make_group(Ref.SubStore.GrpStore, function (New, Ent)
		New.btnSchedule.onAction = Ent.btnSchedule.onAction
		New.btnMechanism.onAction = Ent.btnMechanism.onAction
		New.btnPerson.onAction = Ent.btnPerson.onAction
		New.btnShowCompare.onAction = Ent.btnShowCompare.onAction
		New.btnSubmitCompare.onAction = Ent.btnSubmitCompare.onAction
		New.btnInfo.onAction = Ent.btnInfo.onAction
	end)
	--!*以上：自动注册的回调函数*--
end

local function init_logic()
	NW.subscribe("WORK.SC.GETSTORE", on_ui_init)
	local projectId = UI_DATA.WNDSelectStore.projectId
	local Project = DY_DATA.ProjectList[projectId]
	if Project.StoreList == nil or #Project.StoreList == 0 then
		local nm = NW.msg("WORK.CS.GETSTORE")
		nm:writeU32(projectId)
		nm:writeU32(DY_DATA.User.id)
		NW.send(nm)
		return
	end
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
	NW.unsubscribe("WORK.SC.GETSTORE", on_ui_init)
end

local P = {
	start = start,
	update_view = update_view,
	on_recycle = on_recycle,
}
return P

