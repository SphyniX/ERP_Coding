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


local function on_read_msg(index)
	local MsgList = DY_DATA.MsgList
	local Msg = MsgList[index]
	print(JSON:encode(Msg))
	if Msg.type == 1 then
		UIMGR.create_window("UI/WNDSupWork")
	elseif Msg.type == 2 then
		print("<color=#00ff00>on_submsg_grpmsg_entmsg_subcontext_btncontext_click 订阅回调"..tostring(index).."</color>")
   -- WNDsupmsg
	   	local listdata={}
	  	local MsgList = DY_DATA.MsgList
	 	listdata.MsgIndex = tonumber(index)
	  	Ref.SubMsg.Grp:dup( #MsgList, function (i, Ent, isNew)
	   		if i == listdata.MsgIndex then
	   			listdata.username = Ent.SubContext.lbTitle.text
	   		end
	   	end)
	    UI_DATA.WNDsupmsgData.listdata =listdata
	    UIMGR.create_window("UI/WNDSupMsgcontent")
	elseif Msg.type == 3 then

		local listdata={}
	  	local MsgList = DY_DATA.MsgList
	 	listdata.MsgIndex = tonumber(index)
	  	Ref.SubMsg.Grp:dup( #MsgList, function (i, Ent, isNew)
	   		if i == listdata.MsgIndex then
	   			listdata.username = Ent.SubContext.lbTitle.text
	   			listdata.context = Msg.context
	   		end
	   	end)
	   	local StrT = Msg.context:split("_")
	   	listdata.id = StrT[1]
	   	local LowerList = DY_DATA.LowerList
	   	for i=1,#LowerList do
			if LowerList[i].id == Msg.people then
				listdata.name = LowerList[i].name
			end
		end
	   	listdata.storename = StrT[2]
	   	listdata.projectname = StrT[3]
	   	listdata.starttime = StrT[4]
	   	listdata.endtime = StrT[5]
	   	listdata.reasonstate = StrT[6]
	   	listdata.reason = StrT[7]
	   	listdata.submittime = StrT[8]
	   	listdata.messageid = Msg.id
	   	print("listdata is " .. JSON:encode(listdata))
	    UI_DATA.WNDSupAskOffMag.listdata = listdata
	    UIMGR.create_window("UI/WNDSupAskOffMag")

	end
end

local function on_subtop_btnwrite_click(btn)
	UIMGR.create_window("UI/WNDSupEditorMsg")
end

local function on_submsg_grp_ent_subcontext_btncontext_click(btn)
	local index = tonumber(btn.name)
	local MsgList = DY_DATA.MsgList
	local Msg = MsgList[index]
	if Msg.state == 1 then
		local nm = NW.msg("MESSAGE.CS.UPSTATU")
		nm:writeU32(Msg.id)
		NW.send(nm)
	end
	on_read_msg(index)
end 

local function on_submsg_grp_ent_subcontext_btndel_click(btn)
	local index = tonumber(btn.transform.parent.parent.name:sub(4))
	local MsgList = DY_DATA.MsgList
	local Msg = MsgList[index]
	local nm = NW.msg("MESSAGE.CS.DELETE")
	nm:writeU32(Msg.id)
	NW.send(nm)
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





local function on_tgltoggle_change(tgl)
	print("<color=#00ff00>on_submsg_grpmsg_entmsg_subcontext_btncontext_click 订阅回调</color>")
end

local function on_subcontext_btncontext_click(btn)

end

local function on_submsg_grpmsg_entmsg_subcontext_btndel_click(btn)

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
	if DY_DATA.LowerList == nil or next(DY_DATA.LowerList) == nil then
		local nm = NW.msg("MESSAGE.CS.GETLOWER")
		nm:writeU32(DY_DATA.User.id)
		NW.send(nm)
		return
	end
	local MsgList = DY_DATA.MsgList
	if MsgList == nil then 
		print("<color=#00ff00>获取信息数据失败</color>")
		libunity.SetActive(Ref.SubMsg.spNil, true)
		return 
	else
		print("<color=#00ff00>获取信息数据成功---</color>")

	end

	print(#MsgList)
	local LowerList = DY_DATA.LowerList
	if LowerList ~=nil and next(LowerList) ~=nil then
	UI_DATA.WNDSUPSENDEESELECT.LowerList=LowerList
	else
		UI_DATA.WNDSUPSENDEESELECT.LowerList={}
	end
	print(JSON:encode(LowerList))
	print("Length"..#MsgList)
	print("<color=#00ff00>获取信息数据成功------------</color>"..tostring(#MsgList))
	Ref.SubMsg.Grp:dup( #MsgList, function (i, Ent, isNew)
		local Msg = MsgList[i]
		print(JSON:encode(Msg))
		print(" Msg people ")
		print(Msg.people)
		print(JSON:encode(LowerList[Msg.people]))
		if tonumber(Msg.state) == 1 then
			libunity.SetActive(Ref.SubContext.spTip, true)
		else
			libunity.SetActive(Ref.SubContext.spTip, false)
		end
		local obj = libunity.FindGameObject(Ent.SubContext,"btnContext")
		if obj then
			obj.name=tostring(i);
			print("<color=#00ffff>按钮查找初始化成功</color>")
		else
			print("<color=#00ffff>按钮查找失败</color>")
		end
		if Msg.type == 2 then
			if Msg.people == 0 then
				Ent.SubContext.lbTitle.text = "系统消息"
			else
				for i=1,#LowerList do
					if LowerList[i].id == Msg.people then
						Ent.SubContext.lbTitle.text = LowerList[i].name
					end
				end
			end
				--LowerList[Msg.people] and LowerList[Msg.people].name or Msg.people
			Ent.SubContext.lbText.text = Msg.context
			Ent.SubContext.lbTime.text = Msg.time:sub(0,5)
			--Ent.SubContext.lbDay.text = Msg.day
			elseif Msg.type == 3 then 
				
				local StrT = Msg.context:split("_")
				local peopleName
				for i=1,#LowerList do
					if LowerList[i].id == Msg.people then
						peopleName = LowerList[i].name
					end
				end
				Ent.SubContext.lbTitle.text = "请假申请 : " .. peopleName 
				Ent.SubContext.lbText.text = " 申请时间： " ..StrT[8]
				Ent.SubContext.lbTime.text = Msg.time:sub(0,5)
			end
		end)
end

local function init_view()
	Ref.SubTop.btnWrite.onAction = on_subtop_btnwrite_click
	Ref.SubMsg.Grp.Ent.SubContext.btnContext.onAction = on_submsg_grp_ent_subcontext_btncontext_click
	Ref.SubMsg.Grp.Ent.SubContext.btnDel.onAction = on_submsg_grp_ent_subcontext_btndel_click
	Ref.SubBtm.spAtt.onAction = on_subbtm_spatt_click
	Ref.SubBtm.btnWork.onAction = on_subbtm_btnwork_click
	Ref.SubBtm.btnSch.onAction = on_subbtm_btnsch_click
	Ref.SubBtm.btnMsg.onAction = on_subbtm_btnmsg_click
	Ref.SubBtm.btnUser.onAction = on_subbtm_btnuser_click
	UIMGR.make_group(Ref.SubMsg.Grp, function (New, Ent)
		New.SubContext.btnContext.onAction = Ent.SubContext.btnContext.onAction
		New.SubContext.btnDel.onAction = Ent.SubContext.btnDel.onAction
	end)
	--!*以上：自动注册的回调函数*--
end

local function init_logic()
		UI_DATA.WNDMsgHint.state = true
	NW.subscribe("MESSAGE.SC.GETMESSAGELIST", on_ui_init)
	NW.subscribe("MESSAGE.SC.GETLOWER", on_ui_init)



	if DY_DATA.MsgList == nil or next(DY_DATA.MsgList) == nil then
		local nm = NW.msg("MESSAGE.CS.GETMESSAGELIST")
		nm:writeU32(DY_DATA.User.id)
		NW.send(nm)
		return
	end

	-- if DY_DATA.LowerList == nil or next(DY_DATA.LowerList) == nil then
	-- 	local nm = NW.msg("MESSAGE.CS.GETMESSAGELIST")
	-- 	nm:writeU32(DY_DATA.User.id)
	-- 	NW.send(nm)
	-- 	return
	-- end
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
		UI_DATA.WNDMsgHint.state = false
	NW.unsubscribe("MESSAGE.SC.GETMESSAGELIST", on_ui_init)
	NW.unsubscribe("MESSAGE.SC.GETLOWER", on_ui_init)
end

local P = {
	start = start,
	update_view = update_view,
	on_recycle = on_recycle,
}
return P

