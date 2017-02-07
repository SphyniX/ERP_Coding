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

--将会重复3次唤醒coroDL协程下载失败任务
local function on_download_fail(err, cbf_retry)
	if err == 404 then     --404表示资源不存在服务器
	else
		failCount = failCount + 1				---文件下载失败后尝试重复下载的次数
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
		Ref.lbTip.text = (math.floor(progress * 10000) / 100).."%"   -----进度条数值设置
		Ref.srbScroll.size = progress						--进度条设置
		if progress == 1 then
			-- 提示安装
			_G.UI.MessageBox:make("更新完成", "更新完成，确认安装", true)
				:set_event(function ( ... )
						libsystem.CallApiReturn("com.rongygame.util.XSysUtil", "installApp", patchRoot..appName)
						libunity.AppQuit()
			end):show()
		end
	end
end

-- 资源下载管理---任务下载完回调
local coroDL, LocalFileList, RemoteFileList, UpdateFileList
local totalSiz, currSiz, currPatch   --  --currSiz：以下在文件的总大小，totalSiz--需要下载文件的总大小，currPatch--记录以下载的文件名及信息

------
local function on_download(url, current, totalm, err)
	-- print("dl : "..url)
	if true then

		local progress = (current + currSiz) / totalSiz
		Ref.lbTip.text = (math.floor(progress * 10000) / 100).."%"    --设置编辑器下进度数值
		Ref.srbScroll.size = progress       --设置编辑器下进度
		if current == totalm then
			-- 记录已下载文件
			UpdateFileList.Assets[currPatch.name] = currPatch.Info
			libcsharpio.WriteAllText(patchRoot.."filelist.bytes", JSON:encode(UpdateFileList))
			-- 更新已下载文件和不需要下载的文件的总大小
			currSiz = currSiz + currPatch.Info.siz
			-- 继续下一个下载任务
			
			if coroutine.status(coroDL) == "suspended" then
				-- 唤醒并通知协程内部，下载成功，继续下一个
				failCount = 0
				--libunity.LogD("ui/system/lc_wndpatch.lua---xxx---on_download() --xx--唤醒并通知协程内部，下载成功，继续下一个:{0}--xx--currPatch:peek(){1}",coroutine.status(coroDL),JSON:encode( currPatch:peek()))
				coroutine.resume(coroDL,"next")
			end
		end
	else
		on_download_fail(err, function ()      					--将会重复3次唤醒coroDL协程下载失败任务
			coroutine.resume(coroDL,"retry")
		end)
	end	
end

local function coro_download(Queue)
	local resUrl = UI_DATA.WNDPatch.resUrl    ---网络路径---初始化函数：libmgr/login.lua---x--------on_get_version_back(resp, isDone, err)
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
	
	
	if UI_DATA.WNDPatch.appUrl then
		local os = libsystem.GetOS()
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
		local QuePatch = _G.DEF.Queue:new()     ---datamgr\object\queue.lua
		totalSiz, currSiz = 0, 0					--totalSiz记录所有文件的总大小----记录不需要下载文件的总大小
		for k,v in pairs(RemoteAssets) do
			local LocalF = UpdateAssets[k]
			if LocalF and LocalF.md5 == v.md5 then --如果更新文件存在且md5相同，--计算不需要下载的文件的总大小
				-- 已下载的文件
				currSiz = currSiz + LocalF.siz  --计算不需要下载的文件的总大小
				totalSiz = totalSiz + LocalF.siz
			else
				-- 是否需要下载
				LocalF = LocalAssets[k]     --记录网络所有资源
			end
			if LocalF == nil or LocalF.md5 ~= v.md5 then --如果本地文件不存在，或者md5不一样，将要下载的文件压入栈中
				--if true or k == "ui.unity3d" or k == "lua/script.unity3d" then
				--if k == "ui.unity3d"  or k == "lua/script.unity3d" or k == "config.unity3d" then
				QuePatch:enqueue({name = k, Info = v})
				totalSiz = totalSiz + v.siz 		----计算需要下载的文件的总大小
				--end
			end
		end
		print("开始下载")
		Ref.lbTip.text = "0%"
		Ref.srbScroll.size = 0
		failCount = 0
		coroDL = coroutine.create(coro_download)		--创建协程
		coroutine.resume(coroDL, QuePatch, totalSiz)     --启动协程，传入参数
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

