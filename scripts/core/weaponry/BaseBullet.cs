using Godot;

public enum Owner { Player, Enemy }

public partial class BaseBullet : CharacterBody2D
{
    [Export]
    protected int Damage = 1;
    [Export]
    protected Owner CurrentOwner;
    [Export]
    protected bool CanCollideWithSelf;
    [Export]
    protected float Speed = 600;
    protected Vector2 Direction = Vector2.Zero;
    public GunController Gun { get; set; }
    private int _collisionCount;
    private int _maxCollisions = 1;

    public override void _PhysicsProcess(double delta)
    {
        HandleCollision(delta);
    }

    private void HandleCollision(double delta)
    {
        KinematicCollision2D collision = MoveAndCollide(Velocity * (float)delta);

        if (collision?.GetCollider() is not Node2D collidedObject) return;
        if (_collisionCount >= _maxCollisions) return;
        
        _collisionCount++;
        // Check if the "Health" child node exists
        Node healthNode = collidedObject.GetNodeOrNull("Health");
        if (healthNode is Health health)
        {
            if (health.CurrentOwner == CurrentOwner && !CanCollideWithSelf) return;

            // Apply damage to the health node
            health.TakeDamage(Damage);
        }
        DeactivateBullet();
    }

    public virtual void SetDirection(Vector2 direction)
    {
        _collisionCount = 0;
        Direction = direction.Normalized();
        Velocity = Direction * Speed;
    }

    public virtual void SetOwner(Owner owner)
    {
        CurrentOwner = owner;
    }

    public void DeactivateBullet()
    {
        //GD.Print("Deactivate");
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