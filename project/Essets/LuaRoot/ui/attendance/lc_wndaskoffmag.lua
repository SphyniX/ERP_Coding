--
-- @file    ui/attendance/lc_wndaskoffmag.lua
-- @authors cks
-- @date    2016-11-30 21:08:28
-- @desc    WNDAskOffMag
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

local LeaveDetails
local UnderId
--!*以下：自动生成的回调函数*--


local function on_ui_init( )
	-- body
	LeaveDetails = DY_DATA.LeaveDetails
	Ref.SubMain.SubContent.lbStore.text = LeaveDetails.storename
	Ref.SubMain.SubContent.lbProject.text = LeaveDetails.projectname
	Ref.SubMain.SubContent.lbStartTime.text = LeaveDetails.starttime
	Ref.SubMain.SubContent.lbEndTime.text = LeaveDetails.endtime
	Ref.SubMain.SubContent.lbTime.text = LeaveDetails.submittime .. "    "
	if LeaveDetails.reason ~= "nil" then
		Ref.SubMain.SubContent.lbReason.text = LeaveDetails.reasonstate .. ":" .. LeaveDetails.reason
	else
		Ref.SubMain.SubContent.lbReason.text = LeaveDetails.reasonstate .. ": 未填写理由"
	end
end


local function on_subtop_btnback_click(btn)
	UIMGR.close_window(Ref.root)
end

local function init_view()
	Ref.SubTop.btnBack.onAction = on_subtop_btnback_click
	--!*以上：自动注册的回调函数*--
end

local function init_logic()
	
	NW.subscribe("ATTENCE.SC.GETLEAVEINFOR",on_ui_init)

	UnderId = UI_DATA.WNDAskOffMag.UnderId
	UI_DATA.WNDAskOffMag.UnderId = nil


	LeaveDetails = {}
	LeaveDetails = DY_DATA.LeaveDetails
	-- if next(LeaveDetails) == nil then
		local nm = NW.msg("ATTENCE.CS.GETLEAVEINFOR")
		nm:writeU32(UnderId)
		NW.send(nm)
	-- 	return
	-- else
	-- 	on_ui_init()
	-- end

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
	NW.unsubscribe("ATTENCE.SC.GETLEAVEINFOR",on_ui_init)
end

local P = {
	start = start,
	update_view = update_view,
	on_recycle = on_recycle,
}
return P

