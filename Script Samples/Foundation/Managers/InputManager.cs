using UnityEngine;
using static UnawareInputActions;


public class InputManager : MonoBehaviour
{
    private UnawareInputActions _inputActions;

    public GameplayActions GameplayActionMap => _inputActions.Gameplay;
    public UIActions UIActionMap => _inputActions.UI;

    public enum ActionMapType
    {
        None,
        Gameplay,
        UI,
    }

    public ActionMapType CurrentActionMap { get; private set; }

    public void Init()
    {
        _inputActions = new UnawareInputActions();
        _inputActions.Enable();
        SetExclusiveActionMap(CurrentActionMap = ActionMapType.Gameplay);
    }

    public void Shutdown()
    {
        _inputActions.Disable();
        _inputActions.Dispose();
    }

    public void SetExclusiveActionMap(ActionMapType type)
    {
        switch (type)
        {
            case ActionMapType.None:
                _inputActions.Gameplay.Disable();
                _inputActions.UI.Disable();
                break;
            case ActionMapType.Gameplay:
                _inputActions.Gameplay.Enable();
                _inputActions.UI.Disable();
                break;
            case ActionMapType.UI:
                _inputActions.Gameplay.Disable();
                _inputActions.UI.Enable();
                break;
        }

        CurrentActionMap = type;
    }
}