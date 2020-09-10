using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropZone : MonoBehaviour {
    public int limit = 5;
    public bool isTable = false;

    private void Update() {
        UpdateZOrder();
    }

    void UpdateZOrder() {
        int children = transform.childCount;
        for (int i = 0; i < children; i++) {
            Transform child = transform.GetChild(i);
            child.position = new Vector3(child.position.x, child.position.y, (i * -0.1f - 0.1f));
        }
    }
}
