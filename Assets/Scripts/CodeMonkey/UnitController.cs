using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Elias;

public class UnitController : MonoBehaviour
{

    Vector3 startPosition;
    List<Entity> selectedEntities;

    private void Awake() {
        selectedEntities = new List<Entity>();
    }

    private void Update() {
        if(Input.GetMouseButtonDown(0)){
            // Left mouse button pressed
            startPosition = Utilities.GetMouseWorldPosition();
        }

        if(Input.GetMouseButtonUp(0)){
            // Left mouse button released

            // Clears list of selected units
            foreach(Entity entity in selectedEntities){
                entity.SelectEntity(false);
            }
            selectedEntities.Clear();

            // Gathers all colliders in selected area and adds all entities to selected entities
            Collider2D[] colliders = Physics2D.OverlapAreaAll(startPosition, Utilities.GetMouseWorldPosition());
            Debug.Log("####");
            foreach(Collider2D c in colliders){
                Entity e = c.GetComponent<Entity>();
                if(e){
                    selectedEntities.Add(e);
                    e.SelectEntity(true);
                }
            }

        }
    }
}
