using System;
using System.Collections.Generic;
using Godot;

public partial class TerrainDetector : Area2D
{
    private TileMap _currentTileMap;
    private TrapHandler _trapHandler;
    private Entity _entity;
    private readonly Dictionary<Vector2I, int> _activeTraps = new();

    public override void _Ready()
    {
        _entity = Owner as Entity;
        _trapHandler = Owner.GetNode<TrapHandler>("TrapHandler");
    }

    /// <summary>
    /// Retrieves a dictionary of active traps within the tile coordinates where the entity is currently standing.
    /// The dictionary keys represent unique trap coordinates, and the values correspond to the trap IDs (type of trap).
    /// </summary>
    /// <returns>A dictionary containing trap coordinates as keys and trap IDs as values.</returns>
    public Dictionary<Vector2I, int> GetActiveTraps()
    {
        return _activeTraps;
    }

    private void ProcessTileMap(Node2D body, Rid bodyRid, Action<TileData, Vector2I> processAction)
    {
        _currentTileMap = (TileMap)body;
        Vector2I tileCoords = _currentTileMap.GetCoordsForBodyRid(bodyRid);
        for (int i = 0; i < _currentTileMap.GetLayersCount(); i++)
        {
            TileData tileData = _currentTileMap.GetCellTileData(i, tileCoords);
            if (tileData is null) continue;

            processAction(tileData, tileCoords);
        }
    }

    private void CheckForTraps(TileData tileData, Vector2I tileCoords)
    {
        Variant tileMask = tileData.GetCustomDataByLayerId(0);
        if (tileMask.AsInt32() != (int)GameplayConstants.TerrainType.Trap) return;
        if (_entity.GetCollisionLayerValue(GameplayConstants.GetLayerBit(GameplayConstants.CollisionLayer.Invulnerable))) return;
        
        int trapId = tileData.GetCustomDataByLayerId(1).AsInt32();
        if (!_activeTraps.ContainsKey(tileCoords))
        {
            // Trap is stepped on for the first time
            _activeTraps.Add(tileCoords, trapId);
            _trapHandler.HandleTrap(trapId);
            GD.Print("Entering Trap: " + tileCoords);
        }
    }
    
    private void HandleTrapExit(TileData tileData, Vector2I tileCoords)
    {
        Variant tileMask = tileData.GetCustomDataByLayerId(0);
        if (tileMask.AsInt32() != (int)GameplayConstants.TerrainType.Trap) return;
        
        if (_activeTraps.TryGetValue(tileCoords, out int _))
        {
            // Remove trap from the active set when exited
            _activeTraps.Remove(tileCoords);
            GD.Print("Exiting Trap: " + tileCoords);
        }
    }

    public void _on_body_shape_entered(Rid bodyRid, Node2D body, int bodyShapeIndex, int localShapeIndex)
    {
        if (body is TileMap)
        {
            ProcessTileMapCollision(body, bodyRid);
        }
    }
    
    private void ProcessTileMapCollision(Node2D body, Rid bodyRid)
    {
        ProcessTileMap(body, bodyRid, CheckForTraps);
    }

    public void _on_body_shape_exited(Rid bodyRid, Node2D body, int bodyShapeIndex, int localShapeIndex)
    {
        if (body is TileMap)
        {
            ProcessTileMapExit(body, bodyRid);
        }
    }
    
    private void ProcessTileMapExit(Node2D body, Rid bodyRid)
    {
        ProcessTileMap(body, bodyRid, HandleTrapExit);
    }
}