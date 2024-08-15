using UnityEngine;


public class BookshelfPuzzle : Interactable
{
    [SerializeField] private Openable _door;
    [SerializeField] private BookshelfPuzzleSocketTrigger[] _socketTriggers;
    [SerializeField] private GameObject _triggerFallingBooks;
    [SerializeField] private BoxCollider _boxCollider;


    public override void Examine()
    {
        base.Examine();
        GameInstance.UI.PlayDialogue("Player_BookPuzzle_Strange_Bookshelf");
    }

    public override bool UseItem(ItemData itemData)
    {
        GameInstance.UI.PlayDialogue("Player_Generic_Error");
        return false;
    }

    public void OpenBookshelf()
    {
        _door.Access();

        GameInstance.Data.Quest.CompleteQuest(13);

        _triggerFallingBooks.SetActive(true);
        _boxCollider.enabled = true;
    }

    public bool CheckCorrectBooks()
    {
        for (int i = 0; i < _socketTriggers.Length; i++)
        {
            if (!_socketTriggers[i].IsCorrectPlacement)
            {
                return false;
            }
        }

        return true;
    }
}
