--
-- @file    ui/schedule/lc_wndsupdataprogersstaste.lua
-- @authors zl
-- @date    2016-11-09 03:26:12
-- @desc    WNDSupDataProgerssTaste
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

local SampleReList
local StoreId
--!*以下：自动生成的回调函数*--

local function on_ui_init( )
	-- body
	SampleReList = DY_DATA.StoreData.SampleReList
	if SampleReList == nil then
		local nm = NW.msg("REPORTED.CS.GETSUPSAMPLERE")
			nm:writeU32(StoreId)
			NW.send(nm)
		return
	end

	local TempSampleReList = {}
	for i=1,#SampleReList do
		if SampleReList[i].number ~= "nil" then 
			table.insert(TempSampleReList,SampleReList[i])
		end
	end

	Ref.SubProject.GrpProject:dup(#TempSampleReList, function (i, Ent, isNew)
		local SampleRe = TempSampleReList[i]
		Ent.lbName.text = SampleRe.name
		Ent.lbPeople.text = SampleRe.username
		Ent.lbPer.text = SampleRe.per
		Ent.lbNumber.text = SampleRe.number
		Ent.lbValue.text = SampleRe.value

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
	NW.subscribe("REPORTED.SC.GETSUPSAMPLERE", on_ui_init)

	SampleReList = DY_DATA.StoreData.SampleReList
	StoreId = UI_DATA.WNDSupStoreData.storeId
	if SampleReList == nil then
		local nm = NW.msg("REPORTED.CS.GETSUPSAMPLERE")
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
	NW.unsubscribe("REPORTED.SC.GETSUPSAMPLERE", on_ui_init)
end

local P = {
	start = start,
	update_view = update_view,
	on_recycle = on_recycle,
}
return P

