using UnityEngine;

public class CubePuzzleClearSocket : MonoBehaviour
{
    [SerializeField] private CubePuzzleSocketTrigger _socketTrigger;

    private void OnTransformChildrenChanged()
    {
        if (transform.childCount == 0 && _socketTrigger.IsOccupied)
       _socketTrigger.ClearSocket();
    }
}
