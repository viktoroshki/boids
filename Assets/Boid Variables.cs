using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;

public class Boid
{
    public Vector3 position;
    public Vector3 velocity;
    public GameObject visual;

    public Boid(Vector3 position, Vector3 velocity, GameObject visual)
    {
        this.position = position;
        this.velocity = velocity;
        this.visual = visual;
    }
}

public class GlobalVariables
{
    public static List<Boid> boidList = new List<Boid>(); // List of all the boids data in the simulation

    // Constants to show the boundary of where the boids interact
    public static float barrierX = 200f;
    public static float barrierY = 200f;
    public static float barrierZ = 200f;

    // Constants for boids
    public static float separationDistance = 5f;
    public static float alignmentPower = 1000f; // Smaller is more powerful
    public static float boidReactionDist = 30f;
    public static float maxSpeed = 100f;
}
