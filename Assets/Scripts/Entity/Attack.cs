using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    [SerializeField]
    float timeBetweenAttacks;

    float variabilityBetweenAttacks = .1f;
    float counter = 0f;

    private void Update() {
        counter += Time.deltaTime;

        if(counter >= timeBetweenAttacks){

            Transform target = LookForTarget();
            
            if(target){
                TryAttack();
                counter = 0f + UnityEngine.Random.Range(-variabilityBetweenAttacks, variabilityBetweenAttacks);
            }
        }
    }

    private Transform LookForTarget()
    {
        throw new NotImplementedException();
    }

    private void TryAttack()
    {

    }

    //    // TODO: Line of sight.
    // List<Entity> ScanForEnemies()
    // {
    //     collider.enabled = false;

    //     RaycastHit2D[] hit = Physics2D.CircleCastAll(transform.position, scanningRange, Vector2.zero, 0f, entityLayer);

    //     collider.enabled = true;

    //     List<Entity> enemies = new List<Entity>();

    //     foreach (var h in hit)
    //     {
    //         Entity hitEntity = h.transform.parent.GetComponent<Entity>();

    //         if(hitEntity == null){
    //             Debug.LogWarning("Entity " + h.transform.parent.gameObject.name + " is without Entity component");
    //         }
    //         else if(hitEntity.faction != entity.faction){
    //             enemies.Add(hitEntity);
    //         }
    //     }

    //     return enemies;
    // }
    
    // Entity ClosestEnemy(List<Entity> enemiesInSight){
    //     Entity result = null;
    //     float closestDistance = scanningRange * 2f;

    //     foreach (Entity entity in enemiesInSight)
    //     {
    //         float distanceToEnemy = (entity.transform.position - transform.position).magnitude;
    //         if(distanceToEnemy < closestDistance){
    //             result = entity;
    //             closestDistance = distanceToEnemy;
    //         }
    //     }

    //     return result;
    // }
}
