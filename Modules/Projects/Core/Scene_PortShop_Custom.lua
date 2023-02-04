-- If Toby was spared during MQ03, he shows up here after MQ05 is complete
local toby_in_shop = Storage.GetFlag("MQ05_DONE") and not Storage.GetFlag("TOBY_DEAD")
