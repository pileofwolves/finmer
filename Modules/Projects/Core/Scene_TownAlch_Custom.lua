local potions = {
    "U_PotionHeal",
    "U_PotionHeal",
    "U_PotionDigestResist",
    "U_PotionDigestResist",
    "U_PotionAttack",
    "U_PotionAttack",
    "U_PotionDefense",
    "U_PotionDefense",
    "U_PotionPred",
}

function ShowAlchemyShop()
    local shop = Shop("TownAlchemist")
    shop.Title = "Jar'la"
    shop.RestockInterval = 20
    if shop.RestockRequired then
        shop:MarkRestocked()
        shop:RemoveDefaultStock()
        AddRandomizableShopStock(shop, potions, math.random(4, 5))

        -- Always stock 1 respec potion, so players have the option available
        shop:AddItem(Item("U_PotionRespec"), 1)
    end
    shop:Show()
    shop:Save()
end
