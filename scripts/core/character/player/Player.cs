public partial class Player : Entity
{
	protected override void Initialize()
	{
		base.Initialize();
		SeHandler = GetNode<StatusEffectHandler>("StatusEffectHandler");
		GunReference = GetNodeOrNull<Gun>("Gun");
		
		InitEntityType(GameplayConstants.CharacterType.Player, GameplayConstants.CollisionLayer.Player);
		if (GunReference.IsValid())
		{
			GunReference.Initialize(this);
		}
	}
}