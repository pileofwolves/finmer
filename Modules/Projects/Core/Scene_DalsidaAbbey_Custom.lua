function DalsidaAbbey_NoiseIncrement()
    -- Increment the counter
    local noise = Storage.GetNumber("ABBEY_EXPLORE_NOISE_LEVEL") + 1
    Storage.SetNumber("ABBEY_EXPLORE_NOISE_LEVEL", noise)

    -- Display an appropriate text
    PreySense(Creature("CR_AbbeyLuxray"), EPreySenseType.OralVore, EPreySenseType.DigestionFatal)
    Log("ABBEY_LUXRAY_NOISE" .. tostring(noise))
end

function DalsidaAbbey_IsNoiseTooHigh()
    return Storage.GetNumber("ABBEY_EXPLORE_NOISE_LEVEL") >= 3
end
