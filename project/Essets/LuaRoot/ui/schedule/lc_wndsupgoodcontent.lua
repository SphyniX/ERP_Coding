--
-- @file    ui/schedule/lc_wndsupgoodcontent.lua
-- @authors cks
-- @date    2016-11-14 19:38:29
-- @desc    WNDsupGoodContent
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
local IfcanSave
local PhotoName
local Ref
--!*以下：自动生成的回调函数*--

local function on_subtop_back_click(btn)
	
end

local function on_subtop_clear_click(btn)
	
end
-- local function on_upload_photo_callback( Ret )
-- 	-- body
-- 	if Ret.ret == 1 then
-- 		IfcanSave = true
-- 		_G.UI.Toast:make(nil, "图片上传成功,可以打卡"):show()
-- 	else
-- 		_G.UI.Toast:make(nil, "图片上传失败，请重新拍摄"):show()
-- 		UIMGR.load_photo( tex, "nil.png")
-- 	end
	
--end
local function on_upload_photo_callback( Ret )
	-- body
	if Ret.ret == 1 then
		IfcanSave = true
		PhotoName = Ret.photoid[1]
		_G.UI.Toast:make(nil, "成功"):show()
	else
		_G.UI.Toast:make(nil, "图片上传失败，请重新拍摄"):show()
		UIMGR.load_photo( tex, "nil.png")
	end
end
local function on_submain_subbtnimg_click(btn)
	print("拍图片")
	local name = "upload".. btn.name:sub(4) .. "png"
	local tex = Ref.SubMain.spTex
	-- local tex = Ent.spPhoto
	UIMGR.on_sdk_take_photo(name, tex, function (succ, name, image)
		if succ then
			LOGIN.try_uploadphotoforreport(DY_DATA.User.id,image,on_upload_photo_callback)
		else
			PhotoName = nil
		end
	end)

	---test ----
	UIMGR.load_photo(tex,"1.png" , function (succ, name, image)
		if succ then
			LOGIN.try_uploadphotoforreport(DY_DATA.User.id,image,on_upload_photo_callback)
		else
			-- PhotoList[i].image = nil
		end
	end)
end

local function on_save_click(btn)
		--IfcanSave=true
		--PhotoName="qw.png"
		if PhotoName ~= nil then
		print("督导考勤")
		local nm = NW.msg("REPORTED.CS.GETSUPUPLOADCOMANALYSIS")
		print("UI_DATA.WNDSupStoreData.storeId---"..UI_DATA.WNDSupStoreData.storeId)
		nm:writeU32(tonumber(UI_DATA.WNDSupStoreData.storeId))

		nm:writeU32(1)
		print("UI_DATA.WNDSelectStore.projectId---"..UI_DATA.WNDSelectStore.projectId)
		local roject = DY_DATA.SchProjectList[UI_DATA.WNDSelectStore.projectId] -- DY_DATA.StoreData.ComListRe
		print("-----------"..JSON:encode(roject))
		local ComListRe = roject.ComList
		print("-----------"..JSON:encode(ComListRe))
		print("UI_DATA.WNDSupDataGoodAnalysis.index---"..UI_DATA.WNDSupDataGoodAnalysis.index)
		print("roject.ComList[UI_DATA.WNDSupDataGoodAnalysis.index].id--"..ComListRe[UI_DATA.WNDSupDataGoodAnalysis.index].id)
		nm:writeU32(tonumber(ComListRe[UI_DATA.WNDSupDataGoodAnalysis.index].id))
		print(Ref.SubMain.SubPrice.inpInput.text)
		nm:writeString(Ref.SubMain.SubPrice.inpInput.text)
		print(Ref.SubMain.SubMechanism.inpInput.text)
		nm:writeString(Ref.SubMain.SubMechanism.inpInput.text)
		print("PhotoName------------------------"..PhotoName)
		nm:writeString(PhotoName)
		NW.send(nm)
		Ref.SubMain.SubPrice.inpInput.text = ""
		Ref.SubMain.SubMechanism.inpInput.text = ""
		UIMGR.close_window(Ref.root)
		else
		 _G.UI.Toast:make(nil, "请拍摄图片"):show()
		end
end

local function init_view()
	Ref.SubTop.Back.onAction = on_subtop_back_click
	Ref.SubTop.Clear.onAction = on_subtop_clear_click
	Ref.SubMain.SubbtnImg.btn.onAction = on_submain_subbtnimg_click
	Ref.Save.onAction = on_save_click
	--!*以上：自动注册的回调函数*--
end

local function init_logic()

	-- local roject =   DY_DATA.SchProjectList[UI_DATA.WNDSelectStore.projectId] -- DY_DATA.StoreData.ComListRe
	-- local ComListRe = roject.ComList
	-- print("UI_DATA.WNDSupDataGoodAnalysis.index"..UI_DATA.WNDSupDataGoodAnalysis.index)  --UI_DATA.WNDSupDataGoodAnalysis.index
	-- local Com = ComListRe[UI_DATA.WNDSupDataGoodAnalysis.index]
	-- print("Com---------------------"..Com.price)
	-- if Com ~= nil and next(Com) ~= nil then
	-- 	Ref.SubMain.SubPrice.inpInput.text = Com.price
	-- 	Ref.SubMain.SubMechanism.inpInput.text = Com.value
	-- end
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

