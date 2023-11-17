using Godot;


public partial class Entity : CharacterBody2D
{
	// Children:
	// Damage Taker script
	// Health script
	// Gun script
	// Hitbox script (run into things to hurt them)
	// Either a character controller or FSM script (handled by child class)
	[Export] private GameplayConstants.CharacterType CharacterType;
	public Health HealthReference { get; private set; }
	public CollisionEffectHandler CollisionEffectHandlerReference { get; private set; }
	public Gun GunReference { get; private set; }
	public Hitbox HitboxReference { get; private set; }
	
	private int _collisionCount;
	private int _maxCollisions = 1;

	private int invulFrames = 150;
	private double invulTimer;

	
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		HealthReference = GetNode<Health>("Health");
		CollisionEffectHandlerReference = GetNode<CollisionEffectHandler>("CollisionEffectHandler");
		if (CollisionEffectHandlerReference == null)
		{
			GD.Print(Name + " is null");
		}
		HitboxReference = GetNode<Hitbox>("Hitbox");
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		HandleInvulTimer(delta);
	}
	
	public override void _PhysicsProcess(double delta)
	{
		HandleCollision(delta);
	}

	protected KinematicCollision2D HandleCollision(double delta)
	{
		KinematicCollision2D collision = MoveAndCollide(Velocity * (float)delta);

		if (collision == null) return null;
		if (collision.GetCollider().HasMethod(GameplayConstants.HandleCollisionString))
		{
			if (collision.GetCollider() is Entity collidedObject)
			{
				//GD.Print( "I am collided with: " + collidedObject.Name);
				CollisionEffectHandlerReference.HandleCollision(collidedObject.HitboxReference);
			}
		}
		return collision;
	}
	
	
	private void HandleInvulTimer(double delta)
	{
		if (invulTimer >= 0)
		{
			invulTimer -= delta * Engine.GetFramesPerSecond();
			_collisionCount = 0;
			//GD.Print("timer: " + invulTimer);
		}
		else
		{
			SetCollisionLayerValue(2, true);
		}
	}
	
	private void SetInvulnerable()
	{
		//GD.Print("Set collision now");
		SetCollisionLayerValue(2, false);
		invulTimer = invulFrames;
	}

}
