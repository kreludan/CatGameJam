using Godot;
using System.Collections.Generic;

public partial class GameplayConstants
{
	// enums
	public static readonly StringName HandleCollisionString = new("HandleCollision");
	public enum CharacterType{Player, Enemy}
	public enum BulletType{BaseBullet, NotBaseBullet}
	public enum StatusEffect{Freeze, Burn, Poison, Paralysis, Sleep, None}
	public enum TerrainType { GROUND = 1, SAND = 2, WATER = 4, TRAP = 8 }
	public enum TrapType { SPIKES = 1, HOLE = 2 }

	
	// collision layer references
	public enum CollisionLayer
	{
		None = 0,
		PassableTerrain = 1,
		NonPassableTerrain = 2,
		
		Player = 5,
		PlayerBullets = 6,
		Enemies = 7,
		EnemyBullets = 8,
		
		Invulnerable = 20,
		DeactivatedBullets = 21,
		Delete = 22
	}
	
	// Collision mask references for each collision layer.
	// Player scans for non-passable terrain, enemies and enemy bullets.
	private static readonly List<CollisionLayer> PlayerMask = new()
		{ CollisionLayer.NonPassableTerrain, CollisionLayer.Enemies, CollisionLayer.EnemyBullets };
	// Enemies scan for non-passable terrain, player and player bullets.
	private static readonly List<CollisionLayer> EnemiesMask = new()
		{ CollisionLayer.NonPassableTerrain, CollisionLayer.Player, CollisionLayer.PlayerBullets };
	// Player bullets scan for non-passable terrain and enemies.
	private static readonly List<CollisionLayer> PlayerBulletMask = new()
		{ CollisionLayer.NonPassableTerrain, CollisionLayer.Enemies };
	// Enemy bullets scan for non-passable terrain and players.
	private static readonly List<CollisionLayer> EnemiesBulletMask = new()
		{ CollisionLayer.NonPassableTerrain, CollisionLayer.Player };
	// Invulnerable characters scan only for non-passable terrain.
	private static readonly List<CollisionLayer> InvulnerableMask = new(){ CollisionLayer.NonPassableTerrain };
	public static List<CollisionLayer> GetCollisionMasksPerLayer(CollisionLayer layer)
	{
		if(!CollisionMasksPerLayer.ContainsKey(layer)) return new List<CollisionLayer>();
		return CollisionMasksPerLayer[layer];
	}
	private static readonly Dictionary<CollisionLayer, List<CollisionLayer>> CollisionMasksPerLayer =
		new()
		{
			[CollisionLayer.Player] = PlayerMask,
			[CollisionLayer.Enemies] = EnemiesMask,
			[CollisionLayer.PlayerBullets] = PlayerBulletMask,
			[CollisionLayer.EnemyBullets] = EnemiesBulletMask,
			[CollisionLayer.Invulnerable] = InvulnerableMask
		};
	
	// scene & asset references
	public static readonly PackedScene BaseBulletScene = GD.Load<PackedScene>("res://assets/scenes/base_bullet.tscn");
}
