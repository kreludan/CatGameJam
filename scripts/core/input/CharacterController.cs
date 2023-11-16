using System.Diagnostics;
using Godot;

public partial class CharacterController : Node2D
{
	private InputManager _inputManager;
	private Entity _entity;
	private Vector2 _velocity = Vector2.Zero;
	[Export]
	private float _speed;

	public override void _Ready()
	{
		_inputManager = GetNode<InputManager>("/root/InputManager");
		_entity = Owner as Entity;
		if (_entity == null)
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
		if (_entity == null) return;
		Vector2 moveDirection = _inputManager.GetMoveDirection();
		_entity.Velocity = moveDirection * _speed;
	}

}
