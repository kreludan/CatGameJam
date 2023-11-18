using Godot;
using System.Diagnostics;

public partial class TerrainDetector : Area2D
{
	private TileMap _currentTileMap;
	private TrapHandler _trapHandler;
	private Entity _entity;

	public override void _Ready()
	{
		_entity = Owner as Entity;
		_trapHandler = Owner.GetNode<TrapHandler>("TrapHandler");
	}

	private void ProcessTileMapCollision(Node2D body, Rid bodyRid)
	{
		_currentTileMap = (TileMap)body;
		Vector2I collidedTileCoords = _currentTileMap.GetCoordsForBodyRid(bodyRid);
		for (int i = 0; i < _currentTileMap.GetLayersCount(); i++)
		{
			TileData tileData = _currentTileMap.GetCellTileData(i, collidedTileCoords);
			if (tileData is null) continue;
			
			Variant tileMask = tileData.GetCustomDataByLayerId(0);
			Debug.Print("Tile mask: " + tileMask);
			if (tileMask.AsInt32() == (int)GameplayConstants.TerrainType.TRAP)
			{
				if (_entity.GetCollisionLayerValue((int)GameplayConstants.CollisionLayer.Invulnerable)) return;
				
				Variant trapMask = tileData.GetCustomDataByLayerId(1);
				Debug.Print("Trap mask: " + trapMask);
				_trapHandler.HandleTrap(trapMask.AsInt32());
			}
		}
	}

	//signals
	public void _on_body_shape_entered(Rid bodyRid, Node2D body, int bodyShapeIndex, int localShapeIndex)
	{
		if (body is TileMap)
		{
			ProcessTileMapCollision(body, bodyRid);
		}
	}
}
