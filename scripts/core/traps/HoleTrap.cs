using Godot;

public partial class HoleTrap : Trap
{
    public HoleTrap(Entity entity, GameplayConstants.TrapType type) : base(entity, type) { }

    public override void Activate()
    {
        GD.Print("This is a hole");
        Damage = 2;
        ApplyDamage();
    }

    public override void Update()
    {
        GD.Print("UPDATING HOLE");
    }
}