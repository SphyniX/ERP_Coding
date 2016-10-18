--
-- @file 	datamgr/selector.lua
-- @authors xing weizhen (xingweizhen@rongygame.com)
-- @date	2016-03-07 16:52:02
-- @desc    对象选择界面，选择器
--

local OBJDEF = {}
OBJDEF.__index = OBJDEF

function OBJDEF.new()
	return setmetatable({}, OBJDEF)
end

function OBJDEF:set_items(All, Selected, current)
	-- 迭代器
	function self:ipairs()
		local i = 0
		return function ()
			i = i + 1
			local v = All[i]
			if v then return i, v else return nil, nil end
		end
	end
	-- 索引器
	function self:get_item(index)
		if index == nil then 
			return current
		elseif index == "#" then
			return #All
		else
			return All[index]
		end
	end
	function self:is_selected(...)
		for _,v in ipairs({...}) do
			if Selected[v] then return true end
		end
		return false
	end
	function self:is_current(v)
		return current == v
	end
	return self
end

function OBJDEF:set_params(Param)
	for k,v in pairs(Param) do
		self[k] = v
	end
	return self
end

function OBJDEF:set_event(on_selected, handback)
	function self:on_selected(v)
		if type(v) == "number" then
			on_selected(self:get_item(v), handback)
		else
			on_selected(v, handback)
		end
	end
	return self
end

function OBJDEF:uninit()
	for k,v in pairs(self) do self[k] = nil end
end

return OBJDEF.new()
