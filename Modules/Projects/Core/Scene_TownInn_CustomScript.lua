-- balancing variables
local room_price = 5
local drink_price = 1

-- on first encounter, generate a random canine species for the barkeep
if not Storage.GetFlag("barkeep_generated") then
    Storage.SetString("barkeep_species", Text.GetString("SPECIES_CANINE"))
    Storage.SetString("foxtaur_partner_species", Text.GetString("SPECIES_FELINE"))
    Storage.SetFlag("barkeep_generated", true)
end
-- keep it around in a variable
local barkeep = Storage.GetString("barkeep_species")
local ftpartner = Storage.GetString("foxtaur_partner_species")