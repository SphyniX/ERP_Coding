-- File Name : global/scenemgr.lua
local libugui = require "libugui.cs"
local libunity = require "libunity.cs"
local libasset = require "libasset.cs"

local goLogViewer

-- 对外导出的包接口

local function load_login_level()
    libasset.PrepareAssets( {
        { name = "Scenes/login/", unload = true},
    })
    libunity.LoadLevel("Scenes/Login/Login")
end

local function load_home_level()
    libunity.Delete("BattleMgr")
     libasset.PrepareAssets( {
        { name = "Scenes/home/", unload = true},
    })
    libunity.LoadLevel("Scenes/Home/Home")
end

local function load_stage_level(levelName)
    local Seg = levelName:split("-")
    if #Seg == 3 then
        local levelGrp = string.format("Scenes/%s-%s/", Seg[1], Seg[2])
        local PrepareAssets = {
            { name = levelGrp, unload = true, },
            { name = "FX/Common/", unload = true, },
            { name = "FX/_Voice/", unload = true, },
            { name = "ITOR/", unload = true, },
        }

        -- 角色和武器资源预加载
        local UI_DATA = _G.PKG["datamgr/uidata"]
        local Roles = UI_DATA.FRMBattle.Roles
        local PreLoads = {}
        for _,v in ipairs(Roles) do
            local PreLoad = v:get_preload_data()
            for _,asset in ipairs(PreLoad) do
                PreLoads[asset] = true                
            end
        end
        
        for k,_ in pairs(PreLoads) do
            table.insert(PrepareAssets, {name = k, unload = true})
        end

        libasset.PrepareAssets(PrepareAssets)
        libunity.LoadLevel(levelGrp..levelName, "UIFX/Stage/StageLoading")
    else
        libunity.LogE("错误的场景名称: {0}", levelName)
    end
end

local function on_login_level()
    local UIMGR = MERequire "ui/uimgr"
    -- UIMGR.create_window("UI/WNDLaunch")
    UIMGR.create_window("UI/WNDLogin")
end

local function on_home_level()
    libunity.SetEnable("/UIROOT/RoleCamera", "Camera", true)
    -- import("ZFrame.AudioManager").Inst:Replay("BGM/main/main", "Bgm")

    local UIMGR = MERequire "ui/uimgr"
    local TopWnd = UIMGR.WNDStack:peek()
    if TopWnd == nil then
        UIMGR.create_window("UI/FRMHome", 1)
    else
        TopWnd:open()
    end
end

local function on_stage_level()
    libunity.SetEnable("/UIROOT/RoleCamera", "Camera", false)

    local UIMGR = MERequire "ui/uimgr"
    UIMGR.create("UI/FRMBattle", 1)
end

local function prepare_stage_assets(on_prepared)
    libasset.LoadAsync(nil, "FX/Common/", true)
    libasset.LoadAsync(nil, "FX/_Voice/", true)
    libasset.LoadAsync(nil, "ITOR/", true)
    libasset.LoadAsync(nil, "", true, on_prepared)
end

local function load_logviewer()
    if not libunity.IsObject(goLogViewer) then
        print("<color=yellow>LogViewer is loading</color>")
        local UIMGR = MERequire "ui/uimgr.lua"
        goLogViewer = libunity.NewChild("/UIROOT/UICanvas","UI/LogViewer")
        if goLogViewer then
            local rectTrans = libunity.FindComponent(goLogViewer,nil,"RectTransform")
            if rectTrans then
                local Vector2 = import("UnityEngine.Vector2")
                rectTrans.offsetMax = Vector2.zero
                rectTrans.offsetMin = Vector2.zero
            end
        end
    end
end

-- 场景载入回调
local LevelCBF = {
    ZERO = load_login_level,
    Login = on_login_level,
    Home = on_home_level,
    Formula = function ()
        libunity.Delete("/UIROOT")
        libunity.SendMessage("/UIROOT_/UICanvas/FormulaTest", "OnReady")
    end,
    Train = function ()
        prepare_stage_assets(function ()
            _G.PKG["ui/uimgr"].create("UI/FRMTrain", 1)    
        end)        
    end,
}

_G.on_level_loaded = function (scene, launching)
    _G.ENV.current_level_name = scene
    print(string.format("Level Loaded => %s [%s]", scene, tostring(launching)))

    _G.UI.MonoToast.clear()

    if launching then
        libugui.SetLocalize(_G.ENV.lang)
        load_logviewer()
        local AppVer, AssetVer = libasset.GetVersion()        
        local txtVer = string.format("App: %s\n%s\n%s\n\nAsset: %s\n%s\n%s",
            AppVer.version, AppVer.timeCreated, AppVer.whoCreated,
            AssetVer.version, AssetVer.timeCreated, AssetVer.whoCreated)
        libugui.SetText(goLogViewer, txtVer, "Root/lbVersion")
    end
    
    if scene:sub(1, 5) == "Stage" then 
        if launching then
            prepare_stage_assets(on_stage_level)
        else
            on_stage_level()
        end
    else
        local cbf = LevelCBF[scene]
        if cbf then cbf() end
    end 
end

return {
    load_login_level = load_login_level,
    load_home_level = load_home_level,
    load_stage_level = load_stage_level,
}