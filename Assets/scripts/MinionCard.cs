using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinionCard : MonoBehaviour, ICard {
    public CardStats stats;
    public GameObject minionPrefab;

    public void Activate() {
        GameObject minion = Instantiate(minionPrefab, transform.parent);
        minion.transform.SetSiblingIndex(transform.GetSiblingIndex());
        minion.GetComponent<Minion>().stats = stats;

        Destroy(gameObject);
    }

    public void SetCardStats(CardStats cardStats) {
        stats = cardStats;
    }
    
    public CardStats GetCardStats() => stats;
}
