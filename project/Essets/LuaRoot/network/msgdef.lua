--
-- @file    network/msgdef.lua
-- @authors xing weizhen (xingweizhen@rongygame.com)
-- @date    2015-11-18 16:54:01
-- @desc    网络消息ID定义
--

local P = {}

do
    -- local CS_BASE, SC_BASE = 0, 5000
    local BASE = 0
    
    local Funcs = { }
    local ATTENCE_BASE = BASE + 0
    function  Funcs.attence_cs()
        local ID_BASE = ATTENCE_BASE + 10
        return {
            GETWORK = ID_BASE + 1,
            UPWORK = ID_BASE + 2,
            BEDEMOBILIZED = ID_BASE + 3,
            FUGANG = ID_BASE + 4,
            LEAVE = ID_BASE + 5,
            GETLEAVELIST = ID_BASE + 6,
            GETTIME = ID_BASE + 7,
            VERIFYLATLNG = ID_BASE + 8, 
            PHUNCH = ID_BASE + 9,
            VERIFY = ID_BASE + 10,
            GETATTENCE = ID_BASE + 11,
            GETPROJECT = ID_BASE + 12,
            GETATTSTORE = ID_BASE + 13,
            GETCITY = ID_BASE + 14,
            GETKAO = ID_BASE + 15,
        }
    end
    function  Funcs.attence_sc()
        local ID_BASE = ATTENCE_BASE + 510
        return {
            GETWORK = ID_BASE + 1,
            UPWORK = ID_BASE + 2,
            BEDEMOBILIZED = ID_BASE + 3,
            FUGANG = ID_BASE + 4,
            LEAVE = ID_BASE + 5,
            GETLEAVELIST = ID_BASE + 6,
            GETTIME = ID_BASE + 7,
            VERIFYLATLNG = ID_BASE + 8, 
            PHUNCH = ID_BASE + 9,
            VERIFY = ID_BASE + 10,
            GETATTENCE = ID_BASE + 11,
            GETPROJECT = ID_BASE + 12,
            GETATTSTORE = ID_BASE + 13,
            GETCITY = ID_BASE + 14,
            GETKAO = ID_BASE + 15,
        }
    end

    local USER_BASE = BASE + 1000
    function Funcs.user_cs()
        local ID_BASE = USER_BASE + 10  
          
        return {
            UPDATENAME = ID_BASE + 1,
            UPDATETOCH = ID_BASE + 2,
            VALPWD = ID_BASE + 3,
            UPDATE = ID_BASE + 4,
            UPDATEPHONE = ID_BASE + 5,
            UPDATEINF = ID_BASE + 6,
            UPDATEIDNUM = ID_BASE + 7,
            UPDATECARDNO = ID_BASE + 8,
            GETTYPE = ID_BASE + 9,
            FEEDVACK = ID_BASE + 10,
            CONTRACT = ID_BASE + 11,
            GETINFOR = ID_BASE + 12,
            GETUSERINFOR = ID_BASE + 13,
            GETSUPERLIST = ID_BASE + 14,
        }
    end
    function Funcs.user_sc()
        local ID_BASE = USER_BASE + 510
        return {
            UPDATENAME = ID_BASE + 1,
            UPDATETOCH = ID_BASE + 2,
            VALPWD = ID_BASE + 3,
            UPDATE = ID_BASE + 4,
            UPDATEPHONE = ID_BASE + 5,
            UPDATEINF = ID_BASE + 6,
            UPDATEIDNUM = ID_BASE + 7,
            UPDATECARDNO = ID_BASE + 8,
            GETTYPE = ID_BASE + 9,
            FEEDVACK = ID_BASE + 10,
            CONTRACT = ID_BASE + 11,
            GETINFOR = ID_BASE + 12,
            GETUSERINFOR = ID_BASE + 13,
            GETSUPERLIST = ID_BASE + 14,
        }
    end

    local REPORTED_BASE = BASE + 2000
    function Funcs.reported_cs()
        local ID_BASE = REPORTED_BASE + 10
        return {
            REPORTEDPRO = ID_BASE + 1,
            GETREP = ID_BASE + 2,
            GETME = ID_BASE + 3,
            GETCOM = ID_BASE + 4,
            AUD = ID_BASE + 5,
            COM = ID_BASE + 6,
            GETMECHANRE = ID_BASE + 7,
            GETINTELLIGENCE = ID_BASE + 8,
            GETATTINFOR = ID_BASE + 9,
            GETPROJECT = ID_BASE + 10,
            GETSTORE = ID_BASE + 11,
            GETSTOREINFOR = ID_BASE + 12,
            GETPRODUCT = ID_BASE + 13,
            GETSAMPLE = ID_BASE + 14,
            GETGIFT = ID_BASE + 15,
            GETSUPMATTER = ID_BASE + 16,
            GETSUPMECHANISM = ID_BASE + 17,
            GETSUPGIFTRE = ID_BASE + 18,
            GETSUPSAMPLERE = ID_BASE + 19,
            GETSUPGETFEEDBACK = ID_BASE + 20,
            GETSUPGETPHOTO = ID_BASE + 21,
            GETSUPGETCOMPETING = ID_BASE + 22,
            GETSUPGETAGGREGATE = ID_BASE + 23,
            GETSUPGETPROAGGREGATE = ID_BASE + 24,
            GETSUPGUPLOADPHOTO = ID_BASE + 25,
            GETSUPUPLOADFEEDBACK = ID_BASE + 26,
            GETSUPUPLOADCOMANALYSIS = ID_BASE + 27,
        }
    end

    function Funcs.reported_sc()
        local ID_BASE = REPORTED_BASE + 510
        return {
            REPORTEDPRO = ID_BASE + 1,
            GETREP = ID_BASE + 2,
            GETME = ID_BASE + 3,
            GETCOM = ID_BASE + 4,
            AUD = ID_BASE + 5,
            COM = ID_BASE + 6,
            GETMECHANRE = ID_BASE + 7,
            GETINTELLIGENCE = ID_BASE + 8,
            GETATTINFOR = ID_BASE + 9,
            GETPROJECT = ID_BASE + 10,
            GETSTORE = ID_BASE + 11,
            GETSTOREINFOR = ID_BASE + 12,
            GETPRODUCT = ID_BASE + 13,
            GETSAMPLE = ID_BASE + 14,
            GETGIFT = ID_BASE + 15,
            GETSUPMATTER = ID_BASE + 16,
            GETSUPMECHANISM = ID_BASE + 17,
            GETSUPGIFTRE = ID_BASE + 18,
            GETSUPSAMPLERE = ID_BASE + 19,
            GETSUPGETFEEDBACK = ID_BASE + 20,
            GETSUPGETPHOTO = ID_BASE + 21,
            GETSUPGETCOMPETING = ID_BASE + 22,
            GETSUPGETAGGREGATE = ID_BASE + 23,
            GETSUPGETPROAGGREGATE = ID_BASE + 24,
            GETSUPGUPLOADPHOTO = ID_BASE + 25,
            GETSUPUPLOADFEEDBACK = ID_BASE + 26,
            GETSUPUPLOADCOMANALYSIS = ID_BASE + 27,
        }
    end

    local PROJECT_BASE = BASE + 3000
    function Funcs.project_cs()
        local ID_BASE = PROJECT_BASE + 10
        return {
            GETPROINFOR = ID_BASE + 1,
            GETSTOREINFOR = ID_BASE + 2,
            GETSAlESWORKFLOW = ID_BASE + 3,
            GETSUPWORKFLOW = ID_BASE + 4,
        }
    end

    function Funcs.project_sc()
        local ID_BASE = PROJECT_BASE + 510 
        return {
            GETPROINFOR = ID_BASE + 1,
            GETSTOREINFOR = ID_BASE + 2,
            GETSAlESWORKFLOW = ID_BASE + 3,
            GETSUPWORKFLOW = ID_BASE + 3,
        }
    end

    local WORK_BASE = 4000
    function Funcs.work_cs()
        local ID_BASE = WORK_BASE + 10
        return {
            GETPROJECT = ID_BASE + 1,
            GETSTORE = ID_BASE + 2,
            GETSTARTDATE = ID_BASE + 3,
            ISSUED = ID_BASE + 4,
            GETPRODUCT = ID_BASE + 5,
            GETMATER = ID_BASE + 6,
            GETCOMLIST = ID_BASE + 7,
            GETSALES = ID_BASE + 8,
            GETMECHANISM = ID_BASE + 9,
            GETASSIGNMENT = ID_BASE + 10,
            UPDATEASS = ID_BASE + 11,
            DELETEASS = ID_BASE + 12,
            GETSELLPHOTO = ID_BASE + 13,
            GETBRAND = ID_BASE + 14,
        }
    end
    function Funcs.work_sc()
        local ID_BASE = WORK_BASE + 510
        return {
            GETPROJECT = ID_BASE + 1,
            GETSTORE = ID_BASE + 2,
            GETSTARTDATE = ID_BASE + 3,
            ISSUED = ID_BASE + 4,
            GETPRODUCT = ID_BASE + 5,
            GETMATER = ID_BASE + 6,
            GETCOMLIST = ID_BASE + 7,
            GETSALES = ID_BASE + 8,
            GETMECHANISM = ID_BASE + 9,
            GETASSIGNMENT = ID_BASE + 10,
            UPDATEASS = ID_BASE + 11,
            DELETEASS = ID_BASE + 12,
            GETSELLPHOTO = ID_BASE + 13,
            GETBRAND = ID_BASE + 14,
        }
    end

    local DISTRICTMAG_BASE = 5000
    function Funcs.districtmag_cs()
        local ID_BASE = DISTRICTMAG_BASE + 10
        return {
            GET_DMAG = ID_BASE + 1,
            GET_REGCITIES = ID_BASE + 2,
            GET_REGCITIES = ID_BASE + 3,
            GET_CITYSTORE = ID_BASE + 4,
            DATA_TASK_ASSIGNMENT = ID_BASE + 5,
            GET_TASK_ASSIGNMENT = ID_BASE + 6,
            GET_TASK_ASSIGNMENT = ID_BASE + 7,
            DATA_FOLLOW = ID_BASE + 8,
            GET_COWA = ID_BASE + 9,
            GET_COMAX = ID_BASE + 10,
            GET_PICTURE = ID_BASE + 11,
            GET_USERINFO = ID_BASE + 12,
        }
    end

    function Funcs.districtmag_sc()
        local ID_BASE = DISTRICTMAG_BASE + 510
        return {
            GET_DMAG = ID_BASE + 1,
            GET_REGCITIES = ID_BASE + 2,
            GET_REGCITIES = ID_BASE + 3,
            GET_CITYSTORE = ID_BASE + 4,
            DATA_TASK_ASSIGNMENT = ID_BASE + 5,
            GET_TASK_ASSIGNMENT = ID_BASE + 6,
            GET_TASK_ASSIGNMENT = ID_BASE + 7,
            DATA_FOLLOW = ID_BASE + 8,
            GET_COWA = ID_BASE + 9,
            GET_COMAX = ID_BASE + 10,
            GET_PICTURE = ID_BASE + 11,
            GET_USERINFO = ID_BASE + 12,
        }
    end    

    local MESSAGE_BASE = 6000
    function Funcs.message_cs()
        local ID_BASE = MESSAGE_BASE + 10
        return {
            GETLOWER = ID_BASE + 1,
            SENDMESSAGE = ID_BASE + 2,
            GETMESSAGELIST = ID_BASE + 3,
            UPSTATU = ID_BASE + 4,
            DELETE = ID_BASE + 5,
            LEAVEAUDIT = ID_BASE + 6,
        }
    end
    function Funcs.message_sc()
        local ID_BASE = MESSAGE_BASE + 510
        return {
            GETLOWER = ID_BASE + 1,
            SENDMESSAGE = ID_BASE + 2,
            GETMESSAGELIST = ID_BASE + 3,
            UPSTATU = ID_BASE + 4,
            DELETE = ID_BASE + 5,
            LEAVEAUDIT = ID_BASE + 6,
        }
    end

    local COMMON_BASE = 7000
    function Funcs.common_cs()
        local ID_BASE = COMMON_BASE + 10
        return {
            HEART = ID_BASE + 1,
            LOGOUT = ID_BASE + 2,
        }
    end
    function Funcs.common_sc()
        local ID_BASE = COMMON_BASE + 510
        return {
            HEART = ID_BASE + 1,
            LOGOUT = ID_BASE + 2,
        }
    end

    local LOGIN_BASE = 8000
    function Funcs.login_cs()
        local ID_BASE = LOGIN_BASE + 10
        return {
            LOGIN = ID_BASE + 1,
        }
    end
    function Funcs.login_sc()
        local ID_BASE = LOGIN_BASE + 510
        return {
            LOGIN = ID_BASE + 1,
        } 
    end

    local function make_msg_map(module)
        local func = module:lower()
        local CS, SC = Funcs[func.."_cs"](), Funcs[func.."_sc"]()
        for k,v in pairs(CS) do
            P[module..".CS."..k] = v
        end

        for k,v in pairs(SC) do
            P[module..".SC."..k] = v
        end        
    end

    make_msg_map("COMMON")
    make_msg_map("LOGIN")
    make_msg_map("ATTENCE")
    make_msg_map("USER")
    make_msg_map("REPORTED")
    make_msg_map("PROJECT")
    make_msg_map("WORK")
    make_msg_map("DISTRICTMAG")
    make_msg_map("MESSAGE")

    P.get_msg_name = function (code)
        for k,v in pairs(P) do
            if v == code then
                return k.."("..code..")"
            end
        end
        return "Unkown code: "..code
    end

    getmetatable(import("clientlib.net.NetMsg")).__tostring = function (nm)
        local nmType = nm.type
        local siz = nmType > 5000 and nm.readSize or nm.writeSize
        return string.format("[%s %d bytes]", P.get_msg_name(nmType), siz)
    end
end

return P
