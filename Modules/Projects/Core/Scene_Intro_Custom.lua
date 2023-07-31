local function DebugStartMQ01()
    Storage.SetFlag("TOWN_FIRST", true)
    Storage.SetFlag("TOWN_INN_RENTED_ROOM", true)
    Storage.SetFlag("MQ01_STARTED", true)
end

local function DebugCompleteMQ01()
    DebugStartMQ01()
    Player:AwardXP(100)
    Storage.SetFlag("MQ01_DONE", true)
end

local function DebugStartMQ02()
    DebugCompleteMQ01()
    Player.EquippedWeapon = Item("W_SwordSteel")
end

local function DebugCompleteMQ02()
    DebugStartMQ02()
    Player:AwardXP(200)
    Storage.SetFlag("MQ02_DONE", true)
end

local function DebugStartMQ03()
    DebugCompleteMQ02()
    Storage.SetFlag("MQ03_STARTED", true)
    Storage.SetFlag("MQ03_RODE_SOUTH", true)
    Storage.SetFlag("BRIDGE_IS_REPAIRED", true)
end

local function DebugCompleteMQ03()
    DebugStartMQ03()
    Player:AwardXP(200)
    Player.EquippedArmor = Item("A_LeatherArmor")
    Player.EquippedAccessory1 = Item("AC_IsoRing2")
    Storage.SetFlag("MQ03_TAVERN_EVENT", true)
    Storage.SetFlag("MQ03_TEMPLE_EVENT", true)
    Storage.SetFlag("MQ03_DONE", true)
end

local function DebugStartMQ04()
    DebugCompleteMQ03()
    Storage.SetNumber("MQ04", 1)
    Storage.SetNumber("SQ02", 999)
    Storage.SetFlag("FOREST_ADEPT_FIRST", true)
    Player:GiveItem("I_MQ04_RuxBook")
end

local function DebugStartMQ05()
    DebugStartMQ04()
    Storage.SetFlag("MQ04_DONE", true)
    Player:TakeItem("I_MQ04_RuxBook")
    Storage.SetNumber("MQ04", 4)
end
