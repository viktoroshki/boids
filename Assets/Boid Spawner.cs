using UnityEditor.Rendering;
using UnityEngine;

public class BoidSpawner : MonoBehaviour
{
    // Spawn parameters:
    public int boidNumber = 10;
    public float minSpeed = 1f;
    public float maxSpeed = 10f;
    public GameObject preFab;

    void Start()
    {
        for (int i = 0; i < boidNumber; i++)
        {
            // Set random conditions: (starting position somewhere random inside the box-barrier)
            Vector3 randomPos = new Vector3(Random.Range(0, GlobalVariables.barrierX), Random.Range(0, GlobalVariables.barrierY), Random.Range(0, GlobalVariables.barrierZ));
            Vector3 randomVelocity = Random.insideUnitSphere * Random.Range(minSpeed, maxSpeed);

            GameObject boidVisual = Instantiate(preFab, randomPos, Quaternion.identity); // Intialise and spawn in the visual

            GlobalVariables.boidList.Add(new Boid(randomPos, randomVelocity, boidVisual));
        }

        // Turn off the visual of the initial boid visual as we won't use it to simulate anything
        Destroy(preFab);

    }
}
