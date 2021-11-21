local function GetBridgeTimeLeft()
    return Storage.GetNumber("bridge_repair") - GetTimeHourTotal()
end

function GoEast()
    BeginRoadTravel("Scene_ForestCottage")
end

function GoSouth()
    if not Storage.GetFlag("mq02_done") then
        Log("bridge_broken")
        return
    end

    -- MQ02 bridge repair sequence
    if not Storage.GetFlag("bridge_is_repaired") then
        local timeLeft = GetBridgeTimeLeft()
        if timeLeft > 8 then
            Log("bridge_repair_long")
        else
            Log("bridge_repair_short")
        end
        return
    end
    -- if bridge is repaired, start up travel to the south
    --BeginRoadTravel("Scene_Cliffside")
    Log("DEMO_END", Color.Highlight)
end
