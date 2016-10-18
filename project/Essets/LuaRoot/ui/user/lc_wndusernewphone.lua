--
-- @file    ui/user/lc_wndusernewphone.lua
-- @authors zl
-- @date    2016-08-12 11:18:32
-- @desc    WNDUserNewPhone
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

local function on_reset_phone(Ret)
	if Ret.ret == 1 then
		UIMGR.close_window(Ref.root)
	end
end
--!*以下：自动生成的回调函数*--

local function on_submain_btnbutton_click(btn)
	local phone = Ref.SubMain.inpInput.text
	if phone == "" then
		return
	end
	local nm = NW.msg("USER.CS.UPDATEPHONE")
	nm:writeU32(DY_DATA.User.id)
	nm:writeString(phone)
	NW.send(nm)
end

local function on_subtop_btnback_click(btn)
	UIMGR.close_window(Ref.root)
end

local function init_view()
	Ref.SubMain.btnButton.onAction = on_submain_btnbutton_click
	Ref.SubTop.btnBack.onAction = on_subtop_btnback_click
	--!*以上：自动注册的回调函数*--
end

local function init_logic()
	NW.subscribe("USER.SC.UPDATEPHONE", on_reset_phone)
	Ref.SubMain.inpInput.text = ""
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
	NW.unsubscribe("USER.SC.UPDATEPHONE", on_reset_phone)
end

local P = {
	start = start,
	update_view = update_view,
	on_recycle = on_recycle,
}
return P

