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

-- Task 如果设定Task，则日期只能为当天或明天 SourceTime.day 不为空才可使用Task,调用Task时必须传入TaskData以界定结束时间

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
local Task,TaskData

local callback
local SourceTime

local Time = {
	month = nil,
	day = nil,
	hour = nil,
	minute = nil,
}
--!*以下：自动生成的回调函数*--

local function on_btnback_click(btn)
	UI_DATA.WNDSetTime = {}
	UIMGR.close(Ref.root)
end

local function on_subbtm_btncancle_click(btn)
	UI_DATA.WNDSetTime = {}
	UIMGR.close(Ref.root)
end

local function on_subbtm_btnconfirm_click(btn)
	
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

	if Task == nil then 

		if tonumber(Time.day) < 1 or tonumber(Time.day) > 30 then
			_G.UI.Toast:make(nil, "日期超出范围错误"):show()
			return
		end
	else

		if tonumber(Time.day) < tonumber(SourceTime.day) or tonumber(Time.day) > tonumber(SourceTime.day)+1 then
			_G.UI.Toast:make(nil, "下发任务日期只能为当天或隔天"):show()
			return
		end
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
	if Task ~= nil then
		if tonumber(Time.hour) < tonumber(TaskData.hour) then
			_G.UI.Toast:make(nil, "下发任务结束时间不得早于开始时间"):show()
			return
		end
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
	if Task ~= nil then
		if tonumber(Time.minute) <= tonumber(TaskData.minute) then
			_G.UI.Toast:make(nil, "下发任务结束时间不得早于开始时间"):show()
			return
		end
	end
	if tonumber(Time.minute) < 0 or tonumber(Time.minute) > 60 then
		_G.UI.Toast:make(nil, "分钟超出范围错误"):show()
		return
	end
	if #Time.minute > 2 then
		_G.UI.Toast:make(nil, "分钟位数错误"):show()
		return
	end
	if #Time.month < 2 then
		Time.month = "0" .. Time.month 
	end
	if #Time.day < 2 then
		Time.day = "0" .. Time.day 
	end
	if #Time.hour < 2 then
		Time.hour = "0" .. Time.hour 
	end
	if #Time.minute < 2 then
		Time.minute = "0" .. Time.minute 
	end


	if callback then callback(Time) end
	print("Time in SetTime is " .. JSON:encode(Time))

	UI_DATA.WNDSetTime = {}
	UIMGR.close(Ref.root)
end

local function init_view()
	Ref.btnBack.onAction = on_btnback_click
	Ref.SubBtm.btnCancle.onAction = on_subbtm_btncancle_click
	Ref.SubBtm.btnConfirm.onAction = on_subbtm_btnconfirm_click
	--!*以上：自动注册的回调函数*--
end

local function init_logic()

	callback = nil
	Task = nil
	SourceTime = nil

	callback = UI_DATA.WNDSetTime.on_call_back
	UI_DATA.WNDSetTime.on_call_back = nil


	local type = UIMGR.get_ui_type()
	-- libunity.SetActive(Ref.SubBtm.spRed, type == 1)
	-- libunity.SetActive(Ref.SubBtm.spBlue, type == 2)
	-- libunity.SetActive(Ref.SubBtm.spYellow, type == 3)
	if UI_DATA.WNDSetTime.SourceTime ~= nil then
		SourceTime = UI_DATA.WNDSetTime.SourceTime
		UI_DATA.WNDSetTime.SourceTime = nil
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
		UI_DATA.WNDSetTime.SetInteractable = nil
		if SetInteractable.year ~= nil then
			-- libunity.SetInteractable(Ref.SubMain.inpYear,false)
			Ref.SubMain.inpYear:SetInteractable(false)
		else
			Ref.SubMain.inpYear:SetInteractable(true)
		end
		if SetInteractable.month ~= nil then
			-- libunity.SetInteractable(Ref.SubMain.inpMonth,false)
			Ref.SubMain.inpMonth:SetInteractable(false)
		else
			Ref.SubMain.inpMonth:SetInteractable(true)
		end
		if SetInteractable.day ~= nil then
			-- libunity.SetInteractable(Ref.SubMain.inpDay,false)
			Ref.SubMain.inpDay:SetInteractable(false)
		else
			Ref.SubMain.inpDay:SetInteractable(true)
		end
		if SetInteractable.hour ~= nil then
			-- libunity.SetInteractable(Ref.SubMain.inpHour,false)
			Ref.SubMain.inpHour:SetInteractable(false)
		else
			Ref.SubMain.inpHour:SetInteractable(true)
		end
		if SetInteractable.minute ~= nil then
			-- libunity.SetInteractable(Ref.SubMain.inpMinute,false)
			Ref.SubMain.inpMinute:SetInteractable(false)
		else
			Ref.SubMain.inpMinute:SetInteractable(true)
		end
	end
	if SourceTime ~= nil then 
		if SourceTime.day ~= nil then
			if UI_DATA.WNDSetTime.Task ~= nil then 
				Task = UI_DATA.WNDSetTime.Task
				UI_DATA.WNDSetTime.Task = nil
				TaskData = UI_DATA.WNDSetTime.TaskData
				UI_DATA.WNDSetTime.TaskData = nil
			end
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

