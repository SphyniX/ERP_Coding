--
-- @file    ui/attendance/lc_wndsupselectproject.lua
-- @authors zl
-- @date    2016-08-28 17:50:22
-- @desc    WNDSupSelectProject
--

local ipairs, pairs
    = ipairs, pairs
local libugui = require "libugui.cs"
local libunity = require "libunity.cs"
local UIMGR = MERequire "ui/uimgr"
local NW = MERequire "network/networkmgr"
local DY_DATA = MERequire "datamgr/dydata.lua"
local UI_DATA = MERequire "datamgr/uidata.lua"
local Ref
local ProjectList
--!*以下：自动生成的回调函数*--

local function on_subproject_grpproject_entproject_click(btn)
	local index = tonumber(btn.name:sub(11))
	local id = ProjectList[index].id
	local on_call_back = UI_DATA.WNDSelectProject.on_call_back
	if on_call_back ~= nil then on_call_back(id) end
	UIMGR.close_window(Ref.root)
end

local function on_subtop_btnback_click(btn)
	UIMGR.close_window(Ref.root)
end

local function on_ui_init()
	print("try init ui")
	ProjectList = DY_DATA.get_project_list(false)
	if ProjectList == nil then 
		libunity.SetActive(Ref.SubProject.spNil, true)
		return end
	libunity.SetActive(Ref.SubProject.spNil, #ProjectList == 0)
	Ref.SubProject.GrpProject:dup(#ProjectList, function ( i, Ent, isNew)
		local Project = ProjectList[i]
		Ent.lbName.text = Project.name
		print(Ent.spIcon, Project.icon)
		UIMGR.get_photo(Ent.spIcon, Project.icon)
	end)
end

local function init_view()
	Ref.SubProject.GrpProject.Ent.btn.onAction = on_subproject_grpproject_entproject_click
	Ref.SubTop.btnBack.onAction = on_subtop_btnback_click
	UIMGR.make_group(Ref.SubProject.GrpProject, function (New, Ent)
		New.btn.onAction = Ent.btn.onAction
	end)
	--!*以上：自动注册的回调函数*--
end

local function init_logic()
	NW.subscribe("WORK.SC.GETPROJECT",on_ui_init)

	if DY_DATA.ProjectList == nil or next(DY_DATA.ProjectList) == nil then
		print("ProjectList is nil")
		if NW.connected() then
			local nm = NW.msg("WORK.CS.GETPROJECT")
		    nm:writeU32(DY_DATA.User.id)
		    NW.send(nm)
		    return
		end
	end
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
	NW.unsubscribe("WORK.SC.GETPROJECT",on_ui_init)
end

local P = {
	start = start,
	update_view = update_view,
	on_recycle = on_recycle,
}
return P

