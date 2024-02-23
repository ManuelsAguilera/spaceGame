using Godot;
using System;

public partial class space : Node2D
{
	
	public override void _Ready()
	{
		GD.Print("Hello World");
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
