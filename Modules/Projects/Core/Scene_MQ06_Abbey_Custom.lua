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
    local iso = Creature("CR_MQ06_Iso"); iso.IsAlly = true
    local rux = Creature("CR_MQ06_Rux"); rux.IsAlly = true
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
