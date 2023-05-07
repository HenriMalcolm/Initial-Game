using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour{
    public Transform target;
    public float speed = 3f;
    public float rotateSpeed = 0.0025f;
    private Rigidbody2D rb;

    private void Start() {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update() {
        if (!target) {
            GetTarget();
        } else {
            RotateTowardsTarget();
        }
    }

    private void FixedUpdate() {
        
    }

    private void RotateTowardsTarget() {
        Vector2 targetDirection = target.position - transform.position;
        float angle = Mathf.Atan2(targetDirection.y, targetDirection.x) * Mathf.Rad2Deg -90f;
    }

    private void GetTarget () {
        target = GameObject.FindGameObjectWithTag("Player").transform;
    }
}
