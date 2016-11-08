--
-- @file    ui/schedule/lc_wndsupdataprogerssstoreimg.lua
-- @authors zl
-- @date    2016-11-09 03:12:23
-- @desc    WNDSupDataProgerssStoreIMg
--

local ipairs, pairs
    = ipairs, pairs
local libugui = require "libugui.cs"
local libunity = require "libunity.cs"
local UIMGR = MERequire "ui/uimgr"
local Ref
--!*以下：自动生成的回调函数*--

local function on_subproject_grpproject_entproject_click(btn)
	
end

local function on_subtop_back_click(btn)
	UIMGR.close_window(Ref.root)
end

local function on_subproject_grpproject_entproject_grpphoto_entimage_click(btn)
	
end

local function init_view()
	Ref.SubProject.GrpProject.Ent.btn.onAction = on_subproject_grpproject_entproject_click
	Ref.SubTop.Back.onAction = on_subtop_back_click
	Ref.SubProject.GrpProject.Ent.GrpPhoto.entimage.onAction = on_subproject_grpproject_entproject_grpphoto_entimage_click
	UIMGR.make_group(Ref.SubProject.GrpProject, function (New, Ent)
		New.btn.onAction = Ent.btn.onAction
		New.GrpPhoto.entimage.onAction = Ent.GrpPhoto.entimage.onAction
	end)
	UIMGR.make_group(Ref.SubProject.GrpProject.entProject.GrpPhoto, function (New, Ent)
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

