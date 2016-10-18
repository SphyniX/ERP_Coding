--
-- @file    ui/user/lc_wnduserinfo.lua
-- @authors ckxz
-- @date    2016-08-01 11:26:42
-- @desc    WNDUserInfo
--

local ipairs, pairs
    = ipairs, pairs
local libugui = require "libugui.cs"
local libunity = require "libunity.cs"
local UIMGR = MERequire "ui/uimgr"
local DY_DATA = MERequire "datamgr/dydata.lua"
local TEXT = _G.ENV.TEXT
local NW = MERequire "network/networkmgr"
local UI_DATA = MERequire "datamgr/uidata.lua"
local Ref
--!*以下：自动生成的回调函数*--

local function on_subtop_btnback_click(btn)
	UIMGR.close_window(Ref.root)
end

local function on_subtop_btnsave_click(btn)
	local height = Ref.SubMain.SubHeight.inpInput.text
	local weight = Ref.SubMain.SubWeight.inpInput.text
	local User = DY_DATA.User
	if height ~= User.height or weight ~= User.weight then
		local nm = NW.msg("USER.CS.UPDATEINF")
		nm:writeU32(DY_DATA.User.id)
		nm:writeU32(DY_DATA.User.sex)
		nm:writeU32(DY_DATA.User.age)
		nm:writeU32(weight)
		nm:writeU32(height)
		NW.send(nm)
	end
	UIMGR.close_window(Ref.root)
end

local function init_view()
	Ref.SubTop.btnBack.onAction = on_subtop_btnback_click
	Ref.SubTop.btnSave.onAction = on_subtop_btnsave_click
	--!*以上：自动注册的回调函数*--
end

local function init_logic()
	local User = DY_DATA.User
	Ref.SubMain.SubSex.lbText.text = TEXT.Sex[User.sex]
	Ref.SubMain.SubAge.lbText.text = User.age
	Ref.SubMain.SubHeight.inpInput.text = User.height
	Ref.SubMain.SubWeight.inpInput.text = User.weight

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

