-- File Name : util/table.lua

table.unpack = unpack 

function table.void(T)
    if T == nil then 
        return true 
    else
        local k, v = next(T)
        return v == nil
    end
end

function table.clear(T)
    for k,v in pairs(T) do
        T[k] = nil
    end
end

function table.toarray(T, sort)
    local Array = {}
    for _,v in pairs(T) do
        table.insert(Array, v)
    end
    if sort then table.sort(Array, sort) end
    return Array
end

function table.tomap(Array, key)
    local Map = {}
    for _,v in ipairs(Array) do
        Map[v[key]] = v
    end
    return Map
end

function table.arrvalue(Arr)
    local Dst = {}
    for _,v in pairs(Arr) do
        Dst[v] = true
    end
    return Dst
end

-- 表深拷贝
function table:dup(orig, metable)
    local orig_type = type(orig)
    local copy
    if orig_type == 'table' then
        copy = {}
        for orig_key, orig_value in next, orig, nil do
            copy[table:dup(orig_key)] = table:dup(orig_value)
        end
        -- 可选拷贝元表
        if metable then setmetatable(copy, table:dup(getmetatable(orig))) end
    else
        -- number, string, boolean, etc
        copy = orig
    end
    return copy
end

function table.match(T, Sub, multiple)
    local Ents = {}
    for _,Ent in ipairs(T) do
        local found = true
        for k,v in pairs(Sub) do
            if Ent[k] ~= v then 
                found = false
                break
            end
        end
        if found then 
            if not multiple then 
                return Ent 
            else
                table.insert(Ents, Ent)
            end
        end
    end
    if #Ents == 0 then return nil else return Ents end
end

function table.has(Array, value)
    for i,v in ipairs(Array) do
        if v.id == value then return i, v end
    end
end

function table.contains(Dict, value)
    for k,v in pairs(Dict) do
        if v == value then return k, v end
    end
end

function table.insert_once(Array, elm)
    for _,v in ipairs(Array) do
        if v == elm then return end
    end
    table.insert(Array, elm)
end

function table.remove_elm(Array, elm)
    if not Array then return end

    local i, _ = table.has(Array, elm)
    if i then table.remove(Array, i) end
end

function table.get_base_data(this, Lib, tag)
    local Base = this.Base
    if Base == nil then
        Base = Lib[this.id] 
        assert(Base, string.format("%s#%s无配置数据", tostring(tag), tostring(this.id)))
        this.Base = Base
    end
    return Base
end
