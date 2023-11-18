using Godot;

public partial class Trap
{
    protected readonly GameplayConstants.TrapType TrapType;
    protected Health Health;
    protected int Damage;

    public Trap(Entity entity, GameplayConstants.TrapType type)
    {
        Health = entity.GetNode<Health>("Health");
        TrapType = type;
    }
    
    public virtual void Activate() { }
    
    protected void ApplyDamage()
    {
        Health?.TakeDamage(Damage);
        GD.Print(TrapType + " damage dealt: " + Damage);
    }
}