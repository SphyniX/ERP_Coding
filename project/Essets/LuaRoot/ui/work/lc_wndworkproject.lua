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

	Ref_SubMain_SubContent.SubStore.lbAddress.text = Store.Info.address
	Ref_SubMain_SubContent.SubSeven.lbText.text = string.format(TEXT.fmtContext1,Store.Info.supcontact,Store.Info.salecontact)
	local WorkDays = Store.Info.WorkDays
	if WorkDays == nil then return end
	Ref_SubMain_SubContent.SubWorkDay.Grp:dup( #WorkDays, function (i, Ent, isNew)
		local WorkDay = WorkDays[i]
		-- Ent.lbDay.text = WorkDay.day
		Ent.lbWeek.text = TEXT.Week[WorkDay.week]
		Ent.lbTime.text = WorkDay.day.." "..WorkDay.time

	end)
end

local function on_project_init()
	local Ref_SubMain_SubContent = Ref.SubMain.SubContent
	local projectId = UI_DATA.WNDWorkProject.projectId
	Project = DY_DATA.ProjectList[projectId]
	if Project.Info == nil then return end
	local Info = Project.Info
	Ref_SubMain_SubContent.lbTip.text = Project.Info.type
	local product = ""
	for k,v in pairs(Info.ProductList) do
		product = product.."、"..v
	end
	Ref_SubMain_SubContent.SubOne.lbText.text = string.format(TEXT.fmtInfo, Info.brand, product) -- 品牌， 产品 /projectinfo
	Ref_SubMain_SubContent.SubTwo.lbText.text = string.format(TEXT.fmtType, Info.act_form, Info.act_calendar, Info.act_goal, Info.goal_sale, Info.goal_expvolume, Info.goal_exppeople) -- 活动形式， 
	Ref_SubMain_SubContent.SubThree.lbText.text = string.format(TEXT.fmtProduct, Info.product_info, Info.selling_point) 
	Ref_SubMain_SubContent.SubFour.lbText.text = string.format(TEXT.fmtWord, Info.words, Info.sales_technique)
	Ref_SubMain_SubContent.SubFive.lbText.text = string.format(TEXT.fmtWork, Info.rule_att, Info.rule_face, Info.rule_sch, Info.rule_photo, Info.rule_data, Info.rule_leave, Info.rule_sale, Info.rule_plan)
	Ref_SubMain_SubContent.SubSix.lbText.text = string.format(TEXT.fmtRole, Info.info_wages, Info.info_reward)
	Ref_SubMain_SubContent.SubSeven.lbText1.text = string.format(TEXT.fmtContext2, Info.info_procontact)

end

-- local function on_work_init()

-- 	local Attendance = DY_DATA.AttendanceList[0]
-- 	for i=1,DY_DATA.AttendanceList.length do
-- 		if DY_DATA.AttendanceList[i].name = UI_DATA.WNDWorkProject.ProjcetName then
-- 			Ref.SubContent.SubWorkDay
-- 	end



-- 	Ref.SubMain.SubContent.SubWorkDay.lbText.text = DY_DATA.AttendanceList
-- end

local function init_view()
	Ref.SubTop.btnBack.onAction = on_subtop_btnback_click
	UIMGR.make_group(Ref.SubMain.SubContent.SubWorkDay.Grp)
	--!*以上：自动注册的回调函数*--
end

local function init_logic()
	NW.subscribe("PROJECT.SC.GETPROINFOR", on_project_init)
	NW.subscribe("PROJECT.SC.GETSTOREINFOR",on_store_init)
	-- NW.subscribe("ATTENCE.SC.GETWORK",on_work_init)
	
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
	Ref_SubMain_SubContent.SubStore.lbName.text = Store.name

	if Store.Info == nil then
		local nm = NW.msg("PROJECT.CS.GETSTOREINFOR")
		nm:writeU32(projectId)
		nm:writeU32(storeId)
		NW.send(nm)
	else
		on_store_init()	
	end

	-- local AttendanceList = DY_DATA.AttendanceList
	-- if AttendanceList == nil then
	-- 	local nm = NW.msg("ATTENCE.SC.GETWORK")
	-- 	nm:writeU32(DY_DATA.User.id)
	-- 	NW.send(nm)
	-- else
	-- 	on_work_init()
	-- end

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


