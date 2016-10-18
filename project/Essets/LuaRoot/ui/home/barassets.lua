-- File Name : ui/home/barassets.lua
local ipairs, pairs, tostring, table, math
    = ipairs, pairs, tostring, table, math    
local libugui = require "libugui.cs"
local libunity = require "libunity.cs"
local libsystem = require "libsystem.cs"
local UIMGR = MERequire "ui/uimgr"
local DY_DATA = MERequire "datamgr/dydata"
local Ref

local function debug_get_asset(Asset, amount)
	local Base = Asset:get_base_data()
	local name = UIMGR.cfgname(Base)
	_G.UI.MessageBox:make("获得"..name, "确认后获得"..name.." x"..amount, true)
		:set_event(function ( ... )
			_G.PKG["console/console"].parse_cmd(string.format("gm item add %d %d", Asset.id, amount))
		end)
		:show()
end
local PurcharseFunc = {
	[2] = function (Asset)
	 	debug_get_asset(Asset, 100)
	end,
	[3] = function (Asset)
		debug_get_asset(Asset, 1000000)
	end,
	[4] = function (Asset)
		debug_get_asset(Asset, 10000)
	end,
}

local function on_grpasset_entasset_click(btn)
	local index = tonumber(btn.name:sub(9))
	local Ent = Ref.GrpAsset.Ents[index]
	local purcharse_func = PurcharseFunc[Ent.assetID]
	local Asset = DY_DATA.get_item(Ent.assetID)
	if purcharse_func then
		purcharse_func(Asset)
	else		
		local TEXT = _G.ENV.TEXT
		_G.UI.MessageBox:make(tostring(Asset), TEXT.tipNotImplemented)
			:show()
	end
end

local function set_value(Item, Ent)
	if Item.limit then
		Ent.lbAmount.text = string.format("%d/%d", Item.amount, Item.limit)
	else
		if Item.amount < 1000 then
			Ent.lbAmount.text = tostring(Item.amount)
		else
			Ent.lbAmount.text = libsystem.StringFmt("{0:0,0}", Item.amount)
		end
	end
end

local function show_asset(Item, Ent)
	Item:show_ico(Ent.spIcon)
	set_value(Item, Ent)	
	libunity.SetActive(Ent.spAdd, PurcharseFunc[Ent.assetID] ~= nil)
end

-- Exports Methods
local function show(AssetIDs)
	local ItemDEF = _G.DEF.Item
	local DY_DATA_Assets = DY_DATA.Assets	

	if Ref == nil then 
		local go = UIMGR.create("UI/BARAssets", 10)
		Ref = libugui.GenLuaTable(go, "root")
		Ref.GrpAsset.Ent.btn.onAction = on_grpasset_entasset_click
		UIMGR.make_group(Ref.GrpAsset, function (New, Ent)
			New.btn.onAction = Ent.btn.onAction
		end)
	end

	if not libunity.IsActive(Ref.root) then
		UIMGR.create("UI/BARAssets", 10)
	end

	local GrpAsset = Ref.GrpAsset
	GrpAsset:dup(#AssetIDs, function (i, Ent, isNew)
		local assetID = AssetIDs[i]
		Ent.assetID = assetID
		local Asset = DY_DATA_Assets[assetID] or ItemDEF.new(assetID, 0)
		show_asset(Asset, Ent)
	end)
end

local function hide()
	if Ref and libunity.IsActive(Ref.root) then
		-- 立即隐藏
		UIMGR.close(Ref.root, true)
	end
end

local function update(Ret)
	if Ref and libunity.IsActive(Ref.root) then
		local DY_DATA_Assets = DY_DATA.Assets
		for _,v in ipairs(Ref.GrpAsset.Ents) do
			local Item = DY_DATA_Assets[v.assetID]
			set_value(Item, v)
		end
	end
end

do
	local NW = _G.PKG["network/networkmgr"]
	NW.subscribe("ROLE.SC.ROLE_ASSET", update)
end

local P = {
	show = show,
	hide = hide,
}
return P

