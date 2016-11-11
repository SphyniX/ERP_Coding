--
-- @file    ui/system/lc_wndselectplace.lua
-- @authors zl
-- @date    2016-11-04 10:04:10
-- @desc    WNDSelectPlace
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

local ProvinceList
local CityList
local FromWhere
local cityid
local callbackfunc
local CityChoose
local ProvinceNow
local CityIDList
local NeedGetCityList
local projectId
local GETCITYDONE

--!*以下：自动生成的回调函数*--

local function on_submain_subselect_subprovince_grp_ent_click(btn)

	local index = tonumber(btn.name:sub(4))
	local province_name
	if NeedGetCityList then
		Ref.SubMain.SubSelect.SubProvince.Grp:dup(#ProvinceList+1, function (i, Ent, isNew)
			if i == index then 
				province_name = Ent.lbName.text
			end
		end)
	else
		Ref.SubMain.SubSelect.SubProvince.Grp:dup(#ProvinceList, function (i, Ent, isNew)
			if i == index then 
				province_name = Ent.lbName.text
			end
		end)
	end
	print("province_name in WNDSelectPlace is " .. province_name)
	if FromWhere == "fromserver" then
		if province_name == "全部" then
			CityList = {}
			table.insert(CityList,{id = 0 , name = "全部"})
		else
			CityList = _G.CFG.CityLib.get_city_list_fromserver(cityid,province_name)
		end
	else
		CityList = _G.CFG.CityLib.get_city_list(index)
	end
	print("CityLis in WNDSelectPlace is :" .. JSON:encode(CityList))

	Ref.SubMain.SubSelect.SubCity.Grp:dup(#CityList, function (i, Ent, isNew)
		local City = CityList[i]
		Ent.lbName.text = City.name
	end)
	

	Ref.SubMain.SubSelect.lbCity.text = province_name
	ProvinceNow = province_name
end

local function on_submain_subselect_subcity_grp_ent_click(btn)
	local index = tonumber(btn.name:sub(4))

	Ref.SubMain.SubSelect.lbCity.text = ProvinceNow .. "  -   " .. CityList[index].name
	CityChoose = CityList[index].id
	
end

local function on_submain_subselect_btnsave_click(btn)
	if CityChoose == nil then
		_G.UI.Toast:make(nil, "请选择城市"):show()
		return
	end
	if callbackfunc ~= nil then callbackfunc(CityChoose) end
	if NeedGetCityList then 
		if CityList ~= nil then
			Ref.SubMain.SubSelect.SubCity.Grp:dup(#CityList, function (i, Ent, isNew)
				Ent.lbName.text = nil
			end)
		end
		Ref.SubMain.SubSelect.SubProvince.Grp:dup(#ProvinceList+1, function (i, Ent, isNew)
			Ent.lbName.text = nil
		end)
	else
		if CityList ~= nil then
			Ref.SubMain.SubSelect.SubCity.Grp:dup(#CityList, function (i, Ent, isNew)
				Ent.lbName.text = nil
			end)
		end
		Ref.SubMain.SubSelect.SubProvince.Grp:dup(#ProvinceList, function (i, Ent, isNew)
			Ent.lbName.text = nil
		end)
	end
	Ref.SubMain.SubSelect.lbCity.text = nil
	ProvinceList = nil
	if CityList ~= nil then
		CityList = nil
	end
	FromWhere = nil
	cityid =nil
	callbackfunc = nil
	CityChoose = nil
	ProvinceNow = nil
	
	UIMGR.close(Ref.root)
end

local function on_submain_subselect_btncancle_click(btn)
	if NeedGetCityList then 
		if CityList ~= nil then
			Ref.SubMain.SubSelect.SubCity.Grp:dup(#CityList, function (i, Ent, isNew)
				Ent.lbName.text = nil
			end)
		end
		Ref.SubMain.SubSelect.SubProvince.Grp:dup(#ProvinceList+1, function (i, Ent, isNew)
			Ent.lbName.text = nil
		end)
	else
		if CityList ~= nil then
			Ref.SubMain.SubSelect.SubCity.Grp:dup(#CityList, function (i, Ent, isNew)
				Ent.lbName.text = nil
			end)
		end
		Ref.SubMain.SubSelect.SubProvince.Grp:dup(#ProvinceList, function (i, Ent, isNew)
			Ent.lbName.text = nil
		end)
	end
	Ref.SubMain.SubSelect.lbCity.text = nil
	ProvinceList = nil
	CityList = nil
	FromWhere = nil
	cityid =nil
	callbackfunc = nil
	CityChoose = nil
	ProvinceNow = nil
	UIMGR.close(Ref.root)
end

local function on_place_init()
	if NeedGetCityList then
		cityid = DY_DATA.GetCityList 
		if GETCITYDONE then  else
			local nm = NW.msg("ATTENCE.CS.GETCITY")
			nm:writeU32(DY_DATA.User.id)
			nm:writeU32(projectId)
			NW.send(nm)
			return
		end
	end

	if ProvinceList == nil then
		ProvinceList = {}
	end
	if FromWhere == "fromserver" then 
		ProvinceList = _G.CFG.CityLib.get_province_list_fromserver(cityid)
    else
    	ProvinceList = _G.CFG.CityLib.get_province_list()
    end
    if NeedGetCityList then 
	    Ref.SubMain.SubSelect.SubProvince.Grp:dup(#ProvinceList + 1, function (i, Ent, isNew)
	    	if i == 1 then
	    		Ent.lbName.text = "全部"
	    	else
				local Province = ProvinceList[i-1]
				Ent.lbName.text = Province.name
			end
			end)
	else
		Ref.SubMain.SubSelect.SubProvince.Grp:dup(#ProvinceList, function (i, Ent, isNew)
			local Province = ProvinceList[i]
			Ent.lbName.text = Province.name
		end)
	end
end

local function on_get_city()
	GETCITYDONE = true
	on_place_init()
end 

local function init_view()
	Ref.SubMain.SubSelect.SubProvince.Grp.Ent.btn.onAction = on_submain_subselect_subprovince_grp_ent_click
	Ref.SubMain.SubSelect.SubCity.Grp.Ent.btn.onAction = on_submain_subselect_subcity_grp_ent_click
	Ref.SubMain.SubSelect.btnSave.onAction = on_submain_subselect_btnsave_click
	Ref.SubMain.SubSelect.btnCancle.onAction = on_submain_subselect_btncancle_click
	UIMGR.make_group(Ref.SubMain.SubSelect.SubProvince.Grp, function (New, Ent)
		New.btn.onAction = Ent.btn.onAction
	end)
	UIMGR.make_group(Ref.SubMain.SubSelect.SubCity.Grp, function (New, Ent)
		New.btn.onAction = Ent.btn.onAction
	end)
	--!*以上：自动注册的回调函数*--
end

local function init_logic()
	NW.subscribe("ATTENCE.SC.GETCITY", on_get_city)
	NeedGetCityList = UI_DATA.WNDSelectPlace.NeedGetCityList
	UI_DATA.WNDSelectPlace.NeedGetCityList = nil
	projectId = UI_DATA.WNDSelectPlace.projectId
	UI_DATA.WNDSelectPlace.projectId = nil
	
	FromWhere = ""
	FromWhere = UI_DATA.WNDSelectPlace.FromWhere
	UI_DATA.WNDSelectPlace.FromWhere = nil
	cityid = {}
	if FromWhere == "fromserver" then 
		if NeedGetCityList then else
			cityid = UI_DATA.WNDSelectPlace.cityid
			UI_DATA.WNDSelectPlace.cityid = nil
		end
	end
	callbackfunc = UI_DATA.WNDSelectPlace.callbackfunc
	UI_DATA.WNDSelectPlace.callbackfunc = nil
	if NeedGetCityList == nil then NeedGetCityList = false end
	if NeedGetCityList then
		GETCITYDONE = false
		DY_DATA.GetCityList = nil
		print("Start Getting CityList!!")

		local nm = NW.msg("ATTENCE.CS.GETCITY")
		nm:writeU32(DY_DATA.User.id)
		nm:writeU32(projectId)
		NW.send(nm)
		return
		
	end
	on_place_init()

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
	NW.unsubscribe("ATTENCE.SC.GETCITY", on_place_init)
end

local P = {
	start = start,
	update_view = update_view,
	on_recycle = on_recycle,
}
return P

