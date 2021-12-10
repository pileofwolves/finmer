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

-- handles drawing a random rumor on a global cooldown
function LogRumor(success, fail, vars, color)
    vars = vars or {}
    -- TODO: Temporarily disabled rumor system, until I actually know some rumors to write.
    -- Need more content for that.
    if true --[[Storage.GetNumber("next_rumor") > GetTimeHourTotal()]] then
        LogGsub(fail, vars, color)
    else
        -- pull a random rumor and show it
        vars["rumor"] = Text.GetString("RUMOR")
        LogGsub(success, vars, color)
        -- cooldown
        Storage.SetNumber("next_rumor", GetTimeHourTotal() + 1)
    end
end

function LogGsub(key, vars, color)
    -- Deprecation warning
    if IsDebugMode() then
        print("Called LogGsub(" .. key .. "), this function is deprecated")
    end

    local strval = Text.GetString(key)
    assert(type(vars) == "table")
    for k, v in pairs(vars) do
        strval = string.gsub(strval, "%%" .. k, v)
    end
    LogRaw(strval, color)
end

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

function PreySense(pred, style, does_digest)
    if Player.IsPreySenseEnabled then
        local vtype
        if style == EVoreStyle.OV then vtype = "oral vore"
        elseif style == EVoreStyle.AV then vtype = "anal vore"
        elseif style == EVoreStyle.CV then vtype = "cock vore"
        elseif style == EVoreStyle.UB then vtype = "unbirth"
        end
        local vfinal = does_digest and "fatal" or "endo"
        local vcolor = does_digest and Color.Hostile or Color.Neutral
        LogRaw("You have that strange feeling...  " .. pred.Name .. ": " .. vtype .. ", " .. vfinal, vcolor)
    end
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

------------------------------------------------------------------------------
-- Game randomization
------------------------------------------------------------------------------

-- returns randomly generated character, see script documentation
function GetRandomCharacter()
    local result = Creature("CR_RandomAnthro")
    result.Alias = Text.GetString("SPECIES_ANY")
    result.Gender = math.random(0, 2)
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
        ch_species = Text.GetString("SPECIES_ANY")
        Storage.SetFlag(prefix .. "_generated", true)
        Storage.SetNumber(prefix .. "_gender", ch_gender)
        Storage.SetString(prefix .. "_species", ch_species)
    end

    -- Wrap as a Creature, so it can be used as a grammar context
    local result = Creature("CR_RandomAnthro")
    result.Alias = ch_species
    result.Gender = ch_gender
    return result
end

------------------------------------------------------------------------------
-- Combat callback utilities
------------------------------------------------------------------------------

-- Meant to be used from a Combat.OnPlayerKilled callback, to force a vore scene
-- despite the player being defeated through non-vore combat actions
function EatThePlayerAnyway(predator)
    -- Deprecation warning - replace with PredatorAlwaysSwallowPlayer
    if IsDebugMode() then
        print("Called EatThePlayerAnyway - this function is deprecated")
    end

    Text.SetContext("predator", predator)
    Text.SetContext("prey", Player)
    Sleep(2)
    LogRaw(predator:GetString("vore_hit_pov"))
    Sleep(3)
    LogRaw(predator:GetString("vore_win_pov"))
    Sleep(2)
    LogRaw(predator:GetString("kill_digested_pov"))
end
function EatThePreyAnyway(prey)
    -- Deprecation warning - this should be replaced by a new flag like PreyAlwaysSwallowedOnDefeat
    if IsDebugMode() then
        print("Called EatThePreyAnyway - this function is deprecated")
    end

    -- As EatThePlayerAnyway(), but from player POV
    Text.SetContext("predator", Player)
    Text.SetContext("prey", prey)
    Combat.SetVored(Player, prey, true)
    Sleep(2)
    LogRaw(prey:GetString("VORE_HIT", Player))
    Sleep(3)
    LogRaw(prey:GetString("VORE_WIN", Player))
    Sleep(2)
    LogRaw(prey:GetString("KILL_DIGESTED", Player))
end
function PostVore(scat, noscat)
    -- Deprecation warning
    if IsDebugMode() then
        print("Called PostVore(" .. scat .. ") - this function is deprecated")
    end

    -- Forward to new function
    LogPostVore(scat, noscat)
end
