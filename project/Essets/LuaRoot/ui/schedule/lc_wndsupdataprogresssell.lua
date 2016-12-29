--
-- @file    ui/schedule/lc_wndsupdataprogresssell.lua
-- @authors zl
-- @date    2016-11-09 02:58:31
-- @desc    WNDSupDataProgressSell
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

local SaleList
local StoreId
local Aggregate
local ProAggregateList
local userid
local numberorvalue
local closesubsale
--!*以下：自动生成的回调函数*--

local function on_salelist_init( )
	SaleList = DY_DATA.get_promoter_List()
	print("SaleList is :" .. JSON:encode(SaleList))
	if SaleList == nil or SaleList == {} then 
		local nm = NW.msg("WORK.CS.GETSALES")
			nm:writeU32(DY_DATA.User.id)
			NW.send(nm)
		return
	end

	libunity.SetActive(Ref.SubSelect.SubMask.root,true)
	Ref.SubSelect.SubMask.GrpList:dup(#SaleList+1, function ( i, Ent, isNew)
		if i == 1 then 
			Ent.lbName.text = "店铺"
		else
			Ent.lbName.text = SaleList[i-1].name
		end
	end)
	libunity.SetActive(Ref.SubSelect.SubMask.root,false)
end

local function on_aggregate_init()
	Aggregate = DY_DATA.StoreData.Aggregate
	print("Aggregate----xxx----"..JSON:encode(Aggregate))
	if Aggregate == nil then 
		local nm = NW.msg("REPORTED.CS.GETSUPGETAGGREGATE")
			nm:writeU32(StoreId)
			if userid == nil then userid = 0 end
			nm:writeU32(userid)
			NW.send(nm)
		return
	end

	if Aggregate.value == nil or Aggregate.value == "nil" then Aggregate.value = 0 end
	if Aggregate.number == nil or Aggregate.number == "nil" then Aggregate.number = 0 end

	Ref.lbValue.text = Aggregate.value 
	Ref.lbNumber.text = Aggregate.number
end


local function on_proaggregate_init()
	
	ProAggregateList = DY_DATA.StoreData.ProAggregateList
	if ProAggregateList == nil then 
		local nm = NW.msg("REPORTED.CS.GETSUPGETPROAGGREGATE")
			nm:writeU32(StoreId)
			if userid == nil then userid = 0 end
			nm:writeU32(userid)
			NW.send(nm)
		return
	end
	print("ProAggregateList----xxx----"..JSON:encode(ProAggregateList))
	if numberorvalue then
		-- number --
		Ref.SubSale.SubMask.GrpList:dup(#ProAggregateList, function ( i, Ent, isNew)
			Ent.lbName.text = ProAggregateList[i].name
			Ent.lbValue.text = ""
			Ent.lbPrice.text = ""
			Ent.lbNumber.text = ProAggregateList[i].number
			Ent.lbPer.text = ProAggregateList[i].per
		end)
	else
		-- value -- 
		Ref.SubSale.SubMask.GrpList:dup(#ProAggregateList, function ( i, Ent, isNew)
			Ent.lbName.text = ProAggregateList[i].name
			Ent.lbValue.text = "总销量： <size=30>" ..  ProAggregateList[i].value .. "</size> 元"
			Ent.lbPrice.text = "销售价： <size=30> "..  ProAggregateList[i].price .. "</size> 元"
			Ent.lbNumber.text = ""
			Ent.lbPer.text = ""
		end)
	end

end

local function on_subselect_submask_grplist_ent_click(btn)
	local index = tonumber(btn.name:sub(4))
	if index == 1 then
		userid = 0
		Ref.SubSelect.lbName.text = "店铺"
	else
		userid = SaleList[index-1].id
		Ref.SubSelect.lbName.text = SaleList[index-1].name
	end

	local nm = NW.msg("REPORTED.CS.GETSUPGETAGGREGATE")
	nm:writeU32(StoreId)
	nm:writeU32(userid)
	NW.send(nm)
	libunity.SetActive(Ref.SubSelect.SubMask.root,false)
end


local function on_subtop_btnback_click(btn)
	-- print(libunity.SelfActive(Ref.SubSale.SubMask.GrpList))
	if closesubsale then 
		Ref.SubTop.lbTitle.text = "总数据"
		libunity.SetActive(Ref.SubSale.root,false)
		closesubsale = false
	else
		UIMGR.close_window(Ref.root)
	end
	
end

local function on_btnvalue_click(btn)
	Ref.SubTop.lbTitle.text = "详细数据"
	closesubsale = true
	local nm = NW.msg("REPORTED.CS.GETSUPGETPROAGGREGATE")
	nm:writeU32(StoreId)
	if userid == nil then userid = 0 end
	nm:writeU32(userid)
	NW.send(nm)
	numberorvalue = false
	libunity.SetActive(Ref.SubSale.root,true)
end

local function on_btnnumber_click(btn)
	Ref.SubTop.lbTitle.text = "详细数据"
	closesubsale = true
	local nm = NW.msg("REPORTED.CS.GETSUPGETPROAGGREGATE")
	nm:writeU32(StoreId)
	if userid == nil then userid = 0 end
	nm:writeU32(userid)
	NW.send(nm)
	numberorvalue = true
	libunity.SetActive(Ref.SubSale.root,true)
end

local function on_subselect_btnselect_click(btn)

	libunity.SetActive(Ref.SubSelect.SubMask.root,true)

end


local function init_view()
	Ref.SubSelect.SubMask.GrpList.Ent.btn.onAction = on_subselect_submask_grplist_ent_click
	-- Ref.SubSale.SubMask.GrpList.Ent.btn.onAction = on_subsale_submask_grplist_ent_click
	Ref.SubTop.btnBack.onAction = on_subtop_btnback_click
	Ref.btnValue.onAction = on_btnvalue_click
	Ref.btnNumber.onAction = on_btnnumber_click
	Ref.SubSelect.btnSelect.onAction = on_subselect_btnselect_click
	UIMGR.make_group(Ref.SubSelect.SubMask.GrpList, function (New, Ent)
		New.btn.onAction = Ent.btn.onAction
	end)
	UIMGR.make_group(Ref.SubSale.SubMask.GrpList, function (New, Ent)
		New.btn.onAction = Ent.btn.onAction
	end)
	--!*以上：自动注册的回调函数*--
end

local function init_logic()
	----init callback -- 

	NW.subscribe("WORK.SC.GETSALES", on_salelist_init)
	NW.subscribe("REPORTED.SC.GETSUPGETAGGREGATE", on_aggregate_init)

	NW.subscribe("REPORTED.SC.GETSUPGETPROAGGREGATE", on_proaggregate_init)

	----set UIstarff -- 
	print(libunity.SelfActive(Ref.SubSelect.SubMask.root))
	libunity.SetActive(Ref.SubSale.root,false)
	libunity.SetActive(Ref.SubSelect.SubMask.root,false)
	Ref.SubTop.lbTitle.text = "总数据"

	----get DY_DATA and UI_DATA --
	SaleList = DY_DATA.get_promoter_List()
	StoreId = UI_DATA.WNDSupStoreData.storeId
	Aggregate = DY_DATA.StoreData.Aggregate

	----get SocketData --
	local nm = NW.msg("WORK.CS.GETSALES")
	nm:writeU32(DY_DATA.User.id)
	NW.send(nm)

	---- init UI ---
	on_aggregate_init()
	-- if SaleList == nil then
	-- 	local nm = NW.msg("WORK.CS.GETSALES")
	-- 		nm:writeU32(DY_DATA.User.id)
	-- 		NW.send(nm)
	-- 	return
	-- end
	
	-- if Aggregate == nil then
	-- 	local nm = NW.msg("REPORTED.CS.GETSUPGETAGGREGATE")
	-- 		nm:writeU32(StoreId)
	-- 		nm:writeU32(0)
	-- 		NW.send(nm)
	-- 	return
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
	
end

local P = {
	start = start,
	on_recycle = on_recycle,
	update_view = update_view,
}
return P

