--
-- @file    ui/schedule/lc_wndsetcompeteproduct.lua
-- @authors ckxz
-- @date    2016-07-28 18:54:14
-- @desc    WNDSetCompeteProduct
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

local Project, CompeteProductList

local callback = nil
--!*以下：自动生成的回调函数*--

local function on_subtop_btnback_click(btn)
	UIMGR.close_window(Ref.root)
end

local function on_subtop_btnsave_click(btn)
	-- local SubmitList = UI_DATA.WNDSubmitSchedule.CompeteProductList
	local SubmitList = {}
	local isNil = true
	for i,v in ipairs(CompeteProductList) do
		local Ent = Ref.SubMain.Grp.Ents[i]
		local ComPro = CompeteProductList[i]
		
		local id = ComPro.id
		if id == nil or id == "" then 
			_G.UI.Toast:make(nil, "数据异常"):show()
			return
		end
		local TitleList = ComPro.TitleList

		for j,w in ipairs(TitleList) do
			local EntJ = Ent.Grp.Ents[j]
			local value = EntJ.inpValue.text
			-- if value == nil or value == "" then 
			-- 	_G.UI.Toast:make(nil, "有数据未填写"):show()
			-- 	return
			-- end
			if value ~= "" then 
				isNil = false 
				table.insert(SubmitList, {id = w.id, value = value} )
			end
		end
	end
	if isNil then 
		_G.UI.Toast:make(nil, "数据不能全为空"):show()
		return
	end
	if callback then UIMGR.close_window(Ref.root) callback(SubmitList) return end
	UI_DATA.WNDSubmitSchedule.CompeteProductList = SubmitList
	UIMGR.close_window(Ref.root)
end

local function on_ui_init()
	CompeteProductList = Project.CompeteProductList
	if CompeteProductList == nil then return end
	Ref.SubMain.Grp:dup( #CompeteProductList, function (i, Ent,  isNew)
		local ComPro = CompeteProductList[i]
		Ent.lbName.text = ComPro.name
		UIMGR.get_photo(Ent.spIcon, ComPro.icon)
		local TitleList = ComPro.TitleList
		UIMGR.dup_new_group(Ent, Ent.go, "Grp", Ref.SubInfo.root, #TitleList, function (j, EntJ, isNewJ)
			local Title = TitleList[j]
			EntJ.lbName.text = Title.name
		end)
	end)
end

local function init_view()
	Ref.SubTop.btnBack.onAction = on_subtop_btnback_click
	Ref.SubTop.btnSave.onAction = on_subtop_btnsave_click
	UIMGR.make_group(Ref.SubInfo)
	UIMGR.make_group(Ref.SubMain.Grp)
	--!*以上：自动注册的回调函数*--
end

local function init_logic()
	NW.subscribe("WORK.SC.GETCOMLIST", on_ui_init)
	callback = UI_DATA.WNDSetCompeteProduct.callback
	UI_DATA.WNDSetCompeteProduct.callback = nil
	local type = UIMGR.get_ui_type()
	
	libunity.SetActive(Ref.SubMain.SubHard.spRed, type == 1)
	libunity.SetActive(Ref.SubMain.SubHard.spBlue, type == 2)
	local projectId = UI_DATA.WNDSubmitSchedule.projectId
	Project = DY_DATA.SchProjectList[projectId]
	if Project == nil then return end

	if Project.CompeteProductList == nil then
		local nm = NW.msg("WORK.CS.GETCOMLIST")
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
	NW.unsubscribe("WORK.SC.GETCOMLIST", on_ui_init)
end

local P = {
	start = start,
	update_view = update_view,
	on_recycle = on_recycle,
}
return P

