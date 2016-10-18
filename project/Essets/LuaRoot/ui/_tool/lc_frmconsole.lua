-- File Name : ui/_tool/lc_frmconsole.lua
local ipairs, pairs, tostring
    = ipairs, pairs, tostring
local libugui = require "libugui.cs"
local libunity = require "libunity.cs"
local UIMGR = MERequire "ui/uimgr.lua"
local CONSOLE = MERequire "console/console.lua"
local Ref

local function on_inpcommand_submit(inp)	
	local cmdline = inp.text
	local text = CONSOLE.parse_cmd(cmdline)
	--if text then output(text) end
	inp.text = ""
	libugui.Select("/UIROOT/UICanvas")
end

--!*以下：自动生成的回调函数*--

local function init_view()
	--!*以上：自动注册的回调函数*--
	Ref.inpCommand.onSubmit = on_inpcommand_submit
end

local function init_logic()
	
end

local function update_view()
	
end

local function start(self)
	if Ref == nil or Ref.root ~= self then
		Ref = libugui.GenLuaTable(self, "root")
		init_view()
	end
	init_logic()
end

local P = {
	update_view = update_view,
	start = start,
}
return P

