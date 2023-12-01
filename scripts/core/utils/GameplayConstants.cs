using System;
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

	[Flags]
	public enum CollisionLayer
	{
		None = 1 << 0,
		PassableTerrain = 1 << 1,
		NonPassableTerrain = 1 << 2,
		DynamicTerrain = 1 << 3,
    
		Player = 1 << 5,
		PlayerBullets = 1 << 6,
		Enemies = 1 << 7,
		EnemyBullets = 1 << 8,
		PlayerPassable = 1 << 9,  // layer for a player to pass through dynamic terrain
		EnemiesPassable = 1 << 10, // layer for flying enemies to pass through dynamic terrain
    
		Invulnerable = 1 << 20,
		DeactivatedBullets = 1 << 21,
		Delete = 1 << 22
	}
	
	private static readonly Dictionary<CollisionLayer, int> LayerMasks = new()
	{
		{ CollisionLayer.Player, (int)(CollisionLayer.NonPassableTerrain | CollisionLayer.DynamicTerrain | CollisionLayer.Enemies | CollisionLayer.EnemyBullets) },
		{ CollisionLayer.PlayerPassable, (int)(CollisionLayer.NonPassableTerrain | CollisionLayer.Enemies | CollisionLayer.EnemyBullets) },
		{ CollisionLayer.Enemies, (int)(CollisionLayer.NonPassableTerrain | CollisionLayer.DynamicTerrain | CollisionLayer.Player | CollisionLayer.PlayerBullets) },
		{ CollisionLayer.EnemiesPassable, (int)(CollisionLayer.NonPassableTerrain | CollisionLayer.Player | CollisionLayer.PlayerBullets) },
		{ CollisionLayer.PlayerBullets, (int)(CollisionLayer.NonPassableTerrain | CollisionLayer.Enemies) },
		{ CollisionLayer.EnemyBullets, (int)(CollisionLayer.NonPassableTerrain | CollisionLayer.Player) },
		{ CollisionLayer.Invulnerable, (int)CollisionLayer.NonPassableTerrain }
		// Add more layers as needed
	};

	public static int GetMask(CollisionLayer layer)
	{
		return LayerMasks.TryGetValue(layer, out int mask) ? mask : 0;
	}
	
	public static int GetLayerBit(CollisionLayer layer)
	{
		return (int)Math.Log((int)layer, 2);
	}

	// scene & asset references
	public static readonly PackedScene BaseBulletScene = GD.Load<PackedScene>("res://assets/scenes/base_bullet.tscn");
}