--
-- @file    ui/user/lc_wndsuggest.lua
-- @authors ckxz
-- @date    2016-08-01 15:13:53
-- @desc    WNDSuggest
--

local ipairs, pairs
    = ipairs, pairs
local libugui = require "libugui.cs"
local libunity = require "libunity.cs"
local UIMGR = MERequire "ui/uimgr"
local NW = MERequire "network/networkmgr"
local DY_DATA = MERequire "datamgr/dydata.lua"
local UI_DATA = MERequire "datamgr/uidata.lua"
local Ref

local id

local function on_set_end(Ret)
	if Ret.ret == 1 then
		UIMGR.close_window(Ref.root)
	end
end
--!*以下：自动生成的回调函数*--

local function on_submain_sub1_tgltoggle_change(tgl)
	if tgl.value == true then id = 1 end
end

local function on_submain_sub2_tgltoggle_change(tgl)
	if tgl.value == true then id = 2 end
end

local function on_submain_sub3_tgltoggle_change(tgl)
	if tgl.value == true then id = 3 end
end

local function on_submain_sub4_tgltoggle_change(tgl)
	if tgl.value == true then id = 4 end
end

local function on_submain_sub5_tgltoggle_change(tgl)
	if tgl.value == true then id = 5 end
end

local function on_submain_btnsubmit_click(btn)
	if id == nil then
		_G.UI.Toast:make(nil, "未选择"):show()
		return
	end
    local context = Ref.SubMain.SubInput.inpInput.text
	local nm = NW.msg("USER.CS.FEEDVACK")
	nm:writeU32(DY_DATA.User.id)
	nm:writeU32(id)
	nm:writeString(context)
	NW.send(nm)
end

local function on_subtop_btnback_click(btn)
	UIMGR.close_window(Ref.root)
end

local function init_view()
	Ref.SubMain.Sub1.tglToggle.onAction = on_submain_sub1_tgltoggle_change
	Ref.SubMain.Sub2.tglToggle.onAction = on_submain_sub2_tgltoggle_change
	Ref.SubMain.Sub3.tglToggle.onAction = on_submain_sub3_tgltoggle_change
	Ref.SubMain.Sub4.tglToggle.onAction = on_submain_sub4_tgltoggle_change
	Ref.SubMain.Sub5.tglToggle.onAction = on_submain_sub5_tgltoggle_change
	Ref.SubMain.btnSubmit.onAction = on_submain_btnsubmit_click
	Ref.SubTop.btnBack.onAction = on_subtop_btnback_click
	--!*以上：自动注册的回调函数*--
end

local function init_logic()
	NW.subscribe("USER.SC.FEEDVACK", on_set_end)
	Ref.SubMain.SubInput.inpInput.text = ""
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
	NW.unsubscribe("USER.SC.FEEDVACK", on_set_end)
end

local P = {
	start = start,
	update_view = update_view,
	on_recycle = on_recycle,
}
return P

