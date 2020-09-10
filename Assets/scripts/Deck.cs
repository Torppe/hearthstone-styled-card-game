using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Deck : MonoBehaviour {
    public List<CardStats> cards = new List<CardStats>();
    public GameObject cardPrefab;
    public GameObject deckGraphics;
    public DropZone hand;

    private float cardThickness;

    private void Start() {
        for(int i = 0; i < 30; i++) {
            cards.Add(CardDatabase.cardDatabase[Random.Range(0, CardDatabase.cardDatabase.Count)]);
        }
        Shuffle();

        cardThickness = deckGraphics.transform.localScale.x / cards.Count;
    }

    private void Update() {
        if(Input.GetKeyDown(KeyCode.D)) {
            Draw();
        }
    }

    public void Shuffle() {
        var shuffled = new List<CardStats>();

        while(cards.Count != 0) {
            var i = Random.Range(0, cards.Count);
            shuffled.Add(cards[i]);
            cards.RemoveAt(i);
        }

        cards = shuffled;
    }

    public void Draw() {
        if (cards.Count == 0)
            return;

        CardStats drawnCard = cards[cards.Count - 1];

        if(hand.transform.childCount < hand.limit) {
            GameObject card = Instantiate(cardPrefab, hand.transform);
            card.GetComponent<ICard>().SetCardStats(drawnCard);
        }

        cards.RemoveAt(cards.Count - 1);

        UpdateSize();
    }

    public void UpdateSize() {
        deckGraphics.transform.localScale -= Vector3.right * cardThickness;
        if (deckGraphics.transform.localScale.x <= cardThickness)
            deckGraphics.transform.localScale = Vector3.zero;
    }

}
