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
local LOGIN = MERequire "libmgr/login.lua"
local Ref

local reason, reasonIndex, photoIndex
local state, stateIndex
local MaterList
local MaterListForUpdate
local Imagelist = {}
local InfoList = {}
local on_photo_init
local NeedChange = true

local function on_set_reason_callback(reason)
	if reasonIndex == nil then return end
	local Ent = Ref.SubMain.Grp.Ent[reasonIndex]
	if Ent then
		Ent.lbState.text = TEXT.SuppliesType[reason]
		-- Ent.lbInfo.text = context
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

	local function on_http_photo_callback(Ret)
		if photoIndex == nil then return end
		Ref.SubMain.Grp:dup(#MaterList, function (i, Ent, isNew)
			if i == photoIndex then 
				_G.UI.Toast:make(nil, "成功"):show()
				Ent.lbPhoto.text = Ret.photoid[1]
			end
		end)
		-- body
	end
	LOGIN.try_uploadphotoforreport(DY_DATA.User.id,Photolist[1].image, on_http_photo_callback)
	-- on_photo_init()
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
	NeedChange = false
	print("on_set_state_callback stateIndex is :" .. stateIndex)
	if stateIndex == nil then return end
	Ref.SubMain.Grp:dup(#MaterList, function (i, Ent, isNew)
		if i == stateIndex then 
			Ent.lbState.text = state
		end
	end)
	libunity.SetActive(Ref.SubState.root, false)
	
		-- local id = MaterList[reasonIndex].id
		-- InfoList[id] = {state = state}
	
end

--!*以下：自动生成的回调函数*--

local function on_submain_grp_ent_btnstate_click(btn)
	stateIndex = tonumber(btn.transform.parent.name:sub(4))
	Ref.SubMain.Grp:dup(#MaterList, function (i, Ent, isNew)
		if i == stateIndex then 
			if Ent.lbState.text == "完好" then
				Ref.SubState.tglGood.isOn = true
				Ref.SubState.tglGood:SetInteractable(false)
				Ref.SubState.tglBad.isOn = false
				Ref.SubState.tglBad:SetInteractable(true)
			end
			if Ent.lbState.text == "需维修/补货" then
				Ref.SubState.tglGood.isOn = false
				Ref.SubState.tglGood:SetInteractable(true)
				Ref.SubState.tglBad.isOn = true
				Ref.SubState.tglBad:SetInteractable(false)
			end
			if Ent.lbState.text == "状态" then
				Ref.SubState.tglGood.isOn = false
				Ref.SubState.tglGood:SetInteractable(true)
				Ref.SubState.tglBad.isOn = false
				Ref.SubState.tglBad:SetInteractable(true)
			end
		end
	end)
	NeedChange = true
	libunity.SetActive(Ref.SubState.root, true)
end

local function on_submain_grp_ent_btnphoto_click(btn)
	-- 上传图片
	set_photo(tonumber(btn.transform.parent.name:sub(4)))
end

local function on_subtop_btnclear_click(btn)
	Ref.SubMain.Grp:dup(#MaterList, function (i, Ent, isNew)
		Ent.inpInput.text = nil
		Ent.lbState.text = "状态"
		Ent.lbPhoto.text = nil
	end)
end

local function on_subtop_btnback_click(btn)
	UIMGR.close_window(Ref.root)
end

local function on_substate_tglgood_change(tgl)
	if NeedChange then
		on_set_state_callback("完好")
	end
	
end

local function on_substate_tglbad_change(tg2)
	if NeedChange then
		on_set_state_callback("需维修/补货")
	end

end

local function on_substate_btnback_click(btn)
	libunity.SetActive(Ref.SubState.root, false)
end

local function on_btnsave_click(btn)
	if not UI_DATA.WNDSubmitSchedule.WNDSetSuppliesNWStata then
		_G.UI.Toast:make(nil, "网络请求失败，请重新登陆"):show()
	end
	MaterListForUpdate = {}

	Ref.SubMain.Grp:dup(#MaterList, function (i, Ent, isNew)

		local name = Ent.lbName.text
		local photo = Ent.lbPhoto.text
		local state = Ent.lbState.text
		local id = MaterList[i].id
		local discribe = Ent.inpInput.text
		if state == "状态" then state = "" end
		if photo == nil then photo = "" end
		if discribe == nil then discribe = "" end
		if state ~= "" then
			table.insert(MaterListForUpdate,{id = id,name = name , photo = photo , state = state , discribe = discribe})
		end
	end)
	if DY_DATA.WNDSubmitSchedule.MaterList == nil then
		DY_DATA.WNDSubmitSchedule.MaterList = {}
	end
	DY_DATA.WNDSubmitSchedule.MaterList = MaterListForUpdate
	print("WNDSubmitSchedule.MaterList in WNDSetSupplies is :" .. JSON:encode(DY_DATA.WNDSubmitSchedule.MaterList) )
	UIMGR.close_window(Ref.root)
end

local function on_submain_grp_ent_click(btn)
	
end

local function on_submain_grp_btnsave_click(btn)
	UIMGR.close_window(Ref.root)
end

local function on_ui_init(NWStata)
	print("on_ui_init--"..tostring(NWStata))

	DY_DATA.WNDSubmitSchedule.MaterList = {}
	UI_DATA.WNDSubmitSchedule.WNDSetSuppliesNWStata=NWStata
	local projectId = UI_DATA.WNDSubmitSchedule.projectId
	local storeId = UI_DATA.WNDSubmitSchedule.storeId


	MaterList = DY_DATA.SchProjectList[projectId].MaterList
	Ref.SubMain.Grp:dup(#MaterList, function (i, Ent, isNew)
		Ent.lbName.text = MaterList[i].name
	end)

	MaterListForUpdate = UI_DATA.WNDSubmitSchedule.MaterList
	if MaterListForUpdate ~= nil then 
		Ref.SubMain.Grp:dup(#MaterListForUpdate, function (i, Ent, isNew)
			Ent.lbState.text = MaterListForUpdate[i].state
			Ent.lbPhoto.text = MaterListForUpdate[i].photo
			Ent.inpInput.text = MaterListForUpdate[i].discribe
		end)
	end
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
		UI_DATA.WNDSubmitSchedule.WNDSetSuppliesNWStata=NWStata
		New.btnState.onAction = Ent.btnState.onAction
		New.btnPhoto.onAction = Ent.btnPhoto.onAction
	end)
	--!*以上：自动注册的回调函数*--
end
local function on_ui_initBack()
		on_ui_init(true)
end
local function init_logic()
	NW.subscribe("WORK.SC.GETMATER", on_ui_initBack)
	libunity.SetActive(Ref.SubState.root, false)

	local projectId = UI_DATA.WNDSubmitSchedule.projectId
	local storeId = UI_DATA.WNDSubmitSchedule.storeId

	local Project = DY_DATA.SchProjectList[projectId]
	if Project.MaterList == nil or next(Project.MaterList) == nil then
		local nm = NW.msg("WORK.CS.GETMATER")
		nm:writeU32(projectId)
		NW.send(nm)
		return
	end
	on_ui_init(false)
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

