-- File Name : util/string.lua

function string:split(p)
    local insert = table.insert 
    local fields = {}
    local pattern = string.format("([^%s]+)", p)
    self:gsub(pattern, function(w) insert(fields, w) end)
    
    if p == "." then p = "%." end
    if (self:find(p)) == 1 then
        table.insert(fields, 1, "")
    end
    return fields
end

function string:splitn(p)
    local insert = table.insert 
    local fields = {}
    local pattern = string.format("([^%s]+)", p)
    self:gsub(pattern, function(w) insert(fields, tonumber(w)) end)
    return fields
end

-- 根据"2001:1|1002:2"格式的字符串生成一个表
-- {
--     { id = 2001, amount = 1 ,},    
--     { id = 1002, amount = 2 ,},
-- }
function string:splitgn(g, n)
    local insert = table.insert 
    local Groups = self:split(g)
    
    local Ret = {}
    for _,v in ipairs(Groups) do
        local T = v:splitn(n)
        insert(Ret, {id = T[1], amount = T[2]} )
    end

    return Ret
end

-- 获取目录名
function string:getdir()
    return self:match(".*/")
end

-- 获取文件名
function string:getfilename()
    return self:match(".*/(.*)")
end

-- "需要数值"
function string.require(req, curr)
    if curr < req then
        return string.format("<color=red>%d</color>", req)
    else
        return tostring(req)
    end
end

-- "<拥有数量>/<需要数量>"
function string.own_needs(own, needs, color)
    if own < needs then
        return string.format("<color=%s>%d/%d</color>", color or "#FF0000FF", own, needs)
    else
        return string.format("%d/%d", own, needs)
    end
end