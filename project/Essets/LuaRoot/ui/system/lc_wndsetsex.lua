--
-- @file    ui/system/lc_wndsetsex.lua
-- @authors zl
-- @date    2016-08-24 22:14:41
-- @desc    WNDSetSex
--

local ipairs, pairs
    = ipairs, pairs
local libugui = require "libugui.cs"
local libunity = require "libunity.cs"
local UIMGR = MERequire "ui/uimgr"
local UI_DATA = MERequire "datamgr/uidata.lua"
local Ref
--!*以下：自动生成的回调函数*--

local function on_btnback_click(btn)
	UIMGR.close(Ref.root)
end

local function on_submain_tglwoman_change(tgl)
	
end

local function on_submain_tglman_change(tgl)
	
end

local function on_subbtm_btncancle_click(btn)
	UIMGR.close(Ref.root)
end

local function on_subbtm_btnconfirm_click(btn)
	local callback = UI_DATA.WNDSetSex.on_call_back
	local sex = Ref.SubMain.tglWoman.value and 2 or 1
	if callback then callback(sex) end
	UIMGR.close(Ref.root)
end

local function init_view()
	Ref.btnBack.onAction = on_btnback_click
	Ref.SubMain.tglWoman.onAction = on_submain_tglwoman_change
	Ref.SubMain.tglMan.onAction = on_submain_tglman_change
	Ref.SubBtm.btnCancle.onAction = on_subbtm_btncancle_click
	Ref.SubBtm.btnConfirm.onAction = on_subbtm_btnconfirm_click
	--!*以上：自动注册的回调函数*--
end

local function init_logic()
	
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

