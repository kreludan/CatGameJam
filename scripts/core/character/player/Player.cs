public partial class Player : Entity
{
	protected override void Initialize()
	{
		base.Initialize();
		InitEntityType(GameplayConstants.CharacterType.Player, GameplayConstants.CollisionLayer.Player);
		if (GunReference.IsValid())
		{
			GunReference.Initialize(this);
		}
	}
}