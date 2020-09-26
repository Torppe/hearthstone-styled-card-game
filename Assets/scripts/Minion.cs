using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Minion : MonoBehaviour, ISelectable, IDamageable {
    public Player owner;
    public CardStats stats;
    public LayerMask enemyLayer;

    public int damage;
    public int health;
    
    private bool disabled = false;

    private AnimationController animationController;
    private GameObject target = null;
    private GameObject line;
    private LineRenderer lr;

    public void Start() {
        damage = stats.damage;
        health = stats.health;
        animationController = GetComponent<AnimationController>();

        owner.OnStartTurnTable += Refresh;
        owner.OnEndTurn += OnEndTurn;
    }

    private void OnDisable() {
        owner.OnStartTurnTable -= Refresh;
        owner.OnEndTurn -= OnEndTurn;
    }

    public void Attack() {
        if (target == null)
            return;

        IDamageable damageable = target.GetComponent<IDamageable>();
        int incomingDamage = damageable.TakeDamage(damage);

        TakeDamage(incomingDamage);

        animationController.StartAttack(target);

        disabled = true;
    }

    public bool IsSelectable() {
        return !disabled && owner.isPlayer;
    }

    public void OnSelect() {
        CreateLine();
    }

    public void OnDrag(Vector3 position, Ray ray) {
        if (disabled)
            return;

        if(Physics.Raycast(ray, out var hitInfo, Mathf.Infinity, enemyLayer)) {
            if(hitInfo.transform.gameObject != target)
                target = hitInfo.transform.gameObject;
        } else {
            target = null;
        }

        //draw line
        lr.SetPosition(1, position);
    }

    public void OnDeselect() {
        if (disabled)
            return;

        Attack();
        Destroy(line);
    }

    public int TakeDamage(int incomingDamage) {
        health -= incomingDamage;
        animationController.animationQueue.Enqueue(animationController.PlayDamageTaken(incomingDamage));

        if (health <= 0) {
            animationController.animationQueue.Enqueue(animationController.PlayDeathAnimation());
        }

        return damage;
    }
    private void CreateLine() {
        line = new GameObject();
        line.transform.position = transform.position;
        line.AddComponent<LineRenderer>();
        lr = line.GetComponent<LineRenderer>();
        lr.material = new Material(Shader.Find("Sprites/Default"));
        lr.startColor = Color.red;
        lr.endColor = Color.red;
        lr.SetPosition(0, transform.position);
    }
    private void Refresh() {
        disabled = false;
    }

    private void OnEndTurn() {
        if(line != null)
            Destroy(line);

        target = null;
        disabled = true;
    }
}
