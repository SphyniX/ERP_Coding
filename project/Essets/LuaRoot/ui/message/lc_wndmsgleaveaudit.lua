--
-- @file    ui/message/lc_wndmsgleaveaudit.lua
-- @authors zl
-- @date    2016-09-05 06:45:03
-- @desc    WNDMsgLeaveAudit
--

local ipairs, pairs
    = ipairs, pairs
local libugui = require "libugui.cs"
local libunity = require "libunity.cs"
local UIMGR = MERequire "ui/uimgr"
local NW = MERequire "network/networkmgr"
local UI_DATA = MERequire "datamgr/uidata.lua"
local DY_DATA = MERequire "datamgr/dydata.lua"
local Ref
local Msg

local function on_leaveaudit(Ret)
	if Ret.ret == 1 then
		UIMGR.close_window(Ref.root)
	end
end
--!*以下：自动生成的回调函数*--

local function on_submain_btncomfire_click(btn)
	local nm = NW.msg("MESSAGE.CS.LEAVEAUDIT")
	nm:writeU32(Msg.id)
	nm:writeU32(2)
	NW.send(nm)
end

local function on_submain_btncancel_click(btn)
	local nm = NW.msg("MESSAGE.CS.LEAVEAUDIT")
	nm:writeU32(Msg.id)
	nm:writeU32(3)
	NW.send(nm)
end

local function on_subtop_btnback_click(btn)
	UIMGR.close_window(Ref.root)
end

local function init_view()
	Ref.SubMain.btnComfire.onAction = on_submain_btncomfire_click
	Ref.SubMain.btnCancel.onAction = on_submain_btncancel_click
	Ref.SubTop.btnBack.onAction = on_subtop_btnback_click
	--!*以上：自动注册的回调函数*--
end

local function init_logic()
	local Msg = UI_DATA.WNDMsgContext.Msg
	UI_DATA.WNDMsgContext.Msg = nil
	if Msg == nil then return end
	if Msg.people == 0 then 
		Ref.SubMain.lbName.text = "系统消息"
	else
		Ref.SubMain.lbName.text = LowerList[Msg.people] and LowerList[Msg.people].name or Msg.people
	end
	Ref.SubMain.lbTime.text = Msg.time
	Ref.SubMain.lbText.text = Msg.context
end

local function start(self)
	NW.subscribe("MESSAGE.SC.LEAVEAUDIT", on_leaveaudit)
	if Ref == nil or Ref.root ~= self then
		Ref = libugui.GenLuaTable(self, "root")
		init_view()
	end
	init_logic()
end

local function update_view()
	
end

local function on_recycle()
	NW.unsubscribe("MESSAGE.SC.LEAVEAUDIT", on_leaveaudit)
end

local P = {
	start = start,
	update_view = update_view,
	on_recycle = on_recycle,
}
return P

