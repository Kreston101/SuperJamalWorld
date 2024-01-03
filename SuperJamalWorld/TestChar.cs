using Godot;
using System;

public partial class TestChar : CharacterBody2D
{
	public float speed = 300.0f;
	public int bulletsPerShot = 1;
	public float damage = 1f;
	public float fireDelay = 0.5f;
	public int maxHealth = 10;
	public int currentHealth = 10;

	[Export] private PackedScene bulletFab;
	[Export] private Marker2D bulletSpawn;
	private float timer = 0f;

	public override void _PhysicsProcess(double delta)
	{
		timer += (float)delta;

		Vector2 velocity = new Vector2();
		if (Input.IsActionPressed("Up"))
		{
			velocity.Y = -1 * speed;
		}
		if (Input.IsActionPressed("Down"))
		{
			velocity.Y = 1 * speed;
		}
		if (Input.IsActionPressed("Left"))
		{
			velocity.X = -1 * speed;
		}
		if (Input.IsActionPressed("Right"))
		{
			velocity.X = 1 * speed;
		}

		Position += velocity * (float)delta;

		if (Input.IsActionPressed("Shoot") && timer >= fireDelay)
		{
			Shoot();
			timer = 0;
		}

		LookAt(GetGlobalMousePosition());
	}

	public void Shoot()
	{
		Node2D bullet = (Node2D)bulletFab.Instantiate();
		AddSibling(bullet);
		bullet.Position = bulletSpawn.GlobalPosition;
	}
}
