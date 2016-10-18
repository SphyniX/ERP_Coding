--
-- @file    ui/attendance/lc_wndleavelog.lua
-- @authors zl
-- @date    2016-08-09 01:17:51
-- @desc    WNDLeaveLog
--

local ipairs, pairs
    = ipairs, pairs
local libugui = require "libugui.cs"
local libunity = require "libunity.cs"
local UIMGR = MERequire "ui/uimgr"
local NW = MERequire "network/networkmgr"
local DY_DATA = MERequire "datamgr/dydata.lua"
local TEXT = _G.ENV.TEXT
local Ref
--!*以下：自动生成的回调函数*--

local function on_subtop_btnback_click(btn)
	UIMGR.close_window(Ref.root)	
end

local function on_ui_init()
	local LeaveList = DY_DATA.LeaveList
	if LeaveList == nil then return end
	Ref.SubLog.GrpLog:dup(#LeaveList, function (i, Ent, isNew)
		local Log = LeaveList[i]
		local reason = Log.reason
		libunity.SetActive(Ent.spBlue, reason == "病假")
		libunity.SetActive(Ent.spRed, reason == "事假")
		libunity.SetActive(Ent.spYellow, reason == "其他")
		Ent.lbTip.text = TEXT.LeaveState[Log.state]
		Ent.lbStart.text = Log.starttime
		Ent.lbTime.text = ""
		Ent.lbText.text = ""
	end)
end

local function init_view()
	Ref.SubTop.btnBack.onAction = on_subtop_btnback_click
	UIMGR.make_group(Ref.SubLog.GrpLog)
	--!*以上：自动注册的回调函数*--
end

local function init_logic()
	NW.subscribe("ATTENCE.SC.GETLEAVELIST", on_ui_init)
	if DY_DATA.LeaveList == nil or next(DY_DATA.LeaveList) == nil then
		local nm = NW.msg("ATTENCE.CS.GETLEAVELIST")
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
	NW.unsubscribe("ATTENCE.SC.GETLEAVELIST", on_ui_init)
end

local P = {
	start = start,
	update_view = update_view,
	on_recycle = on_recycle,
}
return P

