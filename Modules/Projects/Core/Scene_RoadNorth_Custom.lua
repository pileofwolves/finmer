local function GetBridgeTimeLeft()
    return Storage.GetNumber("BRIDGE_REPAIR_TIME") - GetTimeHourTotal()
end

function GoSouth()
    -- Bridge is out
    if not Storage.GetFlag("MQ03_STARTED") then
        Log("BRIDGE_BROKEN")
        return
    end

    -- Bridge is being repaired
    if not Storage.GetFlag("BRIDGE_IS_REPAIRED") then
        local hours_left = GetBridgeTimeLeft()
        if hours_left > 8 then
            Log("BRIDGE_REPAIR_LONG")
        else
            Log("BRIDGE_REPAIR_SHORT")
        end
        return
    end

    -- Bridge has been repaired, go south!
    -- Note: the first trip south (with the carriage) doesn't go through this function; check the scene instead
    assert(Storage.GetFlag("MQ03_RODE_SOUTH"), "bypassing carriage ride")
    BeginRoadTravel(ECompass.South, "Scene_Cliffside")
end

-- Once MQ02 is done, we can repair the bridge
if Storage.GetFlag("MQ03_STARTED") and GetBridgeTimeLeft() <= 0 then
    Storage.SetFlag("BRIDGE_IS_REPAIRED", true)
end
