-- File Name : framework/clock.lua
local table = table
local DY_TIMER = MERequire "libmgr/dytimer.lua"

local function on_clock(n)
    local Timers = DY_TIMER.Timers
    for i,v in ipairs(Timers) do
        v:update(n)
    end
    -- 安全删除
    for i=#Timers, 1, -1 do
        if Timers[i].paused then table.remove(Timers, i) end
    end
end

return {
	on_clock = on_clock,
}