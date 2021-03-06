﻿using System.Collections;
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
    FormationSettings settings;
    private int terrainMask;
    private float linkDistance;

    Direction direction;
    List<List<PhalanxLink>> columns = new List<List<PhalanxLink>>();
    Vector3 linkVectorForward;
    Vector3 linkVectorRight;
    Vector3 linkVectorBackward;

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

    // TODO: Error: enum changed while iterating.
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
        // Updates phalanx direction
        this.direction = direction;

        // Updates vectors
        linkVectorForward = DirectionExtensions.DirectionToVector3(direction) * linkDistance;
        linkVectorRight = Quaternion.Euler(0, 0, -90) * linkVectorForward;
        linkVectorBackward = Quaternion.Euler(0, 0, -180) * linkVectorForward;

        int nextEntityIndex = 0;

        // Generate first rank phalanx links
        List<Vector3> firstRankPositions = GetPhalanxFrontline(position, direction);
        if (firstRankPositions.Count == 0) return;

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
            for (int i = 0; i < columns.Count; i++)
            {
                // Checks if number of needed positions is reached + guard
                if (nextEntityIndex >= activeEntities.Count || tries >= activeEntities.Count * 2) break;

                tries++;

                // Try to add new link to end of column
                PhalanxLink newLink = GetNewRearLink(i);

                // If the new link is valid, add entity and add link to list for this column
                if (ValidPosition(newLink.position))
                {
                    newLink.entity = activeEntities[nextEntityIndex++];
                    columns[i].Add(newLink);
                }
            }

            // Checks if number of needed positions is reached + guard
            if (nextEntityIndex == activeEntities.Count || tries >= activeEntities.Count * 2) break;
        }


        // Give move commands
        for (int i = 0; i < columns.Count; i++)
        {
            RefreshPositionsInColumn(i);
        }
    }

    // Gets a list of front rank positions from an origin position and direction
    public List<Vector3> GetPhalanxFrontline(Vector3 origin, Direction direction)
    {
        List<Vector3> positions = new List<Vector3>();

        // Gets number of units in phalanx
        int positionCount = activeEntities.Count;

        // Sets righthand offset vector between links based on direction of frontline
        float linkDistance = settings.standardPhalanxLinkDistance;
        Vector3 linkVectorForward = DirectionExtensions.DirectionToVector3(direction) * linkDistance;
        Vector3 linkVectorRight = Quaternion.Euler(0, 0, 90) * linkVectorForward;

        if (!ValidPosition(origin)) return positions;

        // Finds right flank
        int rightFlankOffset = 0;

        for (int i = 1; i < positionCount / 2; i++)
        {
            Vector3 pos = origin + (linkVectorRight * i);

            if (!ValidPosition(pos)) break;

            rightFlankOffset = i;

            if (LinksToWall(pos)) break;
        }

        // Generates front rank positions
        for (int i = rightFlankOffset; i > -positionCount; i--)
        {
            Vector3 pos = origin + (linkVectorRight * i);

            if (!ValidPosition(pos)) break;

            positions.Add(pos);

            if (i < rightFlankOffset && LinksToWall(pos)) break;
        }

        return positions;
    }

    public void RotateAllColumns()
    {
        for (int i = 0; i < columns.Count; i++)
        {
            RotateColumn(i);
        }
    }

    public void RotateColumn(Entity entity)
    {
        RotateColumn(GetColumnIndexFromEntity(entity));
    }

    // Moves entity at first rank to rear and updates positions
    public void RotateColumn(int index)
    {
        if (columns[index].Count > 0)
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
        }

        MinimizeColumnSizeVariation();

        RefreshPositionsInColumn(index);
    }

    public void AdvanceColumnContainingEntity(Entity entity)
    {
        int column = GetColumnIndexFromEntity(entity);

        MoveColumn(column, 1);
        FollowNeighborColumn(column - 1, column, 1);
        FollowNeighborColumn(column + 1, column, 1);

        CheckFlanksForLinkToWall();
    }

    public void RetreatColumnContainingEntity(Entity entity)
    {
        int column = GetColumnIndexFromEntity(entity);

        MoveColumn(column, -1);
        FollowNeighborColumn(column - 1, column, -1);
        FollowNeighborColumn(column + 1, column, -1);

        CheckFlanksForLinkToWall();
    }

    public int GetColumnDepth(Entity entity){
        int index = GetColumnIndexFromEntity(entity);
        return columns[index].Count;
    }


    //////////////////////////////////////
    /// PRIVATE METHODS AND PROPERTIES ///
    //////////////////////////////////////
    private void CheckFlanksForLinkToWall()
    {
        bool linksToWallLeft = false;
        bool linksToWallRight = false;

        // Check if left flank is linked to wall. If not, add column, redistribute units and check again.
        while (!linksToWallLeft && columns.Count < activeEntities.Count)
        {
            if (LinksToWall(columns[0][0].position))
            {
                linksToWallLeft = true;
            }
            else
            {
                columns.Insert(0, new List<PhalanxLink>());
                MinimizeColumnSizeVariation();
            }
        }

        // Same for right flank
        while (!linksToWallRight && columns.Count < activeEntities.Count)
        {
            if (LinksToWall(columns[columns.Count - 1][0].position))
            {
                linksToWallRight = true;
            }
            else
            {
                columns.Add(new List<PhalanxLink>());
                MinimizeColumnSizeVariation();
            }
        }


        // Shaves off unneccesary columns at flanks
        while (ColumnUnneccesary(0)) EliminateColumn(0);
        while (ColumnUnneccesary(columns.Count - 1)) EliminateColumn(columns.Count - 1);
    }

    bool ColumnUnneccesary(int columnIndex)
    {
        // Guard against reducing phalanx below minimum size of 2
        if (columns.Count < 3) return false;

        // Left flank
        if (columnIndex == 0) return LinksToWall(columns[1][0].position);

        else
        {
            if (columns[columnIndex - 1].Count > 0) return LinksToWall(columns[columnIndex - 1][0].position);
            else return true;
        }
    }

    void EliminateColumn(int index)
    {

        // Move all units over to column[1]
        foreach (var link in columns[index])
        {
            PhalanxLink newLink = GetNewRearLink(1);
            newLink.entity = link.entity;
            columns[1].Add(newLink);
            newLink.entity.MoveTo(newLink.position);
        }

        columns.RemoveAt(index);

        // Redistribute units
        MinimizeColumnSizeVariation();
    }

    void FollowNeighborColumn(int columnCalled, int columnCalling, int steps)
    {
        // Move if exceeding max link distance.
        // If moved, call follow on the neighbor column that was not the caller

        // Guard against index out of bounds
        if (columnCalled >= 0 && columnCalled < columns.Count)
        {
            // Check distance to caller
            float distToCaller = (columns[columnCalled][0].position - columns[columnCalling][0].position).magnitude;

            // If larger than max distance
            if (distToCaller > settings.maxPhalanxLinkDistance)
            {
                // Move to within legal distance
                MoveColumn(columnCalled, steps);

                // Call follow on neighbor that is not the caller
                if (columnCalled - 1 != columnCalling) FollowNeighborColumn(columnCalled - 1, columnCalled, steps);
                else FollowNeighborColumn(columnCalled + 1, columnCalled, steps);
            }
        }
    }


    // Advances or retreats column a given steps. 1 step = 1/4 unit.
    void MoveColumn(int columnIndex, int steps)
    {

        Vector3 stepForward = linkVectorForward * settings.stepDistance;

        foreach (var link in columns[columnIndex])
        {
            link.position += stepForward * steps;
            link.entity.MoveTo(link.position);
        }
    }

    // TODO: Løse for at siste rad fylles på fra midterste kolonne.
    void MinimizeColumnSizeVariation()
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
                MoveEntityToColumn(entity, smallesColumnIndex);
            }
            else reduced = true;
        }
    }

    void MoveEntityToColumn(Entity entity, int targetColumnIndex)
    {
        // Removes link at origin
        List<PhalanxLink> originColumn = columns[GetColumnIndexFromEntity(entity)];
        originColumn.RemoveAt(originColumn.Count - 1);

        // Creates new link at destination and adds entity
        PhalanxLink newLink = GetNewRearLink(targetColumnIndex);
        newLink.entity = entity;
        columns[targetColumnIndex].Add(newLink);

        // Gives entity move command to new position
        entity.MoveTo(newLink.position);
    }

    // Removes entity from columns list and refreshers entity's column's positions.
    // TODO: Bug: column list must be removed if last link is removed.
    private void RemoveEntityFromColumnsList(Entity entity)
    {
        int columnIndex = GetColumnIndexFromEntity(entity);
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
    public int GetColumnIndexFromEntity(Entity entity)
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

    // Gives all entities in column move command to respective positions
    void RefreshPositionsInColumn(int index)
    {
        foreach (var link in columns[index])
        {
            link.entity.MoveTo(link.position);
        }
    }

    // Adds new PhalanxLink to end of column with correct position field
    private PhalanxLink GetNewRearLink(int columnIndex)
    {
        List<PhalanxLink> column = columns[columnIndex];

        Vector3 newPosition;

        // If column is not empty
        if (column.Count > 0) newPosition = column[0].position + (linkVectorBackward * column.Count);

        // If column is empty and not leftmost flank
        else if (columnIndex > 0)
        {
            newPosition = columns[columnIndex - 1][0].position;
            newPosition += linkVectorRight;
        }

        // If column is empty and leftmost flank
        else
        {
            newPosition = columns[columnIndex + 1][0].position;
            newPosition -= linkVectorRight;
        }

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