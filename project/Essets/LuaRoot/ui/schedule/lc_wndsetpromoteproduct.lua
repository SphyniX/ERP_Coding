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
local NowEnt
local ProductList
local ProductListOld
local ProductListForUpdate
--!*以下：自动生成的回调函数*--

local function on_submain_grp_ent_click(btn)
	NowEnt = tonumber(btn.name:sub(4))
	libunity.SetActive(Ref.SubSet.root, true)
	libunity.SetActive(Ref.SubSet.btnSubmit,false)
end

local function on_subtop_btnclear_click(btn)
	Ref.SubMain.Grp:dup(#ProductList, function (i, Ent, isNew)
		local Product = ProductList[i]
		Ent.lbName.text = Product.name
		Ent.lbVolume.text = "   " .. ProductList[i].per
		Ent.lbPrice.text = "   " .. "元"
		Ent.lbSale.text = "   " .. "元"
	end)
end

local function on_subtop_btnback_click(btn)
	UIMGR.close_window(Ref.root)
end

local function on_subset_btncount_click(btn)
	Ref.SubSet.lbSale.text = tostring(tonumber(Ref.SubSet.inpVolume.text) * tonumber(Ref.SubSet.inpPrice.text))
	libunity.SetActive(Ref.SubSet.btnCount,false)
	libunity.SetActive(Ref.SubSet.btnSubmit,true)
end

local function on_subset_btnsubmit_click(btn)
	Ref.SubMain.Grp:dup(#ProductList, function (i, Ent, isNew)
		if i == NowEnt then 
			Ent.lbVolume.text = "<size=36>" .. Ref.SubSet.inpVolume.text .. "</size>" .. ProductList[i].per
			Ent.lbPrice.text = "<size=36>" .. Ref.SubSet.inpPrice.text .. "</size>" .. "元"
			Ent.lbSale.text = "<size=36>" .. Ref.SubSet.lbSale.text .. "</size>" .. "元"
			Ref.SubSet.inpVolume.text = nil
			Ref.SubSet.inpPrice.text = nil
			Ref.SubSet.lbSale.text = nil
		end
	end)
	libunity.SetActive(Ref.SubSet.btnCount,true)
	libunity.SetActive(Ref.SubSet.root, false)
end

local function on_subset_btnback_click(btn)
	libunity.SetActive(Ref.SubSet.root, false)
end

local function on_btnsave_click(btn)
	ProductListForUpdate = {}
	Ref.SubMain.Grp:dup(#ProductList, function (i, Ent, isNew)
		local price = Ent.lbPrice.text:sub(1,string.len(Ent.lbPrice.text)-3)
		local volume = Ent.lbVolume.text:sub(1,string.len(Ent.lbVolume.text)-3)
		local sale = Ent.lbSale.text:sub(1,string.len(Ent.lbSale.text)-3)
		local value = ""
		if price == "   "then 
			price = ""
		end
		if volume == "   "then 
			volume = ""
		end
		local id = ProductList[i].id
		table.insert(ProductListForUpdate,{id = id , price = price , volume = volume , sale = sale})
	end)
	UI_DATA.WNDSubmitSchedule.ProductList = ProductListForUpdate
	
	UIMGR.close_window(Ref.root)
end



local function on_ui_init()
	libunity.SetActive(Ref.SubSet.root, false)
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
	on_subtop_btnclear_click()
	ProductListForUpdate = UI_DATA.WNDSubmitSchedule.ProductList
	print("ProductListForUpdate is :" .. JSON:encode(ProductListForUpdate))
	if ProductListForUpdate ~= nil then
		Ref.SubMain.Grp:dup(#ProductList, function (i, Ent, isNew)
			local Product = ProductListForUpdate[i]
			if Product.price == ""then 
			Product.price = "   "
			end
			if Product.volume == ""then 
				Product.volume = "   "
			end
			if Product.sale == ""then 
				Product.sale = "   "
			end
			Ent.lbVolume.text = "<size=36>" .. Product.volume .. "</size>" .. ProductList[i].per
			Ent.lbPrice.text = "<size=36>" .. Product.price .. "</size>" .. "元"
			Ent.lbSale.text = "<size=36>" .. Product.sale .. "</size>" .. "元"
		end)
	end
end

local function init_view()
	Ref.SubMain.Grp.Ent.btn.onAction = on_submain_grp_ent_click
	Ref.SubTop.btnClear.onAction = on_subtop_btnclear_click
	Ref.SubTop.btnBack.onAction = on_subtop_btnback_click
	Ref.SubSet.btnCount.onAction = on_subset_btncount_click
	Ref.SubSet.btnSubmit.onAction = on_subset_btnsubmit_click
	Ref.SubSet.btnBack.onAction = on_subset_btnback_click
	Ref.btnSave.onAction = on_btnsave_click
	UIMGR.make_group(Ref.SubMain.Grp, function (New, Ent)
		New.btn.onAction = Ent.btn.onAction
	end)
	--!*以上：自动注册的回调函数*--
end

local function init_logic()
	NW.subscribe("REPORTED.SC.GETPRODUCT", on_ui_init)

	libunity.SetActive(Ref.SubSet.root, false)
	local projectId = UI_DATA.WNDSubmitSchedule.projectId
	local Project = DY_DATA.SchProjectList[projectId]
	if Project == nil then print("Project 为空"..projectId) return end
	if Project.ProductList == nil then
		local nm = NW.msg("REPORTED.CS.GETPRODUCT")
		nm:writeU32(projectId)
		NW.send(nm)
		return
	end
	print("ProductList init:"..#Project.ProductList..JSON:encode(Project.ProductList))
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

