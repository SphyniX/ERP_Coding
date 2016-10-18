-- File Name : ui/qte/lc_qtemulticlick.lua
local ipairs, pairs, tostring
    = ipairs, pairs, tostring
local libugui = require "libugui.cs"
local libunity = require "libunity.cs"
local UIMGR = MERequire "ui/uimgr"
local UI_DATA = MERequire "datamgr/uidata"
local Ref
local count, SldTween

local function on_qte_time_up()
	UI_DATA.QTE.on_finished(Ref, count == 0)
end

--!*以下：自动生成的回调函数*--

local function on_subclick_click()
	count = count - 1

	local maxCount = UI_DATA.QTE.value
	Ref.SubClick.lbCount.text = string.own_needs(count, maxCount)
	if count == 0 then
		libugui.KillTween(Ref.SubClick.sldIcon)
		on_qte_time_up()
	end
end

local function init_view()
	Ref.SubClick.btn.onAction = on_subclick_click
	--!*以上：自动注册的回调函数*--
end

local function init_logic()
	local QTE = UI_DATA.QTE
	local SubClick = Ref.SubClick
	libugui.Follow(SubClick.root, QTE.point)
	count = QTE.value
	SubClick.lbCount.text = string.own_needs(count, count)

	local timeLimit = QTE.timeLimit
	local SubClick_sldIcon = SubClick.sldIcon
	SubClick_sldIcon.minValue = 0
	SubClick_sldIcon.maxValue = timeLimit	
	local SldTween = _G.DEF.Tween.new(SubClick.sldIcon, nil, timeLimit, 0, timeLimit)
	SldTween:on_complete(on_qte_time_up):forward()
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

local P = {
	start = start,
	update_view = update_view,
}
return P

