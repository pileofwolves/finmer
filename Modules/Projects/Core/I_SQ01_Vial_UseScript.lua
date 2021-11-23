local state = GetActiveCombat()
assert(state ~= nil, "Item cannot be used outside combat")

-- Check whether the player participant is swallowed in this combat context
if state:IsSwallowed(Player) then
    -- Progress quest
    Log("SQ01_Vial_Use")
    Player:TakeItem("I_SQ01_Vial")
    Player:GiveItem("I_SQ01_VialFull")
    Journal.Update("SQ01", 20)
else
    -- Incorrect context
    Log("ITEM_CANNOTUSEHERE")
end
