using Godot;

public partial class State : Node2D
{
	[Signal]
	public delegate void TransitionedEventHandler(State state, string newState);
	
	protected StateMachine StateMachine { get; private set; }
	
	public virtual void Enter(StateMachine stateMachine)
	{
		// Set the reference to the current state machine
		StateMachine = stateMachine;
	}
	
	public virtual void Update(float delta) { }

	public virtual void PhysicsUpdate(float delta) { }

	public virtual void Exit() { }
}