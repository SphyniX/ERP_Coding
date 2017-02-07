--
-- @file    ui/schedule/lc_wndselectschstore.lua
-- @authors ckxz
-- @date    2016-07-28 17:17:08
-- @desc    WNDSelectSchStore
--

local ipairs, pairs
    = ipairs, pairs
local libugui = require "libugui.cs"
local libunity = require "libunity.cs"
local UIMGR = MERequire "ui/uimgr"
local UI_DATA = MERequire "datamgr/uidata.lua"
local DY_DATA = MERequire "datamgr/dydata.lua"
local NW = MERequire "network/networkmgr"
local Ref

local StoreList
--!*以下：自动生成的回调函数*--

local function on_substore_grpstore_entstore_btnbutton_click(btn)
	local index = tonumber(btn.transform.parent.name:sub(9))
	local Store = StoreList[index]
	local on_selected = UI_DATA.WNDSelectStore.on_selected
	if on_selected then on_selected(Store.id) return end
	
	UI_DATA.WNDSubmitSchedule.projectId = Store.projectId
	UI_DATA.WNDSubmitSchedule.storeId = Store.id
	UI_DATA.WNDSubmitSchedule.state = Store.state
	UIMGR.create_window("UI/WNDSubmitSchedule")
end

local function on_subtop_btnback_click(btn)
	UIMGR.close_window(Ref.root)
end

local function on_upload_photo_callback(Ret)

	if Ret.ret == 1 then
		_G.UI.Toast:make(nil, "成功"):show()
	end
end

local function on_take_photo_call_back(image)

	LOGIN.try_uploadphotoforreport(DY_DATA.User.id,image,on_upload_photo_callback)
end 

local function on_ui_init()
	local projectId = UI_DATA.WNDSelectStore.projectId
	local Project = DY_DATA.SchProjectList[projectId]
	StoreList = Project.SchStoreList
	if StoreList == nil then
		libunity.SetActive(Ref.SubStore.spNil, true)
		return 
	end
	libunity.SetActive(Ref.SubStore.spNil, #StoreList == 0)
	print("storelist init:"..#StoreList..JSON:encode(StoreList))
	Ref.SubStore.GrpStore:dup(#StoreList, function ( i, Ent, isNew)
		local Store = StoreList[i]
		Ent.lbName.text = Store.name
		libugui.GetSetResourcesPic(Ent.spIcon,"default",nil)
		UIMGR.get_photo(Ent.spIcon, Store.icon)
		-- local clr = i % 3
		-- libunity.SetActive(Ent.spRed, clr == 1)
		-- libunity.SetActive(Ent.spBlue, clr == 2)
		-- libunity.SetActive(Ent.spYellow, clr == 0)
	end)
end

local function init_view()
	Ref.SubStore.GrpStore.Ent.btnButton.onAction = on_substore_grpstore_entstore_btnbutton_click
	Ref.SubTop.btnBack.onAction = on_subtop_btnback_click
	UIMGR.make_group(Ref.SubStore.GrpStore, function (New, Ent)
		New.btnButton.onAction = Ent.btnButton.onAction
	end)
	--!*以上：自动注册的回调函数*--
end

local function init_logic()
	print("初始化店铺")
	UI_DATA.WNDSubmitSchedule.WNDSubmitPhotoForReportNWStata = false
	UI_DATA.WNDSubmitSchedule.WNDSetPromoteInfoNWStata = false
	UI_DATA.WNDSubmitSchedule.ProductListForetasteNWStata = false
	UI_DATA.WNDSubmitSchedule.ProductListGiftNWStata = false
	UI_DATA.WNDSubmitSchedule.WNDSetComProductNWStata = false
	UI_DATA.WNDSubmitSchedule.WNDSetSuppliesNWStata = false
	UI_DATA.WNDSubmitSchedule.WNDSetInforNWStata = false
	UI_DATA.WNDSubmitSchedule.DataInitStata = false   -------控制进度界面初始化第一次

--


	NW.subscribe("WORK.SC.GETSTORE", on_ui_init)
	local projectId = UI_DATA.WNDSelectStore.projectId
	local Project = DY_DATA.SchProjectList[projectId]
	print(JSON:encode(Project))
	if Project.SchStoreList == nil or #Project.SchStoreList == 0 then
		local nm = NW.msg("WORK.CS.GETSTORE")
		nm:writeU32(projectId)
		nm:writeU32(DY_DATA.User.id)
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
	NW.unsubscribe("WORK.SC.GETSTORE", on_ui_init)
end

local P = {
	start = start,
	update_view = update_view,
	on_recycle = on_recycle,
}
return P

