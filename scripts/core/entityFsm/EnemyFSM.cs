using System.Reflection.Metadata;
using Godot;

public partial class EnemyFSM : Node2D
{
    public enum EnemyState
    {
        Idle,
        Patrol,
        Chase,
        Attack,
        Dead
    }
    
    private EnemyState _currentState = EnemyState.Idle;
    protected Entity EntityRef;
    protected Entity PlayerRef;
    private static readonly StringName PlayerString = new("Player");

    public override void _Ready()
    {
        base._Ready();
        EntityRef = Owner as Entity;
        // Determine whether to transition to Idle or Patrol state with a 50/50 chance
        TransitionToState(GD.Randf() < 0.5f ? EnemyState.Idle : EnemyState.Patrol);
        PlayerRef = (Entity)GetTree().GetNodesInGroup(PlayerString)[0];
    }
    
    public override void _Process(double delta)
    {
        // Update the current state
        UpdateState();
    }

    private void UpdateState()
    {
        switch (_currentState)
        {
            case EnemyState.Idle:
                UpdateIdleState();
                break;
            case EnemyState.Patrol:
                UpdatePatrolState();
                break;
            case EnemyState.Chase:
                UpdateChaseState();
                break;
            case EnemyState.Attack:
                UpdateAttackState();
                break;
            case EnemyState.Dead:
                UpdateDeadState();
                break;
            default:
                GD.Print("Unknown state!");
                break;
        }
    }

    protected virtual void UpdateIdleState()
    {
        // Implement idle behavior here
    }

    protected virtual void UpdatePatrolState()
    {
        // Implement patrol behavior here
    }

    protected virtual void UpdateChaseState()
    {
        // Implement chase behavior here
    }

    protected virtual void UpdateAttackState()
    {
        // Implement attack behavior here
    }

    protected virtual void UpdateDeadState()
    {
        // Implement dead behavior here
    }

    // Function to transition to a new state
    protected virtual void TransitionToState(EnemyState newState)
    {
        _currentState = newState;
    }

    protected virtual void StartPatrol()
    {
        TransitionToState(EnemyState.Patrol);
    }

    protected virtual void StartChase()
    {
        TransitionToState(EnemyState.Chase);
    }

    protected virtual void StartAttack()
    {
        TransitionToState(EnemyState.Attack);
    }
}