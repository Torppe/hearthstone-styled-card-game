using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour, IDamageable {
    public bool isPlayer = true;

    public int health = 30;
    public int damage = 0;
    public int armor = 0;

    public ManaPool manaPool;

    public event Action OnStartTurnTable;
    public event Action OnStartTurn;
    public event Action OnEndTurn;
    public event Action OnDamaged;

    public static event Action<Player> OnAnyPlayerDeath;

    public void StartTurn() {
        OnStartTurnTable?.Invoke();
        OnStartTurn?.Invoke();
        manaPool.IncreaseMana();
    }

    public void EndTurn() {
        OnEndTurn?.Invoke();
    }

    public int TakeDamage(int incomingDamage) {
        health -= incomingDamage;
        OnDamaged?.Invoke();

        if (health <= 0)
            OnAnyPlayerDeath(this);

        return damage;
    }
}
