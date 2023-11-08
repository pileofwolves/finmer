local iso = Creature("CR_MQ06_Iso"); iso.IsAlly = true
local rux = Creature("CR_MQ06_Rux"); rux.IsAlly = true
local randall = Creature("CR_MQ06_WereRandall")

function MQ06_MakeAmbushThugs()
    -- Reuse the same creature template, but tweak names, because I can't
    -- be arsed to make three different ones and keep them in sync.
    local t1 = Creature("CR_MQ06_AmbushThug")
    local t2 = Creature("CR_MQ06_AmbushThug")
    local t3 = Creature("CR_MQ06_AmbushThug")
    t1.Name = "Rodent"
    t2.Name = "Feline"
    t3.Name = "Canine"
    t1.Alias = "the rodent"
    t2.Alias = "the feline"
    t3.Alias = "the canine"

    -- Some fun with turn order and combat params
    t2.Strength = t2.Strength - 1
    t2.Agility = t2.Agility + 1
    t3.Strength = t3.Strength + 1
    t1.Wits = t1.Wits + 2
    t3.Wits = t3.Wits - 2

    return t1, t2, t3
end

local function MQ06_MakeCombat_Ambush()
    local fight = Combat2()

    -- Prepare combatants
    local thugs = { MQ06_MakeAmbushThugs() }
    fight:AddParticipant(Player)
    fight:AddParticipant(iso)
    fight:AddParticipant(rux)
    for i, t in ipairs(thugs) do
        fight:AddParticipant(t)
    end

    -- Utility used in multiple combat callbacks
    local function CheckCombatEnd()
        -- Count number of thugs still in the fight
        local live_thugs = 0
        for i, t in ipairs(thugs) do
            if not t:IsDead() and not fight:IsSwallowed(t) then
                Text.SetContext("thug", t) -- Used later in text
                live_thugs = live_thugs + 1
            end
        end
        -- Combat ends if only one thug is left standing
        if live_thugs <= 1 then
            -- Increase vore stats if fight ends before thug is digested
            for i, t in ipairs(thugs) do
                if fight:IsSwallowed(t) and not t:IsDead() then
                    Player.TotalPreyDigested = Player.TotalPreyDigested + 1
                end
            end

            fight:End()
        end
    end

    -- Check whether to end combat whenever an opponent could be incapacitated
    fight:OnCreatureKilled(CheckCombatEnd)
    fight:OnCreatureVored(function(pred, prey)
        -- Comments on player revealing their pred side
        if pred == Player then
            -- Iso turns that smile upside down
            Sleep(2)
            Storage.SetFlag("ISO_KNOWS_PLAYER_PRED", true)
            Storage.SetFlag("MQ06_AMBUSH_THUG_EATEN", true)
            Log("MQ06_AMBUSH_VORE_PRED01")

            -- Disallow swallowing the other thugs
            for i, t in ipairs(thugs) do
                t.Flags = ECharacterFlags.NoPrey + ECharacterFlags.NoGrapple
            end
        end
        -- End combat if only one thug is left
        CheckCombatEnd()
    end)

    return fight
end

local function MQ06_MakeCombat_Fight1()
    -- Recover
    iso.Health = iso.HealthMax
    rux.Health = rux.HealthMax

    -- Configure fight
    local fight = Combat2()
    local mako = Creature("CR_MQ06_Mako")
    fight:AddParticipant(Player)
    fight:AddParticipant(iso)
    fight:AddParticipant(rux)
    fight:AddParticipant(mako)
    fight:AddParticipant(Creature("CR_MQ06_Ethel"))

    -- Display different dialog for defeating Mako, depending on lethality choice
    fight:OnCreatureKilled(function(killer, victim)
        if victim == mako then
            if Storage.GetFlag("MQ06_PREFER_NONLETHAL") then
                Log("MQ06_ABBEY_FIGHT1_MAKO_NONLETHAL")
            else
                Storage.SetFlag("MAKO_DEAD", true)
                Log("MQ06_ABBEY_FIGHT1_MAKO_LETHAL")
            end
        end
    end)

    return fight
end

local function MQ06_OnPlayerKilled()
    -- Note: we do not need to check CombatSession::IsSwallowed, because OnCreatureVored ends the combat
    if not Storage.GetFlag("MQ06_ABBEY_FIGHT_REVIVED") then
        Sleep(1)
        Log("MQ06_ABBEY_FIGHT2_PLAYER_REVIVE")
        Storage.SetFlag("MQ06_ABBEY_FIGHT_REVIVED", true)
        Sleep(1)
        Player.Health = Player.HealthMax
    else
        -- Combat ends with vore
        Storage.SetFlag("MQ06_ABBEY_PLAYER_SWALLOWED", true)
        GetActiveCombat():End()
    end
end

local function MQ06_MakeCombat_Fight2()
    -- Configure fight
    local fight = Combat2()
    fight:AddParticipant(Player)
    fight:AddParticipant(iso)
    fight:AddParticipant(randall)
    randall.Health = randall.HealthMax -- needed for the HP accessory

    -- If player is eaten, go to Ending C
    fight:OnCreatureVored(function(pred, prey)
        if prey == Player then
            Storage.SetFlag("MQ06_ABBEY_PLAYER_SWALLOWED", true)
            fight:End()
        end
    end)

    -- Just for fun, one retry is allowed
    fight:OnPlayerKilled(MQ06_OnPlayerKilled)

    return fight
end

local function MQ06_MakeCombat_Fight3()
    -- Note: We revive Randall at the start, because showing the bar refill animation is cool
    randall.Health = 0
    iso.Health = iso.HealthMax
    Player.Health = Player.HealthMax

    -- Configure fight
    local fight = Combat2()
    fight:AddParticipant(Player)
    fight:AddParticipant(iso)
    fight:AddParticipant(randall)

    -- At combat start, revive Randall
    fight:OnRoundStart(function(round)
        if round == 1 then
            Sleep(1)
            randall.Health = randall.HealthMax
            Sleep(1.25)
        end
    end)

    -- On half health, show the 'halfway' milestone text
    local halfway_round = 0
    fight:OnRoundEnd(function(round)
        if halfway_round == 0 and randall.Health <= randall.HealthMax / 2 then
            halfway_round = round
            Sleep(1)
            LogSplit()
            Log("MQ06_ABBEY_FIGHT3_HALFWAY01")
            Sleep(4)
            Log("MQ06_ABBEY_FIGHT3_HALFWAY02")
            LogSplit()
            Sleep(4)
        elseif halfway_round ~= 0 and round == halfway_round + 1 then
            -- After one more round, player wins
            fight:End()
        end
    end)

    -- If player is eaten, go to Ending C
    fight:OnCreatureVored(function(pred, prey)
        if prey == Player then
            Storage.SetFlag("MQ06_ABBEY_PLAYER_SWALLOWED", true)
            fight:End()
        end
    end)

    -- Randall can't die
    fight:OnCreatureKilled(function(killer, victim)
        if victim == randall then
            victim.Health = 1
        end
    end)

    -- Just for fun, one retry is allowed
    fight:OnPlayerKilled(MQ06_OnPlayerKilled)

    return fight
end
