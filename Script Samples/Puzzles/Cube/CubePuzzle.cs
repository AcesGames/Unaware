using UnityEngine;

public class CubePuzzle : InteractablePuzzle
{
    [SerializeField] private InteractableDoor[] _doors;
    [SerializeField] private BoxCollider _boxCollider;

    [SerializeField] private CubePuzzleSocketTrigger[] _socketTriggers;

    [SerializeField] private AudioClip _turnCube;
    [SerializeField] private AudioClip _unlock;
    [SerializeField] private AudioClip _locked;

    private Animator _anim;
    private AudioSource _audioSource;

    private bool _isSolved;
    private bool _insidePuzzle;

    private const string ITEM_CUBE = "ITEM_CUBE";
    private const string HANDLE = "handle";

    private void Start()
    {
        _anim = GetComponent<Animator>();
        _audioSource = GetComponent<AudioSource>();
    }

    public override void Examine()
    {
        base.Examine();

        if (!_isSolved)
        {
            if (!_insidePuzzle)
            {
                _boxCollider.enabled = false;

                _insidePuzzle = true;
                GameInstance.Data.Quest.AcceptQuest(2);
            }
        }
        else
        {
            GameInstance.UI.PlayDialogue("Player_Already_Solved");
        }
    }

    public override bool UseItem(ItemData itemData)
    {
        if (!_isSolved)
        {
            if (itemData.Name == ITEM_CUBE)
            {
                GameInstance.UI.PlayDialogue("Player_CubePuzzle_Need_Cube");
            }
            else
            {
                GameInstance.UI.PlayDialogue("Player_Generic_Error");
            }
        }

        return false;
    }

    public override void OnCancel()
    {
        base.OnCancel();

        _insidePuzzle = false;

        if (!_isSolved)
            _boxCollider.enabled = true;
    }

    public override void OnLeftClick()
    {
        var interactable = GameInstance.UI.CurrentInteractable;
        if (interactable == null) return;

        base.OnLeftClick();

        if (_isSolved) return;

        CubePuzzleButton button = interactable.GetComponent<CubePuzzleButton>();

        if (button != null)
            button.ClickButton();

        CubePuzzleHandle handle = interactable.GetComponent<CubePuzzleHandle>();

        if (handle != null)
            handle.ClickHandle();
    }

    private void Unlock()
    {
        _audioSource.clip = _unlock;
        _audioSource.Play();

        GameInstance.UI.PlayDialogue("Player_CubePuzzle_Solved");
    }

    public void UseHandle()
    {
        if (!_isSolved)
        {
            Debug.Log("Color " + CheckColors() + "Combi " + CheckCombination());

            if (CheckColors() && CheckCombination())
            {
                GameInstance.Data.Quest.CompleteQuest(2);

                _anim.SetBool(HANDLE, true);
                _isSolved = true;
                _boxCollider.enabled = false;

                foreach (var door in _doors)
                {
                    door.ToggleLockState(false);
                }

                foreach (var socket in _socketTriggers)
                {
                    socket.RemoveCubeCollider();
                }
            }
            else
            {
                _audioSource.clip = _locked;
                _audioSource.Play();

                GameInstance.UI.PlayDialogue("Player_Generic_Error");
            }
        }
    }

    public void ClickOnButtons(CubePuzzleSocketTrigger socketTrigger)
    {
        if (socketTrigger.IsOccupied)
        {
            _audioSource.clip = _turnCube;
            _audioSource.Play();

            socketTrigger.RotateCube();
        }
    }

    public void PickUpCube(GameObject itemObj)
    {
        if (itemObj.GetComponentInParent<CubePuzzleSocketTrigger>() != null)
        {
            itemObj.GetComponentInParent<CubePuzzleSocketTrigger>().ClearSocket();
            itemObj.GetComponentInChildren<LockTrigger>().Correct = false;
        }
    }

    private bool CheckCombination()
    {
        for (int i = 0; i < _socketTriggers.Length; i++)
        {
            if (!_socketTriggers[i].IsCorrectCube)
            {
                return false;
            }
        }

        return true;
    }

    private bool CheckColors()
    {
        for (int i = 0; i < _socketTriggers.Length; ++i)
        {
            if (!_socketTriggers[i].IsCorrectSymbol())
            {
                Debug.Log("THIS COLOR IS FALSE");
                return false;
            }
        }

        return true;
    }
}
