using Godot;

public partial class CharacterController : CharacterBody2D
{
    private InputManager _inputManager;
    private Vector2 _velocity = Vector2.Zero;
    [Export]
    private float _speed;
    private static readonly Vector2 ScalePositive = new Vector2(1, 1);
    private static readonly Vector2 ScaleNegative = new Vector2(-1, 1);
    private int _collisionCount;
    private int _maxCollisions = 1;

    private int invulFrames = 150;
    private double invulTimer;

    public override void _Ready()
	{
		_inputManager = GetNode<InputManager>("/root/InputManager");
	}

	public override void _Process(double delta)
	{
		HandleInput();
		HandleInvulTimer(delta);
	}

	public override void _PhysicsProcess(double delta)
	{
		HandleCollision(delta);
	}

	private void HandleCollision(double delta)
	{
		KinematicCollision2D collision = MoveAndCollide(Velocity * (float)delta);
		//
		// if (collision?.GetCollider() is not Node2D collidedObject) return;
		// // Check if the "Health" child node exists
		// Node healthNode = collidedObject.GetNodeOrNull("Health");
		// if (healthNode is Health health)
		// {
		// 	if (health.CurrentOwner == global::Owner.Player) return;
		// 	if (_collisionCount >= _maxCollisions) return;
		//
		// 	_collisionCount++;
		// 	
		// 	
		// }
	}

	public void _on_health_on_take_damage()
	{
		SetInvulnerable();
	}

	private void SetInvulnerable()
	{
		GD.Print("Set collision now");
		SetCollisionLayerValue(2, false);
		invulTimer = invulFrames;
	}

	private void HandleInput()
	{
		Vector2 moveDirection = _inputManager.GetMoveDirection();
		Velocity = moveDirection * _speed;
	}

	private void HandleInvulTimer(double delta)
	{
		if (invulTimer >= 0)
		{
			invulTimer -= delta * Engine.GetFramesPerSecond();
			_collisionCount = 0;
			GD.Print("timer: " + invulTimer);
		}
		else
		{
			SetCollisionLayerValue(2, true);
		}
	}
}
