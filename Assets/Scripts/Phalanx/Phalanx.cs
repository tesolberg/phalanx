using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityAtoms.BaseAtoms;
using UnityEngine.Tilemaps;

public class Phalanx
{
    /////////////////////
    /// PUBLIC FIELDS ///
    /////////////////////


    //////////////////////
    /// PRIVATE FIELDS ///
    //////////////////////

    List<Entity> entities = new List<Entity>();
    List<PhalanxLink> links = new List<PhalanxLink>();
    List<Vector3Int> desiredFrontline = new List<Vector3Int>();

    LayerMask terrainMask;
    int maxWidth = 9;

    ////////////////////
    /// CONSTRUCTORS ///
    ////////////////////

    public Phalanx(List<Entity> entities)
    {
        this.entities = entities;
        terrainMask = LayerMask.GetMask("Terrain");
    }

    /////////////////////////////////////
    /// PUBLIC PROPERTIES ///////////////
    /////////////////////////////////////


    /////////////////////////////////////
    /// PUBLIC METHODS //////////////////
    /////////////////////////////////////

    // public void AddEntity(Entity entity){
    //     if(!entities.Contains(entity)) entities.Add(entity);
    // }

    // public void RemoveEntity(Entity entity){
    //     if(entities.Contains(entity)) entities.Remove(entity);
    // }

    public List<Vector3> GetFrontlinePositions(Vector3 origin, Direction direction)
    {
        List<Vector3> positions = new List<Vector3>();

        // Sets righthand offset vector between links based on direction of frontline
        float linkDistance = (PhalanxLink.maxLinkDist + PhalanxLink.minLinkDist) / 2f;
        Vector3 linkVectorForward = DirectionExtensions.DirectionToVector3(direction) * linkDistance;
        Vector3 linkVectorRight = Quaternion.Euler(0, 0, -90) * linkVectorForward;


        // Adds position of cursor
        positions.Add(origin);

        // Adds positivly offset positions untill reaching wall
        for (int i = 1; i <= maxWidth / 2; i++)
        {
            // Tentative position of new link
            Vector3 position = (linkVectorRight * i) + origin;

            // Check for wall at tentative position.
            if (!ValidLinkPosition(position)) break;

            // If no wall, add to positions.
            positions.Add((linkVectorRight * i) + origin);

            // Check for wall within link max distance and break if found
            if (LinksToWall(position)) break;
        }

        // Adds negativly offset positions untill reaching wall
        for (int i = -1; i >= -maxWidth / 2; i--)
        {
            // Tentative position of new link
            Vector3 position = (linkVectorRight * i) + origin;

            // Check for wall at tentative position.
            if (!ValidLinkPosition(position)) break;

            // If no wall, add to positions.
            positions.Add((linkVectorRight * i) + origin);

            // Check for wall within link max distance and break if found
            if (LinksToWall(position)) break;
        }

        return positions;
    }


    public void SetFrontline(Vector3 origin, Direction direction){

    }

    public List<Vector3Int> GetFrontline(Vector3Int position, Direction direction)
    {
        List<Vector3Int> positions = new List<Vector3Int>();

        return positions;
    }

    public bool CheckFrontlineIntegrety()
    {
        return true;
    }

    public void ReestablishFrontlineIntegrity()
    {

    }

    //////////////////////////////////////
    /// PRIVATE METHODS AND PROPERTIES ///
    //////////////////////////////////////

    bool ValidLinkPosition(Vector2 position)
    {
        RaycastHit2D hit = Physics2D.CircleCast(new Vector2(position.x, position.y), PhalanxLink.linkRadius, Vector2.zero, 1f, terrainMask);
        return !hit;
    }

    bool LinksToWall(Vector2 position)
    {
        RaycastHit2D hit = Physics2D.CircleCast(position, PhalanxLink.minLinkDist, Vector2.zero, 1f, terrainMask);
        return hit;
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
