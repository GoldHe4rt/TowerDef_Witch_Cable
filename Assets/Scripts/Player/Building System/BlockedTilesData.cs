using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public enum BlockedTileLayer
{
    Floor,
    Furniture,
    Both
}

public class BlockedTilesData : MonoBehaviour
{
    public static BlockedTilesData Instance { get; private set; }

    [SerializeField] private Tilemap floorBlockedTilemap;
    [SerializeField] private Tilemap furnitureBlockedTilemap;
    [SerializeField] private Tilemap blockedTilemap; // legacy support
    [SerializeField] private Grid grid;
    
    private HashSet<Vector3Int> floorBlockedTiles = new();
    private HashSet<Vector3Int> furnitureBlockedTiles = new();

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
        // Load all blocked tiles from the tilemaps
        if (floorBlockedTilemap != null || furnitureBlockedTilemap != null || blockedTilemap != null)
        {
            RefreshBlockedTiles();
        }
    }

    /// <summary>
    /// Refreshes blocked tiles from the configured tilemaps
    /// </summary>
    public void RefreshBlockedTiles()
    {
        floorBlockedTiles.Clear();
        furnitureBlockedTiles.Clear();

        RefreshBlockedTilesFrom(floorBlockedTilemap, floorBlockedTiles);
        RefreshBlockedTilesFrom(furnitureBlockedTilemap, furnitureBlockedTiles);
        RefreshBlockedTilesFrom(blockedTilemap, floorBlockedTiles);
        RefreshBlockedTilesFrom(blockedTilemap, furnitureBlockedTiles);
    }

    private void RefreshBlockedTilesFrom(Tilemap tilemap, HashSet<Vector3Int> blockedSet)
    {
        if (tilemap == null)
            return;

        foreach (Vector3Int pos in tilemap.cellBounds.allPositionsWithin)
        {
            if (tilemap.HasTile(pos))
            {
                blockedSet.Add(pos);
            }
        }
    }

    /// <summary>
    /// Marks a tile as blocked for a specific layer
    /// </summary>
    public void BlockTile(Vector3Int gridPosition, BlockedTileLayer layer = BlockedTileLayer.Both)
    {
        if (layer == BlockedTileLayer.Floor || layer == BlockedTileLayer.Both)
        {
            floorBlockedTiles.Add(gridPosition);
        }

        if (layer == BlockedTileLayer.Furniture || layer == BlockedTileLayer.Both)
        {
            furnitureBlockedTiles.Add(gridPosition);
        }
    }

    /// <summary>
    /// Unblocks a tile for a specific layer
    /// </summary>
    public void UnblockTile(Vector3Int gridPosition, BlockedTileLayer layer = BlockedTileLayer.Both)
    {
        if (layer == BlockedTileLayer.Floor || layer == BlockedTileLayer.Both)
        {
            floorBlockedTiles.Remove(gridPosition);
        }

        if (layer == BlockedTileLayer.Furniture || layer == BlockedTileLayer.Both)
        {
            furnitureBlockedTiles.Remove(gridPosition);
        }
    }

    /// <summary>
    /// Checks if a tile is blocked on either layer
    /// </summary>
    public bool IsBlocked(Vector3Int gridPosition)
    {
        return floorBlockedTiles.Contains(gridPosition) || furnitureBlockedTiles.Contains(gridPosition);
    }

    /// <summary>
    /// Checks whether a tile is blocked for a specific layer
    /// </summary>
    public bool IsBlocked(Vector3Int gridPosition, bool forFurniture)
    {
        return forFurniture ? furnitureBlockedTiles.Contains(gridPosition) : floorBlockedTiles.Contains(gridPosition);
    }

    /// <summary>
    /// Gets all blocked tiles from both layers
    /// </summary>
    public IReadOnlyCollection<Vector3Int> GetBlockedTiles()
    {
        HashSet<Vector3Int> allBlocked = new(floorBlockedTiles);
        allBlocked.UnionWith(furnitureBlockedTiles);
        return allBlocked;
    }

    /// <summary>
    /// Clears all blocked tiles
    /// </summary>
    public void ClearBlockedTiles()
    {
        floorBlockedTiles.Clear();
        furnitureBlockedTiles.Clear();
    }
}
