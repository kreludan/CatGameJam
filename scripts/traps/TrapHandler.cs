using Godot;

public partial class TrapHandler : Node2D
{
	private Health _health;
	private Entity parent;
	
	public override void _Ready()
	{
		parent = Owner as Entity;
	}

	public void HandleTrap(int trapId)
	{
		if (_health == null)
		{
			_health = parent?.HealthReference;
		}
		switch ((GameplayConstants.TrapType)trapId)
		{
			case GameplayConstants.TrapType.HOLE:
				GD.Print("This is a hole");
				_health.TakeDamage(1);
				break;
			case GameplayConstants.TrapType.SPIKES:
				GD.Print("THESE ARE SPIKES. OUCH");
				_health.TakeDamage(1);
				break;
		}
	}
}