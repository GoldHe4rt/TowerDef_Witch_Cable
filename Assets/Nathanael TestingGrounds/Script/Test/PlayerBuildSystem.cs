using System;
using UnityEditor;
using UnityEngine;

public class PlayerBuildSystem : MonoBehaviour
{
    [SerializeField] private GameObject placementObject;
    [SerializeField] private GameObject cellIndicator;
    [SerializeField] private Grid grid;
    private Vector3 placementPosition;

    [SerializeField] private InputManager inputManager;
    [SerializeField] private ObjectDatabaseSO databaseSO;
    private int selectedObjectIndex = -1;

    [SerializeField] private GameObject gridVisualization;

    private void Start()
    {
        StopPlacement();
    }

    public void StartPlacement(int ID)
    {
        StopPlacement();
        selectedObjectIndex = databaseSO.objectData.FindIndex(data => data.ID == ID);
        if(selectedObjectIndex < 0)
        {
            Debug.LogError($"No ID found {ID}");
            return;
        }
        gridVisualization.SetActive(true);
        cellIndicator.SetActive(true);
        inputManager.OnClicked += PlaceStructure;
        inputManager.OnExit += StopPlacement;
    }
    
    private void PlaceStructure()
    {
        if (inputManager.IsPointerOverUI())
        {
            return;
        }
        placementPosition = placementObject.transform.position;
        Vector3Int gridPosition = grid.WorldToCell(placementPosition);
        GameObject newObject = Instantiate(databaseSO.objectData[selectedObjectIndex].Prefab);
        newObject.transform.position = grid.CellToWorld(gridPosition);
    }

    private void StopPlacement()
    {
        selectedObjectIndex = -1;
        gridVisualization.SetActive(false);
        cellIndicator.SetActive(false);
        inputManager.OnClicked -= PlaceStructure;
        inputManager.OnExit -= StopPlacement;
    }

    void Update()
    {
        if(selectedObjectIndex < 0)
            return;
        placementPosition = placementObject.transform.position;
        Vector3Int gridPosition = grid.WorldToCell(placementPosition);
        cellIndicator.transform.position = grid.CellToWorld(gridPosition);
    }

}
