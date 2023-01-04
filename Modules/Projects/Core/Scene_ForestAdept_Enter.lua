-- Initial state for the 'first time entry' path; these are overridden by Entry_R_Main.
SetLocation("Forest, Strange Path")
SetInventoryEnabled(false)

-- Entry main node is used as a hub, so it shouldn't show extensive text about entering
-- the area. Instead, to make sure enter text only shown once, we do that here.
if Storage.GetFlag("FOREST_ADEPT_FIRST") then
    Log("MQ04_ADEPT_ENTER01")

    if not Storage.GetFlag("FOREST_DIREWOLF_DEAD") then
        Log("MQ04_ADEPT_ENTER_CHIP")
    end
end
