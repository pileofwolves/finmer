SetLocation("North Finmer, Inn")

-- Set up the bartender context
Text.SetContext("bartender", barkeep)

-- Long/detailed message on first visit, short version afterwards
if Storage.GetFlag("TOWN_INN_VISITED") then
    Log("town_inn_enter")
else
    Storage.SetFlag("TOWN_INN_VISITED", true)
    Log("town_inn_enter_first")
end

-- Mention present patrons
if not Storage.GetFlag("town_inn_foxtaur_arrested") then
    Log("TOWN_INN_PATRONS_TAUR")
end
if not Storage.GetFlag("MQ03_STARTED") then
    Log("INN_ZOSK_PATRON")
end

-- Allow sleeping in the private room after leaving and re-entering the inn
Storage.SetFlag("TOWN_INN_CAN_SLEEP", true)
