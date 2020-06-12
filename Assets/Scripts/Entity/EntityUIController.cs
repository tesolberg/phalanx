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
    [SerializeField] Faction playerFaction;
    [SerializeField] Transform selectionArea;
    [SerializeField] FormationGenerator fGen;
    Vector3 mouseStartPosition;
    List<Entity> selectedEntities;
    List<Phalanx> phalanxes;
    Phalanx selectedPhalanx;
    int lastSelectedPhalanxIndex = 0;
    public Direction formationDirection = Direction.N;

    // Formation ui
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
        phalanxes = new List<Phalanx>();
        selectedPhalanx = null;
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

            // Clears selected phalanx
            if (selectedPhalanx != null)
            {
                selectedPhalanx.SetSelected(false);
            }
            selectedPhalanx = null;
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
                if (e && e.faction == playerFaction && e.ActivePhalanx == null)
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

            // If phalanx is selected
            if (selectedPhalanx != null)
            {
                formation = fGen.GetPhalanxFormation(targetPosition, formationDirection, selectedPhalanx.entities.Count);
                for (int i = 0; i < selectedPhalanx.entities.Count; i++)
                {
                    selectedPhalanx.entities[i].MoveTo(formation[i]);
                }
            }

            // No phalanx selected
            else
            {
                formation = fGen.GetCircleFormation(targetPosition);
                int targetPositionsIndex = 0;

                foreach (Entity entity in selectedEntities)
                {
                    entity.MoveTo(formation[targetPositionsIndex]);
                    targetPositionsIndex = (targetPositionsIndex + 1) % formation.Count;
                }
            }
        }

        // Clears old formation indication
        HideFormationIndication();

        // Draws new formation indication if there is entities selected
        if (selectedEntities.Count > 0 || selectedPhalanx != null)
        {
            DrawFormationIndication();
        }

        // Rotating formation
        if (Input.GetKeyDown(KeyCode.R))
        {
            int nextVal = (int)formationDirection + 1;
            formationDirection = (Direction)(nextVal % Enum.GetNames(typeof(Direction)).Length);
        }

        // Grouping entities
        if (Input.GetKeyDown(KeyCode.G))
        {
            if (selectedPhalanx != null)
            {
                selectedPhalanx.Disband();
                phalanxes.Remove(selectedPhalanx);
                selectedPhalanx = null;
            }
            else if (selectedEntities.Count > 0)
            {
                // Create new phalanx
                // Add selected entities as phalanx links
                // 

                Phalanx newPhalanx = new Phalanx();
                foreach (Entity entity in selectedEntities)
                {
                    entity.SelectEntity(false);
                    newPhalanx.AddEntity(entity);
                    entity.ActivePhalanx = newPhalanx;
                }

                selectedEntities.Clear();

                selectedPhalanx = newPhalanx;
                selectedPhalanx.SetSelected(true);
                phalanxes.Add(newPhalanx);
            }
        }

        // Cycles selected phalanx
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            if (selectedPhalanx == null)
            {
                if (phalanxes.Count > 0)
                {
                    selectedPhalanx = phalanxes[lastSelectedPhalanxIndex % phalanxes.Count];
                    selectedPhalanx.SetSelected(true);
                }
            }
            else
            {
                selectedPhalanx.SetSelected(false);
                lastSelectedPhalanxIndex = (phalanxes.FindIndex(item => item == selectedPhalanx) + 1) % phalanxes.Count;
                selectedPhalanx = phalanxes[lastSelectedPhalanxIndex];
                selectedPhalanx.SetSelected(true);
            }
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
        int positionsToDraw;

        // Grabs formation positions
        if (selectedPhalanx != null)
        {
            formationPositions = fGen.GetPhalanxFormation(formationOrigin, formationDirection, selectedPhalanx.entities.Count);
            positionsToDraw = formationPositions.Count;
        }
        else
        {
            formationPositions = fGen.GetCircleFormation(formationOrigin);
            positionsToDraw = selectedEntities.Count;
        }

        // Draws formation
        for (int i = 0; i < positionsToDraw; i++)
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

