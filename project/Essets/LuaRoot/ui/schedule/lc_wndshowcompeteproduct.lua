--
-- @file    ui/schedule/lc_wndshowcompeteproduct.lua
-- @authors zl
-- @date    2016-09-04 05:18:54
-- @desc    WNDShowCompeteProduct
--

local ipairs, pairs
    = ipairs, pairs
local libugui = require "libugui.cs"
local libunity = require "libunity.cs"
local libsystem = require "libsystem.cs"
local UIMGR = MERequire "ui/uimgr"
local NW = MERequire "network/networkmgr"
local DY_DATA = MERequire "datamgr/dydata.lua"
local UI_DATA = MERequire "datamgr/uidata.lua"
local Ref
local Project, CompeteProductList
local curProductIndex

local on_day_init, on_com_init

local function get_title_name(TitleList, id)
	for i,v in ipairs(TitleList) do
		if v.id == id then return v.name end
	end
	return nil
end

--!*以下：自动生成的回调函数*--

local function on_submain_subhard_subproduct_subselect_grp_entproduct_click(btn)
	local index = tonumber(btn.name:sub(11))
	libunity.SetActive(Ref.SubMain.SubHard.SubProduct.root, false)
	curProductIndex = index
	-- on_com_init()
	on_day_init()
end

local function on_submain_subhard_btnproduct_click(btn)
	libunity.SetActive(Ref.SubMain.SubHard.SubProduct.root, true)
end

local function on_submain_subhard_subproduct_btnback_click(btn)
	libunity.SetActive(Ref.SubMain.SubHard.SubProduct.root, false)
end

local function on_subtop_btnback_click(btn)
	UIMGR.close_window(Ref.root)
end

local function on_subtop_subtime_click(btn)
	UI_DATA.WNDSetTime.on_call_back = function (Time)
		local strTime = string.format("%d/%d/%d", Time.year, Time.month, Time.day)
		UI_DATA.WNDSelectStore.day = strTime
		on_day_init()
	end
	UIMGR.create("UI/WNDSetDay")
end

on_com_init = function()
	print("on_com_init")
	local Product = CompeteProductList[curProductIndex]
	if Product == nil then return end
	local id = Product.id

	local day = UI_DATA.WNDSelectStore.day
	if day == nil then 
		--设置时间
		Ref.SubTop.SubTime.lbTime.text = "选择时间"
		return 
	end
	Ref.SubTop.SubTime.lbTime.text = day

	if DY_DATA.CompareProductList == nil or DY_DATA.CompareProductList[id] == nil or DY_DATA.CompareProductList[id][day] == nil then
		return
	end
	local InfoList = DY_DATA.CompareProductList[id][day]
	-- debug
	-- InfoList = {
	-- 	{ id = 1, PersonList = {{id = 2, name = "AA", context = "asdf"},{id = 2, name = "AA", context = "asdf"},},},
	-- 	{ id = 2, PersonList = {{id = 2, name = "AA", context = "asdf"},{id = 2, name = "AA", context = "asdf"},},},
	-- 	{ id = 1, PersonList = {{id = 2, name = "AA", context = "asdf"},{id = 2, name = "AA", context = "asdf"},},},
	-- 	{ id = 1, PersonList = {{id = 2, name = "AA", context = "asdf"},{id = 2, name = "AA", context = "asdf"},},},
	-- }

	Ref.SubMain.Grp:dup( #InfoList, function (i, Ent,  isNew)
		local Info = InfoList[i]
		local name =  get_title_name(Product.TitleList, Info.id) or "数据"
		Ent.lbName.text = name
		local PersonList = Info.PersonList
		UIMGR.dup_new_group(Ent, Ent.go, "Grp", Ref.SubInfo.root, #PersonList, function (j, EntJ, isNewJ)
			local Person = PersonList[j]
			EntJ.lbName.text = Person.name
			EntJ.lbText.text = Person.context
		end)
	end)
end

on_day_init = function()
	local Product = CompeteProductList[curProductIndex]
	if Product == nil then return end
	Ref.SubMain.SubHard.lbName.text = Product.name
	local id = Product.id

	local day = UI_DATA.WNDSelectStore.day
	if day == nil then 
		--设置时间
		Ref.SubTop.SubTime.lbTime.text = "选择时间"
		return 
	end
	Ref.SubTop.SubTime.lbTime.text = day

	-- debug
	-- DY_DATA.CompareProductList = {}
	-- DY_DATA.CompareProductList[id] = {}
	-- DY_DATA.CompareProductList[id][day] = {
	-- 	{ id = 1, PersonList = {{id = 2, name = "AA", context = "asdf"},{id = 2, name = "AA", context = "asdf"},},},
	-- 	{ id = 2, PersonList = {{id = 2, name = "AA", context = "asdf"},{id = 2, name = "AA", context = "asdf"},},},
	-- 	{ id = 3, PersonList = {{id = 2, name = "AA", context = "asdf"},{id = 2, name = "AA", context = "asdf"},},},
	-- 	{ id = 4, PersonList = {{id = 2, name = "AA", context = "asdf"},{id = 2, name = "AA", context = "asdf"},},},
	-- }

	if DY_DATA.CompareProductList == nil or DY_DATA.CompareProductList[id] == nil or DY_DATA.CompareProductList[id][day] == nil then
		local nm = NW.msg("REPORTED.CS.GETCOM")
		nm:writeU32(id)
		nm:writeString(day)
		NW.send(nm)
		return
	end
	on_com_init()
end

local function on_product_init()
	libunity.SetActive(Ref.SubMain.SubHard.SubProduct.root, true)
	
	CompeteProductList = Project.CompeteProductList
	if CompeteProductList == nil then return end
	Ref.SubMain.SubHard.SubProduct.SubSelect.Grp:dup( #CompeteProductList, function (i, Ent,  isNew)
		local ComPro = CompeteProductList[i]
		Ent.lbText.text = ComPro.name
	end)
	libunity.SetActive(Ref.SubMain.SubHard.SubProduct.root, false)
	if curProductIndex == nil then curProductIndex = 1 end
	local Product = CompeteProductList[curProductIndex]
	if Product == nil then return end
	Ref.SubMain.SubHard.lbName.text = Product.name
	local day = UI_DATA.WNDSelectStore.day
	if day == nil then 
		--设置时间
		Ref.SubTop.SubTime.lbTime.text = "选择时间"
		return 
	end
	on_day_init()
end

local function init_view()
	Ref.SubMain.SubHard.SubProduct.SubSelect.Grp.Ent.btn.onAction = on_submain_subhard_subproduct_subselect_grp_entproduct_click
	Ref.SubMain.SubHard.btnProduct.onAction = on_submain_subhard_btnproduct_click
	Ref.SubMain.SubHard.SubProduct.btnBack.onAction = on_submain_subhard_subproduct_btnback_click
	Ref.SubTop.btnBack.onAction = on_subtop_btnback_click
	Ref.SubTop.SubTime.btn.onAction = on_subtop_subtime_click
	UIMGR.make_group(Ref.SubInfo)
	UIMGR.make_group(Ref.SubMain.Grp)
	UIMGR.make_group(Ref.SubMain.SubHard.SubProduct.SubSelect.Grp, function (New, Ent)
		New.btn.onAction = Ent.btn.onAction
	end)
	--!*以上：自动注册的回调函数*--
end

local function init_logic()
	NW.subscribe("WORK.SC.GETCOMLIST", on_product_init)
	NW.subscribe("REPORTED.CS.GETCOM", on_com_init)
	libunity.SetActive(Ref.SubMain.SubHard.SubProduct.root, false)
	curProductIndex = nil

	local day = UI_DATA.WNDSelectStore.day
	if day == nil then
		local dateTime = libsystem.DateTime()
		print(dateTime)
		local strT = dateTime:split(' ')
		print(JSON:encode(dateTime))
		day = strT[1]
		UI_DATA.WNDSelectStore.day = day
		--设置时间
	end

	local projectId = UI_DATA.WNDSelectStore.projectId
	Project = DY_DATA.ProjectList[projectId]
	if Project == nil then return end

	if Project.CompeteProductList == nil then
		local nm = NW.msg("WORK.CS.GETCOMLIST")
		nm:writeU32(projectId)
		NW.send(nm)
		return
	end
	
	on_product_init()
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
	NW.unsubscribe("WORK.SC.GETCOMLIST", on_product_init)
	NW.unsubscribe("REPORTED.CS.GETCOM", on_com_init)
end

local P = {
	start = start,
	update_view = update_view,
	on_recycle = on_recycle,
}
return P

