--
-- @file    ui/user/lc_wnduserresetpassword.lua
-- @authors zl
-- @date    2016-08-28 10:07:31
-- @desc    WNDUserResetPassword
--

local ipairs, pairs
    = ipairs, pairs
local libugui = require "libugui.cs"
local libunity = require "libunity.cs"
local UIMGR = MERequire "ui/uimgr"
local NW = MERequire "network/networkmgr"
local DY_DATA = MERequire "datamgr/dydata.lua"
local Ref
local PasswordCtrl = true

local function on_reset_pass(Ret)
	if Ret.ret == 1 then
		UIMGR.close_window(Ref.root)
	end
end

local function on_val_pass(Ret)
	print("on_val_pass")
	if Ret.ret == 1 then
		local password = Ref.SubMain.SubPassword.inpPassword.text
		local password2 = Ref.SubMain.SubPasswordTwo.inpPassword.text
		if password == "" then
			_G.UI.Toast:make(nil, "密码为空"):show()
			return
		end
		if password ~= password2 then
			_G.UI.Toast:make(nil, "密码不相同"):show()
			return
		end
		local nm = NW.msg("USER.CS.UPDATE")
		nm:writeU32(DY_DATA.User.id)
		nm:writeString(password)
		NW.send(nm)
	end
end

--!*以下：自动生成的回调函数*--

local function on_submain_tglshow_change(tgl)
	if tgl.value == true then
		local txt = Ref.SubMain.SubOldPassword.inpPassword.text
		Ref.SubMain.SubOldPassword.inpPassword.text = ""
		Ref.SubMain.SubOldPassword.inpPassword.contentType = "Standard"
		Ref.SubMain.SubOldPassword.inpPassword.text = txt

		local txt = Ref.SubMain.SubPassword.inpPassword.text
		Ref.SubMain.SubPassword.inpPassword.text = ""
		Ref.SubMain.SubPassword.inpPassword.contentType = "Standard"
		Ref.SubMain.SubPassword.inpPassword.text = txt

		txt = Ref.SubMain.SubPasswordTwo.inpPassword.text
		Ref.SubMain.SubPasswordTwo.inpPassword.text = ""
		Ref.SubMain.SubPasswordTwo.inpPassword.contentType = "Standard"
		Ref.SubMain.SubPasswordTwo.inpPassword.text = txt
	else
		local txt = Ref.SubMain.SubOldPassword.inpPassword.text
		Ref.SubMain.SubOldPassword.inpPassword.text = ""
		Ref.SubMain.SubOldPassword.inpPassword.contentType = "Password"
		Ref.SubMain.SubOldPassword.inpPassword.text = txt

		local txt = Ref.SubMain.SubPassword.inpPassword.text
		Ref.SubMain.SubPassword.inpPassword.text = ""
		Ref.SubMain.SubPassword.inpPassword.contentType = "Password"
		Ref.SubMain.SubPassword.inpPassword.text = txt

		local txt = Ref.SubMain.SubPasswordTwo.inpPassword.text
		Ref.SubMain.SubPasswordTwo.inpPassword.text = ""
		Ref.SubMain.SubPasswordTwo.inpPassword.contentType = "Password"
		Ref.SubMain.SubPasswordTwo.inpPassword.text = txt
	end
end


local function on_submain_btnenter_click(btn)
	if PasswordCtrl then
		local oldpass = Ref.SubMain.SubOldPassword.inpPassword.text
		local nm = NW.msg("USER.CS.VALPWD")
		nm:writeU32(DY_DATA.User.id)
		nm:writeString(oldpass)
		NW.send(nm)
	else
		_G.UI.Toast:make(nil, "密码设定过于简单，请重设"):show()
	end
end

local function on_subtop_btnback_click(btn)
	UIMGR.close_window(Ref.root)
end

local function on_recycle()
	NW.unsubscribe("USER.SC.VALPWD", on_val_pass)
	NW.unsubscribe("USER.SC.UPDATE", on_reset_pass)
end

local function init_view()
	Ref.SubMain.tglShow.onAction = on_submain_tglshow_change
	Ref.SubMain.btnEnter.onAction = on_submain_btnenter_click
	Ref.SubTop.btnBack.onAction = on_subtop_btnback_click
	--!*以上：自动注册的回调函数*--
end
local function onSubPassword(input)
	if #Ref.SubMain.SubPassword.inpPassword.text > 8 then
		libunity.SetActive(Ref.SubMain.SubPassword.lbHint,false)
		PasswordCtrl = true
	else
		libunity.SetActive(Ref.SubMain.SubPassword.lbHint,true)
		PasswordCtrl = false
	end
end
local function onSubPasswordTwo(input)
end

local function init_logic()
	NW.subscribe("USER.SC.VALPWD", on_val_pass)
	NW.subscribe("USER.SC.UPDATE", on_reset_pass)
	Ref.SubMain.SubPassword.inpPassword.onSubmit = onSubPassword
	Ref.SubMain.SubPasswordTwo.inpPassword.onSubmit = onSubPasswordTwo
	Ref.SubMain.SubOldPassword.inpPassword.text = ""
	Ref.SubMain.SubPassword.inpPassword.text = ""
	Ref.SubMain.SubPasswordTwo.inpPassword.text = ""
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

local P = {
	start = start,
	update_view = update_view,
}
return P

