-- File Name : ui/window.lua
local ipairs, pairs, tostring
    = ipairs, pairs, tostring
local libunity = require "libunity.cs"
local libugui = require "libugui.cs"

local OBJDEF = { 
	DEPTH_FRM = 1,
	DEPTH_WND = 2,
}
OBJDEF.__index = OBJDEF
OBJDEF.__tostring = function (self)
	if self then
		return string.format("[Window:%s@%d]%s", self.path, self.depth, tostring(self.go))
	else
		return "[WindowDEF]"
	end
end

local function do_destroy_wnd(go)
	libunity.Destroy(go)
end

local function coro_pop_wnd(Wnd)
	coroutine.yield()
	if Wnd then Wnd:open() end
end

local function do_create_wnd(go)	
	libugui.DOMethod(go, "update_view")
end

local function coro_create_wnd(go)
	coroutine.yield()
	do_create_wnd(go)
end

--=============================================================================

function OBJDEF.u_create(prefab, depth, instantly)
	local go = libugui.CreateWindow(prefab, depth or 0)
	if instantly then
		libunity.StartCoroutine(go, coro_create_wnd, go)
	else
		libugui.DOFade(go, "In", do_create_wnd, true)		
	end
	return go
end

function OBJDEF.u_close(go, instantly)
	if instantly then
		do_destroy_wnd(go)
	else
		libugui.DOFade(go, "Out", do_destroy_wnd, false)		
	end
end

--=============================================================================

function OBJDEF.new(path, depth)
print("window.lua    OBJDEF.new")
	local self = { 
		path = path,
		depth = depth,
		PopWnd = nil,
   	}
   	return setmetatable(self, OBJDEF)
end

function OBJDEF:is_opened()
  -- print("打开界面---------OBJDEF:is_opened()")
	return libunity.IsActive(self.go)
end

function OBJDEF:open(instantly)
  -- print("打开界面")
	if self:is_opened() then
	 -- print("打开界面  self:is_opened()")
		--libugui.DOMethod(self.go, "update_view")
	else
	-- print("打开界面  self:is_opened()1")
		if self.Cached then
		   -- print("打开界面  self:is_opened()3")
			local self_Cached = self.Cached
			self_Cached.T[self_Cached.k] = self_Cached.v
			self.Cached = nil
		end
		-- print("打开界面  self:is_opened()4")
		self.go = OBJDEF.u_create(self.path, self.depth, instantly)
		-- print("打开界面  self:is_opened()5")
		if self.on_open then 
		   -- print("打开界面  self:is_opened()6")
			self.on_open()
		end
	end
	-- print("打开界面  self:is_opened()7")
	if self.PopWnd then
	  -- print("打开界面  self:is_opened()8")
		libunity.StartCoroutine(self.go, coro_pop_wnd, self.PopWnd)
	end
	return self.go
end

function OBJDEF:close(instantly)
	if self:is_opened() then
		if self.on_close then
			self.on_close()
		end
		OBJDEF.u_close(self.go, instantly)
	end
	if self.PopWnd then
		self.PopWnd:close()
	end
end

function OBJDEF:set_pop(Wnd)
	self.PopWnd = Wnd
end

function OBJDEF:set_on_open(cbf)
	self.on_open = cbf
end

function OBJDEF:set_on_close(cbf)
	self.on_close = cbf
end

function OBJDEF:set_cached_data(Table, key)
	self.Cached = { T = Table, k = key, v = Table[key]}
end

return OBJDEF
