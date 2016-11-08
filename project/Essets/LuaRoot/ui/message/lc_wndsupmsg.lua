--
-- @file    ui/message/lc_wndsupmsg.lua
-- @authors zl
-- @date    2016-08-14 20:20:28
-- @desc    WNDSupMsg
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
--!*以下：自动生成的回调函数*--

local function on_subtop_btnwrite_click(btn)
	UIMGR.create_window("UI/WNDSupEditorMsg")
end

local function on_subbtm_spatt_click(btn)
	UIMGR.create_window("UI/WNDSupAttendance")
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

end

local function on_subbtm_btnuser_click(btn)

-- ##	UIMGR.WNDStack:pop()
UIMGR.create_window("UI/WNDSupUser")
end

local function on_submsg_grpmsg_entmsg_subcontext_btncontext_click(btn)
	print("<color=#00ff00>on_submsg_grpmsg_entmsg_subcontext_btncontext_click 订阅回调"..tostring(btn.name).."</color>")
   -- WNDsupmsg
   local listdata={}
   listdata.MsgIndex= tonumber(btn.name)
   UI_DATA.WNDsupmsgData=listdata
   UIMGR.create_window("UI/WNDSupMsgcontent")
end

local function on_tgltoggle_change(tgl)
	print("<color=#00ff00>on_submsg_grpmsg_entmsg_subcontext_btncontext_click 订阅回调</color>")
end

local function on_subcontext_btncontext_click(btn)

end

local function on_submsg_grpmsg_entmsg_subcontext_btndel_click(btn)
	local index = tonumber(btn.transform.parent.parent.name:sub(7))
	local MsgList = DY_DATA.MsgList
	local Msg = MsgList[index]
	local nm = NW.msg("MESSAGE.CS.DELETE")
	nm:writeU32(Msg.id)
	NW.send(nm)
end

local function on_subtop_btnnext_click(btn)
	print(Ref.SubTop.MsgInput.text)

end

local function on_subtop_btnbutton_click(btn)

end

local function on_btnwrite_click(btn)

end

local function on_subbtm_btnatt_click(btn)
-- ##	UIMGR.WNDStack:pop()
UIMGR.create_window("UI/WNDSupAttendance")
end

local function on_subtop_btncontact_click(btn)
	libunity.SetActive(Ref.SubTop.SubContact.root, true)
end

local function on_subtop_subcontact_btnarea_click(btn)
	libunity.SetActive(Ref.SubTop.SubContact.root, false)
	UI_DATA.WNDMsgLower.type = 3
	UI_DATA.WNDMsgLower.callback = function (root, Lower)
	UI_DATA.WNDUserSupervisor.superId = Lower.id
	UIMGR.create_window("UI/WNDUserSupervisor")
end
UIMGR.create_window("UI/WNDMsgLower")
end

local function on_subtop_subcontact_btnsale_click(btn)
	libunity.SetActive(Ref.SubTop.SubContact.root, false)
	UI_DATA.WNDMsgLower.type = 1
	UI_DATA.WNDMsgLower.callback = function (root, Lower)
	UI_DATA.WNDUserSupervisor.superId = Lower.id
	UIMGR.create_window("UI/WNDUserSupervisor")
end
UIMGR.create_window("UI/WNDMsgLower")
end

local function on_ui_init()
	local MsgList = DY_DATA.MsgList
	if MsgList == nil then 
		print("<color=#00ff00>获取信息数据失败</color>")
		libunity.SetActive(Ref.SubMsg.spNil, true)
		return 
	else
		print("<color=#00ff00>获取信息数据成功</color>")

	end

	print(#MsgList)
	local LowerList = DY_DATA.LowerList
	print(JSON:encode(LowerList))
	print(#MsgList)
	Ref.SubMsg.GrpMsg:dup( #MsgList, function (i, Ent, isNew)
		local Msg = MsgList[i]
		print(JSON:encode(Msg))
		print(" Msg people ")
		print(Msg.people)
		print(JSON:encode(LowerList[Msg.people]))
		local obj = libunity.FindGameObject(Ent.SubContext,"btnContext")
		if obj then
			obj.name=tostring(i);
			print("<color=#00ffff>按钮查找初始化成功</color>")
		else
			print("<color=#00ffff>按钮查找失败</color>")
		end
		Ent.SubContext.lbTitle.text = Msg.people--LowerList[Msg.people] and LowerList[Msg.people].name or Msg.people
		Ent.SubContext.lbText.text = Msg.context
		Ent.SubContext.lbTime.text = Msg.time
		--Ent.SubContext.lbDay.text = Msg.day
		end)
end

local function init_view()
	Ref.SubTop.btnWrite.onAction = on_subtop_btnwrite_click
	Ref.SubBtm.spAtt.onAction = on_subbtm_spatt_click
	Ref.SubBtm.btnWork.onAction = on_subbtm_btnwork_click
	Ref.SubBtm.btnSch.onAction = on_subbtm_btnsch_click
	Ref.SubBtm.btnMsg.onAction = on_subbtm_btnmsg_click
	Ref.SubBtm.btnUser.onAction = on_subbtm_btnuser_click
	Ref.SubMsg.GrpMsg.Ent.SubContext.btnContext.onAction = on_submsg_grpmsg_entmsg_subcontext_btncontext_click
	UIMGR.make_group(Ref.SubMsg.GrpMsg, function (New, Ent)
		New.SubContext.btnContext.onAction = Ent.SubContext.btnContext.onAction
		end)
	--!*以上：自动注册的回调函数*--
end

local function init_logic()
	NW.subscribe("MESSAGE.SC.GETMESSAGELIST", on_ui_init)

	if DY_DATA.MsgList == nil or next(DY_DATA.MsgList) == nil then
		local nm = NW.msg("MESSAGE.CS.GETMESSAGELIST")
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
	NW.unsubscribe("MESSAGE.SC.GETMESSAGELIST", on_ui_init)
	NW.unsubscribe("MESSAGE.SC.GETLOWER", on_ui_init)
end

local P = {
start = start,
update_view = update_view,
on_recycle = on_recycle,
}
return P

