using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    public event Action OnStartTurnTable;
    public event Action OnStartTurn;
    public event Action OnEndTurn;

    public void StartTurn() {
        OnStartTurnTable?.Invoke();
        OnStartTurn?.Invoke();
    }

    public void EndTurn() {
        OnEndTurn?.Invoke();
    }
}
