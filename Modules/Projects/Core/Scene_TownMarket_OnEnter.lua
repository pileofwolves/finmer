-- Post-MQ02 Chase Event: don't print "how lovely is this weather" text if we're initiating the chase
if Storage.GetFlag("mq02_done") and not Storage.GetFlag("town_market_postmq02_event") then
    return
end

-- show marketplace description when entering, but not when coming from intro sequence
if Storage.GetFlag("town_first") then
    if math.random(0, 1) == 1 then
        Log(GetIsNight() and "town_market_night" or "town_market_day")
    end
    Log("town_market")
end

-- fish vendor is always a fox (because that was established in the werewolf short story)
-- but we can randomly generate a fruit vendor
local fruit_vendor = GetUniqueCharacter("town_fruit_vendor")