--
-- @file    ui/home/lc_wndresetframe.lua
-- @authors admin
-- @date    2016-02-23 20:47:36
-- @desc    WNDResetFrame
--

local ipairs, pairs
    = ipairs, pairs
local libugui = require "libugui.cs"
local libunity = require "libunity.cs"
local UIMGR = MERequire "ui/uimgr"
local Ref
local FrameList
--!*以下：自动生成的回调函数*--

local function on_submain_subiconlist_subview_grplist_entframe_btnselect_click(btn)
	
end

local function on_submain_btnclose_click(btn)
	UIMGR.close(Ref.root)
end

local function on_submain_subiconlist_subiconlist_grplist_entframe_btnselect_click(btn)
	local index = tonumber(btn.name:sub(10))
	local Frame = FrameList[index]
	UIMGR.close(Ref.root)
end

local function init_view()
	Ref.SubMain.SubIconList.SubView.GrpList.Ent.btnSelect.onAction = on_submain_subiconlist_subview_grplist_entframe_btnselect_click
	Ref.SubMain.btnClose.onAction = on_submain_btnclose_click
	UIMGR.make_group(Ref.SubMain.SubIconList.SubView.GrpList, function (New, Ent)
		New.btnSelect.onAction = Ent.btnSelect.onAction
	end)
	--!*以上：自动注册的回调函数*--
end

local function init_logic()
	local IconLib = MERequire "datamgr/parser/iconlib.lua"
	FrameList = IconLib.Frames
	Ref.SubMain.SubIconList.SubView.GrpList:dup(#FrameList, function ( i, E, isNew)
		local Frame = FrameList[i]
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

