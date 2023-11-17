using System.Numerics;
using Godot;
using Vector2 = Godot.Vector2;

public partial class Bat : EnemyFSM
{
	private const float PatrolSpeed = 100f;
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

	[Export]
	private AnimationPlayer _batPlayer;

	public override void _Ready()
	{
		//base._Ready();
		//_batPlayer = Owner.GetNode<Sprite2D>("Sprite").GetNode<AnimationPlayer>("AnimationPlayer");
		if (_batPlayer == null)
		{
			GD.Print("Player NULL");
		}
	}

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
			PlayMovementAnimation();
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

	private void PlayMovementAnimation()
	{
		if (_batPlayer == null) return;
		
		if (_currentDirection.X < 0 && _currentDirection.Y == 0)
		{
			_batPlayer.Play("fly_left");
		}
		else if (_currentDirection.X > 0 && _currentDirection.Y == 0)
		{
			_batPlayer.Play("fly_right");
		}
		if (_currentDirection.Y < 0)
		{
			_batPlayer.Play("fly_up");
		}
		if (_currentDirection.Y > 0)
		{
			_batPlayer.Play("fly_down");
		}
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
			PlayMovementAnimation();
		}
		//Position += _currentChaseDirection * ChaseSpeed;
		Velocity = _currentChaseDirection * ChaseSpeed;
		_chaseFrameCounter = (_chaseFrameCounter + 1) % _chaseChangeFrequency;
		PlayMovementAnimation();
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
		if (!IsInstanceIdValid(Player.GetInstanceId())) return false;
		
		float distanceToPlayer = Position.DistanceTo(Player.Position);
		return distanceToPlayer <= range;
	}
}