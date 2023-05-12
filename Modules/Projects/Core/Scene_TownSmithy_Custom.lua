local weapons = {
    "W_SwordSilver",
}
local armors = {
    "A_LeatherArmor",
    "A_EnduranceArmor",
    "A_MirrorArmor",
    "A_ReactiveArmor",
}
local accessories = {
    "AC_CharmBlock",
    "AC_CharmBody",
    "AC_CharmFight",
    "AC_CharmMissRecover",
}

local function ShowSmithyShop()
    -- Basic configuration
    local shop = Shop("TownSmithy")
    shop.Title = "North Finmer Smithy"
    shop.RestockInterval = 20

    -- Restock logic
    if shop.RestockRequired then
        shop:MarkRestocked()
        shop:RemoveDefaultStock()

        -- Basic weapons and armor stock
        shop:AddItem(Item("A_LeatherArmor"), 1)
        shop:AddItem(Item("W_Dagger"), math.random(1, 3))
        shop:AddItem(Item("W_SwordSteel"), 1)

        -- Randomized special stock
        AddRandomizableShopStock(shop, weapons, math.random(0, 1))
        AddRandomizableShopStock(shop, armors, math.random(1, 2))
        AddRandomizableShopStock(shop, accessories, 3)
    end

    -- Present shop to user
    shop:Show()
    shop:Save()
end
