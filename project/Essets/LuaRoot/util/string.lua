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



-- @authors zzg
-- @date    2016-12-20 14:48:22
-- @description   utf-8编码字符串编辑

--判断字符所占字节数
function byteNumber(coding)
    if 127 >= coding then
        return 1
    elseif coding < 192 then
        return 0
    elseif coding < 224 then
        return 2
    elseif coding < 240 then
        return 3
    elseif coding < 248 then
        return 4
    elseif coding < 252 then
        return 5
    else
        return 6
    end
end 

--截取从n到le的字符串
function string.utf8Sub(s, n, le)
    if s ~= nil then
        if tostring(type(s)) == "string" then
            if n ==nil then
                n = 1
            else
                if tostring(type(n)) ~= "number" or n < 1 then
                    n = 1
                end
            end 
            if le == nil then
                le = 1
            else
                if tostring(type(le)) ~= "number"  or le < 1 then
                    le = 1
                end             
            end 
            local index = 0
            local startIndex = 0 
            local endIndex = 0 
            for i = 1 , #s do

                local coding =  string.byte(s,i)
                if byteNumber(coding) == 0 then    
                else
                    index = index + 1
                    if index == n then
                        startIndex = i
                    end
                    if index == le then
                        endIndex = i + byteNumber(coding) - 1
                    end
                 
                 end
            end
           return string.sub(s,startIndex,endIndex) 
       end
    else
        return nil
    end
end 


--获取第n个字符
function string.utf8Index(s,n)
    if s ~= nil then
        if tostring(type(s)) == "string" then
            if n ==nil then
                n = 1
            else
                 if tostring(type(n)) ~= "number" or n < 1  then
                    n = 1
                end
            end 
            local index = 0
            local startIndex = 0 
            for i = 1 , #s do
                local coding =  string.byte(s,i)
                 if coding >= 128 and coding < 192 then    
                 else
                     index = index + 1
                     if index == n then

                        return string.sub(s,i,i + byteNumber(coding) - 1)
                     end
                 
                 end
            end
        end
    else
        return nil
    end
end


--获取字符串长度
function string.utf8Len(s)
    if s ~= nil then
        if tostring(type(s)) == "string" then 
            local index = 0
            for i = 1 , #s do
                local coding =  string.byte(s,i)
                 if byteNumber(coding) == 0 then    
                 else
                     index = index + 1       
                 end
            end
            return index
        end
    else
        return nil
    end
end


--以下是不需要传入字符串的方法
--如：local str = "截取字符串"  str = str:utf8SelfSub(1,2)     --输出str为"截取"  
function string:utf8SelfSub(n, le)
    if self ~= nil then
        if tostring(type(self)) == "string" then
            if n ==nil then
                n = 1
            else
                if tostring(type(n)) ~= "number" or n < 1 then
                    n = 1
                end
            end 
            if le == nil then
                le = 1
            else
                if tostring(type(le)) ~= "number"  or le < 1 then
                    le = 1
                end             
            end 
            local index = 0
            local startIndex = 0 
            local endIndex = 0 
            for i = 1 , #self do

                local coding =  string.byte(self,i)
                if coding >= 128 and coding < 192 then    
                else
                    index = index + 1
                    if index == n then
                        startIndex = i
                    end
                    if index == le then
                        endIndex = i + byteNumber(coding) - 1
                    end
                 
                 end
            end
           return string.sub(self,startIndex,endIndex) 
       end
    else
        return nil
    end
end 

function string:utf8SelfIndex(n)
    if self ~= nil then
        if tostring(type(self)) == "string" then
            if n ==nil then
                n = 1
            else
                 if tostring(type(n)) ~= "number" or n < 1  then
                    n = 1
                end
            end 
            local index = 0
            local startIndex = 0 
            for i = 1 , #self do
                local coding =  string.byte(self,i)
                 if coding >= 128 and coding < 192 then    
                 else
                     index = index + 1
                     if index == n then

                        return string.sub(self,i,i + byteNumber(coding) - 1)
                     end
                 
                 end
            end
        end
    else
        return nil
    end
end

function string:utf8SelfLen()
    if self ~= nil then
        if tostring(type(self)) == "string" then 
            local index = 0
            for i = 1 , #self do
                local coding =  string.byte(self,i)
                 if coding >= 128 and coding < 192 then    
                 else
                     index = index + 1       
                 end
            end
            return index
        end
    else
        return nil
    end
end