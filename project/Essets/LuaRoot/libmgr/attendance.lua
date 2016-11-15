-- File Name : libmgr/attendance.lua
local libunity = require "libunity.cs"
local libasset = require "libasset.cs"
local libsystem = require "libsystem.cs"
local libcsharpio = require "libcsharpio.cs"
local NW = MERequire "network/networkmgr"
local DY_DATA = MERequire "datamgr/dydata"
local UI_DATA = MERequire "datamgr/uidata"
local TEXT = _G.ENV.TEXT
local LOGIN = MERequire "libmgr/login.lua"
local MB = _G.UI.MessageBox
local UIMGR = MERequire "ui/uimgr"

local P = { } 
local punch_type,punch_id

function P.clear_punch_photo()
	
end

local function on_punch_callback(Photolist, inp)
   	local nPhoto = 0
   	local nPhotoList = 0
	for i,v in ipairs(Photolist) do
		if v.image then nPhotoList = nPhotoList + 1 end
	end
   	if NW.connected() then
		local function on_http_photo_callback()
	   		nPhoto = nPhoto + 1
	   		if nPhoto >= nPhotoList then
		   		local nm = NW.msg("ATTENCE.CS.UPWORK")
				nm:writeU32(punch_id)
				nm:writeU32(punch_type)
				NW.send(nm)
	   		end
	   	end

	   	for i,v in ipairs(Photolist) do
	   		print(v)
	   		if v.image then
	   			LOGIN.try_uploadphoto(DY_DATA.User.id, v.typeId, punch_id,v.image, on_http_photo_callback)
	   		end
	   	end
	end
end

local function on_try_punch_on()	
	-- 上班
	UI_DATA.WNDShowPhoto.title = "上班"
   	UI_DATA.WNDShowPhoto.tip = ""
   	UI_DATA.WNDShowPhoto.photolist = {
   		{ title = "门头照和人像", name = "sub_7.png" , typeId = 7 , need = true},
   	}
   	UI_DATA.WNDShowPhoto.callback = on_punch_callback
   	UIMGR.create_window("UI/WNDSubmitPhoto")
end

local function on_try_punch_off()
	-- 下班
	UI_DATA.WNDShowPhoto.title = "下班"
   	UI_DATA.WNDShowPhoto.tip = ""
   	UI_DATA.WNDShowPhoto.photolist = {
   		{ title = "门头照和人像", name = "sub_8.png" , typeId = 8, need = true },
   	}
   	UI_DATA.WNDShowPhoto.callback = on_punch_callback
   	UIMGR.create_window("UI/WNDSubmitPhoto")
end

function P.on_try_punch( type, projectId)
	punch_type = type
	punch_id = projectId
	if punch_type == 1 then
		on_try_punch_on()
	else
		on_try_punch_off()
	end
end

return P