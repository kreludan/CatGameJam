using Godot;
using System.Collections.Generic;
using System.Diagnostics;

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
		Character character = _gun.Owner as Character;
		//Debug.Print(character?.name);
		if (character == null) return;
		_gun.IsFiring = _inputManager.LeftClickHeld();
		_gun.BulletDirection = (_inputManager.GetMousePosition() - character.GlobalPosition).Normalized();
		_gun.GlobalPosition = character.GlobalPosition + (_gun.BulletDirection * _gun.OwnerDistanceConstant);
		_gun.Rotation = new Vector2(1, 0).AngleTo(_gun.BulletDirection);
	}
}
