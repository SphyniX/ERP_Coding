--
-- @file    ui//lc_wndsubmitphotoforreport.lua
-- @authors zl
-- @date    2016-11-15 06:53:22
-- @desc    WNDSubmitPhotoForReport
--

local ipairs, pairs
    = ipairs, pairs
local libugui = require "libugui.cs"
local libunity = require "libunity.cs"
local UIMGR = MERequire "ui/uimgr"
local NW = MERequire "network/networkmgr"
local UI_DATA = MERequire "datamgr/uidata.lua"
local DY_DATA = MERequire "datamgr/dydata.lua"
local LOGIN = MERequire "libmgr/login.lua"
local Ref

local PhotoList
local PhotoListForUpdate
local IfDone
local PhotoListForSaveData
local NowBtn
local NowNumber
--!*以下：自动生成的回调函数*--

local function on_ui_init(NWStata)
	UI_DATA.WNDSubmitSchedule.WNDSubmitPhotoForReportNWStata = NWStata
	local projectId
	if DY_DATA.User.limit == 1 then
		projectId = UI_DATA.WNDSubmitSchedule.projectId
	else
		projectId = UI_DATA.WNDSupStoreData.projectId
	end
	--UIMGR.get_photo(tex, Com.icon)
	-- PhotoListInit = DY_DATA.WNDSubmitScheduleData.SchedulePhoto
	-- print("PhotoList in WNDSubmitPhotoForReport is " .. JSON:encode(PhotoList))
	-- Ref.SubPhoto.GrpPhoto:dup(#PhotoList, function (i, Ent, isNew)
	-- 	local Photo = PhotoList[i]

	-- 	if Photo.state == 1 then 
	-- 		Ent.lbTitle.text = Photo.name
	-- 		print("Photo.icon----------"..tostring(Photo.icon))
	-- 		UIMGR.get_photo(Ent.spIcon, Photo.icon)
	-- 	else
	-- 		libunity.SetActive(Ent.spState,false)
	-- 	end
	-- 	libunity.SetActive(Ent.spIfsucc,false)
	-- end)

	PhotoListForSaveData = {}
	PhotoList = DY_DATA.SchProjectList[projectId].SellPhoto
	Ref.SubPhoto.GrpPhoto:dup(#PhotoList, function (i, Ent, isNew)
		local Photo = PhotoList[i]
		table.insert(PhotoListForSaveData,Photo)
		UI_DATA.WNDSubmitSchedule.PhotoListForSaveData = PhotoListForSaveData
		if Photo.state == 1 then 
			Ent.lbTitle.text = Photo.name
			libunity.SetActive(Ent.spState,true)
		else
			Ent.lbTitle.text = Photo.name
			libunity.SetActive(Ent.spState,false)
		end
		libunity.SetActive(Ent.spIfsucc,false)
		
	end)

	local SchedulePhotoListtUpdate = UI_DATA.WNDSubmitSchedule.SchedulePhotoListtUpdate
	print("图片SchedulePhotoListtUpdate---图片---"..tostring(JSON:encode(UI_DATA.WNDSubmitSchedule.SchedulePhotoListtUpdate1)))
	if SchedulePhotoListtUpdate ~= nil then
		if SchedulePhotoListtUpdate ~= nil and next(SchedulePhotoListtUpdate) ~= nil then
			Ref.SubPhoto.GrpPhoto:dup(#SchedulePhotoListtUpdate, function (i, Ent, isNew)
			local Photo = SchedulePhotoListtUpdate[i]
			print("图片Id"..Photo.productPhotoid)
				UIMGR.get_photo(Ent.spPhoto, Photo.productPhotoid)
					--libunity.SetActive(Ent.spState,false)
			end)
		end
	end


	--本地加载图片

	local loadPhotoList = UI_DATA.WNDSubmitSchedule.LoadPhotoListForSaveData
	print("PhotoList in WNDSubmitPhotoForReport is011 " .. JSON:encode(loadPhotoList))
	if loadPhotoList ~= nil and next(loadPhotoList) ~= nil then
		print("PhotoList in WNDSubmitPhotoForReport is0 " .. JSON:encode(loadPhotoList))
		Ref.SubPhoto.GrpPhoto:dup(#loadPhotoList, function (i, Ent, isNew)
			print("PhotoList in WNDSubmitPhotoForReport is1 " .. JSON:encode(loadPhotoList))
			local Photo = loadPhotoList[i]
			print("PhotoList in WNDSubmitPhotoForReport is2 " .. JSON:encode(loadPhotoList))
			if Photo.state == 1 then 
				Ent.lbTitle.text = Photo.name
				libunity.SetActive(Ent.spState,true)
			else
				Ent.lbTitle.text = Photo.name
				libunity.SetActive(Ent.spState,false)
			end
			libunity.SetActive(Ent.spIfsucc,false)
			if Photo.PicPath ~= nil then
							print("PhotoList in WNDSubmitPhotoForReport is3 " .. JSON:encode(Photo.PicPath))
				local tex = Ent.spPhoto
				libugui.SetPhoto( tex, Photo.PicPath, function (o, p)
					if p then
						_G.UI.Toast:make(nil,"图片加载成功"):show()
					end
				end)
			end
		end)
	end

end

local function on_upload_photo_callback(Ret)
		print("on_upload_photo_callbackPhotoForUpdate------------")
	
	if tonumber(Ret.ret) == 1 then
		libunity.SetActive(NowBtn.spIfsucc,true)
		local PhotoForUpdate = {
			id = PhotoList[NowNumber].id,
			photo = Ret.photoid[1],
			state = PhotoList[NowNumber].state,
		}
		print("PhotoForUpdate------------"..PhotoForUpdate.state)
		local blTemp = false
		if PhotoListForUpdate ~= nil then
			if next(PhotoListForUpdate) ~= nil then 
				for i=1,#PhotoListForUpdate  do
					if PhotoListForUpdate[i].id == PhotoList[NowNumber].id then
						PhotoListForUpdate[i] = PhotoForUpdate
						_G.UI.Toast:make(nil, "更新成功"):show()
						blTemp = true
					end
				end
				if not blTemp then
					table.insert(PhotoListForUpdate,PhotoForUpdate)	
					_G.UI.Toast:make(nil, "添加成功"):show()
				end
			else
				table.insert(PhotoListForUpdate,PhotoForUpdate)	
				_G.UI.Toast:make(nil, "添加成功"):show()
			end
		end
	else
		_G.UI.Toast:make(nil, "失败"):show()
	end
end

local function on_take_photo_call_back(image)

	LOGIN.try_uploadphotoforreport(DY_DATA.User.id,image,on_upload_photo_callback)
end 


local imagesindex = 2
local function upload_allphoto_callback(Ret)
	if tonumber(Ret.ret) == 1 then
		_G.UI.Toast:make(nil, "成功上传图片"..tostring(imagesindex)):show()
		if #Ref.SubPhoto.GrpPhoto.Ents >0 then
			NowBtn = Ref.SubPhoto.GrpPhoto.Ents[imagesindex]
			imagesindex = imagesindex + 1
			if NowBtn ~= nil then
				local tex = NowBtn.spPhoto
				upload_localphoto_callback(images[imagesindex])
			else
				imagesindex = 1
				return
			end
		end

		-- libunity.SetActive(NowBtn.spIfsucc,true)
		-- local PhotoForUpdate = {
		-- 	id = PhotoList[NowNumber].id,
		-- 	photo = Ret.photoid[1],
		-- 	state = PhotoList[NowNumber].state,
		-- }
		-- print("PhotoForUpdate------------"..PhotoForUpdate.state)
		-- local blTemp = false
		-- if PhotoListForUpdate ~= nil then
		-- 	if next(PhotoListForUpdate) ~= nil then 
		-- 		for i=1,#PhotoListForUpdate  do
		-- 			if PhotoListForUpdate[i].id == PhotoList[NowNumber].id then
		-- 				PhotoListForUpdate[i] = PhotoForUpdate
		-- 				_G.UI.Toast:make(nil, "更新成功"):show()
		-- 				blTemp = true
		-- 			end
		-- 		end
		-- 		if not blTemp then
		-- 			table.insert(PhotoListForUpdate,PhotoForUpdate)	
		-- 			_G.UI.Toast:make(nil, "添加成功"):show()
		-- 		end
		-- 	else
		-- 		table.insert(PhotoListForUpdate,PhotoForUpdate)	
		-- 		_G.UI.Toast:make(nil, "添加成功"):show()
		-- 	end
		-- end
	else
		imagesindex = 1
		_G.UI.Toast:make(nil, "失败"):show()
	end
end
local function upload_localphoto_callback(image)
	LOGIN.try_uploadphotoforreport(DY_DATA.User.id,image,upload_allphoto_callback)
end 
local function uploadPic(img)

	--Ref.SubPhoto.GrpPhoto.Ents[1]
	-- body
end


local function on_subphoto_grpphoto_entphoto_click(btn)
	local storeId = UI_DATA.WNDSubmitSchedule.storeId
	local projectId = UI_DATA.WNDSubmitSchedule.projectId
	local name = btn.name:sub(9) .. ".png"
	name = "WNDSubmitPhotoForReport_".."PId"..projectId.."_SId"..storeId.."_Id"..name
	NowNumber = tonumber(btn.name:sub(9))
	if PhotoListForSaveData[NowNumber] then
		PhotoListForSaveData[NowNumber].PicPath = name
	end
	UI_DATA.WNDSubmitSchedule.PhotoListForSaveData = PhotoListForSaveData
	NowBtn = Ref.SubPhoto.GrpPhoto.Ents[tonumber(btn.name:sub(9))]
	local tex = NowBtn.spPhoto

	if DY_DATA.User.limit == 1 then
		UIMGR.on_sdk_take_photo_selecttype(name, tex, "nottakephoto" , function (succ, name, image)
			if succ then
				on_take_photo_call_back(image)
			else
		
			end
		end)
	else
		UIMGR.on_sdk_take_photo(name, tex, function (succ, name, image)
			if succ then
				on_take_photo_call_back(image)
			else
		
			end
		end)

	end

	-- -- 				-- test ---
	local platform = ENV.unity_platform
    local standalone = platform == "OSXEditor" 
                   or platform == "OSXPlayer" 
                   or platform == "WindowsEditor" 
                   or platform == "WindowsPlayer"
	if standalone then
		if name ~= nil then
			libugui.SetPhoto( tex, name, function (o, p)
			if p then
				on_take_photo_call_back(o)
				_G.UI.Toast:make(nil,"图片加载成功"):show()
			end
			end)
		end
	-- -- 				-- test ---
		-- UIMGR.load_photo(tex, name, function (succ, name, image)
		-- 	if succ then
		-- 		on_take_photo_call_back(image)
		-- 	else
			
		-- 	end
		-- end)
	end
	---------------------------

end

local function on_subtop_btnback_click(btn)
	UIMGR.close_window(Ref.root)
end

local function on_btnsave_click(btn)
	local need = 0
	local nowtrue = 0

	for i=1,#PhotoList do
		if PhotoList[i].state == 1 then
			need = need + 1
		end
	end

	for i=1,#PhotoListForUpdate do
		if PhotoListForUpdate[i].state == 1 then
			nowtrue = nowtrue + 1
		end
	end
	print("need/nowtrue"..tostring(need).."/"..tostring(nowtrue))
	if need ~= nowtrue then 
		_G.UI.Toast:make(nil, "必打图片缺少，请检查！"):show()
		return
	else
		if DY_DATA.User.limit == 1 then
			UI_DATA.WNDSubmitSchedule.PhotoListForUpdate = PhotoListForUpdate
		else
			local nm = NW.msg("REPORTED.CS.GETSUPGUPLOADPHOTO")
			nm:writeU32(UI_DATA.WNDSupStoreData.storeId)
			nm:writeU32(#PhotoListForUpdate)
			for i=1,#PhotoListForUpdate do
				nm:writeU32(PhotoListForUpdate[i].id)
				nm:writeString(PhotoListForUpdate[i].photo)
			end
			NW.send(nm)
		end
		UIMGR.close_window(Ref.root)
	end
	print("UI_DATA.WNDSubmitSchedule.PhotoListForSaveData----"..JSON:encode(UI_DATA.WNDSubmitSchedule.PhotoListForSaveData))

end

local function init_view()
	Ref.SubPhoto.GrpPhoto.Ent.btn.onAction = on_subphoto_grpphoto_entphoto_click
	Ref.SubTop.btnBack.onAction = on_subtop_btnback_click
	Ref.btnSave.onAction = on_btnsave_click
	UIMGR.make_group(Ref.SubPhoto.GrpPhoto, function (New, Ent)
		New.btn.onAction = Ent.btn.onAction
	end)
	--!*以上：自动注册的回调函数*--
end
local function on_ui_initBack()
		on_ui_init(true)
end
local function init_logic()
	NW.subscribe("WORK.SC.GETSELLPHOTO",on_ui_initBack)
	NW.subscribe("WORK.SC.GETSUPPHOTO",on_ui_init)
	if PhotoListForUpdate == nil then
		PhotoListForUpdate = {}
	end
	local projectId
	if DY_DATA.User.limit == 1 then
		projectId = UI_DATA.WNDSubmitSchedule.projectId
	else
		projectId = UI_DATA.WNDSupStoreData.projectId
	end
	PhotoList = DY_DATA.SchProjectList[projectId].SellPhoto
	if PhotoList == nil or PhotoList == {} then
		if DY_DATA.User.limit == 1 then
			local nm = NW.msg("WORK.CS.GETSELLPHOTO")
			nm:writeU32(projectId)
			NW.send(nm)
			return
		else
			local nm = NW.msg("WORK.CS.GETSUPPHOTO")
			nm:writeU32(projectId)
			NW.send(nm)
			return
		end
	end
	on_ui_init(true)


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
	NW.unsubscribe("WORK.SC.GETSELLPHOTO",on_ui_init)
	NW.unsubscribe("WORK.SC.GETSUPPHOTO",on_ui_init)
end

local P = {
	start = start,
	update_view = update_view,
	on_recycle = on_recycle,
}
return P

