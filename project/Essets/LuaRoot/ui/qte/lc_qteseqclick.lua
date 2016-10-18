--
-- @file    ui/qte/lc_qteseqclick.lua
-- @authors xing weizhen (xingweizhen@rongygame.net)
-- @date    2015-11-24 23:53:08
-- @desc    这是一个点击类型的QTE。
-- 			<point>包含所有的可点击的点。
-- 			1、随机从<point>中选取一个点（不能和前一次一样），显示在屏幕上。
-- 			2、在<timeLimit>秒内点中即可继续第1步，否则本次QTE失败。
-- 			3、累计成功<value>次则QTE成功。

local ipairs, pairs, tostring
    = ipairs, pairs, tostring
local libugui = require "libugui.cs"
local libunity = require "libunity.cs"
local UIMGR = MERequire "ui/uimgr"
local UI_DATA = MERequire "datamgr/uidata"
local Ref
-- 成功计数，上一个点的索引
local nSucCount, prevIndex

local function is_teaching()
	-- 教学模式
	-- TODO (正式版需要额外判断此qte是否首次出现)
	return UI_DATA.QTE.teaching
end

local function quit_qte(ret)
	coroutine.yield(_G.UnityEngine.WaitForSeconds.new(1))
	libunity.SetActive(Ref.spResult, false)

	if is_teaching() and ret then
		local API = _G.PKG["battle/api"]
		API.pause_battle()
		API.show_cgmask()
		API.load_dialog(16, function ()
			API.hide_cgmask()
			API.resume_battle()
			UI_DATA.QTE.on_finished(Ref, ret)
		end)
	else
		UI_DATA.QTE.on_finished(Ref, ret)
	end
end

-- 时间结束或者全部完成
local function on_qte_time_up()	
	-- 隐藏准星
	libunity.SetActive(Ref.SubCross.root, false)

	local UI_DATA_QTE = UI_DATA.QTE
	local ret = nSucCount == UI_DATA_QTE.value
	Ref.spResult.sprite = ret and Ref.spSuc.sprite or Ref.spFail.sprite
	libunity.SetActive(Ref.spResult, true)
	libunity.StartCoroutine(nil, quit_qte, ret)
end

local function aim_next_point()
	nSucCount = nSucCount + 1
	local UI_DATA_QTE = UI_DATA.QTE	
	local nPoint = #UI_DATA_QTE.point
	local n = math.random(1, nPoint)
	if n == prevIndex then
		n = n + 1
		if n > nPoint then n = 1 end
	end
	local Ent = Ref.GrpPoints.Ents[n]
	libunity.SetActive(Ent.go, true)
	Ent.btn.interactable = true
	prevIndex = n

	-- 计时
	local timeLimit = UI_DATA_QTE.timeLimit
	local SldTween = _G.DEF.Tween.new(Ref.bar, nil, 1, 0, timeLimit)
	SldTween:on_complete(on_qte_time_up):forward()
end

local function launch_next_qte()
	local finished = nSucCount == UI_DATA.QTE.value

	local SubCross = Ref.SubCross
	-- 十字归位
	local follow = libunity.FindComponent(SubCross.root, nil, "UIFollowTarget")
	follow.enabled = false
	libugui.DOFade(SubCross.root, "Out", finished and on_qte_time_up or aim_next_point, false)
	-- 隐藏准星
	libunity.SetActive(SubCross.SubAim.root, false)
	-- 隐藏点
	if prevIndex then
		libunity.SetActive(Ref.GrpPoints.Ents[prevIndex].go, false)
	end
end

-- 成功完成一次QTE
local function complete_qte()
	local on_success = UI_DATA.QTE.on_success
	if on_success then 
		local point = UI_DATA.QTE.point[prevIndex]
		on_success(point) 
	end

	launch_next_qte()
end

-- 完成QTE点击
local function on_hit_qte(o)
	local SubCross = Ref.SubCross
	local SubAim_root = SubCross.SubAim.root 	
	libunity.SetActive(SubAim_root, true)
	libugui.DOFade(SubAim_root, "In", complete_qte, true)
	libugui.DOFade(SubCross.SubBead.root, "In", nil, true)
	local follow = libunity.FindComponent(SubCross.root, nil, "UIFollowTarget")
	follow.enabled = true
end

--!*以下：自动生成的回调函数*--

local function on_grppoints_entpoint_click()
	libugui.KillTween(Ref.bar)

	local current = _G.UGUI.UIButton.current
	current.interactable = false
	local index = tonumber(current.name:sub(9))
	-- 准星对准
	local SubCross_root = Ref.SubCross.root	
	local Ent_go = Ref.GrpPoints.Ents[index].go	
	libugui.FreeTween("TweenTransform", SubCross_root, nil, Ent_go.transform, {
			duration = 0.5, complete = on_hit_qte, ease = "InQuart",
		})

	local follow  = libugui.Follow(SubCross_root, UI_DATA.QTE.point[index])
	follow.enabled = false
end

local function gen_grppoints_ent(root, Ents, i)
	if root == nil then root = Ref.GrpPoints.root end
	if Ents == nil then Ents = Ref.GrpPoints.Ents end
	local goEnt = Ref.GrpPoints.ent
	local Ent = Ents[i]
	if Ent == nil then
		local go = libunity.AddChild(root, goEnt, "entPoint"..i)
		Ent = libugui.GenLuaTable(go, "go")
		Ent.btn.onAction = on_grppoints_entpoint_click
		Ents[i] = Ent
	end
	return Ent
end

local function dup_grppoints(n, cbf_init)
	local root, Ents = Ref.GrpPoints.root, Ref.GrpPoints.Ents
	local nEnt = #Ents
	for i = 1, n do
		local Ent = gen_grppoints_ent(root, Ents, i)
		if cbf_init then cbf_init(i, Ent, i > nEnt) end
	end
end

local function on_teaching_mode()
	local API = _G.PKG["battle/api"]
	API.pause_battle()
	API.show_cgmask()
	API.load_dialog(15, function ()
		API.resume_battle()
		API.hide_cgmask()
		launch_next_qte()
	end)
end

local function init_view()
	--!*以上：自动注册的回调函数*--

	Ref.bar = libunity.FindComponent(Ref.root, "barCountdown", "UIProgressBar")
	libunity.SetActive(Ref.spSuc, false)
	libunity.SetActive(Ref.spFail, false)
end

local function init_logic()
	local UI_DATA_QTE = UI_DATA.QTE
	
	local Points = UI_DATA_QTE.point
	local nMax = math.max(#Points, #Ref.GrpPoints.Ents)
	dup_grppoints(nMax, function (i, Ent, isNew)
		local point = Points[i]
		if point then
			--Ent.lbIndex.text = tostring(i)
			libugui.Follow(Ent.go, point)
		end
		libunity.SetActive(Ent.go, false)
	end)
	libunity.SetActive(Ref.spResult, false)

	nSucCount = 0
	prevIndex = nil

	local SubCross = Ref.SubCross
	libunity.SetActive(SubCross.root, true)
	-- 隐藏准星
	libunity.SetActive(SubCross.SubAim.root, false)
	libugui.Follow(SubCross.root, nil)

	if is_teaching() then	
		on_teaching_mode()
	else
		launch_next_qte()
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

local P = {
	start = start,
	update_view = update_view,
}
return P

