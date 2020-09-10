using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationController : MonoBehaviour
{
    public float animationSpeed = 1;
    public AnimationCurve animationCurve;

    private float timer = 0;
    private bool attacking = false;

    private Vector3 startPosition;
    private Vector3 targetPosition;

    void Start()
    {
        
    }

    void Update()
    {
        Attack();
    }

    public void StartAttack(Vector3 target) {
        if (attacking)
            return;

        attacking = true;
        startPosition = gameObject.transform.position;
        targetPosition = target;
    }

    void Attack() {
        if (!attacking)
            return;

        transform.position = Vector3.Lerp(startPosition, targetPosition, animationCurve.Evaluate(timer));

        if (timer >= 1) {
            timer = 0;
            attacking = false;
        } else {
            timer += Time.deltaTime * animationSpeed;
        }
    }
}
