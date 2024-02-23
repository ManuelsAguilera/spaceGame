using Godot;
using System;
/*
 
 */

public partial class SpaceShip : CharacterBody2D

{
    [Export]
	int Speed = 10;

	private float G = 6.67430e-11f;

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
		inputVector = new Vector2();
		if (Input.IsActionPressed("ui_right"))
		{
			inputVector.X += 1;
		}
		if (Input.IsActionPressed("ui_left"))
		{
			inputVector.X -= 1;
		}
		if (Input.IsActionPressed("ui_down"))
		{
			inputVector.Y += 1;
		}
		if (Input.IsActionPressed("ui_up"))
		{
			inputVector.Y -= 1;
		}
	
	}
	private void checkMovement()
	{
		//get the input
		if (Velocity.X <300 || Velocity.Y <300)
		{
			Velocity += inputVector * Speed;
		}
		//If with air,
		Velocity -= (inputVector * Speed)/3;
		
		
		//GD.Print(Velocity);
		
		
	}
	protected void checkRotation()
	{
		LookAt(GetGlobalMousePosition());
		Velocity += Transform.X * Input.GetAxis("down", "up") * Speed;
	}

	public void addForce(float Mass, float distance, Vector2 direction)
	{
		//add the force to the velocity
		Velocity += (G * Mass * Mass) / distance * distance * direction;
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
