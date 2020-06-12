using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#pragma warning disable 0649 // variable declared but never assigned

public class FormationGenerator : MonoBehaviour
{
    /////////////////////
    /// PUBLIC FIELDS ///
    /////////////////////


    //////////////////////
    /// PRIVATE FIELDS ///
    //////////////////////

    [SerializeField] FormationSettings settings;
    LayerMask terrainMask;


    /////////////////////////////////////
    /// PUBLIC PROPERTIES ///////////////
    /////////////////////////////////////


    /////////////////////////////////////
    /// PUBLIC METHODS //////////////////
    /////////////////////////////////////

    // For grouped units
    public List<Vector3> GetPhalanxFormation(Vector3 origin, Direction direction, int positionCount)
    {
        List<Vector3> positions = new List<Vector3>();

        // Sets righthand offset vector between links based on direction of frontline
        float linkDistance = settings.standardPhalanxLinkDistance;
        Vector3 linkVectorForward = DirectionExtensions.DirectionToVector3(direction) * linkDistance;
        Vector3 linkVectorRight = Quaternion.Euler(0, 0, -90) * linkVectorForward;

        if (!ValidPosition(origin)) return positions;

        // Adds position of cursor
        positions.Add(origin);

        bool rightFlankReached = false;
        bool leftFlankReached = false;

        // Generate frontline untill reaching wall or max width
        for (int i = 1; i <= settings.frontlineMaxWidth / 2; i++)
        {
            // Breaks when needed positions is generated
            if (positions.Count >= positionCount) return positions;

            // Tentative right hand position
            Vector3 position = (linkVectorRight * i) + origin;
            // Check for wall at tentative right hand position.
            if (!ValidPosition(position)) rightFlankReached = true;
            // If no wall, add to positions.
            if (!rightFlankReached) positions.Add(position);
            // Check for wall within link max distance and flag if found
            if (LinksToWall(position)) rightFlankReached = true; ;

            // Repeats for left hand side
            position = (linkVectorRight * -i) + origin;
            if (!ValidPosition(position)) leftFlankReached = true;
            if (!leftFlankReached) positions.Add(position);
            if (LinksToWall(position)) leftFlankReached = true; ;
        }

        // Creates copy of all frontline positions (in reversed order)
        List<Vector3> frontlinePositions = new List<Vector3>();
        foreach (Vector3 pos in positions)
        {
            frontlinePositions.Add(pos);
        }
        frontlinePositions.Reverse();

        // Gets backwards vector
        int val = (int)direction + 4;
        val = val % (Enum.GetNames(typeof(Direction)).Length);
        Direction dir = (Direction)val;
        Vector3 backwards = DirectionExtensions.DirectionToVector3(dir) * settings.standardPhalanxLinkDistance;

        // Generate positions backwards from frontline up to 100 positions deep
        for (int i = 1; i <= 100; i++)
        {
            for (int j = frontlinePositions.Count - 1; j >= 0; j--)
            {
                // Breaks when needed positions is generated
                if (positions.Count >= positionCount) return positions;

                Vector3 tentativePosition = frontlinePositions[j] + (backwards * i);

                if(ValidPosition(tentativePosition)) positions.Add(tentativePosition);
                else frontlinePositions.RemoveAt(j);
            }
        }

        return positions;
    }

    // For ungrouped units
    public List<Vector3> GetCircleFormation(Vector3 origin)
    {

        List<Vector3> positions = new List<Vector3>();

        if (ValidPosition(origin)) positions.Add(origin);

        for (int i = 0; i < settings.circleFormationDistances.Length; i++)
        {
            positions.AddRange(GetPositionListAround(origin, settings.circleFormationDistances[i], settings.circleFormationRingCounts[i]));
        }

        return positions;
    }

    //////////////////////////////////////
    /// PRIVATE METHODS AND PROPERTIES ///
    //////////////////////////////////////

    private void Awake()
    {
        terrainMask = LayerMask.GetMask("Terrain");
    }

    private Vector3 ApplyRotationVector(Vector3 vector3, float angle)
    {
        return Quaternion.Euler(0f, 0f, angle) * vector3;
    }

    bool ValidPosition(Vector2 position)
    {
        RaycastHit2D hit = Physics2D.CircleCast(position, PhalanxLink.linkRadius, Vector2.zero, 1f, terrainMask);
        return !hit;
    }

    bool LinksToWall(Vector2 position)
    {
        RaycastHit2D hit = Physics2D.CircleCast(position, PhalanxLink.minLinkDist, Vector2.zero, 1f, terrainMask);
        return hit;
    }

    List<Vector3> GetPositionListAround(Vector3 origin, float distance, int positionCount)
    {
        List<Vector3> positions = new List<Vector3>();
        for (int i = 0; i < positionCount; i++)
        {
            float angle = i * (360f / positionCount);
            Vector3 dir = ApplyRotationVector(new Vector3(1, 0), angle);
            Vector3 position = origin + dir * distance;
            if (ValidPosition(position)) positions.Add(position);
        }
        return positions;
    }


}

public enum Direction { N, NE, E, SE, S, SW, W, NW }

public static class DirectionExtensions {
    public static Vector3 DirectionToVector3(Direction direction){
        switch (direction){
            case Direction.N :
            return new Vector3(0f, 1f, 0f);
            case Direction.NE :
            return new Vector3(.7f, .7f, 0f);
            case Direction.E :
            return new Vector3(1f, 0f, 0f);
            case Direction.SE :
            return new Vector3(.7f, -.7f, 0f);
            case Direction.S :
            return new Vector3(0f, -1f, 0f);
            case Direction.SW :
            return new Vector3(-.7f, -.7f, 0f);
            case Direction.W :
            return new Vector3(-1f, 0f, 0f);
            default :
            return new Vector3(-.7f, .7f, 0f);
       }
    }
}
