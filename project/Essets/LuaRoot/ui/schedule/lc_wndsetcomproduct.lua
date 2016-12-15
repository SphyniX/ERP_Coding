--
-- @file    ui/schedule/lc_wndsetcomproduct.lua
-- @authors zl
-- @date    2016-10-17 01:11:09
-- @desc    WNDSetCompeteProduct
--

local ipairs, pairs
    = ipairs, pairs
local libugui = require "libugui.cs"
local libunity = require "libunity.cs"
local UIMGR = MERequire "ui/uimgr"
local UI_DATA = MERequire "datamgr/uidata.lua"
local NW = MERequire "network/networkmgr"
local DY_DATA = MERequire "datamgr/dydata.lua"
local Ref
--!*以下：自动生成的回调函数*--
local ComList



local function on_submain_grp_ent_click(btn)
	UI_DATA.WNDSetComPhoto.id = btn.name:sub(4)
	UIMGR.create_window("UI/WNDSetComPhoto")
end

local function on_subtop_btnback_click(btn)
	if not UI_DATA.WNDSubmitSchedule.WNDSetComProductNWStata then
		_G.UI.Toast:make(nil, "网络请求失败，请重新登陆"):show()
	end

	UIMGR.close_window(Ref.root)
end


local function on_ui_init(NWStata)
	print("竞品分析回调" .. tostring(NWStata))
	UI_DATA.WNDSubmitSchedule.WNDSetComProductNWStata=NWStata
	if UI_DATA.WNDSubmitSchedule.ComList == nil then
    	UI_DATA.WNDSubmitSchedule.ComList = {}
   	end
	local projectId = UI_DATA.WNDSubmitSchedule.projectId
 	local Project = DY_DATA.SchProjectList[projectId]

 	ComList = Project.ComList
 	if ComList == nil then
		libunity.SetActive(Ref.SubMain.Grp.spNil, true)
		return 
	end
	libunity.SetActive(Ref.SubMain.Grp.spNil, #ComList == 0)
	Ref.SubMain.Grp:dup(#ComList, function (i, Ent, isNew)
		local Com = ComList[i]
		Ent.lbName.text = Com.name
		UIMGR.get_photo(Ent.spIcon, Com.icon)
	end)
	local ComListUpdate = UI_DATA.WNDSubmitSchedule.ComList
	if ComListUpdate ~= nil then
		Ref.SubMain.Grp:dup(#ComListUpdate, function (i, Ent, isNew)
			local Com = ComListUpdate[i]
			Ent.lbName.text = Com.name
			UIMGR.get_photo(Ent.spIcon, Com.icon)
		end)
	end


end


local function init_view()
	Ref.SubMain.Grp.Ent.btn.onAction = on_submain_grp_ent_click
	Ref.SubTop.btnBack.onAction = on_subtop_btnback_click
	UIMGR.make_group(Ref.SubMain.Grp, function (New, Ent)
		New.btn.onAction = Ent.btn.onAction
	end)
	--!*以上：自动注册的回调函数*--
end


local function on_ui_initBack()

		on_ui_init(true)
end

local function init_logic()
	NW.subscribe("WORK.SC.GETCOMLIST",on_ui_initBack)

	local projectId = UI_DATA.WNDSubmitSchedule.projectId
	local Project = DY_DATA.SchProjectList[projectId]
	if Project == nil then print("Project 为空"..projectId) return end
	if Project.ComList == nil or next(Project.ComList) == nil then
		local nm = NW.msg("WORK.CS.GETCOMLIST")
		nm:writeU32(projectId)
		NW.send(nm)
		return
	end
	print("ComList init:"..#Project.ComList..JSON:encode(Project.ComList))
	on_ui_init(false)


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

