SetLocation("The Snout and Thorn")
SetInventoryEnabled(true)

if not Storage.GetFlag("PORT_TAVERN_ENTER_FIRST") then
    Storage.SetFlag("PORT_TAVERN_ENTER_FIRST", true)
    Log("PORT_TAVERN_ENTER_FIRST")
end

Log("PORT_TAVERN_ENTER")

-- Extra text mentioning Zosk at MQ05 start
if Storage.GetFlag("MQ04_DONE") and not Storage.GetFlag("MQ05_INTRO_ZOSK") then
    Log("MQ05_INTRO_ZOSK1")
else
    Log("PORT_TAVERN_ENTER_EMPTY")
end
