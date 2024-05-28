using Godot;
using System;

public partial class GameOver : Control
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	public void _on_quit_pressed()
	{
		GetTree().Quit();
	}

	public void _on_menu_pressed()
	{
		var scene = GD.Load<PackedScene>("res://menus/MainMenu/menu_principal.tscn");
		GD.Print(scene == null);
		GetTree().ChangeSceneToPacked(scene);
		
	}
	
	public void _on_try_again_pressed()
	{
		var scene = GD.Load<PackedScene>("res://Space/space.tscn");
		GD.Print(scene == null);
		GetTree().ChangeSceneToPacked(scene);
	}
	

}
