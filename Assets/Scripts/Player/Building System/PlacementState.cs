using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Unity.VisualScripting.Member;

public class PlacementState : IBuildingState
{
    private int selectedObjectIndex = -1;
    int ID;
    Grid grid;
    PreviewSystem previewSystem;
    ObjectDatabaseSO database;
    GridData floorData;
    GridData furnitureData;
    ObjectPlacer objectPlacer;

    public PlacementState(int iD,
                          Grid grid,
                          PreviewSystem previewSystem,
                          ObjectDatabaseSO database,
                          GridData floorData,
                          GridData furnitureData,
                          ObjectPlacer objectPlacer)
    {
        ID = iD;
        this.grid = grid;
        this.previewSystem = previewSystem;
        this.database = database;
        this.floorData = floorData;
        this.furnitureData = furnitureData;
        this.objectPlacer = objectPlacer;

        selectedObjectIndex = database.objectData.FindIndex(data => data.ID == ID);
        if (selectedObjectIndex > -1)
        {
            if (previewSystem != null)
            {
                previewSystem.StartShowingPlacementPreview(
                    database.objectData[selectedObjectIndex].Prefab,
                    database.objectData[selectedObjectIndex].Size,
                    database.objectData[selectedObjectIndex].Name,
                    database.objectData[selectedObjectIndex].Cost
                    );
            }
        }
        else
            throw new System.Exception($"No object with ID {iD}");
        
    }

    public void SetPreviewSystem(PreviewSystem preview)
    {
        previewSystem = preview;
    }

    public void EndState()
    {
        // Preview cleanup is handled by PlayerBuildSystem, not by state
    }

    public void OnAction(Vector3Int gridPosition, int playerID, CurrencyManager currencyManager)
    {
        bool placementValidity = CheckPlacementValidity(gridPosition, selectedObjectIndex);
        if (placementValidity == false)
        {
            //soundFeedback.PlaySound(SoundType.wrongPlacement);
            return;
        }
        //soundFeedback.PlaySound(SoundType.Place);

        currencyManager.RemoveCurrency(database.objectData[selectedObjectIndex].Cost, playerID);
        int index = objectPlacer.PlaceObject(database.objectData[selectedObjectIndex].Prefab,
            grid.CellToWorld(gridPosition), playerID, currencyManager);

        ObjectData objectData = database.objectData[selectedObjectIndex];
        GridData selectedData = objectData.IsFloor ?
            floorData :
            furnitureData;
        selectedData.AddObjectAt(gridPosition,
            objectData.Size,
            objectData.ID,
            index);

        // Preview updates are handled by PlayerBuildSystem
    }

    private bool CheckPlacementValidity(Vector3Int gridPosition, int selectedObjectIndex)
    {
        ObjectData objectData = database.objectData[selectedObjectIndex];
        GridData selectedData = objectData.IsFloor ?
            floorData :
            furnitureData;

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

    public void UpdateState(Vector3Int gridPosition)
    {
        // Preview updates are handled by PlayerBuildSystem, not by state
    }
}

