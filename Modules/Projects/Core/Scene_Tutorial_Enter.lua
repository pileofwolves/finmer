SetLocation("")
SetInventoryEnabled(false)
ClearLog()

-- If Explorer Mode is on, temporarily remove the cheat ring; it will be restored by TutorialHelper
if Player.IsExplorerModeEnabled then
    Player.EquippedAccessory2 = nil
end
