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
local ProductListWithProductAndInfo
local Project, Store

--!*以下：自动生成的回调函数*--

local function on_submain_subcontent_subinfolist_btnproduct_click(btn)
	libunity.SetActive(Ref.SubMain.SubProduct.root, true)
end

local function on_submain_subcontent_subinfolist_btncompeteproduct_click(btn)
	UIMGR.create_window("UI/WNDSetComProduct")
end

local function on_submain_subcontent_subinfolist_btnsupplies_click(btn)
	
	libunity.SetActive(Ref.SubMain.SubSupplies.root, true)
end

local function on_submain_subcontent_subinfolist_btninfo_click(btn)
	UIMGR.create_window("UI/WNDSetInfor")
end

local function on_submain_subcontent_btnbutton_click(btn)
	---上传数据准备-----
	local TempProductList = UI_DATA.WNDSubmitSchedule.ProductList
	local TempProductListInfo = UI_DATA.WNDSubmitSchedule.ProductListInfo
	local TempProductListForetaste = UI_DATA.WNDSubmitSchedule.ProductListForetaste
	local TempPhotoListForUpDate = UI_DATA.WNDSubmitSchedule.PhotoListForUpdate
	local TempProductListGift = UI_DATA.WNDSubmitSchedule.ProductListGift
	local TempComList = UI_DATA.WNDSubmitSchedule.ComList
	local TempMaterList = DY_DATA.WNDSubmitSchedule.MaterList
	local TempInfor = DY_DATA.WNDSubmitSchedule.Infor
	if TempProductList ~= nil then
		if TempProductListInfo ~= nil then
			ProductListWithProductAndInfo = {}
			for i=1,#TempProductList do
				local id = TempProductList[i].id
				local price = TempProductList[i].price
				local volume = TempProductList[i].volume
				local value = TempProductListInfo[i].value
				table.insert(ProductListWithProductAndInfo,{id = id , price = price , volume = volume , value = value})
			end
		else
			_G.UI.Toast:make(nil, "促销机制不能全为空"):show()
			return 
		end
	else
		_G.UI.Toast:make(nil, "产品数据不能全为空"):show()
		return 
	end
	if ProductListWithProductAndInfo ~= nil then
		print("WNDSubmitSchedule.ProductListWithProductAndInfo is :" .. JSON:encode(ProductListWithProductAndInfo))
	end

	if TempProductListForetaste ~= nil then
		print("WNDSubmitSchedule.ProductListForetaste is :" .. JSON:encode(TempProductListForetaste))
	else
		_G.UI.Toast:make(nil,"体验品数据不能全为空"):show()
		return
	end

	if TempPhotoListForUpDate ~= nil then 
		print("WNDSubmitSchedule.PhotoListForUpdate is :" .. JSON:encode(TempPhotoListForUpDate))
	else
		_G.UI.Toast:make(nil,"请上传必传图片"):show()
		return
	end

	if TempProductListGift ~= nil then
		print("WNDSubmitSchedule.ProductListGift is :" .. JSON:encode(TempProductListGift))
	else
		_G.UI.Toast:make(nil,"赠品数据不能全为空"):show()
		return
	end
	if TempComList ~= nil then
		print("WNDSubmitSchedule.ComList is :" .. JSON:encode(TempComList))
	else
		_G.UI.Toast:make(nil,"竞品数据不能全为空"):show()
		return
	end
	if TempMaterList ~= nil then 
		print("WNDSubmitSchedule.MaterList is :" .. JSON:encode(TempMaterList))
	else
		_G.UI.Toast:make(nil,"物料数据不能全为空"):show()
		return
	end
	if TempInfor == nil then TempInfor = "" else print("WNDSubmitSchedule.TempInfor is :" .. TempInfor) end 
	----上传数据------
	if NW.connected() then
		local storeId = UI_DATA.WNDSubmitSchedule.storeId
		local projectId = UI_DATA.WNDSubmitSchedule.projectId
		local nm = NW.msg("REPORTED.CS.REPORTEDPRO")
		nm:writeU32(DY_DATA.User.id)
		nm:writeU32(storeId)


		nm:writeU32(#ProductListWithProductAndInfo)
		for _,v in ipairs(ProductListWithProductAndInfo) do
			nm:writeU32(v.id == "" and 0 or tonumber(v.id))
			nm:writeU32(v.price == "" and 0 or tonumber(v.price))
			nm:writeU32(v.volume == "" and 0 or tonumber(v.volume))
			nm:writeString(v.value)
			-- print(JSON:encode(v))
		end

		nm:writeU32(#TempMaterList)
		for _,v in ipairs(TempMaterList) do
			nm:writeU32(v.id == "" and 0 or tonumber(v.id))
			nm:writeString(v.state)
			nm:writeString(v.discribe)
			nm:writeString(v.photo)
			-- print(JSON:encode(v))
		end

		nm:writeU32(#TempComList)
		for _,v in ipairs(TempComList) do
			nm:writeU32(v.id == "" and 0 or tonumber(v.id))
			nm:writeString(v.price)
			nm:writeString(v.info)
			nm:writeString(v.name)
			-- print(JSON:encode(v))
		end

		nm:writeU32(#TempPhotoListForUpDate)
		for _,v in ipairs(TempPhotoListForUpDate) do
			nm:writeU32(v.id == "" and 0 or tonumber(v.id))
			nm:writeString(v.photo)
			-- print(JSON:encode(v))
		end
		nm:writeU32(#TempProductListGift)
		for _,v in ipairs(TempProductListGift) do
			nm:writeU32(v.id == "" and 0 or tonumber(v.id))
			nm:writeU32(v.volume == "" and 0 or tonumber(v.volume))
			-- print(JSON:encode(v))
		end

		nm:writeU32(#TempProductListForetaste)
		for _,v in ipairs(TempProductListForetaste) do
			nm:writeU32(v.id == "" and 0 or tonumber(v.id))
			nm:writeU32(v.number == "" and 0 or tonumber(v.number))
			nm:writeU32(v.value == "" and 0 or tonumber(v.value))
			-- print(JSON:encode(v))
		end

		
		nm:writeString(TempInfor or "")
		NW.send(nm)
		_G.UI.Waiting.show()
	end
end

-- 物料
local function on_submain_subsupplies_btn1_click(btn)
	
	libunity.SetActive(Ref.SubMain.SubSupplies.root, false)
	UIMGR.create_window("UI/WNDSetSupplies")
end

local function on_submain_subsupplies_btnback_click(btn)
	libunity.SetActive(Ref.SubMain.SubSupplies.root, false)

end

-- 销量
local function on_submain_subproduct_btn1_click(btn)
	libunity.SetActive(Ref.SubMain.SubProduct.root, false)
	UIMGR.create_window("UI/WNDSetPromoteProduct")
end

-- 照片
local function on_submain_subproduct_btn2_click(btn)
	libunity.SetActive(Ref.SubMain.SubProduct.root, false)
	UIMGR.create_window("UI/WNDSubmitPhotoForReport")
end

-- 促销机制
local function on_submain_subproduct_btn3_click(btn)
	
	libunity.SetActive(Ref.SubMain.SubProduct.root, false)
	UIMGR.create_window("UI/WNDSetPromoteInfo")
end

-- 体验品
local function on_submain_subproduct_btn4_click(btn)
	
	libunity.SetActive(Ref.SubMain.SubProduct.root, false)
	UIMGR.create_window("UI/WNDSetForetaste")
end

-- 赠品
local function on_submain_subproduct_btn5_click(btn)
	
	libunity.SetActive(Ref.SubMain.SubProduct.root, false)
	UIMGR.create_window("UI/WNDSetGift")
end

local function on_submain_subproduct_btnback_click(btn)
	
	libunity.SetActive(Ref.SubMain.SubProduct.root, false)
end

local function on_subtop_btnback_click(btn)
	UIMGR.close_window(Ref.root)
end

-- local function on_work_init()
-- 	local AttendanceList = DY_DATA.get_attendance_list(false)
-- 	if AttendanceList == nil then 
-- 		local nm = NW.msg("ATTENCE.SC.GETWORK")
-- 		nm:writeU32(DY_DATA.User.id)
-- 		NW.send(nm)
-- 		return
-- 	end
-- 	print(JSON:encode(AttendanceList))
-- 	for i=1,#AttendanceList do
-- 		if AttendanceList[i].name == Project.name then
-- 			Ref.SubMain.SubContent.SubTime.lbStart.text = AttendanceList[i].starttime
-- 			Ref.SubMain.SubContent.SubTime.lbEnd.text = AttendanceList[i].endtime
-- 		end
-- 	end
	

-- end


local function on_store_init()
	local storeId = UI_DATA.WNDSubmitSchedule.storeId
	local projectId = UI_DATA.WNDSubmitSchedule.projectId
	Project = DY_DATA.SchProjectList[projectId]
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
	Ref.SubMain.SubContent.SubTime.lbStart.text = Store.Info.starttime
	Ref.SubMain.SubContent.SubTime.lbEnd.text = Store.Info.endtime

end

local function init_view()
	Ref.SubMain.SubContent.SubInfoList.btnProduct.onAction = on_submain_subcontent_subinfolist_btnproduct_click
	Ref.SubMain.SubContent.SubInfoList.btnCompeteProduct.onAction = on_submain_subcontent_subinfolist_btncompeteproduct_click
	Ref.SubMain.SubContent.SubInfoList.btnSupplies.onAction = on_submain_subcontent_subinfolist_btnsupplies_click
	Ref.SubMain.SubContent.SubInfoList.btnInfo.onAction = on_submain_subcontent_subinfolist_btninfo_click
	Ref.SubMain.SubContent.btnButton.onAction = on_submain_subcontent_btnbutton_click
	Ref.SubMain.SubSupplies.btn1.onAction = on_submain_subsupplies_btn1_click
	Ref.SubMain.SubSupplies.btnBack.onAction = on_submain_subsupplies_btnback_click
	Ref.SubMain.SubProduct.btn1.onAction = on_submain_subproduct_btn1_click
	Ref.SubMain.SubProduct.btn2.onAction = on_submain_subproduct_btn2_click
	Ref.SubMain.SubProduct.btn3.onAction = on_submain_subproduct_btn3_click
	Ref.SubMain.SubProduct.btn4.onAction = on_submain_subproduct_btn4_click
	Ref.SubMain.SubProduct.btn5.onAction = on_submain_subproduct_btn5_click
	Ref.SubMain.SubProduct.btnBack.onAction = on_submain_subproduct_btnback_click
	Ref.SubTop.btnBack.onAction = on_subtop_btnback_click
	--!*以上：自动注册的回调函数*--
end

local function init_logic()
	NW.subscribe("REPORTED.SC.GETSTOREINFOR",on_store_init)
	-- NW.subscribe("ATTENCE.SC.GETWORK",on_work_init)
	libunity.SetActive(Ref.SubMain.SubSupplies.root, false)
	libunity.SetActive(Ref.SubMain.SubProduct.root, false)
	local projectId = UI_DATA.WNDSubmitSchedule.projectId
	Project = DY_DATA.SchProjectList[projectId]
	if Project == nil then print("StoreList 为空"..projectId) return end
	UIMGR.get_photo(Ref.SubMain.SubContent.spIcon, Project.icon)
	Ref.SubMain.SubContent.lbName.text = Project.name
	Ref.SubMain.SubContent.lbTip.text = Project.type

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
	
	-- if Store.Info == nil then
		local nm = NW.msg("REPORTED.CS.GETSTOREINFOR")
		nm:writeU32(projectId)
		nm:writeU32(storeId)
		nm:writeU32(DY_DATA.User.id)
		NW.send(nm)
	-- else
		-- on_store_init()	
	-- end


	-- if DY_DATA.AttendanceList == nil or next(DY_DATA.AttendanceList) == nil then
	-- 	print("AttendanceList is nil")
	-- 	if NW.connected() then
	-- 		local nm = NW.msg("ATTENCE.CS.GETWORK")
	-- 	    nm:writeU32(DY_DATA.User.id)
	-- 	    NW.send(nm)
	-- 	    return
	-- 	else
	-- 		DY_DATA.AttendanceList = {}
	-- 	end
	-- end
	
	
	-- on_work_init()
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
	NW.unsubscribe("REPORTED.SC.GETSTOREINFOR",on_store_init)
end

local P = {
	start = start,
	update_view = update_view,
	on_recycle = on_recycle,
}
return P

