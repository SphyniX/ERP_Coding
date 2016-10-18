--
-- @file 	battle/play/defense.lua
-- @authors xing weizhen (xingweizhen@rongygame.com)
-- @date	2016-03-14 15:14:03
-- @desc    防守玩法
--

local OBJDEF = {}
OBJDEF.__index = OBJDEF

function OBJDEF.new(target)
	local Obj = {}
	Obj.__index = Obj

	local libunity = require "libunity.cs"
	local FightObj = import("Battle.FightObj").GetType()

	-- 出生的怪直奔指定的目标
	function Obj.on_object_join(obj)
		local objType = obj:GetType()	
		if objType:IsSubclassOf(FightObj) and tostring(obj.objCamp) == "Enemy" then
			obj.lockedTarget = target
		end
	end

	function Obj:add_health(value)
		self.value = self.value + value
		return self.value
	end

	function Obj:set_health(value)
		self.value = value
	end

	function Obj:finish()
		libunity.SetEnable(target, "Collider", false)
	end

	return setmetatable(Obj, OBJDEF)
end

return OBJDEF