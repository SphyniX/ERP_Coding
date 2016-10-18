-- File Name: datamgr/object/stage.lua
local OBJDEF = { }
local DY_DATA = _G.PKG["datamgr/dydata"]
OBJDEF.__index = OBJDEF
OBJDEF.__tostring = function (self)
    return string.format("[章节: %d]", self.id)
end
local ChapterList
function OBJDEF.new(id)
    local self = { 
        id = id,
    }
    return setmetatable(self, OBJDEF)
end

function OBJDEF:get_stages()
	local StageDEF = _G.DEF.Stage

	if self.Stages then return self.Stages end
	local ChapterStages =  _G.CFG.StageLib.ChapterStages[self.id]
	local Stages = {}
	for _,v in pairs(ChapterStages) do
		table.insert(Stages, StageDEF.new(v.id, self.id))
	end
	local function sort_stages(a, b)
        return a.id < b.id
    end        
    table.sort(Stages, sort_stages)
    
	self.Stages = Stages
	return self.Stages
end

function OBJDEF:get_base_data()
    if self.Base == nil then self.Base = _G.CFG.StageLib.Chapters[self.id] end
    return self.Base
end

local function on_chapterList_init()
	if not ChapterList or #ChapterList == 0 then
		local ChapterLib = _G.CFG.StageLib.ChapterMap
		ChapterList = { [1] = {}, [2] = {}, [3] = {},}
		for _,v in pairs(ChapterLib) do
			for j,w in ipairs(v) do
				table.insert(ChapterList[j], OBJDEF.new(w))
			end
		end
	end
	return ChapterList
end

function OBJDEF.get_chapterList(level)
	if level == nil or type(level) ~= "number" then level = 1 end
	local ChapterList = on_chapterList_init()
	return ChapterList[level]
end

function OBJDEF.get_chapter(level, index)
	local ChapterList = OBJDEF.get_chapterList(level)
	return ChapterList[index]
end                        

function OBJDEF:is_locked(diff)
	local Stages = self:get_stages()
	local StageInfo = DY_DATA.Raid[diff]
	return StageInfo.mainline < Stages[#Stages].id
end

function OBJDEF.get_next(id)
	local Base = _G.CFG.StageLib.Chapters[id]
	return OBJDEF.get_chapter(Base.level, Base.index + 1)
end

return OBJDEF