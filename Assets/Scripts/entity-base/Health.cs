using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#pragma warning disable 0649 // variable declared but never assigned

public class Health : MonoBehaviour
{
    /////////////////////
    /// PUBLIC FIELDS ///
    /////////////////////
    public int maxHealth = 3;
    public int currentHealth = 3;

    //////////////////////
    /// PRIVATE FIELDS ///
    //////////////////////

    [SerializeField] FormationSettings settings;
    new SpriteRenderer renderer;
    Entity entity;

    /////////////////////////////////////
    /// PUBLIC PROPERTIES ///////////////
    /////////////////////////////////////


    /////////////////////////////////////
    /// PUBLIC METHODS //////////////////
    /////////////////////////////////////

    public void IncomingAttack(Entity attacker)
    {
        CheckForPush(attacker);
        if (UnityEngine.Random.Range(0, 100) < 40) TakeDamage(1);
    }

    public void TakeDamage(int amount)
    {
        // Check for retreat
        // if (entity.ActivePhalanx != null && Random.Range(0,2) > 0)
        // {
        //     entity.ActivePhalanx.RetreatColumnContainingEntity(entity);
        //     return;
        // }

        currentHealth -= amount;

        UpdateHealthGFX();

        if (currentHealth <= 0)
        {
            entity.Die();
        }
        else if (entity.ActivePhalanx != null && settings.rotateOnDamage)
        {
            entity.ActivePhalanx.RotateColumn(entity);
        }
    }

    public void Heal(int amount)
    {
        currentHealth = Mathf.Clamp(amount + currentHealth, 0, 3);

        UpdateHealthGFX();
    }

    //////////////////////////////////////
    /// PRIVATE METHODS AND PROPERTIES ///
    //////////////////////////////////////

    private void Start()
    {
        renderer = transform.Find("GFX").GetComponent<SpriteRenderer>();
        entity = transform.GetComponent<Entity>();
        StartCoroutine("HealthRegen");
    }

    private void CheckForPush(Entity attacker)
    {
        Vector2 defenderRearVector = transform.position - attacker.transform.position;
        Vector2 attackerRearVector = defenderRearVector * -1f;

        // Get defender power
        int defensePower = entity.GetPushPower(defenderRearVector);
        int attackerPower = attacker.GetPushPower(attackerRearVector);

        Debug.Log(attacker.faction.name + " " + attacker.gameObject.name + " attacking. aPow = " + attackerPower + ". dPow = " + defensePower);
        // Random.range attacker vs defender
        // IF attacker > defender, push
        
    }

    void UpdateHealthGFX()
    {
        if (currentHealth == 3) renderer.color = Color.cyan;
        else if (currentHealth == 2) renderer.color = Color.yellow;
        else if (currentHealth == 1) renderer.color = Color.red;
        else if (currentHealth == 0) renderer.color = Color.black;
    }

    IEnumerator HealthRegen()
    {
        while (entity.Alive)
        {
            yield return new WaitForSeconds(Random.Range(10f, 20f));

            if (!entity.Alive) break;

            Heal(1);
        }
    }
}
