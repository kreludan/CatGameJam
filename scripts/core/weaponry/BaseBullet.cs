using Godot;

public partial class BaseBullet : Area2D
{
    [Export]
    protected int Damage = 1;
    [Export]
    protected float Speed = 300;
    protected Vector2 Direction = Vector2.Zero;
    public GunController Gun { get; set; }

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
        if (area.IsInGroup("Player") || area.IsInGroup("PlayerGun") || area.IsInGroup("PlayerBullet"))
        {
            return;
        }
        DeactivateBullet();
    }

    public void DeactivateBullet()
    {
        Hide();
        SetProcess(false);
    }
    
    public void ActivateBullet()
    {
        Show();
        SetProcess(true);
    }

    public int GetBulletDamage()
    {
        return Damage;
    }

    public void OnExitScreen()
    {
        DeactivateBullet();
    }
}