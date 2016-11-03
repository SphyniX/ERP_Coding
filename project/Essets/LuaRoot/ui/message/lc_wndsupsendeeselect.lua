--
-- @file    ui/message/lc_wndsupsendeeselect.lua
-- @authors cks
-- @date    2016-11-03 16:24:31
-- @desc    WNDSupSendeeSelect
--

local ipairs, pairs
    = ipairs, pairs
local libugui = require "libugui.cs"
local libunity = require "libunity.cs"
local UIMGR = MERequire "ui/uimgr"
local Ref
local cityPanel
--!*以下：自动生成的回调函数*--

local function on_subtop_cityselect_click(btn)
	  if cityPanel then
    libunity.SetActive(cityPanel, true)
    else
    print("未找到城市界面")
    end
end

local function on_subtop_btnprevious_click(btn)
	UIMGR.create_window("UI/WNDSupReceiveMsg")
end

local function on_submsg_grpmsg_entmsg_subcontext_tgltoggle_change(tgl)
	
end

local function on_btbsave_click(btn)
	UIMGR.create_window("UI/WNDSupReceiveMsg")
end

local function on_editor_click(btn)
	
end

local function init_view()
	Ref.SubTop.citySelect.onAction = on_subtop_cityselect_click
	Ref.SubTop.btnPrevious.onAction = on_subtop_btnprevious_click
	Ref.SubMsg.GrpMsg.Ent.SubContext.tglToggle.onAction = on_submsg_grpmsg_entmsg_subcontext_tgltoggle_change
	Ref.BtbSave.onAction = on_btbsave_click
	UIMGR.make_group(Ref.SubMsg.GrpMsg, function (New, Ent)
		New.SubContext.tglToggle.onAction = Ent.SubContext.tglToggle.onAction
	end)
	--!*以上：自动注册的回调函数*--
end

local function init_logic()
   cityPanel= libunity.FindGameObject(nil,"/UIROOT/UICanvas/WNDSupSendeeSelect/SubCity")
   if cityPanel then
    libunity.SetActive(cityPanel, false)
    else
    print("未找到城市界面")
    end
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

