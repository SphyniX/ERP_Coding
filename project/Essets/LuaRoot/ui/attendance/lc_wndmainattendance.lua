--
-- @file    ui/attendance/lc_wndmainattendance.lua
-- @authors ckxz
-- @date    2016-08-02 15:24:28
-- @desc    WNDMainAttendance
--

local ipairs, pairs
    = ipairs, pairs
local libugui = require "libugui.cs"
local libunity = require "libunity.cs"
local libsystem = require "libsystem.cs"

local UIMGR = MERequire "ui/uimgr"
local DY_DATA = MERequire "datamgr/dydata.lua"
local UI_DATA = MERequire "datamgr/uidata.lua"
local TEXT = _G.ENV.TEXT
local NW = MERequire "network/networkmgr"
local LOGIN = MERequire "libmgr/login.lua"
local Ref

local undergoreason ,leavereason = nil, nil
local AttendanceProject = nil
local projectId = nil

local on_project_init

local punch_type -- 1 上班， 2 下班

local function time_to_string(Time)
	return string.format("%d-%d-%d %d:%d", Time.year, Time.month, Time.day, Time.hour, Time.minute)
end

local function on_select_project(id)
	projectId = id
	on_project_init()
end

local function on_punch_on_callback(Photolist, inp)
   	local nPhoto = 0
   	local nPhotoList = 0
	for i,v in ipairs(Photolist) do
		if v.image then nPhotoList = nPhotoList + 1 end
	end
   	if NW.connected() then
   		-- libunity.SetActive(Ref.spWait, true)
		local function on_http_photo_callback()
	   		nPhoto = nPhoto + 1
	   		if nPhoto >= nPhotoList then
				libunity.SetActive(Ref.spWait, false)
		   		local nm = NW.msg("ATTENCE.CS.UPWORK")
				nm:writeU32(projectId)
				nm:writeU32(punch_type)
				NW.send(nm)
				libunity.SetActive(Ref.spWait, false)
	   		end
	   	end

	   	for i,v in ipairs(Photolist) do
	   		print(v)
	   		if v.image then
	   			LOGIN.try_uploadphoto(DY_DATA.User.id, v.typeId, projectId,v.image, on_http_photo_callback)
	   		end
	   	end
	end
end

local function on_punch_off_callback(Photolist)
	local nPhoto = 0
	local nPhotoList = 0
	for i,v in ipairs(Photolist) do
		if v.image then nPhotoList = nPhotoList + 1 end
	end
   	if NW.connected() then
   		-- libunity.SetActive(Ref.spWait, true)
		local function on_http_photo_callback()
	   		nPhoto = nPhoto + 1
	   		if nPhoto >= nPhotoList then
				libunity.SetActive(Ref.spWait, false)
		   		local nm = NW.msg("ATTENCE.CS.UPWORK")
				nm:writeU32(projectId)
				nm:writeU32(punch_type)
				NW.send(nm)
				libunity.SetActive(Ref.spWait, false)
	   		end
	   	end

	   	for i,v in ipairs(Photolist) do
	   		print(v)
	   		if v.image then
	   			LOGIN.try_uploadphoto(DY_DATA.User.id, v.typeId, projectId, v.image, on_http_photo_callback)
	   		end
	   	end
	else

	end
end

local function on_try_punch_on()	
	-- 上班
	UI_DATA.WNDShowPhoto.title = "上班"
   	UI_DATA.WNDShowPhoto.tip = ""
   	UI_DATA.WNDShowPhoto.photolist = {
   		{ title = "门头照和人像", name = "sub_7.png" , typeId = 7 , need = true},
   	}
   	UI_DATA.WNDShowPhoto.callback = on_punch_on_callback
   	UIMGR.create_window("UI/WNDSubmitPhoto")
end

local function on_try_punch_off()
	-- 下班
	UI_DATA.WNDShowPhoto.title = "下班"
   	UI_DATA.WNDShowPhoto.tip = ""
   	UI_DATA.WNDShowPhoto.photolist = {
   		{ title = "门头照和人像", name = "sub_8.png" , typeId = 8, need = true },
   		{ title = "婴儿纸尿裤", name = "sub_9.png" , typeId = 9 },
   		{ title = "端架或地堆", name = "sub_10.png" , typeId = 10 },
   		{ title = "湿巾", name = "sub_11.png" , typeId = 11 },
   		{ title = "纸巾", name = "sub_12.png" , typeId = 12 },
   	}
   	UI_DATA.WNDShowPhoto.callback = on_punch_off_callback
   	UIMGR.create_window("UI/WNDSubmitPhoto")
end

local function on_try_punch(Ret)
	print("on_try_punch"..JSON:encode(Ret))
	if Ret.ret == 1 then
		local workstate = DY_DATA.User.workstate -- 1 下班， 2， 上班中， 3 离岗
		if punch_type == 1 then 
			on_try_punch_on()
		elseif punch_type == 2 then
			on_try_punch_off()
		end
	end
end

--!*以下：自动生成的回调函数*--

local function on_submain_subscroll_subcontent_subundergo_subinfo_subreason_click(btn)
	-- 选择原因
	libunity.SetActive(Ref.SubUndergoSelect.root, true)
end

local function on_submain_subscroll_subcontent_subundergo_subinfo_subconfirm_btnundergo_click(btn)
	-- 离岗
	local workstate = DY_DATA.User.workstate -- 1 下班， 2， 上班中， 3 离岗
	print(JSON:encode(DY_DATA.User))
	if workstate == 2 then
		local context = Ref.SubMain.SubScroll.SubContent.SubUndergo.SubInfo.inpInput.text
		if NW.connected() then
			local nm = NW.msg("ATTENCE.CS.BEDEMOBILIZED")
			nm:writeU32(DY_DATA.User.taskid)
			nm:writeString(undergoreason)
			nm:writeString(context)
			NW.send(nm)

		end
	elseif workstate == 3 then
	-- 复岗
		if NW.connected() then
			local nm = NW.msg("ATTENCE.CS.FUGANG")
			nm:writeU32(DY_DATA.User.taskid)
			NW.send(nm)
		end
	else
		_G.UI.Toast:make(nil, "当前不在上班状态"):show()
	end
end

local function on_submain_subscroll_subcontent_subpunch_subinfo_subproject_click(btn)
	-- 选择项目
	UI_DATA.WNDSelectProject.on_call_back = on_select_project
	UIMGR.create_window("UI/WNDSelectProject")
end

local function on_submain_subscroll_subcontent_subpunch_subinfo_subpunch_btnon_click(btn)
	-- 上班
	local workstate = DY_DATA.User.workstate -- 1 下班， 2， 上班中， 3 离岗
	if workstate == 1 then
		if projectId == nil then 
			_G.UI.Toast:make(nil, "请选择项目"):show()	
			return
		end

		punch_type = 1
		-- local nm = NW.msg("ATTENCE.CS.VERIFYLATLNG")
		-- local gps = libsystem.GetGps()
		-- nm:writeU32(projectId)
		-- nm:writeString(gps)
		-- NW.send(nm)
		
		on_try_punch({ret = 1})
	else
		_G.UI.Toast:make(nil, "当前不在下班状态"):show()	
	end
end

local function on_submain_subscroll_subcontent_subpunch_subinfo_subpunch_btnoff_click(btn)
	-- 下班
	local workstate = DY_DATA.User.workstate -- 1 下班， 2， 上班中， 3 离岗
	if workstate == 2 then 
		if projectId == nil then 
			_G.UI.Toast:make(nil, "请选择项目"):show()	
			return
		end

		punch_type = 2
		-- local nm = NW.msg("ATTENCE.CS.VERIFYLATLNG")
		-- local gps = libsystem.GetGps()
		-- nm:writeU32(projectId)
		-- nm:writeString(gps)
		-- NW.send(nm)

		on_try_punch({ret = 1})
	else
		_G.UI.Toast:make(nil, "当前不在上班状态"):show()
	end
end

local function on_submain_subscroll_subcontent_subpunch_subtip_btnback_click(btn)
	-- 提醒返回
	libunity.SetActive(Ref.SubMain.SubScroll.SubContent.SubPunch.SubTip.root, false)
end

local function on_submain_subscroll_subcontent_subleave_subinfo_substarttime_click(btn)
	-- 选择开始时间
	UI_DATA.WNDSetTime.on_call_back = function (Time)
		Ref.SubMain.SubScroll.SubContent.SubLeave.SubInfo.SubStartTime.lbText.text = time_to_string(Time)
	end
	UIMGR.create("UI/WNDSetTime")
end

local function on_submain_subscroll_subcontent_subleave_subinfo_subendtime_click(btn)
	-- 选择结束时间
	UI_DATA.WNDSetTime.on_call_back = function (Time)
		Ref.SubMain.SubScroll.SubContent.SubLeave.SubInfo.SubEndTime.lbText.text = time_to_string(Time)
	end
	UIMGR.create("UI/WNDSetTime")
end

local function on_submain_subscroll_subcontent_subleave_subinfo_subreason_click(btn)
	-- 选择原因
	libunity.SetActive(Ref.SubLeaveSelect.root, true)
end

local function on_submain_subscroll_subcontent_subleave_subinfo_subbtn_btnleave_click(btn)
	-- 请假
	local starttime = Ref.SubMain.SubScroll.SubContent.SubLeave.SubInfo.SubStartTime.lbText.text
	local endtime = Ref.SubMain.SubScroll.SubContent.SubLeave.SubInfo.SubEndTime.lbText.text
	if starttime == "" or endtime then 
		_G.UI.Toast:make(nil, "请填写时间"):show()
		return 
	end
	if leavereason == "" then 
		_G.UI.Toast:make(nil, "请填写原因"):show()
		return 
	end
	
	if NW.connected() then
		local nm = NW.msg("ATTENCE.CS.LEAVE")
		nm:writeU32(DY_DATA.User.id)
		nm:writeString(starttime)
		nm:writeString(endtime)
		nm:writeString(leavereason)
		NW.send(nm)
	end
end

local function on_submain_subscroll_subcontent_subleave_subinfo_subbtn_btnrecord_click(btn)
	-- 查看记录
	UIMGR.create_window("UI/WNDLeaveLog")
end

local function on_subtop_subbtn_subundergo_click(btn)
	-- 换页
end

local function on_subtop_subbtn_subpunchclock_click(btn)
	-- 换页
end

local function on_subtop_subbtn_subleave_click(btn)
	-- 换页
end

local function on_subbtm_btnwork_click(btn)
-- ##	UIMGR.WNDStack:pop()
	UIMGR.create_window("UI/WNDMainWork")
end

local function on_subbtm_btnsch_click(btn)
-- ##	UIMGR.WNDStack:pop()
	UIMGR.create_window("UI/WNDMainSchedule")
end

local function on_subbtm_btnmsg_click(btn)
-- ##	UIMGR.WNDStack:pop()
	UIMGR.create_window("UI/WNDMainMsg")
end

local function on_subbtm_btnuser_click(btn)
-- ##	UIMGR.WNDStack:pop()
	UIMGR.create_window("UI/WNDMainUser")
end

local function on_subleaveselect_btnback_click(btn)
	libunity.SetActive(Ref.SubLeaveSelect.root, false)
end

local function on_subleaveselect_subone_click(btn)
	leavereason = Ref.SubLeaveSelect.SubOne.lbText.text
	Ref.SubMain.SubScroll.SubContent.SubLeave.SubInfo.SubReason.lbText.text = leavereason
	libunity.SetActive(Ref.SubLeaveSelect.root, false)
end

local function on_subleaveselect_subtwo_click(btn)
	leavereason = Ref.SubLeaveSelect.SubTwo.lbText.text
	Ref.SubMain.SubScroll.SubContent.SubLeave.SubInfo.SubReason.lbText.text = leavereason
	libunity.SetActive(Ref.SubLeaveSelect.root, false)
end

local function on_subleaveselect_subthree_click(btn)
	leavereason = Ref.SubLeaveSelect.SubThree.lbText.text
	Ref.SubMain.SubScroll.SubContent.SubLeave.SubInfo.SubReason.lbText.text = leavereason
	libunity.SetActive(Ref.SubLeaveSelect.root, false)
end

local function on_subundergoselect_btnback_click(btn)
	libunity.SetActive(Ref.SubUndergoSelect.root, false)
end

local function on_subundergoselect_subone_click(btn)
	undergoreason = Ref.SubUndergoSelect.SubOne.lbText.text
	Ref.SubMain.SubScroll.SubContent.SubUndergo.SubInfo.SubReason.lbText.text = undergoreason
	libunity.SetActive(Ref.SubUndergoSelect.root, false)
end

local function on_subundergoselect_subtwo_click(btn)
	undergoreason = Ref.SubUndergoSelect.SubTwo.lbText.text
	Ref.SubMain.SubScroll.SubContent.SubUndergo.SubInfo.SubReason.lbText.text = undergoreason
	libunity.SetActive(Ref.SubUndergoSelect.root, false)
end

local function on_subundergoselect_subthree_click(btn)
	undergoreason = Ref.SubUndergoSelect.SubThree.lbText.text
	Ref.SubMain.SubScroll.SubContent.SubUndergo.SubInfo.SubReason.lbText.text = undergoreason
	libunity.SetActive(Ref.SubUndergoSelect.root, false)
end

local function on_subundergoselect_subfour_click(btn)
	undergoreason = Ref.SubUndergoSelect.SubFour.lbText.text
	Ref.SubMain.SubScroll.SubContent.SubUndergo.SubInfo.SubReason.lbText.text = undergoreason
	libunity.SetActive(Ref.SubUndergoSelect.root, false)
end

on_project_init = function ()
	local Ref_SubPunch = Ref.SubMain.SubScroll.SubContent.SubPunch.SubInfo
	if projectId == nil then
		Ref_SubPunch.SubProject.lbProject.text = ""
		Ref_SubPunch.lbStartTime.text = ""
		Ref_SubPunch.lbEndTime.text = ""
		libunity.SetActive(Ref.SubMain.SubScroll.SubContent.SubPunch.SubTip.root, true)
		return
	end
	
	local AttendanceProject = DY_DATA.AttendanceList[projectId]
	Ref_SubPunch.SubProject.lbProject.text = AttendanceProject.name
	libunity.SetActive(Ref.SubMain.SubScroll.SubContent.SubPunch.SubTip.root, false)
	Ref_SubPunch.lbStartTime.text = AttendanceProject.starttime
	Ref_SubPunch.lbEndTime.text = AttendanceProject.endtime
end

local function on_ui_init()
	libunity.SetActive(Ref.spWait, false)
	libunity.SetActive(Ref.SubLeaveSelect.root, false)
	libunity.SetActive(Ref.SubUndergoSelect.root, false)
	
	local Ref_SubMain_SubScroll_SubContent = Ref.SubMain.SubScroll.SubContent
	local User = DY_DATA.User
	local Ref_SubUndergo = Ref_SubMain_SubScroll_SubContent.SubUndergo.SubInfo
	Ref_SubUndergo.lbName.text = User.name
	Ref_SubUndergo.lbId.text = User.id
	Ref_SubUndergo.lbTime.text = libsystem.DateTime()
	Ref_SubUndergo.SubReason.lbText.text = ""
	if User.workstate == 1 then
		Ref_SubUndergo.SubConfirm.btnUndergo:SetInteractable(false)
		Ref_SubUndergo.SubConfirm.lbText.text = "离岗"
	elseif User.workstate == 2 then
		Ref_SubUndergo.SubConfirm.btnUndergo:SetInteractable(true)
		Ref_SubUndergo.SubConfirm.lbText.text = "离岗"
	elseif User.workstate == 3 then
		Ref_SubUndergo.SubConfirm.btnUndergo:SetInteractable(true)
		Ref_SubUndergo.SubConfirm.lbText.text = "复岗"
	end

	local Ref_SubPunch = Ref_SubMain_SubScroll_SubContent.SubPunch.SubInfo
	Ref_SubPunch.lbName.text = User.name
	Ref_SubPunch.lbId.text = User.id
	Ref_SubPunch.lbTime.text = libsystem.DateTime()
	Ref_SubPunch.SubProject.lbProject.text = ""
	Ref_SubPunch.lbStartTime.text = ""
	Ref_SubPunch.lbEndTime.text = ""
	
	on_project_init()

	local Ref_SubLeave = Ref_SubMain_SubScroll_SubContent.SubLeave.SubInfo
	Ref_SubLeave.lbName.text = User.name
	Ref_SubLeave.lbId.text = User.id
	Ref_SubLeave.lbTime.text = libsystem.DateTime()
	Ref_SubLeave.SubReason.lbText.text = ""
end

local function on_close_wait()
   	libunity.SetActive(Ref.spWait, false)
end

local function init_view()
	Ref.SubMain.SubScroll.SubContent.SubUndergo.SubInfo.SubReason.btn.onAction = on_submain_subscroll_subcontent_subundergo_subinfo_subreason_click
	Ref.SubMain.SubScroll.SubContent.SubUndergo.SubInfo.SubConfirm.btnUndergo.onAction = on_submain_subscroll_subcontent_subundergo_subinfo_subconfirm_btnundergo_click
	Ref.SubMain.SubScroll.SubContent.SubPunch.SubInfo.SubProject.btn.onAction = on_submain_subscroll_subcontent_subpunch_subinfo_subproject_click
	Ref.SubMain.SubScroll.SubContent.SubPunch.SubInfo.SubPunch.btnOn.onAction = on_submain_subscroll_subcontent_subpunch_subinfo_subpunch_btnon_click
	Ref.SubMain.SubScroll.SubContent.SubPunch.SubInfo.SubPunch.btnOff.onAction = on_submain_subscroll_subcontent_subpunch_subinfo_subpunch_btnoff_click
	Ref.SubMain.SubScroll.SubContent.SubPunch.SubTip.btnBack.onAction = on_submain_subscroll_subcontent_subpunch_subtip_btnback_click
	Ref.SubMain.SubScroll.SubContent.SubLeave.SubInfo.SubStartTime.btn.onAction = on_submain_subscroll_subcontent_subleave_subinfo_substarttime_click
	Ref.SubMain.SubScroll.SubContent.SubLeave.SubInfo.SubEndTime.btn.onAction = on_submain_subscroll_subcontent_subleave_subinfo_subendtime_click
	Ref.SubMain.SubScroll.SubContent.SubLeave.SubInfo.SubReason.btn.onAction = on_submain_subscroll_subcontent_subleave_subinfo_subreason_click
	Ref.SubMain.SubScroll.SubContent.SubLeave.SubInfo.SubBtn.btnLeave.onAction = on_submain_subscroll_subcontent_subleave_subinfo_subbtn_btnleave_click
	Ref.SubMain.SubScroll.SubContent.SubLeave.SubInfo.SubBtn.btnRecord.onAction = on_submain_subscroll_subcontent_subleave_subinfo_subbtn_btnrecord_click
	Ref.SubTop.SubBtn.SubUndergo.btn.onAction = on_subtop_subbtn_subundergo_click
	Ref.SubTop.SubBtn.SubPunchClock.btn.onAction = on_subtop_subbtn_subpunchclock_click
	Ref.SubTop.SubBtn.SubLeave.btn.onAction = on_subtop_subbtn_subleave_click
	Ref.SubBtm.btnWork.onAction = on_subbtm_btnwork_click
	Ref.SubBtm.btnSch.onAction = on_subbtm_btnsch_click
	Ref.SubBtm.btnMsg.onAction = on_subbtm_btnmsg_click
	Ref.SubBtm.btnUser.onAction = on_subbtm_btnuser_click
	Ref.SubLeaveSelect.btnBack.onAction = on_subleaveselect_btnback_click
	Ref.SubLeaveSelect.SubOne.btn.onAction = on_subleaveselect_subone_click
	Ref.SubLeaveSelect.SubTwo.btn.onAction = on_subleaveselect_subtwo_click
	Ref.SubLeaveSelect.SubThree.btn.onAction = on_subleaveselect_subthree_click
	Ref.SubUndergoSelect.btnBack.onAction = on_subundergoselect_btnback_click
	Ref.SubUndergoSelect.SubOne.btn.onAction = on_subundergoselect_subone_click
	Ref.SubUndergoSelect.SubTwo.btn.onAction = on_subundergoselect_subtwo_click
	Ref.SubUndergoSelect.SubThree.btn.onAction = on_subundergoselect_subthree_click
	Ref.SubUndergoSelect.SubFour.btn.onAction = on_subundergoselect_subfour_click
	--!*以上：自动注册的回调函数*--
end

local function init_logic()
	NW.subscribe("ATTENCE.SC.VERIFYLATLNG", on_try_punch)
	NW.subscribe("USER.SC.GETUSERINFOR", on_ui_init)
	NW.subscribe("ATTENCE.SC.UPWORK", on_close_wait)
	-- libsystem.StartGps()
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
	NW.unsubscribe("ATTENCE.SC.VERIFYLATLNG", on_try_punch)
	NW.unsubscribe("USER.SC.GETUSERINFOR", on_ui_init)
	NW.unsubscribe("ATTENCE.SC.UPWORK", on_close_wait)
	libsystem.StopGps()
end

local P = {
	start = start,
	update_view = update_view,
	on_recycle = on_recycle,
}
return P

