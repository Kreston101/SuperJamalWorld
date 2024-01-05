using Godot;
using System;
using System.Collections.Generic;

public partial class GameManager : Node2D
{
	public CharacterBody2D player;
	[Export] public PackedScene enemyFab;
	[Export] public PackedScene shooterFab;
	[Export] public PackedScene bigEnemyFab;
	[Export] public float spawnDistance;
	[Export] public float spawnDelay;
	[Export] public int waveSize;
	public int killCount = 0;
	public float timeInGame = 0f;

	private float timer = 0;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		player = (CharacterBody2D)GetParent().GetNodeOrNull("TestChar");
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		timeInGame += (float)delta;
		timer += (float)delta;

		if (timer >= spawnDelay)
		{
			for (int i = 0; i < waveSize; i++)
			{
				SpawnEnemy();
			}
			timer = 0;
		}
	}

	private void SpawnEnemy()
	{
		RandomNumberGenerator rng = new RandomNumberGenerator();
		int whatToSpawn = rng.RandiRange(0, 9); //0-1 Big; 2-4 Shooter; rest normal 
		if (whatToSpawn <= 4) 
		{
			Node2D enemy = (Node2D)enemyFab.Instantiate();
			AddChild(enemy);
			//enemy.Position += (GetGlobalMousePosition() - player.GlobalPosition).Normalized() * spawnDistance;
		}
		else if (whatToSpawn > 4 && whatToSpawn <= 7) 
		{
			Node2D enemy = (Node2D)shooterFab.Instantiate();
			AddChild(enemy);
			//enemy.Position += (GetGlobalMousePosition() - player.GlobalPosition).Normalized() * spawnDistance;
		}
		else if (whatToSpawn > 7 && whatToSpawn <= 9)
		{
			Node2D enemy = (Node2D)bigEnemyFab.Instantiate();
			AddChild(enemy);
			//enemy.Position += (GetGlobalMousePosition() - player.GlobalPosition).Normalized() * spawnDistance;
		}
	}
}
