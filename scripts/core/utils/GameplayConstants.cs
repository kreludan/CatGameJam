using Godot;

public partial class GameplayConstants
{
	public static readonly StringName HandleCollisionString = new("HandleCollision");
	public enum CharacterType{Player, BasicEnemy}
	public enum BulletType{BaseBullet, NotBaseBullet}
	public enum StatusEffect{Freeze, Burn, Poison, Paralysis, Sleep, None}
	public enum TerrainType { GROUND = 1, SAND = 2, WATER = 4, TRAP = 8 }
	public enum TrapType { SPIKES = 1, HOLE = 2 }

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
	
	// bullet scene references
	public static PackedScene BaseBulletScene = GD.Load<PackedScene>("res://assets/scenes/base_bullet.tscn");
}
