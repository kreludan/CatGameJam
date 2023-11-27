using Godot;

public partial class PrismGun : Gun
{
    private readonly Vector2 _upRight = new(1, -1);
    private readonly Vector2 _upLeft = new(-1, -1);

    public override void _Ready()
    {
        base._Ready();
        Shoot();
    }

    public void Shoot()
    {
        BulletDirection = Vector2.Down;
        HandleAiFiring();
        BulletDirection = _upRight;
        HandleAiFiring();
        BulletDirection = _upLeft;
        HandleAiFiring();
    }
}