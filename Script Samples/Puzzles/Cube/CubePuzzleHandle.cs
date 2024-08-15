using UnityEngine;

public class CubePuzzleHandle : Interactable
{
    [SerializeField] private CubePuzzle _cubePuzzle;

    public void ClickHandle()
    {
        _cubePuzzle.UseHandle();
    }
}
