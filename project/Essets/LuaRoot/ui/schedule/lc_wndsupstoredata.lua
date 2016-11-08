--
-- @file    ui/schedule/lc_wndsupstoredata.lua
-- @authors zl
-- @date    2016-11-08 08:02:34
-- @desc    WndsupStoreData
--

local ipairs, pairs
    = ipairs, pairs
local libugui = require "libugui.cs"
local libunity = require "libunity.cs"
local UIMGR = MERequire "ui/uimgr"
local Ref
--!*以下：自动生成的回调函数*--

local function on_subtop_btnback_click(btn)
	
end

local function on_sublist_subcellimg_click(btn)
	
end

local function on_sublist_subcellmsg_click(btn)
	
end

local function on_sublist_subcell_click(btn)
	
end

local function on_subcheck_click(btn)
	
end

local function on_subprogress_click(btn)
	
end

local function init_view()
	Ref.SubTop.btnBack.onAction = on_subtop_btnback_click
	Ref.SubList.SubCellImg.btn.onAction = on_sublist_subcellimg_click
	Ref.SubList.SubCellMsg.btn.onAction = on_sublist_subcellmsg_click
	Ref.SubList.SubCell.btn.onAction = on_sublist_subcell_click
	Ref.SubCheck.btn.onAction = on_subcheck_click
	Ref.SubProgress.btn.onAction = on_subprogress_click
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

