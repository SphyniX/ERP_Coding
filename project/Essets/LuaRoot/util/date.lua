--
-- @file    util/date.lua
-- @authors xing weizhen (xingweizhen@rongygame.net)
-- @date    2016-01-17 11:22:10
-- @desc    日期工具
-- 

local zero = os.time{year=1970, month=1, day=1, hour=8}

local function date2secs(Date)
	return os.time(Date) - zero	
end

local function secs2date(fmt, secs)
	if secs == nil then secs = date2secs() end
	return os.date(fmt, secs + zero)
end

return {
	date2secs = date2secs,
	secs2date = secs2date,
}

