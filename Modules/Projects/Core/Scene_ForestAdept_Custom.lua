-- Utility function for checking time
-- The scene is fatal if no time passed since Chip's last meal
local function IsChipVoreFatal()
    return Storage.GetNumber("MQ04_CHIP_LAST_ATE") == GetTimeHourTotal()
end

-- Shop stock
local potions = {
    "U_PotionHeal1",
    "U_PotionHeal1",
    "U_PotionDigestResist",
    "U_PotionAttack",
    "U_PotionDefense",
}
local function ShowRuxShop()
    -- Basic configuration
    local shop = Shop("ForestAdept")
    shop.Title = "Rux"
    shop.RestockInterval = 22

    -- Restock logic
    if shop.RestockRequired then
        shop:MarkRestocked()
        shop:RemoveDefaultStock()
        shop:AddItem(Item("AC_CharmPrey"), 1)
        shop:AddItem(Item("AC_CharmPoisonDef"), 1)
        shop:AddItem(Item("AC_CharmArmorBreak"), 1)
        AddRandomizableShopStock(shop, potions, math.random(2, 3))
    end

    -- Present shop to user
    shop:Show()
    shop:Save()
end
