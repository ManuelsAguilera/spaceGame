using Godot;
using System;

public partial class FpsMeter : Label
{
	// Called when the node enters the scene tree for the first time.
	private SpaceShip ship;
	private double time_elapsed = 0;
	private Vector2 shipPosition;
	private Vector2 shipVelocity;
	public override void _Ready()
	{
		ship = (SpaceShip) GetNode("/root/Space/SpaceShip");
		GD.Print("ship "+ (ship !=null));

		//add position and velocity
		shipPosition = ship.Position;
		shipVelocity = ship.Velocity;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		var fps = Engine.GetFramesPerSecond();
		Text = "FPS: " + fps.ToString();

		if (time_elapsed < 0.5)
			time_elapsed+=delta;
		else{ //update the position and velocity of the ship
			shipPosition = new Vector2(MathF.Round(ship.Position.X,0) , MathF.Round(ship.Position.Y,0));
			shipVelocity = new Vector2(MathF.Round(ship.Velocity.X,0) , MathF.Round(ship.Velocity.Y,0));
			time_elapsed = 0;
			}
		Text += "\nCoordinates: " + shipPosition.ToString() + "\n";
		Text += "Velocity: " + shipVelocity.ToString() + "\n";
		
	}
}
