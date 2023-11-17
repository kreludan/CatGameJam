using Godot;
using System;

public partial class Player : Entity
{
	public override void _Ready()
	{
		base._Ready();
		CharacterType = GameplayConstants.CharacterType.Player;
		BaseCollisionLayer = GameplayConstants.CollisionLayer.Player;
		SetCollisionLayerAndMask(BaseCollisionLayer);
	}
}
