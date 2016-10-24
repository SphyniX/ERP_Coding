--
-- @file    ui/user/lc_wndusersupervisorlist.lua
-- @authors ckxz
-- @date    2016-08-01 15:50:17
-- @desc    WNDUserSupervisorList
--

local ipairs, pairs
    = ipairs, pairs
local libugui = require "libugui.cs"
local libunity = require "libunity.cs"
local UIMGR = MERequire "ui/uimgr"
local UI_DATA = MERequire "datamgr/uidata.lua"
local DY_DATA = MERequire "datamgr/dydata.lua"
local NW = MERequire "network/networkmgr"
local TEXT = _G.ENV.TEXT
local Ref

local SupervisorList
--!*以下：自动生成的回调函数*--

local function on_submain_grpsupervisor_entsup_btncall_click(btn)
	local index = tonumber(btn.transform.parent.name:sub(7))
	UI_DATA.WNDUserSupervisor.superId = SupervisorList[index].id
	UIMGR.create_window("UI/WNDUserSupervisor")	
end

local function on_subtop_btnback_click(btn)
	UIMGR.close_window(Ref.root)
end

local function on_subtop_btnadd_click(btn)
	UIMGR.create_window("UI/WNDUserAddSupervisor")
end

local function on_ui_init()
	SupervisorList = DY_DATA.get_super_list()
	print(JSON:encode(SupervisorList))
	if SupervisorList == nil then return end
	Ref.SubMain.GrpSupervisor:dup(#SupervisorList, function (i, Ent, isNew)
		local Supervisor = SupervisorList[i]
		Ent.lbName.text = Supervisor.name
		Ent.lbState.text = TEXT.SurperState[Supervisor.state]
	end)
end

local function init_view()
	Ref.SubMain.GrpSupervisor.Ent.btnCall.onAction = on_submain_grpsupervisor_entsup_btncall_click
	Ref.SubTop.btnBack.onAction = on_subtop_btnback_click
	Ref.SubTop.btnAdd.onAction = on_subtop_btnadd_click
	UIMGR.make_group(Ref.SubMain.GrpSupervisor, function (New, Ent)
		New.btnCall.onAction = Ent.btnCall.onAction
	end)
	--!*以上：自动注册的回调函数*--
end

local function init_logic()
	NW.subscribe("USER.SC.GETSUPERLIST", on_ui_init)

	SupervisorList = DY_DATA.get_super_list()
	if SupervisorList == nil or #SupervisorList == 0 then
		local nm = NW.msg("USER.CS.GETSUPERLIST")
		nm:writeU32(DY_DATA.User.id)
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
	NW.unsubscribe("USER.SC.GETSUPERLIST", on_ui_init)
end

local P = {
	start = start,
	update_view = update_view,
	on_recycle = on_recycle,
}
return P

