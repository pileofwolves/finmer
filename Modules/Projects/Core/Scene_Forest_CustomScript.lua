-------------------------------------------
-- EVENT 1: SQ02 CLEARING
-------------------------------------------

function Event1_Clearing()
    SetScene("Scene_ForestClearing")
end

-------------------------------------------
-- SYSTEM CODE
-------------------------------------------

local encounters = {
    Event1_Clearing
}

function Wander()
    local night = GetIsNight()
    local roll = math.random(0, 2)
    AdvanceTime(1)

    if roll ~= 0 then
        -- skip round
        Log(night and "FOREST_NO_EVENT_NIGHT" or "FOREST_NO_EVENT_DAY")
    else
        -- run random encounter
        encounters[math.random(#encounters)]()
    end
end

function ExitForest()
    Log("FOREST_EXIT")
    AdvanceTime(1)
    SetScene("Scene_ForestCottage")
end
