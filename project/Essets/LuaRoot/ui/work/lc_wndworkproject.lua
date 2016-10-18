--
-- @file    ui/work/lc_wndworkproject.lua
-- @authors ckxz
-- @date    2016-08-02 11:05:45
-- @desc    WNDWorkProject
--

local ipairs, pairs
    = ipairs, pairs
local libugui = require "libugui.cs"
local libunity = require "libunity.cs"
local UIMGR = MERequire "ui/uimgr"
local DY_DATA = MERequire "datamgr/dydata.lua"
local UI_DATA = MERequire "datamgr/uidata.lua"
local NW = MERequire "network/networkmgr"
local TEXT = _G.ENV.TEXT
local Ref

local Project, Store
--!*以下：自动生成的回调函数*--

local function on_subtop_btnback_click(btn)
	UIMGR.close_window(Ref.root)
end

local function on_subtop_btnsurpervisor_click(btn)
	UI_DATA.WNDUserSupervisor.superId = Store.Info.superId
	UIMGR.create_window("UI/WNDUserSupervisor")
end

local function on_product_init()
	local Ref_SubMain_SubContent = Ref.SubMain.SubContent

	local ProductList = Project.ProductList
	if ProductList == nil then return end
	local strProduct = ""
	for k,v in pairs(ProductList) do
		strProduct = strProduct..v.name.."\n"
	end
	Ref_SubMain_SubContent.SubProduct.lbText.text = strProduct
end

local function on_store_init()
	local Ref_SubMain_SubContent = Ref.SubMain.SubContent

	local storeId = UI_DATA.WNDWorkProject.storeId

	if Project.StoreList == nil then print("StoreList 为空"..projectId) return end
	local StoreList = Project.StoreList
	Store = nil
	for i,v in ipairs(StoreList) do
		if v.id == storeId then
			Store = v
		end
	end
	if Store == nil then print("Store 为空"..storeId) return end

	if Store.Info == nil then return end

	Ref_SubMain_SubContent.SubAddress.lbText.text = Store.Info.address
	local WorkDays = Store.Info.WorkDays
	if WorkDays == nil then return end
	Ref_SubMain_SubContent.SubWorkDay.Grp:dup( #WorkDays, function (i, Ent, isNew)
		local WorkDay = WorkDays[i]
		Ent.lbDay.text = WorkDay.day
		Ent.lbWeek.text = TEXT.Week[WorkDay.week]
		Ent.lbTime.text = WorkDay.time
	end)
end

local function on_project_init()
	local Ref_SubMain_SubContent = Ref.SubMain.SubContent
	local projectId = UI_DATA.WNDWorkProject.projectId
	Project = DY_DATA.ProjectList[projectId]
	if Project.Info == nil then return end

	Ref_SubMain_SubContent.lbTip.text = Project.Info.type
	Ref_SubMain_SubContent.SubWords.lbText.text = Project.Info.words
	Ref_SubMain_SubContent.SubTitle.lbText.text = Project.Info.title
	Ref_SubMain_SubContent.SubWay.lbText.text = Project.Info.method
end

local function init_view()
	Ref.SubTop.btnBack.onAction = on_subtop_btnback_click
	Ref.SubTop.btnSurpervisor.onAction = on_subtop_btnsurpervisor_click
	UIMGR.make_group(Ref.SubMain.SubContent.SubWorkDay.Grp)
	--!*以上：自动注册的回调函数*--
end

local function init_logic()
	NW.subscribe("PROJECT.SC.GETPROINFOR", on_project_init)
	NW.subscribe("PROJECT.SC.GETSTOREINFOR",on_store_init)
	NW.subscribe("WORK.SC.GETPRODUCT", on_product_init)
	
	local projectId = UI_DATA.WNDWorkProject.projectId
	Project = DY_DATA.ProjectList[projectId]
	if Project == nil then print("Project 为空"..projectId) return end
	
	local Ref_SubMain_SubContent = Ref.SubMain.SubContent
	Ref_SubMain_SubContent.lbName.text = Project.name
	UIMGR.get_photo(Ref_SubMain_SubContent.spIcon, Project.icon)
	
	if Project.Info == nil then
		local nm = NW.msg("PROJECT.CS.GETPROINFOR")
		nm:writeU32(projectId)
		NW.send(nm)
	else
		on_project_init()
	end

	local storeId = UI_DATA.WNDWorkProject.storeId
	if Project.StoreList == nil then print("StoreList 为空"..projectId) return end
	local StoreList = Project.StoreList
	Store = nil
	for i,v in ipairs(StoreList) do
		if v.id == storeId then
			Store = v
		end
	end
	if Store == nil then print("Store 为空"..storeId) return end

	if Store == nil then print("Store 为空"..storeId) return end
	Ref_SubMain_SubContent.SubStore.lbText.text = Store.name

	if Store.Info == nil then
		local nm = NW.msg("PROJECT.CS.GETSTOREINFOR")
		nm:writeU32(projectId)
		nm:writeU32(storeId)
		NW.send(nm)
	else
		on_store_init()	
	end

	if Project.ProductList == nil then
		local nm = NW.msg("WORK.CS.GETPRODUCT")
		nm:writeU32(projectId)
		NW.send(nm)
	else
		on_product_init()
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
	NW.unsubscribe("WORK.SC.GETPRODUCT", on_product_init)
end

local P = {
	start = start,
	update_view = update_view,
	on_recycle = on_recycle,
}
return P

