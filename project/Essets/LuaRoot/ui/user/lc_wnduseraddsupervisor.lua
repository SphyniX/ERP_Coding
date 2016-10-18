--
-- @file    ui/user/lc_wnduseraddsupervisor.lua
-- @authors zl
-- @date    2016-08-12 13:32:12
-- @desc    WNDUserAddSupervisor
--

local ipairs, pairs
    = ipairs, pairs
local libugui = require "libugui.cs"
local libunity = require "libunity.cs"
local UIMGR = MERequire "ui/uimgr"
local DY_DATA = MERequire "datamgr/dydata.lua"
local NW = MERequire "network/networkmgr"
local UI_DATA = MERequire "datamgr/uidata.lua"
local Ref

--!*以下：自动生成的回调函数*--

local function on_submain_btnbutton_click(btn)
	local name = Ref.SubMain.inpName.text
	local id = Ref.SubMain.inpID.text
	local nm = NW.msg("USER.CS.CONTRACT")
	nm:writeU32(DY_DATA.User.id)
	nm:writeU32(id)
	nm:writeString(name)
	NW.send(nm)
	UIMGR.close_window(Ref.root)
end

local function on_subtop_btnback_click(btn)
	UIMGR.close_window(Ref.root)
end

local function init_view()
	Ref.SubMain.btnButton.onAction = on_submain_btnbutton_click
	Ref.SubTop.btnBack.onAction = on_subtop_btnback_click
	--!*以上：自动注册的回调函数*--
end

local function init_logic()
	Ref.SubMain.inpID.text = ""
	Ref.SubMain.inpName.text = ""
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

