using Godot;
using System;

public partial class GameplayConstants
{
	public static readonly StringName HandleCollisionString = new("HandleCollision");
	public enum CharacterType{Player, BasicEnemy}
	public enum BulletType{BaseBullet, NotBaseBullet}
	public enum StatusEffect{Freeze, Burn, Poison, Paralysis, Sleep, None}
	
	// bullet scene references
	public static PackedScene BaseBulletScene = GD.Load<PackedScene>("res://assets/scenes/base_bullet.tscn");
}
