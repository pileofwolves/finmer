if Storage.GetFlag("forest_cottage_first") then
    Log("forest_cottage")
else
    Storage.SetFlag("forest_cottage_first", true)
    Log("forest_cottage_first")
end

SetInventoryEnabled(true)
SetLocation("Forest Edge")