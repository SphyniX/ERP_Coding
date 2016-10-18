--
-- @file    ui/schedule/lc_wndsetsupplies.lua
-- @authors ckxz
-- @date    2016-07-29 11:56:57
-- @desc    WNDSetSupplies
--

local ipairs, pairs
    = ipairs, pairs
local libugui = require "libugui.cs"
local libunity = require "libunity.cs"
local UIMGR = MERequire "ui/uimgr"
local UI_DATA = MERequire "datamgr/uidata.lua"
local DY_DATA = MERequire "datamgr/dydata.lua"
local TEXT = _G.ENV.TEXT
local NW = MERequire "network/networkmgr"
local Ref

local reason, reasonIndex, photoIndex
local MaterList
local Imagelist = {}
local InfoList = {}
local on_photo_init

local function on_set_reason_callback(reason, context)
	if reasonIndex == nil then return end
	local Ent = Ref.SubMain.GrpContent.Ents[reasonIndex]
	if Ent then
		Ent.lbTitle.text = TEXT.SuppliesType[reason]
		Ent.lbInfo.text = context
		local id = MaterList[reasonIndex].id
		InfoList[id] = {reason = reason, context = context}
	end
end

local function set_reason(index)
	reasonIndex = index
	reason = ""
	libunity.SetActive(Ref.SubReason.root, true)
end
local function on_set_photo_callback(Photolist)
	local id = MaterList[photoIndex].id
	Imagelist[id] = Photolist[1].image
	on_photo_init()
end

local function set_photo(index)
	photoIndex = index
	UI_DATA.WNDShowPhoto.title = "物料"
	UI_DATA.WNDShowPhoto.tip = ""
	UI_DATA.WNDShowPhoto.photolist = {
		[1] = { title = "", name = "18_"..index..".png", typeId = 18},
	}

	UI_DATA.WNDShowPhoto.callback = on_set_photo_callback
   	UIMGR.create_window("UI/WNDSubmitPhoto")
end

on_photo_init = function ()
	for i,v in ipairs(Ref.SubMain.GrpContent.Ents) do
		local Ent = v
		local m = MaterList[i]
		libunity.SetActive(Ent.btnShow, Imagelist[m.id])
		libunity.SetActive(Ent.btnSubmit, Imagelist[m.id])
	end
end

--!*以下：自动生成的回调函数*--

local function on_subtop_btnback_click(btn)
	UIMGR.close_window(Ref.root)
end

local function on_subtop_btnsave_click(btn)
	local SubmitProductList = UI_DATA.WNDSubmitSchedule.MaterList 
	if SubmitProductList == nil then SubmitProductList = {} end
	
	for i,v in ipairs(MaterList) do
		local Ent = Ref.SubMain.GrpContent.Ents[i]
		
		local id = MaterList[i].id
		if id == nil or id == "" then 
			_G.UI.Toast:make(nil, "有数据未填写"):show()
			return
		end
		local state = InfoList[id].reason
		if state == nil or state == "" then 
			_G.UI.Toast:make(nil, "有数据未填写"):show()
			return
		end
		local context = InfoList[id].context
		if context == nil or context == "" then 
			_G.UI.Toast:make(nil, "有数据未填写"):show()
			return
		end
		local image = Imagelist[id]
		if image == nil then 
			_G.UI.Toast:make(nil, "有图片未上传"):show()
			return
		end
		
		if SubmitProductList[id] == nil then SubmitProductList[id] = {} end
		SubmitProductList[i].id = id
		SubmitProductList[i].state = state
		SubmitProductList[i].context = context
		SubmitProductList[i].image = image
	end
	UI_DATA.WNDSubmitSchedule.MaterList = SubmitProductList
	UIMGR.close_window(Ref.root)
end

local function on_submain_grpcontent_enthard_spstate_click(btn)
	local index = tonumber(btn.transform.parent.name:sub(8))
	set_reason(index)
end

local function on_submain_grpcontent_enthard_spphoto_click(btn)
	local index = tonumber(btn.transform.parent.name:sub(8))
	set_photo(index)
end

local function on_subreason_btnback_click(btn)
	libunity.SetActive(Ref.SubReason.root, false)
end

local function on_subreason_sub1_tgltoggle_change(tgl)
	if tgl.value then
		-- reason = Ref.SubReason.sub1.lbName.text
		reason = 1
	end
end

local function on_subreason_sub2_tgltoggle_change(tgl)
	if tgl.value then
		-- reason = Ref.SubReason.sub2.lbName.text
		reason = 2
	end
end

local function on_subreason_sub3_tgltoggle_change(tgl)
	if tgl.value then
		-- reason = Ref.SubReason.sub3.lbName.text
		reason = 3
	end
end

local function on_subreason_sub4_tgltoggle_change(tgl)
	if tgl.value then
		-- reason = Ref.SubReason.sub4.lbName.text
		reason = 4
	end
end

local function on_subreason_sub5_tgltoggle_change(tgl)
	if tgl.value then
		-- reason = Ref.SubReason.sub5.lbName.text
		reason = 5
	end
end

local function on_subreason_btnsubmit_click(btn)
	local context = Ref.SubReason.SubInput.inpInput.text
	on_set_reason_callback(reason, context)
	libunity.SetActive(Ref.SubReason.root, false)
end

local function on_ui_init()
	local projectId = UI_DATA.WNDSelectStore.projectId
	local Project = DY_DATA.ProjectList[projectId]
	
	MaterList = Project.MaterList
	if MaterList == nil then return end
	Ref.SubMain.GrpContent:dup(#MaterList, function ( i, Ent, isNew)
		local Mater = MaterList[i]
		Ent.lbName.text = Mater.name
	end)
end

local function init_view()
	Ref.SubTop.btnBack.onAction = on_subtop_btnback_click
	Ref.SubTop.btnSave.onAction = on_subtop_btnsave_click
	Ref.SubMain.GrpContent.Ent.spState.onAction = on_submain_grpcontent_enthard_spstate_click
	Ref.SubMain.GrpContent.Ent.spPhoto.onAction = on_submain_grpcontent_enthard_spphoto_click
	UIMGR.make_group(Ref.SubMain.GrpContent, function (New, Ent)
		New.spState.onAction = Ent.spState.onAction
		New.spPhoto.onAction = Ent.spPhoto.onAction
	end)
	--!*以上：自动注册的回调函数*--
end

local function init_logic()
	NW.subscribe("WORK.SC.GETMATER", on_ui_init)
	libunity.SetActive(Ref.SubReason.root, false)

	local projectId = UI_DATA.WNDSelectStore.projectId
	local Project = DY_DATA.ProjectList[projectId]
	if Project.MaterList == nil then
		local nm = NW.msg("WORK.CS.GETMATER")
		nm:writeU32(projectId)
		NW.send(nm)
		return
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
	NW.unsubscribe("WORK.SC.GETMATER", on_ui_init)
end

local P = {
	start = start,
	update_view = update_view,
	on_recycle = on_recycle,
}
return P

