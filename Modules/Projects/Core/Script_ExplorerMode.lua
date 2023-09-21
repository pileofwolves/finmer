local k_ExplorerItem = "AC_ExplorerModeRing"

-- Obtain the asset names of the items in the accessory slots
local slot1 = Player.EquippedAccessory1 and Player.EquippedAccessory1.AssetName or nil
local slot2 = Player.EquippedAccessory2 and Player.EquippedAccessory2.AssetName or nil

-- The player is granted a cheat item while Explorer Mode is enabled
if Player.IsExplorerModeEnabled then
    -- First, check if the player already has the accessory equipped or in the inventory
    if slot1 == k_ExplorerItem or slot2 == k_ExplorerItem or Player:HasItem(k_ExplorerItem) then
        -- Player has the item already, so bail
        return
    end

    -- Tag save data, for debugging purposes
    Storage.SetFlag("EXPLORER_MODE_USED", true)

    -- Player does not have the item yet; try to equip it in one of the accessory slots
    if slot2 == nil then
        Player.EquippedAccessory2 = Item(k_ExplorerItem)
    elseif slot1 == nil then
        Player.EquippedAccessory1 = Item(k_ExplorerItem)
    else
        -- Both accessory slots are occupied; just add the item to the inventory instead
        Player:GiveItem(k_ExplorerItem)
    end
else
    -- Explorer Mode is disabled; remove the ring
    if slot1 == k_ExplorerItem then
        Player.EquippedAccessory1 = nil
    elseif slot2 == k_ExplorerItem then
        Player.EquippedAccessory2 = nil
    else
        Player:TakeItem(k_ExplorerItem)
    end
end
