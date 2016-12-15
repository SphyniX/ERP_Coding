--
-- @file    ui/message/lc_wndsupmsgcontent.lua
-- @authors cks
-- @date    2016-11-08 00:45:20
-- @desc    WNDSupMsgcontent
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

local function on_subtop_back_click(btn)
	UIMGR.create_window("UI/WNDSupMsg")
end

local function init_view()
	Ref.SubTop.Back.onAction = on_subtop_back_click
	--!*以上：自动注册的回调函数*--
end

local function init_logic()
	           --  id = tonumber(nm:readString()),
            -- type = tonumber(nm:readString()), -- (1, 项目， 2， 文本， 3 请假)
            -- people = tonumber(nm:readString()),
            -- context = nm:readString(),
            -- day = nm:readString(),
            -- time = nm:readString(),
            -- state = nm:readString()
            local data = DY_DATA.MsgList[UI_DATA.WNDsupmsgData.listdata.MsgIndex]
            print("data.context -----------is:"..data.context)
            Ref.SubTitle.usermame.text =  UI_DATA.WNDsupmsgData.listdata.username
            Ref.lbText.text=data.context
            Ref.SubTitle.date.text=data.day.." "..data.time
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

