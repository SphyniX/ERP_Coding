--
-- @file    network/networkmgr.lua
-- @authors xing weizhen (xingweizhen@rongygame.com)
-- @date    2015-04-17 19:07:04
-- @desc    网络消息管理
--

local tostring, pairs, table, string
    = tostring, pairs, table, string
local libunity = require "libunity.cs"
local libasset = require "libasset.cs"
local libsystem = require "libsystem.cs"
local libnetwork = require "libnetwork.cs"
local UI_DATA = MERequire "datamgr/uidata.lua"
local UE_Time = import("UnityEngine.Time")
local TEXT = _G.ENV.TEXT
local MSG = _G.PKG["network/msgdef"]
local MB = _G.UI.MessageBox

local GameCli
-- 发送队列
local MsgQueue = _G.DEF.Queue:new()
-- HTTP回调
local HTTPHandler = {}
-- 下载回调
local DownloadCbf = {}
-- TCP回调
local NCHandler = {}
-- 订阅回调
local SubscriberSet = {}
-- 连接回调
local onConnected, onDisconnected, on_relogin_fail
-- 重连次数
local cntReconnect, lastHeartTime = 0, 0
-- 没有返回的消息列表

-- 没有返回的消息列表
local NtfNmList = {
    [MSG["COMMON.CS.HEART"]] = true,
}

local log = _G.DEF.Client.log

local function chk_msg_type(code)
    local id = code
    if type(code) == "string" then 
        id = MSG[code] 
        if id == nil then
            libunity.LogE("错误的消息ID={0}", code)
            return
        end
    end
    return id
end

local function default_on_disconnect(cli)
    -- if cli.Error == nil then
    --     _G.UI.Toast:make(nil, TEXT.tipConnectTimeout):show()
    -- end
end

local function on_tcp_connected(cli)
    _G.UI.Waiting.hide()    

    lastHeartTime = UE_Time.realtimeSinceStartup
    cntReconnect = 0
    if onConnected then 
        onConnected()
    else
        cli:disconnect()
        libunity.LogW("TCP连接回调为空!")
    end
end

local function on_tcp_disconnected(cli)
    _G.UI.Waiting.hide()
    if cli.Error == nil then
        libunity.LogW("Timeout")
    end
    if onDisconnected then onDisconnected(cli) end
end

local P = {}

-- 回调方法
function P.on_nc_init(mgr)
    GameCli = _G.DEF.Client.create("GameTcp")
    GameCli.doRecieving = P.on_nc_receiving
    GameCli:set_event(on_tcp_connected, on_tcp_disconnected)
    log("<color=yellow>网络模块初始化：</color> {0}", GameCli)
end

function P.on_nc_receiving(cli, nm)
    if nm then
        if cli == GameCli then
            lastHeartTime = UE_Time.realtimeSinceStartup            
        end
        
        _G.UI.Waiting.hide()
        local id = nm.type
        local handle = NCHandler[id]
        libunity.LogD("NCHandler : {0}", JSON:encode(NCHandler))
        local msgName = tostring(nm)
        if handle then
            log("{1} <-- {0}", msgName, cli)
            local Ret = handle(nm)
            local Subscriber = SubscriberSet[id]
            if Subscriber then
                log("{2}   > {0} x{1}", msgName, #Subscriber, cli)
                -- 自动发布订阅消息
                for _,publish in ipairs(Subscriber) do publish(Ret) end
            end
        else
            log("<color=red>miss handler for: {0} </color>", msgName)
        end
        -- 弹出消息
        MsgQueue:dequeue()
        local o = MsgQueue:peek() or {}
        GameCli:send(o.nm, false, o.only)
    end
end

function P.on_www(tag, resp, isDone, err)
    _G.UI.Waiting.hide()

    local cbf = HTTPHandler[tag]
    if isDone then
        if err then
            log("<color=red>Http Fail: [{0}]{1}; {2}</color>", tag, resp, err)
        end
    else
        local info = string.format("[%s]%s", tag, resp)
        log("<color=red>Http Timeout:[{0}]{1}</color>", tag, resp)
    end
    if cbf then cbf(resp, isDone, err) end
end

function P.on_download( url, current, totalm, err)
    print( url, current, totalm, err)

    local cbf = DownloadCbf[url]
    if not err then
        local progress = current / totalm
        if cbf then cbf(url, current, totalm,  nil) end
    else
        log("<color=red>Download Error: {0}</color>", err)
        if cbf then cbf(url, current, totalm, err) end
    end
end

local function on_reconnect_fail()
    P.clear()
    MB:make("", TEXT.tipPleaseReloginLong, true):set_event(function ()
            local LOGIN = MERequire "libmgr/login.lua"
            LOGIN.do_logout()
    end):show()
end


-- 账号重新登录
local function on_account_relogined(resp, isDone, err)
    _G.UI.Waiting.hide()
    if isDone and err == nil then
        local Ret = JSON:decode(resp)
        if Ret.ret == 1 then
            local UI_DATA_WNDLogin = UI_DATA.WNDLogin      
            local Server = UI_DATA_WNDLogin.Server
            local LOGIN = MERequire "libmgr/login.lua"
            LOGIN.enter_server(Server.adress, Server,port)
        end
        return
    end
    on_reconnect_fail()
end

local function try_login_account(LoginedAcc, cbf)
    local HttpParams = {
        phone = LogicedAcc.acc,
        password = LogicedAcc.pass,
    }
    local LOGIN = MERequire "libmgr/login.lua"
    P.http_post("LOGIN", LOGIN.HTTPSet.loginInterface(), "", HttpParams, "", cbf)
    _G.UI.Waiting.show()
end

on_relogin_fail = function ()
    _G.UI.Waiting.hide()
    P.clear()
    MB:make("", TEXT.tipPleaseReloginLong, true):set_event(function ()      
        local UI_DATA_WNDLogin = UI_DATA.WNDLogin
        local LOGIN = MERequire "libmgr/login.lua"
        try_login_account(LOGIN.LoginedAcc, on_account_relogined)
    end):show()
end
local function on_relogin_suc()
    _G.UI.Waiting.hide()
    local DY_DATA = MERequire "datamgr/dydata.lua"
    local nm = P.msg("LOGIN.CS.LOGIN")
    
    nm:writeU32(UI_DATA.WNDLogin.id)
    P.send(nm)
    print("重新登录")
end

local function do_relogin()
    if cntReconnect < 3 then
        -- 重连
        local UI_DATA_WNDLogin = UI_DATA.WNDLogin
        local Server = UI_DATA_WNDLogin.Server
        if Server then
            print("重连"..cntReconnect, JSON:encode(Server))
            _G.UI.Waiting.show()
            
            local host, port = Server.address, Server.port         
            P.connect(host, port, on_relogin_suc)
        end
    else
        on_relogin_fail()
    end
    cntReconnect = cntReconnect + 1
end

function P.check_state(tm)
    print(tm, UE_Time.realtimeSinceStartup, lastHeartTime)
    local DY_DATA = _G.PKG["datamgr/dydata"]

    if P.connected()then
        -- 尝试发送心跳
        local currTime = UE_Time.realtimeSinceStartup
        print(currTime , lastHeartTime)
        if currTime - lastHeartTime > 8 then
            lastHeartTime = currTime
            P.send(P.msg("COMMON.CS.HEART"))
            print("KEEP HEART")
        end
    else
        --libunity.LogW("网络未连接...")
        if not table.void(DY_DATA.User) then
            if GameCli.Error ~= nil then
                do_relogin()
            end
        end
    end
end

-- ============================================================================
local HTTP_TIMEOUT = 15

-- 启动一个HTTP POST
function P.http_post(tag, url, param, form, headers, cbf)
    libnetwork.HttpPost(tag, url, param, form, headers, HTTP_TIMEOUT);
    if cbf then HTTPHandler[tag] = cbf end
end

-- 启动一个HTTP GET
function P.http_get(tag, url, param, cbf)
    libnetwork.HttpGet(tag, url, param, HTTP_TIMEOUT);
    if cbf then HTTPHandler[tag] = cbf end
end

function P.http_upphoto(tag, url, param, form, Image, headers, cbf)
    libnetwork.HttpUpPhoto(tag, url, param, form, Image, headers, HTTP_TIMEOUT);
    if cbf then HTTPHandler[tag] = cbf end
end

function P.http_upmorephoto(tag, url, param, form, PhotoList, headers, cbf)
    libnetwork.HttpUpMorePhoto(tag, url, form, PhotoList[1], PhotoList[2], PhotoList[3], PhotoList[4], PhotoList[5], PhotoList[6], HTTP_TIMEOUT);
    if cbf then HTTPHandler[tag] = cbf end
end

-- function P.http_upphoto(tag, url, param, form, photos, headers, cbf)
--     print(JSON:encode(photos))
--     libnetwork.HttpUpPhoto(tag, url, param, form, photos, headers, HTTP_TIMEOUT);
--     if cbf then HTTPHandler[tag] = cbf end
-- end

-- 开始一个HTTP下载
-- @url         远程文件地址
-- @range       (该参数已废弃)
-- @savePath    下载保存位置
-- @cbf         下载过程回调
function P.http_download(url, range, savePath, cbf)
    libnetwork.HttpDownload(url, range, savePath, HTTP_TIMEOUT)
    if cbf then DownloadCbf[url] = cbf end
end

-- ============================================================================
-- 建立连接
function P.connect(host, port, connected, disconnected)
    if connected == nil then
        libunity.LogW("TCP连接回调为空!")
        _G.UI.Waiting.hide()        
        return
    end

    GameCli:connect(host, port, 10)    
    onConnected = connected
    onDisconnected = disconnected or default_on_disconnect
end

-- 客户端断开连接
function P.disconnect()
    onDisconnected = nil
    GameCli:disconnect()
end

-- ============================================================================
-- 创建一个消息对象
function P.msg(code, size)   
   
    local id = chk_msg_type(code)
     print("<color=#00aa00>networkmgr.msg创建消息对象：</color>"..tostring(code).."id"..id)
    if id == nil then return end

    local NetMsg = import("clientlib.net.NetMsg")    
    return NetMsg.createMsg(id, size or 1024)
end

-- 客户端发送消息
function P.send(nm, only)
    print("<color=#00aa00>networkmgr.send发送消息：</color>")
    if P.connected() then
        local post = NtfNmList[nm.type]
        if not post then MsgQueue:enqueue({nm = nm, only = only == true}) end
        if post or MsgQueue:count() == 1 then
            GameCli:send(nm, post, only == true)
            -- print("<color=#00ff00>消息内容" .. nm)
        else
            -- print("<color=#00ff00>消息内容" .. nm)
            log("Enqueue: {0}", nm)
        end
    end
end

-- 清空消息队列
function P.clear()
    MsgQueue:clear()
end

-- 获取网络错误描述
function P.get_error(code, op)
    if op == nil then op = "Default" end
    local Error = _G.CFG.ErrorLib[code]
    local ret = Error and Error[op] or string.format(TEXT.fmtUnkonwError, code)
    libunity.LogW("ERROR#{0}:{1}", code, ret)
    return ret
end

function P.connected()
    libunity.LogD("Socket Connect is :{0}", GameCli.IsConnected)
    return GameCli.IsConnected
end

-- ============================================================================
-- 注册消息分析器
-- 一个消息只能注册一次
function P.regist(code, handler, reset)
    -- print("<color=#00aa00>networkmgr.regist:注册消息"..tostring(code).."</color>")
    local id = chk_msg_type(code)
    if id == nil then return end

    local cbf = NCHandler[id]
    if cbf == nil then
        NCHandler[id] = handler
    else
        libunity.LogW("networkmgr.regist消息[{0}({1})]已经被注册！请订阅该消息", code, id)
    end 
end

-- 订阅消息
function P.subscribe(code, handler)
    print("<color=#00aa00>networkmgr.subscribe:开始订阅消息"..tostring(code).."</color>")
    local id = chk_msg_type(code)
    if id == nil then
     print("<color=#00aa00>networkmgr.subscribe订阅消息{"..tostring(code).."}不存在".."</color>")
     return
    else
    print("<color=#00aa00>networkmgr.subscribe订阅消息{"..tostring(code).."}存在，ID："..tostring(id).."</color>")
     end

    local Subscriber = SubscriberSet[id]
    if Subscriber == nil then
        SubscriberSet[id] = { handler }
        Subscriber = SubscriberSet[id]
    else
        for _,v in ipairs(Subscriber) do
            if v == handler then
                libunity.LogW("networkmgr.subscribe{0}已订阅{1}", handler, code)
            return end
        end
        table.insert(Subscriber, handler)
    end
end

-- 取消订阅
function P.unsubscribe(code, handler)
    local id = chk_msg_type(code)
    if id == nil then return end

    local Subscriber = SubscriberSet[id]
    if Subscriber then
        for i,v in ipairs(Subscriber) do
            if v == handler then 
                table.remove(Subscriber, i)
            break end
        end
     end
end

-- ============================================================================

return P
