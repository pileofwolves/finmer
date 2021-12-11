-- show marketplace description when entering, but not when coming from intro sequence
if Storage.GetFlag("town_first") then
    if math.random(0, 1) == 1 then
        Log(GetIsNight() and "town_market_night" or "town_market_day")
    end
    Log("town_market")
end
