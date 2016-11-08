--
-- @file    ui/attendance/lc_wndsupattendance.lua
-- @authors zl
-- @date    2016-08-09 00:17:54
-- @desc    WNDSupAttendance
--

local ipairs, pairs
    = ipairs, pairs
local libugui = require "libugui.cs"
local libunity = require "libunity.cs"
local libsystem = require "libsystem.cs"
local UIMGR = MERequire "ui/uimgr"
local DY_DATA = MERequire "datamgr/dydata.lua"
local TEXT = _G.ENV.TEXT
local UI_DATA = MERequire "datamgr/uidata.lua"
local NW = MERequire "network/networkmgr"
local LOGIN = MERequire "libmgr/login.lua"
local Ref
local x = 1
local TimeInfo = {}
local AttendanceList=nil
local projectId = nil
local reason = nil
local on_project_init

------Fuck You Up!!!!____________-----老子就是任性------------
-- .'"'.         ___,,,___         .'``.
-- : (\   `."'"```          ```"'"-'   /) ;
-- :   \                          `./   .'
--    `.                             :.'
--      /         _          _         \
--     |          0}        {0          |
--     |          /          \          |
--     |         /            \         |
--     |        /              \        |
--      \      |       .-.       |      /
--       `.    | . . /    \ . . |    .'
--         `-._\.'.(      ).'./_.-'
--             `\'   `._.'   '/'
--               `. --'-- .'
--                 `-...-'  

---proj Select callback Func ----
local function on_select_project(id)
	print("Start Making Project :" .. id)
	projectId = id
	
	on_project_init()
end

---end---

---Timer---
local function refreshtime()
	TimeInfo = os.date("*t",os.time())
	
	if DY_DATA.Work.NowTime == nil then
		local nm = NW.msg("ATTENCE.CS.GETTIME")
		NW.send(nm)
		return
	end
	print("Work.NowTime is :" .. JSON:encode(DY_DATA.Work.NowTime))
	local TimeOfDay = DY_DATA.Work.NowTime
	print("TimeOfDay is :" .. JSON:encode(TimeOfDay))
	Ref.SubMain.SubTime.lbTime.text = TimeOfDay.time
	Ref.SubMain.SubTime.lbDay.text = TimeOfDay.day .. " " .. TimeOfDay.week
	TimeInfo.hour = DY_DATA.Work.NowTime.time:sub(1,2)
	TimeInfo.min = DY_DATA.Work.NowTime.time:sub(4,5)
	TimeInfo.sec = DY_DATA.Work.NowTime.time:sub(7,8)
	-- print(DY_DATA.Work.NowTime.time:sub(1,2))
	-- print(DY_DATA.Work.NowTime.time:sub(4,5))
	-- print(DY_DATA.Work.NowTime.time:sub(7,8))
	-- print(libsystem.GetFps())

end

local function refreshtime_onupdate()
	x = x + 1
	if (x % libsystem.GetFps()) == 0 then
		x = 0
		TimeInfo.sec = tostring(tonumber(TimeInfo.sec)+1)
		-- printf(TimeInfo.sec)

		if tonumber(TimeInfo.sec) == 60 then
			TimeInfo.min = tostring(tonumber(TimeInfo.min)+1)
			TimeInfo.sec = tonumber(0)
		end
		if tonumber(TimeInfo.sec) < 10 then TimeInfo.sec = "0" .. TimeInfo.sec end

		if tonumber(TimeInfo.min) == 60 then
			TimeInfo.hour = tostring(tonumber(TimeInfo.hour)+1)
			TimeInfo.min = tonumber(0)
		end
		if tonumber(TimeInfo.min) < 10 then TimeInfo.min = "0" .. tostring(tonumber(TimeInfo.min)) end

		if tonumber(TimeInfo.hour) == 24 then

			TimeInfo.hour = tonumber(0)
		end
		if tonumber(TimeInfo.hour) < 10 then TimeInfo.hour = "0" .. tostring(tonumber(TimeInfo.hour)) end
		
		Ref.SubMain.SubTime.lbTime.text = TimeInfo.hour .. ":" .. TimeInfo.min .. ":" .. TimeInfo.sec
	end

end 
---Timer End ---

local function time_to_string(Time)
	return string.format("%d-%d-%d %d:%d", Time.year, Time.month, Time.day, Time.hour, Time.minute)
end



local function on_punch_on_callback(Photolist)
   	local nPhoto = 0
   	if NW.connected() then
		local function on_http_photo_callback()
	   		nPhoto = nPhoto + 1
	   		if nPhoto >= #Photolist then
		   		local nm = NW.msg("ATTENCE.CS.PHUNCH")
				nm:writeU32(projectId)
				NW.send(nm)
	   		end
	   	end

	   	for i,v in ipairs(Photolist) do
	   		print(v)
	   		LOGIN.try_uploadphoto(DY_DATA.User.id, v.typeId, projectId, v.image, on_http_photo_callback)
	   	end
	end
end

local function on_try_punch_on()	
	-- 上班
	UI_DATA.WNDShowPhoto.title = "巡店"
   	UI_DATA.WNDShowPhoto.tip = ""
   	UI_DATA.WNDShowPhoto.photolist = {
   	-- 门头照和人像、正常排面、端架、堆头、促销互动、促销员形象、离店照片
   		{ title = "门头照和人像", name = "sub_7.png" , typeId = 7 },
   		{ title = "正常排面", name = "sub_8.png" , typeId = 8 },
   		{ title = "端架", name = "sub_9.png" , typeId = 9 },
   		{ title = "堆头", name = "sub_10.png" , typeId = 10 },
   		{ title = "促销互动", name = "sub_11.png" , typeId = 11 },
   		{ title = "促销员形象", name = "sub_12.png" , typeId = 12 },
   		{ title = "离店照片", name = "sub_13.png" , typeId = 13 },
   	}
   	UI_DATA.WNDShowPhoto.callback = on_punch_on_callback
   	UIMGR.create_window("UI/WNDSubmitPhoto")
end


local function on_try_punch(Ret)
	print("on_try_punch"..JSON:encode(Ret))
	if Ret.ret == 1 then
		on_try_punch_on()
	end
end
--!*以下：自动生成的回调函数*--

local function on_subtop_btnbutton_click(btn)
	
end

local function on_subbtm_btnwork_click(btn)
-- ##	UIMGR.WNDStack:pop()
	UIMGR.create_window("UI/WNDSupWork")
end

local function on_subbtm_btnsch_click(btn)
-- ##	UIMGR.WNDStack:pop()
	UIMGR.create_window("UI/WNDSupSchedule")
end

local function on_subbtm_btnmsg_click(btn)
-- ##	UIMGR.WNDStack:pop()
	UIMGR.create_window("UI/WNDSupMsg")
end

local function on_subbtm_btnuser_click(btn)
-- ##	UIMGR.WNDStack:pop()
	UIMGR.create_window("UI/WNDSupUser")
end

local function on_submain_subproject_click(btn)
	UI_DATA.WNDSelectProject.on_call_back = on_select_project
	UIMGR.create_window("UI/WNDSelectProject")
end

local function on_submain_subpunch_btnbutton_click(btn)
	
end

local function on_submain_subunder_btnbutton_click(btn)
	UIMGR.create("UI/WNDAttUnder")
end

-- local function on_submain_subscroll_subcontent_subpunch_subinfo_subproject_click(btn)
-- 	-- 巡店 选择项目
-- 	UI_DATA.WNDSelectProject.on_call_back = on_select_project
-- 	UIMGR.create_window("UI/WNDSupSelectProject")
-- end

local function on_submain_subscroll_subcontent_subpunch_subinfo_substore_grp_entproject_btnbutton_click(btn)
	-- 签到
	local index = tonumber(btn.transform.parent.name:sub(11))
	local Project = DY_DATA.ProjectList[projectId]
	local Ref_SubPunch = Ref.SubMain.SubScroll.SubContent.SubPunch.SubInfo
	Ref_SubPunch.SubProject.lbProject.text = Project.name
	local StoreList = Project.StoreList
	local Store = StoreList[index]
	-- local nm = NW.msg("ATTENCE.CS.VERIFY")
	-- local gps = libsystem.GetGps()
	-- print(gps, Store.id)
	-- nm:writeU32(Store.id)
	-- nm:writeString(gps)
	-- NW.send(nm)

	on_try_punch({ret = 1})
end

local function on_submain_subscroll_subcontent_subleave_subinfo_substarttime_click(btn)
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
	-- libunity.SetActive(Ref.SubSelect.root, true)
end

local function on_submain_subscroll_subcontent_subleave_subinfo_subbtn_btnleave_click(btn)
	local starttime = Ref.SubMain.SubScroll.SubContent.SubLeave.SubInfo.SubStartTime.lbText.text
	local endtime = Ref.SubMain.SubScroll.SubContent.SubLeave.SubInfo.SubEndTime.lbText.text
	if NW.connected() then
		local nm = NW.msg("ATTENCE.CS.LEAVE")
		nm:writeU32(DY_DATA.User.id)
		nm:writeString(starttime)
		nm:writeString(endtime)
		nm:writeString(reason)
		NW.send(nm)
	end
end

local function on_submain_subscroll_subcontent_subleave_subinfo_subbtn_btnrecord_click(btn)
	 UIMGR.create_window("UI/WNDLeaveLog")
end

local function on_subtop_subbtn_subpunchclock_click(btn)
	-- 换页
end

local function on_subtop_subbtn_subleave_click(btn)
	-- 换页
end

-- local function on_subselect_btnback_click(btn)
-- 	libunity.SetActive(Ref.SubSelect.root, false)
-- end

-- local function on_subselect_subone_click(btn)
-- 	reason = Ref.SubSelect.SubOne.lbText.text
-- 	Ref.SubMain.SubScroll.SubContent.SubLeave.SubInfo.SubReason.lbText.text = reason
-- 	libunity.SetActive(Ref.SubSelect.root, false)
-- end

-- local function on_subselect_subtwo_click(btn)
-- 	reason = Ref.SubSelect.SubTwo.lbText.text
-- 	Ref.SubMain.SubScroll.SubContent.SubLeave.SubInfo.SubReason.lbText.text = reason
-- 	libunity.SetActive(Ref.SubSelect.root, false)
-- end

-- local function on_subselect_subthree_click(btn)
-- 	reason = Ref.SubSelect.SubThree.lbText.text
-- 	Ref.SubMain.SubScroll.SubContent.SubLeave.SubInfo.SubReason.lbText.text = reason
-- 	libunity.SetActive(Ref.SubSelect.root, false)
-- end

local function on_punch_finish(Ref)
	local nm = NW.msg("WORK.CS.GETSTORE")
	nm:writeU32(projectId)
	nm:writeU32(DY_DATA.User.id)
	NW.send(nm)
end

local function on_store_init()
	print("on_store_init")
	local Project = DY_DATA.ProjectList[projectId]
	print(JSON:encode(Project))
	local StoreList = Project.StoreList
	if StoreList == nil then return end
	print(JSON:encode(StoreList))

	local Ref_SubPunch = Ref.SubMain.SubScroll.SubContent.SubPunch.SubInfo
	Ref_SubPunch.SubStore.Grp:dup(#StoreList, function( i, Ent, isNew)
		local Store = StoreList[i]
		Ent.lbName.text = Store.name
		Ent.lbTip.text = Store.state == 1 and "已巡店" or "巡店"
		Ent.btnButton:SetInteractable(Store.state == 2)
	end)
end


-----老版存留----
-- on_project_init = function ()
-- 	if projectId == nil then	
-- 		return
-- 	end
-- 	local Project = DY_DATA.ProjectList[projectId]
-- 	local Ref_SubPunch = Ref.SubMain.SubScroll.SubContent.SubPunch.SubInfo
-- 	Ref_SubPunch.SubProject.lbProject.text = Project.name
-- 	local StoreList = Project.StoreList
-- 	if StoreList == nil then
-- 		print("StoreList is nil")
-- 		local nm = NW.msg("WORK.CS.GETSTORE")
-- 		nm:writeU32(projectId)
-- 		nm:writeU32(DY_DATA.User.id)
-- 		NW.send(nm)
-- 	end
-- end
---

on_project_init = function ()
	if projectId == nil then
		Ref.SubMain.SubProject.lbText.text = "请选择项目"
		return
	end
	AttendanceList = DY_DATA.get_attendance_list(false)
	print("UI_DATA.AttendanceList is :" .. JSON:encode(AttendanceList))

	local AttendanceProject = AttendanceList[projectId]
	print("AttendanceProject in MainAttance :" .. JSON:encode(AttendanceProject))
	Ref.SubMain.SubProject.lbText.text = AttendanceProject.name
end

local function on_ui_init()
	-- libunity.SetActive(Ref.SubSelect.root, false)
	
	-- local Ref_SubMain_SubScroll_SubContent = Ref.SubMain.SubScroll.SubContent
	local User = DY_DATA.User

	-- local Ref_SubPunch = Ref_SubMain_SubScroll_SubContent.SubPunch.SubInfo
	-- Ref_SubPunch.lbName.text = User.name
	-- Ref_SubPunch.lbId.text = User.id
	-- Ref_SubPunch.lbTime.text = libsystem.DateTime()
	-- Ref_SubPunch.SubProject.lbProject.text = ""
	-- local Ref_SubLeave = Ref_SubMain_SubScroll_SubContent.SubLeave.SubInfo
	-- Ref_SubLeave.SubStartTime.lbText.text = ""
	-- Ref_SubLeave.SubEndTime.lbText.text = ""
	
	if projectId == nil then
		Ref.SubMain.SubProject.lbText.text = "请选择项目"
		return
	end

	-- local Ref_SubLeave = Ref_SubMain_SubScroll_SubContent.SubLeave.SubInfo
	-- Ref_SubLeave.lbName.text = User.name
	-- Ref_SubLeave.lbId.text = User.id
	-- Ref_SubLeave.lbTime.text = libsystem.DateTime()
	-- Ref_SubLeave.SubReason.lbText.text = ""
end
local function init_view()
	Ref.SubTop.btnButton.onAction = on_subtop_btnbutton_click
	Ref.SubBtm.btnWork.onAction = on_subbtm_btnwork_click
	Ref.SubBtm.btnSch.onAction = on_subbtm_btnsch_click
	Ref.SubBtm.btnMsg.onAction = on_subbtm_btnmsg_click
	Ref.SubBtm.btnUser.onAction = on_subbtm_btnuser_click
	Ref.SubMain.SubProject.btn.onAction = on_submain_subproject_click
	Ref.SubMain.SubPunch.btnButton.onAction = on_submain_subpunch_btnbutton_click
	Ref.SubMain.SubUnder.btnButton.onAction = on_submain_subunder_btnbutton_click
	--!*以上：自动注册的回调函数*--
end

local function init_logic()
	NW.subscribe("ATTENCE.SC.VERIFY", on_try_punch)
	NW.subscribe("USER.SC.GETUSERINFOR", on_ui_init)
	NW.subscribe("WORK.SC.GETSTORE", on_store_init)
	NW.subscribe("ATTENCE.CS.PHUNCH", on_punch_finish)
	
	-- libsystem.StartGps()
	on_ui_init()
	refreshtime()
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
	NW.unsubscribe("ATTENCE.SC.VERIFY", on_try_punch)
	NW.unsubscribe("USER.SC.GETUSERINFOR", on_ui_init)
	NW.unsubscribe("WORK.SC.GETSTORE", on_store_init)
	NW.unsubscribe("ATTENCE.CS.PHUNCH", on_punch_finish)
	libsystem.StopGps()
end

local function update()
	refreshtime_onupdate()
end

local P = {
	start = start,
	update_view = update_view,
	on_recycle = on_recycle,
	update = update,
}
return P

