using Godot;
using System.Collections.Generic;
using System;
using System.Threading;


public partial class Galaxy : Node2D
{


	[Export]
	public int planetCount=2;
	[Export] public Vector2 limits = new Vector2(1000,5000); //limits_x is the minimum radius, limits_y is the maximum radius of spawning from the center

	[Export] public float meanMass;
	[Export] public float massDeviation;
	[Export] public float sunDirection;

	[Export] public float sunMass;
	[Export] public ulong seed=0;
	private RandomNumberGenerator rng;

	private SpaceShip ship =  null;
	private Star sun = null;

	[Export]public float G;
	
	public List<IGravityBody> Bodys = new List<IGravityBody>();
	public override void _Ready()
	{
		rng = new RandomNumberGenerator();
		
		if (seed == 0)
		{
			rng.Randomize();
			seed = rng.Seed;
		}
		else			
			rng.Seed = seed;
	
			
		
		if (ship == null)
		{
			//Find in sceneTree
			ship = (SpaceShip) GetNode("/root/Space/SpaceShip");
			if (ship == null)
				GD.Print("Ship not found");
			else
			{
				GD.Print("Ship found");
				ship.setGravityConstant(G);
				Bodys.Add(ship);
			}
				
		}
		addBodys(planetCount);
		createStar();
		
		
	}

	private void createStar()
	{
		var starScene = GD.Load<PackedScene>("res://Planets/star.tscn");
		sun = starScene.Instantiate<Star>();
		sun.setGravityMass(sunMass);
		sun.setDirection(sunDirection);
		
		AddChild(sun);
		Bodys.Add(sun);

	}

	private void addBodys(int countPlanets)
	{
		for (int i = 0; i < countPlanets; i++)
		{
			createPlanet();
		}
	}

	private Vector2 positionRadius()
	{
		//returns a random position from a circle with radius R
		//such as radiusX < R < radiusY
		
		var length = rng.RandfRange(limits.X, limits.Y);
		Vector2 position = new Vector2(0,length);
		return position.Rotated( rng.RandfRange(0, 2 * Mathf.Pi));
	
	}
	private void createPlanet()
	{
		var planetScene = GD.Load<PackedScene>("res://Planets/planet.tscn");
		var planet = planetScene.Instantiate<Planet>();
		//Set a random position in limit to -limit
				
		Vector2 planetPos = positionRadius();
		planet.setPosition(planetPos);

		//set a random mass
		planet.setGravityMass((float)GD.Randfn((double) meanMass,(double) massDeviation) );
		planet.setGravityConstant(G);
			
		AddChild(planet);
		//store the planet in the list
		Bodys.Add(planet);
	}


	private void calculateGravity(int i, int j)
	{
		//Calculates gravity between two planets in the list
		//a	nd returns a vector2 with the force to apply to the planet.
		
		IGravityBody body1 = Bodys[i];
		IGravityBody body2 = Bodys[j];

		body1.applyGravityForce(body2.getGravityPosition(), body2.getGravityMass());
		body2.applyGravityForce(body1.getGravityPosition(), body1.getGravityMass());
		
		
		
		return;
	}

	private void updateGravity()
	{
		for (int i = 0; i < Bodys.Count; i++)
		{
			for (int j = i+1; j < Bodys.Count; j++)
			{
				if (i == j)
					continue;
				calculateGravity(i, j);
			}
		}
		
	
	}
	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		updateGravity();
		
	}
}
