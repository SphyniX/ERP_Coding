--
-- @file    ui/schedule/lc_wndsubmitphotoforreport.lua
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
local localPictures
local uploadLocalphotoCallback
local uploadAllPhotoCallBack
--!*以下：自动生成的回调函数*--
local function on_upload_photo_callback(Ret)
		print("on_upload_photo_callbackPhotoForUpdate------------")
	
	if tonumber(Ret.ret) == 1 then
		libunity.SetActive(NowBtn.spIfsucc,true)
		local PhotoForUpdate = {
			id = PhotoList[NowNumber].id,
			photo = Ret.photoid[1],
			state = PhotoList[NowNumber].state,
		}
		print("PhotoForUpdate.photo"..tostring(PhotoForUpdate.photo))
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
local function on_subphoto_grpphoto_entphoto_click(btn)
	local name = btn.name:sub(9) .. ".png"
	
	NowNumber = tonumber(btn.name:sub(9))
	NowBtn = Ref.SubPhoto.GrpPhoto.Ents[tonumber(btn.name:sub(9))]
	local tex = NowBtn.spPhoto

	if DY_DATA.User.limit == 1 then
		local storeId = UI_DATA.WNDSubmitSchedule.storeId
		local projectId = UI_DATA.WNDSubmitSchedule.projectId
		name = "WNDSubmitPhotoForReport_".."PId"..projectId.."_SId"..storeId.."_Id"..name
		if PhotoListForSaveData[NowNumber] then
			PhotoListForSaveData[NowNumber].PicPath = name
		end
		UI_DATA.WNDSubmitSchedule.PhotoListForSaveData = PhotoListForSaveData

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

local function on_subtop_btnclear_click(btn)
	
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
	-- if DY_DATA.User.limit == 1 then
	-- 	UI_DATA.WNDSubmitSchedule.PhotoListForUpdateSave = PhotoListForUpdate
	-- 	print("UI_DATA.WNDSubmitSchedule.PhotoListForUpdate-----------xxxx---"..JSON:encode( UI_DATA.WNDSubmitSchedule.PhotoListForUpdateSave))
	-- end
	print("need/nowtrue"..tostring(need).."/"..tostring(nowtrue))
	if need ~= nowtrue then 
		_G.UI.Toast:make(nil, "必打图片缺少，请检查！"):show()
		return
	else
		if DY_DATA.User.limit == 1 then

			UI_DATA.WNDSubmitSchedule.PhotoListForUpdate = PhotoListForUpdate
			print("UI_DATA.WNDSubmitSchedule.PhotoListForUpdate-----------xxxx---"..JSON:encode( UI_DATA.WNDSubmitSchedule.PhotoListForUpdate))
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


local imagesindex = 1
local  function upload_allphoto_callback(Ret)

	if tonumber(Ret.ret) == 1 then
		_G.UI.Toast:make(nil, "成功上传图片"..tostring(imagesindex)):show()
		print("imagesindex--------"..tostring(imagesindex))
		--imagesindex = imagesindex + 1


		if imagesindex <= #localPictures then
			PhotoListForSaveData[imagesindex].PicPath = UI_DATA.WNDSubmitSchedule.LoadPhotoListForSaveData[imagesindex].PicPath
			UI_DATA.WNDSubmitSchedule.PhotoListForSaveData = PhotoListForSaveData
			libunity.SetActive(Ref.SubPhoto.GrpPhoto.Ents[imagesindex].spIfsucc,true)

			local loadPhotoList = UI_DATA.WNDSubmitSchedule.LoadPhotoListForSaveData
			local PhotoForUpdate = {
				id = loadPhotoList[imagesindex].id,
				photo = Ret.photoid[1],
				state = loadPhotoList[imagesindex].state,
			}
			table.insert(PhotoListForUpdate,PhotoForUpdate)
		end
		imagesindex = imagesindex + 1
		if localPictures ~= nil and #localPictures >0 then
			if localPictures[imagesindex] ~= nil then
				if localPictures[imagesindex] then
					uploadLocalphotoCallback(localPictures[imagesindex])
				else
					imagesindex = imagesindex + 1
					--local Ret = {}
					Ret.ret = 1
					uploadAllPhotoCallBack(Ret)
					return
				end


			else
				libunity.SetActive(Ref.btnLoad,false)
				_G.UI.Toast:make(nil, "所有图片上传完成"..tostring(imagesindex - 1)):show()
				return
			end
		else
			_G.UI.Toast:make(nil, "没有可上传图片"..tostring(imagesindex)):show()
		end

	else
		imagesindex = 1
		_G.UI.Toast:make(nil, "上传失败"):show()
	end
end
local function upload_localphoto_callback(image)
	LOGIN.try_uploadphotoforreport(DY_DATA.User.id,image,uploadAllPhotoCallBack)
end 
local function on_btnload_click(btn)     								 -------------------------图片加载按钮
		--upload_localphoto_callback(localPictures[1])
		imagesindex = 1
		if localPictures ~= nil and #localPictures > 0 then
			for i=1,#localPictures do
				if localPictures[i] then
					PhotoListForUpdate = {}
					upload_localphoto_callback(localPictures[i])
					imagesindex = i
					break
				else
					--imagesindex = i
				end
			end
			--imagesindex = 2
		end
end




local function on_ui_init(NWStata)
	UI_DATA.WNDSubmitSchedule.WNDSubmitPhotoForReportNWStata = NWStata
	local projectId
	if DY_DATA.User.limit == 1 then
		projectId = UI_DATA.WNDSubmitSchedule.projectId
	else
		projectId = UI_DATA.WNDSupStoreData.projectId
	end
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
--个人进度数据
	local SchedulePhotoListtUpdate = UI_DATA.WNDSubmitSchedule.SchedulePhotoListtUpdate
	print("图片SchedulePhotoListtUpdate---图片---"..tostring(JSON:encode(UI_DATA.WNDSubmitSchedule.SchedulePhotoListtUpdate)))
	if SchedulePhotoListtUpdate ~= nil then
		if SchedulePhotoListtUpdate ~= nil and next(SchedulePhotoListtUpdate) ~= nil then
			for i=1,#SchedulePhotoListtUpdate do
				for j=1,#Ref.SubPhoto.GrpPhoto.Ents do
					local Ent = Ref.SubPhoto.GrpPhoto.Ents[j]
					local Photo = SchedulePhotoListtUpdate[i]
					if Ent == nil then return end
					print("图片id"..tostring(PhotoList[j].id).."/"..tostring(Photo.productPhotoid))

					if tostring(PhotoList[j].id) == tostring(Photo.productPhotoid) then
						print("插入成功 图片Id"..tostring(Photo.PhotoId))
						UIMGR.get_photo(Ent.spPhoto, Photo.PhotoId)
					end
				end
			end

		end
	end


	--本地加载图片

	local loadPhotoList = UI_DATA.WNDSubmitSchedule.LoadPhotoListForSaveData
	print("PhotoList in WNDSubmitPhotoForReport is011 " .. JSON:encode(loadPhotoList))
	if loadPhotoList ~= nil and next(loadPhotoList) ~= nil then
		print("PhotoList in WNDSubmitPhotoForReport is0 " .. JSON:encode(loadPhotoList))
		for i=1,#Ref.SubPhoto.GrpPhoto.Ents do
			local Ent = Ref.SubPhoto.GrpPhoto.Ents[i]
			local Photo = loadPhotoList[i]
			print("PhotoList in WNDSubmitPhotoForReport is1 " .. JSON:encode(loadPhotoList))
			if Ent == nil or Photo ==nil then return end
			
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
					print("图片路径：Photo.PicPath"..tostring(Photo.PicPath))
					libugui.SetPhoto( tex, tostring(Photo.PicPath), function (o, p)
					if p then
						if localPictures == nil then localPictures = {} end
						localPictures[i] = o
						--on_take_photo_call_back(o)
						print("图片加载成功")
						_G.UI.Toast:make(nil,"图片加载成功"):show()

					end

				end)
			else
						--localPictures[i] = p
				print("图片加载不存在或者失败")
				if localPictures == nil then localPictures = {} end
				localPictures[i] = false
				_G.UI.Toast:make(nil,"图片加载不存在或者失败"):show()
			end
		end
	end

end


local function on_ui_initBack()
		on_ui_init(false)
end
local function init_view()
	Ref.SubPhoto.GrpPhoto.Ent.btn.onAction = on_subphoto_grpphoto_entphoto_click
	Ref.SubTop.btnBack.onAction = on_subtop_btnback_click
	Ref.SubTop.btnClear.onAction = on_subtop_btnclear_click
	Ref.btnSave.onAction = on_btnsave_click
	Ref.btnLoad.onAction = on_btnload_click
	UIMGR.make_group(Ref.SubPhoto.GrpPhoto, function (New, Ent)
		New.btn.onAction = Ent.btn.onAction
	end)
	--!*以上：自动注册的回调函数*--
end

local function init_logic()
	uploadLocalphotoCallback = upload_localphoto_callback
	uploadAllPhotoCallBack = upload_allphoto_callback
	libunity.SetActive(Ref.btnLoad,UI_DATA.WNDSubmitSchedule.loadState and DY_DATA.User.limit == 1)    ---控制进度图片列表是否显示


	NW.subscribe("WORK.SC.GETSELLPHOTO",on_ui_initBack)					 ---获取促销员图片列表
	NW.subscribe("WORK.SC.GETSUPPHOTO",on_ui_init)							---获取督导图片列表
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
	on_ui_init(false)


end


local function uploadPic(img)

	--Ref.SubPhoto.GrpPhoto.Ents[1]
	-- body
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

