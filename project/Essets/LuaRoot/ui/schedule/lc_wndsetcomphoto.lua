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
local DY_DATA = MERequire "datamgr/dydata.lua"
local LOGIN = MERequire "libmgr/login.lua"
local Ref
local PhotoId
local ComListForUpdate
local callback, PhotoList
local PhotoName
--!*以下：自动生成的回调函数*--



local function on_upload_photo_callback( Ret )
	-- body
	if Ret.ret == 1 then
		PhotoName = Ret.photoid[1]
		_G.UI.Toast:make(nil, "成功"):show()
	end
end

 local function on_take_photo_call_back(image)

	LOGIN.try_uploadphotoforreport(DY_DATA.User.id,image,on_upload_photo_callback)

end 
local function on_submain_sptex_click(btn)

	local name = "upload".. btn.name:sub(4) .. ".png"
	local tex = Ref.SubMain.spTexImg
	-- local tex = Ent.spPhoto
	UIMGR.on_sdk_take_photo(name, tex, function (succ, name, image)
		if succ then
			LOGIN.try_uploadphotoforreport(DY_DATA.User.id,image,on_upload_photo_callback)
		else
			PhotoName = nil
		end
	end)


	local platform = ENV.unity_platform
    local standalone = platform == "OSXEditor" 
                   or platform == "OSXPlayer" 
                   or platform == "WindowsEditor" 
                   or platform == "WindowsPlayer"
	if standalone then
	-- -- 				-- test ---
		UIMGR.load_photo(tex, "1.png", function (succ, name, image)
			if succ then
				on_take_photo_call_back(image)
			else
			
			end
		end)
	end

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

	ComListForUpdate = UI_DATA.WNDSubmitSchedule.ComList
	print("ComListForUpdate Now is :" .. JSON:encode(ComListForUpdate))

	if ComListForUpdate ~= nil and next(ComListForUpdate) ~= nil then 
		local id = tonumber(UI_DATA.WNDSetComPhoto.id)
		local Com = ComListForUpdate[id]
		print("ComListForUpdate Now is :-yes--" .. JSON:encode(Com))
		if Com ~= nil and next(Com) ~= nil then 
			Ref.SubMain.inpPrice.text = Com.price
			Ref.SubMain.inInfo.text = Com.info
			print("ComListForUpdate Now is----price---info- :" .. tostring(Com.price).."/"..tostring(Com.info))
			local tex = Ref.SubMain.spTexImg
			UIMGR.get_photo(tex, Com.icon)
		else
			Ref.SubMain.inpPrice.text = ""
			Ref.SubMain.inInfo.text = ""
		end




		-- UIMGR.on_sdk_take_photo(ComListForUpdate[id].icon, tex, function (succ, name, image)
		-- 	if succ then
		-- 		on_take_photo_call_back(image)
		-- 	else
		
		-- 	end
		-- end)
		-- UIMGR.load_photo(Ref.SubMain.spTex, "ww", function (succ, name, image)
		-- 		if succ then
		-- 			--PhotoList[1].image = image
		-- 		else
		-- 			--PhotoList[1].image = nil
		-- 		end
		-- 	end)
	end


	--!*以上：自动注册的回调函数*--
end

local function init_logic()
	Ref.SubTop.lbText.text = UI_DATA.WNDSetComPhoto.name
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

