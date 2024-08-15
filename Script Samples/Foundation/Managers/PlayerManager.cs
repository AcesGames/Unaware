using UnityEngine;


public class PlayerManager : MonoBehaviour
{
    [SerializeField]
    private FirstPersonController _fpsController;
    public FirstPersonController FPSController => _fpsController;

    [SerializeField] private ControllerInput _controllerInput;
    public ControllerInput ControllerInput => _controllerInput;

    [SerializeField]
    private PlayerSettings _playerSettings;
    public PlayerSettings PlayerSettings => _playerSettings;

    [SerializeField] private PlayerAnimationStates _playerAnimationStates;
    public PlayerAnimationStates PlayerAnimationStates => _playerAnimationStates;

    [SerializeField] private Camera _playerCamera;
    public Camera Camera => _playerCamera;

    [SerializeField]
    private PlayerTorch _torch;
    public PlayerTorch Torch => _torch;

    [SerializeField] private StressReceiver _stressReceiver;
    public StressReceiver StressReceiver => _stressReceiver;

    [SerializeField]
    private PlayerRaycast _playerRaycast;
    public PlayerRaycast Raycast => _playerRaycast;

    [SerializeField]
    private PlayerAudio _playerAudio;
    public PlayerAudio PlayerAudio => _playerAudio;

    [SerializeField]
    private GameObject _playerModelNoBoots;
    public GameObject PlayerModelNoBoots => _playerModelNoBoots;

    [SerializeField]
    private GameObject _playerModelBoots;
    public GameObject PlayerModelBoots => _playerModelBoots;

    [SerializeField] private Transform trasherTransform;


    private Vector3 _currentCheckpointPosition;
    private float _currentCheckpointRotation;

    private void Start()
    {
        _fpsController.OnStart();
        _controllerInput.OnStart();
    }

    private void Update()
    {
        _fpsController.OnUpdate();
    }

    private void FixedUpdate()
    {
        _fpsController.OnFixedUpdate();
    }

    private void LateUpdate()
    {
        _fpsController.OnLateUpdate();
    }

    public void SetCurrentCheckpoint(Transform checkpoint, float rotation)
    {
        _currentCheckpointPosition = checkpoint.transform.position;
        _currentCheckpointRotation = rotation;
    }

    public void PlayerRespawn()
    {
    //    GameInstance.Data.Stats.ModifyHealth(100);
     //   GameInstance.Data.Stats.ModifyDeaths(1);
        GameInstance.UI.HUD.ToggleHUD(true);
        SetPlayerPosition(_currentCheckpointPosition, _currentCheckpointRotation);
   //     _fpsController.Torch.TorchObject.SetActive(true);

    }

    public void SetPlayerPosition(Vector3 position, float rotation)
    {
        _fpsController.transform.position = position;
        _fpsController.transform.rotation = Quaternion.Euler(0, rotation, 0);
        ResetMouse();
        FreezePlayer(false);
    }

    public void FreezePlayer(bool toggle) => _fpsController.ToggleFreeze(toggle);

    private void ResetMouse()
    {

    }

    public void PlayerDeath()
    {
        GameInstance.State.SetFlag(GameStateFlag.IsDead);
    }


    public void TrashItem(string itemName)
    {
        //for (int i = 0; i < allItems.Count; i++) {
        //    if (allItems[i].prefabItemName == itemName) {

        //        GameObject itemObject = Instantiate(allItems[i].gameObject);
        //        itemObject.transform.position = trasherTransform.position;
        //        itemObject.GetComponent<Rigidbody>().isKinematic = false;

        //        if (allItems[i].isQuestItem) {
        //         //   ObjectiveManager.INSTANCE.RemoveQuestItems(allItems[i].ItemType);
        //        }

        //        break;
        //    }
        //} 
    }

}
