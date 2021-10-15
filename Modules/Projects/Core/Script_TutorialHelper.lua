function BeginGame()
    -- Remove tutorial prey, if any
    Player.PredatorFullness = 0
    Player.TotalPreySwallowed = 0
    Player.TotalPreyDigested = 0

    -- Assign starting equipment
    Player.Money = 7
    Player.EquippedArmor = Item("A_ClothArmor")

    -- Opening text
    ClearLog()
    Log("opening")
    Sleep(2.5)

    -- Warp to game area
    SetScene("Scene_TownMarket")
end
