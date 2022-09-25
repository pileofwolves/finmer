------------------------------------------------------------------------------
-- Math utilities
------------------------------------------------------------------------------

function Roll(n, d)
    local total = 0
    for _ = 1, n do
        total = total + math.random(1, d)
    end
    return total
end

------------------------------------------------------------------------------
-- Log utilities
------------------------------------------------------------------------------

function LogVore(predator, prey)
    Text.SetContext("predator", predator)
    Text.SetContext("prey", prey)
    Sleep(3)
    LogRaw(prey:GetString("VORE_WIN", Player))
end

function LogPostVore(scat, noscat)
    -- if the player disabled scat, then use the noscat msg if present, otherwise exit
    local text = Player.PreferScat and scat or noscat
    if text == nil then return end

    Sleep(3)
    LogSplit()
    Log(text)
end

EPreySenseType = {
    OralVore            = 0,
    AnalVore            = 1,
    CockVore            = 2,
    Unbirth             = 3,
    Endo                = 4,
    EndoOrFatal         = 5,
    DigestionReform     = 6,
    DigestionFatal      = 7,
}

function PreySense(pred, ...)
    -- Skip if Preysense is not enabled
    if not Player.IsPreySenseEnabled then return end

    -- Map each Preysense type to a description
    local descriptions = {
        [EPreySenseType.OralVore]           = "oral vore",
        [EPreySenseType.AnalVore]           = "anal vore",
        [EPreySenseType.CockVore]           = "cock vore",
        [EPreySenseType.Unbirth]            = "unbirth",
        [EPreySenseType.Endo]               = "safe",
        [EPreySenseType.EndoOrFatal]        = "either safe or fatal",
        [EPreySenseType.DigestionReform]    = "digestion, reformation",
        [EPreySenseType.DigestionFatal]     = "digestion, fatal",
    }

    -- Convert input Preysense types to text, per the mapping
    local is_fatal = false
    local types_num = {...}
    local types_text = {}
    for i, v in ipairs(types_num) do
        table.insert(types_text, descriptions[v])
        is_fatal = is_fatal or (v == EPreySenseType.DigestionFatal)
    end

    -- Concatenate all type descriptions together, then prefix with pred name
    local types_merged = table.concat(types_text, ", ")
    local final_string = table.concat{ "Your prey-senses are tingling... ", pred.Name, ": ", types_merged }

    -- Display string
    LogRaw(final_string, is_fatal and Color.Hostile or Color.Neutral)
end

------------------------------------------------------------------------------
-- Clock management and querying
------------------------------------------------------------------------------

function GetTimeDay()
    local day, _ = GetTime()
    return day
end

function GetTimeHour()
    local _, hour = GetTime()
    return hour
end

function GetTimeHourTotal()
    local day, hour = GetTime()
    return hour + day * 24
end

function GetIsNight()
    local hour = GetTimeHour()
    return hour < 6 or hour > 20
end

function SetTimeHour(target)
    local now = GetTimeHour()
    local diff = target - now
    if diff < 0 then
        -- move clock "backwards" (a full cycle) to reach an earlier timestamp
        AdvanceTime(24 + diff)
    elseif diff > 0 then
        -- move clock forwards by difference to target
        AdvanceTime(diff)
    end
end

function FoodComa()
    -- Used to have its own animation; is now an alias for Rest().
    Rest()
end

function Rest()
    Sleep(1)

    -- Sleep until morning at least
    local sleepTime
    local hr = GetTimeHour()
    if hr >= 7 then
        sleepTime = 24 - (hr - 7)
    else
        sleepTime = 12
    end
    AdvanceTime(sleepTime)
    Sleep(1)

    -- Recover
    Player.Health = Player.HealthMax
end

function TimedCheckpoint(id)
    local now = GetTimeHourTotal()
    id = "_TIMED_CHECKPOINT_" .. id

    -- Take a checkpoint if the game clock advanced since the last time this function was called
    if Storage.GetNumber(id) ~= now then
        Storage.SetNumber(id, now)
        SaveData.TakeCheckpoint()
    end
end

------------------------------------------------------------------------------
-- Game randomization
------------------------------------------------------------------------------

-- returns randomly generated character, see script documentation
function GetRandomCharacter()
    local result = Creature("CR_RandomAnthro")
    local ch_species = string.lower(Text.GetString("SPECIES_ANY"))
    result.Name = ch_species
    result.Alias = "the " .. ch_species
    result.Gender = math.random(0, 1)
    return result
end
function GetUniqueCharacter(ident)
    local prefix = "_uchar_" .. ident
    local ch_species
    local ch_gender
    if Storage.GetFlag(prefix .. "_generated") then
        -- Retrieve previously generated settings
        ch_gender = Storage.GetNumber(prefix .. "_gender")
        ch_species = Storage.GetString(prefix .. "_species")
    else
        -- Randomize and store new settings
        ch_gender = math.random(0, 2)
        ch_species = string.lower(Text.GetString("SPECIES_ANY"))
        Storage.SetFlag(prefix .. "_generated", true)
        Storage.SetNumber(prefix .. "_gender", ch_gender)
        Storage.SetString(prefix .. "_species", ch_species)
    end

    -- Wrap as a Creature, so it can be used as a grammar context
    local result = Creature("CR_RandomAnthro")
    result.Name = ch_species
    result.Alias = "the " .. ch_species
    result.Gender = ch_gender
    return result
end
