using System.Reflection.Metadata;
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
    private Health _health;
    protected CharacterController Player;
    private static readonly StringName PlayerString = new("Player");
    private double _collisionInterval;
    private double _collisionTimer = 50;
    protected int Damage = 1;
    private bool _didDamage;

    public override void _Ready()
    {
        // Determine whether to transition to Idle or Patrol state with a 50/50 chance
        TransitionToState(GD.Randf() < 0.5f ? EnemyState.Idle : EnemyState.Patrol);
        Player = (CharacterController)GetTree().GetNodesInGroup(PlayerString)[0];
        _health = GetNode<Health>("Health");
        // Connect the Death signal to the Die method
        _health.Death += Die;
    }
    
    public override void _Process(double delta)
    {
        // Update the current state
        UpdateState();
        if (!_didDamage) return;

        _collisionInterval -= delta * Engine.GetFramesPerSecond();
    }

    public override void _PhysicsProcess(double delta)
    {
        HandleCollision(delta);
    }
    
    private void HandleCollision(double delta)
    {
        KinematicCollision2D collision = MoveAndCollide(Velocity * (float)delta);

        if (collision?.GetCollider() is not CharacterBody2D collidedObject) return;
        
        // Check if the "Health" child node exists
        Node healthNode = collidedObject.GetNodeOrNull("Health");
        if (healthNode is Health health)
        {
            if (health.CurrentOwner == _health.CurrentOwner) return;
            if (_collisionInterval > 0) return;
            
            _didDamage = true;
            _collisionInterval = _collisionTimer;
            GD.Print("Damage: " + Damage);
            // Apply damage to the health node
            health.TakeDamage(Damage);
        }
    }

    public override void _ExitTree()
    {
        _health.Death -= Die;
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
        GD.Print(_currentState);
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

    protected virtual void Die()
    {
        TransitionToState(EnemyState.Dead);
        QueueFree();
    }

    // public void _on_collision_detection_area_entered(Area2D area)
    // {
    //     if (area.IsInGroup("PlayerBullet"))
    //     {
    //         BaseBullet bulletNode = (BaseBullet)area;
    //         _health.TakeDamage(bulletNode.GetBulletDamage());
    //         GD.Print("Do Damage: " + bulletNode.GetBulletDamage());
    //     }
    //     
    // }
}