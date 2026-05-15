using System;
using System.Collections.Generic;
using UnityEngine;

public class GridPlacementManager : MonoBehaviour
{
    public static GridPlacementManager Instance { get; private set; }

    [SerializeField] private Grid grid;
    [SerializeField] public ObjectDatabaseSO databaseSO;
    //[SerializeField] private GameObject gridVisualization;
    [SerializeField] private ObjectPlacer objectPlacer;

    private GridData floorData, furnitureData;
    private IBuildingState buildingState;

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

    // Starts placement state for the given object ID
    public void StartPlacementState(int objectID)
    {
        StopState();
        //gridVisualization.SetActive(true);
        buildingState = new PlacementState(objectID,
                                           grid,
                                           null, // Will be set by PlayerBuildSystem's preview
                                           databaseSO,
                                           floorData,
                                           furnitureData,
                                           objectPlacer);
    }

    // Starts removing state
    public void StartRemovingState()
    {
        StopState();
        //gridVisualization.SetActive(true);
        buildingState = new RemovingState(grid, null, floorData, furnitureData, objectPlacer);
    }


    // Executes the current building state action
    public void ExecuteStateAction(Vector3Int gridPosition, int objectID)
    {
        if (buildingState == null)
            return;
        if (buildingState is PlacementState)
            buildingState = new PlacementState(objectID,
                                           grid,
                                           null, // Will be set by PlayerBuildSystem's preview
                                           databaseSO,
                                           floorData,
                                           furnitureData,
                                           objectPlacer);

        buildingState.OnAction(gridPosition);
    }

    // Updates the current building state
    public void UpdateState(Vector3Int gridPosition)
    {
        if (buildingState == null)
            return;
        buildingState.UpdateState(gridPosition);
    }

    // Stops the current building state
    public void StopState()
    {
        if (buildingState == null)
            return;
        //gridVisualization.SetActive(false);
        buildingState.EndState();
        buildingState = null;
    }
    
    // Gets the grid reference
    public Grid GetGrid()
    {
        return grid;
    }

    // Gets the object data for a given ID
    public ObjectData GetObjectData(int objectID)
    {
        int objectIndex = databaseSO.objectData.FindIndex(data => data.ID == objectID);
        if (objectIndex < 0)
            return null;
        return databaseSO.objectData[objectIndex];
    }

    // Sets the preview system for the current state
    public void SetPreviewSystem(PreviewSystem previewSystem)
    {
        if (buildingState is PlacementState placementState)
        {
            placementState.SetPreviewSystem(previewSystem);
        }
        else if (buildingState is RemovingState removingState)
        {
            removingState.SetPreviewSystem(previewSystem);
        }
    }

    // Gets the current building state
    public IBuildingState GetCurrentState()
    {
        return buildingState;
    }


    // Checks placement validity for a specific object ID at a grid position
    public bool CheckPlacementValidity(Vector3Int gridPosition, int objectID)
    {
        int objectIndex = databaseSO.objectData.FindIndex(data => data.ID == objectID);
        if (objectIndex < 0)
            return false;

        ObjectData objectData = databaseSO.objectData[objectIndex];
        GridData selectedData = objectData.IsFloor ? floorData : furnitureData;
        Vector2Int size = objectData.Size;

        bool useFurnitureBlocked = !objectData.IsFloor;

        // Check if any tile the object would occupy is blocked for the object layer
        for (int x = 0; x < size.x; x++)
        {
            for (int y = 0; y < size.y; y++)
            {
                Vector3Int checkPos = gridPosition + new Vector3Int(x, y, 0);
                if (BlockedTilesData.Instance.IsBlocked(checkPos, useFurnitureBlocked))
                    return false;
            }
        }

        return selectedData.CanPlaceObjectAt(gridPosition, size);
    }
}
