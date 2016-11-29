--
-- @file    ui/schedule/lc_wndsupdatauploadphoto.lua
-- @authors zl
-- @date    2016-11-09 02:39:46
-- @desc    WNDSupDataUpLoadPhoto
--

local ipairs, pairs
    = ipairs, pairs
local libugui = require "libugui.cs"
local libunity = require "libunity.cs"
local UIMGR = MERequire "ui/uimgr"
local Ref
--!*以下：自动生成的回调函数*--

local function on_subphoto_grpphoto_entphoto_click(btn)
	
end

local function on_subtop_btnback_click(btn)
	UIMGR.close_window(Ref.root)
end

local function on_subtop_btnsave_click(btn)
	
end

local function on_subtop_btnclear_click(btn)
	
end




local function init_view()
	Ref.SubPhoto.GrpPhoto.Ent.btn.onAction = on_subphoto_grpphoto_entphoto_click
	Ref.SubTop.btnBack.onAction = on_subtop_btnback_click
	Ref.SubTop.btnSave.onAction = on_subtop_btnsave_click
	Ref.SubTop.btnClear.onAction = on_subtop_btnclear_click
	UIMGR.make_group(Ref.SubPhoto.GrpPhoto, function (New, Ent)
		New.btn.onAction = Ent.btn.onAction
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

