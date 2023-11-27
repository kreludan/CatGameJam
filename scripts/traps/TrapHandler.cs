using System.Collections.Generic;
using Godot;

public partial class TrapHandler : Node2D
{
	private Entity _parent;
	
	//pre-load traps
	private HoleTrap _holeTrap;
	private SpikeTrap _spikeTrap;
	private TerrainDetector _terrainDetector;
	private readonly List<Trap> _possibleTraps = new();
	
	public override void _Ready()
	{
		_parent = Owner as Entity;
		_terrainDetector = Owner.GetNode<TerrainDetector>("TerrainDetector");
		// Pre-load possible traps
		_holeTrap = new HoleTrap(_parent, GameplayConstants.TrapType.Hole);
		_spikeTrap = new SpikeTrap(_parent, GameplayConstants.TrapType.Spikes);
		_possibleTraps.Add(_holeTrap);
		_possibleTraps.Add(_spikeTrap);
	}

	public override void _Process(double delta)
	{
		foreach (KeyValuePair<Vector2I, int> kvp in _terrainDetector.GetActiveTraps())
		{
			int trapTypeValue = kvp.Value;
			// Check if the trap type is in the list of possible traps
			if (_possibleTraps.Exists(trap => (int)trap.TrapType == trapTypeValue))
			{
				// Find the trap in the list
				Trap matchingTrap = _possibleTraps.Find(trap => (int)trap.TrapType == trapTypeValue);
				// Call the Update method on the matching active trap
				matchingTrap.Update();
			}
		}
	}

	public void HandleTrap(int trapId)
	{
		switch ((GameplayConstants.TrapType)trapId)
		{
			case GameplayConstants.TrapType.Hole:
				_holeTrap.Activate();
				break;
			case GameplayConstants.TrapType.Spikes:
				_spikeTrap.Activate();
				break;
		}
	}
}