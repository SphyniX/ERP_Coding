-- File Name : datamgr/parser/errorlib.lua

local P = {}

do
	local ipairs
		= ipairs
    local libasset = require "libasset.cs"
    local ERROR = libasset.DoLua("config/errorcode_text.lua")
    for _,v in ipairs(ERROR) do
        local Cfg = P[v.ID]
        if Cfg == nil then Cfg = {}; P[v.ID] = Cfg end
        Cfg[v.OP] = v.Info
    end
end

print("已载入错误码配置")

return P