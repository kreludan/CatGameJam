using Godot;

public partial class Enemy : Entity
{
    private Vector2 _previousVelocity = Vector2.Zero;

    public override void _Process(double delta)
    {
        if ((int)Velocity.Y == (int)_previousVelocity.Y) return;
        
        switch (Velocity.Y)
        {
            case < 0:
                GD.Print("flying up");
                PlayAnimation("fly_up");
                break;
            case > 0:
                GD.Print("flying down");
                PlayAnimation("fly_down");
                break;
            default:
                break;
        }
        _previousVelocity = Velocity;
    }
}