--
-- @file    ui/schedule/lc_wndsupdatagoodanalysis.lua
-- @authors zl
-- @date    2016-11-09 02:42:30
-- @desc    WNDSupDataGoodAnalysis
--

local ipairs, pairs
    = ipairs, pairs
local libugui = require "libugui.cs"
local libunity = require "libunity.cs"
local UIMGR = MERequire "ui/uimgr"
local UI_DATA = MERequire "datamgr/uidata.lua"
local DY_DATA = MERequire "datamgr/dydata.lua"
local LOGIN = MERequire "libmgr/login.lua"
local NW = MERequire "network/networkmgr"
local Ref
--!*以下：自动生成的回调函数*--

local function on_submain_grp_ent_click(btn)
	UI_DATA.WNDSupDataGoodAnalysis.index = tonumber(btn.name:sub(4))
	UIMGR.create_window("UI/WNDsupGoodContent")
end

local function on_subtop_back_click(btn)
	UIMGR.close_window(Ref.root)
end

local function on_subcell_click(btn)
	
end

local function on_subcell1_click(btn)
	
end
local function on_panel_init()
	local roject =  DY_DATA.SchProjectList[UI_DATA.WNDSelectStore.projectId] -- DY_DATA.StoreData.ComListRe
	local ComListRe = roject.ComList
	if ComListRe == nil and #ComListRe == 0 then 

	else
		Ref.SubMain.Grp:dup(# ComListRe,function (i,Ent,IsNew)
			Ent.name.text = ComListRe[i].name
			print("ComListRe[i].name----------------------------"..ComListRe[i].name)
			print("ComListRe[i].name----------------------------"..ComListRe[i].id)
			-- body
		end)
	end
	
end 

local function init_view()
	Ref.SubMain.Grp.Ent.btn.onAction = on_submain_grp_ent_click
	Ref.SubTop.Back.onAction = on_subtop_back_click
	UIMGR.make_group(Ref.SubMain.Grp, function (New, Ent)
		New.btn.onAction = Ent.btn.onAction
	end)
	--!*以上：自动注册的回调函数*--
end

local function init_logic()
	NW.subscribe("WORK.SC.GETCOMLIST",on_panel_init)
	 local roject =   DY_DATA.SchProjectList[UI_DATA.WNDSelectStore.projectId] -- DY_DATA.StoreData.ComListRe
	 local ComListRe = roject.ComList
	 ComListRe=nil
	 if ComListRe == nil or #ComListRe == 0 then
	 	local nm = NW.msg("WORK.CS.GETCOMLIST")
	 	print("UI_DATA.WNDSupStoreData.storeId-------------"..UI_DATA.WNDSelectStore.projectId)
	 	nm:writeU32(UI_DATA.WNDSelectStore.projectId)
	 	NW.send(nm)
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

