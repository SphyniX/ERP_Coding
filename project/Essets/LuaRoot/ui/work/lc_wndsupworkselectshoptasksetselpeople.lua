--
-- @file    ui/work/lc_wndsupworkselectshoptasksetselpeople.lua
-- @authors cks
-- @date    2016-11-09 06:54:36
-- @desc    WNDsupWorkSelectShopTaskSetSelPeople
--

local ipairs, pairs
    = ipairs, pairs
local libugui = require "libugui.cs"
local libunity = require "libunity.cs"
local DY_DATA = MERequire "datamgr/dydata.lua"
local UI_DATA = MERequire "datamgr/uidata.lua"
local NW= MERequire "network/networkmgr"
local UIMGR = MERequire "ui/uimgr"
local Ref
local PromoterList
local PersonListForUpdate

local callbackfunc
--!*以下：自动生成的回调函数*--

local function on_subtop_cityselect_click(btn)
	
end

local function on_subtop_btnprevious_click(btn)
	UIMGR.close_window(Ref.root)
end

local function on_submsg_grpmsg_entmsg_subtgltoggle_change(tgl)

		-- body
	
end

local function on_btbsave_click(btn)
	-- local Tgl = {}
	-- Ref.SubMsg.GrpMsg:dup(#PromoterList,function (i,Ent,IsNew)
	-- 	local obj=libunity.FindComponent(Ent,"SubtglToggle","UIToggle")
	-- 	Tgl[i] = obj
	-- end)
	Ref.SubMsg.GrpMsg:dup(#PromoterList,function (i,Ent,IsNew)
		-- if PersonListForUpdate == nil or PersonListForUpdate == "" then PersonListForUpdate = {} end

		if libunity.ToggleSelfActive(Ent.SubtglToggle.root) == 1 then
			local AddOrNot = true
			for j=1,#PersonListForUpdate do
				if PersonListForUpdate[j].name == PromoterList[i].name then
					AddOrNot = false
					if PersonListForUpdate[j].StateNumber ~= 1 then
						PersonListForUpdate[j].StateNumber =0
					end
				end
			end
			
			if AddOrNot then
				local PersonForUpdate = {
					id = PromoterList[i].id,
					name = PromoterList[i].name,
					StateNumber = 1,
				}	
				table.insert(PersonListForUpdate,PersonForUpdate)
			end
		else
			for j=1,#PersonListForUpdate do
				if PersonListForUpdate[j].name == PromoterList[i].name then
					PersonListForUpdate[j].StateNumber = 2
				end
			end
		end
		print("_++++++++++++++++++++++++" .. JSON:encode(PersonListForUpdate))
	end)
	UI_DATA.WNDSupWorkSelectShopTaskSetSelPeople.PersonListForUpdate=PersonListForUpdate
	print("on_btbsave_click---------------"..JSON:encode(UI_DATA.WNDSupWorkSelectShopTaskSetSelPeople.PersonListForUpdate))
	callbackfunc(PersonListForUpdate)
	UIMGR.close_window(Ref.root)

end
--------------------弃用函数
local function findUIToggle(id)
local UIToggles = {}
local GrpMsg = libunity.FindComponent(id,"GrpMsg","RectTransform")
if GrpMsg~=nil then
	print("<color=#0f0>物体找到-----GrpMsg--yes-------</color>"..GrpMsg.name)

	for  i=0,GrpMsg.childCount-1 do
			local entMsg=GrpMsg:GetChild(i)--:GetComponent("UIToggle")
			if entMsg then
				local UIToggle=entMsg:GetChild(0)
				if UIToggle then
					local  UITogglecomponent=UIToggle:GetComponent("UIToggle")
					if UITogglecomponent then
						UITogglecomponent.isOn=false
						UIToggles[i]=UITogglecomponent
						print("UITogglecomponent------------yes---------"..UITogglecomponent.name)
					else
						print("UITogglecomponent--------------------------------no--------")
					end
				else
					print("UIToggle-------------------------no-------------------------")
				end
				--UIToggleGet.isOn=true
				--gameobj[i]=UIToggleGet
				print("entMsg----------------------yes-------------"..entMsg.name)
			else
				print("entMsg----------------------------no--------")

			end
		end
	else
		print("<color=#ff0>物体未找到--no--GrpMsg----------</color>")
	end
	if UIToggles~=nil and next(UIToggles)~=next then
		return UIToggles
	else
		return nil
	end
end

local function on_ui_init()
	print("第一册登陆")
	PromoterList = DY_DATA.get_promoter_List()
	print("PersonListForUpdate is------------------------------------- :" .. JSON:encode(PersonListForUpdate))
	print("PromoterList is ----------------------------:" .. JSON:encode(PromoterList))
	Ref.SubMsg.GrpMsg:dup(#PromoterList,function (i,Ent,IsNew)
		Ent.SubtglToggle.lbName.text = PromoterList[i].name
					print("PromoterList is ----------------UI_DATA.WNDSupWorkSelectShopTaskSetSelPeople.PersonListForUpdate------------:"..i .. JSON:encode(PromoterList))
		if PersonListForUpdate ~= nil then

			for j=1,#PersonListForUpdate do
				if PersonListForUpdate[j].name == PromoterList[i].name  then
					if PersonListForUpdate[j].StateNumber~=2 then
						libunity.ToggleActive(Ent.SubtglToggle.root,true)
						print("SubtglToggle " .. i)
					else
						libunity.ToggleActive(Ent.SubtglToggle.root,false)
					end
				else
						
				end
			end	
		end
	end)

end


local function init_view()

	Ref.SubTop.citySelect.onAction = on_subtop_cityselect_click
	Ref.SubTop.btnPrevious.onAction = on_subtop_btnprevious_click
	Ref.SubMsg.GrpMsg.Ent.SubtglToggle.tgl.onAction = on_submsg_grpmsg_entmsg_subtgltoggle_change
	Ref.BtbSave.onAction = on_btbsave_click
	UIMGR.make_group(Ref.SubMsg.GrpMsg, function (New, Ent)
		New.SubtglToggle.tgl.onAction = Ent.SubtglToggle.tgl.onAction
	end)
	--!*以上：自动注册的回调函数*--
end

local function init_logic()			-----------------------------zzg
	print("初始化界面--------------------------------------------------------")
	print(os.date("%w",os.time()))

	UI_DATA.WNDSupWorkSelectShopTaskSetSelPeople.dateState=false
	PersonListForUpdate = UI_DATA.WNDSupWorkSelectShopTaskSetSelPeople.PersonListForUpdate
	print("wndsupworkselectshoptasksetselpeople----UI_DATA.WNDSupWorkSelectShopTaskSetSelPeople.PersonListForUpdate"..JSON:encode(PersonListForUpdate))
	if UI_DATA.WNDSupWorkSelectShopTaskSetSelPeople.dateState then
	print("selectpeople  第一次进入界面")

	end
	--UI_DATA.WNDSupWorkSelectShopTaskSetSelPeople.PersonListForUpdate = nil	
	callbackfunc = UI_DATA.WNDSupWorkSelectShopTaskSetSelPeople.callbackfunc
	UI_DATA.WNDSupWorkSelectShopTaskSetSelPeople.callbackfunc = nil
	NW.subscribe("WORK.SC.GETSALES",on_ui_init)
	PromoterList = DY_DATA.get_promoter_List()
		if PromoterList==nil or next(PromoterList)==nil then

			if NW.connected() then
				local nm = NW.msg("WORK.CS.GETSALES")
				nm:writeU32(DY_DATA.User.id)
				NW.send(nm)
			end
		return
		end

	on_ui_init()										
	-- Ref.SubMsg.GrpMsg:dup(10,function (i,Ent,IsNew)
	-- 	-- body
	-- end)
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

