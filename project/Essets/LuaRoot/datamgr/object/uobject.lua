--
-- @file 	datamgr/object/uobject.lua
-- @authors xing weizhen (xingweizhen@firedoggame.com)
-- @date	2016-04-01 10:02:21
-- @desc    Unity对象
--

local OBJDEF = { }

function OBJDEF.new(go, luaObj)
	local self = class({ get = function () return luaObj end, }, go)
	setudatametatable(go, self)
	return go
end

return OBJDEF
