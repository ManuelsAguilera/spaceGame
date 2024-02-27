using Godot;
using System;

public partial class Beta_game : Button
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

    public override void _Pressed()
    {
        
		var scene = GD.Load<PackedScene>("res://space/space.tscn");
		GD.Print(scene == null);
		GetTree().ChangeSceneToPacked(scene);
    }
    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(double delta)
	{
	}
}
