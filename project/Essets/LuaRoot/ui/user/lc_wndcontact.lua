--
-- @file    ui/user/lc_wndcontact.lua
-- @authors ckxz
-- @date    2016-08-01 14:43:23
-- @desc    WNDContact
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
local function on_set_end(Ret)
	if Ret.ret == 1 then
		UIMGR.close_window(Ref.root)
	end
end
--!*以下：自动生成的回调函数*--

local function on_submain_btnsave_click(btn)
	local wechat = Ref.SubMain.SubWeChat.inpInput.text
	local qq = Ref.SubMain.SubQQ.inpInput.text
	local email = Ref.SubMain.SubEmail.inpInput.text

	local User = DY_DATA.User
	if wechat == User.wechat and qq == User.qq and email == User.email then 
		UIMGR.close_window(Ref.root)
		return 
	end

	local nm = NW.msg("USER.CS.UPDATETOCH")
	nm:writeU32(DY_DATA.User.id)
	nm:writeString(wechat)
	nm:writeString(qq)
	nm:writeString(email)
	NW.send(nm)
end

local function on_subtop_btnback_click(btn)
	UIMGR.close_window(Ref.root)
end

local function init_view()
	Ref.SubMain.btnSave.onAction = on_submain_btnsave_click
	Ref.SubTop.btnBack.onAction = on_subtop_btnback_click
	--!*以上：自动注册的回调函数*--
end

local function init_logic()
	NW.subscribe("USER.SC.UPDATETOCH", on_set_end)

	local User = DY_DATA.User
	Ref.SubMain.SubWeChat.inpInput.text = User.wechat
	Ref.SubMain.SubQQ.inpInput.text = User.qq
	Ref.SubMain.SubEmail.inpInput.text = User.email

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
	NW.unsubscribe("USER.SC.UPDATETOCH", on_set_end)
end

local P = {
	start = start,
	update_view = update_view,
	on_recycle = on_recycle,
}
return P

