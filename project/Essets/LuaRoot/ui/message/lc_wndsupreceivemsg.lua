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
local DY_DATA = MERequire "datamgr/dydata.lua"
local UI_DATA = MERequire "datamgr/uidata.lua"
local NW = MERequire "network/networkmgr"
local Ref
--!*以下：自动生成的回调函数*--

local function on_subtop_btnprevious_click(btn)
	UIMGR.create_window("UI/WNDSupEditorMsg")
end

local function on_subtop_sendmsg_click(btn)
	if Ref.SubMsg.SubSendee.lbText.text~="" then
	--local UserID = 1
	local UserConcent = Ref.SubMsg.SubTest.lbText.text
	local UserReceive
	print(UserConcent.."------------")
	local nm = NW.msg("MESSAGE.CS.SENDMESSAGE")
	nm:writeU32(DY_DATA.User.id)
	nm:writeString(UserConcent)
	nm:writeU32(#UI_DATA.WNDSUPSENDEESELECT)
	for i=1,#UI_DATA.WNDSUPSENDEESELECT do
		UserReceive = UI_DATA.WNDSUPSENDEESELECT[i].id
		print("UserReceive-------------"..UserReceive)
		nm:writeU32(UserReceive)
	end

	--nm:writeString(UserID)
	NW.send(nm)
	UIMGR.create_window("UI/WNDSupEditorMsg")
	else
	_G.UI.Toast:make(nil, "请选择收件人"):show()
	end
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
	Ref.SubMsg.SubTest.lbText.text=UI_DATA.WNDSupEditorMsg.InputText
	if UI_DATA.WNDSUPSENDEESELECT~=nil and #UI_DATA.WNDSUPSENDEESELECT>0 then
	local  names =""
	for i=1,#UI_DATA.WNDSUPSENDEESELECT do
	
	names=names.. UI_DATA.WNDSUPSENDEESELECT[i].name
	end
	Ref.SubMsg.SubSendee.lbText.text=names
else
	Ref.SubMsg.SubSendee.lbText.text=""
	end
	
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

