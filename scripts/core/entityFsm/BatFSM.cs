using Godot;
using Vector2 = Godot.Vector2;

public partial class BatFSM : EnemyFSM
{
	private const float PatrolSpeed = 100f;
	private const float ChaseRange = 250f;
	private const float ChaseSpeed = 200f;
	private int _chaseChangeFrequency = 100;
	private Vector2 _currentChaseDirection;
	private int _chaseFrameCounter;
	private int _patrolFrameCounter;
	private int _patrolChangeFrequencyMin = 100;
	private int _patrolChangeFrequencyMax = 800; 
	private Vector2 _patrolDirection;
	private int _pauseCounter;
	private int _pauseDuration = 60; 
	private int _idleFrameCounter;
	private int _idlePatrolTransitionFrequency = 100;
	private Vector2 _currentDirection = Vector2.Zero;

	protected override void UpdateIdleState()
	{
		if (EntityRef.Velocity != Vector2.Zero)
		{
			EntityRef.Velocity = Vector2.Zero;
		}
		if (IsPlayerInRange(ChaseRange))
		{
			TransitionToState(EnemyState.Chase);
			return;
		}
		_idleFrameCounter++;
		if (_idleFrameCounter >= _idlePatrolTransitionFrequency && GD.Randf() < 0.008)
		{
			TransitionToState(EnemyState.Patrol);
		}
	}

	protected override void UpdatePatrolState()
	{
		// Check if it's time to change direction
		if (_patrolFrameCounter <= 0)
		{
			// Determine whether to change direction or transition to idle state
			if (GD.Randf() < 1f / 6f)
			{
				TransitionToState(EnemyState.Idle);
			}
			else
			{
				_currentDirection = new Vector2(GD.RandRange(-1, 1), GD.RandRange(-1, 1)).Normalized();
			}
			// Reset the frame counter
			_patrolFrameCounter = GD.RandRange(_patrolChangeFrequencyMin, _patrolChangeFrequencyMax);
		}
		else
		{
			_patrolFrameCounter--;
		}
		if (_currentDirection == Vector2.Zero)
		{
			_currentDirection = new Vector2(GD.RandRange(-1, 1), GD.RandRange(-1, 1)).Normalized();
		}
		EntityRef.Velocity = _currentDirection * PatrolSpeed;
		
		// Check if the player is in range
		if (IsPlayerInRange(ChaseRange))
		{
			TransitionToState(EnemyState.Chase);
		}
		// // Check if the enemy is within the patrol area
		// if (!IsWithinPatrolArea(Position))
		// {
		// 	// Calculate the direction vector towards the player
		// 	Vector2 playerDirection = (PlayerRef.Position - Position).Normalized();
		// 	// Set the velocity to move towards the player
		// 	EntityRef.Velocity = playerDirection * PatrolSpeed;
		// 	GD.Print("Velocity: " + EntityRef.Velocity);
		// }
	}

	// Check if the position is within the specified patrol area
	private bool IsWithinPatrolArea(Vector2 position)
	{
		// Adjust these values based on your patrol area and screen size
		float minX = 200f;
		float maxX = 1000f;
		float minY = 100f;
		float maxY = 400f;
		// Check if the new position is within the patrol area
		return position.X > minX && position.X < maxX && position.Y > minY && position.Y < maxY;
	}

	protected override void UpdateChaseState()
	{
		if (PlayerRef == null) return;
		
		if (!IsPlayerInRange(ChaseRange))
		{
			//GD.Print("Within range");
			TransitionToState(EnemyState.Patrol);
			return;
		}
		if (_chaseFrameCounter % _chaseChangeFrequency == 0)
		{
			_currentChaseDirection = (PlayerRef.Position - EntityRef.Position).Normalized().Rotated(GetRandomAngle());
		}
		EntityRef.Velocity = _currentChaseDirection * ChaseSpeed;
		_chaseFrameCounter = (_chaseFrameCounter + 1) % _chaseChangeFrequency;
	}

	private float GetRandomAngle()
	{
		return (float)GD.RandRange(-Mathf.Pi / 4, Mathf.Pi / 4);
	}
	
	protected override void TransitionToState(EnemyState state)
	{
		_idleFrameCounter = 0;
		_patrolFrameCounter = 0;
		base.TransitionToState(state);
	}
	
	protected override void UpdateDeadState()
	{
		// Implement Bat dead behavior here
	}

	private bool IsPlayerInRange(float range)
	{
		if (PlayerRef == null) return false;
		if (!IsInstanceIdValid(PlayerRef.GetInstanceId())) return false;
		
		float distanceToPlayer = EntityRef.Position.DistanceTo(PlayerRef.Position);
		return distanceToPlayer <= range;
	}
}