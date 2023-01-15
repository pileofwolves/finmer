------------------------------------------------------------------------------
-- Common eligibility predicates

local p_mq01_not_done       = function() return not Storage.GetFlag("MQ01_DONE") end
local p_mq02_done           = function() return     Storage.GetFlag("MQ02_DONE") end
local p_mq02_not_done       = function() return not Storage.GetFlag("MQ02_DONE") end
local p_mq03_not_done       = function() return not Storage.GetFlag("MQ03_DONE") end

------------------------------------------------------------------------------
-- General / always available

Rumor.Add{id = "General01",         weight = 1,     text = "RUMOR_GENERAL01"}
Rumor.Add{id = "General02",         weight = 1,     text = "RUMOR_GENERAL02"}

------------------------------------------------------------------------------
-- Main quest commentary

Rumor.Add{id = "MQ01_Confusion",    weight = 4,     text = "RUMOR_MQ01_CONFUSION",      check = p_mq01_not_done}
Rumor.Add{id = "MQ02_Bridge",       weight = 2,     text = "RUMOR_MQ02_BRIDGE",         check = p_mq02_not_done}
Rumor.Add{id = "MQ02_Preds",        weight = 1,     text = "RUMOR_MQ02_PREDS",          check = p_mq02_done}
Rumor.Add{id = "MQ03_ActingMayor",  weight = 1.5,   text = "RUMOR_MQ03_ACTINGMAYOR",    check = p_mq03_not_done}
