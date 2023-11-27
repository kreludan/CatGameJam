using Godot;

public partial class EnemyState : State
{
    protected CharacterBody2D Player;
    protected CharacterBody2D Enemy;
    protected Vector2 PlayerDirection;
    protected RandomNumberGenerator Rng = new();
    [Export]
    protected NavigationAgent2D Nav;
    protected bool FirstPathMade;

    public override void Enter(StateMachine stateMachine)
    {
        base.Enter(stateMachine);
        if (!Player.IsValid())
        {
            Player =(CharacterBody2D)GetTree().GetFirstNodeInGroup("Player");
        }
        if (!Enemy.IsValid())
        {
            Enemy = Owner as CharacterBody2D;
        }
    }

    public override void Update(float delta)
    {
        base.Update(delta);
        if (!Player.IsValid() || !Enemy.IsValid()) return;
        
        PlayerDirection = Player.GlobalPosition - Enemy.GlobalPosition;
    }

    protected void SetVelocityTarget(Vector2 velocity)
    {
        if (!Enemy.IsValid())
        {
            Enemy = Owner as CharacterBody2D;
        }
        Enemy.Velocity = velocity;
    }

    public void MakePathToPlayer()
    {
        if (!Player.IsValid())
        {
            Player =(CharacterBody2D)GetTree().GetFirstNodeInGroup("Player");
        }
        Nav.TargetPosition = Player.GlobalPosition;
    }

    public void MakePathToLocation(Vector2 pathPosition)
    {
        Nav.TargetPosition = pathPosition;
    }
}