using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemovingState : IBuildingState
{
    private int gameObjectIndex = -1;
    Grid grid;
    PreviewSystem previewSystem;
    GridData floorData;
    GridData furnitureData;
    ObjectPlacer objectPlacer;

    public RemovingState(Grid grid,
                         PreviewSystem previewSystem,
                         GridData floorData,
                         GridData furnitureData,
                         ObjectPlacer objectPlacer)
    {
        this.grid = grid;
        this.previewSystem = previewSystem;
        this.floorData = floorData;
        this.furnitureData = furnitureData;
        this.objectPlacer = objectPlacer;
        
        if (previewSystem != null)
        {
            previewSystem.StartShowingRemovePreview();
        }
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
        // Check if tile is blocked
        if (BlockedTilesData.Instance.IsBlocked(gridPosition))
        {
            return;
        }

        GridData selectedData = null;
        if(furnitureData.CanPlaceObjectAt(gridPosition,Vector2Int.one) == false)
        {
            selectedData = furnitureData;
        }
        else if(floorData.CanPlaceObjectAt(gridPosition, Vector2Int.one) == false)
        {
            selectedData = floorData;
        }

        if(selectedData == null)
        {
            //sound
            //soundFeedback.PlaySound(SoundType.wrongPlacement);
        }
        else
        {
            //soundFeedback.PlaySound(SoundType.Remove);
            gameObjectIndex = selectedData.GetRepresentationIndex(gridPosition);
            if (gameObjectIndex == -1)
                return;
            selectedData.RemoveObjectAt(gridPosition);
            objectPlacer.RemoveObjectAt(gameObjectIndex);
        }
        
        // Preview updates are handled by PlayerBuildSystem
    }

    private bool CheckIfSelectionIsValid(Vector3Int gridPosition)
    {
        return !(furnitureData.CanPlaceObjectAt(gridPosition, Vector2Int.one) &&
            floorData.CanPlaceObjectAt(gridPosition, Vector2Int.one));
    }

    public void UpdateState(Vector3Int gridPosition)
    {
        // Preview updates are handled by PlayerBuildSystem, not by state
    }
}
