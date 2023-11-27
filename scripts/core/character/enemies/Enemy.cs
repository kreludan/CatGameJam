using Godot;

public partial class Enemy : Entity
{
    private Vector2 _previousVelocity = Vector2.Zero;
    [Export]
    private bool hasMovement = true;

    public override void _Ready()
    {
        base._Ready();
        CharacterType = GameplayConstants.CharacterType.Enemy;
        BaseCollisionLayer = GameplayConstants.CollisionLayer.Enemies;
        SetCollisionLayerAndMask(BaseCollisionLayer);
        if (GunReference.IsValid())
        {
            GD.Print("Initializing gun!");
            GunReference.Initialize();
        }
    }
    
    public override void _Process(double delta)
    {
        if (!hasMovement) return;
        if (Velocity == _previousVelocity) return;

        _previousVelocity = Velocity;
        if (Mathf.Abs(Velocity.X) > Mathf.Abs(Velocity.Y))
        {
            // Horizontal movement
            PlayAnimation(Velocity.X > 0 ? "fly_right" : "fly_left");
        }
        else
        {
            // Vertical movement
            PlayAnimation(Velocity.Y > 0 ? "fly_down" : "fly_up");
        }
    }
}