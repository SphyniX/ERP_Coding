--
-- @file    ui/user/lc_wndusersetinfo.lua
-- @authors zl
-- @date    2016-10-09 23:26:11
-- @desc    WNDUserSetInfo
--

local ipairs, pairs
    = ipairs, pairs
local libugui = require "libugui.cs"
local libunity = require "libunity.cs"
local UIMGR = MERequire "ui/uimgr"
local DY_DATA = MERequire "datamgr/dydata.lua"
local UI_DATA = MERequire "datamgr/uidata.lua"
local NW = MERequire "network/networkmgr"
local LOGIN = MERequire "libmgr/login.lua"
local Ref


local function on_set_idcard(Photolist, inp)
   	local nPhoto = 0
   	if NW.connected() then
		local function on_http_photo_callback()
	   		nPhoto = nPhoto + 1
	   		if nPhoto >= #Photolist then
		   		local nm = NW.msg("USER.CS.UPDATEIDNUM")
		   		nm:writeU32(DY_DATA.User.id)
		   		nm:writeString(inp)
		   		NW.send(nm)
	   		end
	   	end

	   	for i,v in ipairs(Photolist) do
	   		LOGIN.try_uploadphoto(DY_DATA.User.id, v.typeId, nil, v.image, on_http_photo_callback)
	   	end
	end
end

local function on_set_cardno(Photolist, inp)
   	local nPhoto = 0
   	if NW.connected() then
		local function on_http_photo_callback()
	   		nPhoto = nPhoto + 1
	   		if nPhoto >= #Photolist then
		   		local nm = NW.msg("USER.CS.UPDATECARDNO")
		   		nm:writeU32(DY_DATA.User.id)
		   		nm:writeString(inp)
		   		NW.send(nm)
	   		end
	   	end

	   	for i,v in ipairs(Photolist) do
	   		print(JSON:encode(v))
	   		LOGIN.try_uploadphoto(DY_DATA.User.id, v.typeId, nil, v.image, on_http_photo_callback)
	   	end
	end
end

--!*以下：自动生成的回调函数*--

local function on_submain_subphone_click(btn)
	UIMGR.create_window("UI/WNDUserChangePhone")
end

local function on_submain_subcontact_click(btn)
	UIMGR.create_window("UI/WNDContact")
end

local function on_submain_subuserinfo_click(btn)
	UIMGR.create_window("UI/WNDUserInfo")
end

local function on_submain_subidcard_click(btn)
	UI_DATA.WNDShowPhoto.title = "身份证"
	UI_DATA.WNDShowPhoto.tip = "身份证照片"
	UI_DATA.WNDShowPhoto.input = "身份证号码"
	UI_DATA.WNDShowPhoto.inputvalue = DY_DATA.User.IDcard
	local User = DY_DATA.User
	UI_DATA.WNDShowPhoto.photolist = {
		[1] = { title = "正面", name = User.idcard_front, typeId = 2, dl = true},
		[2] = { title = "背面", name = User.idcard_back, typeId = 3, dl = true},
		[3] = { title = "手持", name = User.idcard_all, typeId = 4, dl = true},
	}
	UI_DATA.WNDShowPhoto.callback = on_set_idcard
   	UIMGR.create_window("UI/WNDSubmitPhoto")
end

local function on_submain_subbankcard_click(btn)
	UI_DATA.WNDShowPhoto.title = "银行卡"
	UI_DATA.WNDShowPhoto.tip = "银行卡照片"
	UI_DATA.WNDShowPhoto.input = "银行卡号码"
	UI_DATA.WNDShowPhoto.inputvalue = DY_DATA.User.bankcard
	local User = DY_DATA.User
	UI_DATA.WNDShowPhoto.photolist = {
		[1] = { title = "正面", name = User.cardNo_front, typeId = 5, dl = true },
		[2] = { title = "背面", name = User.cardNo_back, typeId = 6, dl = true },
	}
	UI_DATA.WNDShowPhoto.callback = on_set_cardno
	UIMGR.create_window("UI/WNDSubmitPhoto")
end

local function on_submain_subpassword_click(btn)
	UIMGR.create_window("UI/WNDUserResetPassword")
end

local function on_subtop_btnback_click(btn)
	UIMGR.close_window(Ref.root)
end

local function on_ui_init()
	local User = DY_DATA.User
	Ref.SubMain.SubPhone.lbText.text = User.phone
end

local function init_view()
	Ref.SubMain.SubPhone.btn.onAction = on_submain_subphone_click
	Ref.SubMain.SubContact.btn.onAction = on_submain_subcontact_click
	Ref.SubMain.SubUserInfo.btn.onAction = on_submain_subuserinfo_click
	Ref.SubMain.SubIDCard.btn.onAction = on_submain_subidcard_click
	Ref.SubMain.SubBankCard.btn.onAction = on_submain_subbankcard_click
	Ref.SubMain.SubPassword.btn.onAction = on_submain_subpassword_click
	Ref.SubTop.btnBack.onAction = on_subtop_btnback_click
	--!*以上：自动注册的回调函数*--
end

local function init_logic()
	NW.subscribe("USER.SC.GETUSERINFOR", on_ui_init)
	on_ui_init()
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
	NW.unsubscribe("USER.SC.GETUSERINFOR", on_ui_init)
end

local P = {
	start = start,
	update_view = update_view,
	on_recycle = on_recycle,
}
return P

