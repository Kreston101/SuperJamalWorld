using Godot;
using System;
using static System.Net.Mime.MediaTypeNames;

public partial class Shooter : CharacterBody2D
{
	[Export] public float speed;
	[Export] public int damage;
	[Export] public float fireDelay;
	[Export] public int maxHealth;
	public int health;

	[Export] private PackedScene bulletFab;
	[Export] private Marker2D bulletSpawn;
	private float timer = 0f;
	private CharacterBody2D player;
	private Vector2 direction;
	private GameManager gm;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		gm = (GameManager)GetParent().GetScript();
		health = maxHealth;
		player = (CharacterBody2D)GetParent().GetNodeOrNull("TestChar");
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		timer += (float)delta;
		if (Position.DistanceTo(player.Position) > 300f)
		{
			direction = (player.GlobalPosition - GlobalPosition).Normalized();
			Position += direction * speed * (float)delta;
		}
		else if(timer >= fireDelay)
		{
			EnemyShoot();
			timer = 0;
		}

		LookAt(player.Position);

		if (health <= 0)
		{
			gm.killCount++;
			QueueFree();
		}
	}

	public void EnemyShoot() //create enemy bullet
	{
		Node2D bullet = (Node2D)bulletFab.Instantiate();
		AddSibling(bullet);
		bullet.Position = bulletSpawn.GlobalPosition;
		EnemyBullet bulletScript = bullet as EnemyBullet;
		bulletScript.damage = damage;
		bulletScript.direction = (player.GlobalPosition - bullet.GlobalPosition).Normalized();
	}

	private void OnBodyEnteredSig(Node2D body)
	{
		if (body == player)
		{
			TestChar charScript = player as TestChar;
			charScript.TakeDamage(damage);
		}
	}

	public void TakeDamage(int damageRecieved)
	{
		GD.Print("recieved damage");
		health -= damageRecieved;
	}
}
