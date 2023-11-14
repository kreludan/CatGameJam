using Godot;

public partial class BaseBullet : Area2D
{
    [Export]
    protected int Damage = 1;
    [Export]
    protected float Speed = 300;
    protected Vector2 Direction = Vector2.Zero;

    public override void _Process(double delta)
    {
        Translate(Direction * Speed * (float)delta);
    }

    public virtual void SetDirection(Vector2 direction)
    {
        Direction = direction.Normalized();
    }

    //collision
    public void _on_area_entered(Area2D area)
    {
        if (area.IsInGroup("Player") || area.IsInGroup("PlayerGun"))
        {
            return;
        }
        GD.Print("Collide");
        DeactivateBullet();
    }

    public void DeactivateBullet()
    {
        Visible = false;
        SetProcess(false);
    }
    
    public void ActivateBullet()
    {
        Visible = true;
        SetProcess(true);
    }
}