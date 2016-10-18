--
-- @file    ui/system/lc_wndmsglower.lua
-- @authors zl
-- @date    2016-08-29 00:12:36
-- @desc    WNDMsgLower
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
local LowerList, type, callback
--!*以下：自动生成的回调函数*--

local function on_submain_grp_ent_btnbutton_click(btn)
	local index = tonumber(btn.transform.parent.name:sub(4))
	local Lower = LowerList[index]
	if callback then callback( Ref.root, Lower) end
end

local function on_subtop_btnback_click(btn)
	UIMGR.close_window(Ref.root)
end

local function on_ui_init()
	LowerList = DY_DATA.get_lower_list(false, type)
	Ref.SubMain.Grp:dup(#LowerList, function (i, Ent, isNew)
		local Lower = LowerList[i]
		Ent.lbName.text = Lower.name
	end)
end

local function init_view()
	Ref.SubMain.Grp.Ent.btnButton.onAction = on_submain_grp_ent_btnbutton_click
	Ref.SubTop.btnBack.onAction = on_subtop_btnback_click
	UIMGR.make_group(Ref.SubMain.Grp, function (New, Ent)
		New.btnButton.onAction = Ent.btnButton.onAction
	end)
	--!*以上：自动注册的回调函数*--
end

local function init_logic()
	NW.subscribe("MESSAGE.SC.GETLOWER", on_ui_init)
	type = UI_DATA.WNDMsgLower.type
	UI_DATA.WNDMsgLower.type = nil
	callback = UI_DATA.WNDMsgLower.callback
	UI_DATA.WNDMsgLower.callback = nil

	if DY_DATA.LowerList == nil or next(DY_DATA.LowerList) == nil then
		local nm = NW.msg("MESSAGE.CS.GETLOWER")
		nm:writeU32(DY_DATA.User.id)
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
	NW.unsubscribe("MESSAGE.SC.GETLOWER", on_ui_init)
end

local P = {
	start = start,
	update_view = update_view,
	on_recycle = on_recycle,
}
return P

