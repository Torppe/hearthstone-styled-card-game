using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinionCard : MonoBehaviour, ICard {
    public Player owner;
    public CardStats stats;
    public GameObject minionPrefab;
    public GameObject minionSlot;

    public void Activate(GameObject placeholder) {
        GameObject go = Instantiate(minionPrefab, placeholder.transform.position, Quaternion.identity);

        Minion minion = go.GetComponent<Minion>();
        minion.stats = stats;
        minion.owner = owner;

        MinionPosition position = go.GetComponent<MinionPosition>();
        position.minionSlot = placeholder.transform;

        owner.manaPool.UseMana(stats.cost);

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
