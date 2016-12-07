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

local Project, StoreList,CityList,TempStoreList
--!*以下：自动生成的回调函数*--




local function on_ui_init(cityid)
	if cityid == nil then cityid = 0 end
	local projectId = UI_DATA.WNDSupSelectStore.projectId
	local Project = DY_DATA.SchProjectList[projectId]
	StoreList = Project.SchStoreList
	if StoreList == nil then
		libunity.SetActive(Ref.SubStore.spNil, true)
		return 
	end
	libunity.SetActive(Ref.SubStore.spNil, #StoreList == 0)
	print("StoreList in WNDSupSelectStore" .. JSON:encode(StoreList))
	TempStoreList = {}
	if cityid == 0 then
		TempStoreList = StoreList
	else
		for i=1,#StoreList do
			if tonumber(StoreList[i].cityid) == cityid then
				table.insert(TempStoreList,StoreList[i])
			end
		end
	end
	Ref.SubStore.GrpStore:dup(#TempStoreList, function ( i, Ent, isNew)
		local TEXT = _G.ENV.TEXT
		local Store = TempStoreList[i]
		Ent.lbName.text = Store.name
		Ent.StoreState.text = TEXT.StoreState[tonumber(Store.takeorupload)]
		UIMGR.get_photo(Ent.spIcon, Store.icon)

	end)
end

local function on_select_city_callback( cityid )
	on_ui_init(cityid)
	-- body
end

local function on_substore_grpstore_entstore_click(btn)
	local index = tonumber(btn.name:sub(9))
	UI_DATA.WNDSupStoreData.projectId = UI_DATA.WNDSupSelectStore.projectId
	UI_DATA.WNDSupStoreData.storeId = TempStoreList[index].id
	UIMGR.create_window("UI/WNDSupStoreData")
end



local function on_subtop_btnback_click(btn)
	UIMGR.close_window(Ref.root)
end

local function on_subtop_btncity_click(btn)
		UI_DATA.WNDSelectPlace.FromWhere = "fromserver"
		UI_DATA.WNDSelectPlace.NeedGetCityList = true
		UI_DATA.WNDSelectPlace.projectId = UI_DATA.WNDSupSelectStore.projectId
		UI_DATA.WNDSelectPlace.callbackfunc = on_select_city_callback
		UIMGR.create("UI/WNDSelectPlace")
end




local function init_view()
	Ref.SubStore.GrpStore.Ent.btn.onAction = on_substore_grpstore_entstore_click
	Ref.SubTop.btnBack.onAction = on_subtop_btnback_click
	Ref.SubTop.BtnCity.onAction = on_subtop_btncity_click
	UIMGR.make_group(Ref.SubStore.GrpStore, function (New, Ent)
		New.btn.onAction = Ent.btn.onAction
	end)
	--!*以上：自动注册的回调函数*--
end

local function init_logic()
	NW.subscribe("WORK.SC.GETSTORE", on_ui_init)
	local projectId = UI_DATA.WNDSupSelectStore.projectId
	local Project = DY_DATA.SchProjectList[projectId]
	print("projectId in WNDSupSelectStore is " .. projectId)
	if Project.SchStoreList == nil or #Project.SchStoreList == 0 then
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

