using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityAtoms.BaseAtoms;
using UnityEngine.Tilemaps;
using System;

public class Phalanx
{

    // TODO: Phalanxen bør ta over setup av formasjonen fra formation generator. Det kan internt i phalanxen genereres en første rank som så spes på 
    // bakover. Linkene kan legges i lister for hver kolonne. Da kan det kjøres utjevningssjekker.

    /////////////////////
    /// PUBLIC FIELDS ///
    /////////////////////
    public List<PhalanxLink> links = new List<PhalanxLink>();


    //////////////////////
    /// PRIVATE FIELDS ///
    //////////////////////

    private List<List<PhalanxLink>> columns = new List<List<PhalanxLink>>();
    private readonly FormationSettings settings;
    private int terrainMask;
    Direction direction;
    private float linkDistance;
    private Vector3 linkVectorForward;
    private Vector3 linkVectorRight;
    private Vector3 linkVectorBackward;

    ////////////////////
    /// CONSTRUCTORS ///
    ////////////////////

    public Phalanx (FormationSettings settings){
        this.settings = settings;
        linkDistance = settings.standardPhalanxLinkDistance;
        
        terrainMask = LayerMask.GetMask("Terrain");
    }

    /////////////////////////////////////
    /// PUBLIC PROPERTIES ///////////////
    /////////////////////////////////////


    /////////////////////////////////////
    /// PUBLIC METHODS //////////////////
    /////////////////////////////////////

    public void AddLink(PhalanxLink link)
    {
        if (!links.Contains(link)) links.Add(link);
    }

    public void RemoveLink(PhalanxLink link)
    {
        if (links.Contains(link))
        {
            link.OnLinkRemovedFromPhalanx();

            links.Remove(link);
        }
    }

    public void Disband()
    {
        foreach (PhalanxLink link in links)
        {
            Entity entity = link.GetComponent<Entity>();
            entity.ActivePhalanx = null;
            entity.SelectEntityAsPhalanxMember(false);
        }
    }

    public void SetSelected(bool selected)
    {
        foreach (PhalanxLink link in links)
        {
            link.GetComponent<Entity>().SelectEntityAsPhalanxMember(selected);
        }
    }

    public void EstablishFormationAt(Vector3 position, Direction direction){
        // Updates phalanx direction
        this.direction = direction;

        // Updates vectors
        linkVectorForward = DirectionExtensions.DirectionToVector3(direction) * linkDistance;
        linkVectorRight = Quaternion.Euler(0, 0, -90) * linkVectorForward;
        linkVectorBackward = Quaternion.Euler(0, 0, -180) * linkVectorForward;

        // Generate frontline positions
        List<Vector3> firstRankPositions = GetPhalanxFrontline(position, direction);
        List<List<Vector3>> allPositions = new List<List<Vector3>>();

        // Generate position lists. Add first rank positions to position lists.
        columns.Clear();
        for (int i = 0; i < firstRankPositions.Count; i++)
        {
            allPositions.Add(new List<Vector3>());
            allPositions[i].Add(firstRankPositions[i]);
        }

        // Counts positions already added
        int positionCount = firstRankPositions.Count;
        
        // Guard against infinite loop in case it is impossible to generate enough positions
        int tries = 0;

        // Adds remaining needed positions
        while(true){
            foreach (var column in allPositions)
            {
                // Checks if number of needed positions is reached + guard
                if(positionCount == links.Count || tries >= links.Count * 2) break;

                tries++;

                // Try to get next position in column
                Vector3 newPosition = GetNextPositionInColumn(column);

                // If the new position is valid, add to position list for this column and increment position count
                if(ValidPosition(newPosition)){
                    column.Add(newPosition);
                    positionCount++;
                }
            }

            // Checks if number of needed positions is reached + guard
            if(positionCount == links.Count || tries >= links.Count * 2) break;
        }

        // Assign units to column lists and give them move command
        int unitIndex = 0;

        for (int i = 0; i < allPositions.Count; i++)
        {
            columns.Add(new List<PhalanxLink>());

            foreach (var pos in allPositions[i])
            {
                // Gives move command
                links[unitIndex].GetComponent<IMovePosition>().SetMovePosition(pos);
                // Adds unit to right column list
                columns[i].Add(links[unitIndex]);
                unitIndex++;
            }
        }
    }

    private Vector3 GetNextPositionInColumn(List<Vector3> column)
    {
        return column[0] + (linkVectorBackward *  column.Count);
    }

    public List<Vector3> GetPhalanxFrontline(Vector3 origin, Direction direction)
    {
        List<Vector3> positions = new List<Vector3>();
        
        // Gets number of units in phalanx
        int positionCount = links.Count;

        // Sets righthand offset vector between links based on direction of frontline
        float linkDistance = settings.standardPhalanxLinkDistance;
        Vector3 linkVectorForward = DirectionExtensions.DirectionToVector3(direction) * linkDistance;
        Vector3 linkVectorRight = Quaternion.Euler(0, 0, -90) * linkVectorForward;

        if (!ValidPosition(origin)) return positions;

        // Adds position of cursor
        positions.Add(origin);

        // Flags for when flanks are reached
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

            // Breaks when needed positions is generated
            if (positions.Count >= positionCount) return positions;

            // Repeats for left hand side
            position = (linkVectorRight * -i) + origin;
            if (!ValidPosition(position)) leftFlankReached = true;
            if (!leftFlankReached) positions.Add(position);
            if (LinksToWall(position)) leftFlankReached = true; ;
        }


        return positions;
    }



    //////////////////////////////////////
    /// PRIVATE METHODS AND PROPERTIES ///
    //////////////////////////////////////

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

}


        // // Creates copy of all front rank positions (in reversed order)
        // List<Vector3> frontlinePositions = new List<Vector3>();
        // foreach (Vector3 pos in positions)
        // {
        //     frontlinePositions.Add(pos);
        // }
        // frontlinePositions.Reverse();

        // // Gets backwards vector
        // int val = (int)direction + 4;
        // val = val % (Enum.GetNames(typeof(Direction)).Length);
        // Direction dir = (Direction)val;
        // Vector3 backwards = DirectionExtensions.DirectionToVector3(dir) * settings.standardPhalanxLinkDistance;

        // // Generate positions backwards from frontline up to 100 positions deep
        // for (int i = 1; i <= 100; i++)
        // {
        //     for (int j = frontlinePositions.Count - 1; j >= 0; j--)
        //     {
        //         // Breaks when needed positions is generated
        //         if (positions.Count >= positionCount) return positions;

        //         Vector3 tentativePosition = frontlinePositions[j] + (backwards * i);

        //         if(ValidPosition(tentativePosition)) positions.Add(tentativePosition);
        //         else frontlinePositions.RemoveAt(j);
        //     }
        // }
