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


--通过City_ID获取城市信息
P.get_city = function (id)
	if id == nil then return nil end
	return Citys[id]
end

P.get_province_list_fromserver = function (cityid_list)
	local ProvinceListFromServer
	ProvinceListFromServer = {}
	for i=1,#cityid_list do
		print(P.ProvinceList[Citys[cityid_list[i].id].province].name)
		for _,v in ipairs(P.ProvinceList) do
			if v.name == P.ProvinceList[Citys[cityid_list[i].id].province].name then
				local flag = true
				for i=1,#ProvinceListFromServer do
					if ProvinceListFromServer[i].name == v.name then 
						flag = false
					end
				end
				if flag then
					table.insert(ProvinceListFromServer, v)
				end
			end
		end
	end
	print(JSON:encode(ProvinceListFromServer))
	return ProvinceListFromServer
end


P.get_city_list_fromserver = function (cityid_list,name)
	local CityListFromServer
	CityListFromServer = {}
	if name == nil then return nil end
	if P.ProvinceList == nil then return nil end
	for i=1,#cityid_list do
		print(Citys[cityid_list[i].id].province)
		if P.ProvinceList[Citys[cityid_list[i].id].province].name == name then
			table.insert(CityListFromServer,Citys[cityid_list[i].id])
		end
	end
	print("CityList in P.get_city_list_fromserver is :" .. JSON:encode(CityListFromServer))
	return CityListFromServer
end


print("已载入城市配置")

return P