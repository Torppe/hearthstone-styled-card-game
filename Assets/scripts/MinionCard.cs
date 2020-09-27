using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinionCard : MonoBehaviour, ICard {
    public Player owner;
    public CardStats stats;
    public GameObject minionPrefab;
    public GameObject minionSlot;

    private bool activated = false;

    public void Activate(GameObject placeholder) {
        if (activated)
            return;

        StartCoroutine(ActivateAnimation(placeholder));
        //GameObject go = Instantiate(minionPrefab, placeholder.transform.position, Quaternion.identity);

        //Minion minion = go.GetComponent<Minion>();
        //minion.stats = stats;
        //minion.owner = owner;

        //MinionPosition position = go.GetComponent<MinionPosition>();
        //position.minionSlot = placeholder.transform;

        //owner.manaPool.UseMana(stats.cost);

        //Destroy(gameObject);
    }

    float time = 0;
    public IEnumerator ActivateAnimation(GameObject placeholder) {
        activated = true;

        time = 0;
        while(Vector3.Distance(transform.position, placeholder.transform.position) > 0.1f){
            MoveCard(placeholder.transform.position);

            yield return null;
        }

        Destroy(gameObject);

        GameObject go = Instantiate(minionPrefab, placeholder.transform.position, Quaternion.identity);

        Minion minion = go.GetComponent<Minion>();
        minion.stats = stats;
        minion.owner = owner;

        MinionPosition position = go.GetComponent<MinionPosition>();
        position.minionSlot = placeholder.transform;

        owner.manaPool.UseMana(stats.cost);
    }

    private void MoveCard(Vector3 targetPosition) {
        //Move
        Vector3 direction = (targetPosition - transform.position);
        transform.position = Vector3.Lerp(transform.position, transform.position + direction, Time.deltaTime * 5f);

        //Scale
        transform.localScale = Vector3.Lerp(Vector3.one, Vector3.one * 1.5f, Mathf.SmoothStep(0.0f,1f,time * 2));
        time += Time.deltaTime;
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
