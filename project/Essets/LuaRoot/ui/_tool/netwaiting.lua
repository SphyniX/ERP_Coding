-- File Name : ui/_tool/netwaiting.lua
local libunity = require "libunity.cs"
local libugui = require "libugui.cs"
local P = {}
local Ref 
local function chk_args(Object, method)
    if Object == P then
        error(string.format("Waiting.%s must NOT be called in method format", method), 3)
    end
end

local function on_time_out(o)
    _G.PKG["network/networkmgr"].clear()

    _G.PKG["ui/uimgr"].close(Ref.root, true)
    local TEXT = _G.ENV.TEXT
    _G.UI.Toast:make(nil, TEXT.tipConnectTimeout):show()
end

local function internal_show(text, timeout)
    local delay = 0
    if text then
        Ref.lbText.text = text
        Ref.cvMask.alpha = 1
    else
        delay = 1
        Ref.lbText.text = ""
        -- libugui.FreeTween(nil, Ref.tweenAlpha, 0, 1, {
        --         duration = 1, ease = "InExpo",
        --     })
    end
    if timeout == false then return end
    if timeout == nil then timeout = 5 end
    local Vector3 = import("UnityEngine.Vector3")
    libugui.FreeTween(nil, Ref.tweenRotation, Vector3.zero, Vector3(0, 0, -180), {
            duration = 2,
            delay = delay,
            loops = math.floor(timeout / 2),
            loopType = "Incremental",
            complete = on_time_out,
        })
end

function P.show(text, timeout)
    chk_args(text, "show")

    if Ref == nil then
        -- 新建
        local root = _G.PKG["ui/uimgr"].create("UI/NetWaiting", 99)
        Ref = libugui.GenLuaTable(root, "root")
        Ref.cvMask = libunity.FindComponent(Ref.root, "spMask=", "CanvasGroup")
        Ref.tweenAlpha = libunity.FindComponent(Ref.root, "spMask=", "TweenAlpha")
        Ref.tweenRotation = libunity.FindComponent(Ref.root, "spMask=/spLoading_", "TweenRotation")
        internal_show(text, timeout)
    else
        -- 重用
        if not libunity.IsActive(Ref.root) then
            _G.PKG["ui/uimgr"].create("UI/NetWaiting")
            internal_show(text, timeout)
        else
            -- 更新
            Ref.lbText.text = text
        end
    end
end

function P.hide(tcp)
    if not tcp or Ref.lbText.text == "" then
        if Ref then _G.PKG["ui/uimgr"].close(Ref.root, true) end
    end
end


_G.UI.Waiting = P
