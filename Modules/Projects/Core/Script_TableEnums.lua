-- Most of these enums match those defined in C#; be sure to keep both sides up to date when making changes here.

ECompass = {
    North           = 0,
    West            = 1,
    South           = 2,
    East            = 3
}

ESize = {
    Tiny            = 0,
    Small           = 1,
    Medium          = 2,
    Large           = 3,
    Huge            = 4,
}

EItemType = {
    Generic         = 0,
    Equipable       = 1,
    Usable          = 2
}

EGender = {
    Male            = 0,
    Female          = 1,
    Neuter          = 2,
    Herm            = 3,
}

ECharacterFlags = {
    None            = 0,
    NoGrapple       = 1,
    NoPrey          = 2,
    NoFight         = 4,
    NoXP            = 8,
    SkipTurns       = 16,
}

-- Backwards compatibility
Compass = ECompass
