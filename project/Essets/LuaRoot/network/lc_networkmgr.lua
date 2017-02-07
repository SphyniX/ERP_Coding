
-- File Name : network/lc_networkmgr.lua
--------zzg-----设置全局方法变量，以供c#调用
local NW = MERequire "network/networkmgr"

return {
	on_nc_init         = NW.on_nc_init,
	on_www			   = NW.on_www,
	on_download	       = NW.on_download,
}