--
-- @file    datamgr/dydatactr.lua
-- @authors xing weizhen (xingweizhen@rongygame.com)
-- @date    2015-06-29 18:37:19
-- @desc    数据解析中心
--

local DY_DATA = _G.PKG["datamgr/dydata"]
local NW = _G.PKG["network/networkmgr"]
local DY_TIMER = _G.PKG["libmgr/dytimer"]
local Unpack = _G.PKG["network/msgunpack"]
local ItemDEF = _G.DEF.Item
local EquipDEF = _G.DEF.Equip
local WeaponDEF = _G.DEF.Weapon
local GirlDEF = _G.DEF.Girl
local DressDEF = _G.DEF.Dress
local TaskDEF = _G.DEF.Task
local MineDEF = _G.DEF.Mine
local GoodsDEF = _G.DEF.Goods

local P = {}
print("<color=#ff0000>---------------------------------------------加载ydatactr.lua</color>")
local function sc_attence_gettask(nm)
    local n = tonumber(nm:readString())
    local AttendanceList = {}
    for i=1,n do
        local Assignmentid = tonumber(nm:readString())
        local Attendance = {
        Assignmentid = Assignmentid,

        name = nm:readString(),
        supervisor = nm:readString(),
        starttime = nm:readString(),
        endtime = nm:readString(),
    }
    local icon = nm:readString()
    Attendance.icon = icon ~= nil and icon ~= "nil" and icon..".png" or nil
        -- table.insert(AttendanceList, Attendance)
        table.insert(AttendanceList,Attendance)
    end
    DY_DATA.AttendanceList = AttendanceList
    printf("AttendanceList : " .. JSON:encode(AttendanceList))
    -- DY_DATA.get_attendance_list(true)
end
NW.regist("ATTENCE.SC.GETWORK", sc_attence_gettask)
print("<color=#00ff00>NW.regist(ATTENCE.SC.GETWORK, sc_attence_gettask)</color>")

local function sc_attence_getcity(nm)
    local GetCityList = {}
    local projectId = tonumber(nm:readString())
    local n = tonumber(nm:readString())
    for i=1,n do
        local cityid = tonumber(nm:readString())
        local name = nm:readString()
        -- table.insert(AttendanceList, Attendance)
        table.insert(GetCityList,{id = cityid})
    end
    DY_DATA.GetCityList = GetCityList
    printf("GetCityList : " .. JSON:encode(GetCityList))
    -- DY_DATA.get_attendance_list(true)
end
NW.regist("ATTENCE.SC.GETCITY", sc_attence_getcity)

local function sc_attence_getsuptask(nm)

    local n = tonumber(nm:readString())
    print("ProjectLength in Limit 2 is :" .. n)
    local AttendanceList = {}
    for i=1,n do
        local ProjectId = tonumber(nm:readString())
        local Attendance = {
        ProjectId = ProjectId,

        name = nm:readString(),
    }
        -- table.insert(AttendanceList, Attendance)
        table.insert(AttendanceList,Attendance)
    end
    DY_DATA.AttendanceList = AttendanceList
    printf("AttendanceList : " .. JSON:encode(AttendanceList))
    -- DY_DATA.get_attendance_list(true)
end
NW.regist("ATTENCE.SC.GETPROJECT", sc_attence_getsuptask)

local function sc_attence_getattstore(nm)
    local projectId = tonumber(nm:readString())
    local n = tonumber(nm:readString())
    local AttendanceProject = DY_DATA.AttendanceList[projectId]
    local ProjectProject = DY_DATA.ProjectList[projectId]
    local SchProject = DY_DATA.SchProjectList[projectId]        
    if AttendanceProject == nil then return end
    if ProjectProject == nil then return end
    if SchProject == nil then return end
    if AttendanceProject.StoreList == nil then AttendanceProject.StoreList = {} end
    if ProjectProject.StoreList == nil then ProjectProject.StoreList = {} end
    if SchProject.StoreList == nil then SchProject.StoreList = {} end
    local AttendanceStoreList = AttendanceProject.StoreList
    local ProjectStoreList = ProjectProject.StoreList
    local SchStoreList = SchProject.StoreList
    for i=1,n do
        local id = tonumber(nm:readString())
        local name = nm:readString()
        local cityid = tonumber(nm:readString())
        local icon = nm:readString()
        icon = icon ~= nil and icon ~= "nil" and icon..".png" or nil
        local state  = tonumber(nm:readString())
        table.insert(AttendanceStoreList,{id = id, name = name, cityid = cityid , icon = icon, state = state})
        -- table.insert(ProjectStoreList,{id = id, name = name, cityid = cityid , icon = icon, state = state})
        -- table.insert(SchStoreList,{id = id, name = name, cityid = cityid , icon = icon, state = state})
    end
    print("ScheduleList is :" .. JSON:encode(SchStoreList))
    print("ScheduleList in DY_DATA is :" .. JSON:encode(DY_DATA.SchProjectList.StoreList))

    -- DY_DATA.get_attendance_list(true)
end
NW.regist("ATTENCE.SC.GETATTSTORE", sc_attence_getattstore)

local function sc_attence_getleavelist(nm)
    local n = tonumber(nm:readString())
    local List = {}
    for i=1,n do
        local id = tonumber(nm:readString())
        local Info = {
        id = id,
        starttime = nm:readString(),
        reason = nm:readString(),
            state = tonumber(nm:readString()), -- (1 审核中，2 审核成功，3 审核失败)
            time = nm:readString(),
        }
        table.insert(List, Info)
    end
    DY_DATA.LeaveList = List
end
NW.regist("ATTENCE.SC.GETLEAVELIST", sc_attence_getleavelist)

local function sc_attence_gettime(nm)
    local stime = nm:readString()
    -- local NowWeek = nm:readString()
    -- print("Time : " .. stime .. "Week : " .. NowWeek)
    -- local st = 
    local day = stime:sub(1,10)
    local time = stime:sub(12,21)
    local week = tonumber(nm:readString())
    -- print(week)
    local TEXT = _G.ENV.TEXT
    DY_DATA.Work.NowTime = {day = day, time = time, week = TEXT.Week[week]}
    -- print("Work.NowTime is :" .. JSON:encode(DY_DATA.Work.NowTime))
end
NW.regist("ATTENCE.SC.GETTIME", sc_attence_gettime)

local function sc_attence_getattence(nm)
    local stime = nm:readString()
    -- local NowWeek = nm:readString()
    -- print("Time : " .. stime .. "Week : " .. NowWeek)
    -- local st = 
    local n = nm:readString()
    print("GETATTENCE n : " .. n )
    local TEXT = _G.ENV.TEXT
    local WorkAttenceList = {}
    for i=1,n do
        local Day = nm:readString()
        local Week = tonumber(nm:readString())
        local Up = nm:readString()
        local Down = nm:readString()
        local LeaveTimes = nm:readString()

        table.insert(WorkAttenceList,{Day = Day, Week= TEXT.Week[Week], Up = Up, Down = Down, LeaveTimes = LeaveTimes})
        -- else
        --     table.insert(WorkAttenceList,{day = Day})
        -- end
    end

    DY_DATA.Work.AttenceList = WorkAttenceList
    print(JSON:encode(DY_DATA.Work.AttenceList))

end
NW.regist("ATTENCE.SC.GETATTENCE", sc_attence_getattence)

local function sc_attence_getkao(nm)
    local stime = nm:readString()
    -- local NowWeek = nm:readString()
    -- print("Time : " .. stime .. "Week : " .. NowWeek)
    -- local st = 
    local n = nm:readString()
    local TEXT = _G.ENV.TEXT
    local SaleAttenceList = {}
    for i=1,n do
        local Day = nm:readString()
        local Week = tonumber(nm:readString())
        local Name = nm:readString()
        local Up = nm:readString()
        local Down = nm:readString()
        local LeaveTimes = nm:readString()

        table.insert(SaleAttenceList,{Day = Day, Name = Name, Week= TEXT.Week[Week], Up = Up, Down = Down, LeaveTimes = LeaveTimes})
        -- else
        --     table.insert(WorkAttenceList,{day = Day})
        -- end
    end

    DY_DATA.StoreData.SaleAttenceList = SaleAttenceList
    print(JSON:encode(DY_DATA.StoreData.SaleAttenceList))

end
NW.regist("ATTENCE.SC.GETKAO", sc_attence_getkao)



local function sc_user_gettype(nm)
    local n = tonumber(nm:readString())
    local List = {}
    for i=1,n do
        local id = tonumber(nm:readString())
        local name = nm:readString()
        table.insert(List, {id = id, name = name,})
    end
    DY_DATA.FeedvackList = List
end

NW.regist("USER.SC.GETTYPE", sc_user_gettype)

local function sc_user_getinfor(nm)

    local name = nm:readString()
    local id = tonumber(nm:readString())
    local Person = {
    name = name,
    id = id,
        sex = tonumber(nm:readString()), --（1 男,2 女）
        age = tonumber(nm:readString()),
        height = tonumber(nm:readString()),
        weight = tonumber(nm:readString()),
        phone = nm:readString(),
        qq = nm:readString(),
        wechat = nm:readString(),
        email = nm:readString(), 
    }
    local icon = nm:readString()
    Person.icon =  icon ~= nil and icon ~= "nil" and icon..".png" or nil
    DY_DATA.PersonList[id] = Person
end
NW.regist("USER.SC.GETINFOR", sc_user_getinfor)

-- 用户信息
local function sc_user_get_user_infor(nm)
    local User = DY_DATA.User
    local ret = tonumber(nm:readString()) -- 1 成功

    User.limit = tonumber(nm:readString())  -- 1 促销员， 2 督导， 3 区域负责人， 4 项目负责人
    if User.limit == 1 then 
        User.phone = nm:readString()
        User.name = nm:readString()
        User.sex = tonumber(nm:readString())
        User.age = tonumber(nm:readString())
        User.height = tonumber(nm:readString())
        User.weight = tonumber(nm:readString())
        User.qq = nm:readString()
        User.wechat = nm:readString()
        User.email = nm:readString()
        User.bankcard = nm:readString()
        User.IDcard = nm:readString()
        User.workstate = tonumber(nm:readString()) -- 1 下班， 2， 上班中， 3 离岗
        User.taskid = tonumber(nm:readString())
        User.cityid = tonumber(nm:readString())
        local icon = nm:readString()
        local idcard_front = nm:readString()
        local idcard_back = nm:readString()
        local idcard_all = nm:readString()
        local cardNo_front = nm:readString()
        local cardNo_back = nm:readString()
        User.icon = icon == "nil" and nil or icon..".png"
        User.idcard_front = idcard_front == "nil" and nil or idcard_front..".png"
        User.idcard_back = idcard_back == "nil" and nil or idcard_back..".png"
        User.idcard_all = idcard_all == "nil" and nil or idcard_all..".png"
        User.cardNo_front = cardNo_front == "nil" and nil or cardNo_front..".png"
        User.cardNo_back = cardNo_back == "nil" and nil or cardNo_back..".png"
    else
        User.phone = nm:readString()
        User.name = nm:readString()
        User.qq = nm:readString()
        User.wechat = nm:readString()
        User.email = nm:readString()
        local cityid = nm:readString()
        -- User.cityid = tonumber(nm:readString())
        User.cityid = tonumber(cityid)
        local icon = nm:readString()
        User.icon = icon == "nil" and icon == nil and "test.png" or icon..".png"
    end
end
NW.regist("USER.SC.GETUSERINFOR", sc_user_get_user_infor)

-- 获取督导列表
local function sc_user_getsuperlist(nm)
    local List = DY_DATA.SuperList
    if List == nil then List = {} DY_DATA.SuperList = List end
    local n = tonumber(nm:readString())
    print(n)
    for i=1,n do
        -- 废弃
        local something = tonumber(nm:readString())
        local id = tonumber(nm:readString())
        local Info = {
        id = id,
        name = nm:readString(),
        state = tonumber(nm:readString()),
    }
    List[id] = Info
    DY_DATA.SuperList = List
    DY_DATA.get_super_list(true)
end
print("SuperList is :" .. JSON.encode(DY_DATA.SuperList))
end
NW.regist("USER.SC.GETSUPERLIST", sc_user_getsuperlist)

-- 督导使用
local function sc_reported_getrep(nm)
    local storeId = tonumber(nm:readString())
    local day = nm:readString()

    local LocalList = {}
    local n = tonumber(nm:readString())
    for i=1,n do
        local productId = tonumber(nm:readString())
        local productName = nm:readString()
        local personName = nm:readString()
        local sales = tonumber(nm:readString())
        local volume = tonumber(nm:readString())
        local price = tonumber(nm:readString())
        local average_price = tonumber(nm:readString())
        if LocalList[productId] == nil then LocalList[productId] = { id = productId, name = productName, PersonList = {} } end
        local Info = {name = personName, sales = sales, volume = volume, price = price, average_price = average_price}
        table.insert(LocalList[productId].PersonList, Info)
    end
    local List = {}
    for _,v in pairs(LocalList) do
        table.insert(List, v)
    end

    if DY_DATA.ScheduleList == nil then DY_DATA.ScheduleList = {} end
    if DY_DATA.ScheduleList[storeId] == nil then DY_DATA.ScheduleList[storeId] = {} end
    DY_DATA.ScheduleList[storeId][day] = List 
end
NW.regist("REPORTED.SC.GETREP", sc_reported_getrep)

-- local function sc_reported_getme(nm)
--     local id = tonumber(nm:readString())
--     local n, List = tonumber(nm:readString()), {}
--     for i=1,n do
--         local name = nm:readString()
--         local state = tonumber(nm:readString()) --（1 完好 2 补给 3 修理 4 更换）
--         table.insert(List, {name = name, state = state})
--     end
--     DY_DATA.SupplyList = List
-- end
-- NW.regist("REPORTED.SC.GETME", sc_reported_getme)

local function sc_reported_getcom(nm)
    local id = tonumber(nm:readString())
    local day = nm:readString()

    local CurList = {}
    local n = tonumber(nm:readString())
    for i=1,n do
        local personId = tonumber(nm:readString())
        local personName = nm:readString()
        local tid = tonumber(nm:readString())
        local name = ""
        local context = nm:readString()
        
        if CurList[tid] == nil then 
            CurList[tid] = { 
            id = tid, 
            PersonList = {},
        }
    end
    local PersonList = CurList[tid].PersonList
    table.insert(PersonList, {id = personId, name = personName, context = context})
end
local List = {}
for _,v in pairs(CurList) do
    table.insert(List, v)
end

if DY_DATA.CompareProductList == nil then DY_DATA.CompareProductList = {} end
if DY_DATA.CompareProductList[id] == nil then DY_DATA.CompareProductList[id] = {} end

DY_DATA.CompareProductList[id][day] = List
end
NW.regist("REPORTED.SC.GETCOM", sc_reported_getcom)

-- 获得促销机制
local function sc_reported_getmechanre(nm)
    local storeId = tonumber(nm:readString())
    local day = nm:readString()

    local LocalList = {}
    local n = tonumber(nm:readString())
    for i=1,n do
        local personId = tonumber(nm:readString())
        local personName = nm:readString()
        local tid = tonumber(nm:readString())
        local name = nm:readString()
        local context = nm:readString()
        if LocalList[tid] == nil then LocalList[tid] = { id = tid, name = name, PersonList = {}} end
        local PersonList = LocalList[tid].PersonList
        table.insert(PersonList, {id = personId, name = personName, context = context})
    end
    local List = {}
    for _,v in pairs(LocalList) do
        table.insert(List, v)
    end

    if DY_DATA.MechanismList == nil then DY_DATA.MechanismList = {} end
    if DY_DATA.MechanismList[storeId] == nil then DY_DATA.MechanismList[storeId] = {} end
    DY_DATA.MechanismList[storeId][day] = List
end
NW.regist("REPORTED.SC.GETMECHANRE", sc_reported_getmechanre)

-- 获得情报
local function sc_reported_getintelligence(nm)
    local storeId = tonumber(nm:readString())
    local day = nm:readString()

    if DY_DATA.IntelligenceList == nil then DY_DATA.IntelligenceList = {} end
    if DY_DATA.IntelligenceList[storeId] == nil then DY_DATA.IntelligenceList[storeId] = {} end
    if DY_DATA.IntelligenceList[storeId][day] == nil then DY_DATA.IntelligenceList[storeId][day] = {} end
    local List = DY_DATA.IntelligenceList[storeId][day]
    
    local n = tonumber(nm:readString())
    for i=1,n do
        local Info = {
        name = nm:readString(),
        context = nm:readString(),
    }
    table.insert(List, Info)
end
end
NW.regist("REPORTED.SC.GETINTELLIGENCE", sc_reported_getintelligence)

-- 获得考勤信息
local function sc_reported_getattinfor(nm)
    local storeId = tonumber(nm:readString())
    local day = nm:readString()

    if DY_DATA.AttInfoList == nil then DY_DATA.AttInfoList = {} end
    if DY_DATA.AttInfoList[storeId] == nil then DY_DATA.AttInfoList[storeId] = {} end
    if DY_DATA.AttInfoList[storeId][day] == nil then DY_DATA.AttInfoList[storeId][day] = {} end
    local List = DY_DATA.AttInfoList[storeId][day]
    
    local n = tonumber(nm:readString())
    for i=1,n do
        local Info = {
        personId = tonumber(nm:readString()),
            -- personName = nm:readString(),
            personName = "",
            startTime = nm:readString(),
            endtime = nm:readString(),
        }
        local image1 = nm:readString() -- 上班
        local image2 = nm:readString() -- 下班
        local image3 = nm:readString() -- 竞品1
        local image4 = nm:readString() -- 竞品2
        local image5 = nm:readString() -- 竞品3
        local image6 = nm:readString() -- 竞品4
        
        Info.image1 = image1 ~= "nil" and image1 ~= nil and  image1..".png" or nil -- 上班
        Info.image2 = image2 ~= "nil" and image2 ~= nil and  image2..".png" or nil -- 下班
        Info.image3 = image3 ~= "nil" and image3 ~= nil and  image3..".png" or nil -- 竞品1
        Info.image4 = image4 ~= "nil" and image4 ~= nil and  image4..".png" or nil -- 竞品2
        Info.image5 = image5 ~= "nil" and image5 ~= nil and  image5..".png" or nil -- 竞品3
        Info.image6 = image6 ~= "nil" and image6 ~= nil and  image6..".png" or nil -- 竞品4       
        table.insert(List, Info)
    end 
end
NW.regist("REPORTED.SC.GETATTINFOR", sc_reported_getattinfor)

local function sc_reported_getproject(nm)
    local n = tonumber(nm:readString())


    if DY_DATA.SchProjectList == nil then DY_DATA.SchProjectList = {} end
    local List = DY_DATA.SchProjectList
    for i=1,n do
        local id = tonumber(nm:readString())
        local name = nm:readString()
        local type = nm:readString()
        local icon = nm:readString()
        icon = icon ~= nil and icon ~= "nil" and icon..".png" or nil
        List[id] = {id = id, name = name, type = type, icon = icon}
    end
    DY_DATA.SchProjectList = List
    DY_DATA.get_schproject_list(true)




end
NW.regist("REPORTED.SC.GETPROJECT", sc_reported_getproject)

local function sc_reported_getstore(nm)
    local n = tonumber(nm:readString())
    for i=1,n do
        local projectId = tonumber(nm:readString())
        local Project = DY_DATA.SchProjectList[projectId]
        if Project == nil then return end
        if Project.StoreList == nil then Project.StoreList = {} end
        local StoreList = Project.StoreList
        local id = tonumber(nm:readString())
        local name = nm:readString()
        local icon = nm:readString()
        icon = icon ~= nil and icon ~= "nil" and icon..".png" or nil
        table.insert(StoreList,{id = id, projectId = projectId, name = name, icon = icon,})
    end
end
NW.regist("REPORTED.SC.GETSTORE", sc_reported_getstore)


local function sc_reported_getstore(nm)
    
    local PhotoList = {}
    local  storeId = nm:readString()
    local n = tonumber(nm:readString())
    for i=1,n do
        local userid = tonumber(nm:readString())
        local username = nm:readString()
        local j = tonumber(nm:readString())
        for k=1,j do
            local Photo = {
            id = tonumber(nm:readString()),
            name = nm:readString(),
            photo = nm:readString(),
        }
        end
        
        table.insert(PhotoList,{userid = userid, username = username, Photo = Photo})
    end
    DY_DATA.StoreData.PhotoList = PhotoList
    print("PhotoList is " .. JSON:encode(DY_DATA.StoreData.PhotoList))
end
NW.regist("REPORTED.SC.GETSUPGETPHOTO", sc_reported_getstore)


local function sc_reported_getstoreinfor(nm)
    local projectId = tonumber(nm:readString())
    local Project = DY_DATA.SchProjectList[projectId]
    if Project == nil then return end
    if Project.StoreList == nil then Project.StoreList = {} end
    local StoreList = Project.StoreList
    local id = tonumber(nm:readString())
    local name = nm:readString()
    local address = nm:readString()
    local superId = nm:readString()
    local day = nm:readString()
    local week = nm:readString()
    local time = nm:readString()
    local str = time:split('-')
    local starttime = str[1]
    local endtime = str[2]
    local Store
    for _,v in ipairs(StoreList) do
        if v.id == id then Store = v end
    end
    Store.Info = {
    address = address,
    superId = superId,
    day = day,
    week = week,
    starttime = starttime,
    endtime = endtime,
}
print("REPORTED.SC.GETSTOREINFOR StoreInfo : " .. JSON:encode(Store.Info))
end
NW.regist("REPORTED.SC.GETSTOREINFOR", sc_reported_getstoreinfor)

local function sc_reported_getproduct(nm)
    local n = tonumber(nm:readString())
    for i=1,n do
        local projectId = tonumber(nm:readString())
        local Project = DY_DATA.SchProjectList[projectId]
        if Project ~= nil then
            local ProductList = Project.ProductList
            if ProductList == nil then
                ProductList = {}
                Project.ProductList = ProductList
            end

            local Info = {
            id = tonumber(nm:readString()),
            projectId = projectId,
            name = nm:readString(),
            per = nm:readString(),
        }
        local icon = nm:readString()
        Info.icon = icon ~= nil and icon ~= "nil" and icon..".png" or nil

        table.insert(ProductList, Info)
    end
end
end
NW.regist("REPORTED.SC.GETPRODUCT", sc_reported_getproduct)


local function sc_reported_getsamplelist( nm )
    local projectId = tonumber(nm:readString())
    local Project = DY_DATA.SchProjectList[projectId]
    if Project == nil then return end

    if Project.SampleList == nil then Project.SampleList = {} end
    local SampleList = Project.SampleList

    local n = tonumber(nm:readString())
    for i=1,n do

        local Info = {

        id = tonumber(nm:readString()),
        name = nm:readString(),
        per = nm:readString(),
        
    }

    table.insert(SampleList, Info) 

end

end
NW.regist("REPORTED.SC.GETSAMPLE",sc_reported_getsamplelist)

local function sc_reported_getgiftlist ( nm )

    local projectId = tonumber(nm:readString())
    local Project = DY_DATA.SchProjectList[projectId]
    if Project == nil then return end

    if Project.GiftList == nil then Project.GiftList = {} end
    local GiftList = Project.GiftList

    local n = tonumber(nm:readString())
    for i=1,n do

        local Info = {

        id = tonumber(nm:readString()),
        name = nm:readString(),
        per = nm:readString(),
        
    }

    table.insert(GiftList, Info) 

end

end
NW.regist("REPORTED.SC.GETGIFT",sc_reported_getgiftlist)


local function sc_reported_getmechanism ( nm )

    local storeId = tonumber(nm:readString())
    if DY_DATA.StoreData.MechanismList == nil then DY_DATA.StoreData.MechanismList = {} end

    local n = tonumber(nm:readString())
    local MechanismList = {}
    for i=1,n do
        local Mechanism = {
            id = tonumber(nm:readString()),
            name = nm:readString(),
            value = nm:readString(),
            userid = nm:readString(),
            username = nm:readString(),
        }
        table.insert(MechanismList, Mechanism) 
    end
    print("MechanismList is :" .. JSON:encode(MechanismList))
    DY_DATA.StoreData.MechanismList = MechanismList
end
NW.regist("REPORTED.SC.GETSUPMECHANISM",sc_reported_getmechanism)

local function sc_reported_getsamplere ( nm )

    local storeId = tonumber(nm:readString())
    if DY_DATA.StoreData.SampleReList == nil then DY_DATA.StoreData.SampleReList = {} end

    local n = tonumber(nm:readString())
    local SampleReList = {}
    for i=1,n do
        local SampleRe = {
            id = tonumber(nm:readString()),
            name = nm:readString(),
            value = nm:readString(),
            per = nm:readString(),
            number = nm:readString(),
            userid = nm:readString(),
            username = nm:readString(),
        }
        table.insert(SampleReList, SampleRe) 
    end
    print("SampleReList is :" .. JSON:encode(SampleReList))
    DY_DATA.StoreData.SampleReList = SampleReList
end
NW.regist("REPORTED.SC.GETSUPSAMPLERE",sc_reported_getsamplere)

local function sc_reported_getggiftre ( nm )

    local storeId = tonumber(nm:readString())
    if DY_DATA.StoreData.GiftReList == nil then DY_DATA.StoreData.GiftReList = {} end

    local n = tonumber(nm:readString())
    local GiftReList = {}
    for i=1,n do
        local GiftRe = {
            id = tonumber(nm:readString()),
            name = nm:readString(),
            number = nm:readString(),
            per = nm:readString(),
            userid = nm:readString(),
            username = nm:readString(),
        }
        table.insert(GiftReList, GiftRe) 
    end
    print("GiftReList is :" .. JSON:encode(GiftReList))
    DY_DATA.StoreData.GiftReList = GiftReList
end
NW.regist("REPORTED.SC.GETSUPGIFTRE",sc_reported_getggiftre)

local function sc_reported_getcomlistre ( nm )

    local storeId = tonumber(nm:readString())
    if DY_DATA.StoreData.ComListRe == nil then DY_DATA.StoreData.ComListRe = {} end

    local n = tonumber(nm:readString())
    local ComListRe = {}
    for i=1,n do
        local ComRe = {
            id = tonumber(nm:readString()),
            name = nm:readString(),
            price = nm:readString(),
            value = nm:readString(),
            icon = nm:readString(),
            userid = nm:readString(),
            username = nm:readString(),
        }
        local tempicon = ComRe.icon ~= nil and ComRe.icon ~= "nil" and ComRe.icon..".png" or nil
        ComRe.icon = tempicon
        table.insert(ComListRe, ComRe) 
    end
    print("ComListRe is :" .. JSON:encode(ComListRe))
    DY_DATA.StoreData.ComListRe = ComListRe
end
NW.regist("REPORTED.SC.GETSUPGETCOMPETING",sc_reported_getcomlistre)

local function sc_reported_getfeedbackre ( nm )

    local storeId = tonumber(nm:readString())
    if DY_DATA.StoreData.FeedBcakListRe == nil then DY_DATA.StoreData.FeedBcakListRe = {} end

    local n = tonumber(nm:readString())
    local FeedBcakListRe = {}
    for i=1,n do
        local FeedBcak = {
            id = tonumber(nm:readString()),
            value = nm:readString(),
            userid = nm:readString(),
            username = nm:readString(),
        }
        table.insert(FeedBcakListRe, FeedBcak) 
    end
    print("FeedBcakListRe is :" .. JSON:encode(FeedBcakListRe))
    DY_DATA.StoreData.FeedBcakListRe = FeedBcakListRe
end
NW.regist("REPORTED.SC.GETSUPGETFEEDBACK",sc_reported_getfeedbackre)

local function sc_reported_getfeedbackre ( nm )

    local storeId = tonumber(nm:readString())
    if DY_DATA.StoreData.MatterListRe == nil then DY_DATA.StoreData.MatterListRe = {} end

    local n = tonumber(nm:readString())
    local MatterListRe = {}
    for i=1,n do
        local MaterList = {
            id = tonumber(nm:readString()),
            name = nm:readString(),
            state = nm:readString(),
            value = nm:readString(),
            icon = nm:readString(),
            userid = nm:readString(),
            username = nm:readString(),
        }
        local tempicon = MaterList.icon ~= nil and MaterList.icon ~= "nil" and MaterList.icon..".png" or nil
        MaterList.icon = tempicon
        table.insert(MatterListRe, MaterList) 

    end
    print("MatterListRe is :" .. JSON:encode(MatterListRe))
    DY_DATA.StoreData.MatterListRe = MatterListRe
end
NW.regist("REPORTED.SC.GETSUPMATTER",sc_reported_getfeedbackre)


local function sc_reported_getaggregate( nm )
    local storeId = tonumber(nm:readString())
    local Userid = tonumber(nm:readString())
    if DY_DATA.StoreData.Aggregate == nil then DY_DATA.StoreData.Aggregate = {} end
    local Aggregate = {}

    local Aggregate = {
         value = nm:readString(),
         number = nm:readString(),
     }

    print("Aggregate is :" .. JSON:encode(Aggregate))
    DY_DATA.StoreData.Aggregate = Aggregate
end
NW.regist("REPORTED.SC.GETSUPGETAGGREGATE",sc_reported_getaggregate)


local function sc_reported_getproaggregate ( nm )

    local storeId = tonumber(nm:readString())
    local Userid = tonumber(nm:readString())
    if DY_DATA.StoreData.ProAggregateList == nil then DY_DATA.StoreData.ProAggregateList = {} end

    local n = tonumber(nm:readString())
    local ProAggregateList = {}
    for i=1,n do
        local ProAggregate = {
            name = nm:readString(),
            price = nm:readString(),
            value = nm:readString(),
            number = nm:readString(),
            per = nm:readString(),
        }
        table.insert(ProAggregateList, ProAggregate) 

    end
    print("ProAggregateList is :" .. JSON:encode(ProAggregateList))
    DY_DATA.StoreData.ProAggregateList = ProAggregateList
end
NW.regist("REPORTED.SC.GETSUPGETPROAGGREGATE",sc_reported_getproaggregate)
--     local act_form = nm:readString()
--     local act_calendar = nm:readString()
--     local act_goal = nm:readString()
--     local goal_sale = nm:readString()
--     local goal_exppeople = nm:readString()
--     local goal_expvolume = nm:readString()
--     local goal_people = nm:readString()
--     local product_info = nm:readString()
--     local selling_point = nm:readString()
--     local words = nm:readString()
--     local sales_technique = nm:readString()
--     local rule_att = nm:readString()
--     local rule_face = nm:readString()
--     local rule_sch = nm:readString()
--     local rule_photo = nm:readString()
--     local rule_data = nm:readString()
--     local rule_leave = nm:readString()
--     local rule_sale = nm:readString()
--     local rule_plan = nm:readString()
--     local info_wages = nm:readString()
--     local info_reward = nm:readString()
--     local info_supcontact = nm:readString()
--     local info_salecontact = nm:readString()
--     local info_procontact = nm:readString()
--     local icon = nm:readString()


local function  sc_project_getproinfor(nm)
    local id = tonumber(nm:readString())
    local name = nm:readString()
    local type = nm:readString()
    local brand = nm:readString()

    local n = tonumber(nm:readString())
    local List = {}
    for i=1,n do
        name = nm:readString()
        table.insert(List, name)
    end
    
    local Project = DY_DATA.ProjectList[id]
    if Project == nil then
        Project = { id = id, name = name}
        DY_DATA.ProjectList[id] = Project
    end

    Project.Info = {
    type = type,
    brand = brand,
    ProductList = List,
    act_form = nm:readString(),
    act_calendar = nm:readString(),
    act_goal = nm:readString(),
    goal_sale = nm:readString(),
    goal_exppeople = nm:readString(),
    goal_expvolume = nm:readString(),
    goal_people = nm:readString(),
    product_info = nm:readString(),
    selling_point = nm:readString(),
    words = nm:readString(),
    sales_technique = nm:readString(),
    rule_att = nm:readString(),
    rule_face = nm:readString(),
    rule_sch = nm:readString(),
    rule_photo = nm:readString(),
    rule_data = nm:readString(),
    rule_leave = nm:readString(),
    rule_sale = nm:readString(),
    rule_plan = nm:readString(),
    info_wages = nm:readString(),
    info_reward = nm:readString(),
    info_procontact = nm:readString(),
    icon = nm:readString(),
}

end

NW.regist("PROJECT.SC.GETPROINFOR", sc_project_getproinfor)

local function sc_project_getstoreinfor(nm)
    local projectId = tonumber(nm:readString())

    local Project = DY_DATA.ProjectList[projectId]
    local StoreList = Project.StoreList
    
    local id = tonumber(nm:readString())
    local Store
    for _,v in ipairs(StoreList) do
        if v.id == id then Store = v end
    end
    local name = nm:readString()
    local address = nm:readString()
    local superId = tonumber(nm:readString())

    local supcontact = nm:readString()
    local salecontact = nm:readString()
    local n = tonumber(nm:readString())
    print(n)
    local WorkDays = {}
    for i=1,n do
        local day = nm:readString()
        local week = tonumber(nm:readString())
        week = week == 0 and 7 or week
        local time = nm:readString()
        table.insert(WorkDays, { day = day, week = week, time = time })
    end
    Store.Info = {
    supcontact = supcontact,
    salecontact = salecontact,
    address = address,
    superId = superId,
    WorkDays = WorkDays,
}
print(JSON:encode(WorkDays))
end
NW.regist("PROJECT.SC.GETSTOREINFOR", sc_project_getstoreinfor)

local function sc_work_getproject(nm)


    if DY_DATA.User.limit == 1 then
        local List = DY_DATA.ProjectList
        print("Length of List is" .. #List)
        local n = tonumber(nm:readString())
        for i=1,n do
            local id = tonumber(nm:readString())
            if List[id] == nil then List[id] = {} end
            List[id].id = id
            List[id].name = nm:readString()
            local icon = nm:readString()
            List[id].icon = icon ~= nil and icon ~= "nil" and icon..".png" or nil
        end
        DY_DATA.ProjectList = List
        DY_DATA.get_project_list(true)
    else    
        local List = DY_DATA.ProjectList
        local n = tonumber(nm:readString())
        print("Length of List is" .. #List)
        for i=1,n do
            local idstring = nm:readString()
            print(idstring)
            local id = tonumber(idstring)
            if List[id] == nil then List[id] = {} end
            List[id].id = id
            List[id].name = nm:readString()
            List[id].brandnum = tonumber(nm:readString())
            local icon = nm:readString()
            List[id].icon = icon ~= nil and icon ~= "nil" and icon..".png" or nil
        end
        DY_DATA.AttendanceList = List
        DY_DATA.ProjectList = List
        DY_DATA.SchProjectList = List
        DY_DATA.get_project_list(true)
        DY_DATA.get_attendance_list(true)
        DY_DATA.get_schproject_list(true)
        -- print(JSON:encode(DY_DATA.get_project_list(false)))
        -- print(JSON:encode(DY_DATA.get_attendance_list(false)))
        -- print(JSON:encode(DY_DATA.get_schproject_list(false)))
    end

end
NW.regist("WORK.SC.GETPROJECT", sc_work_getproject)

local function sc_work_getstore(nm)
    local n = tonumber(nm:readString())
    local List = {}
    for i=1,n do
        local projectId = tonumber(nm:readString())
        local Project = DY_DATA.ProjectList[projectId]
        local StoreList = Project.StoreList
        if StoreList == nil then
            StoreList = {}
            Project.StoreList = StoreList
        end

        local Info = {
        id = tonumber(nm:readString()),
        name = nm:readString(),
        projectId = projectId,
    }
    local icon = nm:readString()
    Info.icon = icon ~= nil and icon ~= "nil" and icon..".png" or nil
    Info.state = tonumber(nm:readString())
    table.insert(StoreList, Info)
end

end
NW.regist("WORK.SC.GETSTORE", sc_work_getstore)

local function sc_work_getstartdate(nm)
    local WorkDay = {}
    for i=1,7 do
        local str = nm:readString()
        local ymd = str:splitn("-")
        table.insert(WorkDay, { year = ymd[1], month = ymd[2], day = ymd[3]})
    end
    DY_DATA.WorkDay = WorkDay

end
NW.regist("WORK.SC.GETSTARTDATE", sc_work_getstartdate)


local function sc_work_getproduct(nm)
    local n = tonumber(nm:readString())
    for i=1,n do
        local projectId = tonumber(nm:readString())
        local Project = DY_DATA.ProjectList[projectId]
        if Project ~= nil then
            local ProductList = Project.ProductList
            if ProductList == nil then
                ProductList = {}
                Project.ProductList = ProductList
            end

            local Info = {
            id = tonumber(nm:readString()),
            projectId = projectId,
            name = nm:readString(),
            unit = nm:readString(),
        }
        local icon = nm:readString()
        Info.icon = icon ~= nil and icon ~= "nil" and icon..".png" or nil

        table.insert(ProductList, Info)
    end
end
end
NW.regist("WORK.SC.GETPRODUCT", sc_work_getproduct)


local function sc_work_getmater(nm)

    local projectId = tonumber(nm:readString())
    local Project = DY_DATA.SchProjectList[projectId]
    local n = tonumber(nm:readString())
    local MaterList = Project.MaterList
    if MaterList == nil then
        MaterList = {}
        Project.MaterList = MaterList
    end
    for i=1,n do
        local Info = {
        id = tonumber(nm:readString()),
        name = nm:readString(),
    }
    table.insert(MaterList, Info)
end
print("MaterList is : " .. JSON:encode(MaterList))
end
NW.regist("WORK.SC.GETMATER", sc_work_getmater)

local function sc_work_getcomlist(nm)
    -- local n = tonumber(nm:readString())
    -- local ProjectList = DY_DATA.SchProjectList
    -- for i=1,n do
    --     local projectId = tonumber(nm:readString())
    --     local id = tonumber(nm:readString())
    --     local name = nm:readString()
    --     local m, List = tonumber(nm:readString()), {}
    --     for j=1,m do
    --         local tid = tonumber(nm:readString())
    --         local tName = nm:readString()
    --         table.insert(List, {id = tid, name = tName})
    --     end
    --     local icon = nm:readString()
    --     icon = icon ~= nil and icon ~= "nil" and icon..".png" or nil

    --     local Project = ProjectList[projectId]
    --     if Project == nil then return end
    --     if Project.CompeteProductList == nil then Project.CompeteProductList = {} end
    --     local ComList = Project.CompeteProductList

    --     table.insert(ComList, {id = id, projectId = projectId, name = name, icon = icon, TitleList = List})
    local projectId = tonumber(nm:readString())
    local Project = DY_DATA.SchProjectList[projectId]
    if Project == nil then return end

    if Project.ComList == nil then Project.ComList = {} end
    local ComList = Project.ComList

    local n = tonumber(nm:readString())
    for i=1,n do

        local id = tonumber(nm:readString())
        local name = nm:readString()
        local icon = nm:readString()
        icon = icon ~= nil and icon ~= "nil" and icon..".png" or nil
        table.insert(ComList, {id = id, projectId = projectId, name = name, icon = icon})
    end
    print("WORK.SC.GETCOMLIST-- ComList is : " .. JSON:encode(ComList))
end
NW.regist("WORK.SC.GETCOMLIST", sc_work_getcomlist)

local function sc_work_getsales(nm)
    local n = tonumber(nm:readString())
    print("<color=#0f0>WORK.SC.GETSALES-----------</color>"..n)
    local List = DY_DATA.PromoterList
    if List == nil then List = {} DY_DATA.PromoterList = List end
    for i=1,n do
        local id = tonumber(nm:readString())
        local name = nm:readString()
        local state = tonumber(nm:readString())
        List[id] = {id = id, name = name, state = state}
    end

    DY_DATA.get_promoter_List(true)
    print("PromoterList is:"  .. JSON:encode(DY_DATA.get_promoter_List()))
end
NW.regist("WORK.SC.GETSALES", sc_work_getsales)

local function sc_work_getmechanism(nm)
    local projectId = tonumber(nm:readString())
    local Project = DY_DATA.SchProjectList[projectId]
    if Project == nil then return end

    if Project.MechanismList == nil then Project.MechanismList = {} end
    local MechanismList = Project.MechanismList

    local n = tonumber(nm:readString())
    for i=1, n do
        local id = tonumber(nm:readString())
        local name = nm:readString()
        table.insert(MechanismList, {id = id, name = name})
    end
end
NW.regist("WORK.SC.GETMECHANISM", sc_work_getmechanism)

local function sc_work_getassignment(nm)
    print("<color=#0f0>WORK.SC.GETASSIGNMENT</color>")
    local projectId = tonumber(nm:readString())
    local storeId = tonumber(nm:readString())
    local n = tonumber(nm:readString())
    local TaskList = {}
    for i=1,n do
        local id = tonumber(nm:readString())
        local personId = tonumber(nm:readString())
        local name = nm:readString()
        local starttime = nm:readString()
        local endtime = nm:readString()
        if TaskList[starttime.."_"..endtime] == nil then 
        TaskList[starttime.."_"..endtime] = {} 
    end

    local Info = { 
    taskId = id, 
    id = personId,
    name = name, 
    projectId = projectId, 
    storeId = storeId,
    starttime = starttime,
    endtime = endtime,
}
table.insert(TaskList[starttime.."_"..endtime], Info)
end
print("<color=#0f0>WORK.SC.GETASSIGNMENT</color>",projectId, storeId, n, JSON:encode(TaskList))
local ProjectList = DY_DATA.ProjectList
if ProjectList[projectId] == nil or ProjectList[projectId].StoreList == nil then return end
local StoreList = ProjectList[projectId].StoreList
local Store = DY_DATA.get_store(StoreList, storeId)
if Store == nil then return end

local List = {}
for k,v in pairs(TaskList) do
    local Time = k:split('_')
    table.insert(List, { starttime = Time[1], endtime = Time[2], PersonList = v})
end
Store.TaskList = List
end
NW.regist("WORK.SC.GETASSIGNMENT",sc_work_getassignment)

local function sc_work_getsellphoto(nm)
    local projectId = tonumber(nm:readString())
    local Project = DY_DATA.SchProjectList[projectId]
    if Project == nil then return end

    if Project.SellPhoto == nil then Project.SellPhoto = {} end
    local SellPhoto = Project.SellPhoto

    local n = tonumber(nm:readString())
    for i=1, n do
        local id = tonumber(nm:readString())
        local name = nm:readString()
        local state = tonumber(nm:readString())
        table.insert(SellPhoto, {id = id, name = name,state = state})
    end
    Project.SellPhoto = SellPhoto
    print("SellPhoto is :" .. JSON:encode(SellPhoto))
    print("SellPhoto is :" .. JSON:encode(Project.SellPhoto))
end
NW.regist("WORK.SC.GETSELLPHOTO", sc_work_getsellphoto)

local function sc_work_getbrand(nm)
    if DY_DATA.BrandList == nil then DY_DATA.BrandList = {} end
    local BrandList = {}
    local n = tonumber(nm:readString())
    print ("BrandList Length is :" .. n)
    for i=1, n do
        local id = tonumber(nm:readString())
        local name = nm:readString()
        table.insert(BrandList, {id = id, name = name})
    end
    print("BrandList is :"  .. JSON:encode(BrandList))
    DY_DATA.BrandList = BrandList
end
NW.regist("WORK.SC.GETBRAND", sc_work_getbrand)

local function sc_message_getlower(nm)
    local n = tonumber(nm:readString())
    local List = {}
    for i=1,n do
        local limit = tonumber(nm:readString())
        local id = tonumber(nm:readString())
        local People = {
        id = id,
        limit = limit,
        name = nm:readString(),
        phone = nm:readString(),
        qq = nm:readString(),
        wechat = nm:readString(), 
        email = nm:readString(),
    }
    local icon = nm:readString()
    People.icon = icon ~= nil and icon ~= "nil" and icon..".png" or nil
    List[id] = People
end
DY_DATA.LowerList = List
    -- DY_DATA.get_lower_list(true)
end
NW.regist("MESSAGE.SC.GETLOWER", sc_message_getlower)

local function sc_message_getmessagelist(nm)
    if nm==nil then
       print("<color=#00ff00>回调信息列表MESSAGE.SC.GETMESSAGELIST-no-</color>")
   else
    print("<color=#00ff00>回调信息列表MESSAGE.SC.GETMESSAGELIST-yes-</color>")
end
local n = tonumber(nm:readString())
local List = {}
for i=1,n do
    local Msg = {
    id = tonumber(nm:readString()),
            type = tonumber(nm:readString()), -- (1, 项目， 2， 文本， 3 请假)
            people = tonumber(nm:readString()),
            context = nm:readString(),
            day = nm:readString(),
            time = nm:readString(),
            state = nm:readString(), -- （1 未读，2 已读）
        }
       -- print("<color=#00ff00>回调信息列表MESSAGE.SC.GETMESSAGELIST-yes--"..Msg.time.."-</color>")
       table.insert(List, Msg)
   end
   DY_DATA.MsgList = List
end
NW.regist("MESSAGE.SC.GETMESSAGELIST", sc_message_getmessagelist)
--------------------zzg-add----------------------------------



-- local function sc_message_Issued(nm)
--     -- local state = tostring(nm:readString())
--     -- if state ==1 then
--     --     local  n = tostring(nm:readString())
--     --     for i=1,n do

--     --         local  List = {}

--     --     end
--     -- end

-- end
-- NW.regist("MESSAGE.SC.ISSUED", sc_message_Issued)

local function sc_message_sendmessage(nm)
print("发送信息 注册回调")
if nm ~=nil then
    DY_DATA.SENDMESSAGESTATE=tonumber(nm:readString())
    print("发送信息 注册回调值"..DY_DATA.SENDMESSAGESTATE)
end
end
NW.regist("MESSAGE.SC.SENDMESSAGE", sc_message_sendmessage)
----------------------------zzg-end-------------------------------------------


local function sc_districtmag_get_dmag(nm)

end
NW.regist("DISTRICTMAG.SC.GET_DMAG", sc_districtmag_get_dmag)


return P