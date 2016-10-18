-- File Name : console/uitool.lua
local libunity = require "libunity.cs"
local libugui  = require "libugui.cs"
local libasset = require "libasset.cs"

local function open_new_window(prefab)
    local GameObject = import("UnityEngine.GameObject")
    local chk = libasset.Load(GameObject.GetType(), "UI/"..prefab)
    if chk then
        _G.PKG["ui/uimgr"].create_window("UI/"..prefab)
    else
        libunity.LogW("未找到窗口<{0}>", prefab)
    end
end

local function show_window(prefab)
    local GameObject = import("UnityEngine.GameObject")
    local chk = libasset.Load(GameObject.GetType(), "UI/"..prefab)
    if chk then
        _G.PKG["ui/uimgr"].create("UI/"..prefab)
    else
        libunity.LogW("未找到窗口<{0}>", prefab)
    end
end

local function close_window(name)
    if name then
    else
        _G.PKG["ui/uimgr"].close_window()
    end
end

local function load_level(name)
    local SCENE_MGR = _G.PKG["global/scenemgr"]
    if name == "Home" then
        -- SCENE_MGR.load_home_level()
    elseif name == "Login" then
        SCENE_MGR.load_login_level()
    else
        -- SCENE_MGR.load_stage_level(name)
    end
end

local function show_cfgname(flag)
    _G.ENV.debug = flag == nil
end

local function make_toast(style, text)    
    _G.UI.Toast:make(style, text):show()
end

return {
    open = open_new_window,
    show = show_window,
    close = close_window,
    level = load_level,
    showid = show_cfgname,
    toast = make_toast,
}