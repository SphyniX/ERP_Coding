--
-- @file    ui/schedule/lc_wndsetcomphoto.lua
-- @authors zl
-- @date    2016-10-17 08:12:17
-- @desc    WNDSetComPhoto
--

local ipairs, pairs
    = ipairs, pairs
local libugui = require "libugui.cs"
local libunity = require "libunity.cs"
local UIMGR = MERequire "ui/uimgr"
local UI_DATA = MERequire "datamgr/uidata.lua"
local Ref

local callback, PhotoList
--!*以下：自动生成的回调函数*--

local function on_submain_sptex_click(btn)

	local name = "upload.png"
	local tex = Ref.SubMain.spTex
	-- local tex = Ent.spPhoto
	UIMGR.on_sdk_take_photo(name, tex, function (succ, name, image)
		UI_DATA.WNDSubmitSchedule.PhotoList[UI_DATA.WNDSetComPhoto.id].name = name
	end)
end

local function on_submain_btnsave_click(btn)
	UIMGR.close_window(Ref.root)
end

local function on_subtop_btnclear_click(btn)
	Ref.SubMain.inpPrice.text = nil
	Ref.SubMain.inInfo.text = nil
end

local function on_subtop_btnback_click(btn)
	UIMGR.close_window(Ref.root)
end

local function init_view()
	Ref.SubMain.spTex.onAction = on_submain_sptex_click
	Ref.SubMain.btnSave.onAction = on_submain_btnsave_click
	Ref.SubTop.btnClear.onAction = on_subtop_btnclear_click
	Ref.SubTop.btnBack.onAction = on_subtop_btnback_click
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

