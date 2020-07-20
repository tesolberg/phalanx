using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityAtoms.BaseAtoms;
using UnityEngine.Tilemaps;
using System;

public class Phalanx
{

    /////////////////////
    /// PUBLIC FIELDS ///
    /////////////////////
    public List<Entity> activeEntities = new List<Entity>();


    //////////////////////
    /// PRIVATE FIELDS ///
    //////////////////////
    private readonly FormationSettings settings;
    private int terrainMask;
    private float linkDistance;

    Direction direction;
    Vector3 positionalAnchor;
    private List<List<PhalanxLink>> columns = new List<List<PhalanxLink>>();
    private Vector3 linkVectorForward;
    private Vector3 linkVectorRight;
    private Vector3 linkVectorBackward;


    ////////////////////
    /// CONSTRUCTORS ///
    ////////////////////

    public Phalanx(FormationSettings settings)
    {
        this.settings = settings;
        linkDistance = settings.standardPhalanxLinkDistance;

        terrainMask = LayerMask.GetMask("Terrain");
    }

    /////////////////////////////////////
    /// PUBLIC PROPERTIES ///////////////
    /////////////////////////////////////
    public Direction Direction { get => direction; }


    /////////////////////////////////////
    /// PUBLIC METHODS //////////////////
    /////////////////////////////////////

    public void AddEntity(Entity entity)
    {
        if (!activeEntities.Contains(entity))
        {
            activeEntities.Add(entity);
            entity.ActivePhalanx = this;
        }
    }

    public void RemoveEntity(Entity entity)
    {
        if (activeEntities.Contains(entity))
        {
            activeEntities.Remove(entity);
            entity.ActivePhalanx = null;
            entity.SelectEntityAsPhalanxMember(false);
            RemoveEntityFromColumnsList(entity);
        }
    }


    public void Disband()
    {
        foreach (Entity entity in activeEntities)
        {
            RemoveEntity(entity);
        }
    }

    public void SetSelectedStatus(bool selected)
    {
        foreach (Entity entity in activeEntities)
        {
            entity.SelectEntityAsPhalanxMember(selected);
        }
    }

    public void EstablishFormationAt(Vector3 position, Direction direction)
    {
        // Updates phalanx direction and anchor
        this.direction = direction;
        positionalAnchor = position;

        // Updates vectors
        linkVectorForward = DirectionExtensions.DirectionToVector3(direction) * linkDistance;
        linkVectorRight = Quaternion.Euler(0, 0, -90) * linkVectorForward;
        linkVectorBackward = Quaternion.Euler(0, 0, -180) * linkVectorForward;

        int nextEntityIndex = 0;

        // Generate first rank phalanx links
        List<Vector3> firstRankPositions = GetPhalanxFrontline(position, direction);
        columns.Clear();
        for (int i = 0; i < firstRankPositions.Count; i++)
        {
            columns.Add(new List<PhalanxLink>());
            columns[i].Add(new PhalanxLink(firstRankPositions[i], activeEntities[nextEntityIndex++]));
        }

        // Guard against infinite loop in case it is impossible to generate enough positions
        int tries = 0;

        // Adds remaining needed positions
        while (true)
        {
            foreach (var column in columns)
            {
                // Checks if number of needed positions is reached + guard
                if (nextEntityIndex >= activeEntities.Count || tries >= activeEntities.Count * 2) break;

                tries++;

                // Try to add new link to end of column
                PhalanxLink newLink = GetNewRearLink(column);

                // If the new link is valid, add entity and add link to list for this column
                if (ValidPosition(newLink.position))
                {
                    newLink.entity = activeEntities[nextEntityIndex++];
                    column.Add(newLink);
                }
            }

            // Checks if number of needed positions is reached + guard
            if (nextEntityIndex == activeEntities.Count || tries >= activeEntities.Count * 2) break;
        }

        // Give move commands
        foreach (var column in columns)
        {
            foreach (var link in column)
            {
                link.entity.MoveTo(link.position);
            }
        }
    }

    public List<Vector3> GetPhalanxFrontline(Vector3 origin, Direction direction)
    {
        List<Vector3> positions = new List<Vector3>();

        // Gets number of units in phalanx
        int positionCount = activeEntities.Count;

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

    public void RotateAllColumns(){
        for (int i = 0; i < columns.Count; i++)
        {
            RotateColumn(i);
        }
    }

    // Moves entity at first rank to rear and updates positions
    public void RotateColumn(int index){
        List<Entity> entitiesInColumn = new List<Entity>();

        foreach (var link in columns[index])
        {
            entitiesInColumn.Add(link.entity);
        }

        Entity firstRank = entitiesInColumn[0];

        entitiesInColumn.Remove(firstRank);
        entitiesInColumn.Add(firstRank);

        for (int i = 0; i < entitiesInColumn.Count; i++)
        {
            columns[index][i].entity = entitiesInColumn[i];
        }

        RefreshPositionsInColumn(index);
    }

    public void RotateColumnContainingEntity(Entity entity){
        RotateColumn(GetEntityColumnIndex(entity));
    }

    //////////////////////////////////////
    /// PRIVATE METHODS AND PROPERTIES ///
    //////////////////////////////////////

    // Removes entity from columns list and refreshers relevant column positions.
    private void RemoveEntityFromColumnsList(Entity entity)
    {
        int columnIndex = GetEntityColumnIndex(entity);
        List<PhalanxLink> column = columns[columnIndex];

        List<Entity> behindEntity = new List<Entity>();

        for (int i = 0; i < column.Count; i++)
        {
            // Finds link containing entity
            if(column[i].entity == entity){
                // Copies entities behind to list
                for (int j = 0; i < column.Count - i - 1; i++)
                {
                    column[i + j].entity = column[i + j + 1].entity;
                }

                column.RemoveAt(column.Count - 1);
                break;
            }
        }

        RefreshPositionsInColumn(columnIndex);
    }

    // Gets index for column containing given entity. Returns -1 if entity is not found.
    private int GetEntityColumnIndex(Entity entity){
        int index = -1;

        for (int i = 0; i < columns.Count; i++)
        {
            foreach (var link in columns[i])
            {
                if(link.entity == entity) index = i;
            }
        }

        return index; 
    }

    void RefreshPositionsInColumn(int index){
        foreach (var link in columns[index])
        {
            link.entity.MoveTo(link.position);
        }
    }

    private PhalanxLink GetNewRearLink(List<PhalanxLink> column)
    {
        Vector3 newPosition = column[0].position + (linkVectorBackward * column.Count);
        return new PhalanxLink(newPosition);
    }
    bool ValidPosition(Vector2 position)
    {
        RaycastHit2D hit = Physics2D.CircleCast(position, settings.phalanxLinkRadius, Vector2.zero, 1f, terrainMask);
        return !hit;
    }

    bool LinksToWall(Vector2 position)
    {
        RaycastHit2D hit = Physics2D.CircleCast(position, settings.minPhalanxLinkDistance, Vector2.zero, 1f, terrainMask);
        return hit;
    }

}