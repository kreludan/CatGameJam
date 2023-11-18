using Godot;

public partial class SpikeTrap : Trap
{
    public SpikeTrap(Entity entity, GameplayConstants.TrapType type) : base(entity, type) { }

    public override void Activate()
    {
        GD.Print("These are spikes");
        Damage = 1;
        ApplyDamage();
    }

    public override void Update()
    {
        GD.Print("UPDATING SPIKE");
    }
}