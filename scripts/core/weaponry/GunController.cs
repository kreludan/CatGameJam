using Godot;
using System.Collections.Generic;
using System.Diagnostics;

public partial class GunController : Node2D
{
	[Export]
	private Node2D owner;
	[Export]
	private float ownerDistanceConstant;
	[Export] 
	private int frameIntervalBetweenBullets;
	
	private InputManager _inputManager;
	private Queue<Node2D> inactiveBulletQueue;
	private List<Node2D> activeBulletList;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		inactiveBulletQueue = new Queue<Node2D>();
		activeBulletList = new List<Node2D>();
		_inputManager = GetNode<InputManager>("/root/InputManager");
		var bulletScene = GD.Load<PackedScene>("res://assets/scenes/base_bullet.tscn");
		for (int i = 0; i < 100; i++)
		{
			Node2D bulletNode = (Node2D)bulletScene.Instantiate();
			Node2D bulletNodeContainer = (Node2D) GetTree().GetNodesInGroup("PlayerBulletPool")[0];
			bulletNodeContainer.AddChild(bulletNode);
			BaseBullet bullet = bulletNode as BaseBullet;
			bullet?.DeactivateBullet();
			inactiveBulletQueue.Enqueue(bulletNode);
		}
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		ReorientGunAndBullets();
		HandleFiring();
	}
	
	// Moves the gun (and the queue of inactive bullet objects) to the new location based on mouse position
	private void ReorientGunAndBullets()
	{
		Vector2 playerToMouse = (_inputManager.GetMousePosition() - owner.Position).Normalized();
		Position = owner.Position + (playerToMouse * ownerDistanceConstant);
		Rotation = new Vector2(1, 0).AngleTo(playerToMouse);
		
		foreach (Node2D inactiveBullet in inactiveBulletQueue)
		{
			inactiveBullet.Position = Position;
		}

		foreach (Node2D activeBullet in activeBulletList)
		{
			if (!activeBullet.Visible)
			{
				activeBulletList.Remove(activeBullet);
				inactiveBulletQueue.Enqueue(activeBullet);
			}
		}
	}

	private void HandleFiring()
	{
		if (_inputManager.LeftClickPressed())
		{
			Debug.Print("held");
			if (inactiveBulletQueue.Count == 0)
			{
				Debug.Print("empty");
				return;
			}
			Node2D activeBulletNode = inactiveBulletQueue.Dequeue();
			BaseBullet activeBullet = activeBulletNode as BaseBullet;
			activeBullet?.ActivateBullet();
			activeBullet?.SetDirection((_inputManager.GetMousePosition() - owner.Position).Normalized());
		}
		// if the mouse is held down, dequeue a bullet from the inactive bullet queue
		// and make it visible and set process to true
		// and give it a velocity
		
	}


}
