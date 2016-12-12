--
-- @file    ui/user/lc_wndsupuser.lua
-- @authors zl
-- @date    2016-08-14 19:17:58
-- @desc    WNDSupUser
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


local function on_set_photo(Photolist)
	local nPhoto = 0
	local function on_http_photo_callback()
   		nPhoto = nPhoto + 1
   		if nPhoto >= #Photolist then
   		end
   	end
   	for i,v in ipairs(Photolist) do
   		print(v)
   		LOGIN.try_uploadphoto(DY_DATA.User.id, v.typeId, nil,v.image, on_http_photo_callback)
   	end
end

--!*以下：自动生成的回调函数*--

local function on_submain_subicon_btnchange_click(btn)
	print("投点击了 修改图像")
	UI_DATA.WNDShowPhoto.title = "头像"
	UI_DATA.WNDShowPhoto.tip = ""
	local User = DY_DATA.User
	UI_DATA.WNDShowPhoto.photolist = {
		[1] = { title = "头像", name = User.icon, typeId = 1, dl = true},
	}

	UI_DATA.WNDShowPhoto.callback = on_set_photo
   	UIMGR.create_window("UI/WNDSubmitPhoto")
end

local function on_submain_subotherphone_click(btn)
	print("投点击了 联系方式")
	UIMGR.create_window("UI/WNDContact")
end

local function on_submain_subpassword_click(btn)
	print("投点击了 修改密码")
	UIMGR.create_window("UI/WNDUserResetPassword")
end

local function on_submain_subsuggest_click(btn)
	print("投点击了 投诉建议")
	UIMGR.create_window("UI/WNDSuggest")
end

local function on_submain_about_click(btn)
	print("投点击了 关于")
	UIMGR.create_window("UI/WNDAbout")
end

local function on_submain_btnlogout_click(btn)
	_G.PKG["libmgr/login"].do_logout()
end

local function on_subbtm_spatt_click(btn)
-- ##	UIMGR.WNDStack:pop()
	UIMGR.create_window("UI/WNDSupAttendance")
end

local function on_subbtm_btnwork_click(btn)
-- ##	UIMGR.WNDStack:pop()
	UIMGR.create_window("UI/WNDSupWork")
end

local function on_subbtm_btnsch_click(btn)
-- ##	UIMGR.WNDStack:pop()
	UIMGR.create_window("UI/WNDSupSchedule")
end

local function on_subbtm_btnmsg_click(btn)
-- ##	UIMGR.WNDStack:pop()
	UIMGR.create_window("UI/WNDSupMsg")
end

local function on_submain_subaddress_click(btn)
	
end

local function on_submain_subphone_click(btn)
	UIMGR.create_window("UI/WNDUserChangePhone")
end

local function on_subtop_btnback_click(btn)
	_G.PKG["libmgr/login"].do_logout()
end

local function on_ui_init()
	local Ref_SubMain = Ref.SubMain
	
	local User = DY_DATA.User
	if User ~= nil and next(User) ~= nil then
		Ref.SubMain.SubIcon.lbName.text=User.name
	end



	--Ref_SubMain.SubAddress.lbText.text =_G.CFG.CityLib.get_city(User.cityid).name
	--Ref_SubMain.SubPhone.lbText.text = User.phone
	UIMGR.get_photo(Ref_SubMain.SubIcon.spIcon, User.icon)
end

local function init_view()
	Ref.SubMain.SubIcon.btnChange.onAction = on_submain_subicon_btnchange_click
	Ref.SubMain.SubOtherPhone.btn.onAction = on_submain_subotherphone_click
	Ref.SubMain.SubPassword.btn.onAction = on_submain_subpassword_click
	Ref.SubMain.SubSuggest.btn.onAction = on_submain_subsuggest_click
	Ref.SubMain.About.onAction = on_submain_about_click
	Ref.SubMain.btnLogout.onAction = on_submain_btnlogout_click
	Ref.SubBtm.spAtt.onAction = on_subbtm_spatt_click
	Ref.SubBtm.btnWork.onAction = on_subbtm_btnwork_click
	Ref.SubBtm.btnSch.onAction = on_subbtm_btnsch_click
	Ref.SubBtm.btnMsg.onAction = on_subbtm_btnmsg_click
	--!*以上：自动注册的回调函数*--
end

local function init_logic()
	UI_DATA.WNDMsgHint.state = true
	on_ui_init()
	NW.subscribe("USER.SC.GETUSERINFOR", on_ui_init)
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
	UI_DATA.WNDMsgHint.state = false
	NW.unsubscribe("USER.SC.GETUSERINFOR", on_ui_init)
end

local P = {
	start = start,
	update_view = update_view,
	on_recycle = on_recycle,
}
return P

