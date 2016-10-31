-- File Name : libmgr/attendance.lua
local libunity = require "libunity.cs"
local libasset = require "libasset.cs"
local libsystem = require "libsystem.cs"
local libcsharpio = require "libcsharpio.cs"
local NW = MERequire "network/networkmgr"
local DY_DATA = MERequire "datamgr/dydata"
local UI_DATA = MERequire "datamgr/uidata"
local TEXT = _G.ENV.TEXT
local LOGIN = MERequire "libmgr/login.lua"
local MB = _G.UI.MessageBox
local UIMGR = MERequire "ui/uimgr"

local P = { } 