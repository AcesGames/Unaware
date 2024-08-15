using UnityEngine;


public class CoffinPuzzleSocket : MonoBehaviour
{
    [SerializeField] private InteractableCoffinPuzzle _interactable;

    private void OnTransformChildrenChanged()
    {
        if (transform.childCount == 0 && _interactable.IsOccupied)
            _interactable.ClearSocket();
    }

    public void LockChildren()
    {
        foreach (Transform child in transform)
        {
            child.GetComponent<Collider>().enabled = false;
            child.gameObject.layer = 0;
        }
    }
}
