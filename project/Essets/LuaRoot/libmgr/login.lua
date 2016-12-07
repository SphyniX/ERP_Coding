-- File Name : libmgr/login.lua
local libunity = require "libunity.cs"
local libasset = require "libasset.cs"
local libsystem = require "libsystem.cs"
local libcsharpio = require "libcsharpio.cs"
local NW = MERequire "network/networkmgr"
local DY_DATA = MERequire "datamgr/dydata"
local UI_DATA = MERequire "datamgr/uidata"
local TEXT = _G.ENV.TEXT
local MB = _G.UI.MessageBox

local on_account_logined, on_account_registed, on_account_binded, on_http_fail


local P = { } 
P.IPSet = {
test = "192.168.1.47:8888",
productIP = "api.richer.net.cn:8888",
    -- productIP = "139.196.109.3:8080",
    productDomain = "",
}
P.Channel = {
    loginHost = P.IPSet.productIP,
    -- loginHost = P.IPSet.test,
    downloadHost = "139.196.109.3:8888",
    pid = 1,
    minAccLen = 6,
    maxAccLen = 16,
}

P.HTTPSet = {
    -- 督导验证
    supInterface = function () return "http://"..P.Channel.loginHost.."/api/user/sup" end,
    -- 登录
    loginInterface = function () return "http://"..P.Channel.loginHost.."/api/user/login" end,
    -- 发送验证码
    captchaInterface = function () return "http://"..P.Channel.loginHost.."/api/user/captcha" end,
    -- 验证验证码
    validateInterface = function () return "http://"..P.Channel.loginHost.."/api/user/validate" end,
    -- 验证个人信息
    validatenumInterface = function () return "http://"..P.Channel.loginHost.."/api/user/validatenum" end,
    -- 个人信息保存
    insertInterface = function () return "http://"..P.Channel.loginHost.."/api/user/insert" end,
    -- 修改密码
    updatepwdInterface = function () return "http://"..P.Channel.loginHost.."/api/user/updatepwd" end,
    -- 单张图片
    uploadInterface = function () return "http://"..P.Channel.loginHost.."/api/photo/upload" end,
    -- 大王专用
    --uploadphotoInterface = function () return "http://"..P.Channel.loginHost.."/api/photo/uploadphoto" end,
    -- 上报进度专用
    uploadphotoForReport = function () return "http://"..P.Channel.loginHost.."/api/photo/uploadimage" end,

    --getqrcodeInterface = function () return "http://"..P.Channel.loginHost.."/api/photo/getqrcode" end,
    -- 获取版本号
    getversionInterface = function () return "http://"..P.Channel.loginHost.."/api/user/getversion" end,
    -- 下载图片
    downloadPhotoInterface = function () return "http://"..P.Channel.downloadHost.."/Photo" end,

}

function P.try_test()
    local HttpParams = {
    action = "test",
}

local function on_call_back(resp, isDone, err)
    libunity.LogD("Test: {0}", resp)
end
NW.http_get("Test", "http://localhost:8080/TestServer_ll/api.jsp", HttpParams)
end

function P.try_socket_test()
    NW.connect("127.0.0.1", "8080", function (nm)

        end)
end

-- 1尝试登陆
local on_wnd_logined = nil
local function on_login_back(resp, isDone, err)
    _G.UI.Waiting.hide()
    if not isDone or err then
        print("网络连接失败")
        return
    end
    libunity.LogD("Login_Callback: {0}",resp)
    -- 尝试删除apk文件
    local patchRoot = _G.ENV.app_persistentdata_path .. "/Updates/"
    libcsharpio.CreateDir(patchRoot)
    libcsharpio.DeleteFile(patchRoot.."Richer.apk")
    
    local Ret = JSON:decode(resp)
    local LoginedAcc = P.LoginedAcc

    if Ret.ret == 1 then
        local LoginedAcc = P.LoginedAcc 
        libunity.Destroy(P.loginWnd)

        local libnetwork = require("libnetwork.cs")
        libnetwork.SetParam("account", LoginedAcc.acc)

        -- 记录账号
        -- LoginedAcc.date = _G.PKG["util/date"].date2secs()
        -- UI_DATA.save_account(LoginedAcc)

        if on_wnd_logined then on_wnd_logined(Ret) end
    else
        _G.UI.Toast:make(nil, NW.get_error(Ret.ret)):show()
    end
end
function P.try_login(acc, pass, on_call_back)
    on_wnd_logined = on_call_back

    local CMD5 = import("CMD5")
    -- P.LogicedAcc = { acc = acc, pass = CMD5.MD5String(pass)}
    P.LogicedAcc = { acc = acc, pass = pass}
    local HttpParams = {
    phone = acc,
    password = pass,
}
NW.http_post("LOGIN", P.HTTPSet.loginInterface(), "", HttpParams, "", on_login_back)
_G.UI.Waiting.show()
end

-- 2督导验证
local on_wnd_bind_supervisor = nil
local function on_bind_supervisor_back(resp, isDone, err)
    _G.UI.Waiting.hide()
    if not isDone or err then 
        print("网络连接失败")
        return
    end
    libunity.LogD("on_bind_supervisor_back :{0}",resp)
    local Ret = JSON:decode(resp)
    if Ret.ret == 1 then
        if on_wnd_bind_supervisor then on_wnd_bind_supervisor(Ret) end
    else
        _G.UI.Toast:make(nil, NW.get_error(Ret.ret)):show()  
    end
end

function P.try_bind_supervisor(supname, name, code, on_call_back)
    on_wnd_bind_supervisor = on_call_back
    local HttpParams = {
    supname = supname,
    salename = name,
    code = code,
}
NW.http_post("SUPERVISOR", P.HTTPSet.supInterface(), "", HttpParams, "", on_bind_supervisor_back)
_G.UI.Waiting.show()
end

-- 3获取验证码
local on_wnd_get_verify = nil
local function on_get_verify_back(resp, isDone, err)
    _G.UI.Waiting.hide()
    if not isDone or err then 
        print("网络连接失败")
        return
    end
    libunity.LogD("on_get_verify_back :{0}",resp)
    local Ret = JSON:decode(resp)
    if Ret.ret == 1 then
        if on_wnd_get_verify then on_wnd_get_verify(Ret) end
    else
        _G.UI.Toast:make(nil, NW.get_error(Ret.ret)):show()
    end
end

function P.try_get_verify(phone, type, on_call_back)
    on_wnd_get_verify = on_call_back
    local HttpParams = {
    phone = phone,
    type = type,
}
NW.http_get("CAPTCHA", P.HTTPSet.captchaInterface(), HttpParams, on_get_verify_back)
_G.UI.Waiting.show()
end

-- 4尝试验证手机
local on_wnd_bind_phone = nil
local function on_bind_phone_back(resp, isDone, err)
    _G.UI.Waiting.hide()
    if not isDone or err then 
        print("网络连接失败")
        return
    end
    
    libunity.LogD("on_bind_phone_back :{0}",resp)
    local Ret = JSON:decode(resp)
    if Ret.ret == 1 then 
        if on_wnd_bind_phone then on_wnd_bind_phone(Ret) end
    else
        _G.UI.Toast:make(nil, NW.get_error(Ret.ret)):show()
    end
end
function P.try_bind_phone(phone, idf, on_call_back)
    on_wnd_bind_phone = on_call_back
    local HttpParams = {
    phone = phone,
    idf = idf,
}

NW.http_post("VALIDATE", P.HTTPSet.validateInterface(), "", HttpParams, "", on_bind_phone_back)
_G.UI.Waiting.show()
end

-- 5验证个人信息
local on_wnd_validatenum = nil
local function on_validatenum_back(resp, isDone, err)
    _G.UI.Waiting.hide()
    if not isDone or err then 
        print("网络连接失败")
        return
    end
    
    libunity.LogD("on_validatenum_back :{0}",resp)
    local Ret = JSON:decode(resp)
    if Ret.ret == 1 then
        if on_wnd_validatenum then on_wnd_validatenum(Ret) end
    else
        if Ret.ret == 10000 then
            local errinfo = Ret.error
            local ErrInfo = errinfo:splitn("|")
            local info = "\n"
            for _,v in ipairs(ErrInfo) do
                info = info..NW.get_error(v).."\n"
            end
            _G.UI.Toast:make(nil, info):show() 
            return
        end
        _G.UI.Toast:make(nil, NW.get_error(Ret.ret)):show()
    end
end
function P.try_validatenum(idnumber, cardno, on_call_back)
    on_wnd_validatenum = on_call_back
    local HttpParams = {
    idnumber = idnumber,
    cardno = cardno,
}
NW.http_post("validatenum", P.HTTPSet.validatenumInterface(), "", HttpParams, "", on_validatenum_back)
_G.UI.Waiting.show()
end

-- 6尝试注册，上传个人信息
local on_wnd_regist = nil
local function on_regist_back(resp, isDone, err)
    _G.UI.Waiting.hide()
    if not isDone or err then 
        print("网络连接失败")
        return
    end
    
    libunity.LogD("on_regist_back :{0}",resp)
    local Ret = JSON:decode(resp)
    if Ret.ret == 1 then
        if on_wnd_regist then on_wnd_regist(Ret) end
    else
        if Ret.ret == 10000 then
            local errinfo = Ret.error
            local ErrInfo = errinfo:splitn("|")
            local info = "\n"
            for _,v in ipairs(ErrInfo) do
                info = info..NW.get_error(v).."\n"
            end
            _G.UI.Toast:make(nil, info):show() 
            return
        end
        _G.UI.Toast:make(nil, NW.get_error(Ret.ret)):show()
    end
    
end
function P.try_regist(lisInfo, on_call_back)
    on_wnd_regist = on_call_back
    local HttpParams = {
    user_phone = lisInfo.phone,
    user_password = lisInfo.password,
    user_name = lisInfo.name,
    user_age = lisInfo.age,
    user_sex = lisInfo.sex,
    user_height = lisInfo.height,
    user_weight = lisInfo.weight,
    user_qq = lisInfo.qq,
    user_email = lisInfo.email,
    user_wechat = lisInfo.wechat,
    user_idnumber = lisInfo.idnumber,
    user_cardNo = lisInfo.cardNo,
    user_city = lisInfo.city,
    supname = lisInfo.supname,
    photo1 = lisInfo.PhotoList[1],
    photo2 = lisInfo.PhotoList[2],
    photo3 = lisInfo.PhotoList[3],
    photo4 = lisInfo.PhotoList[4],
    photo5 = lisInfo.PhotoList[5],
    photo6 = lisInfo.PhotoList[6],
}
local PhotoList = {}
NW.http_upmorephoto("INSERT", P.HTTPSet.insertInterface(), "", HttpParams, PhotoList,"", on_regist_back)
_G.UI.Waiting.show()
end

-- 尝试设置密码
local on_wnd_update_password = nil
local function on_uodate_password_back(resp, isDone, err)
    _G.UI.Waiting.hide()
    if not isDone or err then 
        print("网络连接失败")
        return
    end
    libunity.LogD("on_uodate_password_back :{0}",resp)
    local Ret = JSON:decode(resp)
    if Ret.ret == 1 then
        if on_wnd_update_password then on_wnd_update_password(Ret) end
    else
        _G.UI.Toast:make(nil, NW.get_error(Ret.ret)):show()
    end
end
function P.try_update_password(phone, pass, on_call_back)
    on_wnd_update_password = on_call_back
    local HttpParams = {
    phone = phone,
    password = pass,
}
NW.http_post("UpdatePass", P.HTTPSet.updatepwdInterface(), "", HttpParams, "", on_uodate_password_back) 
_G.UI.Waiting.show()
end

local on_wnd_uploadphoto
local function on_uploadphoto_back(resp, isDone, err)
    _G.UI.Waiting.hide()
    if not isDone or err then
        print("网络连接失败")
        _G.UI.Toast:make(nil, "网络连接失败"):show()
        return
    end
    libunity.LogD("on_uploadphoto_back :{0}",resp)
    local Ret = JSON:decode(resp)
    -- if Ret.ret == 1 then
    --     if on_wnd_uploadphoto then on_wnd_uploadphoto(Ret) end
    -- else
    --     _G.UI.Toast:make(nil, NW.get_error(Ret.ret)):show()
    -- end
    
    if on_wnd_uploadphoto then on_wnd_uploadphoto(Ret) end
    if Ret.ret ~= 1 then _G.UI.Toast:make(nil, NW.get_error(Ret.ret)):show() end
    
end

function P.try_uploadphoto(userid, typeid, storeid, Image, on_call_back)
    on_wnd_uploadphoto = on_call_back
    local HttpParams = {
    UserID = userid,
    Typeid = typeid,
    Stid = storeid,
}
-- function P.http_upphoto(tag, url, param, form, Image, headers, cbf)
NW.http_upphoto("uploadphoto", P.HTTPSet.uploadInterface(), "", HttpParams, Image, "", on_uploadphoto_back)
_G.UI.Waiting.show()
end

function P.try_uploadphotoforuserinfo(typeid, Image, on_call_back)
    on_wnd_uploadphoto = on_call_back
    local HttpParams = {
    UserID = 0,
    Typeid = typeid,
}
-- function P.http_upphoto(tag, url, param, form, Image, headers, cbf)
NW.http_upphoto("uploadphoto", P.HTTPSet.uploadInterface(), "", HttpParams, Image, "", on_uploadphoto_back)
_G.UI.Waiting.show()
end


function P.try_uploadphotoforreport(userid, Image, on_call_back)
    on_wnd_uploadphoto = on_call_back
    local HttpParams = {
    UserID = userid,
}
NW.http_upphoto("uploadphoto", P.HTTPSet.uploadphotoForReport(), "", HttpParams, Image, "", on_uploadphoto_back)
_G.UI.Waiting.show()
end
-- local on_wnd_getqrcode
-- local function on_getqrcode_back(tex, isDone, err)
--     if not isDone or err then
--         print("网络连接失败")
--         return
--     end
-- end
-- function P.try_getqrcode(photoid, on_call_back)
--     on_wnd_getqrcode = on_call_back
--     local HttpParams = {
--         photoid = photoid,
--     }
--     NW.http_post("LDPHOTO", P.HTTPSet.getqrcodeInterface(), "", HttpParams, "", on_getqrcode_back)
-- end
function P.on_filelist_get(resp, isDone, err)
    if isDone and err == nil then
        UI_DATA.WNDPatch.FileList = JSON:decode(resp)
        local UIMGR = MERequire "ui/uimgr"
        UIMGR.create_window("UI/WNDPatch")
    end
end

local on_wnd_version
local function on_get_version_back(resp, isDone, err)
    _G.UI.Waiting.hide()
    if not isDone or err then 
        print("网络连接失败")
        return
    end
    local function chk_version(VerA, VerB)
        local n = #VerA 
        for i=1,n do
            local va, vb = tonumber(VerA[i]), tonumber(VerB[i])
            if va ~= vb then
                return va - vb, i
            end 
        end
        return 0, nil
    end
    libunity.LogD("on_get_version_back :{0}",resp)
    local Ret = JSON:decode(resp)
    local maxVer = Ret.maxversion
    local apkAddress = Ret.apkaddress
    local bagAddress = Ret.bagaddress .. "/AssetBundles/"
    local locVer = UI_DATA.WNDLogin.LocalFileList.version
    
    local LocVer = locVer:split('.')
    local MaxVer = maxVer:split('.')
    print(maxVer, locVer)
    local isOld, lev = chk_version(MaxVer, LocVer)
    print(isOld, lev)
    isOld = isOld > 0
    if isOld then
        libunity.LogD("有新版本，需要更新")
        if lev == 4 then
            libunity.LogD("资源更新")
            UI_DATA.WNDPatch.maxVer = maxVer
            UI_DATA.WNDPatch.resUrl = bagAddress
            NW.http_get("PATCH", bagAddress.."filelist.bytes", "", P.on_filelist_get)
        else
            libunity.LogD("整包更新")
            UI_DATA.WNDPatch.appUrl = apkAddress
            local UIMGR = MERequire "ui/uimgr"
            UIMGR.create_window("UI/WNDPatch")
        end
    else
        local LoginedAcc = P.LoginedAcc
        -- 尝试删除apk文件
        local patchRoot = _G.ENV.app_persistentdata_path .. "/Updates/"
        libcsharpio.CreateDir(patchRoot)
        libcsharpio.DeleteFile(patchRoot.."Richer.apk")
    end
    if on_wnd_version then on_wnd_version(isOld) end
end
function P.try_getversion(address, port, on_call_back)
    on_wnd_version = on_call_back
    NW.http_get("GETVERSION", P.HTTPSet.getversionInterface(), "", on_get_version_back)
    _G.UI.Waiting.show()
end

function P.do_logout()
    DY_DATA.clear()
    UI_DATA.clear()
    _G.PKG["libmgr/dytimer"].clear()
    _G.PKG["global/scenemgr"].load_login_level()

end



-- ==========================

function P.on_chk_network(cbf)
    local Application = import("UnityEngine.Application")
    local network = tostring(Application.internetReachability)
    if network == "ReachableViaLocalAreaNetwork" then
        if cbf then cbf() end
    else
        -- 非wifi网络
        MB:make("", TEXT.tipAskUpdateViaCarrierDataNetwork, true):set_event(cbf):show()
    end
end

-- ============================================================================

-- 登录完成
function P.on_login_success()
    local LoginedAcc = P.LoginedAcc 
    libunity.Destroy(P.loginWnd)

    local libnetwork = require("libnetwork.cs")
    libnetwork.SetParam("account", LoginedAcc.acc)

    if on_wnd_logined then on_wnd_logined() end
end

local SysInfo = nil
local function on_enter_login()
    -- 网络连接成功，启动网络定时器
    local DY_TIMER = MERequire "libmgr/dytimer.lua"
    DY_TIMER.launch_network_timer()

    local UIMGR = MERequire "ui/uimgr.lua"
    UIMGR.create_window("UI/WNDMain")
end

function P.enter_server(ip, port)
    if _G.Debug then
        local UIMGR = MERequire "ui/uimgr.lua"
        UIMGR.create_window("UI/WNDMain")
    else
        -- print("connect")
        NW.connect(ip, port, on_enter_login)
        _G.UI.Waiting.show(TEXT.tipConnecting)
    end
end

return P