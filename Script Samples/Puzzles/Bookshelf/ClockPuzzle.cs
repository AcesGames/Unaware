using System.Collections;
using UnityEngine;


public class ClockPuzzle : InteractablePuzzle
{
    [SerializeField] private GameObject _arrowHour;
    [SerializeField] private GameObject _arrowMinute;

    [SerializeField] private BookshelfPuzzle _bookShelf;

    [SerializeField] private AudioSource _clockTick;
    [SerializeField] private AudioSource _clockWind;

    private bool _startClock;
    private bool _isSolved;

    private const float Y_VALUE = -30;


    public override void Examine()
    {
        if (!_isSolved)
        {
            GameInstance.Data.Quest.AcceptQuest(13);
            base.Examine();
        }
        else
        {
            GameInstance.UI.PlayDialogue("Player_Already_Solved");
        }
    }

    public override void OnCancel()
    {
        base.OnCancel();
    }


    public void HourButton()
    {
        if (!_isSolved)
        {
            GameInstance.Sound.PlaySFX(SFXType.MenuButton);
            _arrowHour.transform.Rotate(0, Y_VALUE, 0);
            CheckCorrectTime();
        }
    }

    public void MinuteButton()
    {
        if (!_isSolved)
        {
            GameInstance.Sound.PlaySFX(SFXType.MenuButton);
            _arrowMinute.transform.Rotate(0, Y_VALUE, 0);
            CheckCorrectTime();
        }
    }

    private void CheckCorrectTime()
    {
        if (_arrowMinute.transform.forward.y < -0.5f && _arrowMinute.transform.forward.y > -0.7f && _arrowMinute.transform.right.y > -0.7f && _arrowHour.transform.forward.y > 0.9f && _arrowHour.transform.forward.y < 1.2f)
        {
            if (_bookShelf.CheckCorrectBooks())
            {
                GameInstance.UI.PlayDialogue("Player_ClockPuzzle_Clicks");
                _isSolved = true;
                _startClock = true;
                ClockSound();
                _clockWind.Play();
                Invoke("BookShelfOpen", 3);
            }
        }
    }

    private void BookShelfOpen()
    {
        _bookShelf.OpenBookshelf();
    }

    private void ClockSound()
    {
        StartCoroutine(ClockSoundDelay());
    }

    IEnumerator ClockSoundDelay()
    {
        while (_startClock)
        {
            yield return new WaitForSeconds(1);
            _clockTick.Play();
        }
    }
}




