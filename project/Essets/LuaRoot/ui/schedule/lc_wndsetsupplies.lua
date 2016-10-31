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
local state, stateIndex
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

local function on_set_state_callback(state)
	if reasonIndex == nil then return end
	local Ent = Ref.SubMain.GrpContent.Ents[stateIndex]
	if Ent then
		Ent.lbState.text = state

		-- local id = MaterList[reasonIndex].id
		-- InfoList[id] = {state = state}
	end
end

--!*以下：自动生成的回调函数*--

local function on_submain_grp_ent_btnstate_click(btn)
	local index = tonumber(btn.transform.parent.name:sub(4))
	print(index)
	libunity.SetActive(Ref.SubState.root, true)
end

local function on_submain_grp_ent_btnphoto_click(btn)
	-- 上传图片
end

local function on_subtop_btnclear_click(btn)
	Ref.SubMain.Grp:dup(#MaterList, function (i, Ent, isNew)
		Ent.inpInput.text = nil
	end)
end

local function on_subtop_btnback_click(btn)
	UIMGR.close_window(Ref.root)
end

local function on_substate_tglgood_change(tgl)
	on_set_state_callback("完好")
	libunity.SetActive(Ref.SubState.root, false)
end

local function on_substate_tglbad_change(tgl)
	on_set_state_callback("反馈问题")
	libunity.SetActive(Ref.SubState.root, false)	
end

local function on_substate_btnback_click(btn)
	libunity.SetActive(Ref.SubState.root, false)
end

local function on_btnsave_click(btn)
	
end

local function on_submain_grp_ent_click(btn)
	
end

local function on_submain_grp_btnsave_click(btn)
	UIMGR.close_window(Ref.root)
end

local function on_ui_init()
	local projectId = UI_DATA.WNDSubmitSchedule.projectId
	local storeId = UI_DATA.WNDSubmitSchedule.storeId


	MaterList = DY_DATA.SchProjectList[projectId].MaterList
	Ref.SubMain.Grp:dup(#MaterList, function (i, Ent, isNew)
		Ent.lbName.text = MaterList[i].name
	end)
end

local function init_view()
	Ref.SubMain.Grp.Ent.btnState.onAction = on_submain_grp_ent_btnstate_click
	Ref.SubMain.Grp.Ent.btnPhoto.onAction = on_submain_grp_ent_btnphoto_click
	Ref.SubTop.btnClear.onAction = on_subtop_btnclear_click
	Ref.SubTop.btnBack.onAction = on_subtop_btnback_click
	Ref.SubState.tglGood.onAction = on_substate_tglgood_change
	Ref.SubState.tglBad.onAction = on_substate_tglbad_change
	Ref.SubState.btnBack.onAction = on_substate_btnback_click
	Ref.btnSave.onAction = on_btnsave_click
	UIMGR.make_group(Ref.SubMain.Grp, function (New, Ent)
		New.btnState.onAction = Ent.btnState.onAction
		New.btnPhoto.onAction = Ent.btnPhoto.onAction
	end)
	--!*以上：自动注册的回调函数*--
end

local function init_logic()
	NW.subscribe("WORK.SC.GETMATER", on_ui_init)
	libunity.SetActive(Ref.SubState.root, false)

	local projectId = UI_DATA.WNDSubmitSchedule.projectId
	local storeId = UI_DATA.WNDSubmitSchedule.storeId

	local Project = DY_DATA.SchProjectList[projectId]
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

