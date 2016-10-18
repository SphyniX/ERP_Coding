--
-- @file    ui/system/lc_wndselectperson.lua
-- @authors zl
-- @date    2016-09-18 16:17:52
-- @desc    WNDSelectPerson
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

local PersonList, SelectList
local callback
--!*以下：自动生成的回调函数*--

local function on_submain_grp_ent_tglselect_change(tgl)
	local index = tonumber(tgl.transform.parent.name:sub(4))
	local Person = PersonList[index]
	SelectList[Person.id] = tgl.value
end

local function on_subtop_btnback_click(btn)
	UIMGR.close_window(Ref.root)
end

local function on_subtop_btnsave_click(btn)
	local List = {}

	for k,v in pairs(SelectList) do
		if v == true then
			local _, Info = table.has(PersonList, k)
			table.insert(List, Info)
		end
	end
	print(JSON:encode(List))
	print(JSON:encode(SelectList))
	callback(List)
	UIMGR.close_window(Ref.root)
end

local function on_ui_init()
	PersonList = DY_DATA.get_promoter_List()
	if PersonList == nil or #PersonList == 0 then
		return
	end
	Ref.SubMain.Grp:dup(#PersonList, function (i, Ent, isNew)
		local Person = PersonList[i]
		Ent.lbName.text = Person.name
		Ent.tglSelect.value = SelectList[Person.id] == true
	end)
end

local function init_view()
	Ref.SubMain.Grp.Ent.tglSelect.onAction = on_submain_grp_ent_tglselect_change
	Ref.SubTop.btnBack.onAction = on_subtop_btnback_click
	Ref.SubTop.btnSave.onAction = on_subtop_btnsave_click
	UIMGR.make_group(Ref.SubMain.Grp, function (New, Ent)
		New.tglSelect.onAction = Ent.tglSelect.onAction
	end)
	--!*以上：自动注册的回调函数*--
end

local function init_logic()
	NW.subscribe("WORK.SC.GETSALES", on_ui_init)
	local SeList = UI_DATA.WNDSelectPerson.SelectList
	callback = UI_DATA.WNDSelectPerson.callback

	SelectList = {}
	for i,v in ipairs(SeList) do
		SelectList[v.id] = true
	end

	if DY_DATA.PromoterList == nil or next(DY_DATA.PromoterList) == nil then
		local nm = NW.msg("WORK.CS.GETSALES")
		nm:writeU32(DY_DATA.User.id)
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
	NW.unsubscribe("WORK.SC.GETSALES", on_ui_init)
end

local P = {
	start = start,
	update_view = update_view,
	on_recycle = on_recycle,
}
return P

