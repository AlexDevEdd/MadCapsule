using Cysharp.Threading.Tasks;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private int _health = 5;
    [SerializeField] private int _maxHealth = 8;
    [SerializeField] private AudioSource _takeDamegeSound;
    [SerializeField] private AudioSource _addHealthSound;

    private bool _invulnerable = false;
    public async void TakeDamage(int damageValue)
    {
        if (!_invulnerable)
        {
            _health -= damageValue;
            if (_health <= 0)
            {
                _health = 0;
                Die();
            }

            StopInvulnerable(true);
            _takeDamegeSound.Play();
            await UniTask.Delay(TimeSpan.FromSeconds(1.5f));
            StopInvulnerable(false);
        }

    }
    private void StopInvulnerable(bool isInvulnerable)
    {
        _invulnerable = isInvulnerable;
    }

    public void AddHealth(int healthValue)
    {
        _health += healthValue;

        if (_health > _maxHealth)
            _health = _maxHealth;

        _addHealthSound.Play();
    }
    private void Die()
    {
        Debug.Log("LOOOOOOSSSSEEEEEEE");
    }
}
