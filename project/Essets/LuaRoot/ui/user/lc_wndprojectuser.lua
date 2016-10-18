--
-- @file    ui/user/lc_wndprojectuser.lua
-- @authors zl
-- @date    2016-08-14 20:46:34
-- @desc    WNDProjectUser
--

local ipairs, pairs
    = ipairs, pairs
local libugui = require "libugui.cs"
local libunity = require "libunity.cs"
local UIMGR = MERequire "ui/uimgr"
local Ref
--!*以下：自动生成的回调函数*--

local function on_submain_subicon_btnchange_click(btn)
	
end

local function on_submain_subaddress_click(btn)
	
end

local function on_submain_subphone_click(btn)
	
end

local function on_submain_subotherphone_click(btn)
	
end

local function on_submain_subpassword_click(btn)
	
end

local function on_submain_subsuggest_click(btn)
	
end

local function on_subtop_btnback_click(btn)
	_G.PKG["libmgr/login"].do_logout()
end

local function on_subbtm_btnsch_click(btn)
-- ##	UIMGR.WNDStack:pop()
	UIMGR.create_window("UI/WNDProjectSchedule")
end

local function on_subbtm_btnmsg_click(btn)
-- ##	UIMGR.WNDStack:pop()
	UIMGR.create_window("UI/WNDProjectMsg")
end

local function init_view()
	Ref.SubMain.SubIcon.btnChange.onAction = on_submain_subicon_btnchange_click
	Ref.SubMain.SubAddress.btn.onAction = on_submain_subaddress_click
	Ref.SubMain.SubPhone.btn.onAction = on_submain_subphone_click
	Ref.SubMain.SubOtherPhone.btn.onAction = on_submain_subotherphone_click
	Ref.SubMain.SubPassword.btn.onAction = on_submain_subpassword_click
	Ref.SubMain.SubSuggest.btn.onAction = on_submain_subsuggest_click
	Ref.SubTop.btnBack.onAction = on_subtop_btnback_click
	Ref.SubBtm.btnSch.onAction = on_subbtm_btnsch_click
	Ref.SubBtm.btnMsg.onAction = on_subbtm_btnmsg_click
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

