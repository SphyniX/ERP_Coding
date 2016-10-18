--
-- @file    ui/home/lc_wndreseticon.lua
-- @authors admin
-- @date    2016-02-23 18:05:36
-- @desc    WNDResetIcon
--

local ipairs, pairs
    = ipairs, pairs
local libugui = require "libugui.cs"
local libunity = require "libunity.cs"
local UIMGR = MERequire "ui/uimgr"
local Ref
local IconList
--!*以下：自动生成的回调函数*--

local function on_submain_subiconlist_subview_grplist_enticon_click(btn)
	local index = tonumber(btn.name:sub(8))
	local Icon = IconList[index]
	UIMGR.close(Ref.root)
end

local function on_submain_btnclose_click(btn)
	UIMGR.close(Ref.root) 
end

local function init_view()
	Ref.SubMain.SubIconList.SubView.GrpList.Ent.btn.onAction = on_submain_subiconlist_subview_grplist_enticon_click
	Ref.SubMain.btnClose.onAction = on_submain_btnclose_click
	UIMGR.make_group(Ref.SubMain.SubIconList.SubView.GrpList, function (New, Ent)
		New.btn.onAction = Ent.btn.onAction
	end)
	--!*以上：自动注册的回调函数*--
end

local function init_logic()
	local IconLib = MERequire "datamgr/parser/iconlib.lua"
	IconList = IconLib.Icons
	Ref.SubMain.SubIconList.SubView.GrpList:dup(#IconList,function ( i, Ent, isNew)
		local Icon = IconList[i]
		
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

