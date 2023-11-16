using Godot;
using System;

public partial class Hitbox : Node2D
{
	[Export] public int Damage;
	[Export] public float KnockbackIntensity;
	[Export] public GameplayConstants.StatusEffect StatusEffect;

	public Node2D GetOwner()
	{
		return Owner as Node2D;
	}
}
