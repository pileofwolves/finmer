---------------
-- Constants --
---------------

local k_JournalColor = Color(186, 241, 255)

-------------------------------------------
-- Grab bag of dungeon delving utilities --
-------------------------------------------

-- Generate a compass link function that passes control to another State
local function NodeLink(key)
    return function()
        -- Undocumented engine features. Don't try this at home :)
        ResetButtons()
        _RestoreState(key)
    end
end

-- Log the first string key on first visit, and repeat key on subsequent visits
local function LogRoomText(key_repeat)
    -- Skip text if already in this room - useful for linking back to the room's root state
    if Storage.GetString("MQ05_MAW_CURRENT_ROOM") == key_repeat then return end
    Storage.SetString("MQ05_MAW_CURRENT_ROOM", key_repeat)

    -- Print either of the room texts
    key_first = key_repeat .. "_FIRST"
    LogSplit()
    if Storage.GetFlag(key_first) then
        Log(key_repeat)
    else
        Storage.SetFlag(key_first, true)
        Log(key_first)
    end
end

-- Configure the 'linkN' substitution variable to one of two keys, depending on the input flag
local function SetLinkLabelVariable(n, flag, unvisited, visited)
    local text_key = Storage.GetFlag(flag) and visited or unvisited
    Text.SetVariable("link" .. n, Text.GetString(text_key))
end
