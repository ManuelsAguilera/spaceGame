using Godot;
using System;

public partial class AudioStreamPlayer : Godot.AudioStreamPlayer
{
	// Called when the node enters the scene tree for the first time.
	AudioStream hurtSound;
	AudioStream explosionSound;
	public override void _Ready()
	{
		hurtSound = (AudioStream)ResourceLoader.Load("res://Player/Sounds/hurt.mp3");
		explosionSound = (AudioStream)ResourceLoader.Load("res://Player/Sounds/explosion.mp3");
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	public void playHurt()
	{
		this.Stream = hurtSound;
		this.Play();

	}

	public void playDeath()
	{
		this.Stream = explosionSound;
		this.Play();
	}
}
