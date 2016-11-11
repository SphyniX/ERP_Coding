--
-- @file    ui/schedule/lc_wndsupdataprogerssgive.lua
-- @authors zl
-- @date    2016-11-09 03:28:08
-- @desc    WNDSupDataProgerssGive
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

local GiftReList
local StoreId
--!*以下：自动生成的回调函数*--



local function on_subtop_back_click(btn)
	UIMGR.close_window(Ref.root)
end

local function on_ui_init( )
	-- body
	GiftReList = DY_DATA.StoreData.GiftReList
	if GiftReList == nil then
		local nm = NW.msg("REPORTED.CS.GETSUPGIFTRE")
			nm:writeU32(StoreId)
			NW.send(nm)
		return
	end

	local TempGiftReList = {}
	for i=1,#GiftReList do
		if GiftReList[i].value ~= "nil" then 
			table.insert(TempGiftReList,GiftReList[i])
		end
	end

	Ref.SubProject.GrpProject:dup(#TempGiftReList, function (i, Ent, isNew)
		local GiftRe = TempGiftReList[i]
		Ent.lbName.text = GiftRe.name
		Ent.lbPeople.text = GiftRe.username
		Ent.lbNumber.text = GiftRe.number
		Ent.lbPer.text = GiftRe.per
	end)

end

local function init_view()
	Ref.SubTop.Back.onAction = on_subtop_back_click
	UIMGR.make_group(Ref.SubProject.GrpProject)
	--!*以上：自动注册的回调函数*--
end

local function init_logic()
	NW.subscribe("REPORTED.SC.GETSUPGIFTRE", on_ui_init)

	GiftReList = DY_DATA.StoreData.GiftReList
	StoreId = UI_DATA.WNDSupStoreData.storeId
	if GiftReList == nil then
		local nm = NW.msg("REPORTED.CS.GETSUPGIFTRE")
			nm:writeU32(StoreId)
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
	NW.unsubscribe("REPORTED.SC.GETSUPGIFTRE", on_ui_init)
end

local P = {
	start = start,
	update_view = update_view,
	on_recycle = on_recycle,
}
return P

