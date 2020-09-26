using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseController : MonoBehaviour {
    private Camera mainCamera;
    private Plane plane;
    private Ray ray;
    private ISelectable selectedObject;

    void Start() {
        mainCamera = Camera.main;
        plane = new Plane(Vector3.back, -5);
    }

    void Update() {
        if(Input.GetMouseButtonDown(0)) {
            ray = mainCamera.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out var hitInfo, Mathf.Infinity) 
                && hitInfo.transform.TryGetComponent<ISelectable>(out var selectable)
                && selectable.IsSelectable()) {
                selectedObject = selectable;
                selectedObject.OnSelect();
            }
        }

        if (Input.GetMouseButton(0) && selectedObject != null) {
            ray = mainCamera.ScreenPointToRay(Input.mousePosition);
            if (!plane.Raycast(ray, out var distance))
                return;

            selectedObject.OnDrag(ray.GetPoint(distance), ray);
        }

        if (Input.GetMouseButtonUp(0) && selectedObject != null) {
            selectedObject.OnDeselect();
            selectedObject = null;
        }
    }
}
