using UnityEngine;

public class BookshelfPuzzleSocketTrigger : Interactable
{
    public bool IsOccupied;
    public bool IsCorrectPlacement;

    [SerializeField] private BoxCollider _boxCollider;
    [SerializeField] private string _correctBookKey;

    private const string GREEN_BOOK = "ITEM_BOOKGREEN";
    private const string BROWN_BOOK = "ITEM_BOOKBROWN";

    public override void Examine()
    {
        base.Examine();
        GameInstance.UI.PlayDialogue("Player_BookPuzzle_Book_Fits");
    }

    public override bool UseItem(ItemData itemData)
    {
        if (itemData.Name == GREEN_BOOK || itemData.Name == BROWN_BOOK)
        {
            if (!IsOccupied)
            {
                if (_correctBookKey == itemData.Name)
                {
                    IsCorrectPlacement = true;
                }

                int randomQuote = Random.Range(0, 6);

                if (randomQuote == 0)
                    GameInstance.UI.PlayDialogue("Player_BookPuzzle_Book_Seems_To_Fit");

                GameInstance.Sound.PlaySFX(itemData.PickupClip);

                _boxCollider.enabled = false;
                IsOccupied = true;

                Instantiate(itemData.ItemPrefab, transform);

                return true;
            }
            else
            {
                GameInstance.UI.PlayDialogue("Player_BookPuzzle_Already_Book");
            }
        }
        else
        {
            GameInstance.UI.PlayDialogue("Player_BookPuzzle_Not_A_Book");
        }

        return false;
    }

    public override void OnLeftClick()
    {
        base.OnLeftClick();
    }

    private void OnTransformChildrenChanged()
    {
        if (transform.childCount == 0 && IsOccupied)
        {
            IsOccupied = false;
            IsCorrectPlacement = false;
            _boxCollider.enabled = true;
        }
    }
}
