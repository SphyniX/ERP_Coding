--
-- @file    ui/schedule/lc_wndsupgoodanalysis.lua
-- @authors zl
-- @date    2016-11-09 01:38:01
-- @desc    WNDSupGoodAnalysis
--

local ipairs, pairs
    = ipairs, pairs
local libugui = require "libugui.cs"
local libunity = require "libunity.cs"
local UIMGR = MERequire "ui/uimgr"
local Ref
--!*以下：自动生成的回调函数*--

local function on_subtop_back_click(btn)
	
end

local function on_subcell_click(btn)
	
end

local function on_subcell1_click(btn)
	
end

local function init_view()
	Ref.SubTop.Back.onAction = on_subtop_back_click
	Ref.SubCell.btn.onAction = on_subcell_click
	Ref.SubCell1.btn.onAction = on_subcell1_click
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

