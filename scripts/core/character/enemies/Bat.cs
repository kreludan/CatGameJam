using Godot;

public partial class Bat : EnemyFSM
{
	private const float ChaseRange = 250f;
	private const float ChaseSpeed = 1.0f;
	private const float RoughPathFactor = 0.8f; 

	protected override void UpdateIdleState()
	{
		// Example: Start patrolling if the player is not in range
		if (IsPlayerInRange(ChaseRange))
		{
			TransitionToState(EnemyState.Chase);
		}
	}

	protected override void UpdatePatrolState()
	{
		// Example: Transition to idle if the player is out of range
		if (!IsPlayerInRange(ChaseRange))
		{
			TransitionToState(EnemyState.Idle);
		}
		// Example: Start chasing if the player is within chase range
		else if (IsPlayerInRange(ChaseRange))
		{
			TransitionToState(EnemyState.Chase);
		}
	}

	protected override void UpdateChaseState()
	{
		if (!IsPlayerInRange(ChaseRange))
		{
			TransitionToState(EnemyState.Patrol);
			return;
		}

		// Move towards the player's position with a rougher path
		Vector2 directionToPlayer = (Player.Position - Position).Normalized();
		Vector2 roughPath = directionToPlayer.Rotated(Mathf.DegToRad(GD.RandRange(-45, 45))) * ChaseSpeed;
		Position += roughPath * RoughPathFactor;
		float rotation = Mathf.Atan2(directionToPlayer.Y, directionToPlayer.Y);
		Rotation = rotation;
	}

	protected override void UpdateDeadState()
	{
		// Implement Bat dead behavior here
	}

	private bool IsPlayerInRange(float range)
	{
		if (Player == null) return false;
		
		float distanceToPlayer = Position.DistanceTo(Player.Position);
		return distanceToPlayer <= range;
	}
}
