using Godot;

public partial class BatEnemy : EnemyFSM
{
    // Adjust these values based on your game's requirements
    private const float ChaseRange = 150f;
    private const float AttackRange = 50f;

    private CharacterController _player; // Assuming you have a Player class
    
    public override void _Ready()
    {
        _player = GetNode<CharacterController>("path_to_player_node");
    }
    
    protected override void UpdateIdleState()
    {
        // Implement Bat idle behavior here

        // Example: Start patrolling if the player is not in range
        if (IsPlayerInRange(ChaseRange))
        {
            StartChase();
        }
    }

    protected override void UpdatePatrolState()
    {
        // Implement Bat patrol behavior here

        // Example: Transition to idle if the player is out of range
        if (!IsPlayerInRange(ChaseRange))
        {
            TransitionToState(EnemyState.Idle);
        }
        // Example: Start chasing if the player is within chase range
        else if (IsPlayerInRange(ChaseRange) && !IsPlayerInRange(AttackRange))
        {
            StartChase();
        }
        // Example: Start attacking if the player is within attack range
        else if (IsPlayerInRange(AttackRange))
        {
            StartAttack();
        }
    }

    protected override void UpdateChaseState()
    {
        // Implement Bat chase behavior here

        // Example: Start attacking if the player is within attack range during chase
        if (IsPlayerInRange(AttackRange))
        {
            StartAttack();
        }
        // Example: Transition to patrol if the player is out of chase range
        else if (!IsPlayerInRange(ChaseRange))
        {
            StartPatrol();
        }
    }

    protected override void UpdateAttackState()
    {
        // Implement Bat attack behavior here

        // Example: Transition to patrol after attacking
        StartPatrol();
    }

    protected override void UpdateDeadState()
    {
        // Implement Bat dead behavior here
    }

    // Function to check if the player is within a certain range
    private bool IsPlayerInRange(float range)
    {
        if (_player != null)
        {
            float distanceToPlayer = Position.DistanceTo(_player.Position);
            return distanceToPlayer <= range;
        }
        return false;
    }

}
