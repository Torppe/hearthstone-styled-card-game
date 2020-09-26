using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationController : MonoBehaviour
{
    public float animationSpeed = 1;
    public AnimationCurve animationCurve;
    public Queue<IEnumerator> animationQueue = new Queue<IEnumerator>();

    private float timer = 0;
    private bool attacking = false;
    private bool attacked = false;
    private bool damageShown = false;
    private bool risen = false;

    private Vector3 startPosition;
    private Vector3 targetPosition;

    private MinionDisplay minionDisplay;

    void Start() {
        minionDisplay = GetComponent<MinionDisplay>();
    }

    public void StartAttack(GameObject target) {
        if (attacking)
            return;

        StartCoroutine(AttackCoroutine(target));
    }

    private IEnumerator AttackCoroutine(GameObject target) {
        AnimationController targetAnimationController = target.GetComponent<AnimationController>();

        attacked = false;
        damageShown = false;
        risen = false;
        attacking = true;

        while(!risen) {
            Rise();
            yield return null;
        }

        targetPosition = target.transform.position;
        startPosition = gameObject.transform.position;

        while(!attacked) {
            Attack(targetAnimationController);
            yield return null;
        }

        while(risen) {
            Lower();
            yield return null;
        }

        attacking = false;

        if (animationQueue.Count > 0)
            StartCoroutine(animationQueue.Dequeue());

        if (targetAnimationController.animationQueue.Count > 0)
            StartCoroutine(targetAnimationController.animationQueue.Dequeue());
    }

    public IEnumerator PlayDeathAnimation() {
        yield return new WaitForSeconds(2f);
        Destroy(gameObject);
    }

    public IEnumerator PlayDamageTaken(int damage) {
        minionDisplay.UpdateDisplay();
        minionDisplay.damageTaken.gameObject.SetActive(true);
        minionDisplay.damageTaken.text = damage.ToString();

        yield return new WaitForSeconds(2f);

        minionDisplay.damageTaken.gameObject.SetActive(false);
    }

    private void Rise() {
        transform.position -= Vector3.forward * Time.deltaTime * 12f;
        if (transform.position.z <= -2f) {
            transform.position = new Vector3(transform.position.x, transform.position.y, -2f);
            risen = true;
        }
    }

    private void Lower() {
        transform.position += Vector3.forward * Time.deltaTime * 12f;
        if(transform.position.z >= 0) {
            risen = false;
        }
    }

    private void Attack(AnimationController target) {
        transform.position = Vector3.Lerp(startPosition, targetPosition, animationCurve.Evaluate(timer));

        timer += Time.deltaTime * animationSpeed;

        if (Vector3.Distance(transform.position, targetPosition) <= 0.01f && !damageShown) {
            StartCoroutine(animationQueue.Dequeue());
            StartCoroutine(target.animationQueue.Dequeue());
            damageShown = true;
        }

        if (timer >= 1f) {
            attacked = true;
            timer = 0;
        }
    }

}
