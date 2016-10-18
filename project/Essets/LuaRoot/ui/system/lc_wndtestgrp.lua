--
-- @file    ui/system/lc_wndtestgrp.lua
-- @authors zl
-- @date    2016-09-03 22:30:16
-- @desc    WNDTestGrp
--

local ipairs, pairs
    = ipairs, pairs
local libugui = require "libugui.cs"
local libunity = require "libunity.cs"
local UIMGR = MERequire "ui/uimgr"
local Ref


local function on_dup(Root, GORoot, prefab, n, cbf)
	local goGrp = libunity.NewChild(GORoot, prefab, "Grp")
	local Grp = libugui.GenLuaTable(goGrp,"root")
	Root.Grp = Grp
	if Grp.Ents == nil then Grp.Ents = {} end
	local Ents = Grp.Ents
	local nEnt = #Ents
	for i=1,n do
		local Ent = Ents[i]
		if Ent == nil then
			local ent = Grp.Ent.go
			local go = libunity.NewChild(Grp.root, ent, ent.name..i)
			Ent = libugui.GenLuaTable(go,"go")
			Grp.Ents[i] = Ent
		end
		libunity.SetActive(Ent.go,true)
		if cbf then cbf(i,Ent, i > nEnt) end
	end
	for i = n + 1,nEnt do
		libunity.SetActive(Ents[i].go,false)
	end 
end

--!*以下：自动生成的回调函数*--

local function init_view()
	UIMGR.make_group(Ref.Grp)
	--!*以上：自动注册的回调函数*--
end

local function init_logic()
	Ref.Grp:dup(10, function (i, Ent, isNew)
		UIMGR.dup_new_group(Ent, Ent.go, Ref.Sub.root, "Grp", 3, function (j, EntJ, isNewJ)
			-- body
		end)
	end)
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

