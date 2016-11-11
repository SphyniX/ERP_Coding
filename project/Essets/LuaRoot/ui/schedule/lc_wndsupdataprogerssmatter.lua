--
-- @file    ui/schedule/lc_wndsupdataprogerssmatter.lua
-- @authors zl
-- @date    2016-11-09 03:50:58
-- @desc    WNDSupDataProgerssMatter
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

local MatterListRe
local StoreId
--!*以下：自动生成的回调函数*--

local function on_ui_init( )
	-- body
	MatterListRe = DY_DATA.StoreData.MatterListRe
	if MatterListRe == nil then
		local nm = NW.msg("REPORTED.CS.GETSUPMATTER")
			nm:writeU32(StoreId)
			NW.send(nm)
		return
	end

	local TempMatterListRe = {}
	for i=1,#MatterListRe do
		if MatterListRe[i].value ~= "nil" then 
			table.insert(TempMatterListRe,MatterListRe[i])
		end
	end

	Ref.SubProject.GrpProject:dup(#TempMatterListRe, function (i, Ent, isNew)
		local MetterRe = TempMatterListRe[i]
		Ent.lbName.text = MetterRe.name
		Ent.lbPeople.text = MetterRe.username
		Ent.lbValue.text = MetterRe.value
	end)

end

local function on_subtop_back_click(btn)
	UIMGR.close_window(Ref.root)
end

local function on_subproject_grpproject_entproject_spimage_click(btn)
	
end

local function init_view()
	Ref.SubTop.Back.onAction = on_subtop_back_click
	Ref.SubProject.GrpProject.Ent.spImage.onAction = on_subproject_grpproject_entproject_spimage_click
	UIMGR.make_group(Ref.SubProject.GrpProject, function (New, Ent)
		New.spImage.onAction = Ent.spImage.onAction
	end)
	--!*以上：自动注册的回调函数*--
end

local function init_logic()
	NW.subscribe("REPORTED.SC.GETSUPMATTER", on_ui_init)

	MatterListRe = DY_DATA.StoreData.MatterListRe
	StoreId = UI_DATA.WNDSupStoreData.storeId
	if MatterListRe == nil then
		local nm = NW.msg("REPORTED.CS.GETSUPMATTER")
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
	NW.unsubscribe("REPORTED.SC.GETSUPMATTER", on_ui_init)
end

local P = {
	start = start,
	update_view = update_view,
	on_recycle = on_recycle,
}
return P

