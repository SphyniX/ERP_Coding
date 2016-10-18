--
-- @file    ui/_tool/normtip.lua
-- @authors xing weizhen (xingweizhen@rongygame.com)
-- @date    2015-12-22 19:21:47
-- @desc    小提示框
--          Tip.make("TIPSkillInfo").show(Item).anchor(...)

local P = { Ref = nil, }

local function chk_args(Object, method)
	if Object == P then
		error(string.format("Tip.%s must NOT be called in method format", method), 3)
	end
end

local create = _G.memoize(function (tip)
	local libugui = require "libugui.cs"	
	local go = _G.PKG["ui/uimgr"].create("UI/"..tip)
	local Ref = libugui.GenLuaTable(go, "root")
	return Ref
end)

local function coro_anchor(root, pf, target, pt, offset)
	coroutine.yield()

	local libugui = require "libugui.cs"	
	libugui.SetAlpha(root, 1)
	libugui.DOAnchor(root, pf, target, pt, offset)
	libugui.InsideScreen(root)
end

function P.make(tip)
	P.hide()

	local libunity = require "libunity.cs"
	local Ref = create(tip)
	if not libunity.IsActive(Ref.root) then
		_G.PKG["ui/uimgr"].create("UI/"..tip)
	end
	P.Ref = Ref
	return P
end

function P.show(Object)
	chk_args(Object, "show")

	local libugui = require "libugui.cs"
	local libunity = require "libunity.cs"
	if Object then
		local show_tip = Object.show_tip	
		if show_tip then 
			local Ref_Sub = P.Ref.Sub
			libugui.SetPos(Ref_Sub.root, 0, 0)
			show_tip(Object, Ref_Sub)
			return P
		else
			libunity.LogW("{0}没有show_tip方法", tostring(Object))
		end
	else
		libunity.LogW("Tip对象为空")
	end

	_G.PKG["ui/uimgr"].close(P.Ref.root, true)

	return P
end

function P.anchor(pf, target, pt, offset)
	chk_args(pf, "anchor")
	
	local libunity = require "libunity.cs"	
	if P.Ref and libunity.IsActive(P.Ref.root) then
		local libugui = require "libugui.cs"
		local root = P.Ref.root
		local Sub_root = P.Ref.Sub.root
		libugui.SetAlpha(Sub_root, 0)
		libunity.StartCoroutine(root, coro_anchor, Sub_root, pf, target, pt, offset)
	end	
	return P
end

function P.close()
	local libunity = require "libunity.cs"
	if P.Ref and libunity.IsActive(P.Ref.root) then
		_G.PKG["ui/uimgr"].close(P.Ref.root)
	end	
end

function P.hide()
	local libunity = require "libunity.cs"
	if P.Ref and libunity.IsActive(P.Ref.root) then
		_G.PKG["ui/uimgr"].close(P.Ref.root, true)
	end	
end

_G.UI.Tip = P
