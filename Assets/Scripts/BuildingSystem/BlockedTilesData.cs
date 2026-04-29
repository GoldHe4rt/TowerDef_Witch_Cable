using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class BlockedTilesData : MonoBehaviour
{
    public static BlockedTilesData Instance { get; private set; }

    [SerializeField] private Tilemap blockedTilemap;
    [SerializeField] private Grid grid;
    
    private HashSet<Vector3Int> blockedTiles = new();

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
        // Load all blocked tiles from the tilemap
        if (blockedTilemap != null)
        {
            RefreshBlockedTiles();
        }
    }

    /// <summary>
    /// Refreshes blocked tiles from the tilemap
    /// </summary>
    public void RefreshBlockedTiles()
    {
        blockedTiles.Clear();
        
        if (blockedTilemap == null)
            return;

        foreach (Vector3Int pos in blockedTilemap.cellBounds.allPositionsWithin)
        {
            if (blockedTilemap.HasTile(pos))
            {
                blockedTiles.Add(pos);
            }
        }
    }

    /// <summary>
    /// Marks a tile as blocked
    /// </summary>
    public void BlockTile(Vector3Int gridPosition)
    {
        blockedTiles.Add(gridPosition);
    }

    /// <summary>
    /// Unblocks a tile
    /// </summary>
    public void UnblockTile(Vector3Int gridPosition)
    {
        blockedTiles.Remove(gridPosition);
    }

    /// <summary>
    /// Checks if a tile is blocked
    /// </summary>
    public bool IsBlocked(Vector3Int gridPosition)
    {
        return blockedTiles.Contains(gridPosition);
    }

    /// <summary>
    /// Gets all blocked tiles
    /// </summary>
    public IReadOnlyCollection<Vector3Int> GetBlockedTiles()
    {
        return blockedTiles;
    }

    /// <summary>
    /// Clears all blocked tiles
    /// </summary>
    public void ClearBlockedTiles()
    {
        blockedTiles.Clear();
    }
}
