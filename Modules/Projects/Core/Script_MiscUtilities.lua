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

function LogPostVore(scat, noscat)
    -- if the player disabled scat, then use the noscat msg if present, otherwise exit
    local text = Player.IsExplicitDisposalEnabled and scat or noscat
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

function RestUntilNight()
    Sleep(1)

    SetTimeHour(21)
    Player.Health = Player.HealthMax

    Sleep(1)
end

function TimedCheckpoint(id)
    local now = GetTimeHourTotal()
    id = "_TIMED_CHECKPOINT_" .. id

    -- Take a checkpoint if the game clock advanced since the last time this function was called
    if Storage.GetNumber(id) ~= now then
        SaveData.TakeCheckpoint()
        Storage.SetNumber(id, now)
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

-- Select 'count' strings from 'candidates', and add them to the stock of 'shop'.
function AddRandomizableShopStock(shop, candidates, count)
    -- Make a shallow copy of the input table, so we can modify it without affecting future calls
    local t = {}
    for k, v in pairs(candidates) do t[k] = v end

    -- Populate shop stock
    for _ = 1, count do
        -- If table of candidates ran out, nothing left to add
        local max = #t
        if max == 0 then return end

        -- Pop a random item from the candidates table and add it to the shop
        local rand_index = math.random(1, max)
        local item_name = table.remove(t, rand_index)
        if item_name ~= nil then
            shop:AddItem(Item(item_name), 1)
        end
    end
end
