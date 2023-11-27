// An example of a passable enemy would be a flying enemy that could fly over certain terrain

using Godot;
using System;

public partial class EnemyPassable : Entity
{
    private Vector2 _previousVelocity = Vector2.Zero;
    [Export]
    private bool _hasMovement = true;
    
    public override void _Ready()
    {
        base._Ready();
        CharacterType = GameplayConstants.CharacterType.Enemy;
        BaseCollisionLayer = GameplayConstants.CollisionLayer.EnemiesPassable;
        SetCollisionLayerAndMask(BaseCollisionLayer);
        if (GunReference.IsValid())
        {
            GD.Print("Initializing gun!");
            GunReference.Initialize();
        }
    }
    
    public override void _Process(double delta)
    {
        if (!_hasMovement) return;
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