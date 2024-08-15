using UnityEngine;

public class CubePuzzleButton : Interactable
{
    [SerializeField] private CubePuzzle _cubePuzzle;
    [SerializeField] private CubePuzzleSocketTrigger _socketTrigger;

    public void ClickButton()
    {
        _cubePuzzle.ClickOnButtons(_socketTrigger);
    }
}
