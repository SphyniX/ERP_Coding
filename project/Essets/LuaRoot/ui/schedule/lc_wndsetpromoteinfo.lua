--
-- @file    ui/schedule/lc_wndsetpromoteinfo.lua
-- @authors ckxz
-- @date    2016-07-28 18:12:56
-- @desc    WNDSetPromoteInfo
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

local ProductList
local ProductListOld
local ProductListForUpdate
--!*以下：自动生成的回调函数*--

local function on_subtop_btnclear_click(btn)


	Ref.SubMain.Grp:dup(#ProductList, function (i, Ent, isNew)
		
		Ent.inpValue.text = nil
	end)


end

local function on_subtop_btnback_click(btn)
	UIMGR.close_window(Ref.root)
end

local function on_btnsave_click(btn)
	ProductListForUpdate = {}
	Ref.SubMain.Grp:dup(#ProductList, function (i, Ent, isNew)
		local id = ProductList[i].id
		local value = Ent.inpValue.text
		table.insert(ProductListForUpdate,{id = id ,value =value})

		end)
	UI_DATA.WNDSubmitSchedule.ProductListInfo = ProductListForUpdate
	UIMGR.close_window(Ref.root)
end

local function on_submain_grp_btnsave_click(btn)
	
	UIMGR.close_window(Ref.root)
end

local function on_ui_init()

	local projectId = UI_DATA.WNDSubmitSchedule.projectId
	local Project = DY_DATA.SchProjectList[projectId]

	ProductList = Project.ProductList
	if ProductList == nil then
		libunity.SetActive(Ref.SubMain.Grp.spNil, true)
		return 
	end
	libunity.SetActive(Ref.SubMain.Grp.spNil, #ProductList == 0)
	Ref.SubMain.Grp:dup(#ProductList, function (i, Ent, isNew)
		local Product = ProductList[i]
		Ent.lbName.text = Product.name
	end)
	ProductListForUpdate = UI_DATA.WNDSubmitSchedule.ProductListInfo
	if ProductListForUpdate ~= nil then
		Ref.SubMain.Grp:dup(#ProductListForUpdate, function (i, Ent, isNew)
			local Product = ProductListForUpdate[i]
			Ent.inpValue.text = Product.value
		end)
	end
end


local function init_view()
	Ref.SubTop.btnClear.onAction = on_subtop_btnclear_click
	Ref.SubTop.btnBack.onAction = on_subtop_btnback_click
	Ref.btnSave.onAction = on_btnsave_click
	UIMGR.make_group(Ref.SubMain.Grp)
	--!*以上：自动注册的回调函数*--
end

local function init_logic()
	NW.subscribe("REPORTED.SC.GETPRODUCT", on_ui_init)
	local projectId = UI_DATA.WNDSubmitSchedule.projectId
	local Project = DY_DATA.SchProjectList[projectId]
	if Project == nil then print("Project 为空"..projectId) return end
	if Project.MechanismList == nil then
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
	-- NW.unsubscribe("WORK.SC.GETMECHANISM", on_ui_init)
end

local P = {
	start = start,
	update_view = update_view,
	on_recycle = on_recycle,
}
return P

