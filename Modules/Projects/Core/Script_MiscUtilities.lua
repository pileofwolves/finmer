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
    print("using gsub with key " .. key .. ", consider phasing out")
    local strval = Text.GetString(key)
    assert(type(vars) == "table")
    for k, v in pairs(vars) do
        strval = string.gsub(strval, "%%" .. k, v)
    end
    LogRaw(strval, color)
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

-- shared function for simulating sleeping off prey
function FoodComa()
    -- advance by 12 hours gradually
    --[[for i = 1, 6 do
        Sleep(0.65)
        AdvanceTime(2)
    end]]

    Rest()
end

function Rest()
    Sleep(1)

    -- sleep until morning at least
    local sleepTime
    local hr = GetTimeHour()
    if hr >= 7 then
        sleepTime = 24 - (hr - 7)
    else
        sleepTime = 12
    end
    -- sleep a minimum time if digesting prey
    if Player.StomachCount > 0 and Player.StomachDigest then
        sleepTime = math.max(sleepTime, 20)
    end
    AdvanceTime(sleepTime)
    Sleep(1)

    -- recover
    Player.Health = Player.HealthMax
end

------------------------------------------------------------------------------
-- Game randomization
------------------------------------------------------------------------------

-- returns randomly generated character, see script documentation
function GetRandomCharacter()
    print("Genders: " .. #Gender)
    local result = Creature("RandomAnthro")
    result.Alias = Text.GetString("SPECIES_ANY")
    result.Gender = math.random(0, #Gender)
    return result
end
function GetUniqueCharacter(ident)
    print("Genders: " .. #Gender)
    local prefix = "_uchar_" .. ident
    local ch_species
    local ch_gender
    if Storage.GetFlag(prefix .. "_generated") then
        -- Retrieve previously generated settings
        ch_gender = Storage.GetNumber(prefix .. "_gender")
        ch_species = Storage.GetString(prefix .. "_species")
    else
        -- Randomize and store new settings
        ch_gender = math.random(0, #Gender)
        ch_species = Text.GetString("SPECIES_ANY")
        Storage.SetFlag(prefix .. "_generated", true)
        Storage.SetNumber(prefix .. "_gender", ch_gender)
        Storage.SetString(prefix .. "_species", ch_species)
    end

    -- Wrap as a Creature, so it can be used as a grammar context
    local result = Creature("RandomAnthro")
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
    -- if the player disabled scat, then use the noscat msg if present, otherwise exit
    local text = Player.PreferScat and scat or noscat
    if text == nil then return end

    Sleep(3)
    LogSplit()
    Log(text)
end
