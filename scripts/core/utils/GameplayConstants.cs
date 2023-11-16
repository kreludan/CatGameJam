using Godot;
using System;

public partial class GameplayConstants
{
	public static readonly StringName HandleCollisionString = new("HandleCollision");
	public enum CharacterType{Player, BasicEnemy}
	public enum BulletType{BaseBullet, NotBaseBullet}
	public enum StatusEffect{Freeze, Burn, Poison, Paralysis, Sleep, None}

	public enum CollisionLayers
	{
		Walls = 1,
		Player = 2,
		PlayerBullets = 3,
		Enemies = 4,
		EnemyBullets = 5,
		
		DeactivatedPlayerBullets = 20,
		DeactivatedEnemyBullets = 21,
		Delete = 22
	}
	
	// bullet scene references
	public static PackedScene BaseBulletScene = GD.Load<PackedScene>("res://assets/scenes/base_bullet.tscn");
}
