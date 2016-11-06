--
-- @file    ui/system/lc_wndselectplace.lua
-- @authors zl
-- @date    2016-11-04 10:04:10
-- @desc    WNDSelectPlace
--

local ipairs, pairs
    = ipairs, pairs
local libugui = require "libugui.cs"
local libunity = require "libunity.cs"
local UIMGR = MERequire "ui/uimgr"
local UI_DATA = MERequire "datamgr/uidata.lua"
local Ref

local ProvinceList
local CityList
--!*以下：自动生成的回调函数*--

local function on_submain_subselect_subprovince_grp_ent_click(btn)
	
end

local function on_submain_subselect_subcity_grp_ent_click(btn)
	
end

local function on_submain_subselect_btnsave_click(btn)
	
end

local function on_submain_subselect_btncancle_click(btn)
	
end

local function init_view()
	Ref.SubMain.SubSelect.SubProvince.Grp.Ent.btn.onAction = on_submain_subselect_subprovince_grp_ent_click
	Ref.SubMain.SubSelect.SubCity.Grp.Ent.btn.onAction = on_submain_subselect_subcity_grp_ent_click
	Ref.SubMain.SubSelect.btnSave.onAction = on_submain_subselect_btnsave_click
	Ref.SubMain.SubSelect.btnCancle.onAction = on_submain_subselect_btncancle_click
	UIMGR.make_group(Ref.SubMain.SubSelect.SubProvince.Grp, function (New, Ent)
		New.btn.onAction = Ent.btn.onAction
	end)
	UIMGR.make_group(Ref.SubMain.SubSelect.SubCity.Grp, function (New, Ent)
		New.btn.onAction = Ent.btn.onAction
	end)
	--!*以上：自动注册的回调函数*--
end

local function init_logic()
	ProvinceList = UI_DATA.WNDSelectPlace.ProvinceList
	UI_DATA.WNDSelectPlace = nil
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

