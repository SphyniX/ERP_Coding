local type, ipairs, pairs, setmetatable, table
    = type, ipairs, pairs, setmetatable, table

local OBJDEF = { }
OBJDEF.__index = OBJDEF
OBJDEF.__tostring = function (self)
	return table.concat(self, '->')
end

function OBJDEF:new()
   	local self = { }   	
   	return setmetatable(self, OBJDEF)
end

function OBJDEF:push(data)
	table.insert(self, data)
end

function OBJDEF:pop()
	return table.remove(self)
end

function OBJDEF:peek(n)
	if n == nil then n = 0 end
	n = #self - n
	return self[n]
end

function OBJDEF:clear()
	while #self > 0 do
		table.remove(self)
	end
end

function OBJDEF:top()
	return #self
end

function OBJDEF:traversal(func)
	for _,v in ipairs(self) do
		if type(func) == "function" then
			func(v)
		end
	end
end

return OBJDEF