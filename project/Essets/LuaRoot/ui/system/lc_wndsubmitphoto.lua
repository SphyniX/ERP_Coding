--
-- @file    ui/system/lc_wndsubmitphoto.lua
-- @authors ckxz
-- @date    2016-07-29 13:52:24
-- @desc    WNDSubmitPhoto
--

local ipairs, pairs
    = ipairs, pairs
local libugui = require "libugui.cs"
local libunity = require "libunity.cs"
local UIMGR = MERequire "ui/uimgr"
local UI_DATA = MERequire "datamgr/uidata.lua"
local Ref

local callback, PhotoList
local input,inputvalue,characterLimit

--!*以下：自动生成的回调函数*--

local function on_subphoto_grpphoto_entphoto_click(btn)
	local index = tonumber(btn.name:sub(9))
	local name = PhotoList[index].name or "submit_"..PhotoList[index].typeId.."_"..index..".png"
	local Ent = Ref.SubPhoto.GrpPhoto.Ents[index]
	local tex = Ent.spPhoto
	UIMGR.on_sdk_take_photo(name, tex, function (succ, name, image)
		PhotoList[index].image = image
	end)
end

local function on_subtop_btnback_click(btn)
	UIMGR.close_window()
end

local function on_subtop_btnsave_click(btn)
	local n = #PhotoList
	for i,v in ipairs(PhotoList) do
		if v.need and v.image == nil then 
			_G.UI.Toast:make(nil, v.title.."不能为空"):show()
			return 
		end
	end
	local inp = ""
	if input then
		inp = Ref.SubPhoto.GrpPhoto.inpInput.text
		if inp == "" then
			_G.UI.Toast:make(nil, "缺少"..input):show()
			return
		end
	end
	callback(PhotoList, inp)
	UIMGR.close_window(Ref.root)
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
	callback = UI_DATA.WNDShowPhoto.callback
	UI_DATA.WNDShowPhoto.callback = nil
	local title = UI_DATA.WNDShowPhoto.title
	UI_DATA.WNDShowPhoto.title = nil
	Ref.SubTop.lbText.text = title
	local tip = UI_DATA.WNDShowPhoto.tip
	UI_DATA.WNDShowPhoto.tip = nil
	Ref.SubPhoto.GrpPhoto.lbTip.text = tip
	PhotoList = UI_DATA.WNDShowPhoto.photolist
	UI_DATA.WNDShowPhoto.photolist = nil
	input = UI_DATA.WNDShowPhoto.input
	UI_DATA.WNDShowPhoto.input = nil
	inputvalue = UI_DATA.WNDShowPhoto.inputvalue
	UI_DATA.WNDShowPhoto.inputvalue = nil
	Ref.SubPhoto.GrpPhoto.lbInput.text = input or ""
	Ref.SubPhoto.GrpPhoto.inpInput.text = inputvalue or ""

	if UI_DATA.WNDShowPhoto.characterLimit ~= nil then
		characterLimit = tonumber(UI_DATA.WNDShowPhoto.characterLimit)
		Ref.SubPhoto.GrpPhoto.inpInput.characterLimit = characterLimit
	end

	UI_DATA.WNDShowPhoto = {}

	libunity.SetActive(Ref.SubPhoto.GrpPhoto.lbInput, input ~= nil)
	libunity.SetActive(Ref.SubPhoto.GrpPhoto.inpInput, input ~= nil)

	Ref.SubPhoto.GrpPhoto:dup(#PhotoList, function (i, Ent, isNew)
		Ent.lbTitle.text = PhotoList[i].title
		Ent.spPhoto.texture = nil
		local name = PhotoList[i].name or "submit_"..(PhotoList[i].typeId or 0).."_"..i..".png"
		if PhotoList[i].dl then
			UIMGR.get_photo(Ent.spPhoto, name, function (succ, name, image)
				if succ then
					PhotoList[i].image = image
				else
					PhotoList[i].image = nil
				end
			end)
		else
			UIMGR.load_photo(Ent.spPhoto, name, function (succ, name, image)
				if succ then
					PhotoList[i].image = image
				else
					PhotoList[i].image = nil
				end
			end)
		end
	end)
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

