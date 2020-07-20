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
    LayerMask enemyLayer;
    Entity self;

    private void Start()
    {
        collider = GetComponent<Collider2D>();
        self = GetComponent<Entity>();
        enemyLayer = LayerMask.GetMask(self.faction.enemies[0].name);
    }

    private void Update()
    {
        counter += Time.deltaTime;

        if (self.Alive && counter >= timeBetweenAttacks)
        {
            Entity target = LookForTarget(range);

            if (target)
            {
                TryAttack(target);
                counter = 0f + UnityEngine.Random.Range(-variabilityBetweenAttacks, variabilityBetweenAttacks);
            }
        }
    }

    public Entity LookForTarget(float range)
    {
        // Grabs colliders on entity layer within attack range
        collider.enabled = false;
        RaycastHit2D[] hits = Physics2D.CircleCastAll(transform.position, range, Vector2.zero, 0f, enemyLayer);
        collider.enabled = true;

        Entity result = null;
        float closestDistance = range * 2f;

        foreach (var hit in hits)
        {
            Entity entity = hit.transform.GetComponent<Entity>();

            if (entity && entity.Alive && entity.faction != self.faction)
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

    // TODO: Decouple entity, attack and health
    private void TryAttack(Entity entity)
    {
        entity.GetComponent<Health>().IncomingAttack(self);
    }
}