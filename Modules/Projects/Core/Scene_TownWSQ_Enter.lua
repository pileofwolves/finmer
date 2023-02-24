SetLocation("North Finmer, West")
SetInventoryEnabled(true)

-- for the snitch scene, the fluff text is undesirable (because we're just jumping between scenes to reuse the token vore scene)
if not Storage.GetFlag("town_pit_refused_snitch") then
    Log("town_wsq")

    -- don't add in the fluff text every time
    if math.random(0, 1) == 1 then
        Log(GetIsNight() and "town_sq_night" or "town_sq_day")
    end
end