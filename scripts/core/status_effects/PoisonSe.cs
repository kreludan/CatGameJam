using Godot;

public partial class PoisonSe : StatusEffect
{
    private float _damagePerSecond;
    
    public override void ApplyEffect(Node2D target)
    {
        base.ApplyEffect(target);
        // Implement the logic for applying the poison effect to the target
    }

    public override void Update(float delta)
    {
        base.Update(delta);
        // Implement the logic for updating the poison effect (e.g., dealing damage over time)
    }

    public override void RemoveEffect()
    {
        base.RemoveEffect();
        // Implement the logic for removing the poison effect from the target
    }
}