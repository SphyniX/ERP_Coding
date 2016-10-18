--
-- @file    util/color.lua
-- @authors xing weizhen (xingweizhen@rongygame.com)
-- @date    2015-11-30 15:32:50
-- @desc    颜色定义
--

local P

do
    local UE_Color = import("UnityEngine.Color")

    -- 道具品质颜色定义定义
    local Item = {
        UE_Color.new(208 / 255, 203 / 255, 207 / 255, 1),
        UE_Color.new(50 / 255, 208 / 255, 0 / 255, 1),
        UE_Color.new(64 / 255, 220 / 255, 255 / 255, 1), 
        UE_Color.new(255 / 255, 65 / 255, 242 / 255, 1), 
        UE_Color.new(255 / 255, 154 / 255, 62 / 255, 1),
    }

    P = setmetatable({
                Item = Item,
                Role = Item,
                Weapon = Item,
            },
        _G.MT.Const)
end

return P