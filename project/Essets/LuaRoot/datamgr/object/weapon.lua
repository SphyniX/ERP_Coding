--
-- @file    datamgr/object/weapon.lua
-- @authors xing weizhen (xingweizhen@rongygame.net)
-- @date    2015-05-20 20:49:17
-- @desc    武器类
-- 

local string, table
    = string, table
local libunity = require "libunity.cs"
local libugui = require "libugui.cs"
local libasset = require "libasset.cs"
local libbattle = require "libbattle.cs"
local DY_DATA = MERequire "datamgr/dydata"
local CFGLIB = _G.CFG

local OBJDEF = {
    STAR_LMT = 7,
    FrameColors = _G.PKG["util/color"].Weapon,
}
OBJDEF.__index = OBJDEF
OBJDEF.__tostring = function (self)
    return string.format("[武器#%d, Lv=%d, G=%d, S=%d]", self.id, self.level, self.grade, self.star)
end

function OBJDEF.new(id)
    local self = { 
        id = id,
        level = 0, exp = 0,
        grade = 0, 
        star = 0, starExp = 0,        
        Slot = { false, false, false, false },
        evolLevel = 0, evolExp = 0,        
        evolPoint = 0,
        Evols = { },
        Chips = { },
    }
    return setmetatable(self, OBJDEF)
end

function OBJDEF:update_skill(tag, id, level)
    if id then
        local SkillDEF = _G.DEF.Skill
        local Skill = self[tag]
        if not Skill then
            Skill = SkillDEF.new(id)
            self[tag] = Skill
        end
        Skill:set_level(level)
    end
end

function OBJDEF:set_info(level, grade, star)
    self:set_level(level, nil)
    self:set_grade(grade)
    self:set_star(star, nil)
    return self
end

function OBJDEF:set_level(level, exp)
    if level ~= self.level then
        -- 更新技能
        local Base = self:get_base_data()
        self:update_skill("ASkill", Base.aSkill, level)
        self:update_skill("BSkill", Base.bSkill, level)
        self:update_skill("CSkill", Base.cSkill, level)
        self:update_skill("SSkill", Base.sSkill, level)
        self:update_skill("Reload", Base.reload, level)
        self:update_skill("XSkill", Base.xSkill, level)
    end
    self.level = level
    if exp then self.exp = exp end
    return self
end

function OBJDEF:set_grade(grade)
    self.grade = grade
    return self
end

function OBJDEF:set_star(star, starExp)
    self.star = star
    if starExp then self.starExp = starExp end
    return self
end

function OBJDEF:set_evol(evol, evolExp, evolPoint)
    self.evolLevel = evol
    self.evolExp = evolExp
    self.evolPoint = evolPoint
    return self    
end

function OBJDEF:set_evol_attr(attrId, attrPoint)
    self.Evols[attrId] = attrPoint
    return self
end

function OBJDEF:set_chip(index, nFill)
    self.Chips[index] = nFill
    return self
end

function OBJDEF:set_slot(i, val)
    self.Slot[i] = val
    return self
end

function OBJDEF:get_base_data()
    if self.Base == nil then 
        local Base = CFGLIB.WeaponLib.Base[self.id] 
        if Base == nil then
            libunity.LogE("武器#{0}无配置数据", self.id)
        else self.Base = Base end
    end
    return self.Base
end

function OBJDEF:get_view_data()
    local Base = self:get_base_data()
    return {  
        name = Base.name, icon = Base.icon, model = Base.model, 
    }
end

-- 收集武器的属性表
function OBJDEF:collect_attrs(Attrs)
    local ItemDEF = _G.DEF.Item
    local Base = self:get_base_data()
    
    table.insert(Attrs, {
        atkSpeed = Base.atkSpeed,
        critDmgPt = Base.critDmgPt,
    })
    -- 等级&星级 
    local WeaponLEVEL = Base.Level[self.level]
    local WeaponSTAR = CFGLIB.WeaponLib.Star[self.star]
    table.insert(Attrs, WeaponLEVEL.Attr * WeaponSTAR.lvRevise)

    -- 阶级
    local WeaponGRADE = Base.Grade[self.grade]
    table.insert(Attrs, WeaponGRADE.Attr)

    -- 芯片
    local WeaponCHIP = Base.Chip
    for k,v in pairs(self.Chips) do
        local ChipData = WeaponCHIP[k]
        if ChipData and v >= ChipData.maxExp then
            table.insert(Attrs, ChipData.Attr)
        end
    end

    -- 进化 TODO

    -- 附件
    local Slot = WeaponGRADE.Slot
    for i,v in ipairs(self.Slot) do
        if v then
            local Item = ItemDEF.new(Slot[i])
            local Item_Base = Item:get_base_data()
            table.insert(Attrs, Item_Base.Addon.Attr)
        end
    end

    return Attrs
end

function OBJDEF:get_battle_data()
   return OBJDEF:get_view_data()
end

-- 检查附件：返回(已装备的附件? 拥有? 可装备?)
function OBJDEF:chk_addon(i)
    local ItemDEF = _G.DEF.Item
    local Base = self:get_base_data()
    local Grade = Base.Grade[self.grade]
    local addonId = Grade.Slot[i]

    -- 已装备
    if self.Slot[i] then return ItemDEF.new(addonId) end
    
    local Prop = DY_DATA.Props[addonId] or ItemDEF.new(addonId, 0)
    local PropBase = Prop:get_base_data()
    local addonReqLevel = PropBase.Addon.reqLevel
    if Prop.amount > 0 then 
        -- 未装备，拥有的附件，是否可装备
        return nil, Prop, addonReqLevel <= self.level 
    end

    local AddonMaterial = PropBase.Addon.Material
    for _,v in ipairs(AddonMaterial) do
        local Mat = DY_DATA.get_item(v.id)
        if Mat == nil or Mat.amount < v.amount then
            return nil, nil
        end
    end
    -- 未装备，可合成的附件，是否可装备
    return nil, Prop, addonReqLevel <= self.level
end

--
-- @region 界面显示
--
function OBJDEF:show_icon(spIcon)
    local Base = self:get_base_data()
    libugui.SetSprite(spIcon, "Atlas/WeaponIcon/"..Base.icon)
end

function OBJDEF:show_stars(ElmStars)
    local Base = self:get_base_data()
    libugui.SetArray(ElmStars, self.star, Base.maxStar)
end

function OBJDEF:show_view(Ent)
    local Base = self:get_base_data()
    local color = OBJDEF.FrameColors[Base.color]
    Ent.spFrame.color = color
    Ent.lbName.color = color

    Ent.lbLevel:SetFormatArgs(self.level)
    Ent.lbName.text = _G.PKG["ui/uimgr"].cfgname(Base)
    self:show_icon(Ent.spIcon)
    self:show_stars(Ent.ElmStars)
    if Ent.spType then
        Ent.spType.spritePath = "Atlas/WeaponIcon/ico_w"..Base.category
    end
    if Ent.spDType then
        Ent.spDType.spritePath = "Atlas/WeaponIcon/ico_d"..Base.dmgType
    end
end

function OBJDEF:show_detail(Sub, Equip)
    local Base = self:get_base_data()
    Sub.lbDesc.text = Base.desc

    Sub.lbTrain.text = "熟练度30："

    -- Attr
    local AttrLib = _G.CFG.AttrLib
    local SubAttrs = Sub.SubAttrs

    local Attrs = self:collect_attrs{}
    local AttrValue = AttrLib.sum_attrs(Attrs)
    local CmpAttrValue
    if Equip then
        local EqAttrs = Equip:collect_attrs{}
        local EqAttrValue = AttrLib.sum_attrs(EqAttrs)
        CmpAttrValue = AttrValue - EqAttrValue
    else
        CmpAttrValue = AttrLib.sum_attrs{}
    end

    local libsystem = require("libsystem.cs")
    local ATTRName, ATTRFmt = _G.ENV.TEXT.ATTRName, AttrLib.Format
    local AttrKeys = {  
        { "attack", "hit", "crit", },
        { "life", "defense", "dodge"},
        { "reaction", "perception", "stamina", },
    }
    for i,Keys in ipairs(AttrKeys) do
        local AttrContent = {}
        for _,key in ipairs(Keys) do
            local val = libsystem.StringFmt(ATTRFmt[key], AttrValue[key])
            local add = CmpAttrValue[key]
            if add > 0 then
                add = string.format("<color=green>+%d</color>", add)
            elseif add < 0 then
                add = string.format("<color=red>%d</color>", add)
            else
                add = ""
            end
            table.insert(AttrContent, libsystem.StringFmt("{0}：{1} {2}", ATTRName[key], val, add))
        end
        SubAttrs["lbAttr"..i].text = table.concat(AttrContent, '\n')
    end

    -- SSkill
    local SubSSkill = Sub.SubSSkill
    local SkillBase = self.SSkill:get_base_data()
    SubSSkill.spIcon.spritePath = "Atlas/SkillIcon/"..SkillBase.icon
    SubSSkill.lbName.text = _G.PKG["ui/uimgr"].cfgname(SkillBase)
    SubSSkill.lbDesc.text = SkillBase.desc
end

-- ===== 模型加载 ====
-- 实例化模型
function OBJDEF:inst_model(parent, o)
    self.go = libunity.NewChild(parent, o)
end

function OBJDEF:align_model(root, posZ, rotation)
    local transform = self.go.transform
    libugui.Overlay(transform, root, posZ)
    transform.localRotation = rotation
end

function OBJDEF:load_model(on_loaded)
    local Base = self:get_base_data()
    local GameObject = import("UnityEngine.GameObject")
    libasset.LoadAsync(GameObject.GetType(),
        string.format("Models/Weapon/%s/%s", Base.model, Base.model), 
        true, on_loaded, self)
end

return OBJDEF
