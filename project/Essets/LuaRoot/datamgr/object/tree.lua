local setmetatable, string
    = setmetatable, string
local JSON = JSON

local OBJDEF = { }
OBJDEF.__index = OBJDEF
OBJDEF.__tostring = function (self)
	return string.format("[Tree:%d]", self.key)
end

function OBJDEF.new(Parent, key)
   	local self = { key = key, Parent = Parent, Children = {} }
   	if Parent then
	   	Parent.Children[key] = self
	  end
   	return setmetatable(self, OBJDEF)
end

function OBJDEF:child(key, Tree)
	self.Children[key] = Tree
	Tree.Parent = self
end

function OBJDEF:get_root()
	local Parent = self
	while Parent do
		local P = Parent.Parent
		if P then Parent = P else return Parent end
	end
end

return OBJDEF