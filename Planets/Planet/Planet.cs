using Godot;
using System;


public partial class Planet : RigidBody2D, IGravityBody,ICanBeDamaged
{
	
	//Stores the gravity force applied by external bodies.
	[Export]public Vector2 gravityForce;

	private float G;
	private Vector2 scale;

	[Export] public ulong maxHealthPoints;
	[Export] public long healthPoints;

	private Boolean invulnerable = false;
	private double invulnerableTime = 0.5;
	private double invulnerableTimeElapsed = 0;


	public override void _Ready()
	{
		//First code to be executed
		
		
		gravityForce = new Vector2(0,0);
		addTangencialVelocity();
		maxHealthPoints = (ulong)Mass /10;
		healthPoints = (long)maxHealthPoints;

	}
	


	public void setGravityConstant(float G)
	{
		this.G = G;
	}
	public void setGravityMass(float mass)
	{
		if (mass <= 0){
			Mass = 500;
			return;
		}
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
	public void addTangencialVelocity(float Magnitude)
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
		if (healthPoints <= 0)
			killSelf();
		
		if (invulnerable){
			invulnerableTimeElapsed+=delta;
			if (invulnerableTimeElapsed >= invulnerableTime){
				invulnerable = false;
				invulnerableTimeElapsed = 0;
			}
		}


		return;
	}

	//DamageInterface
    public void addDamage(ulong damage)
    {
		if (invulnerable)
			return;
		setGravityMass(Mass - (float)damage);
		invulnerable = true;
		healthPoints -= (long)damage;
		
    }

    public void removeDamage(ulong damage)
    {
		healthPoints += (long)damage;
    }

    public void killSelf()
    {
		
        QueueFree();
    }

	public bool isDead()
	{
		return (healthPoints <= 0);
	}
    public void healToMaximum()
    {
        healthPoints = (long)maxHealthPoints;
    }

    public void setInvulnerable(bool invulnerable)
    {
        invulnerable=true;
    }

    public bool isInvulnerable()
    {
        return invulnerable;
    }

}
