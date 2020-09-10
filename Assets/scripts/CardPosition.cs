﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardPosition : MonoBehaviour, ISelectable {
    public GameObject placeholderPrefab;
    public LayerMask dropZoneLayer;

    private bool selectable = true;
    private GameObject placeholder;
    private ICard card;

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
        return selectable;
    }

    public void OnSelect() {
        placeholder = Instantiate(placeholderPrefab, transform.parent);
        placeholder.transform.SetSiblingIndex(transform.GetSiblingIndex());
        transform.SetParent(null);
    }

    public void OnDrag(Vector3 position, Ray ray) {
        transform.position = position;
        PositionPlaceholder(ray);
    }

    public void OnDeselect() {
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
        transform.SetParent(placeholder.transform.parent);
        transform.SetSiblingIndex(placeholder.transform.GetSiblingIndex());

        Destroy(placeholder);

        if (placeholder.transform.parent.GetComponent<DropZone>().isTable)
            card.Activate();
    }

    void Disable() {
        selectable = false;
    }

    void Enable() {
        selectable = true;
    }
}
