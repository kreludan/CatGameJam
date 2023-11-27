using Godot;

public partial class BatIdle : EnemyState
{
	private Vector2 _moveDirection = Vector2.Zero;
	private float _wanderTime;
	private float _moveSpeed = 40f;
	private PassableTiles _tiles;
	private bool physicsFrameHit;
	
	public override void Enter()
	{
		base.Enter();
		CurrentState = this;
		_tiles = GetNode("/root/RewriteScene/Room1/PassableTerrain") as PassableTiles;
		if (!_tiles.IsValid())
		{
			GD.Print("TILES NOT VALID");
			return;
		}
		Nav.DebugPathCustomColor = Colors.Blue;
		RandomWander();
	}

	public override void Update(float delta)
	{
		base.Update(delta);
		if (_wanderTime > 0)
		{
			_wanderTime -= delta;
		}
		else
		{
			RandomWander();
		}
		if (PlayerDirection.Length() < 200)
		{
			GD.Print("Transition to follow");
			EmitSignal(nameof(Transitioned), this, "follow");
		}
	}

	public override void PhysicsUpdate(float delta)
	{
		base.PhysicsUpdate(delta);
		
		if (Enemy == null) return;

		// If there is remaining time for wandering, continue in the current direction
		if (_wanderTime > 0)
		{
			if (Nav.DistanceToTarget() < 1)
			{
				SetVelocityTarget(Vector2.Zero);
			}
			else
			{
				if (physicsFrameHit)
				{
					SetVelocityTarget(ToLocal(Nav.GetNextPathPosition()).Normalized() * _moveSpeed);
				}
				else
				{
					physicsFrameHit = true;
					RandomWander();
				}
			}
			return;
		}
		// Check if the player is within a certain range to transition to follow state
		if (PlayerDirection.Length() < 200)
		{
			GD.Print("Transition to follow");
			EmitSignal(nameof(Transitioned), this, "follow");
		}
	}
	
	private void RandomWander()
	{
		// _moveDirection = new Vector2(Rng.RandfRange(-1, 1), Rng.RandfRange(-1, 1)).Normalized();
		if (!_tiles.IsValid()) return;
		
		Nav.TargetPosition = _tiles.GetRandomNonTrapTilePosition();
		_wanderTime = Rng.RandfRange(1, 4);
		GD.Print("Wander");
	}
}