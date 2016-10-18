--
-- @file    ui/system/lc_wndselectprovince.lua
-- @authors zl
-- @date    2016-08-24 18:47:28
-- @desc    WNDSelectProvince
--

local ipairs, pairs
    = ipairs, pairs
local libugui = require "libugui.cs"
local libunity = require "libunity.cs"
local UIMGR = MERequire "ui/uimgr"
local UI_DATA = MERequire "datamgr/uidata.lua"
local Ref

local ProvinceList
--!*以下：自动生成的回调函数*--

local function on_subprovince_grp_ent_click(btn)
	local index = tonumber(btn.name:sub(4))
	UI_DATA.WNDSelectProvince.id = ProvinceList[index].id
	-- local wnd = UIMGR.WNDStack:pop()
	-- wnd:close()
	UIMGR.create_window("UI/WNDSelectCity")
end

local function on_subtop_btnback_click(btn)
	UIMGR.close_window(Ref.root)
end

local function init_view()
	Ref.SubProvince.Grp.Ent.btn.onAction = on_subprovince_grp_ent_click
	Ref.SubTop.btnBack.onAction = on_subtop_btnback_click
	UIMGR.make_group(Ref.SubProvince.Grp, function (New, Ent)
		New.btn.onAction = Ent.btn.onAction
	end)
	--!*以上：自动注册的回调函数*--
end

local function init_logic()
	UI_DATA.WNDSelectProvince.id = nil
	ProvinceList = _G.CFG.CityLib.get_province_list()
	Ref.SubProvince.Grp:dup(#ProvinceList, function (i, Ent, isNew)
		local Province = ProvinceList[i]
		Ent.lbName.text = Province.name
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

