-- Intro text
if Storage.GetFlag("OLIVER_COTTAGE_AREA_FIRST") then
    Log("OLIVER_COTTAGE_AREA")
else
    Storage.SetFlag("OLIVER_COTTAGE_AREA_FIRST", true)
    Log("OLIVER_COTTAGE_AREA_FIRST")
end

-- Improve Oliver's mood with story progression
local mood = Storage.GetNumber("OLIVER_MOOD")
if mood == 0 and Storage.GetFlag("MQ03_DONE") then
    -- Enable first checkup scene after MQ03
    Storage.SetNumber("OLIVER_MOOD", 1)
elseif mood == 2 and GetTimeHourTotal() >= Storage.GetNumber("OLIVER_AWOL_RETURN") then
    -- Enable main dialogue tree some time after checkup
    Storage.SetNumber("OLIVER_MOOD", 3)
end
