--
-- @file    ui/message/lc_wndsupaskoffmag.lua
-- @authors zl
-- @date    2016-12-07 06:33:33
-- @desc    WNDSupAskOffMag
--

local ipairs, pairs
    = ipairs, pairs
local libugui = require "libugui.cs"
local libunity = require "libunity.cs"
local UIMGR = MERequire "ui/uimgr"
local UI_DATA = MERequire "datamgr/uidata.lua"
local DY_DATA = MERequire "datamgr/dydata.lua"
local NW = MERequire "network/networkmgr"
local Ref

local listdata
--!*以下：自动生成的回调函数*--

local function on_leaveaudit_back(nm)
	local ret = tonumber(nm)
    -- _G.UI.Toast:make(nil, NW.get_error(ret)):show()
    if ret ~= 1 then 
        -- _G.UI.Toast:make(nil, NW.get_error(ret)):show()
    else
    	local nm = NW.msg("MESSAGE.CS.DELETE")
		nm:writeU32(listdata.messageid)
		NW.send(nm)
    end
    UIMGR.close_window(Ref.root)
end

local function on_subtop_btnback_click(btn)
	UIMGR.close_window(Ref.root)
end

local function on_submain_subcontent_btnyes_click(btn)
	local nm = NW.msg("MESSAGE.CS.LEAVEAUDIT")
        nm:writeU32(listdata.id)
        nm:writeU32(2)
        NW.send(nm)
end

local function on_submain_subcontent_btnno_click(btn)
	local nm = NW.msg("MESSAGE.CS.LEAVEAUDIT")
        nm:writeU32(listdata.id)
        nm:writeU32(3)
        NW.send(nm)
end

local function init_view()
	Ref.SubTop.btnBack.onAction = on_subtop_btnback_click
	Ref.SubMain.SubContent.btnYes.onAction = on_submain_subcontent_btnyes_click
	Ref.SubMain.SubContent.btnNo.onAction = on_submain_subcontent_btnno_click
	--!*以上：自动注册的回调函数*--
end

local function init_logic()
	NW.subscribe("MESSAGE.SC.LEAVEAUDIT", on_leaveaudit_back)
	listdata = nil
	listdata = UI_DATA.WNDSupAskOffMag.listdata
	print("listdata is ".. JSON:encode(listdata))
	Ref.SubMain.SubContent.lbName.text = listdata.name
	Ref.SubMain.SubContent.lbTime.text = listdata.submittime .. "   "
	Ref.SubMain.SubContent.lbStore.text = listdata.storename
	Ref.SubMain.SubContent.lbProject.text = listdata.projectname
	Ref.SubMain.SubContent.lbStartTime.text = listdata.starttime
	Ref.SubMain.SubContent.lbEndTime.text = listdata.endtime
	if listdata.reason ~= "nil" then
		Ref.SubMain.SubContent.lbReason.text = listdata.reasonstate .. " : " .. listdata.reason
	else
		Ref.SubMain.SubContent.lbReason.text = listdata.reasonstate .. " : 未填写理由" 
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
	NW.unsubscribe("MESSAGE.SC.LEAVEAUDIT", on_leaveaudit_back)
end

local P = {
	start = start,
	update_view = update_view,
	on_recycle = on_recycle,
}
return P

