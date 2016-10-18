--
-- @file    ui/home/lc_frmhome.lua
-- @authors xingweizhen
-- @date    2015-12-17 12:26:50
-- @desc    FRMHome
--

local ipairs, pairs, tostring
    = ipairs, pairs, tostring
local libugui = require "libugui.cs"
local libunity = require "libunity.cs"
local libasset = require "libasset.cs"
local UIMGR = MERequire "ui/uimgr"
local UI_DATA = MERequire "datamgr/uidata"
local DY_DATA = MERequire "datamgr/dydata"

local Ref
local m_BeginTime, m_BeginPos

local function on_back_loaded(o, p)
	libasset.LimitAssetBundle("RawImage", 2)	
end

local function on_hologram_loaded(o, p)
	if libunity.IsActive(Ref.root) then
		local go = libunity.NewChild("/UIROOT/ROLE", o)
		local anchor = libunity.FindGameObject(Ref.SubBack.SubRight.btnGirls, "Anchor")
		libugui.Follow(go, anchor, 20)

		-- 加载头像
		libunity.NewChild("/AvatarCam/Anchor", "Models/Other/Assistant/Assistant")
	end
end

local function on_home_leftfx_loaded(o)
	if libunity.IsActive(Ref.root) then
		local go = libunity.NewChild(Ref.SubBack.SubLeft.root, o)
		libunity.SetSibling(go, 0)
	end
end

local function load_uiback(go, name, onLoaded)
	local imagePath = string.format("RawImage/%s/%s", name, name)
	go:GetComponent("UISprite"):Load(imagePath, onLoaded, nil)
end

local function init_player()
	local SubPlayer, Player = Ref.SubPlayer, DY_DATA.Player

	SubPlayer.spFrame.spriteName = "General/"..Player.frame
	--SubPlayer.spIcon.spritePath = "Atlas/RoleIcon/"..Player.icon
	SubPlayer.lbName.text = Player.name
	SubPlayer.lbLevel:SetFormatArgs(Player.level)
	libugui.SetText(SubPlayer.btnVip, string.format("VIP %d",  Player.Vip.level))
end

local function show_1st_page()
	UIMGR.back = "bg_homeleft"
	local Vector3 = import("UnityEngine.Vector3")
	libugui.DOTween("TweenPosition", Ref.SubBack.root, nil, Vector3.zero, 0.3)
end

local function show_2nd_page()
	UIMGR.back = "bg_homeright"
	local Vector3 = import("UnityEngine.Vector3")
	libugui.DOTween("TweenPosition", Ref.SubBack.root, nil, Vector3.new(-1280, 0, 0), 0.3)
end

--!*以下：自动生成的回调函数*--

local function on_subback_subleft_btnwar_click(btn)
	-- UIMGR.create_window("UI/FRMWar")
	UIMGR.create_window("UI/WNDChapter")
end

local function on_subback_subleft_btnweapon_click(btn)
	UIMGR.create_window("UI/WNDWeaponList")
end

local function on_subback_subleft_btnclothes_click(btn)
	UIMGR.create_window("UI/WNDEquipCenter")
end

local function on_subback_subleft_btnright_click(btn)	
	show_2nd_page()
end

local function on_subback_subleft_btnchallenge_click(btn)
	UIMGR.create_window("UI/WNDChallenge")
end

local function on_subback_subright_btngirls_click(btn)	
	local UI_DATA_WNDRoleView = UI_DATA.WNDRoleView
	UI_DATA_WNDRoleView.remote = nil
	UI_DATA_WNDRoleView.List = DY_DATA.get_role_list()
	UI_DATA_WNDRoleView.Current = UI_DATA_WNDRoleView.List[1]
	UIMGR.create_window("UI/WNDRoleView")
end

local function on_subback_subright_btntech_click(btn)
	UIMGR.create_window("UI/WNDTech")
end

local function on_subback_subright_btnzombie_click(btn)
	
end

local function on_subback_subright_btnmine_click(btn)
	UIMGR.create_window("UI/WNDMine")
end

local function on_subback_subright_btnleft_click(btn)	
	show_1st_page()	
end

local function on_subback_subright_btnpvp_click(btn)
	UIMGR.create_window("UI/WNDPvpHall")
end

local function on_subplayer_click(btn)
	UIMGR.create("UI/WNDPlayerSet")
end

local function on_subplayer_btnvip_click(btn)
	
end

local function on_subtr_btnsignin_click(btn)
	
end

local function on_subtr_btnactive_click(btn)
	
end

local function on_subtr_btnmail_click(btn)
	UIMGR.create("UI/WNDMailbox")
end

local function on_subtr_btnrecharge_click(btn)
	
end

local function on_subtr_btnmall_click(btn)
	
end

local function on_subbl_btninventory_click(btn)
	UIMGR.create_window("UI/WNDInventory")
end

local function on_subbl_btnguild_click(btn)
	
end

local function on_subbl_btntask_click(btn)
	-- UI_DATA.WNDTask.Filter = {1}
	UIMGR.create_window("UI/WNDTask",11)
end

local function on_tglguide_change(tgl)
	local Vector2 = import("UnityEngine.Vector2")
	if tgl.value then
		libugui.DOTween("TweenSize", Ref.SubBL.root, nil, Vector2(400, 64), 0.2, "OutBack")
	else
		libugui.DOTween("TweenSize", Ref.SubBL.root, nil, Vector2(-110, 64), 0.2)
	end
end

local function on_begin_drag(evt)
	m_BeginTime = import("UnityEngine.Time").time
	m_BeginPos = evt.eventData.position
end

local function on_end_drag(evt)
	local delta = import("UnityEngine.Time").time - m_BeginTime
	local distance = evt.eventData.position.x - m_BeginPos.x
	local speed = distance / delta
	if speed > 3000 then
		show_1st_page()
	elseif speed < -3000 then
		show_2nd_page()
	end
end

local function init_view()
	Ref.SubBack.SubLeft.btnWar.onAction = on_subback_subleft_btnwar_click
	Ref.SubBack.SubLeft.btnWeapon.onAction = on_subback_subleft_btnweapon_click
	Ref.SubBack.SubLeft.btnClothes.onAction = on_subback_subleft_btnclothes_click
	Ref.SubBack.SubLeft.btnRight.onAction = on_subback_subleft_btnright_click
	Ref.SubBack.SubLeft.btnChallenge.onAction = on_subback_subleft_btnchallenge_click
	Ref.SubBack.SubRight.btnGirls.onAction = on_subback_subright_btngirls_click
	Ref.SubBack.SubRight.btnTech.onAction = on_subback_subright_btntech_click
	Ref.SubBack.SubRight.btnZombie.onAction = on_subback_subright_btnzombie_click
	Ref.SubBack.SubRight.btnMine.onAction = on_subback_subright_btnmine_click
	Ref.SubBack.SubRight.btnLeft.onAction = on_subback_subright_btnleft_click
	Ref.SubBack.SubRight.btnPvp.onAction = on_subback_subright_btnpvp_click
	Ref.SubPlayer.btn.onAction = on_subplayer_click
	Ref.SubPlayer.btnVip.onAction = on_subplayer_btnvip_click
	Ref.SubTR.btnSignin.onAction = on_subtr_btnsignin_click
	Ref.SubTR.btnActive.onAction = on_subtr_btnactive_click
	Ref.SubTR.btnMail.onAction = on_subtr_btnmail_click
	Ref.SubTR.btnRecharge.onAction = on_subtr_btnrecharge_click
	Ref.SubTR.btnMall.onAction = on_subtr_btnmall_click
	Ref.SubBL.btnInventory.onAction = on_subbl_btninventory_click
	Ref.SubBL.btnGuild.onAction = on_subbl_btnguild_click
	Ref.SubBL.btnTask.onAction = on_subbl_btntask_click
	Ref.tglGuide.onAction = on_tglguide_change
	--!*以上：自动注册的回调函数*--

	Ref.SubPlayer.lbLevel.textFormat = "{0}"

	local evt = Ref.SubBack.root:GetComponent("UIEventTrigger")
	evt.onBeginDrag = on_begin_drag
	evt.onEndDrag = on_end_drag

	UIMGR.back = "bg_homeleft"
end

local function init_logic()
	if table.void(DY_DATA.User) then MERequire "datamgr/localdata.lua" end

	_G.PKG["ui/home/barassets"].show({3, 4, 2})

	libunity.SetParent(Ref.SubBack.root, "UIROOT/LayCanvas", false)

	UIMGR.hide_uiback()
	load_uiback(Ref.SubBack.SubLeft.root, "home01")
	load_uiback(Ref.SubBack.SubRight.root, "home02", on_back_loaded)

	libunity.AddChild(nil, "UI/AvatarCam")
	
	local GoType = import("UnityEngine.GameObject").GetType()
	libasset.LoadAsync(nil, "Models/Other/Assistant/", true)
	libasset.LoadAsync(GoType, "Models/Other/Hologram/Hologram", true, on_hologram_loaded)
	libasset.LoadAsync(GoType, "UIFX/Home/zhucheng", true, on_home_leftfx_loaded)
	libasset.LoadAsync(nil, "RawImage/bg_homeleft/", true)
	libasset.LoadAsync(nil, "RawImage/bg_homeright/", true)

	init_player()
end

local function update_view()	
	
end

local function start(self)
	if Ref == nil or Ref.root ~= self then
		Ref = libugui.GenLuaTable(self, "root")
		init_view()
	end
	init_logic()
end

local function on_recycle()
	_G.PKG["ui/home/barassets"].hide()

	libunity.SetParent(Ref.SubBack.root, Ref.root, false)
	libunity.Delete("/UIROOT/ROLE/Hologram")
	libunity.Delete("/AvatarCam/Anchor/Assistant")	
	libunity.Destroy("/AvatarCam")

	local fxL = libunity.FindGameObject(Ref.SubBack.SubLeft.root, "zhucheng")
	libunity.Delete(fxL)
end

local P = {
	start = start,
	update_view = update_view,
	on_recycle = on_recycle,
}
return P

