using Godot;
using System;

public partial class BulletFab : Area2D
{
	public float speed;
	public float damage;
	public Vector2 direction;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
