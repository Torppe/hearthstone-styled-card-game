﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Minion : MonoBehaviour, ISelectable {
    public Player owner;
    public CardStats stats;
    public LayerMask enemyLayer;

    public int damage;
    public int health;

    private bool selectable = true;

    private GameObject target = null;
    private GameObject line;
    private LineRenderer lr;

    public event Action OnDamaged;

    public void Start() {
        damage = stats.damage;
        health = stats.health;

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

        Minion targetMinion = target.GetComponent<Minion>();

        targetMinion.Damage(damage);
        Damage(targetMinion.damage);

        selectable = false;
    }

    public void Damage(int damage) {
        health -= damage;
        OnDamaged?.Invoke();

        if (health <= 0)
            Destroy(gameObject);
    }

    public bool IsSelectable() {
        return selectable;
    }

    public void OnSelect() {
        CreateLine();
    }

    public void OnDrag(Vector3 position, Ray ray) {
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
        Attack();
        Destroy(line);
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
        selectable = true;
    }

    private void OnEndTurn() {
        selectable = false;
    }
}
