using Sirenix.OdinInspector;
using UnityEngine;


public class DataManager : MonoBehaviour
{
    [SerializeField]
    private AchievementManager _achievementManager;
    public AchievementManager Achievement => _achievementManager;

    [SerializeField] private QuestManager _questManager;
    public QuestManager Quest => _questManager;

    [SerializeField] private DocumentManager _documentManager;
    public DocumentManager Document => _documentManager;

    [SerializeField] private GameStats _gameStats;
    public GameStats Stats => _gameStats;

    //[SerializeField] private ChoiceManager _choiceManager;
    //public ChoiceManager Choice => _choiceManager;

    //[SerializeField] private GameStats _gameStats;
    //public GameStats Stats => _gameStats;

    private VariableDatabase _variableDB = new();

    private bool _readyForOutside;

    private const string FOLDER_PATH_ACHIEVEMENTS = "Assets/Settings/UI/Achievements";
    private const string FOLDER_PATH_QUESTS = "Assets/Settings/UI/Quests";
    private const string FOLDER_PATH_DOCUMENTS = "Assets/Settings/UI/Documents";
    private const string FOLDER_PATH_CHOICES = "Assets/Settings/UI/Choices";

    private void Awake()
    {
        // Set Bools
        _variableDB.SetValue(GlobalConsts.KEY_EQUIPPED_BOOTS, false);

        _variableDB.SetValue(GlobalConsts.KEY_EQUIPPED_COMPASS, false);
        _variableDB.SetValue(GlobalConsts.KEY_EQUIPPED_COAT, false);
        _variableDB.SetValue(GlobalConsts.KEY_EQUIPPED_LANTERN, false);

        _variableDB.SetValue(GlobalConsts.KEY_DIARY_COLLECTED, false);
        //  _variableDB.SetValue("hasQueen", false);

        // Set Ints
        //_variableDB.SetValue(GlobalConsts.KEY_COLLECTED_CHESSPIECES, 0);
        //_variableDB.SetValue(GlobalConsts.KEY_COLLECTED_CUBES, 1);
        //_variableDB.SetValue(GlobalConsts.KEY_COLLECTED_LOGS, 0);
        //_variableDB.SetValue(GlobalConsts.KEY_COLLECTED_PAPER, 0);
        //_variableDB.SetValue(GlobalConsts.KEY_COLLECTED_MATCHES, 0);
        //_variableDB.SetValue(GlobalConsts.KEY_COLLECTED_CROSSES, 0);
        //_variableDB.SetValue(GlobalConsts.KEY_COLLECTED_TABLETS, 0);
        //_variableDB.SetValue(GlobalConsts.KEY_COLLECTED_DISCS, 0);

        //_variableDB.SetValue(GlobalConsts.KEY_COLLECTED_HAMMER, 0);
        //_variableDB.SetValue(GlobalConsts.KEY_COLLECTED_SAW, 0);
        //_variableDB.SetValue(GlobalConsts.KEY_COLLECTED_NAILS, 0);
        //_variableDB.SetValue(GlobalConsts.KEY_COLLECTED_PLANKS, 0);

        //_variableDB.SetValue(GlobalConsts.KEY_COLLECTED_MOONKEYS, 0);
    }

    public VariableDatabase GetVariableDB()
    {
        return _variableDB;
    }

    public void DebugVariableDatabase()
    {
        foreach (var item in _variableDB.GetDict())
        {
            Debug.Log("Key: " + item.Key + "Value: " + item.Value);
        }
    }

    public bool CheckFrontdoorObjective()
    {
        bool compassEquipped = GameInstance.Data.GetVariableDB().CreateOrGetBoolValue(GlobalConsts.KEY_EQUIPPED_COMPASS);
        bool coatEquipped = GameInstance.Data.GetVariableDB().CreateOrGetBoolValue(GlobalConsts.KEY_EQUIPPED_COAT);
        bool lanternEquipped = GameInstance.Data.GetVariableDB().CreateOrGetBoolValue(GlobalConsts.KEY_EQUIPPED_LANTERN);
        bool isMapCollected = Document.IsDocumentFound(14);
        bool fullHealth = GameInstance.UI.HUD.PlayerBars.HasFullHealth();

        bool isTaskComplete = compassEquipped && coatEquipped && isMapCollected && lanternEquipped && fullHealth;
        //Debug.Log("Equipping compass: " + compassEquipped);
        //Debug.Log("Equipping coat: " + coatEquipped);
        //Debug.Log("Equipping lantern :" + lanternEquipped);
        //Debug.Log("Collected map :" + isMapCollected);
        //Debug.Log("Full HP: " + fullHealth);

        if (isTaskComplete && !_readyForOutside)
        {
            _readyForOutside = true;
            GameInstance.UI.PlayDialogue("Player_Ready_To_Leave_House");
        }

        return isTaskComplete;
    }

#if UNITY_EDITOR
    [Button]
    private void CacheAllData()
    {
        _achievementManager.CacheAllAchievements(FOLDER_PATH_ACHIEVEMENTS);
        _questManager.CacheAllQuests(FOLDER_PATH_QUESTS);
        _documentManager.CacheAllDocuments(FOLDER_PATH_DOCUMENTS);
    }
#endif
}
