using Godot;


public partial class Entity : CharacterBody2D
{
	// Children:
	// Damage Taker script
	// Health script
	// Gun script
	// Hitbox script (run into things to hurt them)
	// Either a character controller or FSM script (handled by child class)
	public GameplayConstants.CharacterType CharacterType;
	public Health HealthReference { get; private set; }
	public CollisionEffectHandler CollisionEffectHandlerReference { get; private set; }
	public Gun GunReference { get; private set; }
	public Hitbox HitboxReference { get; private set; }
	
	private int _collisionCount;
	private int _maxCollisions = 1;

	private int invulFrames = 150;
	private double invulTimer;
	protected GameplayConstants.CollisionLayer BaseCollisionLayer;
	private GameplayConstants.CollisionLayer _currentLayer;

	[Export] private AnimationPlayer _animationPlayer;
	private bool _invul;
	
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

	public void Die()
	{
		SetCollisionLayerAndMask(GameplayConstants.CollisionLayer.Delete, _currentLayer);
		SetProcess(false);
		SetPhysicsProcess(false);
		Hide();
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
			if (_invul)
			{
				_invul = false;
				GD.Print("setting invul to false");
				SetCollisionLayerAndMask(BaseCollisionLayer, GameplayConstants.CollisionLayer.Invulnerable);
			}
		}
	}
	
	private void SetInvulnerable()
	{
		//GD.Print("Set collision now");
		SetCollisionLayerAndMask(GameplayConstants.CollisionLayer.Invulnerable, BaseCollisionLayer);
		invulTimer = invulFrames;
		_invul = true;
		GD.Print("Setting invul to true");
	}
	
	protected void SetCollisionLayerAndMask(GameplayConstants.CollisionLayer collisionLayerTo,
		GameplayConstants.CollisionLayer  collisionLayerFrom = GameplayConstants.CollisionLayer.None)
	{
		// If we're changing the collision layer from a pre-existing one, we need to remove the old layer and mask
		SetCollisionLayerAndMaskForLayer(collisionLayerFrom, false);
		
		// Then we add the new layer and mask
		SetCollisionLayerAndMaskForLayer(collisionLayerTo, true);

		_currentLayer = collisionLayerTo;
	}

	private void SetCollisionLayerAndMaskForLayer(GameplayConstants.CollisionLayer layer, bool isActive)
	{
		// Case where the entity doesn't have a collision layer yet; we don't make any changes
		
		if (layer == GameplayConstants.CollisionLayer.None) return;
        
		SetCollisionLayerValue((int)layer, isActive);
		GD.Print("setting entity " + Name + "to layer " + layer + " to activity" + isActive);
		foreach (GameplayConstants.CollisionLayer mask in GameplayConstants.GetCollisionMasksPerLayer(layer))
		{
			SetCollisionMaskValue((int)mask, isActive);
		}
	}
}
