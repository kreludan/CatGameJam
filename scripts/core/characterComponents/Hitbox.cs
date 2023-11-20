using Godot;

public partial class Hitbox : Node2D
{
	[Export] public int Damage;
	[Export] public float KnockbackIntensity;
	[Export] public GameplayConstants.StatusEffect StatusEffect;
	private Health _health;

	public Node2D GetOwner()
	{
		return Owner as Node2D;
	}
}
