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
