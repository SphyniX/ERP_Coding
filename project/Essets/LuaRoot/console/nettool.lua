-- File Name : console/nettool.lua

local function login(acc, pass)
    local LOGIN = MERequire "libmgr/login"    
    LOGIN.LoginedAcc = { acc = acc, pass = _G.CMD5:MD5String(pass), }
    LOGIN.try_login(acc, pass)
end

local function connect(host, strPort)
    local port = tonumber(strPort)
    local NW = MERequire "network/networkmgr"
    NW.connect(host, port)
end

local function enter(index)
    local LOGIN = MERequire "libmgr/login"
    LOGIN.enter_server(tonumber(index))
end

local function logout()
    local LOGIN = MERequire "libmgr/login"
    LOGIN.do_logout()
end

return {
    login = login,
    connect = connect,
    enter = enter,
    logout = logout,
}