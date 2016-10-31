--
-- @file    ui/schedule/lc_wndsetforetaste.lua
-- @authors ckxz
-- @date    2016-07-28 19:08:30
-- @desc    WNDSetForetaste
--

local ipairs, pairs
    = ipairs, pairs
local libugui = require "libugui.cs"
local libunity = require "libunity.cs"
local UIMGR = MERequire "ui/uimgr"
local DY_DATA = MERequire "datamgr/dydata.lua"
local UI_DATA = MERequire "datamgr/uidata.lua"
local NW = MERequire "network/networkmgr"
local Ref
local ProductListForUpdate

local SampleList 
local NowEnt

--!*以下：自动生成的回调函数*--

local function on_submain_grp_ent_click(btn)
	
	NowEnt = tonumber(btn.name:sub(4))
	print("NowEnt : " .. NowEnt)
	Ref.SubSet.lbVolumeper.text = SampleList[NowEnt].per
	Ref.SubSet.lbNumberper.text = "人"
	libunity.SetActive(Ref.SubSet.root, true)
end

local function on_subtop_btnclear_click(btn)
	Ref.SubMain.Grp:dup(#SampleList, function (i, Ent, isNew)
	
		Ent.lbVolume.text = "   " .. SampleList[i].per
		Ent.lbNumber.text = "   " .. "次"

	end)
end

local function on_subtop_btnback_click(btn)
	UIMGR.close_window(Ref.root)
end

local function on_subset_btnsubmit_click(btn)

	Ref.SubMain.Grp:dup(#SampleList, function (i, Ent, isNew)
		if i == NowEnt then 
			Ent.lbVolume.text = Ref.SubSet.inpVolume.text .. SampleList[i].per
			Ent.lbNumber.text = Ref.SubSet.inpNumber.text .. "次"
			Ref.SubSet.inpVolume.text = nil
			Ref.SubSet.inpNumber.text = nil
		end
	end)


	libunity.SetActive(Ref.SubSet.root, false)	
end

local function on_subset_btnback_click(btn)
	libunity.SetActive(Ref.SubSet.root, false)
end

local function on_btnsave_click(btn)
	ProductListForUpdate = {}
	Ref.SubMain.Grp:dup(#SampleList, function (i, Ent, isNew)
		local id = SampleList[i].id
		local value = Ent.lbVolume.text:sub(1,string.len(Ent.lbVolume.text)-3)
		local number = Ent.lbNumber.text:sub(1,string.len(Ent.lbNumber.text)-3)
		table.insert(ProductListForUpdate,{id = id ,value =value,number = number})

		end)
	UI_DATA.WNDSubmitSchedule.ProductListForetaste = ProductListForUpdate

	UIMGR.close_window(Ref.root)
end

local function on_submain_grp_btnsave_click(btn)
	UIMGR.close_window(Ref.root)
end

local function on_ui_init()
	libunity.SetActive(Ref.SubSet.root,false)
	local projectId = UI_DATA.WNDSubmitSchedule.projectId
	local Project = DY_DATA.SchProjectList[projectId]

	if Project == nil then return end

	if Project.SampleList == nil then Project.SampleList = {} end

	SampleList = Project.SampleList
	if SampleList == nil then
		libunity.SetActive(Ref.SubMain.Grp.spNil, true)
		return 
	end
	libunity.SetActive(Ref.SubMain.Grp.spNil, #SampleList == 0)
	Ref.SubMain.Grp:dup(#SampleList, function (i, Ent, isNew)
		local Sample = SampleList[i]
		Ent.lbName.text = Sample.name
		Ent.lbVolume.text = "   " .. Sample.per
		Ent.lbNumber.text = "   " .. "次"
	end)
	ProductListForUpdate = UI_DATA.WNDSubmitSchedule.ProductListForetaste

	if ProductListForUpdate ~= nil then 
		print("ProductListForUpdate in WNDSetForetaste is :" .. JSON:encode(ProductListForUpdate))
		Ref.SubMain.Grp:dup(#SampleList, function (i, Ent, isNew)
			local Sample = SampleList[i]
			-- Ent.lbName.text = Sample.name
			Ent.lbVolume.text = ProductListForUpdate[i].value .. Sample.per
			Ent.lbNumber.text = ProductListForUpdate[i].number .. "次"
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

local function init_logic()

	NW.subscribe("REPORTED.SC.GETSAMPLE", on_ui_init)
	libunity.SetActive(Ref.SubSet.root, false)

	local projectId = UI_DATA.WNDSubmitSchedule.projectId
	local Project = DY_DATA.SchProjectList[projectId]
	if Project == nil then print("Project 为空"..projectId) return end
	if Project.SampleList == nil then
		local nm = NW.msg("REPORTED.CS.GETSAMPLE")
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
	
	NW.unsubscribe("WORK.SC.GETPRODUCT", on_ui_init)
end

local P = {
	start = start,
	update_view = update_view,
	on_recycle = on_recycle,
}
return P

