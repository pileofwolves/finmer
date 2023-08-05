---------------
-- Constants --
---------------

local s_Rooms = {}
local k_JournalColor = Color(186, 241, 255)
local ELinkMode = { Skip = 0, Default = 1 }

----------------------
-- Engine utilities --
----------------------

-- Generate a compass link function that passes control to another State
local function _NodeLinkInternal(key)
    return function()
        -- For any curious spelunkers: this function makes 'unsafe' assumptions about the way the scene system
        -- runs turn cycles, and depends on undocumented engine features. If there is enough interest, I might
        -- look into providing proper tools for dungeon-crawler-like gameplay; please reach out on the forum.
        ResetButtons()
        _RestoreState(key)
    end
end

--------------------
-- Room framework --
--------------------

-- Link generator that always transitions to the specified state node
local function NodeLink(target_node)
    return function() return _NodeLinkInternal(target_node) end
end

-- Link generator that evaluates a check, and if true, links to node, if false, prints text.
local function LockableNodeLink(check, target_node, lock_text, lock_repeat_text)
    return function()
        -- Need to use if statement because checks may return false, and that breaks the ternary idiom
        local unlocked; if type(check) == "function" then unlocked = check() else unlocked = Storage.GetFlag(check) end
        if unlocked then
            return _NodeLinkInternal(target_node)
        else
            return function()
                -- Optional alternate text on subsequent poke attempts
                lock_repeat_text = lock_repeat_text or lock_text
                Log(Storage.GetFlag(lock_text) and lock_repeat_text or lock_text)
                Storage.SetFlag(lock_text, true)
            end
        end
    end
end

-- Link generator that transitions to one of two nodes, depending on a flag
local function BranchNodeLink(flag, true_node, false_node)
    return function()
        return _NodeLinkInternal(Storage.GetFlag(flag) and true_node or false_node)
    end
end

-- Create a link label info object
local function LinkLabel(flag, unvisited_text, visited_text)
    return { flag = flag, unvisited_text = unvisited_text, visited_text = visited_text }
end

-- Create and register a room object
local function Room(args)
    assert(args.key ~= nil)
    assert(args.label ~= nil)
    assert(args.text_first ~= nil)
    assert(args.text_repeat ~= nil)
    s_Rooms[args.key] = args
end

-- Activate the specified room key. To be used by node scripts.
local function SetCurrentRoom(room_id, link_mode)
    local room_data = s_Rooms[room_id]

    -- Evaluate compass links
    link_mode = link_mode or ELinkMode.Default
    if link_mode == ELinkMode.Default then
        if room_data.link_n ~= nil then AddLink(ECompass.North, room_data.link_n()) end
        if room_data.link_w ~= nil then AddLink(ECompass.West, room_data.link_w()) end
        if room_data.link_s ~= nil then AddLink(ECompass.South, room_data.link_s()) end
        if room_data.link_e ~= nil then AddLink(ECompass.East, room_data.link_e()) end
    end

    -- Skip text if already in this room - useful for linking back to the room's root state
    if Storage.GetString("MQ05_MAW_CURRENT_ROOM") ~= room_data.key then
        -- Remember which room we are in now
        Storage.SetString("MQ05_MAW_CURRENT_ROOM", room_data.key)

        -- Update location
        SetLocation(room_data.label)

        -- Evaluate link labels
        if room_data.link_labels ~= nil then
            for i, v in ipairs(room_data.link_labels) do
                -- Set the i-th 'link' substitution variable to one of two text keys, depending on the input flag
                local text_key = Storage.GetFlag(v.flag) and v.visited_text or v.unvisited_text
                Text.SetVariable("link" .. i, Text.GetString(text_key))
            end
        end

        -- Log the first string key on first visit, and repeat key on subsequent visits
        LogSplit()
        if Storage.GetFlag(room_data.text_first) then
            Log(room_data.text_repeat)
        else
            Storage.SetFlag(room_data.text_first, true)
            Log(room_data.text_first)
        end
    end
end

-------------------
-- Room database --
-------------------

Room {
    key             = "Room_EntryHall",
    label           = "Red Maw, Entrance",
    text_first      = "MQ05_MAW_ROOM_ENTRY_FIRST",
    text_repeat     = "MQ05_MAW_ROOM_ENTRY",
    link_n          = LockableNodeLink("MQ05_MAW_CAN_LEAVE", "Cart_Exit", "MQ05_MAW_ROOM_ENTRY_EXIT_LOCKED"),
    link_e          = NodeLink("Room_Barracks"),
    link_s          = BranchNodeLink("MQ05_MAW_ROOM_STORAGE1_FIRST", "Room_Storage1", "Encounter_Mako"),
    link_labels     = {
        LinkLabel("MQ05_MAW_ROOM_STORAGE1_FIRST", "MQ05_MAW_ROOM_ENTRY_LINK1A", "MQ05_MAW_ROOM_ENTRY_LINK1B"),
        LinkLabel("MQ05_MAW_ROOM_BARRACKS_FIRST", "MQ05_MAW_ROOM_ENTRY_LINK2A", "MQ05_MAW_ROOM_ENTRY_LINK2B"),
    }
}

Room {
    key             = "Room_Barracks",
    label           = "Red Maw, Barracks",
    text_first      = "MQ05_MAW_ROOM_BARRACKS_FIRST",
    text_repeat     = "MQ05_MAW_ROOM_BARRACKS",
    link_w          = NodeLink("Room_EntryHall"),
    link_e          = NodeLink("Room_Kitchen"),
}

Room {
    key             = "Room_Kitchen",
    label           = "Red Maw, Empty Room",
    text_first      = "MQ05_MAW_ROOM_KITCHEN_FIRST",
    text_repeat     = "MQ05_MAW_ROOM_KITCHEN",
    link_w          = NodeLink("Room_Barracks"),
}

Room {
    key             = "Room_Workshop",
    label           = "Red Maw, Workshop",
    text_first      = "MQ05_MAW_ROOM_WORKSHOP_FIRST",
    text_repeat     = "MQ05_MAW_ROOM_WORKSHOP",
    link_e          = NodeLink("Room_Storage1"),
}

Room {
    key             = "Room_Study",
    label           = "Red Maw, Study",
    text_first      = "MQ05_MAW_ROOM_STUDY_FIRST",
    text_repeat     = "MQ05_MAW_ROOM_STUDY",
    link_s          = NodeLink("Room_CorridorE1"),
}

Room {
    key             = "Room_Storage1",
    label           = "Red Maw, Storage Room",
    text_first      = "MQ05_MAW_ROOM_STORAGE1_FIRST",
    text_repeat     = "MQ05_MAW_ROOM_STORAGE1",
    link_n          = NodeLink("Room_EntryHall"),
    link_w          = NodeLink("Room_Workshop"),
    link_s          = NodeLink("Room_Shaft1"),
    link_labels     = {
        LinkLabel("MQ05_MAW_ROOM_WORKSHOP_FIRST", "MQ05_MAW_ROOM_STORAGE1_LINK1A", "MQ05_MAW_ROOM_STORAGE1_LINK1B"),
    }
}

Room {
    key             = "Room_Storage2",
    label           = "Red Maw, Mess Hall",
    text_first      = "MQ05_MAW_ROOM_STORAGE2_FIRST",
    text_repeat     = "MQ05_MAW_ROOM_STORAGE2",
    link_w          = NodeLink("Room_Shaft2"),
    link_e          = LockableNodeLink("MQ05_MAW_ROOM_CORRIDOR_E2_FIRST", "Room_CorridorE2", "MQ05_MAW_ROOM_CORRIDOR_E2_LOCKED"),
    link_labels     = {
        LinkLabel("MQ05_MAW_ROOM_CORRIDOR_E2_FIRST", "MQ05_MAW_ROOM_STORAGE2_LINK1A", "MQ05_MAW_ROOM_STORAGE2_LINK1B"),
    }
}

Room {
    key             = "Room_Shaft1",
    label           = "Red Maw, Upper Level",
    text_first      = "MQ05_MAW_ROOM_SHAFT1_FIRST",
    text_repeat     = "MQ05_MAW_ROOM_SHAFT1",
    link_n          = NodeLink("Room_Storage1"),
    link_w          = NodeLink("Room_CorridorW1A"),
    link_s          = NodeLink("Room_Shaft2"),
    link_e          = NodeLink("Room_CorridorE1"),
}

Room {
    key             = "Room_Shaft2",
    label           = "Red Maw, Lower Level",
    text_first      = "MQ05_MAW_ROOM_SHAFT2_FIRST",
    text_repeat     = "MQ05_MAW_ROOM_SHAFT2",
    link_n          = NodeLink("Room_Shaft1"),
    link_w          = LockableNodeLink(function() return not Storage.GetFlag("MQ05_MAW_ROOM_COLLAPSED_DONE") end,
                      "Room_Collapsed", "MQ05_MAW_ROOM_COLLAPSED_LOCKED"),
    link_s          = LockableNodeLink("MQ05_MAW_CART_RELEASED", "Room_Shaft3", "MQ05_MAW_ROOM_SHAFT3_LOCKED"),
    link_e          = BranchNodeLink("MQ05_MAW_ROOM_STORAGE2_FIRST", "Room_Storage2", "Encounter_Ethel"),
    link_labels     = {
        LinkLabel("MQ05_MAW_ROOM_STORAGE2_FIRST", "MQ05_MAW_ROOM_SHAFT2_LINK1A", "MQ05_MAW_ROOM_SHAFT2_LINK1B"),
    }
}

Room {
    key             = "Room_Shaft3",
    label           = "Red Maw, Depths",
    text_first      = "MQ05_MAW_ROOM_SHAFT3_FIRST",
    text_repeat     = "MQ05_MAW_ROOM_SHAFT3",
    link_n          = NodeLink("Room_Shaft2"),
    link_s          = NodeLink("Room_Terminus"),
    link_e          = NodeLink("Room_Iron"),
}

Room {
    key             = "Room_CorridorW1A",
    label           = "Red Maw, Side Tunnels, West",
    text_first      = "MQ05_MAW_ROOM_CORRIDOR_W1A",
    text_repeat     = "MQ05_MAW_ROOM_CORRIDOR_W1A",
    link_w          = NodeLink("Room_CorridorW1B"),
    link_e          = NodeLink("Room_Shaft1"),
}

Room {
    key             = "Room_CorridorW1B",
    label           = "Red Maw, Side Tunnels, West",
    text_first      = "MQ05_MAW_ROOM_CORRIDOR_W1B_FIRST",
    text_repeat     = "MQ05_MAW_ROOM_CORRIDOR_W1B",
    link_e          = NodeLink("Room_CorridorW1A"),
}

Room {
    key             = "Room_CorridorE1",
    label           = "Red Maw, Side Tunnels, East",
    text_first      = "MQ05_MAW_ROOM_CORRIDOR_E1",
    text_repeat     = "MQ05_MAW_ROOM_CORRIDOR_E1",
    link_n          = LockableNodeLink(function() return Player:HasItem("I_MQ05_StudyKey") or Storage.GetFlag("MQ05_MAW_ROOM_STUDY_FIRST") end,
                      "Room_Study", "MQ05_MAW_ROOM_STUDY_LOCKED", "MQ05_MAW_ROOM_STUDY_LOCKED2"),
    link_w          = NodeLink("Room_Shaft1"),
    link_s          = NodeLink("Room_CorridorE2"),
    link_labels     = {
        LinkLabel("MQ05_MAW_ROOM_STUDY_FIRST", "MQ05_MAW_ROOM_CORRIDOR_E1_LINK1A", "MQ05_MAW_ROOM_CORRIDOR_E1_LINK1B"),
    }
}

Room {
    key             = "Room_CorridorE2",
    label           = "Red Maw, Side Tunnels, East",
    text_first      = "MQ05_MAW_ROOM_CORRIDOR_E2_FIRST",
    text_repeat     = "MQ05_MAW_ROOM_CORRIDOR_E2",
    link_n          = NodeLink("Room_CorridorE1"),
    link_w          = BranchNodeLink("MQ05_MAW_ROOM_STORAGE2_FIRST", "Room_Storage2", "Encounter_Ethel"),
    link_labels     = {
        LinkLabel("MQ05_MAW_ROOM_STORAGE2_FIRST", "MQ05_MAW_ROOM_CORRIDOR_E2_LINK1A", "MQ05_MAW_ROOM_CORRIDOR_E2_LINK1B"),
    }
}

Room {
    key             = "Room_Collapsed",
    label           = "Red Maw, Collapsed Tunnel",
    text_first      = "MQ05_MAW_ROOM_COLLAPSED",
    text_repeat     = "MQ05_MAW_ROOM_COLLAPSED",
    link_e          = NodeLink("Room_Shaft2"),
}

Room {
    key             = "Room_Iron",
    label           = "Red Maw, Deep Side Tunnel",
    text_first      = "MQ05_MAW_ROOM_IRON_FIRST",
    text_repeat     = "MQ05_MAW_ROOM_IRON",
    link_w          = NodeLink("Room_Shaft3"),
}

Room {
    key             = "Room_Terminus",
    label           = "Red Maw, Pit",
    text_first      = "MQ05_MAW_ROOM_TERMINUS",
    text_repeat     = "MQ05_MAW_ROOM_TERMINUS",
    link_n          = BranchNodeLink("MQ05_MAW_ROOM_TERMINUS_FIRST", "Room_Shaft3", "Encounter_Jett"),
}
