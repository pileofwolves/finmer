-----------------------------------------------------------------
-- MQ03
-----------------------------------------------------------------

function MQ03_IncrementGossip()
    -- Increment counter
    Storage.ModifyNumber("MQ03_ISO_GOSSIP", 1)

    -- If 'desperation' threshold is reached (player doesn't realize where to go), give hint in journal
    if Storage.GetNumber("MQ03_ISO_GOSSIP") == 3 and not Storage.GetFlag("MQ03_TAVERN_EVENT") then
        Journal.Update("MQ03", 41)
    end
end

function MQ03_SelectAssailant()
    -- Select which one of the two assailants we're using for the tavern event
    if Storage.GetFlag("MQ02_RANDALL_TIGRESS_EATEN") then
        -- Original pred (tigress) was eaten during MQ02
        return "husky", Creature("CR_MQ03_Pred2")
    else
        -- Original pred survived
        return "tigress", Creature("CR_MQ03_Pred1")
    end
end

function MQ03_SetupAssailantContext()
    -- Assign grammar contexts
    local species, pred = MQ03_SelectAssailant()
    Text.SetVariable("assailant_species", species)
    Text.SetContext("assailant", pred)
end
