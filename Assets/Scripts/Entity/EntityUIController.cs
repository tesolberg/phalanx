using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Elias;
using System;

#pragma warning disable 0649 // variable declared but never assigned

public class EntityUIController : MonoBehaviour
{

    /////////////////////
    /// PUBLIC FIELDS ///
    /////////////////////


    //////////////////////
    /// PRIVATE FIELDS ///
    //////////////////////
    [SerializeField] Transform selectionArea;
    [SerializeField] FormationGenerator fGen;
    Vector3 mouseStartPosition;
    List<Entity> selectedEntities;
    bool grouped = false;
    public Direction phalanxDirection = Direction.N;

    // Formation indication
    [SerializeField] GameObject unitPositionIndicatorPrefab;
    [SerializeField] Transform positionIndicatorParent;
    List<GameObject> positionIndicators = new List<GameObject>();


    LayerMask terrainMask;

    /////////////////////////////////////
    /// PUBLIC PROPERTIES ///////////////
    /////////////////////////////////////


    /////////////////////////////////////
    /// PUBLIC METHODS //////////////////
    /////////////////////////////////////

    //////////////////////////////////////
    /// PRIVATE METHODS AND PROPERTIES ///
    //////////////////////////////////////
    private void Awake()
    {
        terrainMask = LayerMask.GetMask("Terrain");
        selectedEntities = new List<Entity>();
        selectionArea.gameObject.SetActive(false);
    }

    private void Update()
    {
        // Left mouse button pressed
        if (Input.GetMouseButtonDown(0))
        {
            mouseStartPosition = Utilities.GetMouseWorldPosition();
            selectionArea.gameObject.SetActive(true);

            // Clears list of selected units
            foreach (Entity entity in selectedEntities)
            {
                entity.SelectEntity(false);
            }
            selectedEntities.Clear();
        }

        // While left mouse button pressed
        if (Input.GetMouseButton(0))
        {
            Vector3 currentMousePosition = Utilities.GetMouseWorldPosition();
            Vector3 lowerLeft = new Vector3(
                Mathf.Min(mouseStartPosition.x, currentMousePosition.x),
                Mathf.Min(mouseStartPosition.y, currentMousePosition.y)
            );
            Vector3 upperRight = new Vector3(
                Mathf.Max(mouseStartPosition.x, currentMousePosition.x),
                Mathf.Max(mouseStartPosition.y, currentMousePosition.y)
            );

            selectionArea.position = lowerLeft;
            selectionArea.localScale = upperRight - lowerLeft;
        }


        // Left mouse button released
        if (Input.GetMouseButtonUp(0))
        {
            // Turns off selected area indicator
            selectionArea.gameObject.SetActive(false);

            // Gathers all colliders in selected area and adds all entities to selected entities
            Collider2D[] colliders = Physics2D.OverlapAreaAll(mouseStartPosition, Utilities.GetMouseWorldPosition());
            // TODO: Implement max entity selected limit 
            foreach (Collider2D c in colliders)
            {
                Entity e = c.GetComponent<Entity>();
                if (e)
                {
                    selectedEntities.Add(e);
                    e.SelectEntity(true);
                }
            }
        }

        // Right mouse button pressed
        if (Input.GetMouseButtonDown(1))
        {
            Vector3 targetPosition = Utilities.GetMouseWorldPosition();

            List<Vector3> formation;
            if(grouped) formation = fGen.GetPhalanxFormation(targetPosition,phalanxDirection, selectedEntities.Count);
            else formation = fGen.GetCircleFormation(targetPosition);
            int targetPositionsIndex = 0;

            foreach (Entity entity in selectedEntities)
            {
                entity.MoveTo(formation[targetPositionsIndex]);
                targetPositionsIndex = (targetPositionsIndex + 1) % formation.Count;
            }
        }

        // Clears old formation indication
        HideFormationIndication();

        // Draws new formation indication if there is entities selected
        if (selectedEntities.Count > 0)
        {
            DrawFormationIndication();
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            int nextVal = (int)phalanxDirection + 1;
            phalanxDirection = (Direction)(nextVal % Enum.GetNames(typeof(Direction)).Length);
        }

        if (Input.GetKeyDown(KeyCode.G))
        {
            grouped = !grouped;
        }
    }

    void HideFormationIndication()
    {
        foreach (GameObject indicator in positionIndicators)
        {
            indicator.SetActive(false);
        }
    }

    void DrawFormationIndication()
    {
        Vector3 formationOrigin = Utilities.GetMouseWorldPosition();
        List<Vector3> formationPositions;

        // Grabs formation based on grouped status
        if (grouped) formationPositions = fGen.GetPhalanxFormation(formationOrigin, phalanxDirection, selectedEntities.Count);
        else formationPositions = fGen.GetCircleFormation(formationOrigin);

        for (int i = 0; i < selectedEntities.Count; i++)
        {

            // Generates new indicator if neccessary
            if (positionIndicators.Count <= i)
            {
                GameObject instance = Instantiate(unitPositionIndicatorPrefab, Vector2.zero, Quaternion.identity, positionIndicatorParent);
                positionIndicators.Add(instance);
            }

            // Sets indicator active and position
            if (i < formationPositions.Count)
            {
                positionIndicators[i].SetActive(true);
                positionIndicators[i].transform.position = formationPositions[i];
            }
            else positionIndicators[i].SetActive(false);
        }
    }
}

