function BeginGame()
    -- Remove tutorial prey, if any
    Player:ClearStomach()
    Player.TotalPreySwallowed = 0
    Player.TotalPreyDigested = 0

    -- Opening text
    ClearLog()
    Log("opening")
    Sleep(2.5)

    -- Warp to game area
    SetScene("Scene_TownMarket")
end