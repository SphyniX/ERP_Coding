--
-- @file    ui/work/lc_wndsupworkselectshop.lua
-- @authors cks
-- @date    2016-11-09 06:13:54
-- @desc    WNDsupWorkSelectShop
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
local StoreList

local TempStoreList
--!*以下：自动生成的回调函数*--


local function on_ui_init(cityid)

	if cityid == nil then cityid = 0 end
	local projectId = UI_DATA.WNDSelectStore.projectId
	local Project = DY_DATA.ProjectList[projectId]
	StoreList = Project.StoreList
	if StoreList == nil then
		libunity.SetActive(Ref.SubStore.spNil, true)
		return 
	end
	libunity.SetActive(Ref.SubStore.spNil, #StoreList == 0)
	print("storelist init:"..#StoreList..JSON:encode(StoreList))

	TempStoreList = {}

	if cityid == 0 then
		TempStoreList = StoreList
	else
		print("cityid is :" .. cityid)
		for i=1,#StoreList do
			print("StoreList " .. i .. ".cityid is :" .. StoreList[i].cityid)
			if tonumber(StoreList[i].cityid) == cityid then
				table.insert(TempStoreList,StoreList[i])

			end
		end
	end

	print("TempStoreList is :" .. JSON:encode(TempStoreList))

	Ref.SubStore.GrpStore:dup(#TempStoreList, function ( i, Ent, isNew)
		local Store = TempStoreList[i]
		Ent.lbName.text = Store.name
		UIMGR.get_photo(Ent.spIcon, Store.icon)
		-- local clr = i % 3
		-- libunity.SetActive(Ent.spRed, clr == 1)
		-- libunity.SetActive(Ent.spBlue, clr == 2)
		-- libunity.SetActive(Ent.spYellow, clr == 0)
	end)


	-- local StoreList = DY_DATA.StoreList
	-- if StoreList~=nil then
	-- 	print("lc_wndsupworkselectshop.lua  数据获取成功")
	-- else
	-- 	print("lc_wndsupworkselectshop.lua  数据获取失败")
	-- end
	-- Ref.SubStore.GrpStore:dup(#StoreList,function (i,Ent,Isnew)
	-- 	local storeMsg =StoreList[i]
	-- 	Ent.lbName.text=storeMsg.name

	-- 	end)
end

local function on_substore_grpstore_entstore_click(btn)
	local index = tonumber(btn.name:sub(9))
	local Store = StoreList[index]
	local on_selected = UI_DATA.WNDSelectStore.on_selected
	if on_selected then on_selected(Store.id) return end
	
	UI_DATA.WNDSupWorkSelectShopTask.projectId = Store.projectId
	UI_DATA.WNDSupWorkSelectShopTask.storeId = index
	UI_DATA.WNDSupWorkSelectShopTask.storeIdtrue = Store.id
	print("UI_DATA.WNDSupWorkSelectShopTask.storeId is " .. UI_DATA.WNDSupWorkSelectShopTask.storeId)
	--print("on_substore_grpstore_entstore_click--------"..tostring(btn.name)..tostring(btn.name))
	--DY_DATA.
	-- local index = tonumber(btn.transform.parent.name:sub(11))
	-- print("lc_wndsupwork.lua--------"..btn.transform.name..tostring(btn.name:sub(11)))
	--UI_DATA.WNDSelectStore.projectId = ProjectList[index].id
	UIMGR.create_window("UI/WNDSupWorkSelectShopTask")
end

local function on_subtop_btnback_click(btn)
	UIMGR.create_window("UI/WNDSupWork")
end

local function on_select_city_callback( cityid )
	on_ui_init(cityid)
	-- body
end

local function on_subtop_btncity_click(btn)
	UI_DATA.WNDSelectPlace.FromWhere = "fromserver"
		UI_DATA.WNDSelectPlace.NeedGetCityList = true
		UI_DATA.WNDSelectPlace.projectId = UI_DATA.WNDSelectStore.projectId
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
	UI_DATA.WNDSupWorkSelectShopTaskSetSelPeople.dateState = true
	NW.subscribe("WORK.SC.GETSTORE", on_ui_init)
	local projectId = UI_DATA.WNDSelectStore.projectId
	local Project = DY_DATA.ProjectList[projectId]
	print(JSON:encode(Project))
	if Project.StoreList == nil or #Project.StoreList == 0 then
		local nm = NW.msg("WORK.CS.GETSTORE")
		nm:writeU32(projectId)
		nm:writeU32(DY_DATA.User.id)
		NW.send(nm)
		return
	end
	on_ui_init()


	-- NW.subscribe("WORK.SC.GETSTORE", on_ui_init)

	-- if DY_DATA.StoreList == nil or next(DY_DATA.StoreList) == nil then
	-- 	if NW.connected() then
	-- 		local nm = NW.msg("WORK.CS.GETSTORE")
	-- 		nm:writeU32(DY_DATA.User.id)
	-- 		NW.send(nm)
	-- 	end
	-- 	return
	-- end
	--Ref.SubStore.GrpStore:dup(7,function (i,Ent,Isnew)

		-- body
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
	--NW.unsubscribe("WORK.SC.GETSTORE", on_ui_init)
	end

	local P = {
	start = start,
	update_view = update_view,
	on_recycle = on_recycle,
}
return P

