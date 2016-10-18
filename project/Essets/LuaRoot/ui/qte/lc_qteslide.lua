-- File Name : ui/qte/lc_qteslide.lua
local ipairs, pairs, tostring
    = ipairs, pairs, tostring
local libugui = require "libugui.cs"
local libunity = require "libunity.cs"
local UIMGR = MERequire "ui/uimgr"
local UI_DATA = MERequire "datamgr/uidata"
local Ref
local beginTime

local function on_qte_time_up()
	UI_DATA.QTE.on_finished(Ref, 0)
end

local function on_begin_slide()
	beginTime = _G.UnityEngine.Time.time
end

local function on_end_slide()
	if beginTime > 0 then
		local eventData = _G.UGUI.UIEventTrigger.eventData
		local pressPos = eventData.pressPosition
		local currPos = eventData.position
		local distance = currPos.x - pressPos.x
		local speed = distance / (_G.UnityEngine.Time.time - beginTime)
		local side = math.floor(speed / 1000)
		if side < 0 then side = side + 1 end
		UI_DATA.QTE.on_finished(Ref, side)
	end
end

local function on_interrupt()
	beginTime = -1
end

--!*以下：自动生成的回调函数*--

local function init_view()
	--!*以上：自动注册的回调函数*--

	local evtTgr = libunity.FindComponent(Ref.SubOp.root, nil, "UIEventTrigger")
	evtTgr.onBeginDrag = on_begin_slide
	evtTgr.onEndDrag = on_end_slide
	evtTgr.onPointerExit = on_interrupt
end

local function init_logic()
	local QTE = UI_DATA.QTE
	local timeLimit = QTE.timeLimit
	local bar = libunity.FindComponent(Ref.SubOp.root, "barCountdown", "UIProgressBar")
	local SldTween = _G.DEF.Tween.new(bar, nil, 1, 0, timeLimit)
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

