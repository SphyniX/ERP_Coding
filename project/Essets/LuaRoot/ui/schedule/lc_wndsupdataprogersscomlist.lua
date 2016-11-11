--
-- @file    ui/schedule/lc_wndsupdataprogersscomlist.lua
-- @authors zl
-- @date    2016-11-09 03:31:56
-- @desc    WNDSupDataProgerssComList
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
local ComListRe
local StoreId
--!*以下：自动生成的回调函数*--

local function on_subproject_grpproject_entproject_click(btn)
	local index = tonumber(btn.name:sub(11))
	UI_DATA.WNDSupDataProgerssComData.Comid = index
	UIMGR.create_window("UI/WNDSupDataProgerssComData")
end

local function on_subtop_back_click(btn)
	UIMGR.close_window(Ref.root)
end


local function on_ui_init( )
	-- body
	ComListRe = DY_DATA.StoreData.ComListRe
	if ComListRe == nil then
		local nm = NW.msg("REPORTED.CS.GETSUPGETCOMPETING")
			nm:writeU32(StoreId)
			NW.send(nm)
		return
	end

	local TempComListRe = {}
	for i=1,#ComListRe do
		if ComListRe[i].value ~= "nil" then 
			table.insert(TempComListRe,ComListRe[i])
		end
	end

	Ref.SubProject.GrpProject:dup(#TempComListRe, function (i, Ent, isNew)
		local ComLis = TempComListRe[i]
		Ent.lbName.text = ComLis.name
		Ent.lbPeople.text = ComLis.username
		-- UIMGR.get_photo(Ent.spImage,ComLis.icon)
	end)

end

local function init_view()
	Ref.SubProject.GrpProject.Ent.btn.onAction = on_subproject_grpproject_entproject_click
	Ref.SubTop.Back.onAction = on_subtop_back_click
	UIMGR.make_group(Ref.SubProject.GrpProject, function (New, Ent)
		New.btn.onAction = Ent.btn.onAction
	end)
	--!*以上：自动注册的回调函数*--
end

local function init_logic()
	NW.subscribe("REPORTED.SC.GETSUPGETCOMPETING", on_ui_init)

	ComListRe = DY_DATA.StoreData.ComListRe
	StoreId = UI_DATA.WNDSupStoreData.storeId
	if ComListRe == nil then
		local nm = NW.msg("REPORTED.CS.GETSUPGETCOMPETING")
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
	NW.unsubscribe("REPORTED.SC.GETSUPGETCOMPETING", on_ui_init)
end

local P = {
	start = start,
	update_view = update_view,
	on_recycle = on_recycle,
}
return P

