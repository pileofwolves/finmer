SetLocation("South Finmer, General Store")
SetInventoryEnabled(true)

-- Print this text only once on scene entry. We could also do this in the Main node, but several conversation
-- nodes link back to Main, and we wouldn't want to print the 'welcome' text again, so we do it here instead.
if Storage.GetFlag("PORT_SHOP_FIRST") then
    Log("PORT_SHOP_ENTER")

    -- Toby says hi
    if toby_in_shop and Storage.GetFlag("PORT_SHOP_TOBY_FIRST") then
        Log("PORT_SHOP_TOBY_ANGSTY")
    end
end
