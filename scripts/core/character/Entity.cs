using Godot;
using static GameplayConstants;

public partial class Entity : CharacterBody2D
{
	// Children:
	// Damage Taker script
	// Health script
	// Gun script
	// Hitbox script (run into things to hurt them)
	// Either a character controller or FSM script (handled by child class)
	public CharacterType CharacterType;
	public Health HealthReference { get; private set; }
	public CollisionEffectHandler CollisionEffectHandlerReference { get; private set; }
	public Gun GunReference { get; protected set; }
	public Hitbox HitBoxReference { get; private set; }
	public Sprite2D SpriteRef { get; private set; }
	public StatusEffectHandler SeHandler { get; set; }
	public AnimationController AnimationControllerRef { get; set; }
	
	private int _collisionCount;
	private int _maxCollisions = 1;

	private int _invulnerabilityFrames = 150;
	private double _invulnerabilityTimer;
	public CollisionLayer BaseCollisionLayer;
	private CollisionLayer _currentLayer;

	private AnimationPlayer _animationPlayer;
	private bool _isInvulnerable;

	public override void _Ready()
	{
		Initialize();
	}

	public override void _Process(double delta)
	{
		Update((float)delta);
	}

	public override void _PhysicsProcess(double delta)
	{
		PhysicsUpdate((float)delta);
	}

	//include things every entity should have on initialization
	protected virtual void Initialize()
	{
		SpriteRef = GetNodeOrNull<Sprite2D>("Sprite");
		HealthReference = GetNodeOrNull<Health>("Health");
		CollisionEffectHandlerReference = GetNodeOrNull<CollisionEffectHandler>("CollisionEffectHandler");
		HitBoxReference = GetNodeOrNull<Hitbox>("Hitbox");
		SeHandler = GetNodeOrNull<StatusEffectHandler>("StatusEffectHandler");
		GunReference = GetNodeOrNull<Gun>("Gun");
		SetAnimationController();
	}

	public void SetAnimationController()
	{
		AnimationControllerRef = GetNodeOrNull<AnimationController>("Sprite/AnimationPlayer");
	}

	protected virtual void Update(float delta) { }

	protected virtual void PhysicsUpdate(float delta)
	{
		HandleCollision();
	}
	
	protected void InitEntityType(CharacterType type, CollisionLayer layer)
	{
		CharacterType = type;
		BaseCollisionLayer = layer;
		SetCollisionLayerAndMask(BaseCollisionLayer);
	}

	protected KinematicCollision2D HandleCollision()
	{
		MoveAndSlide();
		KinematicCollision2D collision = GetLastSlideCollision();
		if (!collision.IsValid()) return null;

		if (collision.GetCollider().HasMethod(HandleCollisionString))
		{
			if (collision.GetCollider() is Entity collidedObject)
			{
				if (!CheckIfValidCollision(collidedObject.BaseCollisionLayer)) return null;
				
				CollisionEffectHandlerReference.HandleCollision(collidedObject);
			}
		}
		return collision;
	}

	private bool CheckIfValidCollision(CollisionLayer layerCollidedWith)
	{
		// Get the mask for the current layer
		int myLayerMask = GetMask(BaseCollisionLayer);
		// Get the mask for the collided layer
		int collidedLayerMask = GetMask(layerCollidedWith);
		// Check if the collided layer is in the valid layers for the current layer
		return (myLayerMask & collidedLayerMask) != 0;
	}

	public void SetCollisionLayerAndMask(CollisionLayer collisionLayerTo)
	{
		ClearAllCollisionMasks();
		SetCollisionLayerAndMaskForLayer(collisionLayerTo, true);
		_currentLayer = collisionLayerTo;
	}

	private void SetCollisionLayerAndMaskForLayer(CollisionLayer layer, bool isActive)
	{
		int layerBit = GetLayerBit(layer);
		SetCollisionLayerValue(layerBit, isActive);
		int layerBitMask = GetMask(layer);
		SetCollisionMask(layerBitMask, isActive);
	}

	private void SetCollisionMask(int mask, bool isActive)
	{
		for (int i = 0; i < sizeof(int) * 8; i++)
		{
			int bit = 1 << i;
			if ((mask & bit) == 0) continue;
			//GD.Print("Setting " + Name + " to mask: " + i);
			SetCollisionMaskValue(i, isActive);
		}
	}
	
	private void ClearAllCollisionMasks()
	{
		for (int i = 1; i < sizeof(int) * 8; i++)
		{
			int bit = 1 << i;
			if (bit == 0) continue;
			SetCollisionLayerValue(i, false);
			SetCollisionMaskValue(i, false);
		}
	}

	public void Die()
	{
		SetCollisionLayerAndMask(GameplayConstants.CollisionLayer.Delete);
		SetProcess(false);
		SetPhysicsProcess(false);
		Hide();
	}
}