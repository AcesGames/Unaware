
public enum GameStateFlag
{
    GameStarted = 1 << 0,
    GamePaused = 1 << 1,
    GameMenuOpen = 1 << 2,
    InventoryOpen = 1 << 3,
    JournalOpen = 1 << 4,
    MapOpen = 1 << 5,
    DocumentOpen = 1 << 6,
    ChoiceSelectionOpen = 1 << 7,
    InDialogue = 1 << 8,
    InPuzzle = 1 << 9,
    DeathPanelActive = 1 << 10,
    Loading = 1 << 11,
    CinematicPlaying = 1 << 12,
    UsingSecondaryCam = 1 << 13,
    MenuBlocker = 1 << 14,
    IsDead = 1 << 15
}

public enum PuzzleType
{
    Clock = 0,
    Stove = 1,
    Chess = 2,
    Zodiac = 3,
    Cube = 4,
    Grate = 5,
    Workshop = 6,
    Wagon = 7,
    LetterLock = 8
}

public enum SurfaceType
{
    Wood,
    Carpet,
    Rock,
    Grass,
    Gravel,
    Metal,
    Water,
    Flesh
}


public enum InteractableType
{
    Item,
    Door,
    NPC,
    Inaccessable,

}

public enum ItemType
{
    Default = 0,
    Key = 1,
    Consumeable = 2,
    Readable = 3,
}

public enum ReadableType
{
    Document = 0,
    Book = 1,
    Map = 2,
}

public enum ConsumebleType
{
    Food = 0,
    Fuel = 1,
    Ability = 2,
    Misc = 3,
}

public enum Level
{
    Intro,
    House,
    Outside,
    Crypt,
    Mine,
    Church,
    Cabin,
    Lighthouse,
}

public enum LevelArea
{
    HouseStart,
    HouseEntrance,
    ChurchEntranceMain,
    ChurchEntranceAlternate,
    CryptEntrance,
    MineEntrance,
    MineExit,
    LighthouseEntrance,
    LighthouseHatch,
    CabinEntrance
}

public enum TestAreas
{
    MINE_BRIDGE,
    MINE_WORKSHOP,

}

public enum ChessPieces
{
    BlackPawn,
    BlackRook,
    BlackKnight,
    BlackBishop,
    BlackQueen,
    BlackKing,
    WhitePawn,
    WhiteRook,
    WhiteKnight,
    WhiteBishop,
    WhiteQueen,
    WhiteKing,
}

public enum NPCActorStateType
{
    None = 0,
    Reset = 1,
    Idle = 2,
    Patrolling = 3,
    Chasing = 4,
    Searching = 5,
    Fleeing = 6,
    Attacking = 7,
    Eating = 8,
    Playing = 9,
    Talking = 10,
    Dying = 11,
    Giving = 12
}

public enum Area
{
    None,
    House,
    Mine,
    Cabin,
    Church,
    Lighthouse,
    Tavern
}

public enum DestinationType
{
    Player,
    Window,
    Waypoint
}

public enum BaseConsiderationState
{
    Idle,
    Patrolling,
    GoTowardsPlayer,
    GoTowardsWindow,
    PlayViolin
}

public enum AudioType
{
    Music,
    SFX,
    Ambience,
    Voice
}

public enum MusicType
{
    MainTheme,
    Chapter,
    HouseMain,
    HouseBasement,
    Church,
    Mine,
    Crypt,
    Tavern
}

public enum TensionType
{
    Event01,
    Event02,
    Event03,
    Event04,
    Event06,
    Event07,
    Event08,
    Event11,
    Event12,
    Event16,
    Event17,
}

public enum SFXType
{
    JournalOpen,
    JournalClose,
    JournalWrite,
    InventoryOpen,
    InventoryClose,
    PageFlip,
    MenuButton,
    Eat,
    WrongKey,
    ScrollOpen,
    ScrollClose,
    Click,
    Unlocked,
    TurnMoonKey,     
}

