using Godot;
using Godot.Collections;

public partial class PassableTiles : TileMap
{
	private TileMap _currentTileMap;
	private Array<Vector2I> _nonTrapTileCoordinates = new();

	public override void _Ready()
	{
		_currentTileMap = this;
		SetNonTrapTiles();
		// Uncomment the following line to print tile coordinates and check the random non-trap position
		// PrintTileCoordinates();
		// GD.Print(GetRandomNonTrapTilePosition());
	}

	private void PrintTileCoordinates()
	{
		Array<Vector2I> tileCoordinates = GetUsedCells(0);
		foreach (Vector2I coordinate in tileCoordinates)
		{
			GD.Print("Tile at coordinates: ", coordinate);
			GD.Print("Is Trap: " + IsTrap(coordinate));
			GD.Print("Tile world position: ", MapToLocal(coordinate));
		}
	}
	
	private void SetNonTrapTiles()
	{
		Array<Vector2I> allTileCoordinates = GetUsedCells(0);
		foreach (Vector2I coordinate in allTileCoordinates)
		{
			if (!IsTrap(coordinate))
			{
				_nonTrapTileCoordinates.Add(coordinate);
			}
		}
	}
	
	private Vector2 GetRandomTilePosition()
	{
		Array<Vector2I> tileCoordinates = GetUsedCells(0);
		if (tileCoordinates.Count == 0)
		{
			GD.Print("No used cells found.");
			return Vector2.Zero;
		}
		int randomIndex = GD.RandRange(0, tileCoordinates.Count - 1);
		return MapToLocal(tileCoordinates[randomIndex]);
	}

	public Vector2 GetRandomNonTrapTilePosition()
	{
		Array<Vector2I> nonTrapTileCoordinates = _nonTrapTileCoordinates;
		if (nonTrapTileCoordinates.Count == 0)
		{
			GD.Print("No non-trap tiles found.");
			return Vector2.Zero;
		}
		int randomIndex = GD.RandRange(0, nonTrapTileCoordinates.Count - 1);
		return MapToLocal(nonTrapTileCoordinates[randomIndex]);
	}

	private bool IsTrap(Vector2I tileCoords)
	{
		Variant tileMask = GetCellTileData(0, tileCoords).GetCustomDataByLayerId(0);
		return tileMask.AsInt32() == (int)GameplayConstants.TerrainType.Trap;
	}
}