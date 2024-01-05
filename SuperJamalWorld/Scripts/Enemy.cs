using Godot;
using System;

public partial class Enemy : CharacterBody2D
{
	[Export] public int maxHealth;
	public int health;
	[Export] public int damage;
	[Export] public float speed;

	private GameManager gm;
	private CharacterBody2D player;
	private Vector2 direction;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		gm = (GameManager)(Node2D)GetParent().GetScript();
		health = maxHealth;
		player = (CharacterBody2D)GetParent().GetNodeOrNull("TestChar");
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		direction = (player.GlobalPosition - GlobalPosition).Normalized();
		Position += direction * speed * (float)delta;

		if(health <= 0)
		{
			gm.killCount++;
			QueueFree();
		}
	}

	private void OnBodyEnteredSig(Node2D body)
	{
		if(body == player)
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
