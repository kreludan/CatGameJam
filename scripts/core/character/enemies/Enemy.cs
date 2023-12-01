using Godot;

public partial class Enemy : Entity
{
    private Vector2 _previousVelocity = Vector2.Zero;
    [Export]
    private bool _hasMovement = true;
    
    protected override void Initialize()
    {
        base.Initialize();
        GunReference = GetNodeOrNull<Gun>("Gun");
        InitEntityType(GameplayConstants.CharacterType.Enemy, GameplayConstants.CollisionLayer.Enemies);
        if (GunReference.IsValid())
        {
            GunReference.Initialize(this);
        }
    }
}