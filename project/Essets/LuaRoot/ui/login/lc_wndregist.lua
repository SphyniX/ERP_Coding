--
-- @file    ui/login/lc_wndregist.lua
-- @authors ckxz
-- @date    2016-07-04 14:48:22
-- @desc    WNDRegist
--

local ipairs, pairs
    = ipairs, pairs
local libugui = require "libugui.cs"
local libunity = require "libunity.cs"
local UIMGR = MERequire "ui/uimgr"
local LOGIN = MERequire "libmgr/login.lua"
local UI_DATA = MERequire "datamgr/uidata.lua"
local Ref

local City

local function on_bind_supervisored(Ret)
	if Ret.ret == 1 then
		UI_DATA.WNDBindPhone.type = 1
		UIMGR.create_window("UI/WNDBindPhone")
	end
end
--!*以下：自动生成的回调函数*--

local function on_submain_subinfo_subcity_click(btn)
	UI_DATA.WNDSelectCity.callBack = function (city)
		City = city
		Ref.SubMain.SubInfo.SubCity.lbText.text = City.name
	end
	UIMGR.create_window("UI/WNDSelectProvince")
end

local function on_submain_btnenter_click(btn)
	local inpSupname = Ref.SubMain.SubInfo.SubSupervisor.inpSupname.text
	local inpName = Ref.SubMain.SubInfo.SubName.inpName.text
	local inpCode = Ref.SubMain.SubInfo.SubCode.inpCode.text

	local UI_DATA = MERequire "datamgr/uidata.lua"
	if UI_DATA.WNDRegist.UserInfo == nil then UI_DATA.WNDRegist.UserInfo = {} end
	local UserInfo = UI_DATA.WNDRegist.UserInfo
	UserInfo.name = inpName
	UserInfo.city = City.id
	UserInfo.supname = inpCode
	LOGIN.try_bind_supervisor(inpSupname, inpName, inpCode, on_bind_supervisored)
end

local function on_subtop_btnback_click(btn)
	UIMGR.close_window(Ref.root)
end

local function on_ui_init()
	print("on_ui_init"..JSON:encode(City).."end")
	Ref.SubMain.SubInfo.SubCity.lbText.text = City and City.name or "选择所在城市"
end

local function init_view()
	Ref.SubMain.SubInfo.SubCity.btn.onAction = on_submain_subinfo_subcity_click
	Ref.SubMain.btnEnter.onAction = on_submain_btnenter_click
	Ref.SubTop.btnBack.onAction = on_subtop_btnback_click
	--!*以上：自动注册的回调函数*--
end

local function init_logic()
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
	
end

local P = {
	start = start,
	update_view = update_view,
	on_recycle = on_recycle,
}
return P

