--
-- @file    ui/work/lc_wndsupworkselect.lua
-- @authors cks
-- @date    2016-11-09 05:05:37
-- @desc    WNDSupWorkSelect
--

local ipairs, pairs
    = ipairs, pairs
local libugui = require "libugui.cs"
local libunity = require "libunity.cs"
local UIMGR = MERequire "ui/uimgr"
local Ref
--!*以下：自动生成的回调函数*--

local function on_subbg_btndate_click(btn)
	UIMGR.create_window("UI/WNDSupWorkSelectPageMsg")
	UIMGR.close(Ref.root)
end

local function on_subbg_btntask_click(btn)
	UIMGR.create_window("UI/WNDSupWorkSelectShop")
	UIMGR.close(Ref.root) 
end

local function init_view()
	Ref.Subbg.btnDate.onAction = on_subbg_btndate_click
	Ref.Subbg.btnTask.onAction = on_subbg_btntask_click
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

