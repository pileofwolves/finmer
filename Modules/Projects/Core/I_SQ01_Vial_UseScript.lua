-- if in a taur belly, replace with the filled vial
if Player:IsEaten() then
    --if Player:GetPredator():HasTag("taur") then
        Log("sq01_vial_use")
        Player:TakeItem("SQ01_Vial")
        Player:GiveItem("SQ01_VialFull")
        Journal.Update("SQ01", 20)
    --else
    --    Log("sq01_vial_needtaur")
    --end
else
    Log("item_cannotusehere")
end