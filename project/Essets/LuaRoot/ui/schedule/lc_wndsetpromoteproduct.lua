--
-- @file    ui/schedule/lc_wndsetpromoteproduct.lua
-- @authors ckxz
-- @date    2016-07-28 17:58:06
-- @desc    WNDSetPromoteProduct
--

local ipairs, pairs
    = ipairs, pairs
local libugui = require "libugui.cs"
local libunity = require "libunity.cs"
local UIMGR = MERequire "ui/uimgr"
local UI_DATA = MERequire "datamgr/uidata.lua"
local NW = MERequire "network/networkmgr"
local DY_DATA = MERequire "datamgr/dydata.lua"
local Ref

local ProductList
--!*以下：自动生成的回调函数*--

local function on_subtop_btnback_click(btn)
	UIMGR.close_window(Ref.root)
end

local function on_subtop_btnsave_click(btn)
	-- local SubmitList = UI_DATA.WNDSubmitSchedule.ProductList
	-- if SubmitList == nil then SubmitList = {} end
	
	local SubmitList = {}
	local isNil = true
	for i,v in ipairs(ProductList) do
		local Ent = Ref.SubMain.GrpContent.Ents[i]
		local id = ProductList[i].id
		if id == nil or id == "" then 
			_G.UI.Toast:make(nil, "数据异常"):show()
			return
		end
		
		local volume = Ent.inpVolume.text
		-- if volume == nil or volume == "" then 
		-- 	_G.UI.Toast:make(nil, "有数据未填写"):show()
		-- 	return
		-- end
		
		local price = Ent.inpPrice.text
		-- if price == nil or price == "" then 
		-- 	_G.UI.Toast:make(nil, "有数据未填写"):show()
		-- 	return
		-- end
		
		local average = Ent.inpAveragePrice.text
		-- if average == nil or average == "" then 
		-- 	_G.UI.Toast:make(nil, "有数据未填写"):show()
		-- 	return
		-- end
		if volume ~= "" or price ~= "" or average ~= "" then
			isNil = false
			table.insert(SubmitList, {id = id, volume = volume, price = price, average = average})
		end
	end
	if isNil then 
		_G.UI.Toast:make(nil, "数据不能全为空"):show()
		return
	end
	UI_DATA.WNDSubmitSchedule.ProductList = SubmitList
	UIMGR.close_window(Ref.root)
end

local function on_ui_init()
	local projectId = UI_DATA.WNDSubmitSchedule.projectId
	local Project = DY_DATA.SchProjectList[projectId]
	if Project == nil then print("Project 为空"..projectId) return end
	ProductList = Project.ProductList
	if ProductList == nil then
		return
	end
	Ref.SubMain.GrpContent:dup(#ProductList, function ( i, Ent, isNew)
		local Product = ProductList[i]
		Ent.lbName.text = Product.name
	end)
end

local function init_view()
	Ref.SubTop.btnBack.onAction = on_subtop_btnback_click
	Ref.SubTop.btnSave.onAction = on_subtop_btnsave_click
	UIMGR.make_group(Ref.SubMain.GrpContent)
	--!*以上：自动注册的回调函数*--
end

local function init_logic()
	NW.subscribe("REPORTED.SC.GETPRODUCT", on_ui_init)

	local projectId = UI_DATA.WNDSubmitSchedule.projectId
	local Project = DY_DATA.SchProjectList[projectId]
	if Project == nil then print("Project 为空"..projectId) return end
	if Project.ProductList == nil then
		local nm = NW.msg("REPORTED.CS.GETPRODUCT")
		nm:writeU32(projectId)
		NW.send(nm)
		return
	end
	on_ui_init()
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
	
	NW.unsubscribe("REPORTED.SC.GETPRODUCT", on_ui_init)
end

local P = {
	start = start,
	update_view = update_view,
	on_recycle = on_recycle,
}
return P

