--
-- @file    ui/attendance/lc_wndattunder.lua
-- @authors zl
-- @date    2016-10-09 14:46:00
-- @desc    WNDAttUnder
--

local ipairs, pairs
    = ipairs, pairs
local libugui = require "libugui.cs"
local libunity = require "libunity.cs"
local UIMGR = MERequire "ui/uimgr"
local UI_DATA = MERequire "datamgr/uidata.lua"
local DY_DATA = MERequire "datamgr/dydata.lua"
local NW = MERequire "network/networkmgr"
local TEXT = _G.ENV.TEXT
local Ref
local callbackfunc

local taskid
local Reason


--!*以下：自动生成的回调函数*--

local function on_submain_btnback_click(btn)

	UIMGR.close(Ref.root)
end

local function on_submain_tgl1_change(tgl)
	Reason = TEXT.Reason[1]
end

local function on_submain_tgl2_change(tgl)
	Reason = TEXT.Reason[2]
end

local function on_submain_tgl3_change(tgl)
	Reason = TEXT.Reason[3]
end

local function on_submain_btnselect_click(btn)
	print("Reason is " .. Reason)
	if Reason == nil then
		_G.UI.Toast:make(nil, "请选择事由！"):show()
		return
	end
	if Reason == TEXT.Reason[3] then
		if Ref.SubMain.inpInput.text == "请输入其它事由" then
			_G.UI.Toast:make(nil, "请填写原因！"):show()
			return
		end
	end
	libunity.SetActive(Ref.SubTip.root, true)
end

local function on_subtip_btncomfire_click(btn)
	local nm = NW.msg("ATTENCE.CS.SALESLEAVE")
		nm:writeU32(taskid)
		nm:writeString(Reason)
		if Ref.SubMain.inpInput.text ~= nil then
			nm:writeString(Ref.SubMain.inpInput.text)
		else
			nm:writeString("")
		end
		NW.send(nm)

	if callbackfunc ~= nil then callbackfunc() end
	UIMGR.close(Ref.root)
end

local function on_subtip_btncancle_click(btn)

	UIMGR.close(Ref.root)
end



local function init_view()
	Ref.SubMain.btnBack.onAction = on_submain_btnback_click
	Ref.SubMain.tgl1.onAction = on_submain_tgl1_change
	Ref.SubMain.tgl2.onAction = on_submain_tgl2_change
	Ref.SubMain.tgl3.onAction = on_submain_tgl3_change
	Ref.SubMain.btnSelect.onAction = on_submain_btnselect_click
	Ref.SubTip.btnComfire.onAction = on_subtip_btncomfire_click
	Ref.SubTip.btnCancle.onAction = on_subtip_btncancle_click
	--!*以上：自动注册的回调函数*--
end

local function init_logic()
	Reason = nil 
	taskid = nil
	callbackfunc = nil
	taskid = UI_DATA.WNDAttUnder.taskid
	callbackfunc = UI_DATA.WNDAttUnder.callbackfunc
	UI_DATA.WNDAttUnder.taskid = nil
	UI_DATA.WNDAttUnder.callbackfunc = nil
	if taskid == nil then
		_G.UI.Toast:make(nil, "获取任务失败，请重试"):show()
		return
	end
	libunity.SetActive(Ref.SubTip.root, false)
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

