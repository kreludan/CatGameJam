using System.Diagnostics;
using Godot;

public partial class CharacterController : Node2D
{
	private InputManager _inputManager;
	private Character _character;
	private Vector2 _velocity = Vector2.Zero;
	[Export]
	private float _speed;

	public override void _Ready()
	{
		_inputManager = GetNode<InputManager>("/root/InputManager");
		_character = Owner as Character;
		if (_character == null)
		{
			Debug.Print("Owner is not a character!!!");
		}
	}

	public override void _Process(double delta)
	{
		HandleInput();
	}
	
	private void HandleInput()
	{
		if (_character == null) return;
		Vector2 moveDirection = _inputManager.GetMoveDirection();
		_character.Velocity = moveDirection * _speed;
	}

}
