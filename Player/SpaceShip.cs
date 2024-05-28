using Godot;
using System;
/*
 
 */

public partial class SpaceShip : CharacterBody2D,IGravityBody,ICanBeDamaged

{
    [Export]
	public const float Acceleration = 5f;
	
	[Export] public float Mass = 1200f;

	private float G;
	private AnimatedSprite2D sprite;
	private AudioStreamPlayer sounds; 
	private Vector2 inputVector;
	[Export]public Vector2 gravityForce;

	[Export] public const ulong maxHealth =200;
	[Export] public long healthPoints;
	private Boolean dead = false;
	private Boolean invulnerable = false;
	private double invulnerableTime = 0.7;
	private double invulnerableTimeElapsed = 0;

	public override void _Ready()
	{
		sprite = this.GetNode<AnimatedSprite2D>("ShipSprite");
		sounds = this.GetNode<AudioStreamPlayer>("soundPlayer");
		// Called every time the node is added to the scene.
		healthPoints= (long)maxHealth;
	}

	public override void _Process(double delta)
	{
		if (dead)
			return;
		outOfBounds();

		if (invulnerable){
			invulnerableTimeElapsed += delta;
			if (invulnerableTimeElapsed > invulnerableTime)
			{
				invulnerable = false;
			}
		}

		// Called every frame. 'delta' is the elapsed time since the previous frame.
	}

	void _Input()
	{
		//get the input
		inputVector = new Vector2(0,0);
		if (Input.IsActionPressed("right"))
		{
			inputVector.X += 1;
		}
		if (Input.IsActionPressed("left"))
		{
			inputVector.X -= 1;
		}
		if (Input.IsActionPressed("down"))
		{
			inputVector.Y += 1;
		}
		if (Input.IsActionPressed("up"))
		{
			inputVector.Y -= 1;
		}
	
	}
	private void checkMovement()
	{
		//get the input
		if (Velocity.DistanceTo(new Vector2()) < 4000 )
		{
			Velocity += inputVector * Acceleration;
		}
		
		
	}
	protected void checkRotation()
	{
		LookAt(GetGlobalMousePosition());
		//Velocity += Transform.X * Input.GetAxis("down", "up") * Speed;
	}

	
	

	private void outOfBounds()
	{
		//Make sure the object does not go out of the screen.
		float border = 1e8f;
		
		if (Position.X > border) killSelf();

		if (Position.X < -border)killSelf();

		if (Position.Y > border)killSelf();

		if (Position.Y < -border)killSelf();
	}

	//Gravity:
	private void addForce(Vector2 force)
	{
		//Calculate velocity based on the force applied
		gravityForce =force;
		Velocity += force/Mass;
	}
	//Interface Gravity
	public void setGravityConstant(float g)
	{
		G =g/3;
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

	public void applyGravityForce(Vector2 position, float mass)
	{
		//Calculate the direction of the force
		Vector2 direction = position - Position;
		//Calculate the magnitude of the force
		float distance = direction.Length();
		//Normalize the direction
		direction = direction.Normalized();
		//Calculate the force
		float magnitude = (mass / (distance));
		
		Vector2 gravityForce = direction* magnitude;
		
		gravityForce = gravityForce * G;
		
		addForce(gravityForce);
		
	}
	public override void _PhysicsProcess(double delta)
	{

		if (healthPoints <= 0  && !dead)
			killSelf();

		if (dead)
			return;
		//I like to see the ship, just to stop having input
		//But see it dead still being atracted.
		_Input();
		checkMovement();
		
		checkRotation();

		if (inputVector.Length() > 0)
			playMovement();
		else
			playDefault();
	
		
		if (Velocity.Length() >= 4000)
		{
			this.Velocity = new Vector2();
			killSelf();
		}
			

		MoveAndSlide();
		// Called every time the node is added to the scene.
		// Initialization here
	}

	//Damage interface
    public void setInvulnerable(bool invulnerable)
    {
        this.invulnerable = invulnerable;
    }

    public bool isInvulnerable()
    {
        return invulnerable;
    }

    public void addDamage(ulong damage)
    {
        if (invulnerable || dead)
			return;
		

		healthPoints -= (long)damage;
		sounds.playHurt();
		
		
		invulnerable = true;
		invulnerableTimeElapsed = 0;
    }

    public void removeDamage(ulong damage)
    {
        healthPoints += (long)damage;
    }

    public void killSelf()
    {
		dead = true;
		sounds.playDeath();
    }

    public bool isDead()
    {
        return dead;
    }

    public void healToMaximum()
    {
        throw new NotImplementedException();
    }

	//Animation
	public void playMovement()
	{
		sprite.Play("Moving");
	}
	public void playDefault()
	{
		sprite.Play("default");
	}
}
