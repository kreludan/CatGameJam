using Godot;

public partial class FrozenSe : StatusEffect
{
    public override void ApplyEffect(Node2D target)
    {
        base.ApplyEffect(target);
        // Implement the logic for applying the freezing effect to the target
    }

    public override void Update(float delta)
    {
        base.Update(delta);
        // Implement the logic for updating the freezing effect (e.g., unfreezing)
    }

    public override void RemoveEffect()
    {
        base.RemoveEffect();
        // Implement the logic for removing the freezing effect from the target
    }
}