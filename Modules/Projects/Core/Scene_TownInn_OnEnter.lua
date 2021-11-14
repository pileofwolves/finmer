SetLocation("Town, Inn")

-- Set up the bartender context
Text.SetContext("bartender", barkeep)

-- if loading save, don't tell the player they just entered the inn
if Storage.GetFlag("savepoint") then
    Log("TOWN_INN_SAVEPOINT_ENTER")
    return
end

-- long/detailed message on first visit, short version afterwards
if Storage.GetFlag("town_inn_visited") then
    Log("town_inn_enter")
else
    Storage.SetFlag("town_inn_visited", true)
    Log("town_inn_enter_first")
end
if not Storage.GetFlag("town_inn_foxtaur_arrested") then
    Log("TOWN_INN_PATRONS_TAUR")
end

-- can't sleep twice in a row, and we reset that flag when re-entering the inn
Storage.SetFlag("town_inn_cansleep", true)
