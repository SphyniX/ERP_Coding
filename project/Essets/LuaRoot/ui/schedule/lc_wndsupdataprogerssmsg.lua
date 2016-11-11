--
-- @file    ui/schedule/lc_wndsupdataprogerssmsg.lua
-- @authors zl
-- @date    2016-11-09 03:39:02
-- @desc    WNDSupDataProgerssMsg
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

local FeedBcakListRe
local StoreId
--!*以下：自动生成的回调函数*--
local function on_ui_init( )
	-- body
	FeedBcakListRe = DY_DATA.StoreData.FeedBcakListRe
	if FeedBcakListRe == nil then
		local nm = NW.msg("REPORTED.CS.GETSUPGETFEEDBACK")
			nm:writeU32(StoreId)
			NW.send(nm)
		return
	end

	local TempFeedBcakListRe = {}
	for i=1,#FeedBcakListRe do
		if FeedBcakListRe[i].value ~= "nil" then 
			table.insert(TempFeedBcakListRe,FeedBcakListRe[i])
		end
	end

	Ref.SubProject.GrpProject:dup(#TempFeedBcakListRe, function (i, Ent, isNew)
		local FeedBcak = TempFeedBcakListRe[i]
		Ent.lbPeople.text = FeedBcak.username
		Ent.lbValue.text = FeedBcak.value
	end)

end


local function on_subtop_back_click(btn)
	UIMGR.close_window(Ref.root)
end

local function init_view()
	Ref.SubTop.Back.onAction = on_subtop_back_click
	UIMGR.make_group(Ref.SubProject.GrpProject)
	--!*以上：自动注册的回调函数*--
end

local function init_logic()
	NW.subscribe("REPORTED.SC.GETSUPGETFEEDBACK", on_ui_init)

	FeedBcakListRe = DY_DATA.StoreData.FeedBcakListRe
	StoreId = UI_DATA.WNDSupStoreData.storeId
	if FeedBcakListRe == nil then
		local nm = NW.msg("REPORTED.CS.GETSUPGETFEEDBACK")
			nm:writeU32(StoreId)
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
	NW.unsubscribe("REPORTED.SC.GETSUPGETFEEDBACK", on_ui_init)
end

local P = {
	start = start,
	update_view = update_view,
	on_recycle = on_recycle,
}
return P

