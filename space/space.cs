using Godot;
using System;
using System.Xml;

public partial class space : Node2D
{
	// Called when the node enters the scene tree for the first time.
	SpaceShip ship;
	GameOver gameOver;
	Godot.AudioStreamPlayer music;
	public override void _Ready()
	{
		ship =  this.GetNode<SpaceShip>("SpaceShip");
		gameOver = this.GetNode<GameOver>("GameOver");
		music = this.GetNode<Godot.AudioStreamPlayer>("musicPlayer");
		gameOver.Visible = false;
		
		
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		if (music.Playing == false)
			music.Playing = true;

		if (ship.isDead() ^ gameOver.Visible)
			gameOver.Visible = true;
		
		if (gameOver.Visible)
			gameOver.Position = ship.Position+ new Vector2(-200,-150);
		

		
	}
}
