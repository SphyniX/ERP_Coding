--
-- @file    console/battletool.lua
-- @authors xing weizhen (xingweizhen@rongygame.com)
-- @date    2015-12-24 12:06:48
-- @desc    描述
--

local P = {}

function P.win()    
    local NW = _G.PKG["network/networkmgr"]
    if NW.connected() then
        local UI_DATA = _G.PKG["datamgr/uidata"]
        local Summary = {
                nAliveGirls = #UI_DATA.FRMBattle.Roles,
                nAliveZombie = 0,
                nFrameCount = 3599,
            }
        UI_DATA.FRMBattle.Summary = Summary
        UI_DATA.FRMBattle.Handler.on_over_battle(Summary)
    else

    end
end

return P
