-- File Name: datamgr/uidata.lua
local P = {
QTE = {},
WNDLaunch = {},
WNDLogin = {},
WNDRegist = {
UserInfo = {},
},
WNDPatch = {},
WNDMain = {},
WNDDialog = {},
WNDUserSupervisor = {},
WNDPhoto = {},
WNDShowPhoto = {},
WNDSetTime = {},
WNDSetSex = {},
WNDSelectProject = {},
WNDSelectStore = {},
WNDSelectProvince = {},
WNDSelectCity = {},
WNDSubmitSchedule = {},
WNDWorkProject = {},
WNDUserSupervisor = {},
WNDMsgContext = {},
WNDChangePassword = {},
WNDSupTask = {},
WNDSupTaskList = {},
WNDSupChangeTask = {},
WNDSupNewTask = {},
WNDSelectStore = {},
WNDSupWorkProject = {},
WNDSetCompeteProduct = {},
WNDMsgLower = {},
WNDBindPhone = {},
WNDVerifyPhone = {},
WNDSelectPerson = {},
WNDSupChangeTask = {},
WNDSetComPhoto = {},
WNDSupEditorMsg = {},
WNDSelectPlace = {},
WNDSUPSENDEESELECT={},
WNDsupmsgData={},
WNDSupWork={},
WNDAttLeave = {},
WNDAttUnder = {},
WNDSupDataProgerssComData = {},
WNDSupDataGoodAnalysis = {},
WNDSupSelectStore = {},
WNDSelectBrand = {},
WNDSupStoreData = {},
WNDsupWorkSelectShop = {},
WNDSupWorkSelectShopTask={},
WNDSupWorkSelectShopTaskSet={},
WNDSupWorkSelectShopTaskSetSelPeople = {},
WNDSupAskOffMag = {},
WNDsupShopSelect = {},
WNDsupShopSelectPunch = {},
WNDAskOffMag = {},
WNDMsgHint = {},

}

local LocalDB = {}

-- 保存的帐号信息
-- function P.save_account(Acc)
--     local Accounts = P.load_account()
--     local Account = table.match(Accounts.List, { acc = Acc.acc })    
--     if Account == nil then 
--         table.insert(Accounts.List, Acc)
--     else
--         Account.pass, Account.type, Account.date
--         = Acc.pass, Acc.type, Acc.date
--         if Acc.server then Account.server = Acc.server end
--     end
--     Accounts.last = Acc.acc
--     table.sort(Accounts.List, function (a, b)
--             if a.date == nil then a.date = 0 end
--             if b.date == nil then b.date = 0 end
--             return a.date > b.date
--         end)
--     import("UnityEngine.PlayerPrefs").SetString("account", JSON:encode(Accounts))
-- end

-- 获取保存的帐号信息
-- function P.load_account(acc)
--     local Accounts = LocalDB.Accounts
--     if Accounts == nil then 
--         local joAcc = import("UnityEngine.PlayerPrefs").GetString("account")        
--         Accounts = joAcc and JSON:decode(joAcc) or { List = {} }
--         LocalDB.Accounts = Accounts
--     end

--     return acc and table.match(Accounts.List, { acc = acc }) or Accounts 
-- end

-- 保存的帐号信息
function P.save_account(Acc)
    import("UnityEngine.PlayerPrefs").SetString("account", JSON:encode(Acc))
end
-- 获取保存的帐号信息
function P.load_account()
    local joAcc = import("UnityEngine.PlayerPrefs").GetString("account")
    local Acc = joAcc and JSON:decode(joAcc) or nil
    return Acc
end


function P.form_lineup(Lineup, AllRoles, AllWeapons)
    if type(Lineup) == "string" then
        local DY_DATA = _G.PKG["datamgr/dydata"]
        Lineup = P.load_lineup(DY_DATA.Player.id, Lineup)
    end

    if Lineup == nil or #Lineup > 0 then
        return {}, nil
    end

    local Roles = {}
    for _,v in ipairs(Lineup.Fmt) do
        local Role = AllRoles[v[1]]
        Role.Weapon = AllWeapons[v[2]]
        table.insert(Roles, Role)
    end
    return Roles, Lineup.awake
end

-- 登出时清空缓存的UI数据
function P.clear()
    for _,T in pairs(P) do
        if type(T) == "table" then
            for k,v in pairs(T) do T[k] = nil end
        end
    end
end

setmetatable(P, _G.MT.Const)
return P
