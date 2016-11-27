--
-- @file    ui/system/lc_wndsettime.lua
-- @authors zl
-- @date    2016-08-14 16:49:50
-- @desc    WNDSetTime
--


-- WNDSetTime 使用说明：

-- 如需要设定自动填写内容，应传入UI_DATA.WNDSetTime.SourceTime ,由year、month、day、hour、mintue组成，nil则不填写
-- SourceTime 必须由上述五项组成，不可缺少
-- SourceTime 可为空

-- 如需设定不可填写设置，应传入UI_DATA.WNDSetTime.SetInteractable，由year、month、day、hour、mintue组成，任意值为锁定当前InputField不可填写，nil为不锁定
-- SetInteractable 必须由上述五项组成，不可缺少
-- SetInteractable 可为空

-- 结束回调函数设定， UI_DATA.WNDSetTime.callback
-- callback 不可为空

----------------------------


local ipairs, pairs
    = ipairs, pairs
local libugui = require "libugui.cs"
local libunity = require "libunity.cs"
local UIMGR = MERequire "ui/uimgr"
local UI_DATA = MERequire "datamgr/uidata.lua"
local Ref

local Time = {
	month = nil,
	day = nil,
	hour = nil,
	minute = nil,
}
--!*以下：自动生成的回调函数*--

local function on_btnback_click(btn)
	UIMGR.close(Ref.root)
end

local function on_subbtm_btncancle_click(btn)
	UIMGR.close(Ref.root)
end

local function on_subbtm_btnconfirm_click(btn)
	local callback = UI_DATA.WNDSetTime.on_call_back

	Time.year = Ref.SubMain.inpYear.text
	if Time.year == nil or Time.year == "" then
		
		_G.UI.Toast:make(nil, "请填写年"):show()
		return
	end
	if tonumber(Time.year) < 1990 or tonumber(Time.year) > 2100 then
			_G.UI.Toast:make(nil, "年份超出范围错误"):show()
			return
		end
		if #Time.year ~= 4 then
			_G.UI.Toast:make(nil, "年份位数错误"):show()
			return
		end

	Time.month = Ref.SubMain.inpMonth.text
	
	if Time.month == nil or Time.month == "" then
		
		_G.UI.Toast:make(nil, "请填写月"):show()
		return
	end
	if tonumber(Time.month) < 1 or tonumber(Time.month) > 12 then
		_G.UI.Toast:make(nil, "月份超出范围错误"):show()
		return
	end
	if #Time.month > 2 then
		_G.UI.Toast:make(nil, "月份位数错误"):show()
		return
	end
	
	Time.day = Ref.SubMain.inpDay.text
	if Time.day == nil or Time.day == "" then
		
		_G.UI.Toast:make(nil, "请填写日"):show()
		return
	end
	if tonumber(Time.day) < 1 or tonumber(Time.day) > 30 then
		_G.UI.Toast:make(nil, "日期超出范围错误"):show()
		return
	end
	if #Time.day > 2 then
		_G.UI.Toast:make(nil, "日期位数错误"):show()
		return
	end
	Time.hour = Ref.SubMain.inpHour.text
	if Time.hour == nil or Time.hour == "" then
		
		_G.UI.Toast:make(nil, "请填写小时"):show()
		return
	end
	if tonumber(Time.hour) < 0 or tonumber(Time.hour) > 24 then
		_G.UI.Toast:make(nil, "小时超出范围错误"):show()
		return
	end
	if #Time.hour > 2 then
		_G.UI.Toast:make(nil, "小时位数错误"):show()
		return
	end
	Time.minute = Ref.SubMain.inpMinute.text
	if Time.minute == nil or Time.minute == "" then
		
		_G.UI.Toast:make(nil, "请填写分钟"):show()
		return
	end
	if tonumber(Time.minute) < 0 or tonumber(Time.minute) > 60 then
		_G.UI.Toast:make(nil, "分钟超出范围错误"):show()
		return
	end
	if #Time.minute > 2 then
		_G.UI.Toast:make(nil, "分钟位数错误"):show()
		return
	end
	if callback then callback(Time) end

	UIMGR.close(Ref.root)
end

local function init_view()
	Ref.btnBack.onAction = on_btnback_click
	Ref.SubBtm.btnCancle.onAction = on_subbtm_btncancle_click
	Ref.SubBtm.btnConfirm.onAction = on_subbtm_btnconfirm_click
	--!*以上：自动注册的回调函数*--
end

local function init_logic()
	local type = UIMGR.get_ui_type()
	-- libunity.SetActive(Ref.SubBtm.spRed, type == 1)
	-- libunity.SetActive(Ref.SubBtm.spBlue, type == 2)
	-- libunity.SetActive(Ref.SubBtm.spYellow, type == 3)
	if UI_DATA.WNDSetTime.SourceTime ~= nil then
		local SourceTime = UI_DATA.WNDSetTime.SourceTime
		Time = {
			year = nil,
			month = nil,
			day = nil,
			hour = nil,
			minute = nil,
		}
		-- Year
		if SourceTime.year ~= nil then 
			Time.year = SourceTime.year
			Ref.SubMain.inpYear.text = Time.year
		else
			Ref.SubMain.inpYear.text = ""
		end
		-- Month
		if SourceTime.month ~= nil then 
			Time.month = SourceTime.month 
			Ref.SubMain.inpMonth.text = Time.month
		else
			Ref.SubMain.inpMonth.text = ""
		end
		-- Day
		if SourceTime.day ~= nil then 
			Time.day = SourceTime.day 
			Ref.SubMain.inpDay.text = Time.day
		else
			Ref.SubMain.inpDay.text = ""
		end
		--Hour
		if SourceTime.hour ~= nil then 
			Time.hour = SourceTime.hour 
			Ref.SubMain.inpHour.text = Time.hour
		else
			Ref.SubMain.inpHour.text = ""
		end
		--Nimute
		if SourceTime.minute ~= nil then
			Time.minute = SourceTime.minute 
			Ref.SubMain.inpMinute.text = Time.minute
		else
			Ref.SubMain.inpMinute.text = ""
		end
	else
		Time = {
			year = nil,
			month = nil,
			day = nil,
			hour = nil,
			minute = nil,
		}
		Ref.SubMain.inpYear.text = ""
		Ref.SubMain.inpMonth.text = ""
		Ref.SubMain.inpDay.text = ""
		Ref.SubMain.inpHour.text = ""
		Ref.SubMain.inpMinute.text = ""
	end
	if UI_DATA.WNDSetTime.SetInteractable ~= nil then
		local SetInteractable = UI_DATA.WNDSetTime.SetInteractable
		if SetInteractable.year ~= nil then
			Ref.SubMain.inpYear:SetInteractable(false)
		end
		if SetInteractable.month ~= nil then
			Ref.SubMain.inpMonth:SetInteractable(false)
		end
		if SetInteractable.day ~= nil then
			Ref.SubMain.inpDay:SetInteractable(false)
		end
		if SetInteractable.hour ~= nil then
			Ref.SubMain.inpHour:SetInteractable(false)
		end
		if SetInteractable.minute ~= nil then
			Ref.SubMain.inpMinute:SetInteractable(false)
		end
	end
end

local function start(self)
	if Ref == nil or Ref.root ~= self then
		Ref = libugui.GenLuaTable(self, "root")
		init_view()
	end
	init_logic()
end

local function update_view()
	
end

local function on_recycle()
	
end

local P = {
	start = start,
	update_view = update_view,
	on_recycle = on_recycle,
}
return P

