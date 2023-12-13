function Wander()
    -- Advance the game clock
    AdvanceTime(1)

    -- Roll a random encounter
    local encounter = Encounter.Roll(k_EncounterGroup_Forest)
    if encounter == nil then
        -- No encounter; display placeholder text
        local night = GetIsNight()
        Log(night and "FOREST_NO_EVENT_NIGHT" or "FOREST_NO_EVENT_DAY")
    else
        -- Run encounter script
        encounter()
    end
end
