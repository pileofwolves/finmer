function BeginRoadTravel(direction, destination)
    -- this feature is designed to be reusable: just specify the dest scene,
    -- and it'll loop Scene_RoadEnc for a few times
    Storage.SetString("ROADENC_DEST", destination)
    Storage.SetNumber("ROADENC_DIR", direction)
    Storage.SetNumber("ROADENC_STATE", 0)
    ContinueRoadTravel()
end

function ContinueRoadTravel()
    -- to help simulate distance
    AdvanceTime(2)

    -- check how far along we are so far
    local progress = Storage.GetNumber("ROADENC_STATE") + 1
    if progress >= 2 then
        -- if we've played the on-road scene twice, arrive at destination
        SetScene(Storage.GetString("ROADENC_DEST"))
    else
        -- if not, increase progress and replay the on-road scene
        Storage.SetNumber("ROADENC_STATE", progress)
        SetScene("Scene_RoadEnc")
    end
end
