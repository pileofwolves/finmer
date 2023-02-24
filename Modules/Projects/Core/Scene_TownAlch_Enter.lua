-- Update compass
SetLocation("North Finmer, Alchemist")

-- Skip welcome message if alchemist is killed
if not Storage.GetFlag("town_alch_dead") then

    -- long/detailed message on first visit, short version afterwards
    if Storage.GetFlag("town_alch_visited") then
        Log("town_alch_enter")
    else
        Storage.SetFlag("town_alch_visited", true)
        Log("town_alch_enter_first")
    end

end
