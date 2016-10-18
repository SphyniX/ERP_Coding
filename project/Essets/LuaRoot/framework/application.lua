-- File Name : framework/application.lua

local function on_app_launch()
    print("on_app_launch")
end

local function on_app_pause(paused)
    print("on_app_pause", paused)
end

local function on_app_focus(focused)
   print("on_app_focus", focused)
end

local function on_sys_message(message)
   print("on_sys_message", message)
end

return {
    on_app_launch = on_app_launch,
    on_app_pause = on_app_pause,
    on_app_focus = on_app_focus,
    on_sys_message = on_sys_message,
}