using Godot;
using System;
using System.Diagnostics;

public partial class DamageTaker : Node
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	public void HandleDamage(Hitbox hitbox)
	{
		Debug.Print("HANDLING DAMAGE");
	}
}
