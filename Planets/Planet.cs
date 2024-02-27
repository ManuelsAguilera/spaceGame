using Godot;
using System;


public partial class Planet : RigidBody2D
{
	
	//Stores the gravity force applied by external bodies.
	[Export]public Vector2 gravityForce;

	
	private Vector2 scale;

	public override void _Ready()
	{
		//
		scale=Scale*Mass/500		;
		gravityForce = new Vector2(0,0);
		addTangencialVelocity();

	}
	public void setMass(float mass)
	{
		Mass =  mass ;
		//make the scale of the planet proportional to the mass
		scale = new Vector2(MathF.Log2(mass)/5,MathF.Log2(mass)/5);
		
		var sprite = (Sprite2D)GetNode("PlanetSprite");
		sprite.Scale = scale;
		
	}

	public void setPosition(Vector2 position)
	{
		Position = position;
	}
	public Vector2 getPosition()
	{
		return new Vector2(Position.X, Position.Y);
	}

	public void setGravityForce(Vector2 force)
	{
		
		
		gravityForce=force;
				
	}
	
	
	public void addTangencialVelocity()
	{
		Vector2 directionToSun = Position.Normalized();
		var normal = directionToSun.Rotated((float) GD.RandRange(-1,1) *Mathf.Pi/2);
		var tangencialForce = normal * 5e3f;
		ApplyImpulse(tangencialForce);
	}
    public override void _IntegrateForces(PhysicsDirectBodyState2D  state)
	{
		
		state.ApplyCentralImpulse(gravityForce);
		
	}
    

	//Make sure the object does not go out of the screen.
	public override void _Process(double delta)
	{
		
		
		return;
	}

}
