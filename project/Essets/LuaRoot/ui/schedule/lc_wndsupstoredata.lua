--
-- @file    ui/schedule/lc_wndsupstoredata.lua
-- @authors zl
-- @date    2016-11-09 01:41:54
-- @desc    WNDSupStoreData
--

local ipairs, pairs
    = ipairs, pairs
local libugui = require "libugui.cs"
local libunity = require "libunity.cs"
local UIMGR = MERequire "ui/uimgr"
local Ref
--!*以下：自动生成的回调函数*--

local function on_subtop_btnback_click(btn)
	UIMGR.close_window(Ref.root)
end

local function on_sublist_btncellimg_click(btn)
	UIMGR.create_window("UI/WNDSupDataUpLoadPhoto")
end

local function on_sublist_btncellmsg_click(btn)
	UIMGR.create_window("UI/WNDSupDataMsgInput")
end

local function on_sublist_btncell_click(btn)
	UIMGR.create_window("UI/WNDSupDataGoodAnalysis")
end

local function on_btncheck_click(btn)
	
end

local function on_btnprogress_click(btn)
	UIMGR.create_window("UI/WNDSupDataProgress")
end

local function init_view()
	Ref.SubTop.btnBack.onAction = on_subtop_btnback_click
	Ref.SubList.btnCellImg.onAction = on_sublist_btncellimg_click
	Ref.SubList.btnCellMsg.onAction = on_sublist_btncellmsg_click
	Ref.SubList.btnCell.onAction = on_sublist_btncell_click
	Ref.btnCheck.onAction = on_btncheck_click
	Ref.btnProgress.onAction = on_btnprogress_click
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

