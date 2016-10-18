--
-- @file    ui/message/lc_wndwritemsg.lua
-- @authors zl
-- @date    2016-08-31 11:00:58
-- @desc    WNDWriteMsg
--

local ipairs, pairs
    = ipairs, pairs
local libugui = require "libugui.cs"
local libunity = require "libunity.cs"
local UIMGR = MERequire "ui/uimgr"
local UI_DATA = MERequire "datamgr/uidata.lua"
local DY_DATA = MERequire "datamgr/dydata.lua"
local NW = MERequire "network/networkmgr"
local Ref

local LowerList = {}
local Lower
local function on_send_finish(Ret)
	if Ret.ret == 1 then
		UIMGR.close_window(Ref.root)
	end
end

local function time_to_string(Time)
	return string.format("%d-%d-%d %d:%d", Time.year, Time.month, Time.day, Time.hour, Time.minute)
end

local function on_lower_init()
	local str = ""
	for k,v in pairs(LowerList) do
		str = str + v.name + ";"
	end
	Ref.SubMain.SubReceiver.lbText.text = str
end

--!*以下：自动生成的回调函数*--

local function on_submain_subreceiver_click(btn)
	UI_DATA.WNDMsgLower.type = nil
	UI_DATA.WNDMsgLower.callback = function (root, Lower)
		LowerList[Lower.id] = Lower
		on_lower_init()
		UIMGR.close_window(root)
	end
	UIMGR.create_window("UI/WNDMsgLower")
end

local function on_submain_subtime_click(btn)
	UI_DATA.WNDSetTime.on_call_back = function (Time)
		Ref.SubMain.SubTime.lbText.text = time_to_string(Time)
	end
	UIMGR.create("UI/WNDSetTime")
end

local function on_subbtm_btnsend_click(btn)
	local time = Ref.SubMain.SubTime.lbText.text
	local context = Ref.SubBtm.inpInput.text
	local nm = NW.msg("MESSAGE.CS.SENDMESSAGE")
	nm:writeU32(DY_DATA.User.id)
	nm:writeString(context)
	local nLower = 0
	for k,v in pairs(LowerList) do
		nLower = nLower + 1
	end
	nm:writeU32(nLower)
	for k,v in pairs(LowerList) do
		nm:writeU32(v.id)
	end
	NW.send(nm)
end

local function on_subtop_btnback_click(btn)
	UIMGR.close_window(Ref.root)
end

local function init_view()
	Ref.SubMain.SubReceiver.btn.onAction = on_submain_subreceiver_click
	Ref.SubMain.SubTime.btn.onAction = on_submain_subtime_click
	Ref.SubBtm.btnSend.onAction = on_subbtm_btnsend_click
	Ref.SubTop.btnBack.onAction = on_subtop_btnback_click
	--!*以上：自动注册的回调函数*--
end

local function init_logic()
	NW.subscribe("MESSAGE.SC.SENDMESSAGE",on_send_finish)
	on_lower_init()
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
	NW.unsubscribe("MESSAGE.SC.SENDMESSAGE",on_send_finish)
end

local P = {
	start = start,
	update_view = update_view,
	on_recycle = on_recycle,
}
return P

