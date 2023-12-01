public partial class PrismAttack : EnemyState
{
	private Enemy _myself;
	
	public override void Enter(StateMachine stateMachine)
	{
		base.Enter(stateMachine);
		_myself = Enemy as Enemy;
		if (!_myself.IsValid()) return;
		if (!_myself.AnimationControllerRef.IsValid())
		{
			_myself.SetAnimationController();
		}
		_myself.AnimationControllerRef?.PlayAnimation("Prism/attack");
	}
}