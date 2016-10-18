--
-- @file    ui/system/lc_wndselectcity.lua
-- @authors zl
-- @date    2016-08-24 18:47:34
-- @desc    WNDSelectCity
--

local ipairs, pairs
    = ipairs, pairs
local libugui = require "libugui.cs"
local libunity = require "libunity.cs"
local UIMGR = MERequire "ui/uimgr"
local UI_DATA = MERequire "datamgr/uidata.lua"
local Ref
local CityList
--!*以下：自动生成的回调函数*--

local function on_subcity_grp_ent_click(btn)
	local index = tonumber(btn.name:sub(4))
	local City = CityList[index]

	local callBack = UI_DATA.WNDSelectCity.callBack
	if callBack then callBack(City) end
	-- local wnd = UIMGR.WNDStack:pop()
	-- wnd:close()
	UIMGR.close_window(Ref.root)
	UIMGR.close_window()
end

local function on_subtop_btnback_click(btn)
	UIMGR.close_window(Ref.root)
end

local function init_view()
	Ref.SubCity.Grp.Ent.btn.onAction = on_subcity_grp_ent_click
	Ref.SubTop.btnBack.onAction = on_subtop_btnback_click
	UIMGR.make_group(Ref.SubCity.Grp, function (New, Ent)
		New.btn.onAction = Ent.btn.onAction
	end)
	--!*以上：自动注册的回调函数*--
end

local function init_logic()
	local provinceId = UI_DATA.WNDSelectProvince.id
	UI_DATA.WNDSelectProvince.id = nil
	CityList = _G.CFG.CityLib.get_city_list(provinceId)
	Ref.SubCity.Grp:dup(#CityList , function (i, Ent, isNew)
		 local City = CityList[i]
		 Ent.lbName.text = City.name
	end)
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

