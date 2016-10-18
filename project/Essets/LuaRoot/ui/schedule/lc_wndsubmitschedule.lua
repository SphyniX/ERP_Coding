--
-- @file    ui/schedule/lc_wndsubmitschedule.lua
-- @authors ckxz
-- @date    2016-07-29 10:36:16
-- @desc    WNDSubmitSchedule
--

local ipairs, pairs
    = ipairs, pairs
local libugui = require "libugui.cs"
local libunity = require "libunity.cs"
local UIMGR = MERequire "ui/uimgr"
local UI_DATA = MERequire "datamgr/uidata.lua"
local DY_DATA = MERequire "datamgr/dydata.lua"
local NW = MERequire "network/networkmgr"
local TEXT = _G.ENV.TEXT
local Ref

local Project, Store

local function on_submit_image(Photolist, nm)
	if #Photolist == 0 then
		NW.send(nm)
		return
	end
	local nPhoto = 0
	for i,v in ipairs(Photolist) do
   		LOGIN.try_uploadphoto(DY_DATA.User.id, 18, nil,v, function ()
   			nPhoto = nPhoto + 1
   			if nPhoto >= #Photolist then
	   			NW.send(nm)
	   		end
   		end)
   	end
end
--!*以下：自动生成的回调函数*--

local function on_submain_subcontent_subinfolist_btnproduct_click(btn)
	-- ProductList
	UIMGR.create_window("UI/WNDSetPromoteProduct")
end

local function on_submain_subcontent_subinfolist_btnsetcompeteproduct_click(btn)
	-- CompeteProductList
	UIMGR.create_window("UI/WNDSetCompeteProduct")
end

local function on_submain_subcontent_subinfolist_btnpromoteinfo_click(btn)
	-- ProductList
	UIMGR.create_window("UI/WNDSetPromoteInfo")
end

local function on_submain_subcontent_btnbutton_click(btn)
	-- libunity.SetActive(Ref.SubMain.SubContent.SubTip.root, true)
	libunity.SetActive(Ref.SubMain.SubContent.SubTip.root, false)
	if NW.connected() then
		local storeId = UI_DATA.WNDSubmitSchedule.storeId
		local projectId = UI_DATA.WNDSubmitSchedule.projectId
		local nm = NW.msg("REPORTED.CS.REPORTEDPRO")
		nm:writeU32(DY_DATA.User.id)
		nm:writeU32(storeId)
		local ProductList = UI_DATA.WNDSubmitSchedule.ProductList
		local CompeteProductList =UI_DATA.WNDSubmitSchedule.CompeteProductList
		local MechanismList = UI_DATA.WNDSubmitSchedule.MechanismList

		if ProductList == nil then ProductList = {} end
		if MechanismList == nil then MechanismList = {} end
		if CompeteProductList == nil then CompeteProductList = {} end

		nm:writeU32(#ProductList)
		for _,v in ipairs(ProductList) do
			nm:writeU32(v.id)
			nm:writeU32(v.price == "" and 0 or tonumber(v.price))
			nm:writeU32(v.volume == "" and 0 or tonumber(v.volume))
			nm:writeU32(v.average == "" and 0 or tonumber(v.average))
			print(JSON:encode(v))
		end

		nm:writeU32(#MechanismList)
		for _,v in ipairs(MechanismList) do
			nm:writeU32(tonumber(v.id))
			nm:writeString(v.value)
			print(JSON:encode(v))
		end

		nm:writeU32(#CompeteProductList)
		for _,v in ipairs(CompeteProductList) do
			nm:writeU32(tonumber(v.id))
			nm:writeString(v.name)
			print(JSON:encode(v))
		end
		local info = Ref.SubMain.SubContent.inpInfo.text
		nm:writeString(info or "")
		NW.send(nm)
		_G.UI.Waiting.show()		
	end
end

local function on_submain_subcontent_subtip_subbtn_btnsubmit_click(btn)
	libunity.SetActive(Ref.SubMain.SubContent.SubTip.root, false)
	if NW.connected() then
		local storeId = UI_DATA.WNDSubmitSchedule.storeId
		local projectId = UI_DATA.WNDSubmitSchedule.projectId
		local nm = NW.msg("REPORTED.CS.REPORTEDPRO")
		nm:writeU32(projectId)
		nm:writeU32(storeId)
		local ProductList = UI_DATA.WNDSubmitSchedule.ProductList
		local CompeteProductList =UI_DATA.WNDSubmitSchedule.CompeteProductList
		local MechanismList = UI_DATA.WNDSubmitSchedule.MechanismList

		if ProductList == nil then ProductList = {} end
		if MechanismList == nil then MechanismList = {} end
		if CompeteProductList == nil then CompeteProductList = {} end

		nm:writeU32(#ProductList)
		for _,v in ipairs(ProductList) do
			nm:writeU32(v.id)
			nm:writeU32(tonumber(v.price))
			nm:writeU32(tonumber(v.volume))
			nm:writeU32(tonumber(v.average))
		end

		nm:writeU32(#MechanismList)
		for _,v in ipairs(MechanismList) do
			nm:writeU32(tonumber(v.id))
			nm:writeString(v.value)
		end

		nm:writeU32(#CompeteProductList)
		for _,v in ipairs(CompeteProductList) do
			nm:writeU32(tonumber(v.id))
			nm:writeString(v.name)
		end
		local info = Ref.SubMain.SubContent.inpInfo.text
		nm:writeString(info or "")
		NW.send(nm)
		-- local ImageList = {}
		-- on_submit_image(ImageList, nm)
		
	end
	-- UIMGR.close_window(Ref.root)
end

local function on_submain_subcontent_subtip_subbtn_btnattendance_click(btn)
	libunity.SetActive(Ref.SubMain.SubContent.SubTip.root, false)
	UIMGR.create_window("UI/WNDMainAttendance")
end

local function on_submain_subcontent_subtip_subbtn_btnclose_click(btn)
	libunity.SetActive(Ref.SubMain.SubContent.SubTip.root, false)
end

local function on_subtop_btnback_click(btn)
	UIMGR.close_window(Ref.root)
end

local function on_store_init()
	local storeId = UI_DATA.WNDSubmitSchedule.storeId
	local projectId = UI_DATA.WNDSubmitSchedule.projectId
	Project = DY_DATA.ProjectList[projectId]
	print(projectId,Project)
	if Project.StoreList == nil then print("StoreList 为空"..projectId) return end
	local StoreList = Project.StoreList
	Store = nil
	for i,v in ipairs(StoreList) do
		if v.id == storeId then
			Store = v
		end
	end
	if Store == nil then print("Store 为空"..storeId) return end

	Ref.SubMain.SubContent.SubAddress.lbText.text = Store.Info.address
	local WorkDays = Store.Info.WorkDays
	if WorkDays == nil then return end
	Ref.SubMain.SubContent.SubWorkDay.Grp:dup( #WorkDays, function (i, Ent, isNew)
		local WorkDay = WorkDays[i]
		Ent.lbDay.text = WorkDay.day
		Ent.lbWeek.text = TEXT.Week[WorkDay.week]
		Ent.lbTime.text = WorkDay.time
	end)
end

local function on_project_init()
	local projectId = UI_DATA.WNDSubmitSchedule.projectId
	Project = DY_DATA.ProjectList[projectId]
	if Project == nil or Project.Info == nil then return end
	
	Ref.SubMain.SubContent.lbTip.text = Project.Info.type
end

local function init_view()
	Ref.SubMain.SubContent.SubInfoList.btnProduct.onAction = on_submain_subcontent_subinfolist_btnproduct_click
	Ref.SubMain.SubContent.SubInfoList.btnSetCompeteProduct.onAction = on_submain_subcontent_subinfolist_btnsetcompeteproduct_click
	Ref.SubMain.SubContent.SubInfoList.btnPromoteInfo.onAction = on_submain_subcontent_subinfolist_btnpromoteinfo_click
	Ref.SubMain.SubContent.btnButton.onAction = on_submain_subcontent_btnbutton_click
	Ref.SubMain.SubContent.SubTip.SubBtn.btnSubmit.onAction = on_submain_subcontent_subtip_subbtn_btnsubmit_click
	Ref.SubMain.SubContent.SubTip.SubBtn.btnAttendance.onAction = on_submain_subcontent_subtip_subbtn_btnattendance_click
	Ref.SubMain.SubContent.SubTip.SubBtn.btnClose.onAction = on_submain_subcontent_subtip_subbtn_btnclose_click
	Ref.SubTop.btnBack.onAction = on_subtop_btnback_click
	UIMGR.make_group(Ref.SubMain.SubContent.SubWorkDay.Grp)
	--!*以上：自动注册的回调函数*--
end

local function init_logic()
	NW.subscribe("PROJECT.SC.GETPROINFOR", on_project_init)
	NW.subscribe("PROJECT.SC.GETSTOREINFOR",on_store_init)

	libunity.SetActive(Ref.SubMain.SubContent.SubTip.root, false)
	local projectId = UI_DATA.WNDSubmitSchedule.projectId
	Project = DY_DATA.ProjectList[projectId]
	if Project == nil then print("StoreList 为空"..projectId) return end
	UIMGR.get_photo(Ref.SubMain.SubContent.spIcon, Project.icon)
	Ref.SubMain.SubContent.lbName.text = Project.name
	if Project.Info == nil then
		local nm = NW.msg("PROJECT.CS.GETPROINFOR")
		nm:writeU32(projectId)
		NW.send(nm)
	else
		on_project_init()
	end

	local storeId = UI_DATA.WNDSubmitSchedule.storeId
	if Project.StoreList == nil then print("StoreList 为空"..projectId) return end
	local StoreList = Project.StoreList
	Store = nil
	for i,v in ipairs(StoreList) do
		if v.id == storeId then
			Store = v
		end
	end
	if Store == nil then print("Store 为空"..storeId) return end
	Ref.SubMain.SubContent.SubStore.lbText.text = Store.name

	if Store.Info == nil then
		local nm = NW.msg("PROJECT.CS.GETSTOREINFOR")
		nm:writeU32(projectId)
		nm:writeU32(storeId)
		NW.send(nm)
	else
		on_store_init()	
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

	NW.unsubscribe("PROJECT.SC.GETPROINFOR", on_project_init)
	NW.unsubscribe("PROJECT.SC.GETSTOREINFOR",on_store_init)
end

local P = {
	start = start,
	update_view = update_view,
	on_recycle = on_recycle,
}
return P

