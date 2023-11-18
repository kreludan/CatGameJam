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
	
	public override void _Ready()
	{
		_bulletSpawnPoint = GetNode<Node2D>("BulletSpawnPoint");
		_bulletNodeContainer = (Node2D)GetTree().GetNodesInGroup("PlayerBulletPool")[0];
		GunOwner = Owner as Entity;
		SetFireTimer();
		InitializeBulletPool();
	}

	private void SetFireTimer()
	{
		_fireTimer = GetNode<Timer>("FireTimer");
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

	public override void _Process(double delta)
	{
		HandleFiring();
	}
	
	private void HandleFiring()
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
	
	private void CreateNewBullet()
	{
		Node2D bulletNode = (Node2D)GameplayConstants.BaseBulletScene.Instantiate();
		_bulletNodeContainer.AddChild(bulletNode);
		BaseBullet bullet = bulletNode as BaseBullet;
		bullet?.InitializeFields(this);
		bullet?.DeactivateBullet();
	}
	
	private void FireBullet()
	{
		if (_bulletPool.Count <= 0)
		{
			//Debug.Print("No bullets left.");
			return;
		}
		
		BaseBullet bullet = _bulletPool.Dequeue();

		if (bullet == null)
		{
			//Debug.Print("Dequeued null bullet from the queue.");
			return;
		}
		
		//Debug.Print("Dequeued bullet. " + _bulletPool.Count + " bullets left.");
		bullet.GlobalPosition = _bulletSpawnPoint.GlobalPosition;
		bullet.ActivateBullet();
		bullet.SetDirection(BulletDirection.Normalized());
	}

	public void RequeueBullet(BaseBullet bullet)
	{
		bullet.GlobalPosition = _bulletSpawnPoint.GlobalPosition;
		_bulletPool.Enqueue(bullet);
		//Debug.Print("Requeueing bullet. " + _bulletPool.Count + " bullets left.");
	}
}
