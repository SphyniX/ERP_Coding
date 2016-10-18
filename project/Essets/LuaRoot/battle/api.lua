--
-- @file    battle/api.lua
-- @authors xing weizhen (xingweizhen@rongygame.com)
-- @date    2015-10-29 17:55:26
-- @desc    控制战斗的接口集合，用于编辑剧情和Boss战
--

local libugui = require "libugui.cs"
local libunity = require "libunity.cs"
local libbattle = require "libbattle.cs"
local libasset = require "libasset.cs"

local P = {}

-- 快速创建新模块
function P.create_module(Event)
    return {
        on_custom_event = function(param)
            local event = Event[param]
            if event then return event() end
        end,
    }
end

--=============================================================================
-- 延迟类方法
--=============================================================================

-- 延迟真实时间，单位为秒
function P.wait_seconds(seconds, on_complete)
    libunity.Invoke(nil, on_complete, seconds)
end

-- 延迟战斗时间，单位为秒。当战斗暂停时不计算时间
function P.wait_battle_seconds(seconds, on_complete)
    libbattle.YieldForSeconds(seconds, on_complete)
end

--|||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||

--=============================================================================
-- 摄像机控制
--=============================================================================
-- 摄像机控制：把摄像机挂在某个GameObject下面
function P.put_camera(parent)
    local mgr = import("Battle.BattleCameraMgr").Inst
    mgr.allowZoom = false
    --libugui.KillTween(mgr.m_CamTrans)
    libunity.SetParent(mgr.m_CamTrans, parent)
end

-- 摄像机移动和旋转
-- @pos         目标位置，基于本地坐标
-- @rot         目标旋转的欧拉角，基于本地旋转
-- @duration    持续时间
-- @delay       延迟发动，单位为秒
-- @on_complete 移动到达后的回调
function P.move_camera(pos, duration, delay, on_complete)
    local goCam = import("Battle.BattleCameraMgr").Inst.camTrans
    libugui.DOTween("TweenPosition", goCam, 
        nil, pos, duration, "OutCubic", delay, on_complete)
end
function P.rotate_camera(rot, duration, delay, on_complete)
    local goCam = import("Battle.BattleCameraMgr").Inst.camTrans
    libugui.DOTween("TweenRotation", goCam, 
        nil, rot, duration, "OutCubic", delay, on_complete)
end

-- 设置摄像机焦点，只能看到层是"Focus"的对象
function P.focus_camera(object)
    local mainCam = import("UnityEngine.Camera").main
    mainCam.cullingMask = libunity.DelCullingMask(mainCam.cullingMask, "Role")
    libunity.SetLayer(object, "Focus")
end
-- 取消摄像机焦点
function P.unfocus_camera(object)
    local mainCam = import("UnityEngine.Camera").main
    mainCam.cullingMask = libunity.AddCullingMask(mainCam.cullingMask, "Role")
    libunity.SetLayer(object, "Role")
end

-- 重置摄像机到战斗进行时的位置
function P.reset_camera(posDura, rotDura)
    local mgr = import("Battle.BattleCameraMgr").Inst
    mgr:Resume(posDura or 1, rotDura or 1)
end
--|||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||

--=============================================================================
-- 角色控制
--=============================================================================
function P.move_role(role, pos, on_complete)
    role:MoveTo(pos)
end

function P.turn_role(role, rot, on_complete)
    -- body
end

-- 设置阵形目标
function P.set_target(target)
    local Fmt = libbattle.GetFormation()
    for _,v in ipairs(Fmt.Units) do
        v.castTarget = target
    end
end
--|||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||

--=============================================================================
-- 界面控制
--=============================================================================

-- 尝试从Boss战中加载剧情
function P.try_story(event, callback)
    local FrmBattle = _G.PKG["ui/battle/lc_frmbattle"]
    if not FrmBattle.sync_battle("ntf_custom_event", "Story", event, callback) then
        callback()
    end
end

-- 加载对话
function P.load_dialog(Content, on_finished)
    local ContentQue = nil
    if type(Content) == "number" then
        local Dialogues = _G.CFG.DialogueLib[Content]
        if Dialogues then
            ContentQue = _G.DEF.Queue:new()
            for _,v in ipairs(Dialogues) do
                ContentQue:enqueue(v)
            end
        end
    else ContentQue = Content end

    if ContentQue then
        local UI_DATA = _G.PKG["datamgr/uidata"]
        UI_DATA.WNDDialog.Content = ContentQue
        UI_DATA.WNDDialog.on_finished = on_finished
        _G.PKG["ui/uimgr"].create("UI/WNDDialog", 11)
    else
        local MB = _G.UI.MessageBox
        MB:make("错误", string.format("不存在的对话组:%s", tostring(Content)), false)
          :set_event(on_finished):show()
    end
end

-- 打开Boss战的UI
local GUIBoss
function P.show_boss_ui(boss)
    local go
    if GUIBoss and libunity.IsActive(GUIBoss.root) then
        go = GUIBoss.root
    else
        go = _G.PKG["ui/uimgr"].create("UI/GUIBossFight")
        libunity.SetParent(go, "/UIROOT/UICanvas/FRMBattle", false)
    end

    if GUIBoss == nil then
        GUIBoss = libugui.GenLuaTable(go, "root")
    end
    local Data = _G.CFG.ZombieLib.Base[boss.uniqueId]
    GUIBoss.SubHealth.lbName.text = Data.name
    GUIBoss.SubHealth.spIcon.spritePath = "Atlas/RoleIcon/"..Data.icon
    libunity.SetActive(GUIBoss.SubCount.root, false)
    libunity.SendMessage(go, "SetFighter", boss)
    return GUIBoss
end

-- 关闭Boos战UI
function P.hide_boss_ui()
    if GUIBoss and libunity.IsActive(GUIBoss.root) then
        _G.PKG["ui/uimgr"].close(GUIBoss.root, true)
    end
end

local function on_boss_name_loaded(o)
    if o then
        local go = libunity.NewChild("/UIROOT/LayCanvas", o)
        libunity.Delete(go, 3)
    end
end

function P.show_boss_name(prefab)
    -- local UIMGR = _G.PKG["ui/uimgr"]
    -- local go = UIMGR.create("UI/GUIBossName")
    -- local Ref = libugui.GenLuaTable(go, "root")
    -- Ref.lbName.text = obj and obj.Name or ""
    -- libunity.Destroy(go, 4)
    local GoType = import("UnityEngine.GameObject").GetType()
    local animPath = string.format("AppearAnim/%s/%s", prefab, prefab)
    libasset.LoadAsync(GoType, animPath, true, on_boss_name_loaded)
end

-- 显示/隐藏战斗界面
function P.set_battleframe(visible)
    local alpha = visible and 1 or 0
    libugui.SetAlpha("/UIROOT/UICanvas/FRMBattle", alpha)
    libugui.SetAlpha("/UIROOT/LayCanvas/SubHUDs", alpha)
end

-- 进入CG模式
function P.show_cgmask()
    P.set_battleframe(false)
    _G.PKG["ui/uimgr"].create("UI/MaskStoryCG")
    
end

-- 退出CG模式
function P.hide_cgmask()
    P.set_battleframe(true)
    libunity.Destroy("/UIROOT/UICanvas/MaskStoryCG")
end

-- 创建HUD
function P.hud_object(prefab, obj, alwaysShow)
    local go = libunity.AddChild("/UIROOT/LayCanvas/SubHUDs", prefab)
    if go then
        local hud = go:GetComponent("ObjectHUD")
        if hud then 
            hud.hudObject = obj
            hud.autoHide = not alwaysShow
            return hud
        end
    end
end

-- 标准进入剧情模式
function P.enter_story()
    P.pause_battle()
    P.show_cgmask()
    P.set_battleframe(false)
end

-- 标准离开剧情模式，同时会重置摄像机
function P.leave_story()
    P.reset_camera()
    P.resume_battle()
    P.hide_cgmask()
    P.set_battleframe(true)
end

--|||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||
--=============================================================================
-- 战斗控制
--=============================================================================

-- 触发刷怪
function P.trigger_round(roundPath)
    local tgr = libunity.FindComponent(nil, roundPath, "RoundTrigger")
    libunity.SendMessage(import("Battle.BattleMgr").Inst, "OnEnterRoundTrigger", tgr)
end

-- 游戏逻辑暂停
function P.pause_battle()
    import("Battle.BattleMgr").Inst:Pause()
end

-- 游戏逻辑恢复
function P.resume_battle()
    import("Battle.BattleMgr").Inst:Resume()
end

-- 停止战斗，调出结算窗口
function P.finish_battle(delay)
    local UI_DATA = _G.PKG["datamgr/uidata"]
    libunity.Invoke(nil, UI_DATA.FRMBattle.Handler.stop_battle, delay)
end

-- 离开PVP房间
function P.leave_room()
    local DY_DATA = _G.PKG["datamgr/dydata"]
    local NW = _G.PKG["network/networkmgr"]
    if NW.connected() then
        local nm = NW.msg("PVP_ROOM.CS.EXIT_ROOM")
        NW.send(nm:writeU64(DY_DATA.Player.id))
        table.clear(DY_DATA.PVP)
    else
        
    end
end

-- 退出战斗
function P.leave_battle()
    local GameObject = import("UnityEngine.GameObject")
    local listener = GameObject.FindWithTag("AudioListener")
    libunity.SetParent(listener, "/UIROOT", false)

    local DY_DATA = _G.PKG["datamgr/dydata"]
    local UI_DATA = _G.PKG["datamgr/uidata"]
    
    if DY_DATA.PVP.Room then
        P.leave_room()
    end

    local SCENE_MGR = MERequire "global/scenemgr.lua"
    SCENE_MGR.load_home_level()

    -- 恢复绑定的武器    
    for _,v in ipairs(DY_DATA.get_role_list()) do
        v.Weapon = v.BoundWeapon        
    end

    -- 清空战斗缓存数据
    table.clear(UI_DATA.FRMBattle)
end

--=============================================================================
-- 战斗报告
--=============================================================================
function P.write_report(nm)
     -- TODO 默认S级别
    nm:writeU32(4)
    local Death = _G.PKG["datamgr/uidata"].FRMBattle.SelfDeath
    nm:writeU32(#Death)
    for _,v in ipairs(Death) do
        nm:writeU32(v)
    end
end
return P
