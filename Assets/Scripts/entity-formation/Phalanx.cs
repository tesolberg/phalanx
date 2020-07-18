using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityAtoms.BaseAtoms;
using UnityEngine.Tilemaps;
using System;

public class Phalanx
{

    // TODO: Phalanxen bør ta over setup av formasjonen fra formation generator. Det kan internt i phalanxen genereres en første rank som så spes på 
    // bakover. Linkene kan legges i lister for hver kolonne. Da kan det kjøres utjevningssjekker for å sikre ca like mange i hver kolonne.
    // Phalanxen kan kun presse forover og bli presset bakover i planet den er orientert. Ved angrep mot flankene er det ikke pressmekanikk men stor
    // nedside for phalanxen for å kompensere.

    /////////////////////
    /// PUBLIC FIELDS ///
    /////////////////////
    public List<PhalanxLink> links = new List<PhalanxLink>();


    //////////////////////
    /// PRIVATE FIELDS ///
    //////////////////////

    private List<List<PhalanxLink>> columns = new List<List<PhalanxLink>>();
    Direction direction;
    private readonly FormationSettings settings;
    private readonly int terrainMask;

    ////////////////////
    /// CONSTRUCTORS ///
    ////////////////////

    public Phalanx (FormationSettings settings){
        this.settings = settings;
        terrainMask = LayerMask.GetMask("Terrain");
    }

    /////////////////////////////////////
    /// PUBLIC PROPERTIES ///////////////
    /////////////////////////////////////


    /////////////////////////////////////
    /// PUBLIC METHODS //////////////////
    /////////////////////////////////////

    public void TakeUpPositionAt(Vector2 position){
        // Generate first rank positions
        List<Vector3> firstRank = GetFirstRankPositions(position, direction, links.Count);

        // Generate position columns lists, one for each position in first rank and add respective positions
        List<List<Vector3>> positionColumns = new List<List<Vector3>>();
        for (int i = 0; i < firstRank.Count; i++)
        {
            positionColumns.Add(new List<Vector3>());
            positionColumns[i].Add(firstRank[i]);
        }

        // Generate remaining positions for each column and add them to position column lists
        

        // Assign phalanx links to positions and populate phalanx link columns list.
    }

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

    public void SetSelectedStatus(bool selected)
    {
        foreach (PhalanxLink link in links)
        {
            link.GetComponent<Entity>().SelectEntityAsPhalanxMember(selected);
        }
    }

    //////////////////////////////////////
    /// PRIVATE METHODS AND PROPERTIES ///
    //////////////////////////////////////

public List<Vector3> GetFirstRankPositions(Vector3 origin, Direction direction, int phalanxSize)
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
            if (positions.Count >= phalanxSize) return positions;

            // Tentative right hand position
            Vector3 position = (linkVectorRight * i) + origin;
            // Check for wall at tentative right hand position.
            if (!ValidPosition(position)) rightFlankReached = true;
            // If no wall, add to positions.
            if (!rightFlankReached) positions.Add(position);
            // Check for wall within link max distance and flag if found
            if (LinksToWall(position)) rightFlankReached = true; ;

            // Checks again for if needed positions is reached
            if (positions.Count >= phalanxSize) return positions;

            // Repeats for left hand side
            position = (linkVectorRight * -i) + origin;
            if (!ValidPosition(position)) leftFlankReached = true;
            if (!leftFlankReached) positions.Add(position);
            if (LinksToWall(position)) leftFlankReached = true; ;

            if(rightFlankReached && leftFlankReached) break;
        }

        return positions;
    }

    // public List<Vector3> GetRemainingPositionsFromFrontRank(List<Vector3> frontRank){
        
    //     // Creates copy of all front rank positions (in reversed order)
    //     List<Vector3> frontlinePositions = new List<Vector3>();
    //     foreach (Vector3 pos in positions)
    //     {
    //         frontlinePositions.Add(pos);
    //     }
    //     frontlinePositions.Reverse();

    //     // Gets backwards vector
    //     int val = (int)direction + 4;
    //     val = val % (Enum.GetNames(typeof(Direction)).Length);
    //     Direction dir = (Direction)val;
    //     Vector3 backwards = DirectionExtensions.DirectionToVector3(dir) * settings.standardPhalanxLinkDistance;

    //     // Generate positions backwards from frontline up to 100 positions deep
    //     for (int i = 1; i <= 100; i++)
    //     {
    //         for (int j = frontlinePositions.Count - 1; j >= 0; j--)
    //         {
    //             // Breaks when needed positions is generated
    //             if (positions.Count >= positionCount) return positions;

    //             Vector3 tentativePosition = frontlinePositions[j] + (backwards * i);

    //             if(ValidPosition(tentativePosition)) positions.Add(tentativePosition);
    //             else frontlinePositions.RemoveAt(j);
    //         }
    //     }

    //     return positions;
    // }

    bool ValidPosition(Vector2 position)
    {
        RaycastHit2D hit = Physics2D.CircleCast(position, settings.minPhalanxLinkDistance, Vector2.zero, 1f, terrainMask);
        return !hit;
    }

    bool LinksToWall(Vector2 position)
    {
        RaycastHit2D hit = Physics2D.CircleCast(position, settings.minPhalanxLinkDistance, Vector2.zero, 1f, terrainMask);
        return hit;
    }

}
