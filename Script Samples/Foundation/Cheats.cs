using UnityEngine;
using UnityEngine.InputSystem;


public class Cheats : MonoBehaviour
{
    [SerializeField] private LevelCoordinates[] _levelCoordinates;

    private void Start()
    {
        GameInstance.Data.GetVariableDB().SetValue(GlobalConsts.KEY_COLLECTED_CANDLE, true);
        GameInstance.UI.HUD.PlayerBars.ModifyFuel(100);
        GameInstance.Player.Torch.Torch();
    }

    private void Update()
    {
        if (Keyboard.current.lKey.wasPressedThisFrame)
        {
            DebugDataVariables();
        }

        if (Keyboard.current.oKey.wasPressedThisFrame)
        {

        }

        if (Keyboard.current.hKey.wasPressedThisFrame)
        {
            GameInstance.Data.Quest.AddAllQuests();

        }

        Basic();
        ChangeLevel();
    }

    private void Basic()
    {
        if (Keyboard.current.lKey.wasPressedThisFrame)
        {
            GameInstance.UI.HUD.PlayerBars.ModifyFuel(100);
            //      UIManager.INSTANCE.imageFuelBar.SetActive(true)
        }
        if (Keyboard.current.hKey.wasPressedThisFrame)
        {
            GameInstance.UI.HUD.PlayerBars.ModifyHealth(-90);
            //        UIManager.INSTANCE.panelHUD.SetActive(false);
            //       UIManager.INSTANCE.crosshair.gameObject.SetActive(false);
        }
        if (Keyboard.current.oKey.wasPressedThisFrame)
        {
 
        }
        if (Keyboard.current.nKey.wasPressedThisFrame)
        {
            GameInstance.State.DebugPrintCheckFlags();
            //GameInstance.UI.HUD.DamageIndicator.DisplayDamageIndicator();
        }
        if (Keyboard.current.kKey.wasPressedThisFrame)
        {
            GameInstance.UI.CheatChangeLevelDisplay.gameObject.SetActive(true);
          //  GameInstance.Player.FPSController.SpeedCheat();
        }

        if (Keyboard.current.bKey.wasPressedThisFrame)
        {

            GameInstance.Data.Quest.AddAllQuests();
        }
        if (Keyboard.current.uKey.wasPressedThisFrame) // Check Inventory items
        {
            var inventory = GameInstance.Inventory.Inventory;

            foreach (var item in inventory)
            {
                if (item.GetItemData() != null)
                    Debug.Log(item.GetItemData().Name);
            }
        }

    }

    private void DebugInventoryStacks()
    {
        //foreach (var item in UIInventory.INSTANCE.inventory.GetInventoryStacks()) {
        //    if (item.item != null)
        //        Debug.Log(item.item.ItemName);
        //}
    }

    private void DebugDataVariables()
    {
        GameInstance.Data.DebugVariableDatabase();
    }

    private void ChangeLevel()
    {
        if (Keyboard.current.f1Key.wasPressedThisFrame)
        {
            GameInstance.Level.ChangeLevel(_levelCoordinates[0]);
        }
        if (Keyboard.current.f5Key.wasPressedThisFrame)
        {
            GameInstance.Level.ChangeLevel(_levelCoordinates[1]);
        }
        if (Keyboard.current.f6Key.wasPressedThisFrame)
        {
            GameInstance.Level.ChangeLevel(_levelCoordinates[2]);
        }
        if (Keyboard.current.f7Key.wasPressedThisFrame)
        {
            GameInstance.Level.ChangeLevel(_levelCoordinates[4]);
        }
        if (Keyboard.current.f8Key.wasPressedThisFrame)
        {
            GameInstance.Level.ChangeLevel(_levelCoordinates[5]);
        }
        if (Keyboard.current.f9Key.wasPressedThisFrame)
        {
            GameInstance.Level.ChangeLevel(_levelCoordinates[6]);
        }
        if (Keyboard.current.f10Key.wasPressedThisFrame)
        {
            GameInstance.Level.ChangeLevel(_levelCoordinates[7]);
        }
        if (Keyboard.current.f11Key.wasPressedThisFrame)
        {
            GameInstance.Level.ChangeLevel(_levelCoordinates[8]);
        }
        if (Keyboard.current.f12Key.wasPressedThisFrame)
        {
            GameInstance.Level.ChangeLevel(_levelCoordinates[9]);
        }
    }
}