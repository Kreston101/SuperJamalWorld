using Godot;
using System;

public partial class TestChar : CharacterBody2D
{
	[Export] public float speed = 300.0f;
	[Export] public int bulletsPerShot = 1;
	[Export] public int damage = 1;
	[Export] public float fireDelay = 0.5f;
	[Export] public int maxHealth = 10;
	public int health;

	[Export] private PackedScene bulletFab;
	[Export] private Marker2D bulletSpawn;
	private float timer = 0f;

	public override void _Ready()
	{
		health = maxHealth;
	}

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
		BulletFab bulletScript = bullet as BulletFab;
		bulletScript.damage = damage;
		bulletScript.direction = (GetGlobalMousePosition() - bullet.GlobalPosition).Normalized();
	}

	public void TakeDamage(int damageRecieved)
	{
		GD.Print("le test screaming");
		health -= damageRecieved; 
	}
}
