using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAIBase : MonoBehaviour
{
    /////////////////////
    /// PUBLIC FIELDS ///
    /////////////////////


    //////////////////////
    /// PRIVATE FIELDS ///
    //////////////////////

    Entity self;
    IMovePosition movePosition;
    Attack attack;

    /////////////////////////////////////
    /// PUBLIC PROPERTIES ///////////////
    /////////////////////////////////////


    /////////////////////////////////////
    /// PUBLIC METHODS //////////////////
    /////////////////////////////////////


    //////////////////////////////////////
    /// PRIVATE METHODS AND PROPERTIES ///
    //////////////////////////////////////

    private void Start()
    {
        self = GetComponent<Entity>();
        attack = GetComponent<Attack>();
        movePosition = GetComponent<IMovePosition>();
    }

    private void Update()
    {
        if (self.Alive)
        {
            Entity target = attack.LookForTarget(100f);
            if (target) movePosition.SetMovePosition(target.transform.position);
        }
    }

}
