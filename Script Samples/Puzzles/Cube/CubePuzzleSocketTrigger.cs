using UnityEngine;

public class CubePuzzleSocketTrigger : Interactable
{
    public bool IsCorrectCube; // Make both of these private
    public bool IsOccupied;

    [SerializeField] private BoxCollider _boxCollider;
    [SerializeField] private string _correctDataDescriptionKey;
    [SerializeField] private Transform _rotatorTransform;

    private BoxCollider _cubeCollider;

    private const string CUBE = "ITEM_CUBE";

    public override void Examine()
    {
        base.Examine();
    }

    public override bool UseItem(ItemData itemData)
    {
        if (itemData.Name == CUBE)
        {
            if (!IsOccupied)
            {
                if (itemData.Description == _correctDataDescriptionKey)
                {
                    IsCorrectCube = true;
                }

                _boxCollider.enabled = false;
                IsOccupied = true;

                var cube = Instantiate(itemData.ItemPrefab, _rotatorTransform);
                _cubeCollider = cube.GetComponent<BoxCollider>();
                GameInstance.Sound.PlaySFX(itemData.UseClip);

                return true;
            }
            else
            {
                GameInstance.UI.PlayDialogue("Player_CubePuzzle_Already_Cube");
            }
        }
        else
        {
            GameInstance.UI.PlayDialogue("Player_Generic_Error");
        }

        return false;
    }

    public bool IsCorrectSymbol()
    {
        if(IsOccupied)
        {
            if(_rotatorTransform.GetChild(0) != null)
            {
                return _rotatorTransform.GetChild(0).GetComponentInChildren<LockTrigger>().Correct;
            }
        }

        return false;
    }

    public void RemoveCubeCollider()
    {
        _cubeCollider.enabled = false;
    }

    public void ClearSocket()
    {
        IsOccupied = false;
        IsCorrectCube = false;
        _boxCollider.enabled = true;
    }

    public void RotateCube()
    {
        _rotatorTransform.Rotate(Vector3.forward, 90);
    }
}
