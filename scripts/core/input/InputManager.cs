using Godot;

public partial class InputManager : Node
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
	private bool _interactHeld;
	private bool _interactReleased;
	private bool _leftClickPressed;
	private bool _leftClickHeld;
	private bool _leftClickReleased;
	private bool _rightClickPressed;
	private bool _rightClickHeld;
	private bool _rightClickReleased;
	private bool _dodgePressed;
	private bool _dodgeHeld;
	private bool _dodgeReleased;
	
	private Vector2 _direction = Vector2.Zero;
	private Vector2 _mousePosition = Vector2.Zero;

	private static readonly StringName MoveRightString = new("move_right");
	private static readonly StringName MoveLeftString = new("move_left");
	private static readonly StringName MoveDownString = new("move_down");
	private static readonly StringName MoveUpString = new("move_up");
	private static readonly StringName InteractString = new("interact");
	private static readonly StringName LeftClickString = new("left_click");
	private static readonly StringName RightClickString = new("right_click");
	private static readonly StringName DodgeString = new("dodge");

	public override void _Input(InputEvent @event)
	{
		if (@event is InputEventMouseMotion eventMouseMotion)
		{
			//GD.Print("Mouse Motion at: ", eventMouseMotion.Position);
			_mousePosition = eventMouseMotion.Position;
		}
	}
	
	//for things that need to be read every frame
	public override void _Process(double delta)
	{
		//action pressed will check every frame
		// Move right.
		_movingRight = Input.IsActionPressed(MoveRightString);
		_movingRightReleased = Input.IsActionJustReleased(MoveRightString);
		_movingLeft = Input.IsActionPressed(MoveLeftString);
		_movingLeftReleased = Input.IsActionJustReleased(MoveLeftString);
		_movingDown = Input.IsActionPressed(MoveDownString);
		_movingDownReleased = Input.IsActionJustReleased(MoveDownString);
		_movingUp = Input.IsActionPressed(MoveUpString);
		_movingUpReleased = Input.IsActionJustReleased(MoveUpString);
		//Just pressed only checks once
		// jump
		_interactPressed = Input.IsActionJustPressed(InteractString);
		_interactHeld = Input.IsActionPressed(InteractString);
		_interactReleased = Input.IsActionJustReleased(InteractString);
		_leftClickPressed = Input.IsActionJustPressed(LeftClickString);
		_leftClickHeld = Input.IsActionPressed(LeftClickString);
		_leftClickReleased = Input.IsActionJustReleased(LeftClickString);
		_rightClickPressed = Input.IsActionJustPressed(RightClickString);
		_rightClickHeld = Input.IsActionPressed(RightClickString);
		_rightClickReleased = Input.IsActionJustReleased(RightClickString);
		_dodgePressed = Input.IsActionJustPressed(DodgeString);
		_dodgeHeld = Input.IsActionPressed(DodgeString);
		_dodgeReleased = Input.IsActionJustReleased(DodgeString);
	}

	public Vector2 GetMousePosition()
	{
		return _mousePosition;
	}

	public bool InteractPressed()
	{
		return _interactPressed;
	}

	public bool InteractHeld()
	{
		return _interactHeld;
	}
	
	public bool InteractReleased()
	{
		return _interactReleased;
	}

	public bool LeftClickPressed()
	{
		return _leftClickPressed;
	}

	public bool LeftClickHeld()
	{
		return _leftClickHeld;
	}

	public bool LeftClickReleased()
	{
		return _leftClickReleased;
	}

	public bool RightClickPressed()
	{
		return _rightClickPressed;
	}

	public bool RightClickHeld()
	{
		return _rightClickHeld;
	}

	public bool RightClickReleased()
	{
		return _rightClickReleased;
	}
	
	public bool DodgePressed()
	{
		return _dodgePressed;
	}
	
	public bool DodgeHeld()
	{
		return _dodgeHeld;
	}

	public bool DodgeReleased()
	{
		return _dodgeReleased;
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
			_direction.Y -= 1.0f;
		}
		else if (_movingDown)
		{
			_direction.Y += 1.0f;
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