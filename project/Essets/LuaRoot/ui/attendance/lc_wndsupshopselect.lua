--
-- @file    ui/attendance/lc_wndsupshopselect.lua
-- @authors cks
-- @date    2016-11-13 21:37:47
-- @desc    WNDSupshopSelect
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
local wndsupshopselect_backCall
local projectId 
local TempStoreList
--!*以下：自动生成的回调函数*--

local function on_store_init(cityid)
	if cityid == nil then cityid = 0 end

	print("on_store_init")
	local ProjectList = DY_DATA.get_attendance_list()
	local Project
	for i=1,#ProjectList do
		if ProjectList[i].id == projectId then
			Project =  ProjectList[i]
		end
	end
	print(JSON:encode(Project))
	local StoreList = Project.AttStoreList
	if StoreList == nil then return end
	print(JSON:encode(StoreList))

	TempStoreList = {}

	if cityid == 0 then
		TempStoreList = StoreList
	else
		for i=1,#StoreList do
			if tonumber(StoreList[i].cityid) == tonumber(cityid) then
				table.insert(TempStoreList,StoreList[i])
			end
		end
	end
	libunity.SetActive(Ref.SubScroll.spNil, #TempStoreList == 0)
	--local Ref_SubPunch = Ref.SubMain.SubScroll.SubContent.SubPunch.SubInfo
	Ref.SubScroll.Grp:dup(#TempStoreList, function( i, Ent, isNew)
		local Store = TempStoreList[i]
		print("---------------"..Store.id)
		Ent.lbText.text = Store.name
		if tostring(Store.state) =="1" then
			Ent.SubState.lbText.text = "离店"
			Ent.SubState.btn:SetInteractable(true)
		elseif tostring(Store.state) =="2" then
			Ent.SubState.lbText.text = "进店"
			Ent.SubState.btn:SetInteractable(true)
		else
			Ent.SubState.lbText.text = "已打卡"
			Ent.SubState.btn:SetInteractable(false)
		end
		UIMGR.get_photo(Ent.spImage, Store.icon)
		--Ent.SubState.lbText.text = Store.state == 1 and "离店" or "进店"
		print("店铺状态"..Store.state)
		-- Ent.btnButton:SetInteractable(Store.state == 2)
	end)
end

local function on_select_city_callback( cityid )
	on_store_init(cityid)
	-- body
end

local function on_subtop_btnbutton_click(btn)
	
		UI_DATA.WNDSelectPlace.FromWhere = "fromserver"
		UI_DATA.WNDSelectPlace.NeedGetCityList = true
		UI_DATA.WNDSelectPlace.projectId = tonumber(projectId)
		UI_DATA.WNDSelectPlace.callbackfunc = on_select_city_callback
		UIMGR.create("UI/WNDSelectPlace")
end

local function on_subtop_back_click(btn)
	UIMGR.close_window(Ref.root)
end

local function on_subscroll_grp_ent_substate_click(btn)
	local text = btn.transform:GetChild(0):GetComponent("UILabel").text
	local index = tonumber(btn.transform.parent.name:sub(4))

	print("-------------------------"..index)
	if text =="离店" then
		DY_DATA.WNDsupShopSelectPunch.state=2
	else
		DY_DATA.WNDsupShopSelectPunch.state=1
	end
	-- print("TempStoreList is " .. JSON:encode(TempStoreList))
	DY_DATA.WNDsupShopSelect.StoreId = TempStoreList[index].id
	UIMGR.create_window("UI/WNDSupshopSelectPunch")
end



local function init_view()
	Ref.SubTop.btnButton.onAction = on_subtop_btnbutton_click
	Ref.SubTop.back.onAction = on_subtop_back_click
	Ref.SubScroll.Grp.Ent.SubState.btn.onAction = on_subscroll_grp_ent_substate_click
	UIMGR.make_group(Ref.SubScroll.Grp, function (New, Ent)
		New.SubState.btn.onAction = Ent.SubState.btn.onAction
	end)
	--!*以上：自动注册的回调函数*--
end

local function init_logic()
	projectId = DY_DATA.WNDsupShopSelect.projectId
	DY_DATA.WNDsupShopSelect.ShopSelectBackCall = wndsupshopselect_backCall
	local ProjectList = DY_DATA.get_attendance_list()
	local Project
	for i=1,#ProjectList do
		if ProjectList[i].id == projectId then
			Project =  ProjectList[i]
		end
	end
	print("Project is :" .. JSON:encode(Project))
	Project.AttStoreList = nil
    local StoreList = Project.AttStoreList
	NW.subscribe("WORK.SC.GETSTORE", on_store_init)
	local Project = DY_DATA.AttendanceList[projectId]
	print(JSON:encode(Project))
	StoreList = nil
	if StoreList == nil or #StoreList== 0 then
		print("放消息")
		local nm = NW.msg("WORK.CS.GETSTORE")
		nm:writeU32(projectId)
		nm:writeU32(DY_DATA.User.id)
		NW.send(nm)
		return
	end

	--NW.MSG("USER.CS.GETSTORE", on_ui_init)
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
	NW.unsubscribe("WORK.SC.GETSTORE", on_store_init)
end

local P = {
	start = start,
	update_view = update_view,
	on_recycle = on_recycle,
}
return P

