using Godot;
using System;

public partial class ShipLog : Label
{
	// Called when the node enters the scene tree for the first time.
	private SpaceShip ship;
	public override void _Ready()
	{
		ship = (SpaceShip) GetNode("SpaceShip");
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		Text = "Coordinates: " + ship.Position.ToString() + "\n";
		Text += "Velocity: " + ship.Velocity.ToString() + "\n";
	}
}
