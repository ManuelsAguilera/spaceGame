using Godot;
using System.Collections.Generic;
using System;
using System.Threading;


public partial class Galaxy : Node2D
{


	[Export]
	public int planetCount=2;
	[Export] public float radiusX;
	[Export] public float radiusY;

	[Export] public float meanMass;
	[Export] public float massDeviation;
	[Export] public float sunDirection;

	[Export] public float sunMass;
	

	private SpaceShip ship =  null;
	private Star sun;

	[Export]public float G;
	
	public List<Planet> planets = new List<Planet>();
	public override void _Ready()
	{
		G = 1/G;
		//createStar();
		for (int i = 0; i < planetCount; i++)
		{
			createPlanet();
		}
		createStar();
		sun.setDirection(sunDirection);

		//Find the player, else set it as null

		ship = (SpaceShip) GetNode("/root/Space/SpaceShip");
		if (ship == null)
		{
			GD.Print("No player found");
		}
	}

	private void createStar()
	{
		var starScene = GD.Load<PackedScene>("res://Planets/star.tscn");
		sun = starScene.Instantiate<Star>();
		
		AddChild(sun);
		
	}

	private Vector2 positionRadius()
	{
		//returns a random position from a circle with radius R
		//such as radiusX < R < radiusY
		var rng = new RandomNumberGenerator();
		var length = rng.RandfRange(radiusX, radiusY);
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
		planet.setMass((float)GD.Randfn((double) meanMass,(double) massDeviation) );

			
		AddChild(planet);
		//store the planet in the list
		planets.Add(planet);
	}


	private Vector2 calculateGravity(int i, int j)
	{
		//Calculates gravity between two planets in the list
		//a	nd returns a vector2 with the force to apply to the planet.
		
		Vector2 Gforce = new Vector2(0,0);
		//get the distance between the planets
		var distance = ((Vector2) planets[j].getPosition()) - ((Vector2)planets[i].getPosition());
		
		float magnitude = distance.Length();

		if (magnitude < 1f)
			return Gforce;
		
		//calculate the force magnitude
		var GforceMagn = planets[i].Mass * planets[j].Mass * G;
		GforceMagn = GforceMagn / (magnitude * magnitude);
		//calculate the force direction
		Gforce = distance * GforceMagn;
		return Gforce;
	}

	//Calculate between a planet and the sun
	private Vector2 calculateGravity(int i)
	{
		//Calculates gravity between a planet and the sun
		//and returns a vector2 with the force to apply to the planet.
		
		Vector2 Gforce = new Vector2(0,0);
		//get the distance between the planets
		var distance = (Vector2)planets[i].getPosition() - (Vector2) sun.getPosition();
		
		if (distance.Length() < 20f)
			return Gforce;

		Gforce = G * distance.Normalized()*(-1) *(planets[i].Mass * sun.Mass);
		Gforce = Gforce / distance.Length();
		return Gforce;
	}

	private Vector2 calculateGravity(int i, SpaceShip ship)
	{
		//Calculates gravity for the ship based on the planet
		//and returns a vector2 with the force to apply to the ship.
		
		Vector2 Gforce = new Vector2(0,0);
		//get the distance between the planets
		var distance = (Vector2) ship.Position -(Vector2)planets[i].getPosition();
		
		if (distance.Length() < 20f)
			return Gforce;

		Gforce = G * distance.Normalized()*(-1) *(planets[i].Mass * ship.Mass);
		Gforce = Gforce / distance.Length();
		return Gforce;
	}

	private Vector2 calculateGravity(Star sun, SpaceShip ship)
	{
		//Calculates gravity for the ship based on the planet
		//and returns a vector2 with the force to apply to the ship.
		
		Vector2 Gforce = new Vector2(0,0);
		//get the distance between the planets
		var distance = (Vector2) ship.Position -(Vector2)sun.getPosition();
		
		if (distance.Length() < 20f)
			return Gforce;

		Gforce = G * distance.Normalized()*(-1) *(sun.Mass * ship.Mass);
		Gforce = Gforce / distance.Length();
		return Gforce;
	}
	

	
	private void updateGravity()
	{
		//update the gravity force of each planet
		for (int i = 0; i < planets.Count; i++)
		{
			
			var force = calculateGravity(i);
			for (int j = 0; j < planets.Count; j++)
			{
				if (i != j)
				{
					//calculate the gravity force between the planets
					force += calculateGravity(i, j);
					//apply the force to the planet
					planets[i].setGravityForce(force);
				}
			}
			
		}

		//Add gravity to ship
		if (ship == null)
			return;
		//else
		for (int i =0; i < planets.Count; i++)
		{
			
			ship.addForce(calculateGravity(i, ship));
			
		}
		ship.addForce(calculateGravity(sun, ship));
	
	}
	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		updateGravity();
		
	}
}
