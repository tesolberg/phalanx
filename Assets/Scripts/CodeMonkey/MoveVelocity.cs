using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class MoveVelocity : MonoBehaviour, IMoveVelocity
{
    [SerializeField] float moveSpeed;
Vector3 velocityVector;
Rigidbody2D rigidbody2D;

private void Awake() {
    rigidbody2D = GetComponent<Rigidbody2D>();
}

    public void SetVelocity(Vector3 velocityVector){
        this.velocityVector = velocityVector;
    }

    private void FixedUpdate() {
        rigidbody2D.velocity = velocityVector;

        // Move animation
    }
}
