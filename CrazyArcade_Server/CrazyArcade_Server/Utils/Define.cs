public class Define
{

    public const int MAX_PLAYER = 8;
    public enum EventType
    {

    }

    public enum Scene
    {
        Unknown,
        TitleScene,
        LobbyScene,
        GameRoomScene,
        LoadingScene,
        GameScene,
    }

    public enum Map
    {
        Block,
        Desert
    }

    public enum Sound
    {
        Bgm,
        SubBgm,
        Effect,
        Max,
    }

    public enum TouchEvent
    {
        PointerUp,
        PointerDown,
        Click,
        LongPressed,
        BeginDrag,
        Drag,
        EndDrag,
        Enter,
    }

    public enum Language
    {
        Korean,
        English,
        French,
        SimplifiedChinese,
        TraditionalChinese,
        Japanese,
    }

    public enum ErrorMessage
    {
        Level,
        Class,
        InventoryFull,
        Etc,
    }

}

public static class SortingLayers
{
    public const int HERO = 300;
}

public static class AnimName
{
    public const string IDLE = "idle";
}