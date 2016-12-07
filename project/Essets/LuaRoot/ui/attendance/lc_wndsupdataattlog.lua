--
-- @file    ui/attendance/lc_wndsupdataattlog.lua
-- @authors zl
-- @date    2016-11-10 08:00:59
-- @desc    WNDSupDataAttLog
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
local StoreId
local SaleAttenceList

--!*以下：自动生成的回调函数*--

local function on_sublog_grplog_entlog_subask_btnaskoff_click(btn)
	local index = tonumber(btn.transform.parent.parent.name:sub(7))
	local SaleAttenceList = DY_DATA.StoreData.SaleAttenceList
	-- print("SaleAttenceList is " .. JSON:encode(SaleAttenceList))
	local Attence = SaleAttenceList[index]
	-- print("Attence is " .. JSON:encode(Attence))
	UI_DATA.WNDAskOffMag.UnderId = Attence.UnderId
	UIMGR.create_window("UI/WNDAskOffMag")
end

local function on_subtop_submouth_btnnew_click(btn)
	libunity.SetActive(Ref.SubTop.SubMouth.btnNew, false)
	libunity.SetActive(Ref.SubTop.SubMouth.btnLast, true)
	DY_DATA.StoreData.SaleAttenceList = {}
	local nm = NW.msg("ATTENCE.CS.GETKAO")
	nm:writeU32(StoreId)
	nm:writeU32(2)
	NW.send(nm)
	-- on_ui_init()
end

local function on_subtop_submouth_btnlast_click(btn)
	libunity.SetActive(Ref.SubTop.SubMouth.btnNew, true)
	libunity.SetActive(Ref.SubTop.SubMouth.btnLast, false)
	DY_DATA.StoreData.SaleAttenceList = {}
	local nm = NW.msg("ATTENCE.CS.GETKAO")
	nm:writeU32(StoreId)
	nm:writeU32(1)

	NW.send(nm)
	-- on_ui_init()
end

local function on_subtop_btnback_click(btn)
	SaleAttenceList = nil
	UIMGR.close_window(Ref.root)
end



local function on_ui_init( )
	
	local SaleAttenceList = DY_DATA.StoreData.SaleAttenceList
	if SaleAttenceList == nil then
		local nm = NW.msg("ATTENCE.CS.GETKAO")
		nm:writeU32(DY_DATA.User.id)
		if libunity.SelfActive(Ref.SubTop.SubMouth.btnNew) then
		    nm:writeU32(1)
		else
			nm:writeU32(2)
		end
		NW.send(nm)
		return
	end
	Ref.SubLog.GrpLog:dup(#SaleAttenceList, function ( i, Ent, isNew)
		local Attence = SaleAttenceList[i]
		if i == 1 then 
			libunity.SetActive(Ent.spline,false)
		end
		
		-- if Attence.Day ~= "无" then 
		Ent.lbName.text = Attence.Name
		Ent.lbUp.text = Attence.Up
		Ent.lbDown.text = Attence.Down
		Ent.lbLeaveTimes.text = Attence.LeaveTimes
		Ent.lbDay.text = Attence.Day
		Ent.lbWeek.text = Attence.Week
		if Attence.UnderState == 1 then
			Ent.SubAsk.lbAskOff.text = "无"
			Ent.SubAsk.btnAskOff:SetInteractable(false)
		else
			Ent.SubAsk.lbAskOff.text = "查看"
			Ent.SubAsk.btnAskOff:SetInteractable(true)
		end
		-- end
	end)

end

local function init_view()
	Ref.SubLog.GrpLog.Ent.SubAsk.btnAskOff.onAction = on_sublog_grplog_entlog_subask_btnaskoff_click
	Ref.SubTop.SubMouth.btnNew.onAction = on_subtop_submouth_btnnew_click
	Ref.SubTop.SubMouth.btnLast.onAction = on_subtop_submouth_btnlast_click
	Ref.SubTop.btnBack.onAction = on_subtop_btnback_click
	UIMGR.make_group(Ref.SubLog.GrpLog, function (New, Ent)
		New.SubAsk.btnAskOff.onAction = Ent.SubAsk.btnAskOff.onAction
	end)
	--!*以上：自动注册的回调函数*--
end

local function init_logic()
	if DY_DATA.StoreData.SaleAttenceList ~= nil then 
		DY_DATA.StoreData.SaleAttenceList = nil
	end
	libunity.SetActive(Ref.SubTop.SubMouth.btnNew, true)
	libunity.SetActive(Ref.SubTop.SubMouth.btnLast, false)
	StoreId = UI_DATA.WNDSupStoreData.storeId

	NW.subscribe("ATTENCE.SC.GETKAO",on_ui_init)
	local SaleAttenceList = DY_DATA.StoreData.SaleAttenceList
	if SaleAttenceList == nil then
		local nm = NW.msg("ATTENCE.CS.GETKAO")
		nm:writeU32(StoreId)
		nm:writeU32(1)
		NW.send(nm)
		return
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
	
end

local P = {
	start = start,
	update_view = update_view,
	on_recycle = on_recycle,
}
return P

