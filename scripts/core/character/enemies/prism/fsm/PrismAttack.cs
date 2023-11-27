
public partial class PrismAttack : EnemyState
{
	private Enemy _myself;
	public override void Enter(StateMachine stateMachine)
	{
		base.Enter(stateMachine);
		_myself = Enemy as Enemy;
		_myself?.PlayAnimation("Prism/attack");
	}
}