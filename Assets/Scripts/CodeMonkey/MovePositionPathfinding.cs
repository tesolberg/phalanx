using System.Collections;
using System.Collections.Generic;
using Pathfinding;
using UnityEngine;

public class MovePositionPathfinding : MonoBehaviour, IMovePosition
{
    public void SetMovePosition(Vector3 movePosition){
        AIPath aIPath = GetComponent<AIPath>();
        aIPath.destination = movePosition;
    }
}
