--
-- @file    ui/work/lc_wndsupnewtask.lua
-- @authors zl
-- @date    2016-09-13 19:31:59
-- @desc    WNDSupNewTask
--

local ipairs, pairs
    = ipairs, pairs
local libugui = require "libugui.cs"
local libunity = require "libunity.cs"
local UIMGR = MERequire "ui/uimgr"
local Ref
--!*以下：自动生成的回调函数*--

local function on_submain_subday_grp_btnadd_click(btn)
	-- 添加时间
end

local function on_submain_subday_grp_entday_btndelete_click(btn)
	-- 删除时间
end

local function on_submain_subday_grp_entday_substart_click(btn)
	-- 修改开始时间

end

local function on_submain_subday_grp_entday_subend_click(btn)
	-- 修改结束时间
end

local function on_submain_subpersons_grp_btnadd_click(btn)
	-- 添加人员
end

local function on_submain_subpersons_grp_entperson_btndelete_click(btn)
	-- 删除人员
end

local function on_submain_btnsubmit_click(btn)
	-- 下发新任务
end

local function on_subtop_btnback_click(btn)
	UIMGR.close_window(Ref.root)
end

local function init_view()
	Ref.SubMain.SubDay.Grp.btnAdd.onAction = on_submain_subday_grp_btnadd_click
	Ref.SubMain.SubDay.Grp.Ent.btnDelete.onAction = on_submain_subday_grp_entday_btndelete_click
	Ref.SubMain.SubDay.Grp.Ent.SubStart.btn.onAction = on_submain_subday_grp_entday_substart_click
	Ref.SubMain.SubDay.Grp.Ent.SubEnd.btn.onAction = on_submain_subday_grp_entday_subend_click
	Ref.SubMain.SubPersons.Grp.btnAdd.onAction = on_submain_subpersons_grp_btnadd_click
	Ref.SubMain.SubPersons.Grp.Ent.btnDelete.onAction = on_submain_subpersons_grp_entperson_btndelete_click
	Ref.SubMain.btnSubmit.onAction = on_submain_btnsubmit_click
	Ref.SubTop.btnBack.onAction = on_subtop_btnback_click
	UIMGR.make_group(Ref.SubMain.SubDay.Grp, function (New, Ent)
		New.btnDelete.onAction = Ent.btnDelete.onAction
		New.SubStart.btn.onAction = Ent.SubStart.btn.onAction
		New.SubEnd.btn.onAction = Ent.SubEnd.btn.onAction
	end)
	UIMGR.make_group(Ref.SubMain.SubPersons.Grp, function (New, Ent)
		New.btnDelete.onAction = Ent.btnDelete.onAction
	end)
	--!*以上：自动注册的回调函数*--
end

local function init_logic()
	
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

