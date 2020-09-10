using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Card", menuName = "Card")]
public class CardStats : ScriptableObject {
    public string cardName;
    public string description;

    public Sprite artwork;

    public int health;
    public int damage;
    public int cost;

    public ICard effect;
}
