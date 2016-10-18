-- File Name : datamgr/object/timer.lua
-- 简单的定时器
local tostring, table, string, math
    = tostring, table, string, math
local libunity = require "libunity.cs"

local OBJDEF = {}
OBJDEF.__index = OBJDEF
OBJDEF.__tostring = function (self)
    if self then
        return string.format("[定时器:%s,周期=%d,倒数=%d,暂停=%s]", self.tag, self.cycle, self.count, tostring(self.paused))
    else
        return "[TimerDEF]"
    end
end

function OBJDEF.new(tag, count, cycle, on_cycle)
    if count < 0 then count = 0 end

    local self = {
        tag = tag,
        param = nil,
        count = count,
        cycle = cycle,
        onCycle = on_cycle,
        paused = false,
        CycleSet = {},
        CountingSet = {},
    }   
    return setmetatable(self, OBJDEF)
end

function OBJDEF:init(count, cycle, cbf)
    if count < 0 then count = 0 end 
    if count then self.count = count end
    if cycle then self.cycle = cycle end
    if cbf then self.onCycle = cbf end
    self.paused = count == 0
end

function OBJDEF:subscribe_cycle(on_cycle)
    table.insert_once(self.CycleSet, on_cycle)
end

function OBJDEF:unsubscribe_cycle(on_cycle)
    table.remove_elm(self.CycleSet, on_cycle)
end

function OBJDEF:subscribe_counting(on_counting)
    table.insert_once(self.CountingSet, on_counting)
end

function OBJDEF:unsubscribe_counting(on_counting)
    table.remove_elm(self.CountingSet, on_counting)
end

function OBJDEF:update(n)
    if n == nil then n = 1 end
    if not self.paused then
        local count = self.count
        while n > 0 do
            if n > count then
                n = n - count
                count = 0
            else
                count = count - n
                n = 0
            end
            if count == 0 then
                -- 周期处理
                if self.onCycle then 
                    self.paused = self:onCycle() 
                else
                    self.paused = true
                end
                -- 周期发布
                for _,on_cycle in ipairs(self.CycleSet) do on_cycle(self) end
                count = self.cycle              
            end
            if self.paused then break end
        end
        self.count = count
        -- 计数发布
        for _,on_counting in ipairs(self.CountingSet) do on_counting(self) end
    end
end

function OBJDEF:count_to_string()
    local seconds = self.count
    local min, sec = math.floor(seconds / 60), seconds % 60
    min = min < 10 and "0"..min or min
    sec = sec < 10 and "0"..sec or sec
    return min..":"..sec --string.format("%02d:%02d", min, sec)
end

return OBJDEF
