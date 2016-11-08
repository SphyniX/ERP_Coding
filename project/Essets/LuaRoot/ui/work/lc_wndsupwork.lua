--
-- @file    ui/work/lc_wndsupwork.lua
-- @authors cks
-- @date    2016-11-03 22:34:56
-- @desc    WNDSupWork
--

local ipairs, pairs
    = ipairs, pairs
local libugui = require "libugui.cs"
local libunity = require "libunity.cs"
local UIMGR = MERequire "ui/uimgr"
local UI_DATA = MERequire "datamgr/uidata.lua"
local NW= MERequire "network/networkmgr"
local DY_DATA = MERequire "datamgr/dydata.lua"
	-- body
local Ref
--!*以下：自动生成的回调函数*--

local function on_subproject_grpproject_entproject_btnbutton_click(btn)
	WNDSupWork.btnName=btn.name
	UIMGR.create_window( "UI/WNDsuoWorkSelectStore")
end

local function on_subtop_btnbrand_click(btn)
	
end

local function on_subbtm_spatt_click(btn)
	UIMGR.create_window("UI/WNDSupAttendance")
end

local function on_subbtm_btnsch_click(btn)
-- ##	UIMGR.WNDStack:pop()
	UIMGR.create_window("UI/WNDSupSchedule")
end

local function on_subbtm_btnmsg_click(btn)
-- ##	UIMGR.WNDStack:pop()
	UIMGR.create_window("UI/WNDSupMsg")
end

local function on_subbtm_btnuser_click(btn)
-- ##	UIMGR.WNDStack:pop()
	UIMGR.create_window("UI/WNDSupUser")
end

local function on_ui_init()
	 local ProjectList = DY_DATA.WorkProjectList
	 if ProjectList==nil then
	 	print("<color=#0f0> lc_wndsupwork.lua   项目列表获取失败</color>")
	 else
print("<color=#0f0> lc_wndsupwork.lua   项目列表获取成功</color>")
	 end

	Ref.SubProject.GrpProject:dup( #ProjectList, function (i, Ent, isNew)
	local Project = ProjectList[i]
	local obj = libunity.FindGameObject(Ent,"btnButton")
	if obj then
		print("<color=#0f0> 查找物体成功</color>"..obj.name)

		obj.name=Project.projectId
		else
			print("<color=#0f0> 查找物体失败</color>")		
		end
	Ent.lbText.text=Project.productName

	end)

	-- body
end 


local function init_view()
	Ref.SubProject.GrpProject.Ent.btnButton.onAction = on_subproject_grpproject_entproject_btnbutton_click
	Ref.SubTop.btnBrand.onAction = on_subtop_btnbrand_click
	Ref.SubBtm.spAtt.onAction = on_subbtm_spatt_click
	Ref.SubBtm.btnSch.onAction = on_subbtm_btnsch_click
	Ref.SubBtm.btnMsg.onAction = on_subbtm_btnmsg_click
	Ref.SubBtm.btnUser.onAction = on_subbtm_btnuser_click
	UIMGR.make_group(Ref.SubProject.GrpProject, function (New, Ent)
		New.btnButton.onAction = Ent.btnButton.onAction
	end)
	--!*以上：自动注册的回调函数*--
end

local function init_logic()
	NW.subscribe("WORK.SC.GETPROJECT", on_ui_init)

	if DY_DATA.ProjectList == nil or next(DY_DATA.ProjectList) == nil then
		if NW.connected() then
			local nm = NW.msg("WORK.CS.GETPROJECT")
			nm:writeU32(DY_DATA.User.id)
			NW.send(nm)
		end
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
	
end

local P = {
	start = start,
	update_view = update_view,
	on_recycle = on_recycle,
}
return P

