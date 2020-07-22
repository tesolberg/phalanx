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
    public FormationSettings settings;
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

    public void RotateAllColumns()
    {
        ReduceColumnSizeVariation();

        for (int i = 0; i < columns.Count; i++)
        {
            RotateColumn(i);
        }
    }

    public void RotateColumn(Entity entity){
        RotateColumn(GetEntityColumnIndex(entity));
    }

    // Moves entity at first rank to rear and updates positions
    public void RotateColumn(int index)
    {
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

    public void AdvanceColumnOfEntity(Entity entity){
        MoveColumn(GetEntityColumnIndex(entity), 1);
    }

    public void RetreatColumnOfEntity(Entity entity){
        MoveColumn(GetEntityColumnIndex(entity), -1);
    }

    //////////////////////////////////////
    /// PRIVATE METHODS AND PROPERTIES ///
    //////////////////////////////////////

    // Når en column avanserer, så kaller den follow(self, int som signaliserer retning) 
    // på naboene sine. Naboene justerer hvis de er utenfor maxDistance og kaller follow på
    // sin nabo som ikke gav det originale callet.
    
    
    void Follow(int columnToFollow, int step){
        
    }

    

    // Advances or retreats column a given steps. 1 step = 1/4 unit.
    void MoveColumn(int columnIndex, int steps){
        
        Vector3 stepForward = linkVectorForward * .25f;

        foreach (var link in columns[columnIndex])
        {
            link.position += stepForward * steps;
            link.entity.MoveTo(link.position);            
        }
    }

    void ReduceColumnSizeVariation()
    {
        bool reduced = false;

        while (!reduced)
        {
            int maxColumnSize = 0;
            int largestColumnIndex = -1;
            int minColumnSize = 100;
            int smallesColumnIndex = -1;

            // Finds largest and smalles column
            for (int i = 0; i < columns.Count; i++)
            {
                int size = columns[i].Count;
                if (size > maxColumnSize)
                {
                    maxColumnSize = size;
                    largestColumnIndex = i;
                }

                if (size < minColumnSize)
                {
                    minColumnSize = size;
                    smallesColumnIndex = i;
                }
            }

            // If difference is larger than one, move one unit from largest to smallest
            if (maxColumnSize - 1 > minColumnSize)
            {
                List<PhalanxLink> largestColumn = columns[largestColumnIndex];
                Entity entity = largestColumn[largestColumn.Count - 1].entity;
                MoveEntityTo(entity, smallesColumnIndex);
            }
            else reduced = true;
        }
    }

    void MoveEntityTo(Entity entity, int targetColumnIndex)
    {
        // Removes link at origin
        List<PhalanxLink> originColumn = columns[GetEntityColumnIndex(entity)];
        originColumn.RemoveAt(originColumn.Count - 1);

        // Creates new link at destination and adds entity
        List<PhalanxLink> targetColumn = columns[targetColumnIndex];
        PhalanxLink newLink = GetNewRearLink(targetColumn);
        newLink.entity = entity;
        targetColumn.Add(newLink);

        // Gives entity move command to new position
        entity.MoveTo(newLink.position);
    }

    // Removes entity from columns list and refreshers entity's column's positions.
    // TODO: Bug: column list must be removed if last link is removed.
    private void RemoveEntityFromColumnsList(Entity entity)
    {
        int columnIndex = GetEntityColumnIndex(entity);
        List<PhalanxLink> column = columns[columnIndex];

        List<Entity> behindEntity = new List<Entity>();

        for (int i = 0; i < column.Count; i++)
        {
            // Finds link containing entity
            if (column[i].entity == entity)
            {
                // Moves entities one step forward in column
                for (int j = i; j < column.Count - 1; j++)
                {
                    column[j].entity = column[j + 1].entity;
                }

                column.RemoveAt(column.Count - 1);
                break;
            }
        }

        RefreshPositionsInColumn(columnIndex);
    }

    // Gets index for column containing entity. Returns -1 if entity is not found.
    private int GetEntityColumnIndex(Entity entity)
    {
        int index = -1;

        for (int i = 0; i < columns.Count; i++)
        {
            foreach (var link in columns[i])
            {
                if (link.entity == entity) index = i;
            }
        }

        return index;
    }

    void RefreshPositionsInColumn(int index)
    {
        foreach (var link in columns[index])
        {
            link.entity.MoveTo(link.position);
        }
    }

    // Adds new PhalanxLink to end of column with correct position field
    private PhalanxLink GetNewRearLink(List<PhalanxLink> column)
    {
        Vector3 newPosition = column[0].position + (linkVectorBackward * column.Count);
        return new PhalanxLink(newPosition);
    }

    // Returns true if position is not blocked by terrain
    bool ValidPosition(Vector2 position)
    {
        RaycastHit2D hit = Physics2D.CircleCast(position, settings.phalanxLinkRadius, Vector2.zero, 1f, terrainMask);
        return !hit;
    }

    // Returns true if position is within minimum phalanx link distance of wall
    bool LinksToWall(Vector2 position)
    {
        RaycastHit2D hit = Physics2D.CircleCast(position, settings.minPhalanxLinkDistance, Vector2.zero, 1f, terrainMask);
        return hit;
    }

}