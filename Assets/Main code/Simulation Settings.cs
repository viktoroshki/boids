using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;

public struct Boid
{
    public Vector3 position;
    public Vector3 velocity;

    public Boid(Vector3 position, Vector3 velocity)
    {
        this.position = position;
        this.velocity = velocity;
    }
}

public class GlobalVariables
{
    public static Boid[] boidArray;
    public static Transform[] transformArray; // Where all the visuals are in the scene

    // Constants to show the boundary of where the boids interact
    public static float barrierX = 50f;
    public static float barrierY = 50f;
    public static float barrierZ = 50f;

    // Constants for boids
    public static float separationDistance = 5f;
    public static float alignmentPower = 1000f; // Smaller is more powerful
    public static float boidReactionDist = 30f;
    public static float maxSpeed = 100f;

    // Spawn parameters:
    public static int boidNumber = 1000;
    public static float spawnMinSpeed = 75f;
    public static float spawnMaxSpeed = 100f;
}