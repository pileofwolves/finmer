SetLocation("North Finmer, Smithy")

-- Show longer text on first visit
if Storage.GetFlag("TOWN_SMITHY_FIRST") then
    Log("TOWN_SMITHY")
else
    Storage.SetFlag("TOWN_SMITHY_FIRST", true)
    Log("TOWN_SMITHY_FIRST")
end
