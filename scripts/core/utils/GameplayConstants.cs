using Godot;
using System.Collections.Generic;

public partial class GameplayConstants
{
	// enums
	public static readonly StringName HandleCollisionString = new("HandleCollision");
	public enum CharacterType{ Player, Enemy }
	public enum BulletType{ BaseBullet, NotBaseBullet }
	public enum StatusEffect{ Freeze, Burn, Poison, Paralysis, Sleep, None }
	public enum TerrainType { Ground = 1, Sand = 2, Water = 4, Trap = 8 }
	public enum TrapType { Spikes = 1, Hole = 2 }

	
	// collision layer references
	public enum CollisionLayer
	{
		None = 0,
		PassableTerrain = 1,
		NonPassableTerrain = 2,
		DynamicTerrain = 3,
		
		Player = 5,
		PlayerBullets = 6,
		Enemies = 7,
		EnemyBullets = 8,
		PlayerPassable = 9,  // layer for a player to pass through dynamic terrain
		EnemiesPassable = 10, // layer for flying enemies to pass through dynamic terrain
		
		Invulnerable = 20,
		DeactivatedBullets = 21,
		Delete = 22
	}
	
	// Collision mask references for each collision layer.
	// Player scans for non-passable terrain, enemies and enemy bullets.
	private static readonly List<CollisionLayer> PlayerMask = new()
		{ CollisionLayer.NonPassableTerrain, CollisionLayer.DynamicTerrain, CollisionLayer.Enemies, CollisionLayer.EnemyBullets };
	// PlayerPassable scans for non dynamic terrain.
	private static readonly List<CollisionLayer> PlayerPassableMask = new()
		{ CollisionLayer.NonPassableTerrain, CollisionLayer.Enemies, CollisionLayer.EnemyBullets };
	// Enemies scan for non-passable terrain, player and player bullets.
	private static readonly List<CollisionLayer> EnemiesMask = new()
		{ CollisionLayer.NonPassableTerrain, CollisionLayer.DynamicTerrain, CollisionLayer.Player, CollisionLayer.PlayerBullets };
	// PlayerPassable scans for non dynamic terrain.
	private static readonly List<CollisionLayer> EnemiesPassableMask = new()
		{ CollisionLayer.NonPassableTerrain, CollisionLayer.Player, CollisionLayer.PlayerBullets };
	// Player bullets scan for non-passable terrain and enemies.
	private static readonly List<CollisionLayer> PlayerBulletMask = new()
		{ CollisionLayer.NonPassableTerrain, CollisionLayer.Enemies };
	// Enemy bullets scan for non-passable terrain and players.
	private static readonly List<CollisionLayer> EnemiesBulletMask = new()
		{ CollisionLayer.NonPassableTerrain, CollisionLayer.Player };
	// Invulnerable characters scan only for non-passable terrain.
	private static readonly List<CollisionLayer> InvulnerableMask = new()
		{ CollisionLayer.NonPassableTerrain };
	
	public static List<CollisionLayer> GetCollisionMasksPerLayer(CollisionLayer layer)
	{
		return !CollisionMasksPerLayer.ContainsKey(layer) ? new List<CollisionLayer>() : CollisionMasksPerLayer[layer];
	}
	
	private static readonly Dictionary<CollisionLayer, List<CollisionLayer>> CollisionMasksPerLayer =
		new()
		{
			[CollisionLayer.Player] = PlayerMask,
			[CollisionLayer.PlayerPassable] = PlayerPassableMask,
			[CollisionLayer.Enemies] = EnemiesMask,
			[CollisionLayer.EnemiesPassable] = EnemiesPassableMask,
			[CollisionLayer.PlayerBullets] = PlayerBulletMask,
			[CollisionLayer.EnemyBullets] = EnemiesBulletMask,
			[CollisionLayer.Invulnerable] = InvulnerableMask
		};
	
	
	// scene & asset references
	public static readonly PackedScene BaseBulletScene = GD.Load<PackedScene>("res://assets/scenes/base_bullet.tscn");
}