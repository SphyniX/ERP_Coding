--
-- @file    ui/schedule/lc_wndsetpromoteinfo.lua
-- @authors ckxz
-- @date    2016-07-28 18:12:56
-- @desc    WNDSetPromoteInfo
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

local MechanismList
--!*以下：自动生成的回调函数*--

local function on_subtop_btnback_click(btn)
	UIMGR.close_window(Ref.root)
end

local function on_subtop_btnsave_click(btn)
	local SubmitList = UI_DATA.WNDSubmitSchedule.MechanismList
	if SubmitList == nil then SubmitList = {} end
	local isNil = true
	for i,v in ipairs(MechanismList) do
		local Ent = Ref.SubMain.GrpContent.Ents[i]
		local id = v.id
		local value = Ent.inpValue.text
		-- if value == nil or value == "" then 
		-- 	_G.UI.Toast:make(nil, "有数据未填写"):show()
		-- 	return
		-- end
		if value ~= "" then 
			isNil = false
			table.insert(SubmitList, {id = id, value = value})
		end
	end
	if isNil then 
		_G.UI.Toast:make(nil, "数据不能全为空"):show()
		return
	end
	UI_DATA.WNDSubmitSchedule.MechanismList = SubmitList
	UIMGR.close_window(Ref.root)
end

local function on_ui_init()
	local projectId = UI_DATA.WNDSubmitSchedule.projectId
	local Project = DY_DATA.SchProjectList[projectId]
	if Project == nil then print("Project 为空"..projectId) return end
	MechanismList = Project.MechanismList
	if MechanismList == nil then
		return
	end

	Ref.SubMain.GrpContent:dup(#MechanismList, function ( i, Ent, isNew)
		local Mechanism = MechanismList[i]
		Ent.lbName.text = Mechanism.name
	end)
end


local function init_view()
	Ref.SubTop.btnBack.onAction = on_subtop_btnback_click
	Ref.SubTop.btnSave.onAction = on_subtop_btnsave_click
	UIMGR.make_group(Ref.SubMain.GrpContent)
	--!*以上：自动注册的回调函数*--
end

local function init_logic()
	NW.subscribe("WORK.SC.GETMECHANISM", on_ui_init)
	local projectId = UI_DATA.WNDSubmitSchedule.projectId
	local Project = DY_DATA.SchProjectList[projectId]
	if Project == nil then print("Project 为空"..projectId) return end
	if Project.MechanismList == nil then
		local nm = NW.msg("WORK.CS.GETMECHANISM")
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
	NW.unsubscribe("WORK.SC.GETMECHANISM", on_ui_init)
end

local P = {
	start = start,
	update_view = update_view,
	on_recycle = on_recycle,
}
return P

