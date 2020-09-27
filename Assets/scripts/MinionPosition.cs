using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinionPosition : MonoBehaviour {
    public Transform minionSlot;
    public float movementSpeed = 5f;

    void Update() {
        Vector3 direction = (minionSlot.position - transform.position);
        if(direction.magnitude > 0.1f)
            transform.position = Vector3.Lerp(transform.position, transform.position + direction, Time.deltaTime * movementSpeed);
    }
}
