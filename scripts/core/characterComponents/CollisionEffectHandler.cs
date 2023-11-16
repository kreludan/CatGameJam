using Godot;
using System;
using System.Diagnostics;

public partial class CollisionEffectHandler : Node2D
{
	private Entity _entity;
	
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		_entity = Owner as Entity;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	public void HandleCollision(Hitbox hitbox)
	{
		Health charHealth = _entity.HealthReference;
		
		if(hitbox.Damage > 0) { charHealth.TakeDamage(hitbox.Damage); }
		else if(hitbox.Damage < 0) { charHealth.GainLife(hitbox.Damage); }
		
		//GD.Print(_entity.Name + " takes damage from " + hitbox.GetOwner().Name);
		if (hitbox.KnockbackIntensity != 0)
		{
			Vector2 knockbackVector = CalculateKnockback(hitbox);
			// Debug.Print(
			// 	"Applying knockback of amount" + knockbackVector.X + " , " + knockbackVector.Y +
			// 	" to " + _entity.Name);
		}

		if (hitbox.StatusEffect != GameplayConstants.StatusEffect.None)
		{
			//Debug.Print("Applying status effect " + hitbox.StatusEffect + " to " + _entity.Name);
		} 
	}

	public Vector2 CalculateKnockback(Hitbox hitbox)
	{
		return (_entity.Position - hitbox.GetOwner().Position) * hitbox.KnockbackIntensity;
	}
}
