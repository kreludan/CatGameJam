
public partial class PrismAttack : EnemyState
{
	private Enemy _myself;
	public override void Enter()
	{
		base.Enter();
		CurrentState = this;
		_myself = Enemy as Enemy;
		_myself?.PlayAnimation("Prism/attack");
	}
}
