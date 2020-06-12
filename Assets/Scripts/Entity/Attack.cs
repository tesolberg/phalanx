using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#pragma warning disable 0649 // variable declared but never assigned

[RequireComponent(typeof(Entity))]
public class Attack : MonoBehaviour
{
    [SerializeField] float timeBetweenAttacks;
    [SerializeField] float range;

    float variabilityBetweenAttacks = .1f;
    float counter = 0f;

    new Collider2D collider;
    LayerMask entityLayer;
    Entity self;

    private void Start()
    {
        collider = GetComponent<Collider2D>();
        entityLayer = LayerMask.GetMask("Entity");
    }

    private void Update()
    {
        counter += Time.deltaTime;

        if (counter >= timeBetweenAttacks)
        {
            Entity target = LookForTarget();

            if (target)
            {
                TryAttack(target);
                counter = 0f + UnityEngine.Random.Range(-variabilityBetweenAttacks, variabilityBetweenAttacks);
            }
        }
    }

    private Entity LookForTarget()
    {
        // Grabs colliders on entity layer within attack range
        collider.enabled = false;
        RaycastHit2D[] hits = Physics2D.CircleCastAll(transform.position, range, Vector2.zero, 0f, entityLayer);
        collider.enabled = true;

        Entity result = null;
        float closestDistance = range * 2f;

        foreach (var hit in hits)
        {
            Entity entity = hit.transform.parent.GetComponent<Entity>();

            if (entity.Alive && entity.faction != self.faction)
            {
                float distanceToEnemy = (entity.transform.position - transform.position).magnitude;
                if (distanceToEnemy < closestDistance)
                {
                    result = entity;
                    closestDistance = distanceToEnemy;
                }
            }
        }

        return result;
    }

    private void TryAttack(Entity entity)
    {
        entity.IncomingAttack(self);
    }
}