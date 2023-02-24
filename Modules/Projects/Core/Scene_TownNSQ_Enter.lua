SetLocation("North Finmer, North")
Log("town_nsq")

-- don't add in the fluff text every time
if math.random(0, 1) == 1 then
    Log(GetIsNight() and "town_sq_night" or "town_sq_day")
end