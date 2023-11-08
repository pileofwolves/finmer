-- To be reused by State nodes for encounters
local function ShowContinueCompassLink()
    local dir = Storage.GetNumber("ROADENC_DIR")
    local dest = Storage.GetString("ROADENC_DEST")
    AddLink(dir, dest)
end
