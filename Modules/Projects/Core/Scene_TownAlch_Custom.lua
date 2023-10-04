local potions = {
    "U_PotionHeal1",
    "U_PotionHeal1",
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
    end
    shop:Show()
    shop:Save()
end
