--
-- @file    ui/message/lc_wndmsgcontext.lua
-- @authors ckxz
-- @date    2016-08-02 14:18:10
-- @desc    WNDMsgContext
--

local ipairs, pairs
    = ipairs, pairs
local libugui = require "libugui.cs"
local libunity = require "libunity.cs"
local UIMGR = MERequire "ui/uimgr"
local UI_DATA = MERequire "datamgr/uidata.lua"
local NW = MERequire "network/networkmgr"
local DY_DATA = MERequire "datamgr/dydata.lua"
local Ref
--!*以下：自动生成的回调函数*--

local function on_subtop_btnback_click(btn)
	UIMGR.close_window(Ref.root)
end

local function init_view()
	Ref.SubTop.btnBack.onAction = on_subtop_btnback_click
	--!*以上：自动注册的回调函数*--
end

local function init_logic()
	local Msg = UI_DATA.WNDMsgContext.Msg
	local LowerList = DY_DATA.LowerList
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

