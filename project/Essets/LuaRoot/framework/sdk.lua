-- File Name: framework/sdk.lua

local libunity = require "libunity.cs"
local libasset = require "libasset.cs"

local function on_get_photo(Param)
	local path = Param.path
	local UI_DATA = MERequire "datamgr/uidata.lua"
	local on_get_photo_callback = UI_DATA.WNDPhoto.on_get_photo_callback

	if on_get_photo_callback ~= nil then on_get_photo_callback(path) end
end

local FuncMap = {
	on_get_photo = on_get_photo,
}

local function on_sdk_message(param)
	libunity.LogE("lua on_sdk_message : {0}", param)
	local Param = JSON:decode(param)
	if Param then
		local cbf = FuncMap[Param.method]
		if cbf then cbf(Param) end
	end
end

local function start()
	
end

return {
	start = start,
	on_sdk_message = on_sdk_message,
}