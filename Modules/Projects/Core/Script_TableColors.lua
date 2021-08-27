-- define a function for creating color tables
Color = setmetatable({}, {
    __call = function(self, r, g, b)
        return {r = r, g = g, b = b}
    end
})

-- define a few common colors
Color.Default = Color(0xE6, 0xE5, 0xE5)
Color.White = Color(255, 255, 255)
Color.Black = Color(0, 0, 0)
Color.Red = Color(255, 0, 0)
Color.DarkCyan = Color(8, 139, 139)
Color.Highlight = Color(0x1E, 0x90, 0xFF)
Color.LightBlue = Color(30, 144, 255)
Color.Gray = Color(128, 128, 128)
Color.LightGray = Color(160, 160, 160)
Color.DarkGray = Color(96, 96, 96)
Color.Error = Color.Red
Color.Hostile = Color(0xCF, 0x21, 0x21)
Color.Neutral = Color(0x70, 0x70, 0xFF)
Color.Friendly = Color(0x20, 0xBB, 0x20)