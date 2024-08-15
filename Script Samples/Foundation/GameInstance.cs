using UnityEngine;


public class GameInstance : MonoBehaviour
{
    public static GameInstance INSTANCE;

    [SerializeField] private CameraManager _cameraManager;
    [SerializeField] private LevelManager _levelManager;
    [SerializeField] private Localization _localizationManager;
    [SerializeField] private DataManager _dataManager;
    [SerializeField] private UIManager _uiManager;
    [SerializeField] private InventoryManager _inventoryManager;
    [SerializeField] private SoundManager _soundManager;
    [SerializeField] private SaveManager _saveManager;
    [SerializeField] private CoreFunctions _coreManager;
    [SerializeField] private PlayerManager _playerManager;
    [SerializeField] private InputManager _inputManager;


    public static CameraManager Camera { get; private set; }
    public static LevelManager Level { get; private set; }
    public static Localization Localization { get; private set; }
    public static DataManager Data { get; set; }
    public static UIManager UI { get; private set; }
    public static InventoryManager Inventory { get; private set; }
    public static SoundManager Sound { get; private set; }
    public static SaveManager Save { get; private set; }
    public static CoreFunctions Core { get; private set; }
    public static PlayerManager Player { get; private set; }
    public static InputManager Input { get; private set; }
    public static GameState State { get; private set; }

    public GameType Type;

    public enum GameType
    {
        TestDefault,
        TestMenu,
        Demo,
        Chapters
    };


    private void Awake()
    {
        if (INSTANCE == null)
        {
            INSTANCE = this;
        }
        else if (INSTANCE != null)
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);

        Init();
        SettingUpTheGame();
    }

    private void Init()
    {
        Camera = _cameraManager;
        Level = _levelManager;
        Localization = _localizationManager;
        Data = _dataManager;
        UI = _uiManager;
        Sound = _soundManager;
        //SaveManager = _saveManager;
        Core = _coreManager;
        Inventory = _inventoryManager;
        Player = _playerManager;
        Input = _inputManager;

        State ??= new();
        Input.Init();
    }

    private void SettingUpTheGame()
    {
        if (Type == GameType.TestDefault)
        {
            Core.ToggleCheats(true);
            State.SetFlag(GameStateFlag.GameStarted);
        }
        if (Type == GameType.Chapters || Type == GameType.Demo || Type == GameType.TestMenu)
        {
            State.SetFlag(GameStateFlag.MenuBlocker);
            UI.HUD.PlayerBars.ModifyHealth(-90);
            UI.GameMenu.Toggle(true);
        }
    }

    public void TogglePauseGame(bool toggle)
    {
        if (toggle)
        {
            Time.timeScale = 0;
        }
        else
        {
            Time.timeScale = 1;
        }
    }
}
