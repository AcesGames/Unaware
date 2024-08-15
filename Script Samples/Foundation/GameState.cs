using System;
using UnityEngine;


public class GameState
{
    private GameStateFlag _gameStateCurrent;
    public GameStateFlag Current => _gameStateCurrent;


    public void SetFlag(GameStateFlag state) => _gameStateCurrent |= state;

    public void UnsetFlag(GameStateFlag state) => _gameStateCurrent &= ~state;

    public bool ToggleFlag(GameStateFlag state)
    {
        if ((_gameStateCurrent & state) == state)
            UnsetFlag(state);
        else
            SetFlag(state);

        return HasFlag(state);
    }

    public bool HasFlag(GameStateFlag state) => _gameStateCurrent.HasFlag(state);
    public bool HasOneOrMoreFlags(GameStateFlag state) => (_gameStateCurrent & state) != 0;

    private GameStateFlag AnyUIFlags
    {
        get
        {
            return
                GameStateFlag.GameMenuOpen |
                GameStateFlag.UsingSecondaryCam |
                CloseableUIFlags |
                NonCloseableFlags;
        }
    }

    private GameStateFlag CloseableUIFlags
    {
        get
        {
            return
                GameStateFlag.InventoryOpen |
                GameStateFlag.JournalOpen |
                GameStateFlag.MapOpen |
                GameStateFlag.DocumentOpen |
                GameStateFlag.InDialogue |
                GameStateFlag.InPuzzle |
                GameStateFlag.ChoiceSelectionOpen;
        }
    }

    private GameStateFlag NonCloseableFlags
    {
        get
        {
            return
                GameStateFlag.DeathPanelActive |
                GameStateFlag.Loading |
                GameStateFlag.CinematicPlaying;
        }
    }

    private GameStateFlag InputBlocker
    {
        get
        {
            return
                GameStateFlag.InDialogue |
                GameStateFlag.GameMenuOpen |
                GameStateFlag.ChoiceSelectionOpen |
                GameStateFlag.IsDead |
                NonCloseableFlags;
        }
    }

    private GameStateFlag AllBlocker
    {
        get
        {
            return
                InputBlocker |
                CloseableUIFlags |
                NonCloseableFlags;
        }
    }

    private GameStateFlag InventoryBlocker
    {
        get
        {
            return
                GameStateFlag.JournalOpen |
                GameStateFlag.MapOpen |
                GameStateFlag.DocumentOpen |
                InputBlocker;
        }
    }

    public bool IsAnyUIActive() => HasOneOrMoreFlags(AnyUIFlags);

    public bool IsCloseableUIActive() => HasOneOrMoreFlags(CloseableUIFlags);

    public bool IsNonClosableUIActive() => HasOneOrMoreFlags(NonCloseableFlags);

    public bool IsInputBlockerActive() => HasOneOrMoreFlags(InputBlocker);

    public bool IsAllBlocked() => HasOneOrMoreFlags(AllBlocker);

    public bool IsInventoryBlocked() => HasOneOrMoreFlags(InventoryBlocker);

    public void DebugPrintCheckFlags()
    {
        foreach (GameStateFlag flag in Enum.GetValues(typeof(GameStateFlag)))
        {
            if ((_gameStateCurrent & flag) == flag)
            {
                Debug.Log("Flags active: " + flag);
            }
        }
    }
}
