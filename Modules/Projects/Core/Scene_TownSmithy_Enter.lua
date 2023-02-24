SetLocation("North Finmer, Smithy")
if Storage.GetFlag("town_smithy_first") then
    Log("town_smithy")
else
    Storage.SetFlag("town_smithy_first", true)
    Log("TOWN_SMITHY_FIRST")
end