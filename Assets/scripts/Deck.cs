using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Deck : MonoBehaviour {
    public Player owner;
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

        owner.OnStartTurn += Draw;
    }

    private void OnDisable() {
        owner.OnStartTurn -= Draw;
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
            ICard card = Instantiate(cardPrefab, hand.transform).GetComponent<ICard>();
            card.SetCardStats(drawnCard);
            card.SetOwner(owner);
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
