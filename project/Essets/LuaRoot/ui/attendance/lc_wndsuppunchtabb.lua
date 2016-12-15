--
-- @file    ui/attendance/lc_wndsuppunchtabb.lua
-- @authors cks
-- @date    2016-12-01 08:38:24
-- @desc    WNDSupPunchTabb
--

local ipairs, pairs
    = ipairs, pairs
local libugui = require "libugui.cs"
local libunity = require "libunity.cs"
local UIMGR = MERequire "ui/uimgr"
local NW = MERequire "network/networkmgr"
local DY_DATA = MERequire "datamgr/dydata.lua"
local UI_DATA = MERequire "datamgr/uidata.lua"
local Ref
--!*以下：自动生成的回调函数*--

local function on_subtop_submouth_btnnew_click(btn)
	libunity.SetActive(Ref.SubTop.SubMouth.btnNew, false)
	libunity.SetActive(Ref.SubTop.SubMouth.btnLast, true)
	DY_DATA.Work.SupAttenceList = {}
	local nm = NW.msg("ATTENCE.CS.GETATTSUP")
	nm:writeU32(DY_DATA.User.id)
	nm:writeU32(2)
	NW.send(nm)
	-- on_ui_init()
end

local function on_subtop_submouth_btnlast_click(btn)
	libunity.SetActive(Ref.SubTop.SubMouth.btnNew, true)
	libunity.SetActive(Ref.SubTop.SubMouth.btnLast, false)
	DY_DATA.Work.SupAttenceList = {}
	local nm = NW.msg("ATTENCE.CS.GETATTSUP")
	nm:writeU32(DY_DATA.User.id)
	nm:writeU32(1)

	NW.send(nm)
	-- on_ui_init()
end

local function on_subtop_btnback_click(btn)
	UIMGR.close_window(Ref.root)
end





-- local function on_sublog_grplog_entlog_subaskoff_btnbutton_click(btn)
-- 	UIMGR.create_window("UI/WNDAskOffMag")
	
-- end

local function on_ui_init( )
	
	local AttenceList = DY_DATA.Work.SupAttenceList
	if AttenceList == nil then
		local nm = NW.msg("ATTENCE.CS.GETATTSUP")
		nm:writeU32(DY_DATA.User.id)
		if libunity.SelfActive(Ref.SubTop.SubMouth.btnNew) then
		    nm:writeU32(1)
		else
			nm:writeU32(2)
		end
		NW.send(nm)
		return
	end
	print("DY_DATA.Work.SupAttenceList is " .. JSON:encode(DY_DATA.Work.SupAttenceList))
	Ref.SubLog.GrpLog:dup(#AttenceList, function ( i, Ent, isNew)
		local Attence = AttenceList[i]

		if i == 1 then
			libunity.SetActive(Ent.spline,false)
		end
		-- if Attence.Day ~= "无" then 
		Ent.lbUp.text = Attence.Up:sub(1,5)
		Ent.lbDown.text = Attence.Down:sub(1,5)
		Ent.lbCityName.text = Attence.CityName
		Ent.lbStoreName.text = Attence.StoreName
		Ent.lbDay.text = Attence.Day
		Ent.lbWeek.text = Attence.Week
		-- end
	end)

end

local function init_view()
	Ref.SubTop.SubMouth.btnNew.onAction = on_subtop_submouth_btnnew_click
	Ref.SubTop.SubMouth.btnLast.onAction = on_subtop_submouth_btnlast_click
	Ref.SubTop.btnBack.onAction = on_subtop_btnback_click
	UIMGR.make_group(Ref.SubLog.GrpLog)
	--!*以上：自动注册的回调函数*--
end

local function init_logic()
	libunity.SetActive(Ref.SubTop.SubMouth.btnNew, true)
	libunity.SetActive(Ref.SubTop.SubMouth.btnLast, false)

	NW.subscribe("ATTENCE.SC.GETATTSUP",on_ui_init)
	print("______________________________Start init_logic ~ __________________________")
	DY_DATA.Work.SupAttenceList = nil
	local AttenceList = DY_DATA.Work.SupAttenceList
	if DY_DATA.Work.SupAttenceList == nil then
		print("______________________________Sending Require __________________________")
		local nm = NW.msg("ATTENCE.CS.GETATTSUP")
		nm:writeU32(DY_DATA.User.id)
		nm:writeU32(1)
		NW.send(nm)
		return
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

