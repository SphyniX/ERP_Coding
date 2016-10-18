--
-- @file 	ui/_tool/monotoast.lua
-- @authors xing weizhen (xingweizhen@rongygame.com)
-- @date	2016-03-16 10:19:34
-- @desc    描述
--

local ToastQueue = _G.DEF.Queue:new()

local function on_fade_end(go)
	 _G.PKG["ui/uimgr"].close(go)
    ToastQueue:dequeue()
    local Toast = ToastQueue:peek()
    if Toast then Toast:start() end
end

--=============================================================================

local OBJDEF = {}
OBJDEF.__index = OBJDEF
setmetatable(OBJDEF, _G.UI.Toast)

function OBJDEF:make(style, args)
	if style == nil then style = "Norm" end    
    return setmetatable({ args = args, style = style }, OBJDEF)
end

function OBJDEF:start()
	local libugui = require "libugui.cs"
	local Ref = self:init()
	self.Ref = Ref	
	libugui.DOFade(Ref.root, "In", on_fade_end, false)
end

function OBJDEF:show()
	ToastQueue:enqueue(self)
	if ToastQueue:count() == 1 then
		self:start()
	end
end

function OBJDEF.clear()
	ToastQueue:clear()
end

_G.UI.MonoToast = OBJDEF
