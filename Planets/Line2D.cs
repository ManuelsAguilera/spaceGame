using Godot;
using System;

public partial class Line2D : Godot.Line2D
{
	// Called when the node enters the scene tree for the first time.

	private double time_elapsed = 0;
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{

		if (time_elapsed < 1)
			time_elapsed+=delta*10;
		else{
				time_elapsed = 0;
				var star = (Sprite2D) GetNode("Sprite2D");
				GD.Print("Star position: ", star.Position);

				this.AddPoint(star.Position);
				
			}
		
		
	}
}
