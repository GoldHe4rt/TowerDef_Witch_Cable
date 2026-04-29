using UnityEngine;

public class BlockTileMarker : MonoBehaviour
{
    [SerializeField] private Grid grid;
    [SerializeField] private bool isBlockedTile = true;

    private void OnEnable()
    {
        if (isBlockedTile && grid != null)
        {
            Vector3Int gridPosition = grid.WorldToCell(transform.position);
            BlockedTilesData.Instance.BlockTile(gridPosition);
        }
    }

    private void OnDisable()
    {
        if (grid != null)
        {
            Vector3Int gridPosition = grid.WorldToCell(transform.position);
            BlockedTilesData.Instance.UnblockTile(gridPosition);
        }
    }

    #if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        if (grid == null)
            return;

        Vector3Int gridPosition = grid.WorldToCell(transform.position);
        Vector3 cellCenter = grid.CellToWorld(gridPosition) + grid.cellSize * 0.5f;

        Gizmos.color = isBlockedTile ? Color.red : Color.green;
        Gizmos.DrawWireCube(cellCenter, grid.cellSize);
    }
    #endif
}
