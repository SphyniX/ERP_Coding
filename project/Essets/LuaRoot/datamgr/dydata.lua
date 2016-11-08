-- File Name : datamgr/dydata.lua

local P = setmetatable({}, _G.MT.AutoGen)

function P.get_item(id)
    local UniqueItem = P.Assets[id] or P.Props[id] or P.Dresses[id]
    if UniqueItem then return UniqueItem end
    
    local Array = {}
    for _,v in pairs(P.Equips) do
        if v.id == id then table.insert(Array, v) end
    end
    return Array
end

-- 
-- @region 获取列表
-- 
local function init_data_list(Data, key, compare)
    local List = {}
    if Data then
        for _,v in pairs(Data) do table.insert(List, v) end
        if compare then table.sort(List, compare) end
    end
    rawset(P, key, List)
    return List
end

local function get_data_list(Data, key, compare)
    local List = rawget(P, key)
    if List == nil then
        List = init_data_list(Data, key, compare)
    end
    return List
end


local function get_list_id( List, id)
    for _,v in ipairs(List) do
        if v.id == id then
            return v
        end
    end
    return nil
end

local function get_list_index(List, index)
    local i = 0
    for k,v in pairs(List) do
        i = i + 1
        if i == index then return v end
    end
    return nil
end


function P.get_attendance_list(init)
    if init then 
        return init_data_list(P.AttendanceList, "AttenceList", function (a, b)
            return a.id < b.id
        end)
    end
    return get_data_list(P.AttendanceList, "AttenceList", function (a, b)
        return a.id < b.id
    end)
end

function P.get_project_list(init)
    print("ProjectList in P.get_project_list is:" ..  JSON:encode(P.ProjectList))
    if init then
        return init_data_list(P.ProjectList, "ProList", function (a, b)
            return a.id < b.id
        end)
    end
    return get_data_list(P.ProjectList, "ProList", function (a, b)
        return a.id < b.id
    end)
end

function P.get_schproject_list(init)
    if init then
        return init_data_list(P.SchProjectList, "SchProList", function (a, b)
            return a.id < b.id
        end)
    end
    return get_data_list(P.SchProjectList, "SchProList", function (a, b)
        return a.id < b.id
    end)
end

function P.get_promoter_List(init)
    if init then
        return init_data_list(P.PromoterList, "PromoList", function (a, b)
            return a.id < b.id
        end)
    end
    return get_data_list(P.PromoterList, "PromoList", function (a, b)
        return a.id < b.id
    end)
end

function P.get_lower_list(init, type)
    if init then
        return init_data_list(P.LowerList, "LowList", function (a, b)
            return a.id < b.id
        end)
    end
    local AllList = get_data_list(P.LowerList, "LowList", function (a, b)
        return a.id < b.id
    end)
    local List = {} 
    if type then
        for i, v in ipairs(AllList) do
            if v.limit == type then
                table.insert(List, v)
            end
        end
        return List
    else
        return AllList
    end
end

function P.get_super_list(init)
    if init then
        return init_data_list(P.SuperList, "SupList", function (a, b)
            return a.id < b.id
        end)
    end
    return get_data_list(P.SuperList, "SupList", function (a, b)
        return a.id < b.id
    end)
end

function P.get_store(StoreList, storeId)
    for i, v in ipairs(StoreList) do
        if v.id == storeId then return v end
    end
    return nil
end

function P.clear()
    for k,v in pairs(P) do
        if type(v) ~= "function" then P[k] = nil end
    end
end

return P