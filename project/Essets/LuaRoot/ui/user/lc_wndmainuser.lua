--
-- @file    ui/user/lc_wndmainuser.lua
-- @authors zl
-- @date    2016-08-12 10:23:25
-- @desc    WNDMainUser
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
	-- local nPhoto = 0
	local function on_http_photo_callback()
   		-- nPhoto = nPhoto + 1
   		-- if nPhoto >= #Photolist then
   		-- end
   	end
   	for i,v in ipairs(Photolist) do
   		LOGIN.try_uploadphoto(DY_DATA.User.id, v.typeId, nil, v.image, on_http_photo_callback)
   	end
end

local function on_set_idcard(Photolist, inp)
   	local nPhoto = 0
   	local PhotoListName = {}
   	if NW.connected() then
		local function on_http_photo_callback(Ret)
			table.insert(PhotoListName,{name = Ret.name})
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

local function on_submain_subicon_btnchange_click(btn)
	UI_DATA.WNDShowPhoto.title = "头像"
	UI_DATA.WNDShowPhoto.tip = ""
	local User = DY_DATA.User
	UI_DATA.WNDShowPhoto.photolist = {
		[1] = { title = "头像", name = User.icon, typeId = 1, dl = true},
	}

	UI_DATA.WNDShowPhoto.callback = on_set_photo
   	UIMGR.create_window("UI/WNDSubmitPhoto")
end

local function on_submain_btnboss_click(btn)
	UIMGR.create_window("UI/WNDUserSupervisorList")
end

local function on_submain_btninfo_click(btn)
	UIMGR.create_window("UI/WNDUserSetInfo")
end

local function on_submain_btnsuggest_click(btn)
	UIMGR.create_window("UI/WNDSuggest")
end

local function on_submain_btnabout_click(btn)
	UIMGR.create_window("UI/WNDAbout")
end

local function on_submain_btnlogout_click(btn)
	_G.PKG["libmgr/login"].do_logout()
end

local function on_subbtm_btnatt_click(btn)
-- ##	UIMGR.WNDStack:pop()
	UIMGR.create_window("UI/WNDMainAttendance")
end

local function on_subbtm_btnwork_click(btn)
-- ##	UIMGR.WNDStack:pop()
	UIMGR.create_window("UI/WNDMainWork")
end

local function on_subbtm_btnsch_click(btn)
-- ##	UIMGR.WNDStack:pop()
	UIMGR.create_window("UI/WNDMainSchedule")
end

local function on_subbtm_btnmsg_click(btn)
-- ##	UIMGR.WNDStack:pop()
	UIMGR.create_window("UI/WNDMainMsg")
end

local function on_set_red()
	if DY_DATA.SetRed then
		libunity.SetActive(Ref.SubBtm.SetRed,true)
	else
		libunity.SetActive(Ref.SubBtm.SetRed,false)
	end
end


local function on_ui_init()
	local Ref_SubMain = Ref.SubMain
	local User = DY_DATA.User
	Ref_SubMain.SubIcon.lbName.text = User.name
	-- Ref_SubMain.SubID.lbText.text = User.id
	-- Ref_SubMain.SubAddress.lbText.text =_G.CFG.CityLib.get_city(User.cityid).name
	-- Ref_SubMain.SubPhone.lbText.text = User.phone
	UIMGR.get_photo(Ref_SubMain.SubIcon.spIcon, User.icon)
	if DY_DATA.MsgList == nil or next(DY_DATA.MsgList) == nil then
		local nm = NW.msg("MESSAGE.CS.GETMESSAGELIST")
		nm:writeU32(DY_DATA.User.id)
		NW.send(nm)
	else
		on_set_red()
	end
end

local function init_view()
	Ref.SubMain.SubIcon.btnChange.onAction = on_submain_subicon_btnchange_click
	Ref.SubMain.btnBoss.onAction = on_submain_btnboss_click
	Ref.SubMain.btnInfo.onAction = on_submain_btninfo_click
	Ref.SubMain.btnSuggest.onAction = on_submain_btnsuggest_click
	Ref.SubMain.btnAbout.onAction = on_submain_btnabout_click
	Ref.SubMain.btnLogout.onAction = on_submain_btnlogout_click
	Ref.SubBtm.btnAtt.onAction = on_subbtm_btnatt_click
	Ref.SubBtm.btnWork.onAction = on_subbtm_btnwork_click
	Ref.SubBtm.btnSch.onAction = on_subbtm_btnsch_click
	Ref.SubBtm.btnMsg.onAction = on_subbtm_btnmsg_click
	--!*以上：自动注册的回调函数*--
end

local function init_logic()
	on_ui_init()
	NW.subscribe("USER.SC.GETUSERINFOR", on_ui_init)
	NW.subscribe("MESSAGE.SC.GETMESSAGELIST", on_set_red)
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

