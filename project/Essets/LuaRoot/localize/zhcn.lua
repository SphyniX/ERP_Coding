-- File Name : localize/zhcn.lua

return {
    comma = "，",
    Sex = {[1] = "男", [2] = "女",},
    Week = {
        [1] = "星期一",
        [2] = "星期二",
        [3] = "星期三",
        [4] = "星期四",
        [5] = "星期五",
        [6] = "星期六",
        [7] = "星期天",
    },
    WeekNum = {
        [1] = "一",
        [2] = "二",
        [3] = "三",
        [4] = "四",
        [5] = "五",
        [6] = "六",
        [7] = "七",
    },

    Reason = {
        [1] = "事假",
        [2] = "病假",
        [3] = "其他",
        [4] = "用餐",
        [5] = "休息",
        [6] = "临时调岗",
    },
    LeaveState = {
        [1] = "审核中",
        [2] = "审核成功",
        [3] = "审核失败",
    },

    SuppliesType = {
        [1] = "完好",
        [2] = "补给",
        [3] = "修理",
        [4] = "更换",
        [5] = "其他",
    },
    fmtDownloadException = "下载异常： %s",
    tipNotImplemented = "~功能未实现~",
    tipExepction = "异常",
-- MessageBox
    nameConfirm = "确定",
    nameCancel = "取消",
    nameAll = "全部",
    nameQuality = "品质",
    nameSystemTip = "系统提示",
    nameNeed = "需要",

-- net/login
    tipConnecting = "正在连接...",
    tipLogingIn = "正在登录...",
    tipEnterGame = "正在进入...",
    tipConnectTimeout = "连接超时",
    tipConnectFailure = "连接失败",
    fmtUnkonwError = "未知错误:%d",
    tipNewVersion = "发现新版本，要更新吗？",
    tipNonForceVersion = "(本次更新为非强制更新，取消将不下载更新资源直接进入游戏)",
    tipIsForceVersion = "(本次更新为强制更新，取消将关闭游戏客户端)",
    tipAskUpdateViaCarrierDataNetwork = "您当前不是在WIFI网络环境下，确认要更新吗？(推荐在WIFI网络下进行更新)",
    tipPleaseReloginLong = "数据连接异常，请重新登录",
    nameOffline = "离线提示",
    TIPDropGame = {
        "账号已在别处登录，你被强制下线", 
        "你已经下线。",
        "账号异常。",
        "服务器维护中。",
    },

-- WNDLaunch
    nameLogin = "登录",
    nameSwitchAcc = "切换",
    nameRegist = "注册帐号",
    nameBindAcc = "绑定帐号",
    fmtCurrAcc = "当前帐号：%s",
    tipUnLogined = "未登录",

    -- Time
    tipTime = {"秒","分","小时",},
    NumToWeekDays = { "一", "二", "三", "四", "五", "六", "日"},

}