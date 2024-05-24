using Godot;
using System;


public interface IGravityBody 
{
    /**
     * This interface must be used by any object that has mass and is attracted to other objects by gravity.
     * The word gravity is used to differentiate from self methods of a class that also may return the same values.
     **/

    // Sets the mass of the body

	void setGravityConstant(float G);
    void setGravityMass(float mass);

    // Returns the mass of the body
    float getGravityMass();

    // Returns the position of the body
    Vector2 getGravityPosition();

    // Applies the gravity force to another body
    void applyGravityForce(Vector2 position, float mass);
}

