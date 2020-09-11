using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinionCard : MonoBehaviour, ICard {
    public Player owner;
    public CardStats stats;
    public GameObject minionPrefab;

    public void Activate() {
        GameObject go = Instantiate(minionPrefab, transform.parent);
        go.transform.SetSiblingIndex(transform.GetSiblingIndex());

        Minion minion = go.GetComponent<Minion>();
        minion.stats = stats;
        minion.owner = owner;

        owner.manaPool.unspentMana -= stats.cost;

        Destroy(gameObject);
    }

    public void SetCardStats(CardStats cardStats) {
        stats = cardStats;
    }
    
    public CardStats GetCardStats() => stats;

    public void SetOwner(Player owner) {
        this.owner = owner;
    }

    public Player GetOwner() {
        return this.owner;
    }
}
