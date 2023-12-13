Encounter = {}

local groups = {}

function Encounter.Add(args)
    -- Validate main input
    if type(args) ~= "table" then
        error("argument to Encounter.Add must be table") end

    -- Validate qualified inputs
    args.weight = args.weight or 1
    if type(args.group) ~= "string" then
        error("group argument to Encounter.Add must be string") end
    if type(args.weight) ~= "number" then
        error("weight argument to Encounter.Add must be number or nil") end
    if type(args.fn) ~= "function" and type(args.fn) ~= "nil" then
        error("fn argument to Encounter.Add must be function or nil") end
    if type(args.check) ~= "function" and type(args.check) ~= "nil" then
        error("check argument to Encounter.Add must be function or hnil") end

    -- Find the group
    local g = groups[args.group]
    if g == nil then
        -- If the group has not yet been created, do so now
        g = {}
        groups[args.group] = g
    end

    -- Add the encounter function to the table
    table.insert(g, {
        weight = args.weight,
        check = args.check,
        fn = args.fn
    })
end

function Encounter.Roll(group)
    -- Input validation
    if type(group) ~= "string" then
        error("group argument to Encounter.Roll must be string") end

    -- Find the group
    local g = groups[group]
    if g == nil or #g == 0 then
        -- Cannot roll encounters for a group that has not been registered at all
        error("encounter group " .. group .. " is empty")
    end

    -- Filter out all ineligible encounters, and sum the weights of those that remain
    local sum_of_weights = 0
    local eligible = {}
    for k, v in pairs(g) do
        if v.check == nil or v.check() then
            sum_of_weights = sum_of_weights + v.weight
            table.insert(eligible, v)
        end
    end

    -- If there are no eligible encounters, we're done
    if #eligible == 0 then return nil end

    -- Roll a random number in this range
    local roll = math.random(0, sum_of_weights * 1000) / 1000
    for i, v in ipairs(eligible) do
        -- Find the entry with the cumulative weight total we just rolled
        if roll <= v.weight then return v.fn end

        -- Examine the next entry
        roll = roll - v.weight
    end

    -- Fallback
    return nil
end
