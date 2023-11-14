using Godot;

public partial class EnemyFSM : CharacterBody2D
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
    
    public override void _Process(double delta)
    {
        // Update the current state
        UpdateState();
    }

    // Function to update the current state
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

    // Example methods for handling transitions (override these in derived classes)

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

    protected virtual void Die()
    {
        TransitionToState(EnemyState.Dead);
    }
}