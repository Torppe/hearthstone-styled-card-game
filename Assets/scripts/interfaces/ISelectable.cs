using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ISelectable {
    bool IsSelectable();
    void OnSelect();
    void OnDrag(Vector3 position, Ray ray);
    void OnDeselect();
}
