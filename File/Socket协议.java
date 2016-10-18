/**
	 * 考勤
	 * <br />[0,1000)
	 */
	short Attence = 0{
		/** 考勤消息 [10-100) */
		short CS_Attence = Attence + 10{
			/**
			* 获取今日任务 <br />
			* code：{@value}
			* 
			* <pre>
			* U32:促销员编号
			* </pre>
			*/
			short CS_Attence_getwork = CS_Attence + 1;
			/**
			* 打卡 <br />
			* code：{@value}
			* 
			* <pre>
			* U32:任务编号或活动编号
			* U32:类型编号（1 上班，2 下班，3 巡店）
			* </pre>
			*/
			short CS_Attence_upwork = CS_Attence + 2;
			/**
			* 离岗 <br />
			* code：{@value}
			* 
			* <pre>
			* U32:任务编号
			* String:类型
			* string:事由
			* </pre>
			*/
			short CS_Attence_bedemobilized = CS_Attence + 3;
			/**
			* 复岗 <br />
			* code：{@value}
			* 
			* <pre>
			* U32:离岗编号
			* </pre>
			*/
			short CS_Attence_fugang = CS_Attence + 4;
			/**
			* 请假 <br />
			* code：{@value}
			* 
			* <pre>
			* U32:用户编号
			* string:开始时间
			* string:结束时间
			* string:事因
			* </pre>
			*/
			short CS_Attence_levea = CS_Attence + 5;
			/**
			* 获取请假信息 <br />
			* code：{@value}
			* 
			* <pre>
			* U32:用户编号
			* </pre>
			*/
			short CS_Attence_Getlevealist = CS_Attence + 6;
			/**
			* 获取当前时间 <br />
			* code：{@value}
			*
			*/
			short CS_Attence_Gettime = CS_Attence + 7;
			/**
			* 验证经纬度 <br />
			* code：{@value}
			*
			* <pre>
			* U32:任务编号
			* String:经纬度（前面纬度，后面经度中间用逗号隔开)
			* </pre>
			*
			*/
			short CS_Attence_Verifylatlng = CS_Attence + 8;
			/**
			* 督导打卡 <br />
			* code：{@value}
			*
			* <pre>
			* U32:门店编号
			* </pre>
			*
			*/
			short CS_Attence_phunch = CS_Attence + 9;
			/**
			* 督导验证经纬度 <br />
			* code：{@value}
			*
			* <pre>
			* U32:门店编号
			* String:经纬度（前面纬度，后面经度中间用逗号隔开)
			* </pre>
			*
			*/
			short CS_Attence_Verify = CS_Attence + 10;

		}
		/** 考勤消息返回 [510-600) */
		short SC_Attence = Attence + 510{
			/**
			* 获取今日任务 返回 <br />
			* code：{@value}
			* 
			* <pre>
			* [
			* String:任务编号
			* string:项目名称
			* string:督导姓名
			* String:上班时间
			* String:下班时间
			* String:项目图片
			* ]
			* </pre>
			*/
			short SC_Attence_getwork = SC_Attence + 1;
			/**
			* 打卡 返回 <br />
			* code：{@value}
			* 
			* <pre>
			* U32:1 成功
			* </pre>
			*/
			short SC_Attence_upwork = SC_Attence + 2;
			/**
			* 离岗 返回 <br />
			* code：{@value}
			* 
			* <pre>
			* U32:1 成功
			* U32:离岗编号
			* </pre>
			*/
			short SC_Attence_bedemobilized = SC_Attence + 3;
			/**
			* 复岗 返回 <br />
			* code：{@value}
			* 
			* <pre>
			* U32:1 成功
			* </pre>
			*/
			short SC_Attence_fugang = SC_Attence + 4;
			/**
			* 请假 返回 <br />
			* code：{@value}
			* 
			* <pre>
			* U32:1 成功
			* </pre>
			*/
			short SC_Attence_levea = SC_Attence + 5;
			/**
			* 获取请假信息 返回 <br />
			* code：{@value}
			* 
			* <pre>
			* [
			* U32:请假编号
			* string:开始到结束的时间
			* string:事因
			* U32:状态(1 审核中，2 审核成功，3 审核失败)
			* String:请假时间
			* ]
			* </pre>
			*/
			short SC_Attence_Getlevealist = SC_Attence + 6;
			/**
			* 获取当前时间 返回 <br />
			* code：{@value}
			* 
			* <pre>
			* string:当前时间（24小时制）
			* </pre>
			*/
			short SC_Attence_Gettime = SC_Attence + 7;
			/**
			* 验证经纬度 返回 <br />
			* code：{@value}
			* 
			* <pre>
			* string:1 为可以打卡，2为不可以
			* </pre>
			*/
			short SC_Attence_Verifylatlng = SC_Attence + 8;
			/**
			* 督导打卡 返回 <br />
			* code：{@value}
			* 
			* <pre>
			* string:状态码
			* </pre>
			*/
			short SC_Attence_phunch = SC_Attence + 9;
			/**
			* 督导验证经纬度 返回 <br />
			* code：{@value}
			* 
			* <pre>
			* string:1 为可以打卡，2为不可以
			* </pre>
			*/
			short SC_Attence_Verify = SC_Attence + 10;
		}
	}
	/**
	 * 用户信息
	 * <br />[1000,2000)
	 */
	short UserInformation = 1000{
		/** 用户信息 [1010-1100) */
		short CS_UserInformation = UserInformation + 10{
			/**
			* 修改姓名 <br />
			* code：{@value}
			* 
			* <pre>
			* U32:用户编号
			* string:姓名
			* </pre>
			*/
			short CS_UserInformation_updatename = CS_UserInformation + 1;
			/**
			* 修改联系方式 <br />
			* code：{@value}
			* 
			* <pre>
			* U32:用户编号
			* string:微信
			* string:QQ
			* string:邮箱
			* </pre>
			*/
			short CS_UserInformation_updatetoch = CS_UserInformation + 2;
			/**
			* 验证密码 <br />
			* code：{@value}
			* 
			* <pre>
			* U32:用户编号
			* string:原密码
			* </pre>
			*/
			short CS_UserInformation_valpwd = CS_UserInformation + 3;
			/**
			* 修改密码 <br />
			* code：{@value}
			* 
			* <pre>
			* U32:用户编号
			* string:新密码
			* </pre>
			*/
			short CS_UserInformation_update = CS_UserInformation + 4;
			/**
			* 修改手机号 <br />
			* code：{@value}
			* 
			* <pre>
			* U32:用户编号
			* string:手机号
			* </pre>
			*/
			short CS_UserInformation_updatephone = CS_UserInformation + 5;
			/**
			* 修改其他信息 <br />
			* code：{@value}
			* 
			* <pre>
			* U32:用户编号
			* U32:性别(0 男,1 女)
			* U32:年龄
			* U32:体重(kg)
			* U32:身高(cm)
			* </pre>
			*/
			short CS_UserInformation_updateinf = CS_UserInformation + 6;
			/**
			* 修改身份证号 <br />
			* code：{@value}
			* 
			* <pre>
			* U32:用户编号
			* string:身份证号
			* </pre>
			*/
			short CS_UserInformation_updateIDnum = CS_UserInformation + 7;
			/**
			* 修改银行卡号 <br />
			* code：{@value}
			* 
			* <pre>
			* U32:用户编号
			* string:银行卡号
			* </pre>
			*/
			short CS_UserInformation_updatecardno = CS_UserInformation + 8;
			/**
			* 获取反馈类型 <br />
			* code：{@value}
			*/
			short CS_UserInformation_gettype = CS_UserInformation + 9;
			/**
			* 提交反馈信息 <br />
			* code：{@value}
			* 
			* <pre>
			* U32:用户编号
			* U32:类型编号
			* string:内容
			* </pre>
			*/
			short CS_UserInformation_feedvack = CS_UserInformation + 10;
			/**
			* 添加督导 <br />
			* code：{@value}
			* 
			* <pre>
			* U32:促销员编号
			* U32:督导编号
			* String:督导姓名
			* </pre>
			*/
			short CS_UserInformation_Contract = CS_UserInformation + 11;
			/**
			* 查看个人信息 <br />
			* code：{@value}
			* 
			* <pre>
			* U32:促销员编号
			* </pre>
			*/
			short CS_UserInformation_Getinfor = CS_UserInformation + 12;
			/**
			* 获取个人信息 <br />
			* code：{@value}
			* 
			* <pre>
			* U32:用户编号
			* </pre>
			*
			*/
			short CS_UserInformation_Getuserinfor = CS_UserInformation + 13;
			/**
			* 获取督导信息 <br />
			* code：{@value}
			* 
			* <pre>
			* U32:用户编号
			* </pre>
			*
			*/
			short CS_UserInformation_Getsuperrlist = CS_UserInformation + 14;

		}
		/** 用户消息返回 [1510-1600) */
		short SC_UserInformation = UserInformation + 510{
			/**
			* 修改姓名 返回 <br />
			* code：{@value}
			* 
			* <pre>
			* [
			* U32:1 成功
			* ]
			* </pre>
			*/
			short SC_UserInformation_updatename = SC_UserInformation + 1;
			/**
			* 修改联系方式 返回 <br />
			* code：{@value}
			* 
			* <pre>
			* U32:1 成功
			* </pre>
			*/
			short SC_UserInformation_updatetoch = SC_UserInformation + 2;
			/**
			* 验证密码 返回 <br />
			* code：{@value}
			* 
			* <pre>
			* U32:1 成功 1002 密码错误
			* </pre>
			*/
			short SC_UserInformation_valpwd = SC_UserInformation + 3;
			/**
			* 修改密码 返回 <br />
			* code：{@value}
			* 
			* <pre>
			* U32:1 成功
			* </pre>
			*/
			short SC_UserInformation_update = SC_UserInformation + 4;
			/**
			* 修改手机号 返回 <br />
			* code：{@value}
			* 
			* <pre>
			* U32:1 成功
			* </pre>
			*/
			short SC_UserInformation_updatephone = SC_UserInformation + 5;
			/**
			* 修改其他信息 返回 <br />
			* code：{@value}
			* 
			* <pre>
			* U32:1 成功
			* </pre>
			*/
			short SC_UserInformation_updateinf = SC_UserInformation + 6;
			/**
			* 修改身份证号 返回 <br />
			* code：{@value}
			* 
			* <pre>
			* U32:1 成功 1009 身份证号已存在
			* </pre>
			*/
			short SC_UserInformation_updateIDnum = SC_UserInformation + 7;
			/**
			* 修改银行卡号 返回 <br />
			* code：{@value}
			* 
			* <pre>
			* U32:1 成功 1010 银行卡号已存在
			* </pre>
			*/
			short SC_UserInformation_updatecardno = SC_UserInformation + 8;
			/**
			* 获取反馈类型 返回 <br />
			* code：{@value}
			* 
			* <pre>
			* [
			* U32:反馈类型编号
			* string:反馈类型名称
			* ]
			* </pre>
			*/
			short SC_UserInformation_gettype = SC_UserInformation + 9;
			/**
			* 提交反馈信息 返回 <br />
			* code：{@value}
			* 
			* <pre>
			* U32:1 成功
			* </pre>
			*/
			short SC_UserInformation_feedvack = SC_UserInformation + 10;
			/**
			* 添加督导 返回 <br />
			* code：{@value}
			* 
			* <pre>
			* U32:1 成功,1003 邀请码不正确,1004 姓名和邀请码不匹配
			* </pre>
			*/
			short SC_UserInformation_Contract = SC_UserInformation + 11;
			/**
			* 查看个人信息 返回 <br />
			* code：{@value}
			* 
			* <pre>
			* String:姓名
			* U32:工号
			* U32:性别（1 男,2 女）
			* U32:年龄
			* U32:身高
			* U32:体重
			* String:手机号
			* String:QQ
			* String:微信
			* String:邮箱
			* Sreing:照片
			* </pre>
			*/
			short SC_UserInformation_Getinfor = SC_UserInformation + 12;
			/**
			* 获取个人信息 返回 <br />
			* code：{@value}
			* 
			* <pre>
			* String:1 成功，继续读下面的，
			* String:权限
			* 1{
			* String:手机号
			* String:姓名
			* String:性别（1 男,2 女）
			* String:年龄
			* String:身高
			* String:体重
			* String:QQ
			* String:微信
			* String:邮箱
			* String:银行卡号
			* String:身份证号
			* String:状态码 -- 1 下班， 2， 上班中， 3 离岗
			* String:任务编号或离岗编号
			* String:城市编号
			* String:头像
			* String:身份证正
			* String:身份证反
			* String:身份证手持
			* String:银行卡正
			* String:银行卡反
			* }
			* 不是1{
			* String:手机号
			* String:姓名
			* String:QQ
			* String:微信
			* String:邮箱
			* String:城市
			* String:头像
			* }
			* </pre>
			*/
			short SC_UserInformation_Getuserinfor = SC_UserInformation + 13;
			/**
			* 获取督导列表 返回 <br />
			* code：{@value}
			* 
			* <pre>
			* String:数量
			* [
			* String:促销员编号
			* String:督导编号
			* String:督导姓名
			* ]
			* </pre>
			*/
			short SC_UserInformation_Getsuperrlist = SC_UserInformation + 14;
		}
	}
	/**
	 * 进度
	 * <br />[2000,3000)
	 */
	short Reported = 2000{
		/** 进度信息 [2010-2100) */
		short CS_Reported = Reported + 10{
			/**
			* 上报进度 <br />
			* code：{@value}
			* 
			* <pre>
			* U32:用户编号
			* U32:门店编号
			* U32:数量
			* [
			* U32:产品编号
			* U32:产品促销价
			* U32:产品销售量
			* U32:产品原价
			* ]
			* U32:数量
			* [
			* U32:促销机制编号
			* string:促销机制内容
			* ]
			* U32:数量
			* [
			* U32:标题编号
			* string:标题内容
			* ]
			* String:反馈描述
			* </pre>
			*/
			short CS_Reported_Reportedpro = CS_Reported + 1;
			/**
			* 获取某天进度信息 <br />
			* code：{@value}
			* 
			* <pre>
			* U32:门店编号
			* String:日期（格式:yyyy/MM/dd）
			* </pre>
			*/
			short CS_Reported_GetRep = CS_Reported + 2;
			/**
			* 获0取某天物料信息 <br />
			* 
			* <pre>
			* U32:门店编号
			* String:日期（格式:yyyy/MM/dd）
			* </pre>
			*/
			short CS_Reported_Getme = CS_Reported + 3;
			/**
			* 获取竞品信息 <br />
			* code：{@value}
			* 
			* <pre>
			* U32:竞品编号
			* String:日期（格式:yyyy/MM/dd）
			* </pre>                                                                      
			*/
			short CS_Reported_Getcom = CS_Reported + 4;
			/**
			* 审核 <br />
			* code：{@value}
			* 
			* <pre>
			* U32:进度编号
			* </pre>
			*/
			short CS_Reported_Aud = CS_Reported + 5;
			/**
			* 竞品分析 <br />
			* code：{@value}
			* 
			* <pre>
			* U32:门店编号
			* [
			* U32:竞品标题编号
			* string:值
			* ]
			* </pre>
			*/
			short CS_Reported_Com = CS_Reported + 6;
			/**
			* 获取促销机制 <br />
			* code：{@value}
			* 
			* <pre>
			* U32:门店编号
			* string:时间
			* </pre>
			*/
			short CS_Reported_GetMechanRe = CS_Reported + 7;
			/**
			* 获取情报 <br />
			* code：{@value}
			* 
			* <pre>
			* U32:门店编号
			* string:时间
			* </pre>
			*/
			short CS_Reported_GetIntelligence = CS_Reported + 8;
			/**
			* 获取考勤信息 <br />
			* code：{@value}
			* 
			* <pre>
			* U32:门店编号
			* string:时间
			* </pre>
			*/
			short CS_Reported_GetAttinfor = CS_Reported + 9;
			/**
			* 获取进度项目--促销员<br />
			* code：{@value}
			* 
			* <pre>
			* U32: 用户编号
			* </pre>
			*/
			short CS_Reported_GetProject = CS_Reported + 10
			/**
			* 获取进度店铺--促销员<br />
			* code：{@value}
			* 
			* <pre>
			* U32: 项目编号
			* U32: 用户编号
			* </pre>
			*/
			short CS_Reported_Getstore = CS_Reported + 11
			/**
			* 获取进度门店信息--促销员 返回 <br />
			* code：{@value}
			* 
			* <pre>
			* U32: 项目编号
			* U32: 门店编号
			* U32：促销员编号
			* </pre>
			*/
			short CS_Reported_Getstoinfor = CS_Reported + 12
		}
		/** 进度消息返回 [2510-2600) */
		short SC_Reported = Reported + 510{
			/**
			* 上报进度 返回 <br />
			* code：{@value}
			* 
			* <pre>
			* U32:1 成功
			* </pre>
			*/
			short SC_Reported_Reportedpro = SC_Reported + 1;
			/**
			* 获取某天进度信息 返回 <br />
			* code：{@value}
			* 
			* <pre>
			* string:门店编号
			* string:时间
			* string:数量
			* [
			* string:产品编号
			* string:产品名称
			* string:促销员姓名
			* string:销售额
			* string:销量
			* string:促销价
			* string:原价
			* string:产品图片
			* ]
			* </pre>
			*/
			short SC_Reported_GetRep = SC_Reported + 2;
			/**
			* 获取某天物料信息 返回 <br />
			* code：{@value}
			* 
			* <pre>
			* string:门店编号
			* string:促销员姓名
			* string:物料编号
			* string:物料名字
			* U32:物料状态（1 完好 2 补给 3 修理 4 更换）
			* string:物料备注
			* string:照片
			* </pre>
			*/
			short SC_Reported_Getme = SC_Reported + 3
			/**
			* 获取竞品信息 返回 <br />
			* code：{@value}
			* 
			* <pre>
			* string:竞品编号
			* string:时间
			* string:数量
			* [
			* string:促销员编号
			* string:促销员姓名
			* string:标题编号
			* string:内容
			* ]
			* </pre>
			*/
			short SC_Reported_Getcom = SC_Reported + 4
			/**
			* 审核 返回 <br />
			* code：{@value}
			* 
			* <pre>
			* U32: 1 成功
			* </pre>
			*/
			short SC_Reported_Aud = SC_Reported + 5
			/**
			* 竞品分析 返回 <br />
			* code：{@value}
			* 
			* <pre>
			* U32: 1 成功
			* </pre>
			*/
			short SC_Reported_Com = SC_Reported + 6
			/**
			* 获取促销机制 返回 <br />
			* code：{@value}
			* 
			* <pre>
			* string:门店编号
			* string:时间
			* string:数量
			* [
			* string:促销员编号
			* string:促销员姓名
			* string:促销机制编号
			* string:促销机制名称
			* string:促销机制内容
			* ]
			* </pre>
			*/
			short SC_Reported_GetMechanRe = SC_Reported + 7
			/**
			* 获取情报 返回 <br />
			* code：{@value}
			* 
			* <pre>
			* string:门店编号
			* string:时间
			* string:数量
			* [
			* string:促销员姓名
			* string:情报内容
			* ]
			* </pre>
			*/
			short SC_Reported_GetIntelligence = SC_Reported + 8
			/**
			* 获取考勤信息 返回 <br />
			* code：{@value}
			* 
			* <pre>
			* string:门店编号
			* string:时间
			* string:数量
			* [
			* string:促销员编号
			* string:上班时间
			* string:下班时间
			* string:上班门头照
			* string:上班门头照
			* string:竞品1照
			* string:竞品2照
			* string:竞品3照
			* string:竞品4照
			* ]
			* </pre>
			*/
			short SC_Reported_GetAttinfor = SC_Reported + 9
			/**
			* 获取进度项目--促销员 返回 <br />
			* code：{@value}
			* 
			* <pre>
			* [
			* U32:项目编号
			* String:项目名称
			* String:促销类型
			* String:项目图片
			* ]
			* </pre>
			*/
			short SC_Reported_Getproject = SC_Reported + 10
			/**
			* 获取项目店铺--促销员 返回 <br />
			* code：{@value}
			* 
			* <pre>
			* [
			* String:项目编号
			* U32:活动编号
			* String:门店名称
			* String:门店照片
			* String:打卡状态码 1为已巡店 2为未巡店
			* ]
			* </pre>
			*/
			short SC_Reported_GetStore = SC_Reported + 11
			/**
			* 获取进度门店信息--促销员 返回 <br />
			* code：{@value}
			* 
			* <pre>
			* string:项目编号 
			* String:门店编号
			* String:门店名称
			* String:门店地址
			* String:督导编号
			* String:日期
			* String:星期
			* String:工作时间
			* </pre>
			*/
			short SC_Project_Getstoinfor = SC_Reported + 12
		}
	}
	/**
	 * 项目
	 * <br />[3000,4000)
	 */
	short Project = 3000{
		/** 项目信息 [3010-3100) */
		short CS_Project = Project + 10{
			/**
			* 获取项目信息 返回 <br />
			* code：{@value}
			* 
			* <pre>
			* U32: 项目编号
			* </pre>
			*/
			short CS_Project_Getproinfor = CS_Project + 1
			/**
			* 获取门店信息 返回 <br />
			* code：{@value}
			* 
			* <pre>
			* U32: 项目编号
			* U32: 门店编号
			* U32：促销员编号
			* </pre>
			*/
			short CS_Project_Getstoinfor = CS_Project + 2
		}
		/** 项目消息返回 [3510-3600) */
		short SC_Project = Project + 510{
			/**
			* 获取项目信息 返回 <br />
			* code：{@value}
			* 
			* <pre>
			* U32:项目编号
			* String:项目名称
			* String:促销类型
			* [
			* String:产品名称
			* ]
			* String:促销话术
			* String:促销主题
			* String:促销政策
			* String:项目图片
			* </pre>
			*/
			short SC_Project_Getproinfor = CS_Project + 1
			/**
			* 获取门店信息 返回 <br />
			* code：{@value}
			* 
			* <pre>
			* string:项目编号 
			* String:门店编号
			* String:门店名称
			* String:门店地址
			* String:督导编号
			* String:数量
			* [
			* String:日期
			* String:星期
			* String:工作时间
			* ]
			* </pre>
			*/
			short SC_Project_Getstoinfor = CS_Project + 2

		}
	}
	/**
	 * 工作
	 * <br />[4000,5000)
	 */
	short Work = 4000{
		/** 工作信息 [4010-4100) */
		short CS_Work = Work + 10{
			/**
			* 获取项目<br />
			* code：{@value}
			* 
			* <pre>
			* U32: 用户编号
			* </pre>
			*/
			short CS_Work_Getproject = CS_Work + 1
			/**
			* 获取店铺<br />
			* code：{@value}
			* 
			* <pre>
			* U32: 项目编号
			* U32: 用户编号
			* </pre>
			*/
			short CS_Work_GetStore = CS_Work + 2
			/**
			* 获取工作时间<br />
			* code：{@value}
			* 
			*/
			short CS_Work_Getstartdate = CS_Work + 3
			/**
			* 分配下周工作<br />
			* code：{@value}
			* 
			* <pre>
			* U32: 店铺编号
			* U32：数量
			* [
			* U32: 用户编号
			* U32:数量
			*   [
			*    String:开始日期时间
			*    String:结束日期时间
			*   ]
			* ]
			* </pre>
			*/
			short CS_Work_Issued = CS_Work + 4
			/**
			* 获取产品<br />
			* code：{@value}
			* 
			* <pre>
			* U32: 项目编号
			* </pre>
			*/
			short CS_Work_Getproduct = CS_Work + 5
			/**
			* 获取物料<br />
			* code：{@value}
			* 
			* <pre>
			* U32: 项目编号
			* </pre>
			*/
			short CS_Work_GetMater = CS_Work + 6
			/**
			* 获取竞品列表<br />
			* code：{@value}
			* 
			* <pre>
			* U32: 项目编号
			* </pre>
			*/
			short CS_Work_GetComList = CS_Work + 7
			/**
			*  获取促销员列表--工作<br />
			* code：{@value}
			* 
			* <pre>
			* U32: 督导编号
			* </pre>
			*/
			short CS_Work_Getsales = CS_Work + 8
			/**
			* 获取促销机制列表<br />
			* code：{@value}
			* 
			* <pre>
			* U32: 项目编号
			* </pre>
			*/
			short CS_Work_GetMechanism = CS_Work + 9
			/**
			* 获取任务列表<br />
			* code：{@value}
			* 
			* <pre>
			* U32: 店铺编号
			* </pre>
			*/
			short CS_Work_GetAssignment = CS_Work + 10
			/**
			* 修改任务<br />
			* code：{@value}
			* 
			* <pre>
			* U32: 任务编号
			* U32: 促销员编号
			* String: 开始时间
			* String: 结束时间
			* </pre>
			*/
			short CS_Work_UpdateAss = CS_Work + 11
			/**
			* 删除任务<br />
			* code：{@value}
			* 
			* <pre>
			* U32: 任务编号
			* </pre>
			*/
			short CS_Work_DeleteAss = CS_Work + 12
		}
		/** 工作消息返回 [4510-4600) */
		short SC_Work = Work + 510{
			/**
			* 获取项目 返回 <br />
			* code：{@value}
			* 
			* <pre>
			* [
			* U32:项目编号
			* String:项目名称
			* String:项目图片
			* ]
			* </pre>
			*/
			short SC_Work_Getproject = CS_Work + 1
			/**
			* 获取店铺 返回 <br />
			* code：{@value}
			* 
			* <pre>
			* [
			* String:项目编号
			* U32:活动编号
			* String:门店名称
			* String:门店照片
			* String:打卡状态码 1为已巡店 2为未巡店
			* ]
			* </pre>
			*/
			short SC_Work_GetStore = CS_Work + 2
			/**
			* 获取工作时间 返回 <br />
			* code：{@value}
			* 
			* <pre>
			* [
			* String:日期列表七个，从周四到周三
			* ]
			* </pre>
			*/
			short SC_Work_Getstartdate = CS_Work + 3
			/**
			* 分配下周工作 返回 <br />
			* code：{@value}
			* 
			* <pre>
			* U32:1 成功
			* </pre>
			*/
			short SC_Work_Issued = CS_Work + 4
			/**
			* 获取产品列表 返回 <br />
			* code：{@value}
			* 
			* <pre>
			* N:数量
			* [
			* String:项目编号
			* String:产品编号
			* String:产品名称
			* String:产品单位
			* String:产品图片
			* ]
			* </pre>
			*/
			short SC_Work_Getproduct = CS_Work + 5
			/**
			* 获取物料列表 返回 <br />
			* code：{@value}
			* 
			* <pre>
			* N:数量
			* [
			* String:项目编号
			* String:物料编号
			* String:物料名称
			* ]
			* </pre>
			*/
			short SC_Work_GetMater = CS_Work + 6
			/**
			* 获取竞品列表 返回 <br />
			* code：{@value}
			* 
			* <pre>
			* N:数量
			* [                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                           
			* String:项目编号
			* String:竞品编号
			* String:竞品名称
			* String:标题数量
			* [
			* String:标题编号
			* String:标题名字
			* ]
			* String:竞品图片
			* ]
			* </pre>
			*/
			short SC_Work_GetComList = CS_Work + 7
			/**
			* 获取促销员列表--工作 返回 <br />
			* code：{@value}
			* 
			* <pre>
			* N:数量
			* [                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                           
			* String:用户编号
			* String:用户姓名
			* String:状态（1 为可以分配，2 为不可分配）
			* ]
			* </pre>
			*/
			short SC_Work_Getsales = CS_Work + 8
			/**
			* 获取促销机制列表 返回 <br />
			* code：{@value}
			* 
			* <pre>
			* String:项目编号
			* N:数量
			* [                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                
			* String:促销机制编号
			* String:促销机制名称
			* ]
			* </pre>
			*/
			short SC_Work_GetMechanism = CS_Work + 9
			/**
			* 获取任务列表 返回 <br />
			* code：{@value}
			* 
			* <pre>
			* String:项目编号
			* String:店铺编号
			* N:数量
			* [
			* String:任务编号
			* String:促销员编号
			* String:促销员姓名
			* String:开始时间
			* String:结束时间
			* ]
			* </pre>
			*/
			short SC_Work_GetAssignment = CS_Work + 10
			/**
			* 修改任务 返回 <br />
			* code：{@value}
			* 
			* <pre>
			* String:状态码
			* </pre>
			*/
			short SC_Work_UpdateAss = CS_Work + 11
			/**
			* 删除任务 返回 <br />
			* code：{@value}
			* 
			* <pre>
			* String:状态码
			* </pre>
			*/
			short SC_Work_DeleteAss = CS_Work + 12
		}
	}


	short DISTRICTMAG = 5000{
	    /** 负责人消息请求 [5000-5100) */
		short CS_DISTRICTMAG = DISTRICTMAG + 0{
	        /**
			  * 获取项目信息<br />
		      * code：{@value}
		      * 
		      * <pre>
		      * U32:用户编号
		      * </pre>
		      **/
	          short CS_DISTRICTMAG_GET_DMAG = CS_DISTRICTMAG + 1;

	        /**
	          *获取省份城市<br />
	          *code：{@value}
		      * 
	         **/
	          short CS_DISTRICTMAG_GET_REGCITIES=CS_DISTRICTMAG+2;

	        /**
	          *获取城市督导<br />
	          *code：{@value} 
		      * 
		      * <pre>
		      * U32:用户编号
		      * </pre>
	          */
	          short CS_DISTRICTMAG_GET_REGCITIES=CS_DISTRICTMAG+3;

	        /**
	          *获取城市门店<br />
	          *code：{@value}
		      * 
		      * <pre>
		      * U32:城市编号
		      * U32:项目编号
		      * </pre>
	          */
	          short CS_DISTRICTMAG_GET_CITYSTORE=CS_DISTRICTMAG+4;

	        /**
	          *区域活动分派<br />
	          *code：{@value}
		      * 
		      * <pre>
		      * U32:项目编号
		      * U32:督导编号
		      * [
		      * U32:门店编号
		      * ]
		      * </pre>
	          */
	          short CS_DISTRICTMAG_DATA_TASK_ASSIGNMENT=CS_DISTRICTMAG+5;

	        /**
	          *产品进度-区域负责人<br />
	          *code：{@value}
		      * 
		      * <pre>
		      * U32:用户编号
		      * U32:项目编号
		      * U32:城市编号 可空 为空时查询该区域所有销售数据
		      * U32:时间判断值  1:前七天 2:本月度 3:本季度 4:本年度
		      * U32:当前显示页数
		      * </pre>
	          */
	          short CS_DISTRICTMAG_GET_SCHEDULE=CS_DISTRICTMAG+6;


	        /**
	          *产品进度-项目负责人<br />
	          *code：{@value}
		      * 
		      * <pre>
		      * U32:项目编号
		      * U32:城市编号 可空 为空时查询全国该项目所有销售数据
		      * U32:时间判断值  1:前七天 2:本月度 3:本季度 4:本年度
		      * U32:当前显示页数
		      * </pre>
	          */
	          short CS_DISTRICTMAG_GET_SCHEDULESS=CS_DISTRICTMAG+7;

	        /**
	          *关注店铺<br />
	          *code：{@value}
		      * 
		      * <pre>
		      * U32:用户编号
		      * U32:门店编号
		      * U32:操作判断值  1:关注门店 2:取消关注
		      * </pre>
	          */
	          short CS_DISTRICTMAG_DATA_FOLLOW=CS_DISTRICTMAG+8;

	        /**
	          *考勤信息-督导<br />
	          *code：{@value}
		      * 
		      * <pre>
		      * U32:用户编号
		      * String:考勤时间 默认查询当日考勤
		      * </pre>
	          */
	          short CS_DISTRICTMAG_GET_COWA=CS_DISTRICTMAG+9;

	        /**
	          *考勤信息-促销员<br />
	          *code：{@value}
		      * 
		      * <pre>
		      * U32:门店编号
		      * String:考勤时间 默认查询当日考勤
		      * </pre>
	          */
	          short CS_DISTRICTMAG_GET_COWAX=CS_DISTRICTMAG+10;

            /**
	          *产品进度-关注<br />
	          *code：{@value}
		      * <pre>
		      * U32:用户ID
	       	  * U32:项目ID
		      * U32:时间判断值  1:前七天 2:本月度 3:本季度 4:本年度
		      * </pre>
	          */
	          short CS_DISTRICTMAG_GET_FOLLOWSE=CS_DISTRICTMAG+11;
		}

	    /** 负责人消息返回 [5500-5600) */
		short SC_DISTRICTMAG = DISTRICTMAG + 500{
	        /**
	          *返回项目信息<br />
		      * code：{@value}
		      * 
		      * <pre>
		      * [
		      * U32:项目编号
		      * String:项目名称
		      * String:照片路径
		      * ]
		      * </pre>
		      */
	          short SC_DISTRICTMAG_GET_DMAG = SC_DISTRICTMAG + 1;

	        /**
	          *返回省份城市<br />
	          *code：{@value}
		      * 
		      * <pre>
		      * [
		      * U32:省份编号
		      * String:省份名称
		      * U32:城市编号
		      * String:城市名称
		      * ]
		      * </pre>
	          */
	          short SC_DISTRICTMAG_GET_REGCITIES=SC_DISTRICTMAG+2;

	        /**
	          *返回城市督导<br />
	          *code：{@value}
		      * 
		      * <pre>
		      * [
		      * U32:城市编号
		      * String:城市名称
		      * U32:督导编号
		      * String:督导姓名
		      * ]
		      * </pre>
	          */
	          short SC_DISTRICTMAG_GET_REGCITIES=SC_DISTRICTMAG+3;

	        /**
	          *返回城市门店<br />
	          *code：{@value}
		      * 
		      * <pre>
		      * [
		      * U32:城市编号
		      * String:城市名称
		      * U32:门店编号
		      * String:门店姓名
		      * String:门店图片路径
		      * ]
		      * </pre>
	          */
	          short SC_DISTRICTMAG_GET_CITYSTORE=SC_DISTRICTMAG+4;

	        /**
	          *返回活动分派操作结果<br />
	          *code：{@value}
		      * 
		      * <pre>
		      * U32:返回值 1 成功
		      * </pre>
	          */
	          short SC_DISTRICTMAG_DATA_TASK_ASSIGNMENT=SC_DISTRICTMAG+5;

	        /**
	          *返回产品进度-区域负责人<br />
	          *code：{@value}
		      * <pre>
		      * U32:进度数据总数
		      * U32:当前获取的进度数据总数
		      * [
		      * U32:门店编号
		      * String:门店名称
	       	  * U32:销售产品总数
		      * U32:销售总金额
		      * ]
		      * </pre>
	          */
	          short SC_DISTRICTMAG_GET_SCHEDULE=SC_DISTRICTMAG+6;

	        /**
	          *返回产品进度-项目负责人<br />
	          *code：{@value}
		      * <pre>
		      * U32:进度数据总数
		      * U32:当前获取的进度数据总数
		      * [
		      * U32:门店编号
		      * String:门店名称
	       	  * U32:销售产品总数
		      * U32:销售总金额
		      * ]
		      * </pre>
	          */
	          short SC_DISTRICTMAG_GET_SCHEDULESS=SC_DISTRICTMAG+7;

	        /**
	          *返回关注操作结果<br />
	          *code：{@value}
		      * 
		      * <pre>
		      * U32:返回值 1:成功
		      * </pre>
	          */
	          short SC_DISTRICTMAG_DATA_FOLLOW=SC_DISTRICTMAG+8;

	        /**
	          *返回考勤信息-督导<br />
	          *code：{@value}
		      * 
		      * <pre>
		      * [
		      * U32:用户编号
		      * U32:门店编号
		      * String:门店名称
		      * U32:打卡编号
		      * String:打卡时间
		      * ]
		      * </pre>
	          */
	          short SC_DISTRICTMAG_GET_COWA=SC_DISTRICTMAG+9;

	        /**
	          *返回考勤信息-促销员<br />
	          *code：{@value}
		      * 
	      	  * <pre>
	      	  * [
		      * U32:用户编号
		      * Sting:用户姓名
		      * U32:打卡编号
		      * String:打卡时间（上班）
		      * U32:打卡编号
		      * String:打卡时间（下班）
		      * ]
		      * </pre>
	          */
	          short SC_DISTRICTMAG_GET_COWAX=SC_DISTRICTMAG+10;

            /**
	          *返回产品进度-关注<br />
	          *code：{@value}
		      * <pre>
		      * [
		      * U32:门店编号
		      * String:门店名称
	       	  * U32:销售产品总数
		      * U32:销售总金额
		      * ]
		      * </pre>
	          */
	          short SC_DISTRICTMAG_GET_FOLLOWSE=SC_DISTRICTMAG+11;
		}
	}
	/**
	 * 消息
	 * <br />[6000,7000)
	 */
	short Message =6000{
		/** 消息接口 [6010-6100) */
		short CS_Message = Message + 10{
			 /**
	          *获取联系人<br />
	          *code：{@value}
		      * <pre>
		      * String:用户编号
		      * </pre>
	          */
	          short CS_Message_GetLower=CS_Message+1;、
	          /**
	          *发送文本消息<br />
	          *code：{@value}
		      * <pre>
		      * int:用户编号
		      * String:内容
		      * [
		      * 收件人编号
		      * ]
		      * </pre>
	          */
	          short CS_Message_SendMessage=CS_Message+2;
	          /**
	          *获取信息列表<br />
	          *code：{@value}
		      * <pre>
			  * String:用户编号
		      * </pre>
	          */
	          short CS_Message_Getmessagelist=CS_Message+3;
	          /**
	          *阅读<br />
	          *code：{@value}
		      * <pre>
			  * String:消息编号
		      * </pre>
	          */
	          short CS_Message_upstatu=CS_Message+4;
	          /**
	          *删除<br />
	          *code：{@value}
		      * <pre>
			  * String:消息编号
		      * </pre>
	          */
	          short CS_Message_delete=CS_Message+5;
	          /**
	          *请假审核<br />
	          *code：{@value}
		      * <pre>
			  * U32:请假编号
			  * U32:审核状态（2 通过，3 失败）
		      * </pre>
	          */
	          short CS_Message_LeaveAudit=CS_Message+6;

		}
		/** 消息消息返回 [6510-6600) */
		short SC_Message = Message + 510{
			/**
	          *返回 获取联系人<br />
	          *code：{@value}
		      * <pre>
		      * String:n
		      * [
		      * String:权限
		      * string:用户编号
		      * string:用户姓名
		      * string:用户手机号
		      * string:用户QQ
		      * string:用户微信
		      * string:用户Email
		      * String:联系人图片
		      * ]
		      * </pre>
	          */
	          short SC_Message_GetLower=SC_Message+1;
	          /**
	          *返回 发送文本<br />
	          *code：{@value}
		      * <pre>
		      * String:状态码
		      * </pre>
	          */
	          short SC_Message_GetLower=SC_Message+2;
	          /**
	          *返回 获取信息列表<br />
	          *code：{@value}
		      * <pre>
		      * String:数量
		      * [
		      * String:消息编号
		      * String:消息类型
		      * String:消息发送人
		      * String:消息内容
		      * String:消息日期
		      * String:消息时间
		      * String:消息状态（1 未读，2 已读）
		      * ]
		      * </pre>
	          */
	          short SC_Message_Getmessagelist=SC_Message+3;

	          /**
	          *返回 阅读<br />
	          *code：{@value}
		      * <pre>
		      * String:数量
		      * String:状态码 1为成功
		      * </pre>
	          */
	          short SC_Message_upstatu=SC_Message+4;

			  /**
	          *返回 删除<br />
	          *code：{@value}
		      * <pre>
		      * String:状态码 1为成功
		      * </pre>
	          */
	          short SC_Message_delete=SC_Message+5;

              /**
	          * 返回 请假审核<br />
	          *code：{@value}
		      * <pre>
		      * String:状态码
		      * </pre>
	          */
	          short SC_Message_LeaveAudit=SC_Message+6
			
	}
		/**
	 * 心跳
	 * <br />[7000,8000)
	 */
	short Heartbeat =7000{
		/** 心跳接口 [7010-7100) */
		short CS_Heartbeat = Heartbeat + 10{
			/**
			* 心跳 <br />
			* code：{@value}
			* 
			* <pre>
			* String:消息内容不管，有就行
			* </pre>
			*/
			short CS_Heartbeat_Heartbeat = CS_Heartbeat + 1
		}
		short SC_Heartbeat = Heartbeat + 510{
		}
	}
		/**
	 * 登录
	 * <br />[8000,9000)
	 */
	short Login =8000{
		/** 登录接口 [8010-8100) */
		short CS_Login = Login + 10{
			/**
			* 登录 <br />
			* code：{@value}
			* 
			* <pre>
			* U32:用户编号
			* </pre>
			*/
			short CS_Login_login = CS_Login + 1
		}
		short SC_Login = Login + 10{
			/**
			* 登录 返回 <br />
			* code：{@value}
			* 
			* <pre>
			* String:状态码
			* </pre>
			*/
			short SC_Login_login = SC_Login + 1
		}
	}

		/**
	 * 推送
	 * <br />[9000,10000)
	 */
	short Send =9000{
		short SC_Send = 9500 + 10{
			/**
			* 推送消息 返回 <br />
			* code：{@value}
			* 
			* <pre>
			* String:消息编号
		    * String:消息类型
		    * String:消息发送人
		    * String:消息内容
		    * String:消息日期
		    * String:消息时间
		    * String:消息状态（1 未读，2 已读）
			* </pre>
			*/
			short SC_Send_Sendmessage = SC_Send + 1
		}
		short SC_Send = 9500 + 10{
			/**
			* 更新请假信息 返回 <br />
			* code：{@value}
			* 
			* <pre>
			* U32:请假编号
			* U32:状态(1 审核中，2 审核成功，3 审核失败)
			* </pre>
			*/
			short SC_Send_updateleave = SC_Send + 2
		}
	}







    







    

    





























