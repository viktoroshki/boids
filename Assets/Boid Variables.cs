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
    public static List<Boid> boidList = new List<Boid>();
    public static float barrierX = 50f;
    public static float barrierY = 50f;
    public static float barrierZ = 50f;
}