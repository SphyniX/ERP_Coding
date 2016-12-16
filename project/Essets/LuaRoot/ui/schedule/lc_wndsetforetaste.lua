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
local function stringTypeCtrl(str)
	local strTemp=string.gsub(str,"<size=36>","")
	strTemp=string.gsub(strTemp,"</size>","")
	strTemp=string.sub(strTemp,1,#strTemp-3)
	local n= tonumber(strTemp)
	print("--------------stringTypeCtr1---strTemp------"..tostring(n))
	return strTemp
end
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
	UI_DATA.WNDSubmitSchedule.ProductListForetasteNWStata = true
	UIMGR.close_window(Ref.root)
end

local function on_subset_btnsubmit_click(btn)

	Ref.SubMain.Grp:dup(#SampleList, function (i, Ent, isNew)
		if i == NowEnt then 
			Ent.lbVolume.text =  "<size=36>" .. Ref.SubSet.inpVolume.text.. "</size>" .. SampleList[i].per 
			Ent.lbNumber.text =  "<size=36>" .. Ref.SubSet.inpNumber.text.. "</size>" .."次" 
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
	print("UI_DATA.WNDSubmitSchedule.ProductListForetasteNWStata"..tostring(UI_DATA.WNDSubmitSchedule.ProductListForetasteNWStata))
	if not UI_DATA.WNDSubmitSchedule.ProductListForetasteNWStata then
		_G.UI.Toast:make(nil, "网络请求失败，请重新登陆"):show()
	end
	UI_DATA.WNDSubmitSchedule.ProductListForetasteNWStata = true

	ProductListForUpdate = {}
	print("#SampleList".. tostring(#SampleList) .. JSON:encode(SampleList))
	Ref.SubMain.Grp:dup(#SampleList, function (i, Ent, isNew)
		local id = SampleList[i].id
		local value = stringTypeCtrl(Ent.lbVolume.text)
		local number = stringTypeCtrl(Ent.lbNumber.text)
		if value ~= "   " then
			table.insert(ProductListForUpdate,{id = id ,value =value,number = number})
		end
	end)
	UI_DATA.WNDSubmitSchedule.ProductListForetaste = ProductListForUpdate

	UIMGR.close_window(Ref.root)
end

local function on_submain_grp_btnsave_click(btn)
	UIMGR.close_window(Ref.root)
end

local function on_ui_init(NWStata)
	print("回调--------------体验品"..tostring(NWStata))
	UI_DATA.WNDSubmitSchedule.ProductListForetasteNWStata = NWStata

	UI_DATA.WNDSubmitSchedule.ProductListForetaste = {}
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
		Ent.lbNumber.text = "   ".. "次"
	end)
	ProductListForUpdate = UI_DATA.WNDSubmitSchedule.ProductListForetaste

	if ProductListForUpdate ~= nil and next(ProductListForUpdate) ~=nil then 
		print("ProductListForUpdate in WNDSetForetaste is :" .. JSON:encode(ProductListForUpdate))
		Ref.SubMain.Grp:dup(#SampleList, function (i, Ent, isNew)
			local Sample = SampleList[i]
			-- Ent.lbName.text = Sample.name
			Ent.lbVolume.text = "<size=36>" .. ProductListForUpdate[i].value .. "</size>" .. Sample.per
			Ent.lbNumber.text = "<size=36>" .. ProductListForUpdate[i].number .. "</size>" .. "次"
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

	
	NW.subscribe("REPORTED.SC.GETSAMPLE", on_ui_initBack)
	libunity.SetActive(Ref.SubSet.root, false)

	local projectId = UI_DATA.WNDSubmitSchedule.projectId
	local Project = DY_DATA.SchProjectList[projectId]
	--if Project == nil then print("Project 为空"..projectId) return end
	if Project.SampleList == nil or next(Project.SampleList) == nil then
		local nm = NW.msg("REPORTED.CS.GETSAMPLE")
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

