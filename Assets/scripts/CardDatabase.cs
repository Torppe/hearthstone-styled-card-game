using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardDatabase : MonoBehaviour {
    public List<CardStats> cards;
    public static List<CardStats> cardDatabase = new List<CardStats>();

    void Awake() {
        cardDatabase = new List<CardStats>(cards);
    }
}
