-- File Name : global/variable.lua
do
    _G.Debug = false
    _G.PhotoDebug = false
    _G.Update = true
    -- 公用元表
    _G.MT = {
        Const = {
            __newindex = function  (_, n, v)
                error("attempt to set undeclared variable "..n.."="..tostring(v), 2)
            end,
            __index = function (_, n)
                error("attempt to get undeclared variable "..n, 2)
            end,
        },  
        ReadOnly = {
            __newindex = function  (_, n, v)
               error(string.format("尝试新增只读表字段 [%s] = %s", tostring(n), tostring(v)), 2)
            end,
        },
        AutoGen = {
            __index = function (t, n)
                local v = {}
                rawset(t, n, v)
                return v
            end,
        },
    }

    -- 表
    _G.PKG = setmetatable({}, {
        __index = function (t, n)
            local v = dofile(n)
            if v then t[n] = v end
            return v
        end
    })    
    _G.MERequire = function (path, silent) 
        local s, e = path:find(".lua")
        local k = s and path:sub(1, s - 1) or path
        local ret = _G.PKG[k]
        if ret == nil and (not silent) then
            print(string.format("%s not exist!", path))
        end
        return ret
    end

    -- 类
    _G.DEF = setmetatable({}, {
        __index = function (t, n) return _G.PKG[string.lower("datamgr/object/"..n)] end
    })

    -- 配置数据
    _G.CFG = setmetatable({}, {
        __index = function (t, n) 
            local Cfg = _G.PKG[string.lower("datamgr/parser/"..n)]
            --if Cfg then setmetatable(Cfg, _G.MT.ReadOnly) end
            return Cfg
        end
    })

    -- UI公共(MessageBox, Toast, Tip, ...)
    _G.UI = {}

    local libasset = require "libasset.cs"
    -- 反射：
    if _G.reflecting_csharp then libasset.DoLua("framework/reflection.lua") end
    -- JSON库
    --_G.JSON = libasset.DoLua("framework/JSON.lua")
    _G.JSON = libasset.DoLua("framework/tinyjson.lua")

    local Application = import("UnityEngine.Application")
    -- 环境
    _G.ENV = {
        -- 帧率限制，0表示不限制
        limit_frame_rate = 0,
        -- 平台名
        unity_platform = tostring(Application.platform),
        app_data_path = Application.dataPath,
        
        app_persistentdata_path = "",
        app_streamingassets_Path = "",
        using_assetbundle = "false",

        debug = false,

        -- 设置语言选项
        lang = string.lower("zhCN"),
        current_level_name = "",
    }
    -- 加载本地化字符串
    _G.ENV.TEXT = libasset.DoLua("localize/".._G.ENV.lang..".lua")
    setmetatable(_G.ENV, _G.MT.Const)

    -- 简单的记忆函数
    _G.memoize = function (f)
        local mem = setmetatable({}, { __mode = "kv" })
        return function (x)
            local r = mem[x]
            if r == nil then
                r = f(x)
                mem[x] = r
            end
            return r
        end
    end

    -- 格式化的print函数
    _G.printf = function(fmt, ... )
        print(string.format(fmt, ...))
    end

    -- 其他扩展库
    dofile "util/string.lua"
    dofile "util/table.lua"
    dofile "util/bit32.lua"
    dofile "util/class.lua"

    dofile "ui/_tool/messagebox.lua"
    dofile "ui/_tool/normtip.lua"
    dofile "ui/_tool/toast.lua"
    dofile "ui/_tool/monotoast.lua"
    dofile "ui/_tool/netwaiting.lua"
end
