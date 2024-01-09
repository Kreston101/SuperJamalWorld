using Godot;
using System;

public partial class PowerPellet : Area2D
{
	public int damageBuff = 1;
	public float speedBuff = 100f;
	public float fireRateBuff = 0.1f;

	[Export] public bool isDamageBuff = false;
	[Export] public bool isSpeedBuff = false;
	[Export] public bool isFireRateBuff = false;

	private RandomNumberGenerator rng = new RandomNumberGenerator();
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		int randBuff = rng.RandiRange(0, 2);

		switch(randBuff)
		{
			case 0:
				isDamageBuff = true;
				break;
			case 1:
				isSpeedBuff = true;
				break;
			case 2:
				isFireRateBuff = true;
				break;
		}
	}

	private void OnBodyEnteredSig(Node2D body)
	{
		if (body.IsInGroup("Player"))
		{
			TestChar playerScript = body as TestChar;
			if (playerScript.hasBuff == false)
			{
				playerScript.hasBuff = true;

				if (isDamageBuff)
				{
					GD.Print("gave damage buff");
					playerScript.damageBuff = damageBuff;
				}
				else if (isSpeedBuff)
				{
					GD.Print("gave speedbuff");
					playerScript.speedBuff = speedBuff;
				}
				else if (isFireRateBuff)
				{
					GD.Print("gave fireratebuff");
					playerScript.fireRateBuff = fireRateBuff;
				}
			}
		}
		QueueFree();
	}
}
