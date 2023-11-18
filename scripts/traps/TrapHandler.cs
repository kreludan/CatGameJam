using Godot;

public partial class TrapHandler : Node2D
{
	private Entity _parent;
	
	//pre-load traps
	private HoleTrap _holeTrap;
	private SpikeTrap _spikeTrap;
	
	public override void _Ready()
	{
		_parent = Owner as Entity;
		// Pre-load possible traps
		_holeTrap = new HoleTrap(_parent, GameplayConstants.TrapType.HOLE);
		_spikeTrap = new SpikeTrap(_parent, GameplayConstants.TrapType.SPIKES);
	}

	public void HandleTrap(int trapId)
	{
		switch ((GameplayConstants.TrapType)trapId)
		{
			case GameplayConstants.TrapType.HOLE:
				_holeTrap.Activate();
				break;
			case GameplayConstants.TrapType.SPIKES:
				_spikeTrap.Activate();
				break;
		}
	}
}