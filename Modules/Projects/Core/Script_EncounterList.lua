-------------------------------------------
-- GROUP IDs
-------------------------------------------

k_EncounterGroup_Forest = "forest"

-------------------------------------------
-- DEFAULT EVENTS
-------------------------------------------

Encounter.Add {
    group       = k_EncounterGroup_Forest,
    weight      = 10,
    check       = nil,
    fn          = nil
}

-------------------------------------------
-- FOREST EVENTS
-------------------------------------------

Encounter.Add {
    group       = k_EncounterGroup_Forest,
    weight      = 5,
    fn          = function()
        SetScene("Scene_ForestClearing")
    end
}
