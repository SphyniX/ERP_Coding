--
-- @file    ui/work/lc_wndsupchangetask.lua
-- @authors zl
-- @date    2016-09-14 11:17:55
-- @desc    WNDSupChangeTask
--

local ipairs, pairs
    = ipairs, pairs
local libugui = require "libugui.cs"
local libunity = require "libunity.cs"
local UIMGR = MERequire "ui/uimgr"
local Ref

local PersonList
--!*以下：自动生成的回调函数*--

local function on_submain_subday_substart_click(btn)
	-- 修改开始时间
end

local function on_submain_subday_subend_click(btn)
	-- 修改结束时间
end

local function on_submain_subpersons_grp_btnadd_click(btn)
	-- 添加人员
	Ref.SubMain.SubPersons.Grp:dup(3, nil)
end

local function on_submain_subpersons_grp_entperson_btndelete_click(btn)
	-- 删除人员
end

local function on_submain_btnsubmit_click(btn)
	-- 提交任务
end

local function on_subtop_btnback_click(btn)
	UIMGR.close_window(Ref.root)
end

local function on_subtop_btndelete_click(btn)
	-- 删除该任务
end

local function on_ui_init()
	PersonList = {
		{id = 1, name = "张三"},
		{id = 2, name = "张四"},
		{id = 3, name = "张五"},
		{id = 4, name = "张六"},
	}

	Ref.SubMain.SubPersons.Grp:dup(#PersonList, function ()
		
	end)
end

local function init_view()
	Ref.SubMain.SubDay.SubStart.btn.onAction = on_submain_subday_substart_click
	Ref.SubMain.SubDay.SubEnd.btn.onAction = on_submain_subday_subend_click
	Ref.SubMain.SubPersons.Grp.btnAdd.onAction = on_submain_subpersons_grp_btnadd_click
	Ref.SubMain.SubPersons.Grp.Ent.btnDelete.onAction = on_submain_subpersons_grp_entperson_btndelete_click
	Ref.SubMain.btnSubmit.onAction = on_submain_btnsubmit_click
	Ref.SubTop.btnBack.onAction = on_subtop_btnback_click
	Ref.SubTop.btnDelete.onAction = on_subtop_btndelete_click
	UIMGR.make_group(Ref.SubMain.SubPersons.Grp, function (New, Ent)
		New.btnDelete.onAction = Ent.btnDelete.onAction
	end)
	--!*以上：自动注册的回调函数*--
end

local function init_logic()
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

