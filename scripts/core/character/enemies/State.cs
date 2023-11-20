using Godot;

public partial class State : Node2D
{
	[Signal]
	public delegate void TransitionedEventHandler(State state, string newState);
	
	public virtual void Enter() { }
	
	public virtual void Update(float delta) { }

	public virtual void PhysicsUpdate(float delta) { }

	public virtual void Exit() { }
}