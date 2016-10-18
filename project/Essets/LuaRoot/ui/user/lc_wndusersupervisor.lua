--
-- @file    ui/user/lc_wndusersupervisor.lua
-- @authors ckxz
-- @date    2016-08-01 15:40:27
-- @desc    WNDUserSupervisor
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

local superId
--!*以下：自动生成的回调函数*--

local function on_submain_subinfo_subphone_btncall_click(btn)
	
end

local function on_subtop_btnback_click(btn)
	UIMGR.close_window(Ref.root)
end

local function on_ui_init()
	local PersonList = DY_DATA.PersonList
	print(JSON:encode(PersonList))
	if PersonList == nil then return end
	local Person = PersonList[superId]
	print(superId)
	print(JSON:encode(Person))
	if Person == nil then return end
	UIMGR.get_photo(Ref.SubMain.spIcon, Person.icon)
	local Ref_SubMain_SubInfo = Ref.SubMain.SubInfo
	Ref.SubMain.lbName.text = Person.name
	Ref_SubMain_SubInfo.SubPhone.lbText.text = Person.phone
	Ref_SubMain_SubInfo.SubWechat.lbText.text = Person.wechat
	Ref_SubMain_SubInfo.SubQQ.lbText.text = Person.qq
	Ref_SubMain_SubInfo.SubEmail.lbText.text = Person.email
	
end

local function init_view()
	Ref.SubMain.SubInfo.SubPhone.btnCall.onAction = on_submain_subinfo_subphone_btncall_click
	Ref.SubTop.btnBack.onAction = on_subtop_btnback_click
	--!*以上：自动注册的回调函数*--
end

local function init_logic()
	NW.subscribe("USER.SC.GETINFOR", on_ui_init)
	superId = UI_DATA.WNDUserSupervisor.superId

	local PersonList = DY_DATA.PersonList
	if PersonList == nil or next(PersonList) == nil or PersonList[superId] == nil then
		local nm = NW.msg("USER.CS.GETINFOR")
		nm:writeU32(superId)
		NW.send(nm)
		return
	end

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
	NW.unsubscribe("USER.SC.GETINFOR", on_ui_init)
end

local P = {
	start = start,
	update_view = update_view,
	on_recycle = on_recycle,
}
return P

