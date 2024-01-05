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
	public bool hasBuff = false;

	[Export] private PackedScene bulletFab;
	[Export] private Marker2D bulletSpawn;
	private float timer = 0f;
	private float buffTimer = 0f;
	[Export] private float maxBuffTime = 5f;

	public int damageBuff = 0;
	public float speedBuff = 0f;
	public float fireRateBuff = 0f;

	public override void _Ready()
	{
		health = maxHealth;
	}

	public override void _PhysicsProcess(double delta)
	{
		timer += (float)delta;

		if(hasBuff && buffTimer <= maxBuffTime)
		{
			buffTimer += (float)delta;
		}

		if(buffTimer >= maxBuffTime)
		{
			buffTimer = 0f;
			hasBuff = false;
			damageBuff = 0;
			speedBuff = 0;
			fireRateBuff = 0;
			GD.Print("buff ended");
		}

		Vector2 velocity = new Vector2();
		if (Input.IsActionPressed("Up"))
		{
			velocity.Y = -1;
		}
		if (Input.IsActionPressed("Down"))
		{
			velocity.Y = 1;
		}
		if (Input.IsActionPressed("Left"))
		{
			velocity.X = -1;
		}
		if (Input.IsActionPressed("Right"))
		{
			velocity.X = 1;
		}

		Position += velocity * (speed + speedBuff) * (float)delta;

		if (Input.IsActionPressed("Shoot") && timer >= fireDelay - fireRateBuff)
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
		bulletScript.damage = damage + damageBuff;
		bulletScript.direction = (GetGlobalMousePosition() - bullet.GlobalPosition).Normalized();
	}

	public void TakeDamage(int damageRecieved)
	{
		GD.Print("le test screaming");
		health -= damageRecieved; 
	}
}
