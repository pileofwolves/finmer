local combat
local numParticipants
local hasPlayer

-- Flag for disabling various buggy/testing menu options
local isPublicCombatDemo = true

-- Helper function for adding a participant to both the combat state and the counter variable
local function AddParticipant(c)
    numParticipants = numParticipants + 1

    -- Add number to name for clarity
    if c ~= Player then
        c.Name = c.Name .. " " .. tostring(numParticipants)
        c.Alias = c.Name
    end

    combat:AddParticipant(c)
    print("Added " .. c.Name .. " to combat, now " .. numParticipants .. " total participants")
end

-- Helper function for resetting the combat state
local function ResetCombat()
    combat = Combat2()
    numParticipants = 0
    hasPlayer = true -- TEMP
    combat:AddParticipant(Player)
end

ResetCombat()