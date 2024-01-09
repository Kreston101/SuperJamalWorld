using Godot;
using System;
using static System.Formats.Asn1.AsnWriter;

public partial class UI : CanvasLayer
{
	private Node2D gm;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		gm = (Node2D)GetParent();
		HideButtons();
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	public void UpdateScore(int score)
	{
		GetNode<Label>("KillCounter").Text = score.ToString();
	}
	public void UpdateTime(float time)
	{
		GetNode<Label>("Timer").Text = time.ToString();
	}

	private void OnDamageUpgradePressed()
	{
		GameManager gmScript = gm as GameManager;
		gmScript.UpgradePlayer(0);
		GD.Print("damage up");
	}

	private void OnSpeedUpgradePressed()
	{
		GameManager gmScript = gm as GameManager;
		gmScript.UpgradePlayer(1);
		GD.Print("speed up");
	}

	private void OnFireRateUpgradePressed()
	{
		GameManager gmScript = gm as GameManager;
		gmScript.UpgradePlayer(2);
		GD.Print("brrrrrrt");
	}

	public void ShowButtons()
	{
		GetNode<Button>("Damage+").Show();
		GetNode<Button>("Speed+").Show();
		GetNode<Button>("Fire rate+").Show();
		GD.Print("shown buttons");
	}

	public void HideButtons()
	{
		GetNode<Button>("Damage+").Hide();
		GetNode<Button>("Speed+").Hide();
		GetNode<Button>("Fire rate+").Hide();
		GameManager gmScript = gm as GameManager;
		gmScript.buffWindowUp = false;
		GD.Print("hidden buttons");
	}
}
