using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardPosition : MonoBehaviour, ISelectable {
    public GameObject placeholderPrefab;
    public LayerMask dropZoneLayer;

    private bool dragging = false;
    private bool disabled = false;
    private GameObject placeholder;
    private ICard card;

    private Transform originalParent;
    private int originalSiblingIndex;

    public void Start() {
        card = GetComponent<ICard>();
        card.GetOwner().OnStartTurn += Enable;
        card.GetOwner().OnEndTurn += Disable;
    }

    private void OnDisable() {
        card.GetOwner().OnStartTurn -= Enable;
        card.GetOwner().OnEndTurn -= Disable;
    }

    public bool IsSelectable() {
        return !disabled && card.GetCardStats().cost <= card.GetOwner().manaPool.unspentMana;
    }

    public void OnSelect() {
        placeholder = Instantiate(placeholderPrefab, transform.parent);
        placeholder.transform.SetSiblingIndex(transform.GetSiblingIndex());

        originalParent = transform.parent;
        originalSiblingIndex = transform.GetSiblingIndex();
        dragging = true;

        transform.SetParent(null);
    }

    public void OnDrag(Vector3 position, Ray ray) {
        if (disabled)
            return;

        transform.position = position;
        PositionPlaceholder(ray);
    }

    public void OnDeselect() {
        if (disabled)
            return;

        dragging = false;
        PlaceCard();
    }

    void PositionPlaceholder(Ray ray) {
        if (Physics.Raycast(ray, out var hit, Mathf.Infinity, dropZoneLayer)
            && hit.transform != placeholder.transform.parent
            && hit.transform.TryGetComponent<DropZone>(out var dropZoneComponent)
            && hit.transform.childCount < dropZoneComponent.limit) {
            placeholder.transform.SetParent(hit.transform);
        }

        int newSiblingIndex = placeholder.transform.parent.childCount;

        for (int i = 0; i < placeholder.transform.parent.childCount; i++) {
            if (transform.position.x < placeholder.transform.parent.GetChild(i).position.x) {
                newSiblingIndex = i;

                if (placeholder.transform.GetSiblingIndex() < newSiblingIndex)
                    newSiblingIndex--;

                break;
            }
        }

        placeholder.transform.SetSiblingIndex(newSiblingIndex);
    }

    void PlaceCard() {
        transform.SetSiblingIndex(placeholder.transform.GetSiblingIndex());

        if (placeholder.transform.parent.GetComponent<DropZone>().isTable) {
            Disable();
            card.Activate(placeholder);
        } else {
            transform.SetParent(placeholder.transform.parent);
            Destroy(placeholder);
        }
    }

    public void CancelPlacement() {
        if (dragging) {
            dragging = false;

            placeholder.transform.SetParent(originalParent);
            placeholder.transform.SetSiblingIndex(originalSiblingIndex);

            PlaceCard();
        }
    }

    void Disable() {
        CancelPlacement();
        disabled = true;
    }

    void Enable() {
        disabled = false;
    }
}
