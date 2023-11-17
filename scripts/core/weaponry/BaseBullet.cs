using Godot;

public partial class BaseBullet : Entity
{
    [Export]
    private float _speed;
    private Gun _ownerGun;
    

    public virtual void SetDirection(Vector2 direction)
    {
        Velocity = direction.Normalized() * _speed;
        //Debug.Print("Bullet direction set to " + this.Velocity.X + ", " + this.Velocity.Y);
    }

    public virtual void SetOwner(Gun ownerGun)
    {
        _ownerGun = ownerGun;
    }

    public override void _PhysicsProcess(double delta)
    {
        KinematicCollision2D collision = HandleCollision(delta);
        if (collision != null && Visible)
        {
            DeactivateBullet();
        }
    }

    public void DeactivateBullet()
    {
        Hide();
        SetProcess(false);
        _ownerGun.RequeueBullet(this);
    }
    
    public void ActivateBullet()
    {
        Show();
        SetProcess(true);
    }

    public void OnExitScreen()
    {
        if(Visible) DeactivateBullet();
    }
}