using Godot;
using static GameplayConstants;

public partial class BaseBullet : Entity
{
    [Export]
    private float _speed;
    private Gun _ownerGun;
    private const CollisionLayer DeactivatedBulletLayer = GameplayConstants.CollisionLayer.DeactivatedBullets;
    
    public virtual void InitializeFields(Gun ownerGun)
    {
        _ownerGun = ownerGun;
        Name = ownerGun?.GunOwner.Name + "Bullet";
        InitEntityType(_ownerGun.GunOwner.CharacterType, GetBulletLayer());
    }

    public void UpdateBulletPhysics()
    {
        KinematicCollision2D collision = HandleCollision();
        if (collision.IsValid() && Visible)
        {
            GD.Print("Deactivate");
            DeactivateBullet();
        }
    }

    public virtual void SetDirection(Vector2 direction)
    {
        Velocity = direction.Normalized() * _speed;
        //Debug.Print("Bullet direction set to " + this.Velocity.X + ", " + this.Velocity.Y);
    }

    public void DeactivateBullet()
    {
        SetCollisionLayerAndMask(DeactivatedBulletLayer);
        SetProcess(false);
        SetPhysicsProcess(false);
        Hide();
        _ownerGun.RequeueBullet(this);
    }
    
    public void ActivateBullet()
    {
        SetCollisionLayerAndMask(GetBulletLayer());
        SetProcess(true);
        SetPhysicsProcess(true);
        Show();
    }

    private CollisionLayer GetBulletLayer()
    {
        return CharacterType == CharacterType.Player
            ? GameplayConstants.CollisionLayer.PlayerBullets
            : GameplayConstants.CollisionLayer.EnemyBullets; 
    }
}