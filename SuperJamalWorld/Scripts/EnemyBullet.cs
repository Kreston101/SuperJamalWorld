using Godot;
using System;

public partial class EnemyBullet : Area2D
{
	[Export] public float speed;
	public int damage;
	public Vector2 direction;
	[Export] public float maxLifetime = 1f;

	private float lifetime = 0f;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		Position += direction * speed * (float)delta;
		lifetime += (float)delta;
		LookAt(direction);
		if (lifetime > maxLifetime)
		{
			QueueFree();
		}
	}

	private void OnBodyEnteredSig(Node2D body)
	{
		if (body.IsInGroup("Player"))
		{
			TestChar playerScript = body as TestChar;
			playerScript.TakeDamage(damage);
			GD.Print("hit player!");
			QueueFree();
		}
	}
}
