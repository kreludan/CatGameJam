using Godot;

public partial class PlayerGunController : Node2D
{
	private InputManager _inputManager;
	private Gun _gun;
	private Entity _character;
	
	public override void _Ready()
	{
		_inputManager = GetNode<InputManager>("/root/InputManager");
		_gun = Owner as Gun;
		_character = _gun?.Owner as Entity;
	}

	public override void _Process(double delta)
	{
		if (!_character.IsValid() || !_gun.IsValid()) return;

		_gun.IsFiring = _inputManager.LeftClickHeld();
		Vector2 mousePosition = GetGlobalMousePosition();
		_gun.BulletDirection = mousePosition - _character.GlobalPosition;
		Vector2 newPosition = _character.GlobalPosition + _gun.BulletDirection.Normalized() * _gun.OwnerDistanceConstant;
		if (_gun.GlobalPosition != newPosition)
		{
			_gun.GlobalPosition = newPosition;
			_gun.Rotation = new Vector2(1, 0).AngleTo(_gun.BulletDirection);
		}
		_gun.HandlePlayerFiring();
	}
}