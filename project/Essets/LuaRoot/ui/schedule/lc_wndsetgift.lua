--
-- @file    ui/schedule/lc_wndsetgift.lua
-- @authors ckxz
-- @date    2016-07-29 13:38:12
-- @desc    WNDSetGift
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
local ProductListForUpdate 
local GiftList
local NowEnt
--!*以下：自动生成的回调函数*--

local function on_submain_grp_ent_click(btn)

	NowEnt = tonumber(btn.name:sub(4))
	print("NowEnt : " .. NowEnt)
	Ref.SubSet.lbVolumeper.text = GiftList[NowEnt].per
	libunity.SetActive(Ref.SubSet.root, true)

end

local function on_subtop_btnclear_click(btn)
	Ref.SubMain.Grp:dup(#GiftList, function (i, Ent, isNew)
			Ent.lbVolume.text = "   " .. GiftList[i].per
	end)
end

local function on_subtop_btnback_click(btn)
	UI_DATA.WNDSubmitSchedule.ProductListGiftNWStata = true
	UIMGR.close_window(Ref.root)
end

local function on_subset_btnsubmit_click(btn)

	Ref.SubMain.Grp:dup(#GiftList, function (i, Ent, isNew)
		if i == NowEnt then 
			Ent.lbVolume.text = Ref.SubSet.inpVolume.text .. GiftList[i].per
			Ref.SubSet.inpVolume.text = nil
	
		end
	end)
	libunity.SetActive(Ref.SubSet.root, false)
end

local function on_subset_btnback_click(btn)
	
	libunity.SetActive(Ref.SubSet.root, false)
	UI_DATA.WNDSubmitSchedule.ProductListGiftNWStata = true
end

local function on_btnsave_click(btn)
	if not UI_DATA.WNDSubmitSchedule.ProductListGiftNWStata then
		_G.UI.Toast:make(nil, "网络请求失败，请重新登陆"):show()
	end
	UI_DATA.WNDSubmitSchedule.ProductListGiftNWStata = true

	ProductListForUpdate = {}
	Ref.SubMain.Grp:dup(#GiftList, function (i, Ent, isNew)
		local id = GiftList[i].id
		local volume = Ent.lbVolume.text:sub(1,string.len(Ent.lbVolume.text)-3)
		-- local number = Ent.lbNumber.text
		if volume == "   " then volume = 0 end
		local volumeNumber = tonumber(volume)
		table.insert(ProductListForUpdate,{id = id ,volume = volume})

		end)
	UI_DATA.WNDSubmitSchedule.ProductListGift = ProductListForUpdate
	UIMGR.close_window(Ref.root)
end

local function on_ui_init(NWStata)
	print("赠品回调"..tostring(NWStata))
	UI_DATA.WNDSubmitSchedule.ProductListGiftNWStata = NWStata
	UI_DATA.WNDSubmitSchedule.ProductListGift = {}
	libunity.SetActive(Ref.SubSet.root,false)
	local projectId = UI_DATA.WNDSubmitSchedule.projectId
	local Project = DY_DATA.SchProjectList[projectId]

	if Project == nil then return end

	if Project.GiftList == nil then Project.GiftList = {} end

	GiftList = Project.GiftList
	print("GiftList is :" .. JSON:encode(GiftList))
	if GiftList == nil then
		libunity.SetActive(Ref.SubMain.Grp.spNil, true)
		return 
	end
	libunity.SetActive(Ref.SubMain.Grp.spNil, #GiftList == 0)
	Ref.SubMain.Grp:dup(#GiftList, function (i, Ent, isNew)
		local Gift = GiftList[i]
		Ent.lbName.text = Gift.name
		Ent.lbVolume.text = "   " .. Gift.per

	end)
	ProductListForUpdate = UI_DATA.WNDSubmitSchedule.ProductListGift
	if ProductListForUpdate ~= nil then
		Ref.SubMain.Grp:dup(#GiftList, function (i, Ent, isNew)
			local Gift = GiftList[i]	
			Ent.lbVolume.text = ProductListForUpdate[i].volume .. Gift.per
		end)
	end

	
end

local function init_view()
	Ref.SubMain.Grp.Ent.btn.onAction = on_submain_grp_ent_click
	Ref.SubTop.btnClear.onAction = on_subtop_btnclear_click
	Ref.SubTop.btnBack.onAction = on_subtop_btnback_click
	Ref.SubSet.btnSubmit.onAction = on_subset_btnsubmit_click
	Ref.SubSet.btnBack.onAction = on_subset_btnback_click
	Ref.btnSave.onAction = on_btnsave_click
	UIMGR.make_group(Ref.SubMain.Grp, function (New, Ent)
		New.btn.onAction = Ent.btn.onAction
	end)
	--!*以上：自动注册的回调函数*--
end
local function on_ui_initBack()
		on_ui_init(true)
end
local function init_logic()
	NW.subscribe("REPORTED.SC.GETGIFT", on_ui_initBack)
	libunity.SetActive(Ref.SubSet.root, false)
	
	local projectId = UI_DATA.WNDSubmitSchedule.projectId
	local Project = DY_DATA.SchProjectList[projectId]
	if Project == nil then print("Project 为空 Project 编号 ："..projectId) return end
	if Project.GiftList == nil then
		local nm = NW.msg("REPORTED.CS.GETGIFT")
		nm:writeU32(projectId)
		NW.send(nm)
		return
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
	NW.unsubscribe("WORK.SC.GETPRODUCT", on_ui_init)
end

local P = {
	start = start,
	update_view = update_view,
	on_recycle = on_recycle,
}
return P

