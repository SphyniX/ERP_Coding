--
-- @file    ui/login/lc_wndtest.lua
-- @authors ckxz
-- @date    2016-07-07 17:51:56
-- @desc    WNDTest
--

local ipairs, pairs
    = ipairs, pairs
local libugui = require "libugui.cs"
local libunity = require "libunity.cs"
local libsystem = require "libsystem.cs"
local UIMGR = MERequire "ui/uimgr"
local DY_DATA = MERequire "datamgr/dydata.lua"
local UI_DATA = MERequire "datamgr/uidata.lua"
local LOGIN = MERequire "libmgr/login.lua"
local Ref
--!*以下：自动生成的回调函数*--

local function on_btnback_click(btn)
	UIMGR.close_window(Ref.root)
end

local function on_btntakephoto_click(btn)
	local UI_DATA = MERequire "datamgr/uidata.lua"
	UIMGR.on_sdk_take_photo("test.png", Ref.spImage)
end

local function on_btnloadphoto_click(btn)
	-- UIMGR.load_photo(Ref.spImage, "test.png", function (succ, name, image)
	-- 	print(image)
	-- 	Ref.spImage.texture = image
	-- end)
	UIMGR.get_photo(Ref.spImage, "1.png", function (succ, name, image)
		local Param = {
			phone = "214521345121",
			password = "11234",
			name = "11234",
			age = "1/1/1",
			sex = "1",
			height = "1123",
			weight = "12341",
			wechat = "11234",
			qq = "11234",
			email = "11234",
			idnumber = "11234",
			cardNo = "112341234",
			city = "20",
			supname = "2",
			PhotoList = {
			 		image,
			 		image,
			 		image,
			 		image,
			 		image,
			 		image,
			},
		}
		LOGIN.try_regist(Param, nil)

	end)
end

local function on_btnupphoto_click(btn)
	UIMGR.create_window("UI/WNDSetCompeteProduct")
end

local function on_btngetgps_click(btn)
	local gps = libsystem.GetGps()
	print(gps)
	Ref.lbPath.text = gps
end

local function on_btntest_click(btn)
	UI_DATA.WNDPatch.appUrl = "http://139.196.109.3:8000/Version/1.0.1.1/Richer.apk"
	UIMGR.create_window("UI/WNDPatch")
end

local function on_btnpromotions_click(btn)
-- ##	UIMGR.WNDStack:pop()
	UIMGR.create_window("UI/WNDTestAttendance")
end

local function on_btnsupervisor_click(btn)
-- ##	UIMGR.WNDStack:pop()
	UIMGR.create_window("UI/WNDSupAttendance")
end

local function on_btnarea_click(btn)
-- ##	UIMGR.WNDStack:pop()
	UIMGR.create_window("UI/WNDAreaWork")
end

local function on_btnproject_click(btn)
-- ##	UIMGR.WNDStack:pop()
	UIMGR.create_window("UI/WNDProjectSchedule")
end

local function init_view()
	Ref.btnBack.onAction = on_btnback_click
	Ref.btnTakePhoto.onAction = on_btntakephoto_click
	Ref.btnLoadPhoto.onAction = on_btnloadphoto_click
	Ref.btnUpPhoto.onAction = on_btnupphoto_click
	Ref.btnGetGps.onAction = on_btngetgps_click
	Ref.btnTest.onAction = on_btntest_click
	Ref.btnPromotions.onAction = on_btnpromotions_click
	Ref.btnSupervisor.onAction = on_btnsupervisor_click
	Ref.btnArea.onAction = on_btnarea_click
	Ref.btnProject.onAction = on_btnproject_click
	--!*以上：自动注册的回调函数*--
end

local function init_logic()
	if table.void(DY_DATA.User) then MERequire "datamgr/localdata.lua" end
	_G.Debug = true
	-- libsystem.StartGps()
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

local P = {
	start = start,
	update_view = update_view,
}
return P

