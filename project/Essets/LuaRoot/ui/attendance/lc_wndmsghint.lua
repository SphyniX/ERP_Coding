--
-- @file    ui/attendance/lc_wndmsghint.lua
-- @authors cks
-- @date    2016-12-11 18:51:29
-- @desc    WNDMsgHint
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
--!*以下：自动生成的回调函数*--
local function  on_ui_init()
	local MsgList = DY_DATA.MsgList
	--print("----"..JSON:decode(MsgList))
	local pointBl = false
	local MsgNum = 0
	if MsgList ~= nil and next(MsgList) ~=nil then
		for i = 1,#MsgList do
			if tonumber(MsgList[i].state) == 1 then 
				MsgNum = MsgNum + 1
			end
		end
		if MsgNum > 0 then 
			--libunity.SetActive(Ref.SubPoint.root, true)
			Ref.SubPoint.lbText.text = tostring(MsgNum)
			pointBl = true
		else
			pointBl = false
			libunity.SetActive(Ref.SubPoint.root, false)
		end
	else
		pointBl = false
		libunity.SetActive(Ref.SubPoint.root, false)
	end
	libunity.SetActive(Ref.SubPoint.root,UI_DATA.WNDMsgHint.state and pointBl)
	
end


local function init_view()
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
	print("DY_DATA.MsgList----------------"..JSON:encode(DY_DATA.MsgList))
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
local function fun()
end
local function on_recycle()
	NW.unsubscribe("MESSAGE.SC.GETMESSAGELIST", on_ui_init)
end
local function update()
	on_ui_init()
end
local P = {
	start = start,
	update = update,
	update_view = update_view,
	on_recycle = on_recycle,
}
return P

