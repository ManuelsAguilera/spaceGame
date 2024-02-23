using Godot;
using System;

public partial class Planet : StaticBody2D
{
	// Called when the node enters the scene tree for the first time.
	[Export]
	public float Mass=1;
	
	[Export]
	public float size=1;
	
	public float G=6.674f;
	private Vector2 addedForce;

	public override void _Ready()
	{
		addedForce = new Vector2(0,0);
		Scale = new Vector2(size, size);
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	/*public void addForce( Mass, distance,Vector2 direction)
	{
		addedForce = (G*Mass*Mass)/distance*distance*direction;
		Velocity+=addedForce;
	}*/
	
}
