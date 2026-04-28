using System;
using UnityEngine;

public class PlayerBuildSystem : MonoBehaviour
{
    [SerializeField] private GameObject placementObject;
    [SerializeField] private GameObject BuildingUI;
    [SerializeField] private GameObject FightingUI;
    [SerializeField] private InputManager inputManager;
    [SerializeField] private PreviewSystem previewSystem;

    private Vector3 placementPosition;
    private int selectedObjectID = -1;
    private GridPlacementManager gridPlacementManager;
    private Vector3Int lastDetectedPosition = Vector3Int.zero;

    private void Start()
    {
        StopPlacement();
        gridPlacementManager = GridPlacementManager.Instance;

        if (gridPlacementManager == null)
        {
            Debug.LogError("GridPlacementManager singleton not found in scene!");
        }
    }

    public void StartPlacement(int objectID)
    {
        StopPlacement();

        // Validate that the object ID exists
        if (gridPlacementManager.GetObjectData(objectID) == null)
        {
            Debug.LogError($"No object found with ID {objectID}");
            return;
        }
        Debug.Log($"Starting placement for object with ID {objectID}");
        selectedObjectID = objectID;
        BuildingUI.SetActive(true);
        FightingUI.SetActive(false);
        
        previewSystem.StartShowingPlacementPreview(
            gridPlacementManager.databaseSO.objectData[selectedObjectID].Prefab,
            gridPlacementManager.databaseSO.objectData[selectedObjectID].Size);
    }

    public void PlaceStructure()
    {
        placementPosition = placementObject.transform.position;
        Grid grid = gridPlacementManager.GetGrid();
        Vector3Int gridPosition = grid.WorldToCell(placementPosition);

        // Check with the shared placement manager
        if (!gridPlacementManager.CanPlaceObjectAt(gridPosition, selectedObjectID))
            return;

        // Place the object through the shared placement manager
        gridPlacementManager.PlaceObjectAt(gridPosition, selectedObjectID);
        previewSystem.UpdatePosition(grid.CellToWorld(gridPosition), false);
    }

    public void StopPlacement()
    {
        selectedObjectID = -1;

        BuildingUI.SetActive(false);
        FightingUI.SetActive(true);
        previewSystem.StopShowingPreview();
        lastDetectedPosition = Vector3Int.zero;
    }

    public void IncreaseObjectID()
    {
        int index = selectedObjectID + 1;
        if (gridPlacementManager.GetObjectData(index) == null)
        {
            Debug.LogError($"The ID {index} is too high.");
            index--;
        }
        StartPlacement(index);
    }

    public void DecreaseObjectID()
    {
        int index = selectedObjectID - 1;
        if (gridPlacementManager.GetObjectData(index) == null)
        {
            Debug.LogError($"The ID {index} is too low.");
            index++;
        }
        StartPlacement(index);
    }

    void Update()
    {
        if (selectedObjectID < 0 || gridPlacementManager == null)
            return;
        placementPosition = placementObject.transform.position;
        Grid grid = gridPlacementManager.GetGrid();
        Vector3Int gridPosition = grid.WorldToCell(placementPosition);
        if(lastDetectedPosition != gridPosition)
        {
            lastDetectedPosition = gridPosition;
            // Check validity with the shared placement manager
            bool placementValidity = gridPlacementManager.CanPlaceObjectAt(gridPosition, selectedObjectID);

            grid.CellToWorld(gridPosition);
            previewSystem.UpdatePosition(grid.CellToWorld(gridPosition), placementValidity);
        }
    }
}
