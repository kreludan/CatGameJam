using Godot;

public partial class CharacterController : Node2D
{
    private InputManager _inputManager;
    private Vector2 _velocity = Vector2.Zero;
    [Export]
    private float _speed;
    private static readonly Vector2 ScalePositive = new Vector2(1, 1);
    private static readonly Vector2 ScaleNegative = new Vector2(-1, 1);

	public override void _Ready()
	{
		_inputManager = GetNode<InputManager>("/root/InputManager");
	}

	public override void _Process(double delta)
	{
		HandleInput();
		MoveCharacter();
	}

	private void HandleInput()
	{
		// Get the movement direction from the input manager
		Vector2 moveDirection = _inputManager.GetMoveDirection();
		// Calculate the velocity based on the move direction and speed
		_velocity = moveDirection * _speed;
		// ie: Check for interactions
		if (_inputManager.InteractPressed())
		{
			// Handle interaction logic here
			GD.Print("Interact Pressed");
		}
	}

    private void MoveCharacter()
    {
        // Move the character based on the calculated velocity
        Position += _velocity * (float)GetProcessDeltaTime();
        // Flip the character horizontally based on the move direction
        if (_velocity.X != 0)
        {
            // If moving right, set scale.x to positive; if moving left, set scale.x to negative
            Scale = _velocity.X < 0 ? ScalePositive : ScaleNegative;
        }
    }
}
