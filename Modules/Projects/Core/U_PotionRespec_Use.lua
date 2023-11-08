-- Note: Keep constants in sync with C# code
local k_AbilityScoreMinimum = 3

-- Count spent ability points
-- (This allows us to also reward points from quests at some point)
local spent =
    (Player.Strength - k_AbilityScoreMinimum) +
    (Player.Agility - k_AbilityScoreMinimum) +
    (Player.Body - k_AbilityScoreMinimum) +
    (Player.Wits - k_AbilityScoreMinimum)

-- Reset ability scores
Player.Strength     = k_AbilityScoreMinimum
Player.Agility      = k_AbilityScoreMinimum
Player.Body         = k_AbilityScoreMinimum
Player.Wits         = k_AbilityScoreMinimum

-- Refund the erased ability points
Player.AbilityPoints = Player.AbilityPoints + spent
