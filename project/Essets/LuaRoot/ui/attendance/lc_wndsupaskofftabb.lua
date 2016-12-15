--
-- @file    ui/attendance/lc_wndsupaskofftabb.lua
-- @authors cks
-- @date    2016-11-30 19:00:56
-- @desc    WNDSupAskOffTabb
--

local ipairs, pairs
    = ipairs, pairs
local libugui = require "libugui.cs"
local libunity = require "libunity.cs"
local UIMGR = MERequire "ui/uimgr"
local DY_DATA = MERequire "datamgr/dydata.lua"
local UI_DATA = MERequire "datamgr/uidata.lua"
local NW = MERequire "network/networkmgr"
local TEXT = _G.ENV.TEXT
local Ref

local LeaveList
--!*以下：自动生成的回调函数*--


local function on_ui_init( )
	-- body

	LeaveList = DY_DATA.LeaveList
	print("LeaveList is :" .. JSON:encode(LeaveList))
	Ref.SubMain.Grp:dup( #LeaveList, function (i, Ent, isNew)
		local Leave = LeaveList[i]
	   	Ent.lbStartTime.text = Leave.starttime
	   	Ent.lbEndTime.text = Leave.endtime
	   if Leave.state == 3 then 

	   		libunity.SetActive(Ent.SubAskOff.root,true)
	   		if Leave.reason ~= "nil" then
	   			Ent.SubAskOff.lbReason.text = Leave.reasonstate .. " : " .. Leave.reason
	   		else
	   			Ent.SubAskOff.lbReason.text = Leave.reasonstate .. " : 未填写理由"
	   		end
	   		libunity.SetActive(Ent.Pass,false)
	   		libunity.SetActive(Ent.Fail,true)
	   		-- libunity.SetActive(Ent.SubState.Button,true)
	   		libunity.SetActive(Ent.SubState.root,false)
	   		-- libunity.SetActive(Ent.SubState.SubState1.root,true)
	   		-- libunity.SetActive(Ent.SubState.SubState2.root,false)
	   		-- libunity.SetActive(Ent.SubState.SubState3.root,false)
	  
	   		libunity.SetActive(Ent.FailBack,false)
	   	elseif Leave.state == 2 then 
	   		libunity.SetActive(Ent.SubAskOff.root,true)
	   		if Leave.reason ~= "nil" then
	   			Ent.SubAskOff.lbReason.text = Leave.reasonstate .. " : " .. Leave.reason
	   		else
	   			Ent.SubAskOff.lbReason.text = Leave.reasonstate .. " : 未填写理由"
	   		end
	   		libunity.SetActive(Ent.Pass,true)
	   		libunity.SetActive(Ent.Fail,false)
	   		libunity.SetActive(Ent.SubState.root,false)
	   		libunity.SetActive(Ent.FailBack,true)
	   	elseif Leave.state == 1 then 
	   		libunity.SetActive(Ent.SubAskOff.root,true)
	   		if Leave.reason ~= "nil" then
	   			Ent.SubAskOff.lbReason.text = Leave.reasonstate .. " : " .. Leave.reason
	   		else
	   			Ent.SubAskOff.lbReason.text = Leave.reasonstate .. " : 未填写理由"
	   		end
	   		libunity.SetActive(Ent.Pass,false)
	   		libunity.SetActive(Ent.Fail,false)
	   		-- libunity.SetActive(Ent.SubState.Button,true)
	   		libunity.SetActive(Ent.SubState.SubState1.root,false)
	   		libunity.SetActive(Ent.SubState.SubState2.root,true)
	   		libunity.SetActive(Ent.SubState.SubState3.root,false)
	   		
	   		libunity.SetActive(Ent.FailBack,true)

	   		-- Ent.lbReason.text = Leave.reasonstate .. " : " .. Leave.reason
	   	end



	end)
end


local function on_subtop_btnback_click(btn)
	UIMGR.close_window(Ref.root)
end


local function init_view()
	Ref.SubTop.btnBack.onAction = on_subtop_btnback_click
	UIMGR.make_group(Ref.SubMain.Grp)
	--!*以上：自动注册的回调函数*--
end

local function init_logic()
	NW.subscribe("ATTENCE.SC.GETLEAVELS", on_ui_init)
	LeaveList = {}
	LeaveList = DY_DATA.LeaveList
	print("LeaveList is :" .. JSON:encode(LeaveList))
	-- if next(LeaveList) == nil then
		local nm = NW.msg("ATTENCE.CS.GETLEAVELS")
		nm:writeU32(DY_DATA.User.id)
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
	NW.unsubscribe("ATTENCE.SC.GETLEAVELS", on_ui_init)
end

local P = {
	start = start,
	update_view = update_view,
	on_recycle = on_recycle,
}
return P










