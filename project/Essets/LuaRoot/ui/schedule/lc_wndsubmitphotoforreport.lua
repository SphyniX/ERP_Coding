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

local NowBtn
local NowNumber
--!*以下：自动生成的回调函数*--

local function on_ui_init()
	local projectId
	if DY_DATA.User.limit == 1 then
		projectId = UI_DATA.WNDSubmitSchedule.projectId
	else
		projectId = UI_DATA.WNDSupStoreData.projectId
	end
	PhotoList = DY_DATA.SchProjectList[projectId].SellPhoto
	print("PhotoList in WNDSubmitPhotoForReport is " .. JSON:encode(PhotoList))
	Ref.SubPhoto.GrpPhoto:dup(#PhotoList, function (i, Ent, isNew)
		local Photo = PhotoList[i]
		if Photo.state == 1 then 
			Ent.lbTitle.text = Photo.name
		else
			Ent.lbTitle.text = Photo.name
			libunity.SetActive(Ent.spState,false)
		end
		libunity.SetActive(Ent.spIfsucc,false)
	end)


end

local function on_upload_photo_callback(Ret)

	
	if Ret.ret == 1 then
		libunity.SetActive(NowBtn.spIfsucc,true)
		local PhotoForUpdate = {
			id = PhotoList[NowNumber].id,
			photo = Ret.photoid[1],
			state = PhotoList[NowNumber].state,
		}
		table.insert(PhotoListForUpdate,PhotoForUpdate)
		_G.UI.Toast:make(nil, "成功"):show()
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
	if DY_DATA.User.Limit == 1 then
		
		UIMGR.on_sdk_take_photo(name, tex, function (succ, name, image)
			if succ then
				on_take_photo_call_back(image)
			else
		
			end
		end)
	else
		UIMGR.on_sdk_take_photo_selecttype(name, tex, "nottakephoto" , function (succ, name, image)
			if succ then
				on_take_photo_call_back(image)
			else
		
			end
		end)
	end

	-- test ---
	UIMGR.load_photo(tex, name, function (succ, name, image)
		if succ then
			on_take_photo_call_back(image)
		else
		
		end
	end)
end

local function on_subtop_btnback_click(btn)
	UIMGR.close_window(Ref.root)
end

local function on_subtop_btnsave_click(btn)
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

end

local function init_view()
	Ref.SubPhoto.GrpPhoto.Ent.btn.onAction = on_subphoto_grpphoto_entphoto_click
	Ref.SubTop.btnBack.onAction = on_subtop_btnback_click
	Ref.SubTop.btnSave.onAction = on_subtop_btnsave_click
	UIMGR.make_group(Ref.SubPhoto.GrpPhoto, function (New, Ent)
		New.btn.onAction = Ent.btn.onAction
	end)
	--!*以上：自动注册的回调函数*--
end

local function init_logic()
	NW.subscribe("WORK.SC.GETSELLPHOTO",on_ui_init)
	NW.subscribe("WORK.SC.GETSUPPHOTO",on_ui_init)
	PhotoListForUpdate = {}
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
	on_ui_init()


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

