--
-- @file    network/msgunpack.lua
-- @authors xing weizhen (xingweizhen@rongygame.com)
-- @date    2016-01-18 14:57:06
-- @desc    协议解包
--

local P = {}

function P.sc_weapon_data(nm, Weapon)
    if Weapon == nil then
        Weapon = _G.DEF.Weapon.new(nm:readU32())
    end
    local level, grade, star = nm:readU32(), nm:readU32(), nm:readU32()
    Weapon:set_info(level, grade, star)
    local evolLevel = nm:readU32()
    Weapon:set_evol(evolLevel, 0, 0)
    local nAddon = nm:readU32()
    for i=1,nAddon do
        Weapon:set_slot(i, nm:readU32() == 1)
    end
    -- 芯片信息
    local nChip = nm:readU32()
    for i=1,nChip do
        local index, nFill = nm:readU32(), nm:readU32()
        Weapon:set_chip(index, nFill)
    end
    -- 进化加点信息
    table.clear(Weapon.Evols)
    local nEvol = nm:readU32()
    for i=1,nEvol do
        local attrId, attrPoint = nm:readU32(), nm:readU32()
        Weapon:set_evol_attr(attrId, attrPoint)
    end
    return Weapon
end

function P.sc_player_data(nm)
    local IconLIB = _G.CFG.IconLib
    return {
        Server = {
            id = nm:readU32(),
            name = nm:readString(),
        },
        id = nm:readU64(),
        name = nm:readString(),
        nick = nm:readString(),
        icon = IconLIB.get_icon(nm:readU32()), 
        frame = IconLIB.get_frame(nm:readU32()),
        level = nm:readU32(),
        vipLevel = nm:readU32(),
        Guild = {
            id = nm:readU32(), name = nm:readString(),
        },
    }
end

function P.sc_equip_data(nm, Equip)
    if Equip == nil then
        Equip = _G.DEF.Equip.new(0, nm:readU32())
    end

    local level, skill = nm:readU32(), nm:readU32()
    Equip.level = level

    -- 随机属性
    local nRandom = nm:readU32()
    table.clear(Equip.RandomAttr)
    for i=1,nRandom do
        local attrId, attrVal = nm:readU32(), nm:readU32()
        Equip:set_random_attr(attrId, attrVal)
    end
    -- 改造完属性
    local nReform = nm:readU32()
    table.clear(Equip.ReformAttr)
    for i=1,nReform do
        local attrId, attrVal = nm:readU32(), nm:readU32()
        Equip:set_reform_attr(attrId, attrVal)
    end
    return Equip
end

function P.sc_role_data(nm, Role)
    if Role == nil then
        Role = _G.DEF.Girl.new(nm:readU32())
    end
    local dressId, geneIdx, power = nm:readU32(), nm:readU32(), nm:readU32()
    Role:set_dress(dressId)
    Role:set_gene(geneIdx)
    Role.power = power

    -- 训练
    local nTrain = nm:readU32()
    for i=1,nTrain do
        local trainType, trainLv = nm:readU32(), nm:readU32()
        Role:set_train(trainType, trainLv, 0)
    end
    -- 技能列表<先清空原有技能列表>
    table.clear(Role.Skills)
    local nSkill = nm:readU32()
    for i=1,nSkill do
        local skillId, skillLv = nm:readU32(), nm:readU32()
        Role:set_skill(skillId, skillLv)
    end

    Role:set_weapon(P.sc_weapon_data(nm))

    local nEquip = nm:readU32()
    for i=1,nEquip do
        Role:set_equip(P.sc_equip_data(nm))
    end

    local FinalAttr, nAttr = {}, nm:readU32()
    for i=1,nAttr do
        local key, value = nm:readU32(), nm:readU32() / 1000
        FinalAttr[key] = value
    end
    return Role
end

function P.sc_team_data(nm)
    local power = nm:readU32()  
    local Player = P.sc_player_data(nm)
    local Roles, n = {}, nm:readU32()
    for i=1,n do
        table.insert(Roles, P.sc_role_data(nm))
    end
    return {
        power = power, Player = Player, Roles = Roles,
    }
end

function P.sc_mine_info(nm)
    local id = nm:readU32()
    local type = nm:readU32()
    local nItem, Items =nm:readU32(),{}
    for i=1,nItem do
        local item_id, item_amount = nm:readU32(), nm:readU32()
        table.insert(Items, _G.DEF.Item.new(item_id, item_amount))
    end
    return {
        id = id, type = type,
        Items = Items,
    }
end

return P
