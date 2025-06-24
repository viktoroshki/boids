using UnityEngine;

public class Boidsimulation : MonoBehaviour
{
    void Update()
    {
        foreach (Boid boid in GlobalVariables.boidList)
        {
            moveBoid(boid);
            keepBoidInBox(boid);
        }
    }

    private void moveBoid(Boid boid)
    {
        // Set the boids new position
        boid.position += boid.velocity * Time.deltaTime;
        boid.visual.transform.position = boid.position;

        // Set the boids rotation
        Quaternion offset = Quaternion.Euler(90f, 0f, 0f); // offset due to the tip of the ocne being on the y-axis
        boid.visual.transform.rotation = Quaternion.LookRotation(boid.velocity.normalized) * offset;
    }

    private void keepBoidInBox(Boid boid)
    {
        // Teleports boid to the other side of the box if it reaches the barrier
        // x-axis
        if (boid.position.x > GlobalVariables.barrierX) { boid.position.x = 0f; }
        else if (boid.position.x < 0) { boid.position.x = GlobalVariables.barrierX; }
        // y-axis
        if (boid.position.y > GlobalVariables.barrierY) { boid.position.y = 0f; }
        else if (boid.position.y < 0) { boid.position.y = GlobalVariables.barrierY; }
        // z-axis
        if (boid.position.z > GlobalVariables.barrierZ) { boid.position.z = 0f; }
        else if (boid.position.z < 0) { boid.position.z = GlobalVariables.barrierZ; }

    }
}
