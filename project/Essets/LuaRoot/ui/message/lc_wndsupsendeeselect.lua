--
-- @file    ui/message/lc_wndsupsendeeselect.lua
-- @authors cks
-- @date    2016-11-03 16:24:31
-- @desc    WNDSupSendeeSelect
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
local cityPanel
local  Toggles = {}
local  ToggleTemp = {}
local  Toggleobj={}
local  userMsg = {}
local tg = nil
--!*以下：自动生成的回调函数*--

local function on_subtop_cityselect_click(btn)
	  if cityPanel then
    libunity.SetActive(cityPanel, true)
    else
    print("未找到城市界面")
    end
end
local  function ToggleStateCtrl(id,ctrlbool)
	local idNumer=tonumber(id)
		if  ctrlbool then
				if not Toggles[idNumer] then
				Toggles[idNumer]=idNumer
		
				else
				Toggles[idNumer]=id
				end
				print("添加选择的人数--ww"..tostring(idNumer).."//"..#Toggleobj)
				Toggleobj[idNumer].isOn=true

		else
				Toggles[idNumer]=nil
				Toggleobj[idNumer].isOn=false				

			print("删除选择的人数"..idNumer)
			
		end
	-- body
end 
local function on_subtop_btnprevious_click(btn)
	if ToggleTemp~=nil and Toggles~=nil then
					for i=1,#ToggleTemp do
				ToggleStateCtrl(ToggleTemp[i],false)
			end
		end
	UI_DATA.WNDSUPSENDEESELECT={}
	UIMGR.close_window(Ref.root)
end


local function on_submsg_grpmsg_entmsg_subtgltoggle_change(tgl)
	local  cpt = tgl:GetComponent("UIToggle")
	if tostring(cpt.name) ~="0" then
		ToggleStateCtrl(cpt.name,cpt.isOn)
		-- if #Toggles>=#ToggleTemp then
		-- 	print("shezhi1")
		-- tg.isOn=true
		-- else
		-- 	tg.isOn=false
		-- 	print("shezhi2")
		-- end
	else
		if cpt.isOn then
			for i=1,#ToggleTemp do
							print("添加选择的人数-----"..tostring(ToggleTemp[i]))
				ToggleStateCtrl(ToggleTemp[i],true)

			end
		else
				for i=1,#ToggleTemp do
				ToggleStateCtrl(ToggleTemp[i],false)
			end
		end
	end
	for i=1 ,#Toggles do  
		UI_DATA.WNDSUPSENDEESELECT[i]=userMsg[i]
	end
	-- print("<color=#00ff00>点击的第一个按钮</color>")
	-- local  cpt= tgl:GetComponent("UIToggle") 
	-- if cpt.name=="Default" then

	-- 	if cpt.isOn==true then
	-- 		for i=1,#Toggles do
	-- 		Toggles[i].isOn=true;
	-- 		local  cpt= Toggles[i]:GetComponent("UIToggle")
	-- 	 	ToggleStateCtrl(cpt.name,true) 
	-- 		end
	-- 	else
	-- 		for i=1,#Toggles do
	-- 		Toggles[i].isOn=false;
	-- 		local  cpt= Toggles[i]:GetComponent("UIToggle")
	-- 	 	ToggleStateCtrl(cpt.name,false) 
	-- 		end
	-- 	end
	-- else
	-- 	local  cpt= tgl:GetComponent("UIToggle")
	-- 	 ToggleStateCtrl(cpt.name,cpt.isOn) 


	-- end
	-- for i, v in pairs(ToggleState) do  
	-- print("选择的人数"..tostring(v))
	-- end
	--UIMGR.create_window("UI/WNDSupSendeeSelect")
end
local function on_btbsave_click(btn)
UIMGR.create_window("UI/WNDSupReceiveMsg")
	--UIMGR.create_window("UI/WNDSupReceiveMsg")
end



local function on_tgltoggle_change(tgl)
	
end

local function on_editor_click(btn)
	
end
local function on_ui_init()
	local LowerList = UI_DATA.WNDSUPSENDEESELECT.LowerList
	print("JSON"..JSON:encode(LowerList))
	print("size"..tostring(#LowerList))

	Ref.SubMsg.GrpMsg:dup( #LowerList+1, function (i, Ent, isNew)
		local obj=libunity.FindComponent(Ent,"SubtglToggle","UIToggle")
			local  toggle = nil
		if i==1 then
			--Ent.tglToggle.lbText.text = "全选"
			
			if obj then
			obj.name="0"
			tg=obj
			toggle=obj
			else
			print("<color=#00ff00>on_ui_init ----------没有找到物体</color>")
			end
		else
			local Lower = LowerList[i-1]
			print(JSON:encode(Lower))
			print(" Lower people ")
			print(Lower.name)
			if obj then
			obj.name=tostring(i-1)
			Toggleobj[i-1]=obj
			if i>=#LowerList+1 then
				Toggleobj[i]=toggle
			end
			else
			print("<color=#00ff00>on_ui_init ----------没有找到物体</color>")
			end
			ToggleTemp[i-1]=i-1
			print(#Toggleobj.."/"..#ToggleTemp)
			userMsg[i-1] = {id=Lower.id,name=Lower.name}
			Ent.SubtglToggle.lbText.text = Lower.name--LowerList[Msg.people] and LowerList[Msg.people].name or Msg.people
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

local function init_logic()
	---UI_DATA.WNDSUPSENDEESELECT={}
	--MESSAGE.SC.GETLOWER

	on_ui_init()
	-- NW.subscribe("MESSAGE.SC.GETLOWER", on_ui_init)

	-- if DY_DATA.LowerList == nil or next(DY_DATA.LowerList) == nil then
	-- 	local nm = NW.msg("MESSAGE.CS.GETLOWER")
	-- 	nm:writeU32(DY_DATA.User.id)
	-- 	NW.send(nm)
	-- 	return
	-- end

	
   -- cityPanel= libunity.FindGameObject(nil,"/UIROOT/UICanvas/WNDSupSendeeSelect/SubCity")
   -- if cityPanel then
   --  libunity.SetActive(cityPanel, false)
   --  else
   --  print("未找到城市界面")
   --  end
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
	NW.unsubscribe("MESSAGE.SC.GETLOWER", on_ui_init)
end

local P = {
	start = start,
	update_view = update_view,
	on_recycle = on_recycle,
}
return P

