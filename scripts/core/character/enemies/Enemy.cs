using Godot;

public partial class Enemy : Entity
{
    private Vector2 _previousVelocity = Vector2.Zero;

    public override void _Ready()
    {
        base._Ready();
        CharacterType = GameplayConstants.CharacterType.Enemy;
        BaseCollisionLayer = GameplayConstants.CollisionLayer.Enemies;
        SetCollisionLayerAndMask(BaseCollisionLayer);
    }
    
    public override void _Process(double delta)
    {
        if ((int)Velocity.Y == (int)_previousVelocity.Y) return;
        
        switch (Velocity.Y)
        {
            case < 0:
                PlayAnimation("fly_up");
                break;
            case > 0:
                PlayAnimation("fly_down");
                break;
        }
        _previousVelocity = Velocity;
    }
}