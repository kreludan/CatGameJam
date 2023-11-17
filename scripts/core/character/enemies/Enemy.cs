using Godot;
using System;

public partial class Enemy : Entity
{
	public override void _Ready()
	{
		base._Ready();
		CharacterType = GameplayConstants.CharacterType.Enemy;
		BaseCollisionLayer = GameplayConstants.CollisionLayer.Enemies;
		SetCollisionLayerAndMask(BaseCollisionLayer);
	}
}
