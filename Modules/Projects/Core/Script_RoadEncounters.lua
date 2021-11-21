function BeginRoadTravel(destination)
    -- this feature is designed to be reusable: just specify the dest scene,
    -- and it'll loop Scene_RoadEnc for a few times
    Storage.SetString("roadenc_dest", destination)
    Storage.SetNumber("roadenc_state", 0)
    ContinueRoadTravel()
end

function ContinueRoadTravel()
    -- to help simulate distance
    AdvanceTime(2)

    -- check how far along we are so far
    local progress = Storage.GetNumber("roadenc_state") + 1
    if progress >= 2 then
        -- if we've played the on-road scene twice, arrive at destination
        SetScene(Storage.GetString("roadenc_dest"))
    else
        -- if not, increase progress and replay the on-road scene
        Storage.SetNumber("roadenc_state", progress)
        SetScene("Scene_RoadEnc")
    end
end
