using System;
using UnityEngine;
using UnityEngine.Scripting.APIUpdating;

public class PlayerBuildSystem : MonoBehaviour
{
    [SerializeField] private GameObject placementObject;
    [SerializeField] private GameObject BuildingUI;
    [SerializeField] private GameObject FightingUI;
    [SerializeField] private InputManager inputManager;
    [SerializeField] private PreviewSystem previewSystem;
    [SerializeField] private CurrencyManager currencyManager;
    [SerializeField] private int playerID = 1;
    

    private Vector3 placementPosition;
    private int selectedObjectID = -1;
    private GridPlacementManager gridPlacementManager;
    private Vector3Int lastDetectedPosition = Vector3Int.zero;
    private bool isPlacing = false;
    private bool isRemoving = false;

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
        isPlacing = true;
        isRemoving = false;
        
        gridPlacementManager.StartPlacementState(objectID);
        // Don't pass preview to state - we'll handle preview updates locally
        gridPlacementManager.SetPreviewSystem(null);
        
        BuildingUI.SetActive(true);
        FightingUI.SetActive(false);
        
        previewSystem.StartShowingPlacementPreview(
            gridPlacementManager.databaseSO.objectData[selectedObjectID].Prefab,
            gridPlacementManager.databaseSO.objectData[selectedObjectID].Size,
            gridPlacementManager.databaseSO.objectData[selectedObjectID].Name,
            gridPlacementManager.databaseSO.objectData[selectedObjectID].Cost
        );
    }

    public void StartRemoving()
    {
        StopPlacement();
        isPlacing = false;
        isRemoving = true;
        
        gridPlacementManager.StartRemovingState();
        // Don't pass preview to state - we'll handle preview updates locally
        gridPlacementManager.SetPreviewSystem(null);
        
        BuildingUI.SetActive(true);
        FightingUI.SetActive(false);
        
        previewSystem.StartShowingRemovePreview();
    }

    public void PlaceStructure()
    {
        if (!isPlacing && !isRemoving)
            return;
        if (isPlacing && !currencyManager.PlayerHasSufficientCurrency(playerID, gridPlacementManager.GetObjectData(selectedObjectID).Cost))
        {
            return;
        }
        placementPosition = placementObject.transform.position;
        Grid grid = gridPlacementManager.GetGrid();
        Vector3Int gridPosition = grid.WorldToCell(placementPosition);

        gridPlacementManager.ExecuteStateAction(gridPosition, selectedObjectID, playerID);
    }

    public void StopPlacement()
    {
        selectedObjectID = -1;
        isPlacing = false;
        isRemoving = false;

        BuildingUI.SetActive(false);
        FightingUI.SetActive(true);
        previewSystem.StopShowingPreview();
        lastDetectedPosition = Vector3Int.zero;
        
        if (gridPlacementManager != null)
        {
            gridPlacementManager.StopState();
        }
    }

    public void IncreaseObjectID()
    {
        int index = selectedObjectID + 1;
        if (gridPlacementManager.GetObjectData(index) == null)
        {
            StartRemoving();
            return;
        }
        StartPlacement(index);
    }

    public void DecreaseObjectID()
    {
        int index = selectedObjectID - 1;
        if (index == -1)
        {
            StartRemoving();
            return;
        } else if (index == -2)
        {
            index = gridPlacementManager.databaseSO.objectData[gridPlacementManager.databaseSO.objectData.Count - 1].ID;
        }

        StartPlacement(index);
    }

    void Update()
    {
        if (!isRemoving)
            if (selectedObjectID < 0 || gridPlacementManager == null)
                return;
        placementPosition = placementObject.transform.position;
        Grid grid = gridPlacementManager.GetGrid();
        Vector3Int gridPosition = grid.WorldToCell(placementPosition);
        if(lastDetectedPosition != gridPosition)
        {
            lastDetectedPosition = gridPosition;
            // Get the validity from the state
            bool isValid = gridPlacementManager.CheckPlacementValidity(gridPosition, selectedObjectID);
            if (!isRemoving)
                if (!currencyManager.PlayerHasSufficientCurrency(playerID, gridPlacementManager.GetObjectData(selectedObjectID).Cost))
                    isValid = false;
            
            previewSystem.UpdatePosition(grid.CellToWorld(gridPosition), isValid);
        }
    }
}
