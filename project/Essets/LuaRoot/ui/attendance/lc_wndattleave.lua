--
-- @file    ui/attendance/lc_wndattleave.lua
-- @authors zl
-- @date    2016-10-09 10:45:11
-- @desc    WNDAttLeave
--

local ipairs, pairs
    = ipairs, pairs
local libugui = require "libugui.cs"
local libunity = require "libunity.cs"
local UIMGR = MERequire "ui/uimgr"
local UI_DATA = MERequire "datamgr/uidata.lua"
local NW = MERequire "network/networkmgr"
local TEXT = _G.ENV.TEXT
local Ref
local Assignmentid
local Reason
--!*以下：自动生成的回调函数*--

local function on_submain_btnback_click(btn)
	UIMGR.close(Ref.root)
end

local function on_submain_btnselect_click(btn)
	if Reason == TEXT.Reason[3] then
		if Ref.SubMain.inpInput.text == nil or Ref.SubMain.inpInput.text == "" then
			_G.UI.Toast:make(nil, "请说明原因！"):show()
			return
		end
	end
	if Reason == nil then
		_G.UI.Toast:make(nil, "请选择事由！"):show()
		return
	end
	libunity.SetActive(Ref.SubTip.root, true)
end

local function on_submain_tgl1_change(tgl)
	Reason = TEXT.Reason[4]
end

local function on_submain_tgl2_change(tgl)
	Reason = TEXT.Reason[5]
end

local function on_submain_tgl3_change(tgl)
	Reason = TEXT.Reason[6]
end

local function on_submain_tgl4_change(tgl)
	Reason = TEXT.Reason[3]
end

local function on_subtip_btncomfire_click(btn)
	
	local nm = NW.msg("ATTENCE.CS.BEDEMOBILIZED")
		nm:writeU32(Assignmentid)
		nm:writeString(Reason)
		if Ref.SubMain.inpInput.text ~= nil then
			nm:writeString(Ref.SubMain.inpInput.text)
		else
			nm:writeString("")
		end
	NW.send(nm)

	Assignmentid = nil
	UIMGR.close(Ref.root)
end

local function on_subtip_btncancle_click(btn)
	Assignmentid = nil
	UIMGR.close(Ref.root)
end

local function init_view()
	Ref.SubMain.btnBack.onAction = on_submain_btnback_click
	Ref.SubMain.btnSelect.onAction = on_submain_btnselect_click
	Ref.SubMain.tgl1.onAction = on_submain_tgl1_change
	Ref.SubMain.tgl2.onAction = on_submain_tgl2_change
	Ref.SubMain.tgl3.onAction = on_submain_tgl3_change
	Ref.SubMain.tgl4.onAction = on_submain_tgl4_change
	Ref.SubTip.btnComfire.onAction = on_subtip_btncomfire_click
	Ref.SubTip.btnCancle.onAction = on_subtip_btncancle_click
	--!*以上：自动注册的回调函数*--
end

local function init_logic()
	if UI_DATA.WNDAttLeave.Assignmentid == nil then 
		_G.UI.Toast:make(nil, "获取任务号失败，请重新打开"):show()
		UIMGR.close(Ref.root)
		return
	else
		Assignmentid = UI_DATA.WNDAttLeave.Assignmentid
		UI_DATA.WNDAttLeave.Assignmentid = nil
	end
	Ref.SubMain.tgl1.isOn = false
	Ref.SubMain.tgl2.isOn = false
	Ref.SubMain.tgl3.isOn = false
	Ref.SubMain.tgl4.isOn = false
	Ref.SubMain.inpInput.text = ""
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

