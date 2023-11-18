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
        _entity?.SetCollisionLayerAndMask(GameplayConstants.CollisionLayer.Invulnerable, _entity.BaseCollisionLayer);
        Timer = 0;
    }

    public override void Update(float delta)
    {
        base.Update(delta);
    }

    public override void RemoveEffect()
    {
        base.RemoveEffect();
        _entity?.SetCollisionLayerAndMask(_entity.BaseCollisionLayer, GameplayConstants.CollisionLayer.Invulnerable);
    }
}