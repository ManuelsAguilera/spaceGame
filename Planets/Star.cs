using Godot;
using System;

public partial class Star : RigidBody2D, IGravityBody
{
	// Called when the node enters the scene tree for the first time.
	private double rotation = 0;
	private double time_elapsed = 0;
	private float direction=1;

	
	public override void _Ready()
	{		
	}

	//Interface methods:
	public void setGravityConstant(float G)
	{
		//Not used in this example
	}

	public void setGravityMass(float mass)
	{
		Mass = mass;
	}

	public float getGravityMass()
	{
		return Mass;
	}

	public Vector2 getGravityPosition()
	{
		return new Vector2(Position.X, Position.Y);
	}

	public void applyGravityForce(Vector2 position, float force)
	{
		//A star does not get atracted to other objects.
	}

	public void setDirection(float direction)
	{
		this.direction = direction;
	}
	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		//Calculate magnitude of the force
		float magnitude = 1e-8f;
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
