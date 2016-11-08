--
-- @file    ui/schedule/lc_wndsupdatauploadphoto.lua
-- @authors zl
-- @date    2016-11-09 02:39:46
-- @desc    WNDSupDataUpLoadPhoto
--

local ipairs, pairs
    = ipairs, pairs
local libugui = require "libugui.cs"
local libunity = require "libunity.cs"
local UIMGR = MERequire "ui/uimgr"
local Ref
--!*以下：自动生成的回调函数*--

local function on_subtop_btnclean_click(btn)
	
end

local function on_subtop_btnback_click(btn)
	UIMGR.close_window(Ref.root)
end

local function on_subimgs_btnimg1_click(btn)
	
end

local function on_subimgs_btnimg2_click(btn)
	
end

local function on_subimgs_btnimg3_click(btn)
	
end

local function on_subimgs_btnimg4_click(btn)
	
end

local function on_subimgs_btnimg5_click(btn)
	
end

local function on_save_click(btn)
	
end



local function init_view()
	Ref.SubTop.btnClean.onAction = on_subtop_btnclean_click
	Ref.SubTop.btnBack.onAction = on_subtop_btnback_click
	Ref.SubImgs.btnImg1.onAction = on_subimgs_btnimg1_click
	Ref.SubImgs.btnImg2.onAction = on_subimgs_btnimg2_click
	Ref.SubImgs.btnImg3.onAction = on_subimgs_btnimg3_click
	Ref.SubImgs.btnImg4.onAction = on_subimgs_btnimg4_click
	Ref.SubImgs.btnImg5.onAction = on_subimgs_btnimg5_click
	Ref.Save.onAction = on_save_click
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

