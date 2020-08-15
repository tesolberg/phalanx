using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Push : MonoBehaviour
{
    Entity self;

    private void Start() {
        self = GetComponent<Entity>();
    }

    private void CheckForPush(Entity attacker)
    {
        Vector2 defenderRearVector = transform.position - attacker.transform.position;
        Vector2 attackerRearVector = defenderRearVector * -1f;

        // Get defender power
        int defensePower = self.GetPushPower(defenderRearVector);
        int attackerPower = attacker.GetPushPower(attackerRearVector);

        //Debug.Log(attacker.faction.name + " " + attacker.gameObject.name + " attacking. aPow = " + attackerPower + ". dPow = " + defensePower);
        // Random.range attacker vs defender
        if(Random.Range(0, attackerPower + 1) > Random.Range(0, defensePower + 1)){
            if(attacker.ActivePhalanx != null) attacker.ActivePhalanx.AdvanceColumnContainingEntity(attacker);
            else if(self.ActivePhalanx != null) self.ActivePhalanx.RetreatColumnContainingEntity(self);
        }
        // IF attacker > defender, push
        
    }
}
