--
-- @file    ui/attendance/lc_wndattlog.lua
-- @authors zl
-- @date    2016-10-08 17:01:52
-- @desc    WNDAttLog
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

local function on_subtop_submouth_btnnew_click(btn)
	libunity.SetActive(Ref.SubTop.SubMouth.btnNew, false)
	libunity.SetActive(Ref.SubTop.SubMouth.btnLast, true)
	DY_DATA.Work.AttenceList = {}
	local nm = NW.msg("ATTENCE.CS.GETATTENCE")
	nm:writeU32(DY_DATA.User.id)
	nm:writeU32(2)
	NW.send(nm)
	-- on_ui_init()
end

local function on_subtop_submouth_btnlast_click(btn)
	libunity.SetActive(Ref.SubTop.SubMouth.btnNew, true)
	libunity.SetActive(Ref.SubTop.SubMouth.btnLast, false)
	DY_DATA.Work.AttenceList = {}
	local nm = NW.msg("ATTENCE.CS.GETATTENCE")
	nm:writeU32(DY_DATA.User.id)
	nm:writeU32(1)

	NW.send(nm)
	-- on_ui_init()
end

local function on_subtop_btnback_click(btn)
	UIMGR.close_window(Ref.root)
end





local function on_sublog_grplog_entlog_subaskoff_btnbutton_click(btn)
	local index = tonumber(btn.transform.parent.parent.name:sub(7))
	local AttenceList = DY_DATA.Work.AttenceList
	local Attence = AttenceList[index]
	UI_DATA.WNDAskOffMag.UnderId = Attence.UnderId
	UIMGR.create_window("UI/WNDAskOffMag")
	
end

local function on_ui_init( )
	
	local AttenceList = DY_DATA.Work.AttenceList
	if AttenceList == nil then
		local nm = NW.msg("ATTENCE.CS.GETATTENCE")
		nm:writeU32(DY_DATA.User.id)
		if libunity.SelfActive(Ref.SubTop.SubMouth.btnNew) then
		    nm:writeU32(1)
		else
			nm:writeU32(2)
		end
		NW.send(nm)
		return
	end
	Ref.SubLog.GrpLog:dup(#AttenceList, function ( i, Ent, isNew)
		local Attence = AttenceList[i]

		
		-- if Attence.Day ~= "无" then 
		Ent.lbUp.text = Attence.Up
		Ent.lbDown.text = Attence.Down
		Ent.lbLeaveTimes.text = Attence.LeaveTimes
		Ent.lbDay.text = Attence.Day
		Ent.lbWeek.text = Attence.Week
		if Attence.UnderState == 1 then
			Ent.SubAskOff.lbButton.text = "无"
			Ent.SubAskOff.btnButton:SetInteractable(false)
		else
			Ent.SubAskOff.lbButton.text = "查看"
			Ent.SubAskOff.btnButton:SetInteractable(true)
		end

		if i == 1 then
			libunity.SetActive(Ent.spNil,false)
		end
		-- end
	end)

end

local function init_view()
	Ref.SubTop.SubMouth.btnNew.onAction = on_subtop_submouth_btnnew_click
	Ref.SubTop.SubMouth.btnLast.onAction = on_subtop_submouth_btnlast_click
	Ref.SubTop.btnBack.onAction = on_subtop_btnback_click
	Ref.SubLog.GrpLog.Ent.SubAskOff.btnButton.onAction = on_sublog_grplog_entlog_subaskoff_btnbutton_click
	UIMGR.make_group(Ref.SubLog.GrpLog, function (New, Ent)
		New.SubAskOff.btnButton.onAction = Ent.SubAskOff.btnButton.onAction
	end)
	--!*以上：自动注册的回调函数*--
end

local function init_logic()
	libunity.SetActive(Ref.SubTop.SubMouth.btnNew, true)
	libunity.SetActive(Ref.SubTop.SubMouth.btnLast, false)

	NW.subscribe("ATTENCE.SC.GETATTENCE",on_ui_init)
	local AttenceList = DY_DATA.Work.AttenceList
	-- if AttenceList == nil then
		local nm = NW.msg("ATTENCE.CS.GETATTENCE")
		nm:writeU32(DY_DATA.User.id)
		nm:writeU32(1)
		NW.send(nm)
		return
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
	
end

local P = {
	start = start,
	update_view = update_view,
	on_recycle = on_recycle,
}
return P

