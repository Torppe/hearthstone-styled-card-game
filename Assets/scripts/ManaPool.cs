using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManaPool : MonoBehaviour {
    public int maxMana = 10;
    public int unspentMana = 0;

    public GameObject crystalPrefab;
    public Transform crystalParent;

    private int availableMana = 0;
    private List<GameObject> crystals = new List<GameObject>();

    public void UseMana(int amount) {
        if(amount <= unspentMana) {
            unspentMana -= amount;
            for(int i = crystals.Count - 1; i >= unspentMana; i--) {
                Destroy(crystals[i]);
                crystals.RemoveAt(i);
            }
        }
    }

    public void IncreaseMana() {
        if(availableMana < maxMana) {
            availableMana++;
        }

        for(int i = unspentMana; i < availableMana; i++) {
            crystals.Add(Instantiate(crystalPrefab, crystalParent));
        }

        unspentMana = availableMana;
    }
}
