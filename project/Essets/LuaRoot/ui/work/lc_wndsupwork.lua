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
local DY_DATA = MERequire "datamgr/dydata.lua"
local UI_DATA = MERequire "datamgr/uidata.lua"
local NW= MERequire "network/networkmgr"

local Ref
local ProjectList
--!*以下：自动生成的回调函数*--
local TempProjectList


local function on_ui_init(brandid)
	if brandid == nil then brandid = 0 end
	ProjectList = DY_DATA.get_project_list(false)
	if ProjectList == nil then 
		print("ProjectList is nil")
		libunity.SetActive(Ref.SubProject.spNil, true)
		return 
	end
	TempProjectList = {}
	print("ProjectList is ".. #ProjectList)
	if brandid == 0 then
		TempProjectList = ProjectList
	else
		for i=1,#ProjectList do
			if ProjectList[i].brandnum == brandid then
				table.insert(TempProjectList,ProjectList[i])
			end
		end
	end
	libunity.SetActive(Ref.SubProject.spNil, #ProjectList == 0)
	print("TempProjectList is ".. JSON:encode(TempProjectList))
	Ref.SubProject.GrpProject:dup(#TempProjectList, function (i, Ent, isNew)
		local Project = TempProjectList[i]
		Ent.lbText.text = Project.name
		UIMGR.get_photo(Ent.spIcon, Project.icon)
		-- local clr = i % 3
		-- libunity.SetActive(Ent.spRed, clr == 1)
		-- libunity.SetActive(Ent.spBlue, clr == 2)
		-- libunity.SetActive(Ent.spYellow, clr == 0)
	end)
	-- body
end 


local function on_brand_select_callback(brand)
	-- body
	on_ui_init(brand.id)
end

local function on_subproject_grpproject_entproject_btnbutton_click(btn)
	--WNDSupWork.btnName=btn.name
	local index = tonumber(btn.transform.parent.name:sub(11))
	print("lc_wndsupwork.lua--------"..btn.transform.name..tostring(btn.name:sub(11)))
	UI_DATA.WNDWorkProject.projectId = TempProjectList[index].id
	UI_DATA.WNDSelectStore.projectId = TempProjectList[index].id
	UIMGR.create( "UI/WNDSupWorkSelect")
end

local function on_subtop_btnbrand_click(btn)
	UI_DATA.WNDSelectBrand.callbackfunc = on_brand_select_callback
	UIMGR.create("UI/WNDSelectBrand")
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
	UI_DATA.WNDMsgHint.state = true
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
	UI_DATA.WNDMsgHint.state = false
	NW.unsubscribe("WORK.SC.GETPROJECT", on_ui_init)
end

local P = {
start = start,
update_view = update_view,
on_recycle = on_recycle,
}
return P

