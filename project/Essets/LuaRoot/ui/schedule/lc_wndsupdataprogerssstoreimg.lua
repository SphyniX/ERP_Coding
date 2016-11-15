--
-- @file    ui/schedule/lc_wndsupdataprogerssstoreimg.lua
-- @authors zl
-- @date    2016-11-09 03:12:23
-- @desc    WNDSupDataProgerssStoreIMg
--

local ipairs, pairs
    = ipairs, pairs
local libugui = require "libugui.cs"
local libunity = require "libunity.cs"
local UIMGR = MERequire "ui/uimgr"
local Ref
local DY_DATA = MERequire "datamgr/dydata.lua"
local UI_DATA = MERequire "datamgr/uidata.lua"
local NW = MERequire "network/networkmgr"

local storeId
local PhotoList
--!*以下：自动生成的回调函数*--

local function on_ui_init()
	-- body
	if PhotoList ~= nil then
		Ref.SubProject.GrpProject:dup(#PhotoList, function (i, Ent, isNew)
			local PhotoUser = PhotoList[i]
			Ent.lbName.text = PhotoUser.username
			local Photo = PhotoUser.Photo
			UIMGR.make_group(Ent.GrpPhoto)
			Ent.GrpPhoto:dup(#Photo, function (i, Ent, isNew)
				UIMGR.get_photo(Ent,Photo.photo)
				end)
			
		end)
	end
end


local function on_subtop_back_click(btn)
	UIMGR.close_window(Ref.root)
end

local function on_subproject_grpproject_entproject_grpphoto_entimage_click(btn)
	
end

local function init_view()
	-- Ref.SubProject.GrpProject.Ent.btn.onAction = on_subproject_grpproject_entproject_click
	Ref.SubTop.Back.onAction = on_subtop_back_click
	UIMGR.make_group(Ref.SubProject.GrpProject)
	
	--!*以上：自动注册的回调函数*--
end

local function init_logic()
	NW.subscribe("REPORTED.SC.GETSUPGETPHOTO",on_ui_init)
	
	storeId = UI_DATA.WNDSupStoreData.storeId 
	PhotoList =  DY_DATA.StoreData.PhotoList
	if PhotoList == nil then
		local nm = NW.msg("REPORTED.CS.GETSUPGETPHOTO")
		nm:writeU32(storeId)
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
	NW.unsubscribe("REPORTED.SC.GETSUPGETPHOTO",on_ui_init)
end

local P = {
	start = start,
	update_view = update_view,
	on_recycle = on_recycle,
}
return P

