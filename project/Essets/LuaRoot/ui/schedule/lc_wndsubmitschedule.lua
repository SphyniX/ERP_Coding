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
	--UI_DATA.WNDSubmitSchedule.WNDSetComProductNWStata=false
	UIMGR.create_window("UI/WNDSetComProduct")
end

local function on_submain_subcontent_subinfolist_btnsupplies_click(btn)
	--UI_DATA.WNDSubmitSchedule.WNDSetSuppliesNWStata=false
	libunity.SetActive(Ref.SubMain.SubSupplies.root, true)
end

local function on_submain_subcontent_subinfolist_btninfo_click(btn)
	--UI_DATA.WNDSubmitSchedule.WNDSetInforNWStata=false
	UIMGR.create_window("UI/WNDSetInfor")
end

local function on_submain_subcontent_btnbutton_click(btn)
	print("  wndSubmitScheduleDataInit1-------on_submain_subcontent_btnbutton_click---------"..JSON:encode(DY_DATA.WNDSubmitScheduleData))
	---上传数据准备-----
	local WNDSubmitPhotoForReportNWStata = UI_DATA.WNDSubmitSchedule.WNDSubmitPhotoForReportNWStata
	local WNDSetPromoteInfoNWStata = UI_DATA.WNDSubmitSchedule.WNDSetPromoteInfoNWStata
	local WNDSetForetasteNWStata = UI_DATA.WNDSubmitSchedule.WNDSetForetasteNWStata
	local WNDSetGiftNWStata = UI_DATA.WNDSubmitSchedule.WNDSetGiftNWStata
	local WNDSetComProductNWStata = UI_DATA.WNDSubmitSchedule.WNDSetComProductNWStata
	local WNDSetSuppliesNWStata = UI_DATA.WNDSubmitSchedule.WNDSetSuppliesNWStata
	local WNDSetInforNWStata = UI_DATA.WNDSubmitSchedule.WNDSetInforNWStata

	local TempProductList = UI_DATA.WNDSubmitSchedule.ProductList
	local TempProductListInfo = UI_DATA.WNDSubmitSchedule.ProductListInfo
	local TempProductListForetaste = UI_DATA.WNDSubmitSchedule.ProductListForetaste
	local TempPhotoListForUpDate = UI_DATA.WNDSubmitSchedule.PhotoListForUpdate
	local TempProductListGift = UI_DATA.WNDSubmitSchedule.ProductListGift
	local TempComList = UI_DATA.WNDSubmitSchedule.ComList
	local TempMaterList = DY_DATA.WNDSubmitSchedule.MaterList
	local TempInfor = DY_DATA.WNDSubmitSchedule.Infor
	if TempProductList ~= nil then
		print("----------------------------------------"..11)
		if TempProductListInfo ~= nil or WNDSetPromoteInfoNWStata then
			print("----------------------------------------"..12)
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

	if TempProductListForetaste ~= nil or WNDSetForetasteNWStata then
		print("WNDSubmitSchedule.ProductListForetaste is :" .. JSON:encode(TempProductListForetaste))
	else
		_G.UI.Toast:make(nil,"体验品数据不能全为空"):show()
		return
	end

	if TempPhotoListForUpDate ~= nil or WNDSubmitPhotoForReportNWStata then 
		print("WNDSubmitSchedule.PhotoListForUpdate is :" .. JSON:encode(TempPhotoListForUpDate))
	else
		_G.UI.Toast:make(nil,"请上传必传图片"):show()
		return
	end

	if TempProductListGift ~= nil or WNDSetGiftNWStata then
		print("WNDSubmitSchedule.ProductListGift is :" .. JSON:encode(TempProductListGift))
	else
		_G.UI.Toast:make(nil,"赠品数据不能全为空"):show()
		return
	end
	if TempComList ~= nil or WNDSetComProductNWStata then
		print("WNDSubmitSchedule.ComList is :" .. JSON:encode(TempComList))
	else
		_G.UI.Toast:make(nil,"竞品数据不能全为空"):show()
		return
	end
	if TempMaterList ~= nil or WNDSetSuppliesNWStata then 
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
		print("ProductListWithProductAndInfo-------------"..JSON:encode(ProductListWithProductAndInfo))
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

		local projectId = UI_DATA.WNDSubmitSchedule.projectId
		local nm = NW.msg("WORK.CS.GETSTORE")
		nm:writeU32(projectId)
		nm:writeU32(DY_DATA.User.id)
		NW.send(nm)
		-- local projectId = UI_DATA.WNDSubmitSchedule.projectId
		-- local storeId = UI_DATA.WNDSubmitSchedule.storeId
		-- local nm = NW.msg("REPORTED.CS.GETSTOREINFOR")
		-- nm:writeU32(projectId)
		-- nm:writeU32(storeId)
		-- nm:writeU32(DY_DATA.User.id)
		-- NW.send(nm)
		-- _G.UI.Waiting.show()
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
	--UI_DATA.WNDSubmitSchedule.WNDSubmitPhotoForReportNWStata = false
	libunity.SetActive(Ref.SubMain.SubProduct.root, false)
	UIMGR.create_window("UI/WNDSubmitPhotoForReport")
end

-- 促销机制
local function on_submain_subproduct_btn3_click(btn)
	UI_DATA.WNDSubmitSchedule.WNDSetPromoteInfoNWStata=false
	libunity.SetActive(Ref.SubMain.SubProduct.root, false)
	UIMGR.create_window("UI/WNDSetPromoteInfo")
end

-- 体验品
local function on_submain_subproduct_btn4_click(btn)
	--UI_DATA.WNDSubmitSchedule.ProductListForetasteNWStata=false
	libunity.SetActive(Ref.SubMain.SubProduct.root, false)
	UIMGR.create_window("UI/WNDSetForetaste")
end

-- 赠品
local function on_submain_subproduct_btn5_click(btn)
	--UI_DATA.WNDSubmitSchedule.ProductListGiftNWStata=false
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
local function wndSubmitScheduleDataInit()
	
	-- UI_DATA.WNDSubmitSchedule.storeId = DY_DATA.WNDSubmitScheduleData.storeId
	-- UI_DATA.WNDSubmitSchedule.projectId =  UI_DATA.WNDSubmitSchedule.projectId
	print("  wndSubmitScheduleDataInit----------------"..tostring(UI_DATA.WNDSubmitSchedule.DataInitStata))
	
	if UI_DATA.WNDSubmitSchedule.DataInitStata then       -------UI_DATA.WNDSubmitSchedule.DataInitStata控制进度界面初始化第一次，在WNDSelectSchStore界面初始化此变量
		return
	end

	local ProductList = DY_DATA.WNDSubmitScheduleData.ProductList
	print("  wndSubmitScheduleDataInit1----------------"..JSON:encode(DY_DATA.WNDSubmitScheduleData))
	print("  wndSubmitScheduleDataInit2----------------"..JSON:encode(ProductList))
	if ProductList ~=nil and next(ProductList) ~= nil then
		UI_DATA.WNDSubmitSchedule.ProductList = {}
		if ProductList ~= nil then
			local tempProductLis = {}
			local tempProductListInfo = {}
			for i = 1,# ProductList do
				local ProductListUpdate = {}
				local ProductListInfoUpdate = {}
				ProductListUpdate.name = tostring(ProductList[i].name)
				ProductListUpdate.price = ProductList[i].price
				ProductListUpdate.volume = ProductList[i].volume
				ProductListUpdate.sale = ProductListUpdate.price * ProductListUpdate.volume
				ProductListInfoUpdate.value = ProductList[i].value
				table.insert(tempProductLis,ProductListUpdate)
				table.insert(tempProductListInfo,ProductListInfoUpdate)

			end
			UI_DATA.WNDSubmitSchedule.ProductList = tempProductLis
			UI_DATA.WNDSubmitSchedule.ProductListInfo = tempProductListInfo
		end
		print("UI_DATA.WNDSubmitSchedule.ProductListInfo-----"..JSON:encode(UI_DATA.WNDSubmitSchedule.ProductListInfo))
	end

	
	local ComList = DY_DATA.WNDSubmitScheduleData.ComList
	if ComList ~=nil and next(ComList)  ~=nil then
		UI_DATA.WNDSubmitSchedule.ComList = {}
		print("DY_DATA.WNDSubmitScheduleData.ComList-----"..tostring(JSON:encode(DY_DATA.WNDSubmitScheduleData.ComList)))
		local ComListUpdate = {}
		if ComList ~= nil and next(ComList) then 
		    for i = 1,#ComList do
		    	local com = ComList[i]

		        local id = com.id
		        local Price = com.Price
		        local name = com.name
		        local icon = com.icon
		        
		        table.insert(ComListUpdate, {id = id, Price = Price, name = name, icon = icon})
		    end
		end
   		UI_DATA.WNDSubmitSchedule.ComList = ComListUpdate
	end


	local MaterList = DY_DATA.WNDSubmitScheduleData.MaterList
	if MaterList ~=nil and next(ComList) ~=nil then
		UI_DATA.WNDSubmitSchedule.MaterList = {}		
		print("DY_DATA.WNDSubmitScheduleData.ComList-----"..tostring(JSON:encode(DY_DATA.WNDSubmitScheduleData.MaterList)))
		local MaterListtUpdate = {}
		if MaterList ~= nil and next(MaterList) then 
		    for i = 1,#MaterList do
		    	local Mater = MaterList[i]

		        local id = Mater.id
		        local state = Mater.state
		        local discribe = Mater.discribe
		        local photo = Mater.photo
		        table.insert(MaterListtUpdate, {id = id, state = state, discribe = discribe, photo = photo})
		    end
		end

		UI_DATA.WNDSubmitSchedule.MaterList = MaterListtUpdate
	end


	UI_DATA.WNDSubmitSchedule.DataInitStata = true
    -- local SellPhoto = Project.SellPhoto

    -- for i=1, n do
    --     local id = tonumber(nm:readString())
    --     local name = nm:readString()
    --     local state = tonumber(nm:readString())
    --     table.insert(SellPhoto, {id = id, name = name,state = state})
    -- end

    -- Project.SellPhoto = SellPhoto


-- UI_DATA.WNDSubmitSchedule.ProductListInfo

-- 	--UI_DATA.WNDSubmitSchedule.ProductList
-- 	--UI_DATA.WNDSubmitSchedule.ProductListInfo
-- 	--UI_DATA.WNDSubmitSchedule.ProductListForetaste = 
-- 	--UI_DATA.WNDSubmitSchedule.PhotoListForUpdate = DY_DATA.WNDSubmitSchedule.schedule
-- 	--UI_DATA.WNDSubmitSchedule.ProductListGift =
-- 	--UI_DATA.WNDSubmitSchedule.ComList = DY_DATA.WNDSubmitSchedule.ComList
end

local function on_store_init()
	print("on_store_init-----------------")
	local storeId = UI_DATA.WNDSubmitSchedule.storeId
	local projectId = UI_DATA.WNDSubmitSchedule.projectId
	print("UI_DATA.WNDSubmitSchedule.ProductList--------"..JSON:encode(UI_DATA.WNDSubmitSchedule))
	-- local type = UI_DATA.WNDSubmitSchedule.type
	Project = {}
	Project = DY_DATA.SchProjectList[projectId]
	print("UI_DATA.WNDSubmitSchedule.ProductList----Project----"..JSON:encode(Project))
	print("UI_DATA.WNDSubmitSchedule.ProductList----Project.StoreList----"..JSON:encode(Project.StoreList))
	if Project.StoreList == nil then print("StoreList 为空"..projectId) return end
	local StoreList = Project.StoreList
	Store = nil
	for i,v in ipairs(StoreList) do
		if v.id == storeId then
			Store = v
		end
	end

	if Store == nil or next(Store) == nil then print("Store 为空"..storeId) return end
	print("UI_DATA.WNDSubmitSchedule.ProductList----Project.StoreList----"..JSON:encode(Store))
	local state = nil
	local InfoList = DY_DATA.SchProjectList.InfoList
	print("DY_DATA.SchProjectList.InfoList------"..JSON:encode(InfoList))
	for i,v in ipairs(InfoList) do
		if v.id == storeId then
			state = v.state
		end
	end
	print("state -------------------111"..tostring(state))
	--local state = Store.state        --UI_DATA.WNDSubmitSchedule.state
	--------- 提取已上报进度在这里 ---------
	if state == 2 then
		Ref.SubMain.SubContent.lbTip.text = "<color=red>已上报进度</color>"

	else
		Ref.SubMain.SubContent.lbTip.text = "请确认无误后提交"
	end
	-- Ref.SubMain.SubContent.lbClass.text = type
	if Store.Info ~= nil then
		Ref.SubMain.SubContent.SubAddress.lbText.text = Store.Info.address
		Ref.SubMain.SubContent.SubTime.lbStart.text = Store.Info.starttime
		Ref.SubMain.SubContent.SubTime.lbEnd.text = Store.Info.endtime
		wndSubmitScheduleDataInit()
	end
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
	NW.subscribe("WORK.SC.GETSTORE", on_store_init)
	NW.subscribe("REPORTED.SC.GETSTOREINFOR",on_store_init)
	NW.subscribe("REPORTED.SC.GETPERSONALREP",on_store_init)
	-- NW.subscribe("ATTENCE.SC.GETWORK",on_work_init)
	libunity.SetActive(Ref.SubMain.SubSupplies.root, false)
	libunity.SetActive(Ref.SubMain.SubProduct.root, false)
	local projectId = UI_DATA.WNDSubmitSchedule.projectId
	Project = DY_DATA.SchProjectList[projectId]
	if Project == nil then print("StoreList 为空"..projectId) return end
	UIMGR.get_photo(Ref.SubMain.SubContent.spIcon, Project.icon)
	Ref.SubMain.SubContent.lbName.text = Project.name
	Ref.SubMain.SubContent.lbClass.text = Project.type

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

	local nm = NW.msg("REPORTED.CS.GETSTOREINFOR")
	nm:writeU32(projectId)
	nm:writeU32(storeId)
	nm:writeU32(DY_DATA.User.id)
	NW.send(nm)


	local WNDSubmitScheduleData = DY_DATA.WNDSubmitScheduleData
	print("WNDSubmitScheduleData--------"..JSON:encode(WNDSubmitScheduleData))
	if WNDSubmitScheduleData ==nil or next(WNDSubmitScheduleData)==nil then
		print("发送请求----REPORTED.CS.GETPERSONALREP")
		local nm = NW.msg("REPORTED.CS.GETPERSONALREP")
		nm:writeU32(DY_DATA.User.id)
		nm:writeU32(storeId)
		NW.send(nm)
	end



	-- else
	-- 	on_store_init()	
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
	NW.unsubscribe("WORK.SC.GETSTORE", on_store_init)
	NW.unsubscribe("REPORTED.SC.GETSTOREINFOR",on_store_init)
	NW.unsubscribe("REPORTED.SC.GETPERSONALREP",on_store_init)
end

local P = {
	start = start,
	update_view = update_view,
	on_recycle = on_recycle,
}
return P

