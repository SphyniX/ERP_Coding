--
-- @file    ui/schedule/lc_wndsupdataprogersscomdata.lua
-- @authors zl
-- @date    2016-11-09 03:34:13
-- @desc    WNDSupDataProgerssComData
--

local ipairs, pairs
    = ipairs, pairs
local libugui = require "libugui.cs"
local libunity = require "libunity.cs"
local UIMGR = MERequire "ui/uimgr"
local UI_DATA = MERequire "datamgr/uidata.lua"
local DY_DATA = MERequire "datamgr/dydata.lua"
local Ref

local Comid
local ComListRe
--!*以下：自动生成的回调函数*--

local function on_subtop_back_click(btn)
	UIMGR.close_window(Ref.root)
end

local function init_view()
	Ref.SubTop.Back.onAction = on_subtop_back_click
	--!*以上：自动注册的回调函数*--
end

local function init_logic()
	Comid = UI_DATA.WNDSupDataProgerssComData.Comid
	UI_DATA.WNDSupDataProgerssComData.Comid = nil
	ComListRe = DY_DATA.StoreData.ComListRe
	Ref.SubTop.lbTitle.text = ComListRe[Comid].name 
	Ref.lbValue.text = ComListRe[Comid].value
	Ref.lbPrice.text = ComListRe[Comid].price
	UIMGR.get_photo(Ref.spImage,ComListRe[Comid].icon)
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

