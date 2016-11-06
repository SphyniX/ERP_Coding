
--
-- @file    ui/login/lc_wndregist.lua
-- @authors ckxz
-- @date    2016-07-04 14:48:22
-- @desc    WNDRegist
--

local ipairs, pairs
    = ipairs, pairs
local libugui = require "libugui.cs"
local libunity = require "libunity.cs"
local UIMGR = MERequire "ui/uimgr"
local LOGIN = MERequire "libmgr/login.lua"
local UI_DATA = MERequire "datamgr/uidata.lua"
local Ref
local ProvinceList
local CityList
local City
local CityChoose
local ProvinceNow

local function on_bind_supervisored(Ret)
	if Ret.ret == 1 then
		UI_DATA.WNDBindPhone.on_changed = function ()
			UIMGR.create_window("UI/WNDSetUserInfo")
		end
		UI_DATA.WNDBindPhone.type = 1
		UIMGR.create_window("UI/WNDBindPhone")
	end
end
--!*以下：自动生成的回调函数*--

local function on_submain_subselect_subprovince_grp_ent_click(btn)

	print("a")
	local index = tonumber(btn.name:sub(4))
	CityList = _G.CFG.CityLib.get_city_list(index)
	Ref.SubMain.SubSelect.SubCity.Grp:dup(#CityList, function (i, Ent, isNew)
		local City = CityList[i]
		Ent.lbName.text = City.name
	end)

	Ref.SubMain.SubSelect.lbCity.text = ProvinceList[index].name
	ProvinceNow = ProvinceList[index].name
	-- print(Ref.SubMain.SubSelect.RectTransform.position)
	-- libunity.SetActive(Ref.SubMain.SubSelect.root,false)
end


local function on_submain_subselect_subcity_grp_ent_click(btn)
	local index = tonumber(btn.name:sub(4))

	Ref.SubMain.SubSelect.lbCity.text = ProvinceNow .. "  -   " .. CityList[index].name
	CityChoose = CityList[index].name
end

local function on_select_place_callback( id )
	Ref.SubMain.SubInfo.SubCity.lbcity.text = _G.CFG.CityLib.get_city(id).name
end

local function on_submain_subinfo_subcity_click(btn)
	UI_DATA.WNDSelectPlace.FromWhere = "1"
	UI_DATA.WNDSelectPlace.cityid = {1,10,11,12}
	UI_DATA.WNDSelectPlace.callbackfunc = on_select_place_callback
	UIMGR.create("UI/WNDSelectPlace")
	-- libunity.SetActive(Ref.SubMain.SubSelect.root,true)
end

local function on_submain_btnenter_click(btn)
	local inpSupname = Ref.SubMain.SubInfo.SubSupervisor.inpSupname.text
	local inpName = Ref.SubMain.SubInfo.SubName.inpName.text
	local inpCode = Ref.SubMain.SubInfo.SubCode.inpCode.text
	local inpCity = Ref.SubMain.SubInfo.SubCity.lbcity.text
	local UI_DATA = MERequire "datamgr/uidata.lua"
	if UI_DATA.WNDRegist.UserInfo == nil then UI_DATA.WNDRegist.UserInfo = {} end
	local UserInfo = UI_DATA.WNDRegist.UserInfo
	UserInfo.name = inpName
	UserInfo.city = inpCity
	UserInfo.supname = inpCode
	LOGIN.try_bind_supervisor(inpSupname, inpName, inpCode, on_bind_supervisored)
end

local function on_submain_subselect_btnsave_click(btn)
	Ref.SubMain.SubInfo.SubCity.lbcity.text = CityChoose
	
	libunity.SetActive(Ref.SubMain.SubSelect.root,false)
end

local function on_submain_subselect_btncancle_click(btn)

	libunity.SetActive(Ref.SubMain.SubSelect.root,false)
end

local function on_subtop_btnback_click(btn)
	UIMGR.close_window(Ref.root)
end



local function on_ui_init()

	print("on_ui_init"..JSON:encode(City).."end")
	Ref.SubMain.SubInfo.SubCity.lbcity.text = City and City.name or "选择所在城市"
	ProvinceList = _G.CFG.CityLib.get_province_list()

	--------TEST----------
	-- local cityid_list = {10,11,12}
	-- print(_G.CFG.CityLib.get_province_list_fromserver(cityid_list)[1].name)

	----------------------
	Ref.SubMain.SubSelect.SubProvince.Grp:dup(#ProvinceList, function (i, Ent, isNew)
		local Province = ProvinceList[i]
		Ent.lbName.text = Province.name
	end)
	libunity.SetActive(Ref.SubMain.SubSelect.btnProvince,false)
	libunity.SetActive(Ref.SubMain.SubSelect.btnCity,false)
	libunity.SetActive(Ref.SubMain.SubSelect.root,false)
end

local function init_view()
	Ref.SubMain.SubSelect.SubProvince.Grp.Ent.btn.onAction = on_submain_subselect_subprovince_grp_ent_click
	Ref.SubMain.SubSelect.SubCity.Grp.Ent.btn.onAction = on_submain_subselect_subcity_grp_ent_click
	Ref.SubMain.SubInfo.SubCity.btn.onAction = on_submain_subinfo_subcity_click
	Ref.SubMain.btnEnter.onAction = on_submain_btnenter_click
	Ref.SubMain.SubSelect.btnSave.onAction = on_submain_subselect_btnsave_click
	Ref.SubMain.SubSelect.btnCancle.onAction = on_submain_subselect_btncancle_click
	Ref.SubTop.btnBack.onAction = on_subtop_btnback_click
	UIMGR.make_group(Ref.SubMain.SubSelect.SubProvince.Grp, function (New, Ent)
		New.btn.onAction = Ent.btn.onAction
	end)
	UIMGR.make_group(Ref.SubMain.SubSelect.SubCity.Grp, function (New, Ent)
		New.btn.onAction = Ent.btn.onAction
	end)
	--!*以上：自动注册的回调函数*--
end

local function init_logic()
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
	
end

local P = {
	start = start,
	update_view = update_view,
	on_recycle = on_recycle,
}
return P

