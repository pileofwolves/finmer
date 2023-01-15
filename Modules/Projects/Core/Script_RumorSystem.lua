Rumor = {}

local all_rumors = {}
local active_rumors = {}

function Rumor.Add(args)
    -- Validate main input
    if type(args) ~= "table" then
        error("argument to Rumor.Add must be table") end

    -- Validate qualified inputs
    args.weight = args.weight or 1
    if type(args.id) ~= "string" then
        error("id argument to Rumor.Add must be string") end
    if type(args.text) ~= "string" then
        error("text argument to Rumor.Add must be string") end
    if type(args.weight) ~= "number" then
        error("weight argument to Rumor.Add must be number or nil") end
    if type(args.check) ~= "function" and type(check) ~= "nil" then
        error("check argument to Rumor.Add must be function or nil") end

    -- Add the new rumor to the set
    table.insert(all_rumors, {
        id = args.id,
        text = args.text,
        weight = args.weight,
        check = args.check
    })
end

function Rumor.Remove(id)
    -- Search the table for an entry with a matching ID
    for i, v in ipairs(all_rumors) do
        if v.id == id then
            -- Remove the match
            table.remove(all_rumors, i)
            return
        end
    end
end

function Rumor.RemoveAll()
    all_rumors = {}
    active_rumors = {}
end

function Rumor.Shuffle()
    -- The rumor system cannot be used if no rumors are registered
    if #all_rumors == 0 then error("rumor table empty, cannot shuffle") end

    -- Wipe the current active table
    active_rumors = {}

    -- Shuffle all_rumors into a new active table
    for i, v in ipairs(all_rumors) do
        local random_pos = math.random(1, i)
        table.insert(active_rumors, random_pos, v)
    end
end

local function SumEligibleWeights()
    -- Find the sum of the remaining weights
    local sum_of_weights = 0
    local filtered = {}
    for k, v in pairs(active_rumors) do
        -- Discard rumors that are ineligible according to their check function. Note that we
        -- cannot use table.remove because this shifts the integer keys around, breaking iteration.
        if v.check == nil or v.check() then
            -- This rumor is eligible; note its weight value
            sum_of_weights = sum_of_weights + v.weight
            table.insert(filtered, v)
        end
    end

    -- Replace the rumor set with the filtered, eligibility-checked set
    active_rumors = filtered

    return sum_of_weights
end

function Rumor.Get()
    -- Lazy-load the shuffled list as soon as we need it
    if #active_rumors == 0 then
        Rumor.Shuffle()
    end

    -- Total the eligible weights, and remove ineligible rumors
    local sum_of_weights = SumEligibleWeights()
    if #active_rumors == 0 then
        -- Try refreshing once, to re-add used rumors
        Rumor.Shuffle()
        sum_of_weights = SumEligibleWeights()

        -- If we still have no eligible entries even after a full refresh, the rumor table is broken
        if #active_rumors == 0 then
            error("no eligible rumors, cannot select random rumor")
        end
    end

    -- Roll a random number in this range
    local roll = math.random(0, sum_of_weights * 1000) / 1000
    for i, v in ipairs(active_rumors) do
        -- Find the entry with the cumulative weight total we just rolled
        if roll <= v.weight then
            -- Remove the rumor, so it won't be used again, and return its content
            table.remove(active_rumors, i)
            return v.text
        end

        -- Examine the next entry
        roll = roll - v.weight
    end

    -- Fallback: this should not happen, but if no entries fell within the range due to some
    -- weird FP rounding error, just grab the last one (effectively still random) instead.
    return table.remove(active_rumors).text
end

function Rumor.Present(mill)
    Text.SetContext("mill", mill)
    Log(Rumor.Get())
end
