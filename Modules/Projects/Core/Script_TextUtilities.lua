function CapFirst(str)
    return str:gsub("^%l", string.upper)
end

function UncapFirst(str)
    return str:gsub("^%l", string.lower)
end