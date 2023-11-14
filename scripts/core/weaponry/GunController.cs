using Godot;
using System.Collections.Generic;
using System.Diagnostics;

public partial class GunController : Node2D
{
	[Export]
	private Node2D _owner;
	[Export]
	private float _ownerDistanceConstant;
	[Export] 
	private double _secondsIntervalBetweenBullets;
	private Timer _fireTimer;
	private InputManager _inputManager;
	private readonly Queue<BaseBullet> _bulletPool = new();
	private const int BulletPoolSize = 100;
	private PackedScene _bulletScene = GD.Load<PackedScene>("res://assets/scenes/base_bullet.tscn");
	private Node2D _bulletSpawnPoint;

	public override void _Ready()
	{
		_bulletSpawnPoint = GetNode<Node2D>("BulletSpawnPoint");
		_inputManager = GetNode<InputManager>("/root/InputManager");
		SetFireTimer();
		InitializeBulletPool();
	}

	public override void _Process(double delta)
	{
		ReorientGunAndBullets();
		HandleFiring();
	}

	private void SetFireTimer()
	{
		_fireTimer = GetNode<Timer>("FireTimer");
		_fireTimer.WaitTime = _secondsIntervalBetweenBullets;
		_fireTimer.OneShot = true; 
	}
	
	private void CreateNewBullet()
	{
		Node2D bulletNode = (Node2D)_bulletScene.Instantiate();
		Node2D bulletNodeContainer = (Node2D) GetTree().GetNodesInGroup("PlayerBulletPool")[0];
		bulletNodeContainer.AddChild(bulletNode);
		BaseBullet bullet = bulletNode as BaseBullet;
		bullet?.DeactivateBullet();
		_bulletPool.Enqueue(bullet);
	}

	private void InitializeBulletPool()
	{
		for (int i = 0; i < BulletPoolSize; i++)
		{
			CreateNewBullet();
		}
	}

	private void FireBullet()
	{
		if (_bulletPool.Count <= 0) return;
		
		BaseBullet bullet = _bulletPool.Dequeue();
		Node2D bulletSpawnPoint = GetNode<Node2D>("BulletSpawnPoint");
		bullet.GlobalPosition = bulletSpawnPoint.GlobalPosition;
		bullet.ActivateBullet();
		bullet.SetDirection((_inputManager.GetMousePosition() - _owner.Position).Normalized());
	}

	// Moves the gun (and the queue of inactive bullet objects) to the new location based on mouse position
	private void ReorientGunAndBullets()
	{
		Vector2 playerToMouse = (_inputManager.GetMousePosition() - _owner.Position).Normalized();
		Position = _owner.Position + playerToMouse * _ownerDistanceConstant;
		Rotation = new Vector2(1, 0).AngleTo(playerToMouse);
		// foreach (Node2D inactiveBullet in _inactiveBulletQueue)
		// {
		// 	inactiveBullet.Position = Position;
		// }
		// List<int> indicesToRemove = new(); 
		// for(int i = 0; i < _activeBulletList.Count; i++)
		// {
		// 	if(!_activeBulletList[i].Visible)
		// 	{
		// 		indicesToRemove.Add(i);
		// 		_inactiveBulletQueue.Enqueue(_activeBulletList[i]);
		// 	} 
		// }
		// int indexShift = 0;
		// foreach(int i in indicesToRemove)
		// {
		// 	_activeBulletList.RemoveAt(i - indexShift);
		// 	indexShift++;
		// }
	}

	private void HandleFiring()
	{
		if (!_inputManager.LeftClickHeld()) return;
		//if timer isn't running you can shoot
		if (!_fireTimer.IsStopped()) return;
		
		if (_bulletPool.Count == 0)
		{
			Debug.Print("empty, creating new bullet");
			CreateNewBullet();
			return;
		}
		FireBullet();
		_fireTimer.Start();
		// Node2D activeBulletNode = _inactiveBulletQueue.Dequeue();
		// BaseBullet activeBullet = activeBulletNode as BaseBullet;
		// _activeBulletList.Add(activeBulletNode);
		// activeBullet?.ActivateBullet();
		// activeBullet?.SetDirection((_inputManager.GetMousePosition() - _owner.Position).Normalized());
	}
}