--
-- @file    ui/login/lc_wndsetuserinfo.lua
-- @authors ckxz
-- @date    2016-07-04 19:42:45
-- @desc    WNDSetUserInfo
--

local ipairs, pairs
    = ipairs, pairs
local libugui = require "libugui.cs"
local libunity = require "libunity.cs"
local libsystem = require "libsystem.cs"
local UIMGR = MERequire "ui/uimgr"
local UI_DATA = MERequire "datamgr/uidata.lua"
local LOGIN = MERequire "libmgr/login.lua"
local Ref

local function on_validatenumed(Ret)
	if Ret.ret == 1 then
		UIMGR.create_window("UI/WNDSetPassword")
	end
end

--!*以下：自动生成的回调函数*--

local function on_submain_subcontext_subicon_click(btn)
	UIMGR.on_sdk_take_photo("1.png", Ref.SubMain.SubContext.SubIcon.spIcon, function ( succ, name, image )
		if UI_DATA.WNDRegist.UserInfo == nil then UI_DATA.WNDRegist.UserInfo = {} end
		local UserInfo = UI_DATA.WNDRegist.UserInfo
		if UserInfo.PhotoList == nil then UserInfo.PhotoList = {} end
		UserInfo.PhotoList[1] = image
	end)
end

local function on_submain_subcontext_subinfo_subbaseinfo_subone_subage_click(btn)
	UI_DATA.WNDSetTime.on_call_back = function (Time)
		if UI_DATA.WNDRegist.UserInfo == nil then UI_DATA.WNDRegist.UserInfo = {} end
		local UserInfo = UI_DATA.WNDRegist.UserInfo
		local strTime = string.format("%d/%d/%d", Time.year, Time.month, Time.day)
		UserInfo.age = strTime
		Ref.SubMain.SubContext.SubInfo.SubBaseInfo.SubOne.SubAge.lbAge.text = strTime
	end
	UIMGR.create("UI/WNDSetDay")
end

local function on_submain_subcontext_subinfo_subbaseinfo_subone_subsex_click(btn)
	UI_DATA.WNDSetSex.on_call_back = function (sex)
		if UI_DATA.WNDRegist.UserInfo == nil then UI_DATA.WNDRegist.UserInfo = {} end
		local UserInfo = UI_DATA.WNDRegist.UserInfo
		UserInfo.sex = sex
		Ref.SubMain.SubContext.SubInfo.SubBaseInfo.SubOne.SubSex.lbSex.text = sex == 2 and "女" or "男"
	end
	UIMGR.create("UI/WNDSetSex")
end

local function on_submain_subcontext_subinfo_subidcard_subphotolist_subphotofront_click(btn)
	UIMGR.on_sdk_take_photo("2.png", Ref.SubMain.SubContext.SubInfo.SubIDCard.SubPhotoList.SubPhotoFront.spImage, function ( succ, name, image )
		if UI_DATA.WNDRegist.UserInfo == nil then UI_DATA.WNDRegist.UserInfo = {} end
		local UserInfo = UI_DATA.WNDRegist.UserInfo
		if UserInfo.PhotoList == nil then UserInfo.PhotoList = {} end
		UserInfo.PhotoList[2] = image
	end)

end

local function on_submain_subcontext_subinfo_subidcard_subphotolist_subphotoback_click(btn)
	UIMGR.on_sdk_take_photo("3.png", Ref.SubMain.SubContext.SubInfo.SubIDCard.SubPhotoList.SubPhotoBack.spImage, function ( succ, name, image )
		if succ then
			if UI_DATA.WNDRegist.UserInfo == nil then UI_DATA.WNDRegist.UserInfo = {} end
			local UserInfo = UI_DATA.WNDRegist.UserInfo
			if UserInfo.PhotoList == nil then UserInfo.PhotoList = {} end
			UserInfo.PhotoList[3] = image
		else
			UserInfo.PhotoList[3] = nil
		end
	end)

end

local function on_submain_subcontext_subinfo_subidcard_subphotolist_subphotoall_click(btn)
	
	UIMGR.on_sdk_take_photo("4.png", Ref.SubMain.SubContext.SubInfo.SubIDCard.SubPhotoList.SubPhotoAll.spImage, function ( succ, name, image )
		if UI_DATA.WNDRegist.UserInfo == nil then UI_DATA.WNDRegist.UserInfo = {} end
		local UserInfo = UI_DATA.WNDRegist.UserInfo
		if UserInfo.PhotoList == nil then UserInfo.PhotoList = {} end
		UserInfo.PhotoList[4] = image
	end)

end

local function on_submain_subcontext_subinfo_subbankcard_subphotolist_subphotofront_click(btn)
	UIMGR.on_sdk_take_photo("5.png", Ref.SubMain.SubContext.SubInfo.SubBankCard.SubPhotoList.SubPhotoFront.spImage, function ( succ, name, image )
		if UI_DATA.WNDRegist.UserInfo == nil then UI_DATA.WNDRegist.UserInfo = {} end
		local UserInfo = UI_DATA.WNDRegist.UserInfo
		if UserInfo.PhotoList == nil then UserInfo.PhotoList = {} end
		UserInfo.PhotoList[5] = image
	end)
end

local function on_submain_subcontext_subinfo_subbankcard_subphotolist_subphotoback_click(btn)
	UIMGR.on_sdk_take_photo("6.png", Ref.SubMain.SubContext.SubInfo.SubBankCard.SubPhotoList.SubPhotoBack.spImage, function ( succ, name, image )
		if UI_DATA.WNDRegist.UserInfo == nil then UI_DATA.WNDRegist.UserInfo = {} end
		local UserInfo = UI_DATA.WNDRegist.UserInfo
		if UserInfo.PhotoList == nil then UserInfo.PhotoList = {} end
		UserInfo.PhotoList[6] = image
	end)
end

local function on_submain_subcontext_btnenter_click(btn)
	if UI_DATA.WNDRegist.UserInfo == nil then UI_DATA.WNDRegist.UserInfo = {} end
	local UserInfo = UI_DATA.WNDRegist.UserInfo
	local Ref_Info = Ref.SubMain.SubContext.SubInfo
	UserInfo.height = Ref_Info.SubBaseInfo.SubTwo.inpHigh.text
 	UserInfo.weight = Ref_Info.SubBaseInfo.SubTwo.inpWight.text
 	UserInfo.wechat = Ref_Info.SubContact.SubWeChat.inpWeChat.text
 	UserInfo.qq = Ref_Info.SubContact.SubQQ.inpQQ.text
 	UserInfo.email = Ref_Info.SubContact.SubEmail.inpEmail.text
 	UserInfo.idnumber = Ref_Info.SubIDCard.inpIDCard.text
 	UserInfo.cardNo = Ref_Info.SubBankCard.inpBankCard.text
 	if UserInfo.height == "" or UserInfo.weight == ""  or UserInfo.idnumber == "" or UserInfo.cardNo == ""  then
 		_G.UI.Toast:make(nil, "缺少数据"):show()
 		return
 	end
 	if UserInfo.PhotoList == nil or UserInfo.PhotoList[1] == nil or UserInfo.PhotoList[2] == nil or UserInfo.PhotoList[3] == nil or UserInfo.PhotoList[4] == nil or UserInfo.PhotoList[5] == nil or UserInfo.PhotoList[6] == nil then
 		_G.UI.Toast:make(nil, "缺少图片"):show()
 		return
 	end
 	LOGIN.try_validatenum(UserInfo.idnumber, UserInfo.cardNo, on_validatenumed)
 	-- debug
 	-- on_validatenumed({ret = 1})
end

local function on_subtop_btnback_click(btn)
	UIMGR.close_window(Ref.root)
end

local function init_view()
	Ref.SubMain.SubContext.SubIcon.btn.onAction = on_submain_subcontext_subicon_click
	Ref.SubMain.SubContext.SubInfo.SubBaseInfo.SubOne.SubAge.btn.onAction = on_submain_subcontext_subinfo_subbaseinfo_subone_subage_click
	Ref.SubMain.SubContext.SubInfo.SubBaseInfo.SubOne.SubSex.btn.onAction = on_submain_subcontext_subinfo_subbaseinfo_subone_subsex_click
	Ref.SubMain.SubContext.SubInfo.SubIDCard.SubPhotoList.SubPhotoFront.btn.onAction = on_submain_subcontext_subinfo_subidcard_subphotolist_subphotofront_click
	Ref.SubMain.SubContext.SubInfo.SubIDCard.SubPhotoList.SubPhotoBack.btn.onAction = on_submain_subcontext_subinfo_subidcard_subphotolist_subphotoback_click
	Ref.SubMain.SubContext.SubInfo.SubIDCard.SubPhotoList.SubPhotoAll.btn.onAction = on_submain_subcontext_subinfo_subidcard_subphotolist_subphotoall_click
	Ref.SubMain.SubContext.SubInfo.SubBankCard.SubPhotoList.SubPhotoFront.btn.onAction = on_submain_subcontext_subinfo_subbankcard_subphotolist_subphotofront_click
	Ref.SubMain.SubContext.SubInfo.SubBankCard.SubPhotoList.SubPhotoBack.btn.onAction = on_submain_subcontext_subinfo_subbankcard_subphotolist_subphotoback_click
	Ref.SubMain.SubContext.btnEnter.onAction = on_submain_subcontext_btnenter_click
	Ref.SubTop.btnBack.onAction = on_subtop_btnback_click
	--!*以上：自动注册的回调函数*--
end

local function init_logic()
	local UserInfo = UI_DATA.WNDRegist.UserInfo
	local Ref_Info = Ref.SubMain.SubContext.SubInfo
	Ref_Info.SubContact.SubPhone.lbText.text = UserInfo.phone
	UserInfo.PhotoList = {}
	---- test ----
	UIMGR.get_photo(Ref.SubMain.SubContext.SubIcon.spIcon,"1.png",function ( succ, name, image )
		if succ then
			if UI_DATA.WNDRegist.UserInfo == nil then UI_DATA.WNDRegist.UserInfo = {} end
			local UserInfo = UI_DATA.WNDRegist.UserInfo
			if UserInfo.PhotoList == nil then UserInfo.PhotoList = {} end
			UserInfo.PhotoList[1] = image
		else
			UserInfo.PhotoList[1] = nil
		end
	end)
	UIMGR.get_photo(Ref.SubMain.SubContext.SubInfo.SubIDCard.SubPhotoList.SubPhotoFront.spImage,"1.png",function ( succ, name, image )
		if succ then
			if UI_DATA.WNDRegist.UserInfo == nil then UI_DATA.WNDRegist.UserInfo = {} end
			local UserInfo = UI_DATA.WNDRegist.UserInfo
			if UserInfo.PhotoList == nil then UserInfo.PhotoList = {} end
			UserInfo.PhotoList[2] = image
		else
			UserInfo.PhotoList[2] = nil
		end
	end)
	UIMGR.get_photo(Ref.SubMain.SubContext.SubInfo.SubIDCard.SubPhotoList.SubPhotoBack.spImage,"1.png",function ( succ, name, image )
		if succ then
			if UI_DATA.WNDRegist.UserInfo == nil then UI_DATA.WNDRegist.UserInfo = {} end
			local UserInfo = UI_DATA.WNDRegist.UserInfo
			if UserInfo.PhotoList == nil then UserInfo.PhotoList = {} end
			UserInfo.PhotoList[3] = image
		else
			UserInfo.PhotoList[3] = nil
		end
	end)
	UIMGR.get_photo(Ref.SubMain.SubContext.SubInfo.SubIDCard.SubPhotoList.SubPhotoAll.spImage,"1.png",function ( succ, name, image )
		if succ then
			if UI_DATA.WNDRegist.UserInfo == nil then UI_DATA.WNDRegist.UserInfo = {} end
			local UserInfo = UI_DATA.WNDRegist.UserInfo
			if UserInfo.PhotoList == nil then UserInfo.PhotoList = {} end
			UserInfo.PhotoList[4] = image
		else
			UserInfo.PhotoList[4] = nil
		end
	end)
	UIMGR.get_photo(Ref.SubMain.SubContext.SubInfo.SubBankCard.SubPhotoList.SubPhotoFront.spImage,"1.png",function ( succ, name, image )
		if succ then
			if UI_DATA.WNDRegist.UserInfo == nil then UI_DATA.WNDRegist.UserInfo = {} end
			local UserInfo = UI_DATA.WNDRegist.UserInfo
			if UserInfo.PhotoList == nil then UserInfo.PhotoList = {} end
			UserInfo.PhotoList[5] = image
		else
			UserInfo.PhotoList[5] = nil
		end
	end)
	UIMGR.get_photo(Ref.SubMain.SubContext.SubInfo.SubBankCard.SubPhotoList.SubPhotoBack.spImage,"1.png",function ( succ, name, image )
		if succ then
			if UI_DATA.WNDRegist.UserInfo == nil then UI_DATA.WNDRegist.UserInfo = {} end
			local UserInfo = UI_DATA.WNDRegist.UserInfo
			if UserInfo.PhotoList == nil then UserInfo.PhotoList = {} end
			UserInfo.PhotoList[6] = image
		else
			UserInfo.PhotoList[6] = nil
		end
	end)
	-- UIMGR.get_photo(Ref.SubMain.SubContext.SubInfo.SubBankCard.SubPhotoAll.spImage,"1.png")

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

