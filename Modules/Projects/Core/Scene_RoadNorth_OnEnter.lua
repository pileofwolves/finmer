-- if MQ02 is done, then we can start checking for bridge repairs
if Storage.GetFlag("mq02_done") then
    -- re-evaluate the bridge repair timer
    if GetBridgeTimeLeft() <= 0 then
        Storage.SetFlag("bridge_is_repaired", true)
    end
end

SetLocation("Road near North Finmer")
SetInventoryEnabled(true)