using Godot;
using System;

public partial class TestPlanets : Button
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

	public override void _Pressed()
	{
		GD.Print("Pressed");
		var scene = GD.Load<PackedScene>("res://other/planets_test.tscn");
		GD.Print(scene == null);
		GetTree().ChangeSceneToPacked(scene);
	}
	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
