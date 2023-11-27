using Godot;
using Godot.Collections;

public partial class StateMachine : Node2D
{
	[Export]
	private State _initialState;
	public State CurrentState { get; private set; }
	private Dictionary<string, State> _states = new();

	public override void _Ready()
	{
		foreach (Node child in GetChildren())
		{
			if (child is State state)
			{
				_states[state.Name.ToString().ToLower()] = state;
				state.Transitioned += OnChildTransition;
			}
			if (_initialState.IsValid())
			{
				_initialState.Enter(this);
				CurrentState = _initialState;
			}
		}
	}

	public override void _Process(double delta)
	{
		CurrentState?.Update((float)delta);
	}

	public override void _PhysicsProcess(double delta)
	{
		CurrentState?.PhysicsUpdate((float)delta);
	}

	public void OnChildTransition(State state, string newState)
	{
		if (state != CurrentState) return;

		State nextState = _states[newState.ToLower()];
		if (!nextState.IsValid()) return;

		CurrentState?.Exit();
		nextState.Enter(this);
		CurrentState = nextState;
	}
	
	// Disconnect the signal when the node is about to be removed from the scene.
	public override void _ExitTree()
	{
		foreach (State state in _states.Values)
		{
			state.Transitioned -= OnChildTransition;
		}
		base._ExitTree();
	}
}