using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class CoreFunctions : MonoBehaviour
{
    [SerializeField] private Cheats _cheats;

    [SerializeField] private InteractableObject _currentInteractableObject;
    [SerializeField] private InteractableNPC _currentInteractableNPC;

    private Interactable _currentInteractable;
    private ItemStack _currentItemStack;


    public void ToggleCheats(bool toggle)
    {
        _cheats.enabled = toggle;
    }

    public void SetCurrentItemStack(ItemStack itemStack)
    {
        _currentItemStack = itemStack;
    }

    public void SetNPC(InteractableNPC interactable)
    {
        _currentInteractableNPC = interactable;
    }

    public void OnInteract()
    {
        if (_currentInteractable == null) return;
        if (GameInstance.State.HasFlag(GameStateFlag.UsingSecondaryCam) || GameInstance.State.HasFlag(GameStateFlag.DocumentOpen)) return;

        Examine(_currentInteractable);
    }

    public void Examine(Interactable interactable)
    {
        //var questStarter = obj.GetComponent<InteractableQuestStarter>();

        //if (questStarter != null) { questStarter.StartQuest(); }

        interactable.Examine();

        if (interactable != null && interactable is InteractableObject)
            _currentInteractableObject = interactable as InteractableObject;
    }

    public void EscapeInteractable()
    {
        if (_currentInteractableObject != null)
        {
            _currentInteractableObject.OnCancel();
            _currentInteractableObject = null;
        }
        if (_currentInteractableNPC != null)
        {
            _currentInteractableNPC.OnCancel();
            _currentInteractableNPC = null;
        }
    }

    public bool UseItem(Interactable interactable)
    {
        if (interactable != null && interactable is InteractableObject)
            _currentInteractableObject = interactable as InteractableObject;

        if (_currentItemStack == null) return false;

        ItemData itemData = _currentItemStack.GetItemData();
        bool canBeUsed = interactable.UseItem(itemData);

        var inventory = GameInstance.UI.Inventory;

        if (!canBeUsed)
        {
            GameInstance.Inventory.AddItem(_currentItemStack);
        }

        inventory.SetDraggedItemStack(ItemStack.Empty);

        _currentItemStack = null;
        return canBeUsed;
    }

    public void MouseClick(Interactable interactable)
    {
        interactable.OnLeftClick();
    }

    public InteractableObject GetCurrentInteractableObject()
    {
        return _currentInteractableObject;
    }
}

