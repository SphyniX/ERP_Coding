-- File Name: framework/tinyjson.lua
local libtool = require "libtool.cs"

local OBJDEF = { }
OBJDEF.__index = OBJDEF

function OBJDEF:new()
   	local self = { version = 20150413.23 }
   	return setmetatable(self, OBJDEF)
end

function OBJDEF:encode(T, prettyPrinted)
	return libtool.TableToJSON(T, prettyPrinted)
end

function OBJDEF:decode(json)
	return libtool.JSONToTable(json)
end

return OBJDEF:new()