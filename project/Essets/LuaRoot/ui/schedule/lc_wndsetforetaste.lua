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

local ProductList
--!*以下：自动生成的回调函数*--

local function on_submain_grp_ent_click(btn)
	
	libunity.SetActive(Ref.SubSet.root, true)
end

local function on_submain_grp_btnsave_click(btn)
	UIMGR.close_window(Ref.root)
end

local function on_subtop_btnclear_click(btn)
	
end

local function on_subtop_btnback_click(btn)
	UIMGR.close_window(Ref.root)
end

local function on_subset_btnsubmit_click(btn)
	libunity.SetActive(Ref.SubSet.root, false)	
end

local function on_subset_btnback_click(btn)
	libunity.SetActive(Ref.SubSet.root, false)
end

local function on_ui_init()
end

local function init_view()
	Ref.SubMain.Grp.Ent.btn.onAction = on_submain_grp_ent_click
	Ref.SubMain.Grp.btnSave.onAction = on_submain_grp_btnsave_click
	Ref.SubTop.btnClear.onAction = on_subtop_btnclear_click
	Ref.SubTop.btnBack.onAction = on_subtop_btnback_click
	Ref.SubSet.btnSubmit.onAction = on_subset_btnsubmit_click
	Ref.SubSet.btnBack.onAction = on_subset_btnback_click
	UIMGR.make_group(Ref.SubMain.Grp, function (New, Ent)
		New.btn.onAction = Ent.btn.onAction
	end)
	--!*以上：自动注册的回调函数*--
end

local function init_logic()

	NW.subscribe("WORK.SC.GETPRODUCT", on_ui_init)
	libunity.SetActive(Ref.SubSet.root, false)

	local projectId = UI_DATA.WNDSubmitSchedule.projectId
	local Project = DY_DATA.ProjectList[projectId]
	if Project == nil then print("Project 为空"..projectId) return end
	if Project.ProductList == nil then
		local nm = NW.msg("WORK.CS.GETPRODUCT")
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

