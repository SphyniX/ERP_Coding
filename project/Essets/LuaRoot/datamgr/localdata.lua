--
-- @file    datamgr/localdata.lua
-- @authors xing weizhen (xingweizhen@rongygame.com)
-- @date    2015-04-10 20:38:26
-- @desc    本地测试数据
--

local UI_DATA = MERequire "datamgr/uidata.lua"
local DY_DATA = MERequire "datamgr/dydata.lua"
local DY_TIMER = _G.PKG["libmgr/dytimer"]
local GirlDEF = _G.DEF.Girl
local WeaponDEF = _G.DEF.Weapon
local SkillDEF = _G.DEF.Skill
local DressDEF = _G.DEF.Dress
local ItemDEF = _G.DEF.Item
local TaskDEF = _G.DEF.Task
local EquipDEF = _G.DEF.Equip
local MineDEF = _G.DEF.Mine
local ExploitMineDEF = _G.DEF.ExploitMine
local RobberyMineDEF = _G.DEF.RobberyMine
local GoodsDEF = _G.DEF.Goods

DY_DATA.User.id = "0"
DY_DATA.User.name = "小张"
DY_DATA.User.adress = "上海市徐汇区漕宝路401号"
DY_DATA.User.phone = "13012345678"
DY_DATA.User.wechat = "13324rew"
DY_DATA.User.qq = "3256573"
DY_DATA.User.email = "asdfqwex@163.com"
DY_DATA.User.height = 180
DY_DATA.User.weight = 180

DY_DATA.AttendanceList = {
	{ id = 1, name = "光明"},
	{ id = 2, name = "双汇"},
	{ id = 3, name = "锐澳"},
}

DY_DATA.SupervisorList = {
	{
		name = "张三",
		sex = "男",
		state = "已签约", --已签约
		phone = "13011432132",
		wechat = "13324rew",
		qq = "3256573",
		email = "asdfqwex@163.com",
	},
	{
		name = "李四",
		sex = "男",
		state = "已签约", --已签约
		phone = "13012345678",
		wechat = "1232134",
		qq = "12341234",
		email = "12341234@163.com",
	},
	{
		name = "王毛",
		sex = "男",
		state = "已签约", --已签约
		phone = "13012345678",
		wechat = "1232134",
		qq = "12341234",
		email = "12341234@163.com",
	},

}

DY_DATA.MsgList = {
	{
		title = "督导",
		context = "今天去沃尔玛开会",
		time = "12:00",

	},
	{
		title = "三全",
		context = "今天去沃尔玛开会",
		time = "12:00",
		
	},
	{
		title = "三全",
		context = "今天去沃尔玛开会",
		time = "12:00",
		
	}
}

DY_DATA.ProjectList = {
	{
		id = 1,
		name = "三全",
		state = 1,
		StoreList = {
			{id = 1,name = "家乐福",},
			{id = 2, name = "家乐福",},
		},
		ProductList = {
			{id = 1, name = "产品1"},
			{id = 2, name = "产品2"},
			{id = 3, name = "产品3"},
		},
	},
	{
		id = 2,
		name = "光明",
		state = 1,
		StoreList = {
			{id = 1, name = "家乐福",},
			{id = 2, name = "家乐福",},
		},
		ProductList = {
			{id = 1, name = "产品1"},
			{id = 2, name = "产品2"},
			{id = 3, name = "产品3"},
		},
	},
	{
		id = 3,
		name = "大王",
		state = 2,
		StoreList = {
			{id = 1, name = "家乐福",},
			{id = 2, name = "家乐福",},
		},
		ProductList = {
			{id = 1, name = "产品1"},
			{id = 2, name = "产品2"},
			{id = 3, name = "产品3"},
		},
	}
}

DY_DATA.StoreList = {
	{id = 1, name = "家乐福",},
	{id = 2, name = "家乐福",},
}

DY_DATA.ProductList = {
	{id = 1, name = "产品1"},
	{id = 2, name = "产品2"},
	{id = 3, name = "产品3"},
}

print("<i>已载入本地测试数据</i>")

return {}
