using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MinionDisplay : MonoBehaviour, IDisplay {
    public Image artworkImage;

    public TextMeshPro damageText;
    public TextMeshPro healthText;
    //public TextMeshPro damageTaken;

    public bool isDead = false;

    private Minion minion;

    void Start() {
        minion = GetComponent<Minion>();

        damageText.text = minion.stats.damage.ToString();
        healthText.text = minion.stats.health.ToString();
        artworkImage.sprite = minion.stats.artwork;
    }

    public void UpdateDisplay() {
        healthText.text = minion.health <= 0 ? "0" : minion.health.ToString();
    }
}
