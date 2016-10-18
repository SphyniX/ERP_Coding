--
-- @file    datamgr/dydataop.lua
-- @authors xing weizhen (xingweizhen@rongygame.com)
-- @date    2015-11-20 11:14:27
-- @desc    操作类型的网络协议处理
--

local table = table
local NW = _G.PKG["network/networkmgr"]
local Unpack = _G.PKG["network/msgunpack"]

-- 通用操作结果返回
local function common_op_ret(nm)
    local ret = tonumber(nm:readString())
    -- _G.UI.Toast:make(nil, NW.get_error(ret)):show()
    if ret ~= 1 then 
        _G.UI.Toast:make(nil, NW.get_error(ret)):show()
    end
    return { ret = ret }
end

local function common_op_ret_suc(nm)
	local Ret = common_op_ret(nm)
	if Ret.ret == 1 then
		_G.UI.Toast:make(nil, NW.get_error(Ret.ret)):show()
	end
	return Ret
end

local function common_op_ret_userinfo(nm)
	local Ret = common_op_ret(nm)
	if Ret.ret == 1 then
		_G.UI.Toast:make(nil, NW.get_error(Ret.ret)):show()
		local DY_DATA = MERequire "datamgr/dydata.lua"
		local nm = NW.msg("USER.CS.GETUSERINFOR")
        nm:writeU32(DY_DATA.User.id)
        NW.send(nm)
	end
	return Ret
end

NW.regist("LOGIN.SC.LOGIN", function (nm)
	local Ret = common_op_ret(nm)
	-- if Ret.ret ~= 1 then
	-- 	local LOGIN = MERequire "libmgr/login.lua"
 --        LOGIN.do_logout()
	-- end
	return Ret
end)

NW.regist("ATTENCE.SC.UPWORK", common_op_ret_userinfo)

NW.regist("ATTENCE.SC.BEDEMOBILIZED", common_op_ret_userinfo)

NW.regist("ATTENCE.SC.FUGANG", common_op_ret_userinfo)

NW.regist("ATTENCE.SC.LEAVE", common_op_ret_userinfo)

NW.regist("ATTENCE.SC.VERIFYLATLNG", function (nm)
	local ret = tonumber(nm:readString())
	if ret ~= 1 then
		_G.UI.Toast:make(nil, "坐标不正确，不可打卡"):show()
	end
	return {ret = ret}
end)

NW.regist("ATTENCE.SC.PHUNCH", common_op_ret_userinfo)
-- function (nm)
-- 	local Ret = common_op_ret(nm)
-- 	if Ret.ret == 1 then
-- 		_G.UI.Toast:make(nil, NW.get_error(ret)):show()
-- 	end
-- 	return Ret
-- end
NW.regist("ATTENCE.SC.VERIFY",common_op_ret)

NW.regist("USER.SC.UPDATENAME", common_op_ret_userinfo)

NW.regist("USER.SC.UPDATETOCH", common_op_ret_userinfo)

NW.regist("USER.SC.VALPWD", common_op_ret_userinfo)

NW.regist("USER.SC.UPDATE", common_op_ret_userinfo)

NW.regist("USER.SC.UPDATEPHONE", common_op_ret_userinfo)

NW.regist("USER.SC.UPDATEINF", common_op_ret_userinfo)

NW.regist("USER.SC.UPDATEIDNUM", common_op_ret_userinfo)

NW.regist("USER.SC.UPDATECARDNO", common_op_ret_userinfo)

NW.regist("USER.SC.FEEDVACK", common_op_ret_userinfo)

NW.regist("USER.SC.CONTRACT", common_op_ret_userinfo)

NW.regist("REPORTED.SC.REPORTEDPRO", common_op_ret_suc)

NW.regist("REPORTED.SC.AUD", common_op_ret_suc)

NW.regist("REPORTED.SC.COM", common_op_ret_suc)

NW.regist("WORK.SC.ISSUED", common_op_ret_suc)

NW.regist("WORK.SC.UPDATEASS", common_op_ret_suc)

NW.regist("WORK.SC.DELETEASS", common_op_ret_suc)

NW.regist("MESSAGE.SC.SENDMESSAGE", common_op_ret_suc)

NW.regist("MESSAGE.SC.UPSTATU", common_op_ret_suc)

NW.regist("MESSAGE.SC.DELETE", function (nm)
	local Ret = common_op_ret(nm)
	if Ret.ret == 1 then
		_G.UI.Toast:make(nil, NW.get_error(Ret.ret)):show()
		local DY_DATA = MERequire "datamgr/dydata.lua"
		local NW = MERequire "network/networkmgr"
		local nm = NW.msg("MESSAGE.CS.GETMESSAGELIST")
		nm:writeU32(DY_DATA.User.id)
		NW.send(nm)
	end
	return Ret
end)

NW.regist("COMMON.SC.LOGOUT",function (nm)
	local LOGIN = MERequire "libmgr/login.lua"
    LOGIN.do_logout()
end)

return {}