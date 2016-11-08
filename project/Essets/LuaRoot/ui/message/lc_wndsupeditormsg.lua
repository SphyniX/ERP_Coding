--
-- @file    ui/message/lc_wndsupeditormsg.lua
-- @authors cks
-- @date    2016-11-03 15:10:35
-- @desc    WNDSupEditorMsg
--

local ipairs, pairs
    = ipairs, pairs
local libugui = require "libugui.cs"
local libunity = require "libunity.cs"
local UIMGR = MERequire "ui/uimgr"
local UI_DATA = MERequire "datamgr/uidata.lua"
local Ref
--!*以下：自动生成的回调函数*--

local function on_subtop_btnnext_click(btn)
	if Ref.MsgInput.text~=nil and Ref.MsgInput.text~="" then
	UI_DATA.WNDSupEditorMsg.InputText=Ref.MsgInput.text

	print("--------"..Ref.MsgInput.text)

	UIMGR.create_window("UI/WNDSupReceiveMsg")
	else
	_G.UI.Toast:make(nil, "请输入内容"):show()
	end
end

local function on_subtop_btnprevious_click(btn)
print("关闭UI/WNDSupEditorMsg ")
	UIMGR.create_window("UI/WNDSupMsg")
end

local function init_view()
	Ref.SubTop.btnNext.onAction = on_subtop_btnnext_click
	Ref.SubTop.btnPrevious.onAction = on_subtop_btnprevious_click
	--!*以上：自动注册的回调函数*--
end

local function init_logic()
Ref.MsgInput.text=""
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

