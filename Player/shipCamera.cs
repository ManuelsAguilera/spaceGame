using Godot;
using System;

public partial class shipCamera : Godot.Camera2D
{
// Called when the node enters the scene tree for the first time.
	private CharacterBody2D  ship;
	public override void _Ready()
	{
		ship = GetNode<CharacterBody2D>("../SpaceShip");
		Position = ship.GlobalPosition;
		if (ship == null)
			GD.PrintErr("Character not found");
		else
			GD.Print("Character found");
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		
		Vector2 charPos = ship.Position;
		//go to chPos
		Position = charPos;
		//GD.Print(Position);
		
		}
}
