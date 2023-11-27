using Godot;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection.Metadata;

public partial class CollisionEffectHandler : Node2D
{
    private Entity _entity;
	
    public override void _Ready()
    {
        _entity = Owner as Entity;
    }

    public override void _Process(double delta) { }

    public void HandleCollision(Entity collidedEntity)
    {
        if (!CheckIfValidCollision(collidedEntity.BaseCollisionLayer)) return;
        Debug.Print(_entity.BaseCollisionLayer + " collided with " + collidedEntity.BaseCollisionLayer);
        Hitbox myHitbox = _entity.HitboxReference;
        Health myHealth = _entity.HealthReference;
        Hitbox collidedObjectHitbox = collidedEntity.HitboxReference;
        Health collidedObjectHealth = collidedEntity.HealthReference;
		
        //GD.Print( _entity.Name + " collided with: " + collidedObjectHitbox.Owner.Name);
        myHealth.HandleDamage(collidedObjectHitbox.Damage);
        collidedObjectHealth.HandleDamage(myHitbox.Damage);
        if (collidedObjectHitbox.KnockbackIntensity != 0)
        {
            //Vector2 knockbackVector = CalculateKnockback(hitBox);
            // Debug.Print(
            // 	"Applying knockback of amount" + knockbackVector.X + " , " + knockbackVector.Y +
            // 	" to " + _entity.Name);
        }
        if (collidedObjectHitbox.StatusEffect != GameplayConstants.StatusEffect.None)
        {
            //Debug.Print("Applying status effect " + hitbox.StatusEffect + " to " + _entity.Name);
        } 
    }

    public bool CheckIfValidCollision(GameplayConstants.CollisionLayer layerCollidedWith)
    {
        GameplayConstants.CollisionLayer myLayer = _entity.BaseCollisionLayer;
        List<GameplayConstants.CollisionLayer> validCollisionLayers = GameplayConstants.GetCollisionMasksPerLayer(myLayer);
        foreach (GameplayConstants.CollisionLayer layer in validCollisionLayers)
        {
            if (layer == layerCollidedWith)
            {
                return true;
            }
        }

        return false;
    }
	

    public Vector2 CalculateKnockBack(Hitbox hitBox)
    {
        return (_entity.Position - hitBox.GetOwner().Position) * hitBox.KnockbackIntensity;
    }
}