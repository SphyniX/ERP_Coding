--
-- @file    ui/schedule/lc_wndsupdataprogress.lua
-- @authors zl
-- @date    2016-11-09 02:52:40
-- @desc    WNDSupDataProgress
--

local ipairs, pairs
    = ipairs, pairs
local libugui = require "libugui.cs"
local libunity = require "libunity.cs"
local UIMGR = MERequire "ui/uimgr"
local Ref
--!*以下：自动生成的回调函数*--

local function on_subtop_back_click(btn)
	UIMGR.close_window(Ref.root)
end

local function on_submain_sell_click(btn)
	UIMGR.create_window("UI/WNDSupDataProgressSell")
end

local function on_submain_img_click(btn)
	UIMGR.create_window("UI/WNDSupDataProgerssStoreIMg")
end

local function on_submain_mechanism_click(btn)
	UIMGR.create_window("UI/WNDSupDataProgerssMechanism")
end

local function on_submain_taste_click(btn)
	UIMGR.create_window("UI/WNDSupDataProgerssTaste")
end

local function on_submain_tagive_click(btn)
	UIMGR.create_window("UI/WNDSupDataProgerssGive")
end

local function on_submain_comlist_click(btn)
	UIMGR.create_window("UI/WNDSupDataProgerssComList")
end

local function on_submain_msg_click(btn)
	UIMGR.create_window("UI/WNDSupDataProgerssMsg")
end

local function on_submain_matter_click(btn)
	UIMGR.create_window("UI/WNDSupDataProgerssMatter")
end

local function on_submain_matterinfo_click(btn)
	_G.UI.Toast:make(nil, "敬请期待"):show()
end




local function on_submain_shopnumber_click(btn)
	UIMGR.create_window("UI/")
end

local function init_view()
	Ref.SubTop.Back.onAction = on_subtop_back_click
	Ref.SubMain.Sell.onAction = on_submain_sell_click
	Ref.SubMain.Img.onAction = on_submain_img_click
	Ref.SubMain.Mechanism.onAction = on_submain_mechanism_click
	Ref.SubMain.Taste.onAction = on_submain_taste_click
	Ref.SubMain.TaGive.onAction = on_submain_tagive_click
	Ref.SubMain.ComList.onAction = on_submain_comlist_click
	Ref.SubMain.Msg.onAction = on_submain_msg_click
	Ref.SubMain.Matter.onAction = on_submain_matter_click
	Ref.SubMain.MatterInfo.onAction = on_submain_matterinfo_click
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

