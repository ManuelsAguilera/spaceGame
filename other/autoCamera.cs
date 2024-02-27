using Godot;
using System;

public partial class autoCamera : Camera2D
{
	// Called when the node enters the scene tree for the first time.
	private Vector2 inputVector= new Vector2(0,0);
	private float speed = 30f;
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public void input()
	{
		//get the input
		
		if (Input.IsActionPressed("right"))
		{
			inputVector.X = 1;
		}
		else if (Input.IsActionPressed("left"))
		{
			inputVector.X = -1;
		}
		else if (Input.IsActionPressed("down"))
		{
			inputVector.Y = 1;
		}
		else if (Input.IsActionPressed("up"))
		{
			inputVector.Y = -1;
		}
		else
		{
			inputVector = new Vector2(0,0);
		}
		
		//zoom
		/*
		if (Input.IsActionPressed("zoom_in"))
		{
			Zoom.X =1;
		}
		if (Input.IsActionPressed("zoom_out"))
		{
			Zoom -= 0.1f;
		}*/
	}
	public override void _Process(double delta)
	{
		input(	);
		if (inputVector != new Vector2(0,0))
			Position = Position + inputVector*speed;
		else 
			Position = Position;
	}
}
