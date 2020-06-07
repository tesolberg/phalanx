using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Elias;

public class EntityController : MonoBehaviour
{

    [SerializeField] Transform selectionArea;
    Vector3 startPosition;
    List<Entity> selectedEntities;

    private void Awake()
    {
        selectedEntities = new List<Entity>();
        selectionArea.gameObject.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            // Left mouse button pressed
            startPosition = Utilities.GetMouseWorldPosition();
            selectionArea.gameObject.SetActive(true);

        }

        if (Input.GetMouseButton(0))
        {
            // While left mouse button pressed

            Vector3 currentMousePosition = Utilities.GetMouseWorldPosition();
            Vector3 lowerLeft = new Vector3(
                Mathf.Min(startPosition.x, currentMousePosition.x),
                Mathf.Min(startPosition.y, currentMousePosition.y)
            );
            Vector3 upperRight = new Vector3(
                Mathf.Max(startPosition.x, currentMousePosition.x),
                Mathf.Max(startPosition.y, currentMousePosition.y)
            );

            selectionArea.position = lowerLeft;
            selectionArea.localScale = upperRight - lowerLeft;
        }

        if (Input.GetMouseButtonUp(0))
        {
            // Left mouse button released

            selectionArea.gameObject.SetActive(false);

            // Clears list of selected units
            foreach (Entity entity in selectedEntities)
            {
                entity.SelectEntity(false);
            }
            selectedEntities.Clear();

            // Gathers all colliders in selected area and adds all entities to selected entities
            Collider2D[] colliders = Physics2D.OverlapAreaAll(startPosition, Utilities.GetMouseWorldPosition());
            Debug.Log("####");
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

        if (Input.GetMouseButtonDown(1))
        {
            // Right mouse button clicked

            Vector3 targetPosition = Utilities.GetMouseWorldPosition();

            foreach (Entity entity in selectedEntities)
            {
                entity.MoveTo(targetPosition);
            }
        }
    }
}
