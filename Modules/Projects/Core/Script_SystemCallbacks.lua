-- These functions are called directly from the game core. Don't change their
-- names or argument lists without updating calls in Lua::Initialize and Lua::ReloadPlayer.

function OnReloadPlayer()
    -- persistent context reference for player object
    Text.SetContext("player", Player, true)

    Text.SetGlobalSubstitute("name", Player.Name)
    Text.SetGlobalSubstitute("species", UncapFirst(Player.Species))
    Text.SetGlobalSubstitute("Species", CapFirst(Player.Species))

    -- these are placeholders, to facilitate future support for scalies etc
    -- so you can (and should) use %furry and such in your messages
    Text.SetGlobalSubstitute("furry", "furry")
    Text.SetGlobalSubstitute("fur", "fur")

    -- some other pronouns
    Text.SetGlobalSubstitute("mister", Player.Gender == Gender.Male and "mister" or "miss")
    Text.SetGlobalSubstitute("lad", Player.Gender == Gender.Male and "lad" or "lass")
end