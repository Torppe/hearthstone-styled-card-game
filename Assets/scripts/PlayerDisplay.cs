using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerDisplay : MonoBehaviour, IDisplay {
    //public Image artworkImage;

    public TextMeshPro damageText;
    public TextMeshPro healthText;
    public TextMeshPro armorText;

    private Player player;

    void Start() {
        player = GetComponent<Player>();

        damageText.text = player.damage == 0 ? "" : player.damage.ToString();
        armorText.text = player.armor == 0 ? "" : player.armor.ToString();
        healthText.text = player.health.ToString();
        //artworkImage.sprite = player.artwork;
    }

    public void UpdateDisplay() {
        healthText.text = player.health <= 0 ? "0" : player.health.ToString();
    }
}
