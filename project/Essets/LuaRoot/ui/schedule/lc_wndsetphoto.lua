--
-- @file    ui/schedule/lc_wndsetphoto.lua
-- @authors zl
-- @date    2016-10-17 09:31:25
-- @desc    WNDSetPhoto
--

local ipairs, pairs
    = ipairs, pairs
local libugui = require "libugui.cs"
local libunity = require "libunity.cs"
local UIMGR = MERequire "ui/uimgr"
local Ref
--!*以下：自动生成的回调函数*--

local function on_submain_grp_ent_click(btn)
	
end

local function on_submain_grp_ent_spphoto_click(btn)
	
end

local function on_submain_grp_btnsave_click(btn)
	
end

local function on_subtop_btnclear_click(btn)
	
end

local function on_subtop_btnback_click(btn)
	
end

local function init_view()
	Ref.SubMain.Grp.Ent.btn.onAction = on_submain_grp_ent_click
	Ref.SubMain.Grp.Ent.spPhoto.onAction = on_submain_grp_ent_spphoto_click
	Ref.SubMain.Grp.btnSave.onAction = on_submain_grp_btnsave_click
	Ref.SubTop.btnClear.onAction = on_subtop_btnclear_click
	Ref.SubTop.btnBack.onAction = on_subtop_btnback_click
	UIMGR.make_group(Ref.SubMain.Grp, function (New, Ent)
		New.btn.onAction = Ent.btn.onAction
		New.spPhoto.onAction = Ent.spPhoto.onAction
	end)
	--!*以上：自动注册的回调函数*--
end

local function init_logic()
	
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

