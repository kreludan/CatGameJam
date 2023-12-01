// An example of a passable enemy would be a flying enemy that could fly over certain terrain

using Godot;

public partial class EnemyPassable : Entity
{
    private AnimationController _animationControllerRef;
    private Vector2 _previousVelocity = Vector2.Zero;
    [Export]
    private bool _hasMovement = true;
    
    protected override void Initialize()
    {
        base.Initialize();
        _animationControllerRef = GetNode<AnimationController>("Sprite/AnimationPlayer");
        InitEntityType(GameplayConstants.CharacterType.Enemy, GameplayConstants.CollisionLayer.EnemiesPassable);
    }

    protected override void Update(float delta)
    {
        base.Update(delta);
        if (!_hasMovement) return;
        if (Velocity == _previousVelocity) return;
        
        _previousVelocity = Velocity;
        if (Mathf.Abs(Velocity.X) > Mathf.Abs(Velocity.Y))
        {
            _animationControllerRef.PlayAnimation(Velocity.X > 0 ? "fly_right" : "fly_left");
        }
        else
        {
            _animationControllerRef.PlayAnimation(Velocity.Y > 0 ? "fly_down" : "fly_up");
        }
    }
}