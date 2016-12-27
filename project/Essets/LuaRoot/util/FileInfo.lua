
local libasset = require"libasset.cs"
local libugui = require "libugui.cs"

local path = libasset.PersistentDataPath().."/"
FileInfo = {path = path,filter = filter}

function FileInfo.createDirectory(path)
	libugui.CreateDirectory(path)
end

function FileInfo.stringEndSub(path)
	local pathSplit = path:split("/")
	local tempPath = ""
	for i=1,#pathSplit-1 do
		tempPath = tempPath..pathSplit[i].."/"
	end
	return tempPath
	-- body
end

function FileInfo.fileWrite(path,content)
	local file = {}
	file = io.open(path,"w")
	if file == nil then
		local tempPath = FileInfo.stringEndSub(path)
		print("FileInfo.fileWrite----"..path)
		 FileInfo.createDirectory(tempPath)

		 file = io.open(path,"w")   --重新读入
		--file:close()
	end

	file:write(content.."\r")
	file:close()
	_G.UI.Toast:make(nil,"写入数据成功"):show()
	file = nil
	--print("文件"..tostring(str))
end

function FileInfo.fileState(path)
	local file = {}
	file = io.open(path)
	if file ~= nil then
		return true
	else
		return false
	end
	file:close()
	file = nil
	--print("文件"..tostring(str))
end

function FileInfo.fileRead(path)
	local file = {}
	local text =nil
	file = io.open(path,"r")
	if file == nil then
		file = nil 
		file = io.open(path,"w")
		FileInfo.fileWrite(path,JSON:encode({}))
		file = io.open(path,"r")
		file:close()
		file = nil 
		file = io.open(path,"r")
	end
	text = file:read("*a")
	file:close()
	_G.UI.Toast:make(nil,"读入数据成功"):show()
	file = nil
	return text
end
--保存table表到指定文件
function FileInfo.fileToTable(path)
	if path == nil then
		return nil
	end
	if tostring(type(path)) ~= "string" then
		return nil
	end
	local tlb = {}
	local text = FileInfo.fileRead(path)
	if text == nil then
		return nil
	else
	local tblTemp = JSON:decode(text)
	if tblTemp ~= nil then
		if type(tblTemp) == "table" then
	 		return tblTemp
		else
			print("数据读入或者转换失败")
			return nil
		end
	else
	 	return nil
	end 
	end
	-- body
end

function FileInfo.tableTofile(path,tbl)
	if path == nil or tbl == nil then
		return nil
	end
	if tostring(type(path)) ~= "string" then
		return nil
	end
	if tostring(type(tbl)) ~= "table" then
		return nil
	end

	local tblTemp = JSON:encode(tbl)
	 if tblTemp ~= nil then
	 	FileInfo.fileWrite(path,tblTemp)
	 end 
	-- body
end

function FileInfo.dataAddAndAmend(path,name,tbl)
	local tblTemp = FileInfo.fileToTable(path)
	if tblTemp == nil or type(tblTemp) == "string" then 
		return nil
	end
	if tblTemp ~= nil and type(tblTemp) == "table" then
		if next(tbl) ~= nil then
		tblTemp[name] =  tbl
		end 
	else
		return nil
	end
	FileInfo.tableTofile(path,tblTemp)

	-- body
end

function FileInfo.dataDelete(path,name)
	FileInfo.dataAddAndAmend(path,name,nil)
end
function FileInfo.getTime()
	local time = os.date("*t",os.time())	
	local date = time.year.."-"..time.month.."-"..time.day
	return date
	-- body
end
function  FileInfo.getPath(title,name)
	local date = FileInfo.getTime()
	return FileInfo.path..title.."_D"..date..name..".data"
	-- body
end



--网络数据与本地数据对比
function FileInfo.dataCompare(tblmain,tbl,id)
	if tblmain == nil then
		return
	end
	if tbl == nil then
		return
	end

 	for kmain,vmain in pairs(tblmain) do
 		for k,v in pairs(tbl) do
 			if vmain[id] == v[id] then
 				print("对比"..tostring(vmain[id]).."/"..tostring(v[id]))
 				tblmain[kmain] = tbl[k]
 			end 
		end
	end
	return tblmain
end



-----弃用

function FileInfo.fileToTableList(path)
	local file = {}
	local text =nil
	file = io.open(path,"r")
	text = file:read ("*l")
	file:close()
	local tbl = JSON:decode(text)
	return tbl
end
------



function FileInfo.getFileList(path,filter)
	print(path)
	local files,Count  = libugui.GetFileList(path,filter)
		print("length"..tostring(Count))
	if files == nil then return end 

	for i=0,Count - 1 do
		print(files[i])
	end
	return files ,Count
end

function FileInfo.deleteFiles(path,filter,fileName)
	print("1--"..fileName)
	local files ,Count = FileInfo.getFileList(path,filter)
	local date = FileInfo.getTime()
	fileName = FileInfo.path..fileName.."_D"..date
	print("1-xx-"..fileName)
	for i=0,Count - 1 do
		local fileNameLoad = files[i]
		fileNameLoad = string.gsub(fileNameLoad,"\\","/")
		local pathSplit = fileNameLoad:split("_") --string.split(fileNameLoad,)
		local fileNameLoadDate = pathSplit[1] .."_"..pathSplit[2]
		print("2----"..fileNameLoadDate)
		if fileNameLoadDate ~= fileName then
			os.remove(fileNameLoad)
		end
		
	end
end


function FileInfo.deleteAllFile(path)
	libugui.DeleteAllFile(path)
end



-- local OBJDEF = { }
-- OBJDEF.__index = OBJDEF

-- function OBJDEF:new()
--    	local self = {}
--    	return setmetatable(self, OBJDEF)
-- end








-- return OBJDEF:new()



--io.close ([file])
--io.flush ()
--io.input ([file])
--io.lines ([filename ···])
--io.open (filename [, mode])
--io.output ([file])
--io.popen (prog [, mode])      ---和系统有关
--io.read (···)
--io.tmpfile ()
--io.type (obj)
--io.write (···)


--file:clos e ()
--file:flush ()
--file:lines (···)
--file:read (···)
--file:seek ([whence [, offset]])
--file:setvbuf (mode [, size])
--file:write (···)