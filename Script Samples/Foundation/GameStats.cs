using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStats : MonoBehaviour
{
    private int _deathsTotal;
    public int Deaths => _deathsTotal;

    private int _playerHealth = 100;
    private float _lastRegenTime;

    public void ModifyHealth(int value)
    {
        _playerHealth += value;

        if (_playerHealth < 1)
        {
            GameInstance.UI.DeathDisplay.Toggle(true);
            GameInstance.Player.PlayerDeath();
            _playerHealth = 0;
        }
        else if (value < 0)
        {
            GameInstance.UI.DamageIndicator.DisplayDamageIndicator();
        }           
    }
}
