using System;
using System.Collections.Generic;
using UnityEngine;

public class GridPlacementManager : MonoBehaviour
{
    public static GridPlacementManager Instance { get; private set; }

    [SerializeField] private Grid grid;
    [SerializeField] public ObjectDatabaseSO databaseSO;

    private GridData floorData, furnitureData;
    private List<GameObject> placedGameObjects = new();

    private void Awake()
    {
        // Singleton pattern
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    private void Start()
    {
        floorData = new();
        furnitureData = new();
    }

    /// <summary>
    /// Checks if an object can be placed at the given grid position
    /// </summary>
    public bool CanPlaceObjectAt(Vector3Int gridPosition, int objectID)
    {
        int objectIndex = databaseSO.objectData.FindIndex(data => data.ID == objectID);
        if (objectIndex < 0)
        {
            Debug.LogError($"No object found with ID {objectID}");
            return false;
        }

        GridData selectedData = objectID == 0 ? 
            floorData : 
            furnitureData;
        return selectedData.CanPlaceObjectAt(gridPosition, databaseSO.objectData[objectIndex].Size);
    }

    /// <summary>
    /// Places an object on the grid and returns the instantiated GameObject
    /// </summary>
    public GameObject PlaceObjectAt(Vector3Int gridPosition, int objectID)
    {
        int objectIndex = databaseSO.objectData.FindIndex(data => data.ID == objectID);
        if (objectIndex < 0)
        {
            Debug.LogError($"No object found with ID {objectID}");
            return null;
        }

        if (!CanPlaceObjectAt(gridPosition, objectID))
        {
            return null;
        }

        GameObject newObject = Instantiate(databaseSO.objectData[objectIndex].Prefab);
        newObject.transform.position = grid.CellToWorld(gridPosition);
        placedGameObjects.Add(newObject);

        GridData selectedData = objectID == 0 ? 
            floorData : 
            furnitureData;
        selectedData.AddObjectAt(gridPosition,
            databaseSO.objectData[objectIndex].Size,
            objectID,
            placedGameObjects.Count - 1);

        return newObject;
    }

    /// <summary>
    /// Gets the grid reference
    /// </summary>
    public Grid GetGrid()
    {
        return grid;
    }

    /// <summary>
    /// Gets the object data for a given ID
    /// </summary>
    public ObjectData GetObjectData(int objectID)
    {
        int objectIndex = databaseSO.objectData.FindIndex(data => data.ID == objectID);
        if (objectIndex < 0)
            return null;
        return databaseSO.objectData[objectIndex];
    }
}
