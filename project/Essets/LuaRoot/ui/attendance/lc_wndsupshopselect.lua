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
--!*以下：自动生成的回调函数*--

local function on_subtop_btnbutton_click(btn)
	
end

local function on_subtop_back_click(btn)
	UIMGR.close_window(Ref.root)
end

local function on_subscroll_grp_ent_substate_click(btn)
	local text = btn.transform:GetChild(0):GetComponent("UILabel").text
	print("-------------------------"..text)
	if text =="离店" then
		DY_DATA.WNDsupShopSelectPunch.state=2
	else
		DY_DATA.WNDsupShopSelectPunch.state=1
	end
	DY_DATA.WNDsupShopSelect.StoreId=btn.transform.parent.name:sub(4)
	UIMGR.create_window("UI/WNDSupshopSelectPunch")
end

local function on_store_init()
	print("on_store_init")
	local Project = DY_DATA.AttendanceList[projectId]
	print(JSON:encode(Project))
	local StoreList = Project.AttStoreList
	if StoreList == nil then return end
	print(JSON:encode(StoreList))

	--local Ref_SubPunch = Ref.SubMain.SubScroll.SubContent.SubPunch.SubInfo
	Ref.SubScroll.Grp:dup(#StoreList, function( i, Ent, isNew)
		local Store = StoreList[i]
		print("---------------"..Store.id)
		Ent.lbText.text = Store.name
		if tostring(Store.state) =="1" then
			Ent.SubState.lbText.text = "离店"
		else
			Ent.SubState.lbText.text = "进店"
		end
		--Ent.SubState.lbText.text = Store.state == 1 and "离店" or "进店"
		print("店铺状态"..Store.state)
		-- Ent.btnButton:SetInteractable(Store.state == 2)
	end)
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
	local Project = DY_DATA.AttendanceList[projectId]
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

