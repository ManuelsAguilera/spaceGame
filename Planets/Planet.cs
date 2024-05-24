using Godot;
using System;


public partial class Planet : RigidBody2D, IGravityBody
{
	
	//Stores the gravity force applied by external bodies.
	[Export]public Vector2 gravityForce;

	private float G;
	private Vector2 scale;


	public override void _Ready()
	{
		//First code to be executed
		scale=Scale*Mass/500		;
		gravityForce = new Vector2(0,0);
		addTangencialVelocity();

	}
	


	public void setGravityConstant(float G)
	{
		this.G = G;
	}
	public void setGravityMass(float mass)
	{
		Mass =  mass ;
		//make the scale of the planet proportional to the mass
		//this.setScale(); todo	
	}
	public float getGravityMass()
	{
		return Mass;
	}
	public Vector2 getGravityPosition()
	{
		return new Vector2(Position.X, Position.Y);
	}

	public void applyGravityForce(Vector2 position, float mass)
	{
		gravityForce= new Vector2(0,0);
		//Calculate the direction of the force
		Vector2 direction = position - Position;
		//Calculate the magnitude of the force
		float distance = direction.Length();
		//Normalize the direction
		direction = direction.Normalized();
		//Calculate the force
		gravityForce += direction* (mass) / (distance);
		gravityForce = gravityForce*G;
		
	}

	public void setPosition(Vector2 position)
	{
		Position = position;
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
		//GD.Print("Integrate Forces",state.LinearVelocity);
		state.ApplyCentralImpulse(gravityForce);
		
	}
    

	//Make sure the object does not go out of the screen.
	public override void _Process(double delta)
	{
		
		return;
	}

}
