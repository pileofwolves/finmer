-- On first encounter, generate a random species for some patrons
if not Storage.GetFlag("TOWN_INN_PATRONS_GENERATED") then
    Storage.SetString("TOWN_FOXTAUR_PARTNER_SPECIES", Text.GetString("SPECIES_FELINE"))
    Storage.SetFlag("TOWN_INN_PATRONS_GENERATED", true)
end

-- Prepare text variables
local barkeep = Creature("CR_RandomAnthro")
barkeep.Name = "alsatian"
barkeep.Alias = "the shepherd"
barkeep.Gender = EGender.Male
local foxtaur_partner = Creature("CR_RandomAnthro")
foxtaur_partner.Alias = Storage.GetString("TOWN_FOXTAUR_PARTNER_SPECIES")
