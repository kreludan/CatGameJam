using Godot;

public partial class BaseBullet : Entity
{
    [Export]
    private float _speed;
    private Gun _ownerGun;
    private const GameplayConstants.CollisionLayer DeactivatedBulletLayer =
        GameplayConstants.CollisionLayer.DeactivatedBullets;

    
    public virtual void InitializeFields(Gun ownerGun)
    {
        _ownerGun = ownerGun;
        CharacterType = _ownerGun.GunOwner.CharacterType;
        BaseCollisionLayer = GetBulletLayer();
        SetCollisionLayerAndMask(BaseCollisionLayer);
    }
    
    public virtual void SetDirection(Vector2 direction)
    {
        Velocity = direction.Normalized() * _speed;
        //Debug.Print("Bullet direction set to " + this.Velocity.X + ", " + this.Velocity.Y);
    }

    public override void _PhysicsProcess(double delta)
    {
        KinematicCollision2D collision = HandleCollision();
        if (collision.IsValid() && Visible)
        {
            DeactivateBullet();
        }
    }

    public void DeactivateBullet()
    {
        SetCollisionLayerAndMask(DeactivatedBulletLayer, GetBulletLayer());
        SetProcess(false);
        SetPhysicsProcess(false);
        Hide();
        _ownerGun.RequeueBullet(this);
    }
    
    public void ActivateBullet()
    {
        SetCollisionLayerAndMask(GetBulletLayer(), DeactivatedBulletLayer);
        SetProcess(true);
        SetPhysicsProcess(true);
        Show();
    }

    private GameplayConstants.CollisionLayer GetBulletLayer()
    {
        return CharacterType == GameplayConstants.CharacterType.Player
            ? GameplayConstants.CollisionLayer.PlayerBullets
            : GameplayConstants.CollisionLayer.EnemyBullets; 
    }
}