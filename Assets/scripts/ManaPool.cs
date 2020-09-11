using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManaPool : MonoBehaviour {
    public int maxMana = 10;
    public int unspentMana = 1;
    private int availableMana = 1;

    public void UseMana(int amount) {
        if(amount <= unspentMana)
            unspentMana -= amount;
    }

    public void IncreaseMana() {
        if(availableMana < maxMana)
            availableMana++;

        unspentMana = availableMana;
    }
}
