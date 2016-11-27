--
-- @file    ui/message/lc_wndmainmsg.lua
-- @authors ckxz
-- @date    2016-08-03 10:11:58
-- @desc    WNDMainMsg
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

local function on_ui_init()
	local MsgList = DY_DATA.MsgList
	if MsgList == nil then 
		libunity.SetActive(Ref.SubMsg.spNil, true)
		return 
	end
	libunity.SetActive(Ref.SubMsg.spNil, #MsgList == 0)
		
	local LowerList = DY_DATA.LowerList
	print("LowerList :"..JSON:encode(LowerList))
	
	Ref.SubMsg.GrpMsg:dup( #MsgList, function (i, Ent, isNew)
		local Msg = MsgList[i]
		
		UIMGR.get_photo(Ent.SubContext.spIcon, Msg.icon)
		libunity.SetActive(Ent.SubContext.spTip, Msg.state == 1)

		if Msg.people == 0 then 
			Ent.SubContext.lbTitle.text = "系统消息"
		else
			for i=1,#LowerList do
				if LowerList[i].id == Msg.people then
					Ent.SubContext.lbTitle.text = LowerList[i].name
				end
			end
		end
		Ent.SubContext.lbText.text = Msg.context
		Ent.SubContext.lbTime.text = Msg.time:sub(0,8)
		Ent.SubContext.lbDay.text = Msg.day
	end)
end


local function on_read_msg(index)
	local MsgList = DY_DATA.MsgList
	local Msg = MsgList[index]
	print(JSON:encode(Msg))
	if Msg.type == 1 then
		UIMGR.create_window("UI/WNDMainWork")
	elseif Msg.type == 2 then
		UI_DATA.WNDMsgContext.Msg = Msg
		UIMGR.create_window("UI/WNDMsgContext")
	elseif Msg.type == 3 then

	end
end

--!*以下：自动生成的回调函数*--

local function on_submsg_grpmsg_entmsg_subcontext_btncontext_click(btn)
	local index = tonumber(btn.transform.parent.parent.name:sub(7))
	local MsgList = DY_DATA.MsgList
	local Msg = MsgList[index]
	if Msg.state == 1 then
		local nm = NW.msg("MESSAGE.CS.UPSTATU")
		nm:writeU32(Msg.id)
		NW.send(nm)
	end
end

local function on_submsg_grpmsg_entmsg_subcontext_btndel_click(btn)
	local index = tonumber(btn.transform.parent.parent.name:sub(7))
	local MsgList = DY_DATA.MsgList
	local Msg = MsgList[index]
	local nm = NW.msg("MESSAGE.CS.DELETE")
	nm:writeU32(Msg.id)
	NW.send(nm)
	Ref.SubMsg.GrpMsg:dup(0)
	on_ui_init()
end

local function on_subbtm_btnatt_click(btn)
-- ##	UIMGR.WNDStack:pop()
	UIMGR.create_window("UI/WNDMainAttendance")
end

local function on_subbtm_btnwork_click(btn)
-- ##	UIMGR.WNDStack:pop()
	UIMGR.create_window("UI/WNDMainWork")
end

local function on_subbtm_btnsch_click(btn)
-- ##	UIMGR.WNDStack:pop()
	UIMGR.create_window("UI/WNDMainSchedule")
end

local function on_subbtm_btnuser_click(btn)
-- ##	UIMGR.WNDStack:pop()
	UIMGR.create_window("UI/WNDMainUser")
end



local function init_view()
	Ref.SubMsg.GrpMsg.Ent.SubContext.btnContext.onAction = on_submsg_grpmsg_entmsg_subcontext_btncontext_click
	Ref.SubMsg.GrpMsg.Ent.SubContext.btnDel.onAction = on_submsg_grpmsg_entmsg_subcontext_btndel_click
	Ref.SubBtm.btnAtt.onAction = on_subbtm_btnatt_click
	Ref.SubBtm.btnWork.onAction = on_subbtm_btnwork_click
	Ref.SubBtm.btnSch.onAction = on_subbtm_btnsch_click
	Ref.SubBtm.btnUser.onAction = on_subbtm_btnuser_click
	UIMGR.make_group(Ref.SubMsg.GrpMsg, function (New, Ent)
		New.SubContext.btnContext.onAction = Ent.SubContext.btnContext.onAction
		New.SubContext.btnDel.onAction = Ent.SubContext.btnDel.onAction
	end)
	--!*以上：自动注册的回调函数*--
end

local function init_logic()
	NW.subscribe("MESSAGE.SC.GETMESSAGELIST", on_ui_init)
	-- NW.subscribe("MESSAGE.SC.GETINFOR", on_people_back)
	NW.subscribe("USER.SC.GETLOWER", on_ui_init)
	if DY_DATA.LowerList == nil or next(DY_DATA.LowerList) == nil then
		local nm = NW.msg("MESSAGE.CS.GETLOWER")
		nm:writeU32(DY_DATA.User.id)
		NW.send(nm)
	end

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