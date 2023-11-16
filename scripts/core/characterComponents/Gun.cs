using Godot;
using System;
using System.Collections.Generic;
using System.Diagnostics;

public partial class Gun : Node2D
{
	[Export]
	public float OwnerDistanceConstant;

	[Export] 
	private double _fireRate;
	private Timer _fireTimer;
	
	public Vector2 BulletDirection { get; set; }
	
	public bool IsFiring { get; set; }
	
	public GameplayConstants.BulletType LoadedBullet;

	
	private readonly Queue<BaseBullet> _bulletPool = new();
	private const int BulletPoolSize = 100;
	
	private Node2D _bulletSpawnPoint;
	private Node2D _bulletNodeContainer;
	
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		_bulletSpawnPoint = GetNode<Node2D>("BulletSpawnPoint");
		_bulletNodeContainer = (Node2D) GetTree().GetNodesInGroup("PlayerBulletPool")[0];
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

	// Called every frame. 'delta' is the elapsed time since the previous frame.
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
			Debug.Print("empty, creating new bullet");
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
		bullet?.DeactivateBullet();
		_bulletPool.Enqueue(bullet);
	}
	
	private void FireBullet()
	{
		if (_bulletPool.Count <= 0) return;
		
		BaseBullet bullet = _bulletPool.Dequeue();
		bullet.GlobalPosition = _bulletSpawnPoint.GlobalPosition;
		bullet.ActivateBullet();
		bullet.SetDirection(BulletDirection.Normalized());
		bullet.SetOwner(this);
	}
}
