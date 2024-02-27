using Godot;
using System;

public partial class Star : RigidBody2D
{
	// Called when the node enters the scene tree for the first time.
	private double rotation = 0;
	private double time_elapsed = 0;
	private float direction=1;

	
	public override void _Ready()
	{
		//default mass
		Mass = 5e7f;
		
	}

	public void setDirection(float dir)
	{
		if (dir >=0)
			direction = 1;
		else
			direction = -1;
	}
	public void setMass(float mass)
	{
		Mass =  mass ;
	}
	public Vector2 getPosition()
	{
		return new Vector2(Position.X, Position.Y);
	}
	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		//Calculate magnitude of the force
		float magnitude = 50e8f;
		var force = new Vector2(0,magnitude*direction);
		
		if (time_elapsed < 1)
			time_elapsed+=delta;
		else{
				time_elapsed = 0;

				if (rotation < Math.PI)
					rotation+=Math.PI/36;
				else
					rotation = -Math.PI;
			}
		force =force.Rotated((float) rotation);
		
		ApplyForce(force);
		
	}
}
