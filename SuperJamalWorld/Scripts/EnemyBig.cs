using Godot;
using System;

public partial class EnemyBig : CharacterBody2D
{
	[Export] public int maxHealth;
	public int health;
	[Export] public int damage;
	[Export] public float speed;

	private CharacterBody2D player;
	private Vector2 direction;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		health = maxHealth;
		player = (CharacterBody2D)GetParent().GetNodeOrNull("TestChar");
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		direction = (player.GlobalPosition - GlobalPosition).Normalized();
		Position += direction * speed * (float)delta;

		if (health <= 0)
		{
			QueueFree();
		}
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
