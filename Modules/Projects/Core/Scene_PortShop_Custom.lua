-- If Toby was spared during MQ03, he shows up here after MQ05 is complete
local toby_in_shop = Storage.GetFlag("MQ05_DONE") and not Storage.GetFlag("TOBY_DEAD")

-- Special shop stock
local weapons = {
    "W_BrassKnuckles",
    "W_WarHammer",
}
local armors = {
    "A_DodgeArmor",
}
local accessories = {
    "AC_CharmBlock",
    "AC_CharmBody",
    "AC_CharmFight",
    "AC_CharmPoisonAtk",
}

local function ShowGeneralShop()
    -- Basic configuration
    local shop = Shop("PortShop")
    shop.Title = "Pat"
    shop.RestockInterval = 20

    -- Restock logic
    if shop.RestockRequired then
        shop:MarkRestocked()
        shop:RemoveDefaultStock()

        -- Default stock
        shop:AddItem(Item("U_FoodBread"), math.random(1, 3))
        shop:AddItem(Item("U_FoodCheese"), math.random(0, 3))
        shop:AddItem(Item("U_FoodGrapes"), math.random(1, 3))
        shop:AddItem(Item("U_PotionHeal"), math.random(1, 3))
        shop:AddItem(Item("A_ClothArmor"), 1)
        shop:AddItem(Item("A_LeatherArmor"), 1)

        -- Randomized special stock
        AddRandomizableShopStock(shop, weapons, 1)
        AddRandomizableShopStock(shop, armors, 1)
        AddRandomizableShopStock(shop, accessories, 3)
    end

    -- Present shop to user
    shop:Show()
    shop:Save()
end
