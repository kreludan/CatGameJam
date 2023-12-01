using Godot;

public partial class BurningSe : StatusEffect
{
    //private float _damagePerSecond;
    
    public override void ApplyEffect(Node2D target)
    {
        base.ApplyEffect(target);
        // Implement the logic for applying the burning effect to the target
    }

    public override void Update(float delta)
    {
        base.Update(delta);
        // Implement the logic for updating the burning effect (e.g., dealing damage over time)
    }

    public override void RemoveEffect()
    {
        base.RemoveEffect();
        // Implement the logic for removing the burning effect from the target
    }
}