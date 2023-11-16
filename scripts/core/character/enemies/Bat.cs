using Godot;

public partial class Bat : EnemyFSM
{
	private const float PatrolSpeed = 200f;
	private const float ChaseRange = 350f;
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
		if (Velocity != Vector2.Zero)
		{
			Velocity = Vector2.Zero;
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
		//Position += _currentDirection * PatrolSpeed;
		Velocity = _currentDirection * PatrolSpeed;
		// Check if the player is in range
		if (IsPlayerInRange(ChaseRange))
		{
			TransitionToState(EnemyState.Chase);
			return;
		}
		// // Check if the enemy is within the patrol area
		// if (!IsWithinPatrolArea(Position))
		// {
		// 	// Calculate the direction vector towards the player
		// 	Vector2 playerDirection = (Player.Position - Position).Normalized();
		// 	// Set the velocity to move towards the player
		// 	Velocity = playerDirection * PatrolSpeed;
		// 	GD.Print("Velocity: " + Velocity);
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
		if (Player == null) return;
		
		if (!IsPlayerInRange(ChaseRange))
		{
			//GD.Print("Within range");
			TransitionToState(EnemyState.Patrol);
			return;
		}
		if (_chaseFrameCounter % _chaseChangeFrequency == 0)
		{
			_currentChaseDirection = (Player.Position - Position).Normalized().Rotated(GetRandomAngle());
			//GD.Print("New direction: " + _currentChaseDirection);
		}
		//Position += _currentChaseDirection * ChaseSpeed;
		Velocity = _currentChaseDirection * ChaseSpeed;
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
		if (Player == null) return false;
		
		float distanceToPlayer = Position.DistanceTo(Player.Position);
		return distanceToPlayer <= range;
	}
}