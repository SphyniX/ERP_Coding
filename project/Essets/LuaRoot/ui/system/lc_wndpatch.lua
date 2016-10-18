--
-- @file    ui/system/lc_wndpatch.lua
-- @authors zl
-- @date    2016-08-31 17:35:18
-- @desc    WNDPatch
--

local ipairs, pairs
    = ipairs, pairs
local libugui = require "libugui.cs"
local libunity = require "libunity.cs"
local libcsharpio = require "libcsharpio.cs"
local libasset = require "libasset.cs"
local libsystem = require "libsystem.cs"
local UI_DATA = MERequire "datamgr/uidata.lua"
local NW = MERequire "network/networkmgr"
local DY_DATA = MERequire "datamgr/dydata"
local UIMGR = MERequire "ui/uimgr"
local TEXT = _G.ENV.TEXT
local Ref

local persistentDataPath = _G.ENV.app_persistentdata_path
local patchRoot = persistentDataPath .. "/Updates/"
local bundleRoot = persistentDataPath .. "/AssetBundles/"

local appUrl, on_app_download
local appName = "Richer.apk"

local failCount
local function on_download_fail(err, cbf_retry)
	print("on_download_fail")
	if err == 404 then

	else
		failCount = failCount + 1
		if failCount == 3 then
			_G.UI.MessageBox:make(nil, string.format(TEXT.fmtDownloadException, tostring(err)), true)
				:set_event(function ()				
				failCount = 0
				cbf_retry()
			end):show()

			-- local MB = UI_COM.show_messagebox(string.format(TEXT.fmtDownloadException, tostring(err)), function ()				
			-- 	failCount = 0
			-- 	cbf_retry()
			-- end, true, function ()
			-- 	if UI_DATA.WNDPatch.forceUpdate then
			-- 		libunity.AppQuit()
			-- 	else
			-- 		UIMGR.close_window()
			-- 		UIMGR.create("UI/WNDLogin")
			-- 	end
			-- end)
			-- local lb = libunity.FindComponent(MB.gameObject, "btnConfirm/lbConfirm_", "UILabel")
			-- lb.text = TEXT.tipRetryPatch
		else
			-- 重试
			cbf_retry()
		end
	end
end

local function do_app_download()	
	NW.http_download(appUrl, 0, patchRoot..appName, on_app_download)
end

on_app_download = function(url, current, totalm, err)
	if err then
		on_download_fail(err, do_app_download)
	else
		local progress = current / totalm
		-- Ref.lbTip.text = string.format("%d/%d", current, totalm)
		Ref.lbTip.text = (math.floor(progress * 10000) / 100).."%"
		Ref.srbScroll.size = progress
		if progress == 1 then
			-- 提示安装

			_G.UI.MessageBox:make("更新完成", "更新完成，确认安装", true)
				:set_event(function ( ... )
						libsystem.CallApiReturn("com.rongygame.util.XSysUtil", "installApp", patchRoot..appName)
						libunity.AppQuit()
			end):show()
			print("提示安装")
		end
	end
end

-- 资源下载管理
local coroDL, LocalFileList, RemoteFileList, UpdateFileList
local totalSiz, currSiz, currPatch
local function on_download(url, current, totalm, err)
	-- print("dl : "..url)
	if true then

		local progress = (current + currSiz) / totalSiz
		Ref.lbTip.text = (math.floor(progress * 10000) / 100).."%"
		Ref.srbScroll.size = progress
		
		if current == totalm then
			-- 记录已下载文件
			UpdateFileList.Assets[currPatch.name] = currPatch.Info
			print(JSON:encode(UpdateFileList))
			libcsharpio.WriteAllText(patchRoot.."filelist.bytes", JSON:encode(UpdateFileList))
			-- 更新总大小
			currSiz = currSiz + currPatch.Info.siz
			-- 继续下一个下载任务
			if coroutine.status(coroDL) == "suspended" then
				-- 唤醒并通知协程内部，下载成功，继续下一个
				failCount = 0
				coroutine.resume(coroDL, "next")
			end
		end
	else
		on_download_fail(err, function ()
			coroutine.resume(coroDL, "retry")
		end)
	end	
end

local function coro_download(Queue)
	local resUrl = UI_DATA.WNDPatch.resUrl
	print("while start:"..Queue:count())
	while Queue:count() > 0 do
		currPatch = Queue:peek()
		local url = resUrl .. currPatch.name
		local saveFile = patchRoot .. currPatch.name
		NW.http_download(url, 0, saveFile, on_download)
		local ret = coroutine.yield()
		if ret == "next" then
			-- 下载成功，当前任务出队列
			Queue:dequeue()
		elseif ret == "stop" then
			return
		end
	end
	-- 下载完成，安装所有文件
	
	for k,v in pairs(UpdateFileList.Assets) do		
		if libcsharpio.MoveFile(patchRoot..k, bundleRoot..k, true) then
			LocalFileList.Assets[k] = v
		end
	end
	LocalFileList.version = UpdateFileList.version
	libcsharpio.WriteAllText(bundleRoot.."filelist.bytes", JSON:encode(LocalFileList))
	libcsharpio.DeleteFile(patchRoot.."filelist.bytes")

	libunity.Delete("/AssetsMgr")
	libunity.Delete("/UIROOT")
	libunity.NewLevel("ZERO")
end

--!*以下：自动生成的回调函数*--

local function on_btndebug_click(btn)
	
end

local function init_view()
	Ref.btnDebug.onAction = on_btndebug_click
	--!*以上：自动注册的回调函数*--
end

local function init_logic()
	libcsharpio.CreateDir(patchRoot)
	libcsharpio.CreateDir(patchRoot.."lua/")
	
	if UI_DATA.WNDPatch.appUrl then
		local os = libsystem.GetOS()
		print("os is "..os)
		if os == "Android" then
			appUrl = UI_DATA.WNDPatch.appUrl
			UI_DATA.WNDPatch.appUrl = nil
			do_app_download()
		elseif os == "IOS" then
			local Param = {
				method = "Update_Ipa",
			}
			libsystem.SubmitGameData("com.rongygame.sdk.SDKApi", "OnGameMessageReturn", Param)
		else
			print("PC download APK!")
		end
	else
		-- 或者资源
		LocalFileList = UI_DATA.WNDLogin.LocalFileList
		-- 哪些文件需要更新	
		RemoteFileList = UI_DATA.WNDPatch.FileList
		-- 已下载的文件
		-- local filelist = libcsharpio.ReadAllText(patchRoot .. "filelist.bytes")
		local filelist
		if filelist == nil then
			UpdateFileList = nil
		end
		if UpdateFileList == nil or UpdateFileList.version ~= RemoteFileList.version then 
			-- 不存在已下载列表或者已下载的版本不是最新版则清空数据
			UpdateFileList = {
				version = RemoteFileList.version,
				Assets = {},
			}
		end

		local LocalAssets, RemoteAssets, UpdateAssets 
			= LocalFileList.Assets, RemoteFileList.Assets, UpdateFileList.Assets
		local QuePatch = _G.DEF.Queue:new()
		totalSiz, currSiz = 0, 0
		for k,v in pairs(RemoteAssets) do
			local LocalF = UpdateAssets[k]
			if LocalF and LocalF.md5 == v.md5 then 
				-- 已下载的文件
				currSiz = currSiz + LocalF.siz
				totalSiz = totalSiz + LocalF.siz
			else
				-- 是否需要下载
				LocalF = LocalAssets[k]
			end
			if LocalF == nil or LocalF.md5 ~= v.md5 then
				QuePatch:enqueue({name = k, Info = v})
				totalSiz = totalSiz + v.siz
			end
		end

		Ref.lbTip.text = "0%"
		Ref.srbScroll.size = 0
		failCount = 0
		coroDL = coroutine.create(coro_download)		
		coroutine.resume(coroDL, QuePatch, totalSiz)
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

