-- File Name : console/console.lua

local tonumber, table
    = tonumber, table

local libunity = require "libunity.cs"
local libugui = require "libugui.cs"
local libasset = require "libasset.cs"

local goConsole
-- local output

local mtHelp = {
    __index = function (t, n)
        print("------------")
        for k,v in pairs(t) do
            if k ~= "?" then
                print(k)
            end
        end
        print("------------")
    end
}

local Command = {
    gm = _G.PKG["console/gmtool"],
    ui = setmetatable(_G.PKG["console/uitool"], mtHelp),
    net = setmetatable(_G.PKG["console/nettool"], mtHelp),
    dydata = setmetatable(_G.PKG["console/dydatatool"], mtHelp),
    battle = setmetatable(_G.PKG["console/battletool"], mtHelp),
    -- cfg = _G.PKG["console/config.lua"),    
    -- dy = _G.PKG["console/dydata.lua"),
    -- ver = { 
    --     show = function () print(libasset.GetVersion()) end,
    -- }   
}
setmetatable(Command, mtHelp)

local function help(Cmd)
    local Ret = {}
    for k,_ in pairs(Cmd) do
        table.insert(Ret, k)
    end
    return table.concat(Ret, "\n")
end

local function exec_cmd(CMD, key, ...)    
    local Cmd = CMD[key]
    local cmdType = type(Cmd)
    if cmdType == "function" then
        return Cmd(...)
    elseif cmdType == "table" then
        return exec_cmd(Cmd, ...)
    end
end

local function parse_cmd(cmdline)
    local Param = cmdline:split(" ")
    if #Param > 0 then
        return exec_cmd(Command, table.unpack(Param))
    end
end

-- local function set_output_cbf(output_cbf)
--     output = output_cbf
-- end

-- local function show_output(text)
--     if output then
--         output(text)
--     end
-- end

local function open_console()
    if not libunity.IsActive(goConsole) then
        local UIMGR = MERequire "ui/uimgr.lua" 
        goConsole = UIMGR.create("UI/FRMConsole", 100)
    end
    if goConsole then
        local input = libunity.FindGameObject(goConsole, "inpCommand")
        libugui.Select(input)
    end
end

local function close_console()
    if goConsole then
        libugui.Select("/UIROOT/UICanvas")
        libunity.Destroy(goConsole)
    end
end

return {
    parse_cmd = parse_cmd,
    -- set_output_cbf = set_output_cbf,
    -- show_output = show_output,
    open_console = open_console,
    close_console = close_console,  
}