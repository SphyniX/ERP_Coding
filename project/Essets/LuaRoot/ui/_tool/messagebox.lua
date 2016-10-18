-- File Name : ui/_tool/messagebox.lua

-- 消息框用法
-- 同时只会显示一个消息框，其他的消息框进入队列，当前一个关闭后，弹出下一个。

-- local MB = _G.UI.MessageBox
-- MB:make("默认", "确认框，只有一个按钮，无回调", false):show()
-- MB:make("选择", "两个个按钮\n【确认】有回调\n【取消】关闭窗口", true)
--   :set_event(function () print("confirm!!") end)
--   :show()

local libugui = require "libugui.cs"
local libunity = require "libunity.cs"
local Ref
local BoxQueue, ActivatedBox = _G.DEF.Queue:new(), nil
local do_popup_box

local function on_close(go)
    ActivatedBox = nil
    do_popup_box()
end

local function on_sub_subbtn_btncancel_click()
    if ActivatedBox.on_cancel then ActivatedBox.on_cancel() end    
    libugui.DOFade(Ref.root, "Out", on_close)
end

local function on_sub_subbtn_btnconfirm_click()
    if ActivatedBox.on_confirm then ActivatedBox.on_confirm() end    
    libugui.DOFade(Ref.root, "Out", on_close)
end

local function init_view()
    Ref.Sub.SubBtn.btnCancel.onAction = on_sub_subbtn_btncancel_click
    Ref.Sub.SubBtn.btnConfirm.onAction = on_sub_subbtn_btnconfirm_click
    --!*以上：自动注册的回调函数*--
end

local function init_logic()    
    local Ref_Sub = Ref.Sub
    
    libugui.SetText(Ref_Sub.SubBtn.btnConfirm, ActivatedBox.txtConfirm)
    libugui.SetText(Ref_Sub.SubBtn.btnCancel, ActivatedBox.txtCancel)

    Ref_Sub.lbTitle.text = ActivatedBox.title
    if ActivatedBox.content ~= nil then
        libunity.SetActive(Ref_Sub.lbContent, true)
        Ref_Sub.lbContent.text = ActivatedBox.content
    else
        libunity.SetActive(Ref_Sub.lbContent, false)
    end
    if ActivatedBox.image ~= nil then
        libunity.SetActive(Ref_Sub.spImage, true)
        Ref_Sub.spImage.spritePath = ActivatedBox.image;
    else
        libunity.SetActive(Ref_Sub.spImage, false)
    end
    libunity.SetActive(Ref_Sub.SubBtn.btnCancel, ActivatedBox.dual)
end

local function start(self)    
    if Ref == nil or Ref.root ~= self then
        Ref = libugui.GenLuaTable(self, "root")
        init_view()
    end
    init_logic()
end

do_popup_box = function ()    
    if Ref then libunity.Destroy(Ref.root) end
    ActivatedBox = BoxQueue:dequeue()
    if ActivatedBox then
        local UIMGR = _G.PKG["ui/uimgr"]
        start(UIMGR.create("UI/MessageBox", 21))
        libugui.DOFade(Ref.root, "In", nil, true)
    end
end

--=============================================================================

local OBJDEF = { }
OBJDEF.__index = OBJDEF

local function chk_args(self, method)
    if type(self) ~= 'table' or self.__index ~= OBJDEF then
      error(string.format("MessageBox:%s must be called in method format", method), 3)
   end
end

function OBJDEF:make(title, content, dual, image)
    chk_args(self, "make")

    local TEXT = _G.ENV.TEXT
    local self = {
        title = title, content = content, dual = dual,
        txtConfirm = TEXT.nameConfirm, txtCancel = TEXT.nameCancel,         
    }
    return BoxQueue:enqueue(setmetatable(self, OBJDEF))
end

function OBJDEF:set_event(on_confirm, on_cancel)
    chk_args(self, "set_event")

    if on_confirm then self.on_confirm = on_confirm end
    if on_cancel then self.on_cancel = on_cancel end
    return self
end

function OBJDEF:set_text(txtConfirm, txtCancel)
    chk_args(self, "set_text")

    if txtConfirm then self.txtConfirm = txtConfirm end
    if txtCancel then self.txtCancel = txtCancel end
    return self
end

function OBJDEF:show()
    chk_args(self, "show")

    if ActivatedBox == nil then
        do_popup_box()
    end
end

function OBJDEF:clear()
    chk_args(self, "clear")
    
    BoxQueue:clear()
end

function OBJDEF.is_active()
    return Ref and libunity.IsActive(Ref.root)
end

_G.UI.MessageBox = OBJDEF
