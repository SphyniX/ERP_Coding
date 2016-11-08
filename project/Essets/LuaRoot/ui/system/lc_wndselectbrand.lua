--
-- @file    ui/system/lc_wndselectbrand.lua
-- @authors zl
-- @date    2016-11-04 10:04:10
-- @desc    WNDSelectBrand
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

local BrandList
local ProjectId
local BrandChoose
local callbackfunc
--!*以下：自动生成的回调函数*--

local function on_submain_subselect_subprovince_grp_ent_click(btn)

	local index = tonumber(btn.name:sub(4))
	if index ~= 1 then 
		Ref.SubMain.SubSelect.lbCity.text = BrandList[index-1].name
		BrandChoose = BrandList[index-1]
	else
		Ref.SubMain.SubSelect.lbCity.text = "全部"
		BrandChoose = {id = 0}
	end
end

local function on_submain_subselect_btnsave_click(btn)
	if callbackfunc ~= nil then callbackfunc(BrandChoose) end
	Ref.SubMain.SubSelect.SubProvince.Grp:dup(#BrandList, function (i, Ent, isNew)
		Ent.lbName.text = nil
	end)
	Ref.SubMain.SubSelect.lbCity.text = nil
	BrandList = nil
	ProjectId = nil
	callbackfunc = nil
	UIMGR.close(Ref.root)
end

local function on_submain_subselect_btncancle_click(btn)

	Ref.SubMain.SubSelect.SubProvince.Grp:dup(#BrandList, function (i, Ent, isNew)
		Ent.lbName.text = nil
	end)
	Ref.SubMain.SubSelect.lbCity.text = nil
	BrandList = nil
	ProjectId = nil
	callbackfunc = nil
	UIMGR.close(Ref.root)
end


local function on_brand_init()
	BrandList = DY_DATA.BrandList
	if BrandList == nil then
		if NW.connected() then
			local nm = NW.msg("WORK.CS.GETBRAND")
			nm:writeU32(DY_DATA.User.id)
			NW.send(nm)
		end
		return
	end
	
    Ref.SubMain.SubSelect.SubProvince.Grp:dup(#BrandList+1, function (i, Ent, isNew)
		if i ~= 1 then
			local Brand = BrandList[i-1]
			Ent.lbName.text = Brand.name
		else
			Ent.lbName.text = "全部"
		end
	end)
		
end


local function init_view()
	Ref.SubMain.SubSelect.SubProvince.Grp.Ent.btn.onAction = on_submain_subselect_subprovince_grp_ent_click
	Ref.SubMain.SubSelect.btnSave.onAction = on_submain_subselect_btnsave_click
	Ref.SubMain.SubSelect.btnCancle.onAction = on_submain_subselect_btncancle_click
	UIMGR.make_group(Ref.SubMain.SubSelect.SubProvince.Grp, function (New, Ent)
		New.btn.onAction = Ent.btn.onAction
	end)
	--!*以上：自动注册的回调函数*--
end

local function init_logic()
	NW.subscribe("WORK.SC.GETBRAND", on_brand_init)
	if NW.connected() then
		local nm = NW.msg("WORK.CS.GETBRAND")
		nm:writeU32(DY_DATA.User.id)
		NW.send(nm)
	end
	callbackfunc = UI_DATA.WNDSelectBrand.callbackfunc
	UI_DATA.WNDSelectBrand.callbackfunc = nil
	
	print("BrandList in WNDSelectBrand is :" .. JSON:encode(BrandList))
	


	on_brand_init()

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
	NW.unsubscribe("WORK.SC.GETBRAND", on_brand_init)
end

local P = {
	start = start,
	update_view = update_view,
	on_recycle = on_recycle,
}
return P

