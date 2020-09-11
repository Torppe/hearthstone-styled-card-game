using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour {
    public Player winner;
    public List<Player> players;
    public GameObject winningScreen;

    private void Start() {
        Player.OnAnyPlayerDeath += EndGame;
    }

    public void EndGame(Player losingPlayer) {
        players.Remove(losingPlayer);
        winner = players[0];
        var go = Instantiate(winningScreen);
        go.GetComponentInChildren<TextMeshProUGUI>().text = "WINNER: " + winner.name;
    }
}
