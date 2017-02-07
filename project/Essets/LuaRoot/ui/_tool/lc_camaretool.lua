--
-- @file    ui/_tool/lc_camaretool.lua
-- @authors cks
-- @date    2016-12-17 13:53:10
-- @desc    CamareTool
--

local ipairs, pairs
    = ipairs, pairs
local libugui = require "libugui.cs"
local libunity = require "libunity.cs"
local UIMGR = MERequire "ui/uimgr"
local Ref
--!*以下：自动生成的回调函数*--

local function on_submain_btnbutton_click(btn)
	--FileInfo.fileWrite(FileInfo.path.."asdassad/te.txt","wwwwww")

	-- if _G.CamareToolClass ~= nil then
	-- 	_G.CamareToolClass.pictrueSelect(2)
	-- else
	-- 	_G.UI.Toast:make(nil, "CamareToolClass未初始化"):show()
	-- end
	-- UIMGR.colsewindow(Ref.root)
end

local function on_submain_btncamare_click(btn)

	--FileInfo.createDirectory(FileInfo.path.."test1/ddd");

		-- 	if _G.CamareToolClass ~= nil then
		-- 	_G.CamareToolClass.pictrueSelect(1)
		-- else
		-- 	_G.UI.Toast:make(nil, "CamareToolClass未初始化"):show()
		-- end
end

local function init_view()
	Ref.SubMain.btnButton.onAction = on_submain_btnbutton_click
	Ref.SubMain.btnCamare.onAction = on_submain_btncamare_click
	--!*以上：自动注册的回调函数*--
end

local function init_logic()
	--libugui.GetSetResourcesPic(Ref.spTex,"default1",nil)
	--libugui.GetSetAssetsPic(Ref.spTex,"UI/default1",nil)
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

