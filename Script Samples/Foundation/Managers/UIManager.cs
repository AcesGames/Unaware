using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;


public class UIManager : MonoBehaviour
{
    [SerializeField, BoxGroup("UI Panel")]
    private UIGameMenu _gameMenu;
    public UIGameMenu GameMenu => _gameMenu;

    [SerializeField, BoxGroup("UI Panel")]
    private UICredits _credits;
    public UICredits Credits => _credits;

    [SerializeField, BoxGroup("UI Panel")]
    private UIInventory _inventory;
    public UIInventory Inventory => _inventory;

    [SerializeField, BoxGroup("UI Panel")]
    private UIHud _hud;
    public UIHud HUD => _hud;

    [SerializeField, BoxGroup("UI Panel")]
    private UIDialogue _dialogue;
    public UIDialogue Dialogue => _dialogue;

    [SerializeField, BoxGroup("UI Panel")]
    UIMouseCursor _mouseCursor;
    public UIMouseCursor MouseCursor => _mouseCursor;

    [SerializeField, BoxGroup("UI Panel")]
    private UIFade _fade;
    public UIFade Fade => _fade;

    [SerializeField, BoxGroup("UI Panel")]
    private UILoadingScreen _loadingScreen;
    public UILoadingScreen LoadingScreen => _loadingScreen;

    [SerializeField, BoxGroup("UI Panel")]
    private UIVideoPlayer _videoPlayer;
    public UIVideoPlayer VideoPlayer => _videoPlayer;

    [SerializeField, BoxGroup("UI Panel")]
    private UIDamageIndicator _damageIndicator;
    public UIDamageIndicator DamageIndicator => _damageIndicator;

    [SerializeField, BoxGroup("UI Panel")]
    private UICheatLevelChangePanel _cheatChangeLevelDisplay;
    public UICheatLevelChangePanel CheatChangeLevelDisplay => _cheatChangeLevelDisplay;

    [SerializeField, BoxGroup("UI Panel")]
    private UIChapterDisplay _chapterDisplay;

    [SerializeField, BoxGroup("UI Panel")]
    private UIDeathDisplay _deathDisplay;
    public UIDeathDisplay DeathDisplay => _deathDisplay;

    [SerializeField, BoxGroup("UI Panel")]
    private UICinematicDisplay _cinematicDisplay;

    [SerializeField, BoxGroup("UI Panel")]
    private UIPuzzlePanel _puzzlePanel;
    public UIPuzzlePanel PuzzlePanel => _puzzlePanel;

    [SerializeField, BoxGroup("UI Panel")]
    private UIDocumentDisplay _documentDisplay;
    public UIDocumentDisplay Document => _documentDisplay;

    [SerializeField, BoxGroup("UI Panel")]
    private UIJournalDisplay _journalDisplay;

    [SerializeField, BoxGroup("UI Panel")]
    private UIMapDisplay _mapDisplay;
    public UIMapDisplay Map => _mapDisplay;

    public UIInput Input { get; private set; }

    [SerializeField] private Interactable _currentInteractable;
    public Interactable CurrentInteractable => _currentInteractable;

    private PlayerRaycast _playerRaycast;


    private void Start()
    {
        Input = new UIInput(this);
        Input.Init();
        _mouseCursor.ToggleMouseCursor(true);
        _mouseCursor.NormalCursor();
        _playerRaycast = GameInstance.Player.Raycast;
    }

    private void FixedUpdate()
    {
        if (!GameInstance.State.HasFlag(GameStateFlag.GameStarted)) return;

        _currentInteractable = _playerRaycast.LookAtInteractable();

        if (GameInstance.State.IsAllBlocked()) return;

        if (_playerRaycast == null) return;

        if (_currentInteractable != null)
        {
            var layer = _currentInteractable.gameObject.layer;
            var targetLayer = LayerMask.NameToLayer(GlobalConsts.LAYER_INTERACTABLE);

            if (layer == targetLayer)
            {
                GameInstance.UI.HUD.Crosshair.EnableObjectHighlight(_currentInteractable);
            }
            else
            {
                GameInstance.UI.HUD.Crosshair.DisableObjectHighlight();
            }
        }
        else
        {
            GameInstance.UI.HUD.Crosshair.DisableObjectHighlight();
        }
    }


    public void OnCancel()
    {
        if (GameInstance.State.HasFlag(GameStateFlag.DocumentOpen))
            _documentDisplay.Toggle(false);
        else if (GameInstance.State.HasFlag(GameStateFlag.MapOpen))
            OnMap();
        else if (GameInstance.State.HasFlag(GameStateFlag.InventoryOpen))
            OnInventory();
        else if (GameInstance.State.HasFlag(GameStateFlag.InDialogue))
            _dialogue.Toggle(false);
        else if (GameInstance.State.HasFlag(GameStateFlag.JournalOpen))
            OnJournal();
        else if (GameInstance.State.HasFlag(GameStateFlag.UsingSecondaryCam))
            GameInstance.Core.EscapeInteractable();
        else if (GameInstance.State.HasFlag(GameStateFlag.GameMenuOpen))
        {
            if (GameInstance.State.HasFlag(GameStateFlag.MenuBlocker) == false)
            {
                _gameMenu.Toggle(false);
            }
        }
    }

    public void OnLeftClick()
    {
        if (_currentInteractable == null) return;

        bool isDraggedItem = GameInstance.UI.Inventory.GetDraggedItemStack().GetItemData() != null;

        if (isDraggedItem)
            GameInstance.Core.UseItem(_currentInteractable);
        else if (_currentInteractable is InteractableItem)
            _currentInteractable.Examine();
    }

    public void Fire2()
    {

    }

    public void OnInteract()
    {
        if (_currentInteractable == null) return;
        if (GameInstance.State.HasFlag(GameStateFlag.UsingSecondaryCam) || GameInstance.State.HasFlag(GameStateFlag.DocumentOpen)) return;

        GameInstance.Core.Examine(_currentInteractable);
    }

    public void OnJournal()
    {
        if (GameInstance.State.HasFlag(GameStateFlag.DocumentOpen)) return;
        if (GameInstance.State.IsInputBlockerActive()) return;

        bool isJournalOpen = GameInstance.State.ToggleFlag(GameStateFlag.JournalOpen);

        _journalDisplay.Toggle(isJournalOpen);

        if (GameInstance.State.IsAnyUIActive())
            GameInstance.Input.SetExclusiveActionMap(InputManager.ActionMapType.UI);
        else
            GameInstance.Input.SetExclusiveActionMap(InputManager.ActionMapType.Gameplay);
    }

    public void OnInventory()
    {
        if (GameInstance.State.IsInventoryBlocked()) return;

        bool isInventoryOpen = GameInstance.State.ToggleFlag(GameStateFlag.InventoryOpen);
        bool isUsingSecondCam = GameInstance.State.HasFlag(GameStateFlag.UsingSecondaryCam);

        _inventory.Toggle(isInventoryOpen);

        if (!isUsingSecondCam)
        {
            _mouseCursor.ToggleMouseCursor(isInventoryOpen);
            _hud.ToggleHUD(!isInventoryOpen);
        }

        if (GameInstance.State.IsAnyUIActive())
            GameInstance.Input.SetExclusiveActionMap(InputManager.ActionMapType.UI);
        else
            GameInstance.Input.SetExclusiveActionMap(InputManager.ActionMapType.Gameplay);
    }

    public void OnMap()
    {
        if (GameInstance.State.IsInputBlockerActive()) return;

        bool mapAquired = GameInstance.Data.Document.GetDocument(14).Progress == global::Document.DocumentProgress.FOUND;

        if (mapAquired)
        {
            bool isMapOpen = GameInstance.State.ToggleFlag(GameStateFlag.MapOpen);
            _mapDisplay.Toggle(isMapOpen);
        }
        else
        {
            PlayDialogue("Player_No_Map");
        }

        if (GameInstance.State.IsAnyUIActive())
            GameInstance.Input.SetExclusiveActionMap(InputManager.ActionMapType.UI);
        else
            GameInstance.Input.SetExclusiveActionMap(InputManager.ActionMapType.Gameplay);
    }

    public void PlayDialogue(string node)
    {
        Debug.Log("Playing Node: " + node);

        _dialogue.PlayDialouge(node);
    }
}
