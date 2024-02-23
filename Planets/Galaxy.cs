using Godot;
using System.Collections.Generic;
using System;

public partial class Galaxy : Node2D
{
	[Export]
	public int planetCount=2;
	
	public override void _Ready()
	{
		for (int i = 0; i < planetCount; i++)
		{
			createPlanet();
		}
	}

	private void createPlanet()
	{
		var planetScene = GD.Load<PackedScene>("res://Planet/Planet.tscn");
		var planet = planetScene.Instantiate<Planet>();
		
		AddChild(planet);
		
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		
		
	}
}
