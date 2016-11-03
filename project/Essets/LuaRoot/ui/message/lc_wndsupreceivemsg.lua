--
-- @file    ui/message/lc_wndsupreceivemsg.lua
-- @authors cks
-- @date    2016-11-02 21:46:48
-- @desc    WNDSupReceiveMsg
--

local ipairs, pairs
    = ipairs, pairs
local libugui = require "libugui.cs"
local libunity = require "libunity.cs"
local UIMGR = MERequire "ui/uimgr"
local Ref
--!*以下：自动生成的回调函数*--

local function on_subtop_btnprevious_click(btn)
	UIMGR.create_window("UI/WNDSupEditorMsg")
end

local function on_subtop_sendmsg_click(btn)
	
end

local function on_submsg_subsendee_btnbutton_click(btn)
	UIMGR.create_window("UI/WNDSupSendeeSelect")
end

local function on_submsg_subreceivetime_click(btn)
	
end

local function on_subtop_selectcity_click(btn)
	
end

local function on_subtop_btnbutton_click(btn)
	
end

local function init_view()
	Ref.SubTop.btnPrevious.onAction = on_subtop_btnprevious_click
	Ref.SubTop.SendMsg.onAction = on_subtop_sendmsg_click
	Ref.SubMsg.SubSendee.btnButton.onAction = on_submsg_subsendee_btnbutton_click
	Ref.SubMsg.SubReceiveTime.btn.onAction = on_submsg_subreceivetime_click
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

