SetLocation("The Snout and Thorn")
SetInventoryEnabled(true)

if not Storage.GetFlag("PORT_TAVERN_ENTER_FIRST") then
    Storage.SetFlag("PORT_TAVERN_ENTER_FIRST", true)
    Log("PORT_TAVERN_ENTER_FIRST")
end

Log("PORT_TAVERN_ENTER")