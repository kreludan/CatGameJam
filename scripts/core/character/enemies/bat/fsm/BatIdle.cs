using Godot;

public partial class BatIdle : EnemyState
{
	private Vector2 _moveDirection = Vector2.Zero;
	private float _wanderTime;
	
	private float _moveSpeed = 40f;

	private void RandomWander()
	{
		_moveDirection = new Vector2(Rng.RandfRange(-1, 1), Rng.RandfRange(-1, 1)).Normalized();
		_wanderTime = Rng.RandfRange(1, 3);
	}

	public override void Enter()
	{
		RandomWander();
	}

	public override void Update(float delta)
	{
		if (_wanderTime > 0)
		{
			_wanderTime -= delta;
		}
		else
		{
			RandomWander();
		}
	}

	public override void PhysicsUpdate(float delta)
	{
		if (Enemy == null) return;
		
		Enemy.Velocity = _moveDirection * _moveSpeed;
		if (PlayerDirection.Length() < 200)
		{
			EmitSignal(nameof(Transitioned), this, "follow");
		}
	}
}