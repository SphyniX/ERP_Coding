--
-- @file    ui/work/lc_wndsupworkselectpagemsg.lua
-- @authors cks
-- @date    2016-11-10 03:05:54
-- @desc    WNDSupWorkSelectPageMsg
--

local ipairs, pairs
    = ipairs, pairs
local libugui = require "libugui.cs"
local libunity = require "libunity.cs"
local UIMGR = MERequire "ui/uimgr"
local DY_DATA = MERequire "datamgr/dydata.lua"
local UI_DATA = MERequire "datamgr/uidata.lua"
local NW= MERequire "network/networkmgr"
local WorkTEXT = _G.ENV.WorkTEXT--TEXT
local Project = {}
local Ref

local Store
--!*以下：自动生成的回调函数*--

local function on_subtop_btnback_click(btn)
	UIMGR.create_window("UI/WNDSupWork")
end

local function on_projectflow_init()
	local Ref_SubMain_SubContent = Ref.SubMain.SubContent
	local projectId = UI_DATA.WNDWorkProject.projectId
	Project = DY_DATA.ProjectList[projectId]
	if Project.InfoFlow == nil then return end
	local InfoFlow = Project.InfoFlow
	Ref_SubMain_SubContent.SubFive.lbText.text = string.format(WorkTEXT.WorkProcess,InfoFlow.rule_1,InfoFlow.rule_2,InfoFlow.rule_3,InfoFlow.rule_4,InfoFlow.rule_5,InfoFlow.rule_6,InfoFlow.rule_7,InfoFlow.rule_8,InfoFlow.rule_9,InfoFlow.rule_10)	
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
	Ref_SubMain_SubContent.SubOne.lbText.text = string.format(WorkTEXT.fmtInfo, Info.brand, product) -- 品牌， 产品 /projectinfo
	Ref_SubMain_SubContent.SubTwo.lbText.text = string.format(WorkTEXT.fmtType, Info.act_form, Info.act_calendar, Info.act_goal, Info.goal_sale, Info.goal_expvolume, Info.goal_exppeople,"") -- 活动形式， 
	Ref_SubMain_SubContent.SubThree.lbText.text = string.format(WorkTEXT.fmtProduct, Info.product_info, Info.selling_point) 
	Ref_SubMain_SubContent.SubFour.lbText.text = string.format(WorkTEXT.fmtWord, Info.words, Info.sales_technique)
	-- Ref_SubMain_SubContent.SubFive.lbText.text = string.format(WorkTEXT.WorkProcess,"1","1","1","1","1","1","1","1","1","1")
	Ref_SubMain_SubContent.SubSix.lbText.text = string.format(WorkTEXT.fmtRole, Info.info_wages, Info.info_reward)
	Ref_SubMain_SubContent.SubSeven.lbText.text = string.format(WorkTEXT.fmtContext1,DY_DATA.User.phone)
	Ref_SubMain_SubContent.SubSeven.lbText1.text = string.format(WorkTEXT.fmtContext2, Info.info_procontact)

end
local function init_view()
	Ref.SubTop.btnBack.onAction = on_subtop_btnback_click
	--!*以上：自动注册的回调函数*--
end
local function defualUIData ()

	Ref.SubMain.SubContent.SubOne.lbText.text=WorkTEXT.fmtInfo
	Ref.SubMain.SubContent.SubTwo.lbText.text=WorkTEXT.fmtType
	Ref.SubMain.SubContent.SubThree.lbText.text=WorkTEXT.fmtProduct
	Ref.SubMain.SubContent.SubFour.lbText.text=WorkTEXT.fmtWord
	Ref.SubMain.SubContent.SubFive.lbText.text=WorkTEXT.WorkProcess
	Ref.SubMain.SubContent.SubSix.lbText.text=WorkTEXT.fmtRole
	Ref.SubMain.SubContent.SubSeven.lbText.text=WorkTEXT.fmtContext1
	-- body
end 




local function init_logic()
--defualUIData ()
	NW.subscribe("PROJECT.SC.GETPROINFOR", on_project_init)
	NW.subscribe("PROJECT.SC.GETSUPWORKFLOW", on_projectflow_init)
	-- NW.subscribe("ATTENCE.SC.GETWORK",on_work_init)
	
	local projectId = UI_DATA.WNDWorkProject.projectId
	print("projectId"..projectId)
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

	if Project.InfoFlow == nil then
		local nm = NW.msg("PROJECT.CS.GETSUPWORKFLOW")
		nm:writeU32(projectId)
		NW.send(nm)
		
	else
		on_projectflow_init()
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
	NW.unsubscribe("PROJECT.SC.GETSUPWORKFLOW", on_projectflow_init)
end

local P = {
	start = start,
	update_view = update_view,
	on_recycle = on_recycle,
}
return P

