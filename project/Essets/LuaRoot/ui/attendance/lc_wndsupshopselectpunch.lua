--
-- @file    ui/attendance/lc_wndsupshopselectpunch.lua
-- @authors cks
-- @date    2016-11-13 23:50:28
-- @desc    WNDSupshopSelectPunch
--

local ipairs, pairs
    = ipairs, pairs
local libugui = require "libugui.cs"
local libunity = require "libunity.cs"
local UIMGR = MERequire "ui/uimgr"
local UI_DATA = MERequire "datamgr/uidata.lua"
local DY_DATA = MERequire "datamgr/dydata.lua"
local LOGIN = MERequire "libmgr/login.lua"
local NW = MERequire "network/networkmgr"
local Ref
local state
local typeid
local IfcanPunch
--!*以下：自动生成的回调函数*--
local function on_upload_photo_callback( Ret )
	-- body
	if Ret.ret == 1 then
		IfcanPunch = true
		_G.UI.Toast:make(nil, "图片上传成功,可以打卡"):show()
	else
		_G.UI.Toast:make(nil, "图片上传失败，请重新拍摄"):show()
		UIMGR.load_photo( tex, "nil.png")
	end
	
end


local function on_subtop_back_click(btn)
	UIMGR.close_window(Ref.root)
end

local function on_submain_btnsubmit_click(btn)
	if IfcanPunch then
		
		local state = tonumber(DY_DATA.WNDsupShopSelectPunch.state)+2
		print("督导考勤" .. "______State is ______  " .. state .. "  _ ___________")
		local nm = NW.msg("ATTENCE.CS.PHUNCH")
		nm:writeU32(DY_DATA.WNDsupShopSelect.StoreId)			
		nm:writeU32(state)
		NW.send(nm)
		UIMGR.close_window(Ref.root)
	else
		_G.UI.Toast:make(nil, "请拍摄图片"):show()
	end
end


local function on_submain_btnimg_click(btn)
	print("拍图片")
	--local name = "upload".. btn.name:sub(4) .. "png"
	local tex = Ref.SubMain.spTex
	-- local tex = Ent.spPhoto
		print("拍图片1------"..DY_DATA.WNDsupShopSelect.StoreId)
	UIMGR.on_sdk_take_photo("1.png", tex, function (succ, name, image)

		if DY_DATA.WNDsupShopSelectPunch.state==1 then
			typeid=7
		else
			typeid=8
		end
		print("--------------------"..DY_DATA.WNDsupShopSelect.StoreId.."????"..typeid)
		if succ then
			LOGIN.try_uploadphoto(DY_DATA.User.id,typeid,DY_DATA.WNDsupShopSelect.StoreId,image,on_upload_photo_callback)
		else
			PhotoName = nil
		end
	end)

	-----test ----
	UIMGR.load_photo( tex, "1.png",function (succ, name, image)

		if DY_DATA.WNDsupShopSelectPunch.state==1 then
			typeid=7
		else
			typeid=8
		end
		print("--------------------"..DY_DATA.WNDsupShopSelect.StoreId.."????"..typeid)
		if succ then
			LOGIN.try_uploadphoto(DY_DATA.User.id,typeid,DY_DATA.WNDsupShopSelect.StoreId,image,on_upload_photo_callback)
		else
			PhotoName = nil
		end
	end)
	-- UIMGR.load_photo("1.png",tex, function (succ, name, image)
	-- 	if succ then
	-- 		LOGIN.try_uploadphotoforreport(DY_DATA.User.id,image,on_upload_photo_callback)
	-- 	else
	-- 		-- PhotoList[i].image = nil
	-- 	end
	-- end)
end



local function on_store_punch()
	UIMGR.create_window("UI/WNDSupshopSelect")
	-- body
end 

local function init_view()
	Ref.SubTop.back.onAction = on_subtop_back_click
	Ref.SubMain.btnSubmit.onAction = on_submain_btnsubmit_click
	Ref.SubMain.btnImg.onAction = on_submain_btnimg_click
	IfcanPunch = false
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

