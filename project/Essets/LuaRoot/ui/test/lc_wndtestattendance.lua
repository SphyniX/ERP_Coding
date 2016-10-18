--
-- @file    ui/test/lc_wndtestattendance.lua
-- @authors zl
-- @date    2016-09-17 19:34:08
-- @desc    WNDTestAttendance
--

local ipairs, pairs
    = ipairs, pairs
local libugui = require "libugui.cs"
local libunity = require "libunity.cs"
local UIMGR = MERequire "ui/uimgr"
local Ref

local function on_tgl_change(index)
	libunity.SetActive(Ref.SubMain.SubContent.SubUndergo.root, index == 1)
	libunity.SetActive(Ref.SubMain.SubContent.SubPunch.root, index == 2)
	libunity.SetActive(Ref.SubMain.SubContent.SubLeave.root, index == 3)
	
end
--!*以下：自动生成的回调函数*--

local function on_submain_subcontent_subundergo_subinfo_subreason_click(btn)
	
end

local function on_submain_subcontent_subundergo_subinfo_subconfirm_click(btn)
	
end

local function on_submain_subcontent_subpunch_subinfo_subproject_click(btn)
	
end

local function on_submain_subcontent_subpunch_subinfo_subpunch_btnon_click(btn)
	
end

local function on_submain_subcontent_subpunch_subinfo_subpunch_btnoff_click(btn)
	
end

local function on_submain_subcontent_subleave_subinfo_substarttime_click(btn)
	
end

local function on_submain_subcontent_subleave_subinfo_subendtime_click(btn)
	
end

local function on_submain_subcontent_subleave_subinfo_subreason_click(btn)
	
end

local function on_submain_subcontent_subleave_subinfo_subbtn_btnleave_click(btn)
	
end

local function on_submain_subcontent_subleave_subinfo_subbtn_btnrecord_click(btn)
	
end

local function on_submain_subbtm_btnundergo_click(btn)
	on_tgl_change(1)
end

local function on_submain_subbtm_btnpunchclock_click(btn)
	on_tgl_change(2)
end

local function on_submain_subbtm_btnleave_click(btn)
	on_tgl_change(3)
end

local function on_subbtm_btnwork_click(btn)
	
end

local function on_subbtm_btnsch_click(btn)
	
end

local function on_subbtm_btnmsg_click(btn)
	
end

local function on_subbtm_btnuser_click(btn)
	
end

local function init_view()
	Ref.SubMain.SubContent.SubUndergo.SubInfo.SubReason.btn.onAction = on_submain_subcontent_subundergo_subinfo_subreason_click
	Ref.SubMain.SubContent.SubUndergo.SubInfo.SubConfirm.btn.onAction = on_submain_subcontent_subundergo_subinfo_subconfirm_click
	Ref.SubMain.SubContent.SubPunch.SubInfo.SubProject.btn.onAction = on_submain_subcontent_subpunch_subinfo_subproject_click
	Ref.SubMain.SubContent.SubPunch.SubInfo.SubPunch.btnOn.onAction = on_submain_subcontent_subpunch_subinfo_subpunch_btnon_click
	Ref.SubMain.SubContent.SubPunch.SubInfo.SubPunch.btnOff.onAction = on_submain_subcontent_subpunch_subinfo_subpunch_btnoff_click
	Ref.SubMain.SubContent.SubLeave.SubInfo.SubStartTime.btn.onAction = on_submain_subcontent_subleave_subinfo_substarttime_click
	Ref.SubMain.SubContent.SubLeave.SubInfo.SubEndTime.btn.onAction = on_submain_subcontent_subleave_subinfo_subendtime_click
	Ref.SubMain.SubContent.SubLeave.SubInfo.SubReason.btn.onAction = on_submain_subcontent_subleave_subinfo_subreason_click
	Ref.SubMain.SubContent.SubLeave.SubInfo.SubBtn.btnLeave.onAction = on_submain_subcontent_subleave_subinfo_subbtn_btnleave_click
	Ref.SubMain.SubContent.SubLeave.SubInfo.SubBtn.btnRecord.onAction = on_submain_subcontent_subleave_subinfo_subbtn_btnrecord_click
	Ref.SubMain.SubBtm.btnUndergo.onAction = on_submain_subbtm_btnundergo_click
	Ref.SubMain.SubBtm.btnPunchClock.onAction = on_submain_subbtm_btnpunchclock_click
	Ref.SubMain.SubBtm.btnLeave.onAction = on_submain_subbtm_btnleave_click
	Ref.SubBtm.btnWork.onAction = on_subbtm_btnwork_click
	Ref.SubBtm.btnSch.onAction = on_subbtm_btnsch_click
	Ref.SubBtm.btnMsg.onAction = on_subbtm_btnmsg_click
	Ref.SubBtm.btnUser.onAction = on_subbtm_btnuser_click
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

