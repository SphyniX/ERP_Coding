-- File Name : console/gmtool.lua

local function send_gm_command(...)    
    -- local Args = {...}
    -- local cmdline = table.concat(Args, " ")
    -- local NW = _G.PKG["network/networkmgr"]
    -- if NW.connected() then
    --     local nm = NW.msg("GM.CS.DO_GM_CMD")
    --     nm:writeString(cmdline)
    --     NW.send(nm)        
    -- else
    --     _G.UI.Toast:make(nil, "无网络连接"):show()
    -- end
    
end

do
    -- local NW = _G.PKG["network/networkmgr"]
    -- NW.regist("GM.SC.DO_GM_CMD", function (nm)
    --     local ret = nm:readU32()    
    --     local str = nm:readString()
    --     --_G.UI.Toast:make(nil, str):show()
    --     print(str)
    -- end)
end

return send_gm_command