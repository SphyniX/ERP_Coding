--
-- @file    ui/message/lc_wndprojectmsg.lua
-- @authors zl
-- @date    2016-08-14 20:43:55
-- @desc    WNDprojectMsg
--

local ipairs, pairs
    = ipairs, pairs
local libugui = require "libugui.cs"
local libunity = require "libunity.cs"
local UIMGR = MERequire "ui/uimgr"
local DY_DATA = MERequire "datamgr/dydata.lua"
local UI_DATA = MERequire "datamgr/uidata.lua"
local Ref
--!*以下：自动生成的回调函数*--

local function on_submsg_grpmsg_entmsg_subcontext_btncontext_click(btn)
	
end

local function on_submsg_grpmsg_entmsg_subcontext_btndel_click(btn)
	
end

local function on_subbtm_btnsch_click(btn)
-- ##	UIMGR.WNDStack:pop()
	UIMGR.create_window("UI/WNDProjectSchedule")
end

local function on_subbtm_btnuser_click(btn)
-- ##	UIMGR.WNDStack:pop()
	UIMGR.create_window("UI/WNDProjectUser")
end

local function on_subtop_btncontact_click(btn)
	libunity.SetActive(Ref.SubTop.SubContact.root, true)
end

local function on_subtop_btnwrite_click(btn)
	
end

local function on_subtop_subcontact_btnarea_click(btn)
	libunity.SetActive(Ref.SubTop.SubContact.root, false)
end

local function on_subtop_subcontact_btnsup_click(btn)
	
	libunity.SetActive(Ref.SubTop.SubContact.root, false)
end

local function init_view()
	Ref.SubMsg.GrpMsg.Ent.SubContext.btnContext.onAction = on_submsg_grpmsg_entmsg_subcontext_btncontext_click
	Ref.SubMsg.GrpMsg.Ent.SubContext.btnDel.onAction = on_submsg_grpmsg_entmsg_subcontext_btndel_click
	Ref.SubBtm.btnSch.onAction = on_subbtm_btnsch_click
	Ref.SubBtm.btnUser.onAction = on_subbtm_btnuser_click
	Ref.SubTop.btnContact.onAction = on_subtop_btncontact_click
	Ref.SubTop.btnWrite.onAction = on_subtop_btnwrite_click
	Ref.SubTop.SubContact.btnArea.onAction = on_subtop_subcontact_btnarea_click
	Ref.SubTop.SubContact.btnSup.onAction = on_subtop_subcontact_btnsup_click
	UIMGR.make_group(Ref.SubMsg.GrpMsg, function (New, Ent)
		New.SubContext.btnContext.onAction = Ent.SubContext.btnContext.onAction
		New.SubContext.btnDel.onAction = Ent.SubContext.btnDel.onAction
	end)
	--!*以上：自动注册的回调函数*--
end

local function init_logic()
	libunity.SetActive(Ref.SubTop.SubContact.root, false)

	local MsgList = DY_DATA.MsgList
	Ref.SubMsg.GrpMsg:dup(  #MsgList, function (i, Ent, isNew)
		local Msg = MsgList[i]
		Ent.SubContext.lbTitle.text = Msg.title
		Ent.SubContext.lbText.text = Msg.context
		Ent.SubContext.lbTime.text = Msg.time
	end)
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

