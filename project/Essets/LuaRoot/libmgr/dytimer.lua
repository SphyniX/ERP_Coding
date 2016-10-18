-- File Name : libmgr/dytimer.lua

local P = {
    Timers = {},
}

local function launch_timer(tag, count, cycle, cbf)
    local TimerDEF = _G.DEF.Timer
    local tm = nil
    for _,v in ipairs(P.Timers) do
        if v.tag == tag then tm = v; break end
    end
    if tm == nil then 
        tm = TimerDEF.new(tag, count, cycle, cbf) 
        table.insert(P.Timers, tm)
    else
        tm:init(count, cycle, cbf)
    end
    return tm
end

function P.get_timer(key)
    for _,v in ipairs(P.Timers) do
        if v.tag == key then return v end
    end
    return
end

function P.clear()
    table.clear(P.Timers)
end

function P.launch_network_timer()
    local NW = MERequire "network/networkmgr.lua"
    launch_timer("Network", 0, 6, NW.check_state)
end

-- 
-- 资产自动恢复定时器
--
local function on_asset_inc(tm)
    local DY_DATA = _G.PKG["datamgr/dydata"]
    local Asset = DY_DATA.Assets[tm.param]
    if Asset then
        local amount = Asset.amount
        if amount < Asset.limit then
            Asset.amount = amount + 1
        else
            -- 终止定时器
            return true
        end
    else

    end
end

function P.launch_asset_timer(Asset, count, cycle)
    local tm = launch_timer("Asset#"..Asset.id, count, cycle, on_asset_inc)
    tm.param = Asset.id
    print(tm)
end
-- ============================================================================

--
-- 武器训练冷却定时器
--
local function on_train_cooling(tm)
    local DY_DATA = _G.PKG["datamgr/dydata"]
    local Role = DY_DATA.Roles[tm.param]
    Role.trainCooling = 0
    return true
end

function P.launch_train_timer(Role, count, cycle)
    local tm = launch_timer("Train#"..Role.id, count, cycle, on_train_cooling)
    tm.param = Role.id
    print(tm)
end
-- ============================================================================

local function on_frame_inc(tm)
    return true
end

function P.launch_frame_timer(frameId,count,cycle)
    local tm = launch_timer("Frame#"..frameId,count,cycle,on_frame_inc)
    print(tm)
    return tm
end

local function on_techTree_inc(tm)
    return true
end

function P.launch_techTree_timer(count)
    local tm = launch_timer("TechTree",count,count,on_techTree_inc)
    return tm
end

--
-- PVP房间准备倒计时
--
function P.launch_pvproom_timer(Room)
    local countdown = Room.countdown
    if countdown > 0 then
        local tm = launch_timer("PVPRoom#"..Room.id, countdown, countdown, nil)
        print(tm)
        tm.param = Room
    end
end

--
-- PING定时器
--

local function on_ping_start(tm)
    local Ping = _G.PKG["datamgr/uidata"].FRMBattle.Ping
    local cli = tm.param
    if Ping and cli.IsConnected then
        -- local UnityEngine_Time = import("UnityEngine.Time")
        -- Ping.time = UnityEngine_Time.realtimeSinceStartup
        -- local NW = _G.PKG["network/networkmgr"]
        -- cli:send(NW.msg("PVP_BATTLE.CS.PING"):writeU32(Ping.clock))
    else
        return true
    end
end
function P.launch_ping_timer(cli)
    local tm = launch_timer("Client#"..cli.name, 0, 3, on_ping_start)
    tm.param = cli    
    _G.PKG["datamgr/uidata"].FRMBattle.Ping = {
        clock = -1, value = "--",
    }
    return tm
end
-- ============================================================================

function P.launch_exploitmine_timer(Mine)
    local workTimeMax =  tonumber(_G.CFG.ConstLib.MINE.workTimeMax) * 60
    local countdown = workTimeMax - Mine.exploitTime
    local tm = launch_timer("ExploitMine"..Mine.id, countdown, countdown, nil)
    local Base = Mine:get_base_data()
    tm.param = Base.areaID
end

function P.launch_robberymine_timer(countdown)
    local tm = launch_timer("RobberyMine", countdown, countdown, nil)
    print(tm)
end

function P.launch_searchmine_timer()
    local countdown = _G.CFG.ConstLib.MINE.searchInfoDuration
    local tm = launch_timer("SearchMine", countdown, countdown, nil)
    print(tm)
end

function P.launch_stronghold_timer(time, timeMax)
    local countdown = timeMax - time
    local tm = launch_timer("Stronghold",countdown, countdown, nil)
    print(tm)
end
return P