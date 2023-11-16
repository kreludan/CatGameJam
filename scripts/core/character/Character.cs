using Godot;
using System;
using System.Diagnostics;


public partial class Character : CharacterBody2D
{
	// Children:
	// Damage Taker script
	// Health script
	// Gun script
	// Hitbox script (run into things to hurt them)
	// Either a character controller or FSM script (handled by child class)
	[Export] private GameplayConstants.CharacterType CharacterType;
	public Health HealthReference { get; private set; }
	public DamageTaker DamageTakerReference { get; private set; }
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
		DamageTakerReference = GetNode<DamageTaker>("DamageTaker");
		
		//GunReference = GetNode<Gun>("Gun");
		//HitboxReference = GetNode<Hitbox>("Hitbox");
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

	private void HandleCollision(double delta)
	{
		KinematicCollision2D collision = MoveAndCollide(Velocity * (float)delta);
		if (collision == null) return;
		if (collision.GetCollider().HasMethod(GameplayConstants.HandleCollisionString))
		{
			if (collision.GetCollider() is Character collidedObject)
			{
				Debug.Print("frank and jeffrey warring appropriately");
				DamageTakerReference.HandleDamage(collidedObject.HitboxReference);
			}
		}
	}
	
	
	private void HandleInvulTimer(double delta)
	{
		if (invulTimer >= 0)
		{
			invulTimer -= delta * Engine.GetFramesPerSecond();
			_collisionCount = 0;
			GD.Print("timer: " + invulTimer);
		}
		else
		{
			SetCollisionLayerValue(2, true);
		}
	}
	
	private void SetInvulnerable()
	{
		GD.Print("Set collision now");
		SetCollisionLayerValue(2, false);
		invulTimer = invulFrames;
	}

}
