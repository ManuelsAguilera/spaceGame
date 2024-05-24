using Godot;
using System;
/*
 
 */

public partial class SpaceShip : CharacterBody2D,IGravityBody

{
    [Export]
	float Speed = 10;

	[Export] public float Mass = 5000f;

	private float G;
	private AnimatedSprite2D _AnimSprite2D;

	private Vector2 inputVector;

	public override void _Ready()
	{
		_AnimSprite2D = GetNode<AnimatedSprite2D>("AnimatedSprite2D");
		// Called every time the node is added to the scene.
		
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
		if (Velocity.DistanceTo(new Vector2()) < 2000 )
		{
			Velocity += inputVector * Speed;
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
		if (Position.X > 1e4f)
		{
			Position = new Vector2(-1e4f,Position.Y);
		}
		if (Position.X < -1e4f)
		{
			Position = new Vector2(1e4f,Position.Y);
		}
		if (Position.Y > 1e4f)
		{
			Position = new Vector2(Position.X,-1e4f);
		}
		if (Position.Y < -1e4f)
		{
			Position = new Vector2(Position.X,1e4f);
		}
	}

	//Gravity:
	private void addForce(Vector2 force)
	{
		//Calculate velocity based on the force applied
		Velocity += force/Mass;
	}
	//Interface Gravity
	public void setGravityConstant(float g)
	{
		G =g;
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



	public override void _Process(double delta)
	{
		outOfBounds();
		// Called every frame. 'delta' is the elapsed time since the previous frame.
	}
	public override void _PhysicsProcess(double delta)
	{
		//GD.Print(Velocity);
		_Input();
		checkMovement();
		
		checkRotation();
		
		MoveAndSlide();
		// Called every time the node is added to the scene.
		// Initialization here
	}

}
