using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TurnController : MonoBehaviour {
    public float turnTime = 10f;

    public Player player;
    public Player enemy;

    public Button endTurnButton;

    private float timer;
    private bool yourTurn = true;
    private void Start() {
        timer = turnTime;

        StartTurn();
    }

    void Update() {
        timer -= Time.deltaTime;
        if (timer <= 0)
            EndTurn();
    }

    public void EndTurn() {
        yourTurn = !yourTurn;
        timer = turnTime;

        StartTurn();
    }

    private void StartTurn() {
        if(yourTurn) {
            enemy.EndTurn();
            player.StartTurn();

            endTurnButton.interactable = true;
        } else {
            player.EndTurn();
            enemy.StartTurn();

            endTurnButton.interactable = false;
        }
    }
}
