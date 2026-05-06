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
                    database.objectData[selectedObjectIndex].Size);
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

    public void OnAction(Vector3Int gridPosition)
    {
        bool placementValidity = CheckPlacementValidity(gridPosition, selectedObjectIndex);
        if (placementValidity == false)
        {
            //soundFeedback.PlaySound(SoundType.wrongPlacement);
            return;
        }
        //soundFeedback.PlaySound(SoundType.Place);
        int index = objectPlacer.PlaceObject(database.objectData[selectedObjectIndex].Prefab,
            grid.CellToWorld(gridPosition));

        GridData selectedData = database.objectData[selectedObjectIndex].ID == 0 ?
            floorData :
            furnitureData;
        selectedData.AddObjectAt(gridPosition,
            database.objectData[selectedObjectIndex].Size,
            database.objectData[selectedObjectIndex].ID,
            index);

        // Preview updates are handled by PlayerBuildSystem
    }

    private bool CheckPlacementValidity(Vector3Int gridPosition, int selectedObjectIndex)
    {
        GridData selectedData = database.objectData[selectedObjectIndex].ID == 0 ?
            floorData :
            furnitureData;

        Vector2Int size = database.objectData[selectedObjectIndex].Size;
        
        // Check if any tile the object would occupy is blocked
        for (int x = 0; x < size.x; x++)
        {
            for (int y = 0; y < size.y; y++)
            {
                Vector3Int checkPos = gridPosition + new Vector3Int(x, y, 0);
                if (BlockedTilesData.Instance.IsBlocked(checkPos))
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

