using System;
using System.Collections.Generic;
using UnityEngine;


public class InventoryManager : MonoBehaviour
{
    [SerializeField] private LoadoutData _loadouts;
    public event Action<bool> ItemPickedUp;

    private List<ItemStack> _inventoryContents = new();
    public List<ItemStack> Inventory => _inventoryContents;

    private const int INVENTORY_SIZE = 15;

    private void Start()
    {
        for (int i = 0; i < INVENTORY_SIZE; i++)
        {
            _inventoryContents.Add(new ItemStack(i));
        }

        if (GameInstance.INSTANCE.Type == GameInstance.GameType.TestDefault)
        {
            foreach (var item in _loadouts.Items)
            {
                AddItem(new ItemStack(item, 1));
            }
        }
    }

    public bool AddItem(ItemStack itemStack)
    {
        for (int i = 0; i < _inventoryContents.Count; i++)
        {
            if (_inventoryContents[i].IsEmpty())
            {
                _inventoryContents[i].SetStack(itemStack);
                Debug.Log(itemStack.GetItemData().Name + " was added");

                ItemPickedUp?.Invoke(true);

                return true;
            }
        }

        GameInstance.UI.PlayDialogue("Player_Inventory_Full");
        return false;
    }

    public ItemStack GetStackInSlot(int index)
    {
        return _inventoryContents[index];
    }

    public bool IsInventoryFull()
    {
        foreach (ItemStack stack in _inventoryContents)
        {
            if (stack.IsEmpty())//Itemstack Has no item
            {
                return false;
            }
        }

        return true;
    }


    public void FindAndRemoveItemByNameInInventory(string itemName)
    {
        
    }

    public bool FindItemByNameInInventory(string itemName)
    {
        for (int i = 0; i < _inventoryContents.Count; i++)
        {
            var itemData = _inventoryContents[i].GetItemData();

            if (itemData != null)
            {
                if (itemData.Name == itemName)
                {
                    return true;
                }
            }
        }

        return false;
    }

    public bool HaveItem(string itemName)
    {
        for (int i = 0; i < _inventoryContents.Count; i++)
        {
            var itemData = _inventoryContents[i].GetItemData();

            if (itemData != null)
            {
                if (itemData.Name == itemName)
                {
                    return true;
                }
            }
        }

        return false;
    }

    public int GetItemAmountByName(string itemName)
    {
        int amount = 0;

        for (int i = 0; i < _inventoryContents.Count; i++)
        {
            var itemData = _inventoryContents[i].GetItemData();

            if (itemData != null)
            {
                if (itemData.Name == itemName)
                {
                    amount++;
                }
            }
        }

        return amount;
    }
}
