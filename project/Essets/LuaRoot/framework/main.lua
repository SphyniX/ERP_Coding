-- File Name : framework/main.lua

local libunity = require "libunity.cs"
local libasset = require "libasset.cs"

-- module -- 
dofile "global/variable.lua"
-- 场景管理
MERequire "global/scenemgr"

local KeyNotify

local function awake()
    print ("<color=yellow>lua awake</color>")
    libasset.PrepareAssets({
            { name = "UI/", unload = false, },
            -- { name = "Atlas/ItemIcon/", unload = false, },
            -- { name = "Atlas/RoleIcon/", unload = false, },
            -- { name = "Atlas/WeaponIcon/", unload = false, },
            -- { name = "Atlas/SkillIcon/", unload = false, },
            -- { name = "Atlas/MineMap/", unload = false, },
            -- { name = "Battle/", unload = false, },
        })
end

local function start()
    local ENV = _G.ENV
    print (string.format("===[INFO]===\nplatform: %s\ndata: %s\npersistent: %s\nstreaming: %s", 
        ENV.unity_platform, ENV.app_data_path, ENV.app_persistentdata_path, ENV.app_streamingassets_Path))
    
    -- libunity.FindComponent(nil, "/UI Root/Guide", "LuaComponent").enabled = true
    libunity.NewChild("/UIROOT", "Launch/AppController")
    libunity.NewChild("/UIROOT", "Launch/NetworkMgr")
    libunity.NewChild("/UIROOT", "Launch/AudioMgr")
    libunity.NewChild("/UIROOT", "Launch/LuaClock")
    libunity.NewChild("/UIROOT", "Launch/AudioListener")
    libunity.NewChild("/UIROOT", "Launch/SDKMgr")
    libunity.NewChild("/UIROOT", "Launch/GPSMgr")

    local platform = ENV.unity_platform
    local standalone = platform == "OSXEditor" 
                   or platform == "OSXPlayer" 
                   or platform == "WindowsEditor" 
                   or platform == "WindowsPlayer"
    ENV.debug = standalone
    if standalone then
        local CONSOLE = MERequire "console/console"
        KeyNotify = {
            ["F1"] = CONSOLE.open_console,
            ["Escape"] = CONSOLE.close_console,
        }
    else
        --libngui.LimitResolution()
    end
end

local function on_key(key)
    local platform = _G.ENV.unity_platform
    if platform == "OSXEditor" 
    or platform == "OSXPlayer"
    or platform == "WindowsEditor"
    or platform == "WindowsPlayer"
    then
        local ntf = KeyNotify[key]
        if ntf then
            ntf()
        end
    end
end

local function on_ui_click(go)
    -- local name = go.name
    -- local collider = go.collider
    -- local pre = name:sub(1, 3)
    
end

-- 在最后，禁止定义和访问未定义的全局变量
setmetatable(_G, _G.MT.Const)    

return {
    awake = awake,
    start = start,
    on_key = on_key,
    on_ui_click = on_ui_click,
}
