using Godot;

public partial class InvulnerableSe : StatusEffect
{
    private Entity _entity;
    
    public override void ApplyEffect(Node2D target)
    {
        base.ApplyEffect(target);
        SeName = "INVULNERABLE";
        _entity = target as Entity;
        if (_entity == null)
        {
            GD.Print("ENTITY IS NULL BRUH");
        }
        GD.Print("Setting " + target.Name + " invulnerable for " + Timer + " seconds");
        _entity?.SetCollisionLayerAndMask(GameplayConstants.CollisionLayer.Invulnerable);
        Timer = 0;
    }

    public override void RemoveEffect()
    {
        base.RemoveEffect();
        _entity?.SetCollisionLayerAndMask(_entity.BaseCollisionLayer);
    }
}