local function DebugStartMQ01()
    Storage.SetFlag("TOWN_FIRST", true)
    Storage.SetFlag("TOWN_INN_RENTED_ROOM", true)
    Storage.SetFlag("MQ01_STARTED", true)
end

local function DebugCompleteMQ01()
    DebugStartMQ01()
    Player:AwardXP(RewardXP.MQ01_Accept)
    Player:AwardXP(RewardXP.MQ01_Complete)
    Storage.SetFlag("MQ01_DONE", true)
end

local function DebugStartMQ02()
    DebugCompleteMQ01()
    Player.EquippedWeapon = Item("W_Dagger")
end

local function DebugCompleteMQ02()
    DebugStartMQ02()
    Player:AwardXP(RewardXP.MQ02_Pacifist)
    Player:AwardXP(RewardXP.MQ02_Complete)
    Storage.SetFlag("MQ02_DONE", true)
    Storage.SetFlag("FOREST_COTTAGE_ENTRY_FIRST", true)
end

local function DebugStartMQ03()
    DebugCompleteMQ02()
    Player:AwardXP(RewardXP.MQ03_Accept)
    Storage.SetFlag("MQ03_STARTED", true)
    Storage.SetFlag("MQ03_CARRIAGE_INTRO", true)
    Storage.SetFlag("MQ03_RODE_SOUTH", true)
    Storage.SetFlag("BRIDGE_IS_REPAIRED", true)
end

local function DebugCompleteMQ03()
    DebugStartMQ03()
    Player:AwardXP(RewardXP.MQ03_Complete)
    Player:AwardXP(RewardXP.DiscoveryMajor) -- South Finmer (after cart ride)
    Player:AwardXP(RewardXP.DiscoveryMinor) -- Extracting info from Toby
    Player.EquippedWeapon = Item("W_SwordSteel")
    Player.EquippedArmor = Item("A_LeatherArmor")
    Player.EquippedAccessory1 = Item("AC_IsoRing2")
    Storage.SetFlag("MQ03_TAVERN_EVENT", true)
    Storage.SetFlag("MQ03_TEMPLE_EVENT", true)
    Storage.SetFlag("MQ03_DONE", true)
end

local function DebugStartMQ04()
    DebugCompleteMQ03()
    Player:AwardXP(RewardXP.DiscoveryMajor) -- Rux cabin
    Storage.SetNumber("MQ04", 1)
    Storage.SetNumber("SQ02", 999)
    Storage.SetFlag("FOREST_ADEPT_FIRST", true)
    Player:GiveItem("I_MQ04_RuxBook")
end

local function DebugStartMQ05()
    DebugStartMQ04()
    Player:AwardXP(RewardXP.MQ04_Complete)
    Storage.SetFlag("MQ04_DONE", true)
    Storage.SetFlag("SQ05_AVAILABLE", true)
    Player:TakeItem("I_MQ04_RuxBook")
    Storage.SetNumber("MQ04", 4)
end

local function DebugStartMQ06()
    DebugStartMQ05()
    Player:AwardXP(RewardXP.MQ05_Complete)
    Storage.SetFlag("MQ05_INTRO_ZOSK", true)
    Storage.SetFlag("MQ05_INTRO_COURTYARD", true)
    Storage.SetFlag("MQ05_DONE", true)
    Storage.SetFlag("SQ05_AVAILABLE", false)
    Storage.SetFlag("MQ06_AVAILABLE", true)
end
