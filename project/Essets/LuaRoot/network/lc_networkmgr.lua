
-- File Name : network/lc_networkmgr.lua

local NW = MERequire "network/networkmgr"

return {
	on_nc_init         = NW.on_nc_init,
	on_www			   = NW.on_www,
	on_download	       = NW.on_download,
}