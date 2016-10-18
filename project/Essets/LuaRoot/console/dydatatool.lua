-- File Name : console/dydatatool.lua

local P = {}

function P.list( ... )
	local libsystem = require "libsystem.cs"
	local Args = {...}
	local Data = MERequire "datamgr/dydata.lua"
	for _,v in ipairs(Args) do
		local n = tonumber(v) or v
		Data = Data[n]
		if not Data then break end
	end
	if type(Data) == "table" then
		local Hold = {}
		table.insert(Hold, "# " .. table.concat(Args, ".") .. " = ")
		for k,v in pairs(Data) do
			if type(v) ~= "function" then
				table.insert(Hold, libsystem.StringFmt("{0,-20}{1,20}", tostring(k), tostring(v)))
			end
		end
		print(table.concat(Hold, "\n"))
	else
		print(table.concat(Args, ".").." = nil")
	end
end

function P.clean()
    local DY_DATA = MERequire "datamgr/dydata.lua"
    DY_DATA.clear()
end

return P
