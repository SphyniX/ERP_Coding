--
-- @file    ui/work/lc_wndselectperson.lua
-- @authors cks
-- @date    2016-11-04 15:54:23
-- @desc    WNDSelectPerson
--

local ipairs, pairs
    = ipairs, pairs
local libugui = require "libugui.cs"
local libunity = require "libunity.cs"
local UIMGR = MERequire "ui/uimgr"
local Ref
--!*以下：自动生成的回调函数*--

local function on_submain_grp_ent _tglselect_change(tgl)
	
end

local function on_subbtnsave_click(btn)
	
end

local function on_subtop_subbtnbutton_click(btn)
	
end

local function init_view()
	Ref.SubMain.Grp.Ent.tglSelect.onAction = on_submain_grp_ent _tglselect_change
	Ref.SubbtnSave.btn.onAction = on_subbtnsave_click
	Ref.SubTop.sUBbtnButton.onAction = on_subtop_subbtnbutton_click
	UIMGR.make_group(Ref.SubMain.Grp, function (New, Ent)
		New.tglSelect.onAction = Ent.tglSelect.onAction
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

