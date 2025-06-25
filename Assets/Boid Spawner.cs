using UnityEditor.Rendering;
using UnityEngine;

public class BoidSpawner : MonoBehaviour
{
    // The boid visual we will copy
    public GameObject preFab;

    void Start()
    {
        // Initialise our array of boids
        GlobalVariables.boidArray = new Boid[GlobalVariables.boidNumber];

        for (int i = 0; i < GlobalVariables.boidNumber; i++)
        {
            // Set random conditions: (starting position somewhere random inside the box-barrier)
            Vector3 randomPos = new Vector3(Random.Range(0, GlobalVariables.barrierX), Random.Range(0, GlobalVariables.barrierY), Random.Range(0, GlobalVariables.barrierZ));
            Vector3 randomVelocity = Random.insideUnitSphere * Random.Range(GlobalVariables.spawnMinSpeed, GlobalVariables.spawnMaxSpeed);

            GameObject boidVisual = Instantiate(preFab, randomPos, Quaternion.identity); // Intialise and spawn in the visual

            GlobalVariables.boidArray[i] = new Boid(randomPos, randomVelocity);
        }

        // Turn off the visual of the initial boid visual as we won't use it to simulate anything
        Destroy(preFab);

    }
}
