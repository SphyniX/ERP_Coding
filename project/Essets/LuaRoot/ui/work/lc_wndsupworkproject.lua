--
-- @file    ui/work/lc_wndsupworkproject.lua
-- @authors zl
-- @date    2016-08-15 10:53:33
-- @desc    WNDSupWorkProject
--

local ipairs, pairs
    = ipairs, pairs
local libugui = require "libugui.cs"
local libunity = require "libunity.cs"
local UIMGR = MERequire "ui/uimgr"
local UI_DATA = MERequire "datamgr/uidata.lua"
local DY_DATA = MERequire "datamgr/dydata.lua"
local NW = MERequire "network/networkmgr"
local Ref
local Project
--!*以下：自动生成的回调函数*--

local function on_subtop_btnback_click(btn)
	UIMGR.close_window(Ref.root)
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

local function on_project_init()
	local Ref_SubMain_SubContent = Ref.SubMain.SubContent
	local projectId = UI_DATA.WNDSupWorkProject.projectId
	Project = DY_DATA.ProjectList[projectId]
	if Project.Info == nil then return end

	Ref_SubMain_SubContent.lbTip.text = Project.Info.type
	
	print(Ref_SubMain_SubContent)
	print(Ref_SubMain_SubContent.SubWords)
	print(Ref_SubMain_SubContent.SubWords.lbText)
	Ref_SubMain_SubContent.SubWords.lbText.text = Project.Info.words
	Ref_SubMain_SubContent.SubTitle.lbText.text = Project.Info.title
	Ref_SubMain_SubContent.SubWay.lbText.text = Project.Info.method
end

local function init_view()
	Ref.SubTop.btnBack.onAction = on_subtop_btnback_click
	--!*以上：自动注册的回调函数*--
end

local function init_logic()
	NW.subscribe("PROJECT.SC.GETPROINFOR", on_project_init)
	NW.subscribe("WORK.SC.GETPRODUCT", on_product_init)

	local projectId = UI_DATA.WNDSupWorkProject.projectId
	Project = DY_DATA.ProjectList[projectId]
	if Project == nil then print("Project 为空"..projectId) return end
	
	local Ref_SubMain_SubContent = Ref.SubMain.SubContent
	Ref_SubMain_SubContent.lbName.text = Project.name
	UIMGR.get_photo(Ref_SubMain_SubContent.spIcon, Project.icon)
	-- Ref_SubMain_SubContent.spIcon -=-----------------------------------------------
	
	if Project.Info == nil then
		local nm = NW.msg("PROJECT.CS.GETPROINFOR")
		nm:writeU32(projectId)
		NW.send(nm)
	else
		on_project_init()
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
	NW.unsubscribe("WORK.SC.GETPRODUCT", on_product_init)
end

local P = {
	start = start,
	update_view = update_view,
	on_recycle = on_recycle,
}
return P

