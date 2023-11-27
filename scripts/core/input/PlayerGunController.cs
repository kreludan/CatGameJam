using Godot;

public partial class PlayerGunController : Node2D
{

	private InputManager _inputManager;
	[Export]
	private Gun _gun;

	public override void _Ready()
	{
		_inputManager = GetNode<InputManager>("/root/InputManager");
		_gun = Owner as Gun;
	}

	public override void _Process(double delta)
	{
		Entity character = _gun.Owner as Entity;
		//Debug.Print(character?.name);
		if (character == null) return;
		_gun.IsFiring = _inputManager.LeftClickHeld();
		_gun.BulletDirection = (GetGlobalMousePosition() - character.GlobalPosition);
		_gun.GlobalPosition = character.GlobalPosition + (_gun.BulletDirection.Normalized() * _gun.OwnerDistanceConstant);
		_gun.Rotation = new Vector2(1, 0).AngleTo(_gun.BulletDirection);
	}
}
