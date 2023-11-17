using Godot;
using System.Collections.Generic;

public partial class GameplayConstants
{
	public static readonly StringName HandleCollisionString = new("HandleCollision");
	public enum CharacterType{Player, BasicEnemy}
	public enum BulletType{BaseBullet, NotBaseBullet}
	public enum StatusEffect{Freeze, Burn, Poison, Paralysis, Sleep, None}
	public enum TerrainType { GROUND = 1, SAND = 2, WATER = 4, TRAP = 8 }
	public enum TrapType { SPIKES = 1, HOLE = 2 }

	
	// collision layer references
	public enum CollisionLayers
	{
		PassableTerrain = 1,
		NonPassableTerrain = 2,
		
		Player = 5,
		PlayerBullets = 6,
		Enemies = 7,
		EnemyBullets = 8,
		
		DeactivatedBullets = 21,
		Delete = 22
	}
	
	// Collision mask references for each collision layer.
	// Player scans for non-passable terrain, enemies and enemy bullets.
	private static List<CollisionLayers> _playerMask = new List<CollisionLayers>()
		{ CollisionLayers.NonPassableTerrain, CollisionLayers.Enemies, CollisionLayers.EnemyBullets };
	// Enemies scan for non-passable terrain, player and player bullets.
	private static List<CollisionLayers> _enemiesMask = new List<CollisionLayers>()
		{ CollisionLayers.NonPassableTerrain, CollisionLayers.Player, CollisionLayers.PlayerBullets };
	// Player bullets scan for non-passable terrain and enemies.
	private static List<CollisionLayers> _playerBulletsMask = new List<CollisionLayers>()
		{ CollisionLayers.NonPassableTerrain, CollisionLayers.Enemies };
	// Enemy bullets scan for non-passable terrain and players.
	private static List<CollisionLayers> _enemiesBulletMask = new List<CollisionLayers>()
		{ CollisionLayers.NonPassableTerrain, CollisionLayers.Player };
	
	public static Dictionary<CollisionLayers, List<CollisionLayers>> collisionMasksPerLayer =
		new Dictionary<CollisionLayers, List<CollisionLayers>>()
		{
			[CollisionLayers.Player] = _playerMask,
			[CollisionLayers.Enemies] = _enemiesMask,
			[CollisionLayers.PlayerBullets] = _playerBulletsMask,
			[CollisionLayers.EnemyBullets] = _enemiesBulletMask
		};
	
	// bullet scene references
	public static PackedScene BaseBulletScene = GD.Load<PackedScene>("res://assets/scenes/base_bullet.tscn");
}
