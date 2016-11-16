--
-- @file    ui/schedule/lc_wndsupdatamsginput.lua
-- @authors zl
-- @date    2016-11-09 02:46:21
-- @desc    WNDSupDataMsgInput
--

local ipairs, pairs
    = ipairs, pairs
local libugui = require "libugui.cs"
local libunity = require "libunity.cs"
local UIMGR = MERequire "ui/uimgr"
local DY_DATA = MERequire "datamgr/dydata.lua"
local UI_DATA = MERequire "datamgr/uidata.lua"
local NW = MERequire "network/networkmgr"
local Ref

local StoreId
--!*以下：自动生成的回调函数*--

local function on_subtop_btnclean_click(btn)
	Ref.inpInput.text = nil
end

local function on_subtop_btnback_click(btn)
	Ref.inpInput.text = nil
	UIMGR.close_window(Ref.root)
end

local function on_btnsave_click(btn)

	local feedbackstr = Ref.inpInput.text
	if feedbackstr ~= nil or feedbackstr ~= "" then

		local nm = NW.msg("REPORTED.CS.GETSUPUPLOADFEEDBACK")
			nm:writeU32(StoreId)
			nm:writeU32(DY_DATA.User.id)
			-- nm:writeU32(748932)
			nm:writeString(feedbackstr)
			NW.send(nm)

		else

			_G.UI.Toast:make(nil, "请填写情报"):show()
		end
		UIMGR.close_window(Ref.root)
end

local function init_view()
	Ref.SubTop.btnClean.onAction = on_subtop_btnclean_click
	Ref.SubTop.btnBack.onAction = on_subtop_btnback_click
	Ref.btnSave.onAction = on_btnsave_click
	--!*以上：自动注册的回调函数*--
end

local function init_logic()
	StoreId = UI_DATA.WNDSupStoreData.storeId
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

