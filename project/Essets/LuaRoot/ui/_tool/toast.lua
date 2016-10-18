-- File Name : ui/_tool/toast.lua

-- 浮动提示框

-- local Toast = _G.UI.Toast
-- Toast:make(nil, "提示框框框"):show()

local libunity = require "libunity.cs"
local libugui = require "libugui.cs"

local ToastQueue = _G.DEF.Queue:new()

local function on_fade_end(go)
    _G.PKG["ui/uimgr"].close(go)
    ToastQueue:dequeue()
end

local function coro_sorting(self)
    coroutine.yield()

    local Vector3 = import("UnityEngine.Vector3")
    local height = libugui.GetSiz(self.Ref.root).y + 20
    for i=#ToastQueue,1,-1 do
        local v = ToastQueue[i]
        local root = v.Ref.root
        local pos = libugui.GetPos(root)
        local siz = libugui.GetSiz(root)
        local tar = Vector3(pos.x, height, pos.z)
        height = height + siz.y + 20
        libugui.KillTween(root)
        libugui.DOTween("TweenPosition", root, nil, tar, 0.2)       
    end

    ToastQueue:enqueue(self)
end

--=============================================================================

local OBJDEF = { }
OBJDEF.__index = OBJDEF

function OBJDEF:make(style, args)
    if style == nil then style = "Norm" end
    return setmetatable({ args = args, style = style }, OBJDEF)
end

function OBJDEF:init()
    -- local go = libugui.CreateWindow("UI/TIP"..self.style.."Toast", 31)
    local go = libugui.CreateWindow("UI/TIPNormal", 31)
    local Ref = libugui.GenLuaTable(go, "root")
    local lbTips = Ref.lbTips
    if lbTips then lbTips.text = self.args end
    return Ref
end

function OBJDEF:show()
    local Ref = self:init()
    libugui.DOFade(Ref.root, "In", on_fade_end, false)
    self.Ref = Ref

    libunity.StartCoroutine(nil, coro_sorting, self)
    
    return self
end

_G.UI.Toast = OBJDEF
