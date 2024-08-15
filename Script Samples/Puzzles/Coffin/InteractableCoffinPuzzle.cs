using UnityEngine;

public class InteractableCoffinPuzzle : Interactable
{
    public bool IsCorrect;
    public bool IsOccupied;

    [SerializeField] private EventCoffinPuzzle _puzzle;
    [SerializeField] private Type _type;
    [SerializeField] private BoxCollider _boxCollider;
    [SerializeField] private string _correctItemNameDescription;
    [SerializeField] private CoffinPuzzleSocket _socket;
    public CoffinPuzzleSocket Socket => _socket;

    private const string ITEM_TABLET = "ITEM_STONETABLET";
    private const string ITEM_CROSS = "ITEM_CROSS";

    private enum Type { Cross, Tablet };


    public override bool UseItem(ItemData itemData)
    {
        if (_type == Type.Tablet)
        {
            if (itemData.Name == ITEM_TABLET)
            {
                if (!IsOccupied)
                {
                    IsCorrect = (itemData.Description == _correctItemNameDescription);
                    IsOccupied = true;
                    _boxCollider.enabled = false;
                    _puzzle.CheckCombination();
                    Instantiate(itemData.ItemPrefab, _socket.transform);
                    GameInstance.Sound.PlaySFX(itemData.UseClip);
                    return true;
                }
                else
                {
                    GameInstance.UI.PlayDialogue("Player_CoffinPuzzle_Tablet_Socket_Occupied");
                }
            }    
        }
        else if (_type == Type.Cross)
        {
            if (itemData.Name.Contains(ITEM_CROSS))
            {
                if (!IsOccupied)
                {
                    IsCorrect = (itemData.Description == _correctItemNameDescription);
                    IsOccupied = true;
                    _boxCollider.enabled = false;
                    _puzzle.CheckCombination();
                    Instantiate(itemData.ItemPrefab, _socket.transform);
                    GameInstance.Sound.PlaySFX(itemData.UseClip);
                    return true;
                }
                else
                {
                    GameInstance.UI.PlayDialogue("Player_CoffinPuzzle_Cross_In_Socket");
                }
            }
        }

        return base.UseItem(itemData);
    }

    public void ClearSocket()
    {
        IsOccupied = false;
        IsCorrect = false;
        _boxCollider.enabled = true;
    }

}
