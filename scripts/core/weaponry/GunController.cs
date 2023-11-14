using Godot;

public partial class GunController : Node2D
{
	[Export]
	private Node2D _owner;
	[Export]
	private float _ownerDistanceConstant;
	private InputManager _inputManager;
	
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		_inputManager = GetNode<InputManager>("/root/InputManager");
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		Vector2 playerToMouse = (_inputManager.GetMousePosition() - _owner.Position).Normalized();
		Position = _owner.Position + playerToMouse * _ownerDistanceConstant;
		Rotation = new Vector2(1, 0).AngleTo(playerToMouse);
	}
}
