using Godot;

public partial class CharacterController : Node2D
{
    private InputManager _inputManager;
    private Vector2 _velocity = Vector2.Zero;
    [Export(PropertyHint.None)]
    private float _speed;

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
    }
}