-- File Name : localize/zhcn.lua

return {
    comma = "，",
    Sex = {[1] = "男", [2] = "女",},
    Week = {
        [0] = "星期天",
        [1] = "星期一",
        [2] = "星期二",
        [3] = "星期三",
        [4] = "星期四",
        [5] = "星期五",
        [6] = "星期六",
        [0] = "星期天",
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
    SurperState = {
        [1] = "审核中",
        [2] = "",
        [3] = "",
        [4] = "已签约",


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

    -- 一页精要

    -- 关于瑞切
    fmtInfo = [[<size=28>公司简介</size>

    上海瑞切广告有限公司，秉承“Go after for more!只为更好”的企业核心理念，专注快消十余年，将丰富的活动执行经验与成熟的营销理念传播于消费者，成为消费者与产品的桥梁。自成立以来，已迅速成为行业工作者与品牌客户们共同青睐的品牌市场营销服务商。相信Richer(瑞切)，to be richer! 选择Richer，to get richer”，Richer将带给您不一样的兼职经历，同时给您难忘的职业生涯！

<size=28>服务品牌</size>

%s

<size=28>服务产品</size>

%s]],
    
    -- 活动形式
    fmtType = [[<size=28>活动形式 : </size>

%s

<size=28>活动档期 : </size>

%s

<size=28>活动目标 : </size>

%s

1、 目标销量

%s

2、 体验人次

%s

3、 体验量

%s]],
    -- 产品知识
    fmtProduct = [[<size=28>产品知识 : </size>

%s

<size=28>产品卖点 : </size>

%s]],
    -- 标准话术
    fmtWord = [[<size=28>标准话术 : </size>

%s

<size=28>销售技巧 : </size>

%s]],

    -- 工作流程
    fmtWork = [[<size=28>1、提前到岗</size>

%s

<size=28>2、仪容仪表</size>

%s

<size=28>3、清点物料</size>

上班
活动准备，清点物料，统一摆放规定位置

下班
活动准备，清点物料，盘点物料库存数量

<size=28>4、上传进度: </size>

%s

<size=28>照片要求</size>

%s

<size=28>数据要求</size>

%s

<size=28>5、离岗报备</size>

%s

<size=28>6、产品售卖</size>

%s

<size=28>7、准备下</size>

%s]],
    -- 管理规范
    fmtRole = [[<size=28>卖场规范:</size>

1、不准在卖场偷盗商品及赠品

2、不准穿着工服及工作时间内购买商品及试饮别人家及自己家试吃品

3、不准带着私人物品进入卖场内

4、不准在卖场对顾客或者别家促销员发生语言冲突及肢体动作

5、不准在卖场玩手机及给手机充电

如果违反以上规定造成的罚款由促销员自行承担，不以工资形式抵扣罚款，需要促销员发生违规事件后自行承担现金

<size=28>瑞切制度:</size>

1、工资架构设定 : %s

2、薪资发放制度 : 为了更快更准确的将您的酬劳到达您的手中，请在个人信息中，准确上传您本人的身份证+银行卡（交通或工商银行卡）照片，我们将在次月25-30日期间发放工资。

3、奖惩制度规定 : %s]],

    -- 联系方式

    fmtContext1 = [[您的督导与您所在门店长促是您最好的工作伙伴。有解决难题或者困扰可直接联系他们。

<size=28>1、活动联系人</size>

督导联系方式 : %s

长促联系方式 : %s]],

 fmtContext2 = [[

项目负责人联系方式 : %s

<size=28>2、问题咨询</size>

工资咨询热线    021-54268100*1012

手机终端咨询热线   18918101246

<size=28>3、Richer官方微信：Richer_com</size>]],

}