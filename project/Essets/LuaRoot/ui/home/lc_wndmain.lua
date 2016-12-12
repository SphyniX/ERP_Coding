--
-- @file    ui/home/lc_wndmain.lua
-- @authors ckxz
-- @date    2016-07-05 09:49:13
-- @desc    WNDMain
--

local ipairs, pairs
    = ipairs, pairs
local libugui = require "libugui.cs"
local libunity = require "libunity.cs"
local UIMGR = MERequire "ui/uimgr"
local DY_DATA = MERequire "datamgr/dydata.lua"
local UI_DATA = MERequire "datamgr/uidata.lua"
local TEXT = _G.ENV.TEXT
local NW = MERequire "network/networkmgr"
local MB = _G.UI.MessageBox
local LOGIN = MERequire "libmgr/login.lua"
local Ref

MERequire "datamgr/dydatactr"
MERequire "datamgr/dydataop"

local function on_sc_entergame(Ret)
	_G.UI.Waiting.hide()

	local limit = DY_DATA.User.limit

	if limit == 1 then
		UIMGR.create_window("UI/WNDMainAttendance")
	elseif limit == 2 then
		UIMGR.create_window("UI/WNDSupAttendance")
	elseif limit == 3 then
		UIMGR.create_window("UI/WNDAreaWork")
	elseif limit == 4 then
		UIMGR.create_window("UI/WNDProjectWork")
	else
		MB:make("", TEXT.tipPleaseReloginLong, true):set_event(function ()      
            LOGIN.do_logout()
    	end):show()
	end
end


local function on_get_data(Ret)
	print(JSON:encode(Ret))
	if Ret.ret ~= 1 then
		MB:make("", TEXT.tipPleaseReloginLong, true):set_event(function ()      
            LOGIN.do_logout()
    	end):show()
		return 
	end
    
	-- local nm = NW.msg("MESSAGE.CS.GETLOWER")
	-- nm:writeU32(DY_DATA.User.id)
	-- NW.send(nm)
	
	-- local nm = NW.msg("USER.CS.GETSUPERLIST")
	-- nm:writeU32(DY_DATA.User.id)
	-- NW.send(nm)

	-- local nm = NW.msg("MESSAGE.CS.GETMESSAGELIST")
	-- nm:writeU32(DY_DATA.User.id)
	-- NW.send(nm)

	-- local nm = NW.msg("WORK.CS.GETPROJECT")
	-- nm:writeU32(DY_DATA.User.id)
	-- NW.send(nm)
	print("------------Requiring Base Data !-------------")
	local nm = NW.msg("ATTENCE.CS.GETTIME")
	NW.send(nm)
	
    nm = NW.msg("USER.CS.GETUSERINFOR")
    local id = UI_DATA.WNDLogin.id
    nm:writeU32(id)
    NW.send(nm)
    print("------------Requiring End!-------------")
end
--!*以下：自动生成的回调函数*--

local function on_btnrelogin_click(btn)
	MB:make("", TEXT.tipPleaseReloginLong, true):set_event(function ()      
        LOGIN.do_logout()
	end):show()
end

local function on_btnregisted_click(btn)
	local LOGIN = MERequire "libmgr/login.lua"
    LOGIN.do_logout()
end
--判断用户类型
local function on_ui_init()
	UIMGR.create("UI/WNDMsgHint")     -----加载红点界面
	UI_DATA.WNDMsgHint.state = false   ----初始化消息红点是否显示

	if _G.Debug then
		local id = DY_DATA.User.id
		MERequire "datamgr/localdata.lua"
		DY_DATA.User.id = id
		if id == 1 then
			UIMGR.create_window("UI/WNDMainAttendance")

		elseif id == 2 then
			UIMGR.create_window("UI/WNDSupAttendance")
		elseif id == 3 then
			UIMGR.create_window("UI/WNDAreaWork")
		elseif id == 4 then
			UIMGR.create_window("UI/WNDProjectWork")
		else
			UIMGR.create_window("UI/WNDMainAttendance")
		end
	else
		local nm = NW.msg("LOGIN.CS.LOGIN")
		local id = UI_DATA.WNDLogin.id
	    nm:writeU32(id)
	    NW.send(nm)
	end
end

local function init_view()
	Ref.btnRelogin.onAction = on_btnrelogin_click
	Ref.btnRegisted.onAction = on_btnregisted_click
	--!*以上：自动注册的回调函数*--
end

local function init_logic()
	NW.subscribe("LOGIN.SC.LOGIN",on_get_data)
	NW.subscribe("USER.SC.GETUSERINFOR", on_sc_entergame)

	local UI_DATA = MERequire "datamgr/uidata.lua"
	local regist = UI_DATA.WNDMain.regist
	-- regist = regist ~= true
	UI_DATA.WNDMain.regist = nil
	libunity.SetActive(Ref.btnRegisted, regist)
	if regist then
		return
	else
		on_ui_init()
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
	NW.unsubscribe("LOGIN.SC.LOGIN", on_get_data)
	NW.unsubscribe("USER.SC.GETUSERINFOR", on_sc_entergame)
end

local P = {
	start = start,
	update_view = update_view,
	on_recycle = on_recycle,
}
return P

