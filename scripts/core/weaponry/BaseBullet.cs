using Godot;

public partial class BaseBullet : Area2D
{
    [Export]
    protected int Damage = 1;
    [Export]
    protected float Speed = 300;
    protected Vector2 Direction = Vector2.Zero;
    
    public override void _Ready()
    {
        // Connect the body_entered signal to the OnBulletAreaEntered method
        //Connect("body_entered", this, nameof(OnBulletAreaEntered));
    }

    public override void _Process(double delta)
    {
        Translate(Direction * Speed * (float)delta);
        // Check for collisions or other logic here
    }

    public virtual void SetDirection(Vector2 direction)
    {
        Direction = direction.Normalized();
    }

    // Handle any collision logic here
    private void OnBulletAreaEntered(object area)
    {
        // You can customize the collision logic in the derived classes
        QueueFree();
    }

    public void _on_area_entered(Area2D area)
    {
        GD.Print("Collide");
    }
}