using System.Collections.Generic;
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
	public Sprite2D SpriteRef { get; private set; }
	public StatusEffectHandler SeHandler { get; set; }
	
	private int _collisionCount;
	private int _maxCollisions = 1;

	private int _invulFrames = 150;
	private double _invulTimer;
	public GameplayConstants.CollisionLayer BaseCollisionLayer;
	private GameplayConstants.CollisionLayer _currentLayer;

	private AnimationPlayer _animationPlayer;
	private bool _isInvulnerable;
	
	public override void _Ready()
	{
		SpriteRef = GetNode<Sprite2D>("Sprite");
		HealthReference = GetNode<Health>("Health");
		GunReference = GetNodeOrNull<Gun>("Gun");
		_animationPlayer = GetNode<AnimationPlayer>("Sprite/AnimationPlayer");
		CollisionEffectHandlerReference = GetNode<CollisionEffectHandler>("CollisionEffectHandler");
		if (!CollisionEffectHandlerReference.IsValid())
		{
			GD.Print(Name + " is null");
		}
		HitboxReference = GetNode<Hitbox>("Hitbox");
	}
	
	public override void _PhysicsProcess(double delta)
	{
		HandleCollision();
	}

	public void PlayAnimation(string animationName)
	{
		if (!_animationPlayer.IsValid())
		{
			GD.Print("Getting new player");
			_animationPlayer = GetNode<AnimationPlayer>("Sprite/AnimationPlayer");
		}
		_animationPlayer.Play(animationName);
	}

	protected KinematicCollision2D HandleCollision()
	{
		MoveAndSlide();
		KinematicCollision2D collision = GetLastSlideCollision();
		if (!collision.IsValid()) return null;

		if (collision.GetCollider().HasMethod(GameplayConstants.HandleCollisionString))
		{
			if (collision.GetCollider() is Entity collidedObject)
			{
				if (!CheckIfValidCollision(collidedObject.BaseCollisionLayer)) return null;
				CollisionEffectHandlerReference.HandleCollision(collidedObject);
			}
		}
		return collision;
	}

	public bool CheckIfValidCollision(GameplayConstants.CollisionLayer layerCollidedWith)
	{
		GameplayConstants.CollisionLayer myLayer = BaseCollisionLayer;
		List<GameplayConstants.CollisionLayer> validCollisionLayers = GameplayConstants.GetCollisionMasksPerLayer(myLayer);
		foreach (GameplayConstants.CollisionLayer layer in validCollisionLayers)
		{
			if (layer == layerCollidedWith)
			{
				return true;
			}
		}

		return false;
	}
	
	
	public void Die()
	{
		SetCollisionLayerAndMask(GameplayConstants.CollisionLayer.Delete, _currentLayer);
		SetProcess(false);
		SetPhysicsProcess(false);
		Hide();
	}

	public void SetCollisionLayerAndMask(GameplayConstants.CollisionLayer collisionLayerTo, GameplayConstants.CollisionLayer  collisionLayerFrom = GameplayConstants.CollisionLayer.None)
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
		//GD.Print("setting entity " + Name + "to layer " + layer + " to activity" + isActive);
		foreach (GameplayConstants.CollisionLayer mask in GameplayConstants.GetCollisionMasksPerLayer(layer))
		{
			SetCollisionMaskValue((int)mask, isActive);
		}
	}
}