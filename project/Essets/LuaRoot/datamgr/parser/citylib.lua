-- File Name : datamgr/parser/citylib.lua
local P = {}
P.ProvinceList = {}

local ProvinceList
local Citys = {}
do
	local ipairs
		= ipairs
    local libasset = require "libasset.cs"
    do
	    local PROVINCE = libasset.DoLua("config/province_text.lua")
	    for _,v in ipairs(PROVINCE) do
	        local Cfg = P.ProvinceList[v.id]
	        if Cfg == nil then 
	        	Cfg = {} 
	        	P.ProvinceList[v.id] = Cfg 
	        end
	        Cfg.id = v.id
	        Cfg.name = v.name
	    end
	end
    do
		local CITY = libasset.DoLua("config/city_text.lua")
		for i,v in ipairs(CITY) do
			
			local Province = P.ProvinceList[v.from]
	        if Province == nil then Province = {id = v.from, name = "",} P.ProvinceList[v.from] = Province end
			local CityList = Province.CityList
			if CityList == nil then CityList = {} Province.CityList = CityList end
			table.insert(CityList, { id = v.id, name = v.name, province = v.from})
			table.insert(Citys, { id = v.id, name = v.name, province = v.from})
			
		end
	end
end
P.get_province_list = function ()
	if ProvinceList == nil then
		ProvinceList = {}
		for _,v in ipairs(P.ProvinceList) do
			table.insert(ProvinceList, v)
		end
		table.sort(ProvinceList, function (a, b)
			return a.id < b.id
		end)
	end
	return ProvinceList
end

P.get_city_list = function (id)
	if id == nil then return nil end
	if P.ProvinceList[id] == nil then return nil end
	return P.ProvinceList[id].CityList
end

P.get_city = function (id)
	if id == nil then return nil end
	return Citys[id]
end

print("已载入城市配置")

return P