using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MinionDisplay : MonoBehaviour {
    public Image artworkImage;

    public TextMeshPro damageText;
    public TextMeshPro healthText;

    private Minion minion;
    
    void Start() {
        minion = GetComponent<Minion>();

        damageText.text = minion.stats.damage.ToString();
        healthText.text = minion.stats.health.ToString();
        artworkImage.sprite = minion.stats.artwork;

        minion.OnDamaged += UpdateDisplay;
    }

    private void OnDisable() {
        minion.OnDamaged -= UpdateDisplay;
    }

    public void UpdateDisplay() {
        healthText.text = minion.health.ToString();
    }
}
