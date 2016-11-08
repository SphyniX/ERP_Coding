--
-- @file    ui/schedule/lc_wndsupschedule.lua
-- @authors cks
-- @date    2016-11-06 17:57:08
-- @desc    WNDSupSchedule
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
local SchProjectLis
local TempSchProjectList
--!*以下：自动生成的回调函数*--

local function on_ui_init(brandid)
	if brandid == nil then brandid = 0 end
	SchProjectLis = DY_DATA.get_schproject_list(false)
	if SchProjectLis == nil then 
		print("SchProjectLis is nil")
		libunity.SetActive(Ref.SubProject.spNil, true)
		return 
	end
	libunity.SetActive(Ref.SubProject.spNil, #SchProjectLis == 0)
	print("SchProjectLis is "..#SchProjectLis)
	TempSchProjectList = {}
	if brandid == 0 then
		TempSchProjectList = SchProjectLis
	else
		for i=1,#SchProjectLis do
			if SchProjectLis[i].brandnum == brandid then
				table.insert(TempSchProjectList,SchProjectLis[i])
			end
		end
	end
	Ref.SubProject.GrpProject:dup(#TempSchProjectList, function (i, Ent, isNew)
		local Project = TempSchProjectList[i]
		Ent.SubWorkProject.lbText.text = Project.name
		UIMGR.get_photo(Ent.spIcon, Project.icon)
	end)
end

local function on_brand_select_callback(brand)
	-- body
	on_ui_init(brand.id)
end

local function on_subtop_btnbrand_click(btn)
	UI_DATA.WNDSelectBrand.callbackfunc = on_brand_select_callback
	UIMGR.create("UI/WNDSelectBrand")
end



local function on_subbtm_spatt_click(btn)
		-- ##	UIMGR.WNDStack:pop()
	UIMGR.create_window("UI/WNDSupAttendance")
end

local function on_subbtm_btnwork_click(btn)
	-- ##	UIMGR.WNDStack:pop()
	UIMGR.create_window("UI/WNDSupWork")
end

local function on_subbtm_btnsch_click(btn)
	
end

local function on_subbtm_btnmsg_click(btn)
	-- ##	UIMGR.WNDStack:pop()
	UIMGR.create_window("UI/WNDSupMsg")
end

local function on_subbtm_btnuser_click(btn)
	-- ##	UIMGR.WNDStack:pop()
	UIMGR.create_window("UI/WNDSupUser")
end

local function on_subproject_grpproject_entproject_subworkproject_click(btn)
	local index = tonumber(btn.transform.parent.name:sub(11))
	print("index in on_subproject_grpproject_entproject_subworkproject_click is :"  .. index)
	print("SchProjectLis in WNDSupSchedule is :" .. JSON:encode(SchProjectLis))
	UI_DATA.WNDSelectStore.type = 2
	UI_DATA.WNDSelectStore.projectId = TempSchProjectList[index].id
	UIMGR.create_window("UI/WNDSupSelectStore")
end

local function on_subproject_grpproject_entproject_workproject_click(btn)
	
end



local function init_view()
	Ref.SubTop.BtnBrand.onAction = on_subtop_btnbrand_click
	Ref.SubBtm.spAtt.onAction = on_subbtm_spatt_click
	Ref.SubBtm.btnWork.onAction = on_subbtm_btnwork_click
	Ref.SubBtm.btnSch.onAction = on_subbtm_btnsch_click
	Ref.SubBtm.btnMsg.onAction = on_subbtm_btnmsg_click
	Ref.SubBtm.btnUser.onAction = on_subbtm_btnuser_click
	Ref.SubProject.GrpProject.Ent.SubWorkProject.btn.onAction = on_subproject_grpproject_entproject_subworkproject_click
	UIMGR.make_group(Ref.SubProject.GrpProject, function (New, Ent)
		New.SubWorkProject.btn.onAction = Ent.SubWorkProject.btn.onAction
	end)
	--!*以上：自动注册的回调函数*--
end

local function init_logic()

	NW.subscribe("WORK.SC.GETPROJECT", on_ui_init)
	
	if DY_DATA.SchProjectLis == nil or next(DY_DATA.SchProjectLis) == nil then
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
	NW.unsubscribe("ATTENCE.SC.GETWORK",on_ui_init)
end

local P = {
	start = start,
	update_view = update_view,
	on_recycle = on_recycle,
}
return P

