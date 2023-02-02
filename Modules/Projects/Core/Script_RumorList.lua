------------------------------------------------------------------------------
-- Common eligibility predicates

local p_mq01_not_done       = function() return not Storage.GetFlag("MQ01_DONE") end
local p_mq02_done           = function() return     Storage.GetFlag("MQ02_DONE") end
local p_mq02_not_done       = function() return not Storage.GetFlag("MQ02_DONE") end
local p_mq03_done           = function() return     Storage.GetFlag("MQ03_DONE") end
local p_mq03_not_done       = function() return not Storage.GetFlag("MQ03_DONE") end
local p_mq04_done           = function() return     Storage.GetFlag("MQ04_DONE") end
local p_mq04_not_done       = function() return not Storage.GetFlag("MQ04_DONE") end

------------------------------------------------------------------------------
-- General / always available

Rumor.Add{id = "General01",         weight = 1,     text = "RUMOR_GENERAL01"}
Rumor.Add{id = "General02",         weight = 1,     text = "RUMOR_GENERAL02"}
Rumor.Add{id = "General03",         weight = 1,     text = "RUMOR_GENERAL03"}
Rumor.Add{id = "General04",         weight = 1,     text = "RUMOR_GENERAL04"}
Rumor.Add{id = "General05",         weight = 1,     text = "RUMOR_GENERAL05"}
Rumor.Add{id = "General06",         weight = 1,     text = "RUMOR_GENERAL06"}

------------------------------------------------------------------------------
-- Main quest commentary

Rumor.Add{id = "MQ01_Confusion",    weight = 4,     text = "RUMOR_MQ01_CONFUSION",      check = p_mq01_not_done}
Rumor.Add{id = "MQ02_Bridge",       weight = 2,     text = "RUMOR_MQ02_BRIDGE",         check = p_mq02_not_done}
Rumor.Add{id = "MQ02_Preds",        weight = 1,     text = "RUMOR_MQ02_PREDS",          check = p_mq02_done}
Rumor.Add{id = "MQ02_Supplies",     weight = 2.5,   text = "RUMOR_MQ02_SUPPLIES",       check = p_mq02_not_done}
Rumor.Add{id = "MQ03_ActingMayor",  weight = 1.5,   text = "RUMOR_MQ03_ACTINGMAYOR",    check = p_mq03_not_done}
Rumor.Add{id = "MQ03_TempleEvent",  weight = 1.5,   text = "RUMOR_MQ03_TEMPLE_EVENT",   check = p_mq03_done}
Rumor.Add{id = "MQ03_TobyAlive",    weight = 1.25,  text = "RUMOR_MQ03_TOBY_ALIVE",     check = function() return Storage.GetFlag("MQ03_TOBY_SPARED") and not Storage.GetFlag("MQ05_DONE") end}
Rumor.Add{id = "MQ03_TobyDead",     weight = 1.25,  text = "RUMOR_MQ03_TOBY_DEAD",      check = function() return Storage.GetFlag("TOBY_DEAD") end}
Rumor.Add{id = "MQ04_Amber",        weight = 1,     text = "RUMOR_MQ04_AMBER",          check = p_mq04_not_done}
Rumor.Add{id = "MQ05_TobyAlive",    weight = 1.25,  text = "RUMOR_MQ05_TOBY_ALIVE",     check = function() return Storage.GetFlag("MQ05_DONE") and Storage.GetFlag("MQ03_TOBY_SPARED") end}

------------------------------------------------------------------------------
-- Side quest commentary

Rumor.Add{id = "SQ02_Chip",         weight = 1.5,   text = "RUMOR_SQ02_CHIP"}
Rumor.Add{id = "SQ04_Champion",     weight = 1.5,   text = "RUMOR_SQ04_CHAMPION",       check = function() return Storage.GetFlag("TOWN_PIT_OPP4_WON") end}
Rumor.Add{id = "SQ04_Unlocked",     weight = 1.5,   text = "RUMOR_SQ04_UNLOCKED",       check = function() return Storage.GetNumber("SQ04") ~= 0 end}
