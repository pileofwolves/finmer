SetLocation("South Finmer, Docks")
SetInventoryEnabled(true)

if not Storage.GetFlag("PORT_DOCKS_ENTER_FIRST") then
    Storage.SetFlag("PORT_DOCKS_ENTER_FIRST", true)
    Log("PORT_DOCKS_ENTER_FIRST")
end

Log("PORT_DOCKS_ENTER")