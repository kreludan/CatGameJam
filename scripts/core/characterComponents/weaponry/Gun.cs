using Godot;
using System.Collections.Generic;

public partial class Gun : Node2D
{
	[Export]
	public float OwnerDistanceConstant;
	[Export] 
	private double _fireRate;
	private Timer _fireTimer;
	public Entity GunOwner;
	public Vector2 BulletDirection { get; set; }
	public bool IsFiring { get; set; }
	public GameplayConstants.BulletType LoadedBullet;
	private readonly Queue<BaseBullet> _bulletPool = new();
	private const int BulletPoolSize = 50;
	private Node2D _bulletSpawnPoint;
	private Node2D _bulletNodeContainer;
	private Node2D _bulletPoolContainer;
	
	public override void _Process(double delta)
	{
		HandlePlayerFiring();
	}

	public override void _PhysicsProcess(double delta)
	{
		foreach (BaseBullet bullet in _bulletPool)
		{
			if (!bullet.Visible) continue;
			bullet.UpdateBulletPhysics();
		}
	}

	public void Initialize(Entity gunOwner)
	{
		_bulletSpawnPoint = GetNode<Node2D>("BulletSpawnPoint");
		GunOwner = gunOwner;
		//GD.Print("CREATING BULLETS FOR: " + GunOwner.Name);
		CreateBulletPool();
		InitializeBulletPool();
		SetFireTimer();
	}

	private void CreateBulletPool()
	{
		_bulletNodeContainer = GetNode("/root/RewriteScene/BulletPool") as Node2D;
		Node2D bulletPool = new();
		bulletPool.Name = new StringName(GunOwner.Name + "GunBulletPool");
		_bulletNodeContainer?.AddChild(bulletPool);
		_bulletPoolContainer = bulletPool;
	}

	private void SetFireTimer()
	{
		_fireTimer = GetNodeOrNull<Timer>("FireTimer");
		if (!_fireTimer.IsValid()) return;
		
		_fireTimer.WaitTime = _fireRate;
		_fireTimer.OneShot = true; 
	}
	
	private void InitializeBulletPool()
	{
		for (int i = 0; i < BulletPoolSize; i++)
		{
			CreateNewBullet();
		}
	}

	private void HandlePlayerFiring()
	{
		if (!IsFiring) return;
		if (!_fireTimer.IsStopped()) return;
		
		if (_bulletPool.Count == 0)
		{
			CreateNewBullet();
			return;
		}
		FireBullet();
		_fireTimer.Start();
	}

	protected void HandleAiFiring()
	{
		if (_bulletPool.Count == 0)
		{
			CreateNewBullet();
			return;
		}
		FireBullet();
	}

	private void CreateNewBullet()
	{
		Node2D bulletNode = (Node2D)GameplayConstants.BaseBulletScene.Instantiate();
		_bulletPoolContainer.AddChild(bulletNode);
		BaseBullet bullet = bulletNode as BaseBullet;
		//GD.Print("bullet for: " + GunOwner.CharacterType);
		bullet?.InitializeFields(this);
		bullet?.DeactivateBullet();
	}
	
	private void FireBullet()
	{
		if (_bulletPool.Count <= 0) return;
		
		BaseBullet bullet = _bulletPool.Dequeue();
		if (!bullet.IsValid()) return;
		
		bullet.GlobalPosition = _bulletSpawnPoint.GlobalPosition;
		bullet.ActivateBullet();
		bullet.SetDirection(BulletDirection.Normalized());
	}

	public void RequeueBullet(BaseBullet bullet)
	{
		bullet.GlobalPosition = _bulletSpawnPoint.GlobalPosition;
		_bulletPool.Enqueue(bullet);
	}
}