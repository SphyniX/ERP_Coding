-- File Name : datamgr/object/tween.lua

local OBJDEF = { }
OBJDEF.__index = OBJDEF
OBJDEF.__tostring = function (self)
    return string.format("[Tween: %s@%s]", tostring(self.method), tostring(self.object))
end

function OBJDEF.new(object, method, from, to, duration)
    local self = { 
        object = object,
        method = method,
        from = from,
        to = to,
        Args = {
            duration = duration,
        }
    }
    return setmetatable(self, OBJDEF)
end

function OBJDEF:set_delay(delay)
    self.Args.delay = delay
    return self
end

function OBJDEF:set_loop(loops, loopType)
    self.Args.loops = loops
    self.Args.loopType = loopType
    return self
end

function OBJDEF:set_ease(ease)
    self.Args.ease = ease
    return self
end

function OBJDEF:set_update(updateType, ignoreTimescale)
    self.Args.updateType = updateType
    self.Args.ignoreTimescale = update
    return self
end

function OBJDEF:on_update(func)
    self.Args.update = func
    return self
end

function OBJDEF:on_complete(func)
    self.Args.complete = func
    return self
end

function OBJDEF:change(from, to, duration)
    if from then self.from = from end
    if to then self.to = to end
    if duration then self.Args.duration = duration end
    return self
end

function OBJDEF:forward()
    local libugui = require "libugui.cs"
    libugui.FreeTween(self.method, self.object, self.from, self.to, self.Args)
end

function OBJDEF:backward()
    local libugui = require "libugui.cs"
    libugui.FreeTween(self.method, self.object, self.to, self.from, self.Args)
end

function OBJDEF:kill()
    local libugui = require "libugui.cs"
    libugui.KillTween(self.object)
end

return OBJDEF
