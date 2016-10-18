--
-- @file    datamgr/object/client.lua
-- @authors xing weizhen (xingweizhen@rongygame.com)
-- @date    2016-01-04 12:07:13
-- @desc    客户端类，继承c#的<ZFrame.NetEngine.TcpClientHandler>
--

local libunity = require "libunity.cs"
local MSG = MERequire "network/msgdef"

local OBJDEF = { }

local OBJS = setmetatable({}, {
    __index = function (t, n)
            local nwMgr = import("ZFrame.NetEngine.NetworkMgr").Inst
            local cli = nwMgr:GetTcpHandler(n)
            setudatametatable(cli, OBJDEF)
            t[n] = cli
            return cli
        end,
    })

OBJDEF.__tostring = function(self)
    return string.format("[%s]", self.name)
end

function OBJDEF.log(fmt, ... )
    libunity.LogD("[NW] " .. fmt, ...)
end

function OBJDEF.create(name)
    return OBJS[name]
end

function OBJDEF:connect(host, port, timeout)
    OBJDEF.log("{0} Connect to [{1}:{2}]", self, host, port)
    self:Connect(host, port, timeout)
    _G.UI.Waiting.show(_G.ENV.TEXT.tipConnecting, timeout)
    return self
end

function OBJDEF:send(nm, post, only)
    if nm and self.IsConnected then
        if post then 
            OBJDEF.log("{1} --> {0}", nm, self)
        elseif only then
            OBJDEF.log("{1} |--> {0}", nm, self)
        else
            _G.UI.Waiting.show()
            OBJDEF.log("{1} ==> {0}", nm, self)
        end
        self:Send(nm)
    end
end

function OBJDEF:disconnect()
    self:Disconnect()
end

function OBJDEF:set_event(onTcpConnected, onTcpDisconnected)    
    self.onConnected = onTcpConnected
    self.onDisconnected = onTcpDisconnected
    return self
end

-- 继承<ZFrame.NetEngine.TcpClientHandler>
return class(OBJDEF, import("ZFrame.NetEngine.TcpClientHandler"))
