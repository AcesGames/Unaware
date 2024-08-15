using System.Linq;
using UnityEngine;


public class EventCoffinPuzzle : MonoBehaviour
{
    [SerializeField] private InteractableCoffinLid[] _lids;
    [SerializeField] private InteractableCoffinPuzzle[] _interactables;
    [SerializeField] private AudioSource _audioSource;


    public void CheckCombination()
    {
        if (_interactables.All(socket => socket.IsCorrect))
        {
            UnlockCoffins();
        }
    }

    private void UnlockCoffins()
    {
        GameInstance.Data.Quest.CompleteQuest(231);

        foreach (var lid in _lids)
        {
            lid.ToggleLockState(false);
        }

        foreach (var socket in _interactables)
        {
            socket.Socket.LockChildren();
        }

        _audioSource.Play();
    }
}
