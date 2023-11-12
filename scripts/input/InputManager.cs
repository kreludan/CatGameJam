using Godot;

public class InputManager : Node
{
    private bool _movingRight;
    private bool _movingRightReleased;
    private bool _movingLeft;
    private bool _movingLeftReleased;
    private bool _movingUp;
    private bool _movingUpReleased;
    private bool _movingDown;
    private bool _movingDownReleased;
    private bool _interactPressed;
    private bool _interactReleased;
    private Vector2 _direction = Vector2.Zero;
    
    //for things that need to be read every frame
    public override void _Process(double delta)
    {
        //action pressed will check every frame
        // Move right.
        _movingRight = Input.IsActionPressed("move_right");
        _movingRightReleased = Input.IsActionJustReleased("move_right");
        _movingLeft = Input.IsActionPressed("move_left");
        _movingLeftReleased = Input.IsActionJustReleased("move_left");
        _movingDown = Input.IsActionPressed("move_down");
        _movingDownReleased = Input.IsActionJustReleased("move_down");
        _movingUp = Input.IsActionPressed("move_up");
        _movingUpReleased = Input.IsActionJustReleased("move_up");
        //Just pressed only checks once
        // jump
        _interactPressed = Input.IsActionJustPressed("interact");
        _interactReleased = Input.IsActionJustReleased("interact");
    }

    public bool InteractPressed()
    {
        return _interactPressed;
    }
    
    public bool InteractReleased()
    {
        return _interactReleased;
    }

    public Vector2 GetMoveDirection()
    {
        _direction = Vector2.Zero;
        if (_movingRight)
        {
            _direction.X += 1.0f;
        }
        else if (_movingLeft)
        {
            _direction.X -= 1.0f;
        }
        if (_movingUp)
        {
            _direction.Y += 1.0f;
        }
        else if (_movingDown)
        {
            _direction.Y -= 1.0f;
        }
        return _direction;
    }

    public bool MoveRightPressed()
    {
        return _movingRight;
    }
    
    public bool MoveRightReleased()
    {
        return _movingRightReleased;
    }

    public bool MoveLeftPressed()
    {
        return _movingLeft;
    }
    
    public bool MoveLeftReleased()
    {
        return _movingLeftReleased;
    }

    public bool MoveUpPressed()
    {
        return _movingUp;
    }
    
    public bool MoveUpReleased()
    {
        return _movingUpReleased;
    }

    public bool MoveDownPressed()
    {
        return _movingDown;
    }
    
    public bool MoveDownReleased()
    {
        return _movingDownReleased;
    }
}