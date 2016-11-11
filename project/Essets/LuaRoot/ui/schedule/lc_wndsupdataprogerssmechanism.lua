--
-- @file    ui/schedule/lc_wndsupdataprogerssmechanism.lua
-- @authors zl
-- @date    2016-11-09 03:22:29
-- @desc    WNDSupDataProgerssMechanism
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

local MechanismList
local StoreId
--!*以下：自动生成的回调函数*--

local function on_subtop_back_click(btn)
	UIMGR.close_window(Ref.root)
end





local function on_ui_init( )
	-- body
	MechanismList = DY_DATA.StoreData.MechanismList
	if MechanismList == nil then
		local nm = NW.msg("REPORTED.CS.GETSUPMECHANISM")
			nm:writeU32(StoreId)
			NW.send(nm)
		return
	end

	local TempMechanismList = {}
	for i=1,#MechanismList do
		if MechanismList[i].value ~= "nil" then 
			table.insert(TempMechanismList,MechanismList[i])
		end
	end

	Ref.SubProject.GrpProject:dup(#TempMechanismList, function (i, Ent, isNew)
		local Mechanism = TempMechanismList[i]
		Ent.lbName.text = Mechanism.name
		Ent.lbPeople.text = Mechanism.username
		Ent.lbValue.text = Mechanism.value
		-- UIMGR.get_photo(Ent.spImage,Mechanism.name)
	end)

end

local function init_view()
	Ref.SubTop.Back.onAction = on_subtop_back_click
	UIMGR.make_group(Ref.SubProject.GrpProject)
	--!*以上：自动注册的回调函数*--
end

local function init_logic()

	NW.subscribe("REPORTED.SC.GETSUPMECHANISM", on_ui_init)

	MechanismList = DY_DATA.StoreData.MechanismList
	StoreId = UI_DATA.WNDSupStoreData.storeId
	if MechanismList == nil then
		local nm = NW.msg("REPORTED.CS.GETSUPMECHANISM")
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
	NW.unsubscribe("REPORTED.SC.GETSUPMECHANISM", on_ui_init)
end

local P = {
	start = start,
	update_view = update_view,
	on_recycle = on_recycle,
}
return P

