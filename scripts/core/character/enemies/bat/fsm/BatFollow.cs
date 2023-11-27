using Godot;

public partial class BatFollow : EnemyState
{
    private float _moveSpeed = 80f;
    private float _enemyFollowRange = 250f;
    private float _maxDeviationAngle = 45f;
    private float _deviationUpdateInterval = 0.5f;
    private float _timeSinceLastUpdate;

    public override void Enter(StateMachine stateMachine)
    {
        base.Enter(stateMachine);
        Nav.DebugPathCustomColor = Colors.Red;
    }

    public override void Update(float delta)
    {
        base.Update(delta);
        if (PlayerDirection.Length() > _enemyFollowRange)
        {
            EmitSignal(nameof(Transitioned), this, "idle");
        }
    }

    public override void PhysicsUpdate(float delta)
    {
        base.PhysicsUpdate(delta);
        //GD.Print(Nav.GetNextPathPosition());
        Vector2 velocity = ToLocal(Nav.GetNextPathPosition()).Normalized() * _moveSpeed;
        SetVelocityTarget(velocity);
    }
    
    public void _on_nav_timer_timeout()
    {
        if (!StateMachine.IsValid()) return;
        if (StateMachine.CurrentState != this) return;
        
        FirstPathMade = true;
        MakePathToPlayer();
    }
}