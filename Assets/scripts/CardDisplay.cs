using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CardDisplay : MonoBehaviour {
    public Image artworkImage;

    public TextMeshPro nameText;
    public TextMeshPro descriptionText;
    public TextMeshPro damageText;
    public TextMeshPro healthText;
    public TextMeshPro costText;

    private CardStats card;

    void Start() {
        card = GetComponent<ICard>().GetCardStats();

        if(nameText != null)
            nameText.text = card.cardName;
        if(descriptionText != null)
            descriptionText.text = card.description;
        if(damageText != null)
            damageText.text = card.damage.ToString();
        if(healthText != null)
            healthText.text = card.health.ToString();
        if(costText != null)
            costText.text = card.cost.ToString();
        if(artworkImage != null)
            artworkImage.sprite = card.artwork;
    }
}
