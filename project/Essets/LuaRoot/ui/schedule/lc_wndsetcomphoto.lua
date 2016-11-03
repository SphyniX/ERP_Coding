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
local PhotoId
local ComListForUpdate
local callback, PhotoList
local PhotoName 
--!*以下：自动生成的回调函数*--

local function on_submain_sptex_click(btn)

	local name = "upload".. btn.name:sub(4) .. "png"
	local tex = Ref.SubMain.spTex
	-- local tex = Ent.spPhoto
	UIMGR.on_sdk_take_photo(name, tex, function (succ, name, image)
		if succ then
			PhotoName = name
		else
			PhotoName = nil
		end
	end)
end

local function on_submain_btnsave_click(btn)
	ComListForUpdate = {}
	if PhotoName == nil then 
		PhotoName = ""
	end
	ComListForUpdate = {
		id = UI_DATA.WNDSetComPhoto.id,
		price = Ref.SubMain.inpPrice.text,
		info = Ref.SubMain.inInfo.text,
		name = PhotoName,
	}
	-- table.insert(ComListForUpdate,{id = id , price = price , info = info , name = name})
	if UI_DATA.WNDSubmitSchedule.ComList == nil then
		UI_DATA.WNDSubmitSchedule.ComList = {}
	end
	table.insert(UI_DATA.WNDSubmitSchedule.ComList,ComListForUpdate)
	Ref.SubMain.inpPrice.text = nil
	Ref.SubMain.inInfo.text = nil
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
	local ComList = UI_DATA.WNDSubmitSchedule.ComList
	ComListForUpdate = {}
	if ComList ~= nil then 
		print("ComList is :" .. JSON:encode(ComList))
		print("WNDSetComPhoto.id is :" .. UI_DATA.WNDSetComPhoto.id)
		for i=1,#ComList do
			if ComList[i].id == UI_DATA.WNDSetComPhoto.id then
				ComListForUpdate = ComList[i]
			end
		end
	end
	print("ComListForUpdate Now is :" .. JSON:encode(ComListForUpdate))
	if ComListForUpdate ~= nil then 
		Ref.SubMain.inpPrice.text = ComListForUpdate.price
		Ref.SubMain.inInfo.text = ComListForUpdate.info
		-- UIMGR.load_photo(Ref.SubMain.spTex, ComListForUpdate.name, function (succ, name, image)
		-- 		if succ then
		-- 			PhotoList[1].image = image
		-- 		else
		-- 			PhotoList[1].image = nil
		-- 		end
		-- 	end)
	end


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
	init_view()
end

local function on_recycle()
	
end

local P = {
	start = start,
	update_view = update_view,
	on_recycle = on_recycle,
}
return P

