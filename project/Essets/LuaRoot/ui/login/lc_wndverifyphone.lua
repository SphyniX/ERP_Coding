--
-- @file    ui/login/lc_wndverifyphone.lua
-- @authors ckxz
-- @date    2016-07-04 17:27:51
-- @desc    WNDVerifyPhone
--

local ipairs, pairs
    = ipairs, pairs
local libugui = require "libugui.cs"
local libunity = require "libunity.cs"
local libsystem = require "libsystem.cs"
local UIMGR = MERequire "ui/uimgr"
local LOGIN = MERequire "libmgr/login.lua"
local UI_DATA = MERequire "datamgr/uidata.lua"
local Ref
local phone = ""
local UsingTimer
local x = 1

local function timmer()
	x = x + 1
	if (x % libsystem.GetFps()) == 0 then
		Ref.SubMain.SubVerifyCode.lbTime.text = tostring(tonumber(Ref.SubMain.SubVerifyCode.lbTime.text)-1)
		if tonumber(Ref.SubMain.SubVerifyCode.lbTime.text) == 0 then
			UsingTimer = false
			Ref.SubMain.SubVerifyCode.lbTime.text = "重新发送"
			libunity.SetActive(Ref.SubMain.SubVerifyCode.lbs,false)
		end
	end
end


local function on_bind_phoned(Ret)
	if Ret.ret == 1 then
		local on_changed = UI_DATA.WNDBindPhone.on_changed
		if on_changed then on_changed() end
	end
end

local function on_get_verifyed(Ret)
	if Ret.ret == 1 then
		
	end
end
--!*以下：自动生成的回调函数*--

local function on_subtop_btnback_click(btn)
	UIMGR.close_window(Ref.root)
end

local function on_submain_subverifycode_btntime_click(btn)
	if UsingTimer then
		return
	else
		local typed = UI_DATA.WNDVerifyPhone.type
		print(typed)
		LOGIN.try_get_verify(phone, typed ,on_get_verifyed)
		Ref.SubMain.SubVerifyCode.lbTime.text = "60"
		libunity.SetActive(Ref.SubMain.SubVerifyCode.lbs,true)
		UsingTimer = true
	end
end

local function on_submain_btnenter_click(btn)
	local idf = Ref.SubMain.SubVerifyCode.inpCode.text
	LOGIN.try_bind_phone(phone, idf, on_bind_phoned)
end

local function init_view()
	Ref.SubTop.btnBack.onAction = on_subtop_btnback_click
	Ref.SubMain.SubVerifyCode.btnTime.onAction = on_submain_subverifycode_btntime_click
	Ref.SubMain.btnEnter.onAction = on_submain_btnenter_click
	--!*以上：自动注册的回调函数*--
end

local function init_logic()
	local UserInfo = UI_DATA.WNDRegist.UserInfo
	phone = UserInfo.phone
	Ref.SubMain.SubPhone.lbPhone.text = phone
	UsingTimer = false
	libunity.SetActive(Ref.SubMain.SubVerifyCode.lbs,false)
	Ref.SubMain.SubVerifyCode.lbTime.text = "重新发送"

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

local function update()
	if UsingTimer then
		timmer()
	end
end

local P = {
	start = start,
	update_view = update_view,
	update = update,
}
return P

