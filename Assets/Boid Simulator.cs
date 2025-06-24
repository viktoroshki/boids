using UnityEngine;
using UnityEngine.Rendering;

public class Boidsimulation : MonoBehaviour
{
    void Update()
    {
        // Handles individual boid logic (staying in the box, updating movement)
        foreach (Boid boid in GlobalVariables.boidList)
        {
            moveBoid(boid);
            keepBoidInBox(boid);
        }

        // Handle interactiosn between boids
        seperation();
        alignment();
        cohesion();
    }

    private void moveBoid(Boid boid)
    {
        // Limit boid's speed
        if (boid.velocity.magnitude > GlobalVariables.maxSpeed)
        {
            boid.velocity = boid.velocity.normalized * GlobalVariables.maxSpeed;
        }

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

    private bool cannotCompare(Boid boidA, Boid boidB)
    {
        // Skip if the boid is comparing to itself or the other boid is outside it's reaction distance
        return ((boidA == boidB) || (boidA.position - boidB.position).magnitude < GlobalVariables.boidReactionDist);
    }

    private void seperation()
    {
        foreach (Boid boidA in GlobalVariables.boidList)
        {
            foreach (Boid boidB in GlobalVariables.boidList)
            {
                if (cannotCompare(boidA, boidB)) { continue; }

                // Now we apply the seperation rule
                Vector3 relativePosition = boidB.position - boidA.position;
                if (relativePosition.magnitude < GlobalVariables.separationDistance)
                {
                    boidA.velocity -= relativePosition;
                }
            }
        }
    }

    private void alignment()
    {
        foreach (Boid boidA in GlobalVariables.boidList)
        {
            int total = 0;
            Vector3 velocitySum = Vector3.zero;

            foreach (Boid boidB in GlobalVariables.boidList)
            {
                if (cannotCompare(boidA, boidB)) { continue; }

                // Now we apply the seperation rule
                total++;
                velocitySum += boidB.velocity;
            }

            velocitySum /= (total - 1);
            velocitySum -= boidA.velocity;
            velocitySum /= GlobalVariables.alignmentPower;

            boidA.velocity += velocitySum;
        }
    }

    private void cohesion()
    {
        foreach (Boid boidA in GlobalVariables.boidList)
        {
            int total = 0;
            Vector3 percievedCOM = Vector3.zero; // COM = Centre Of Mass

            foreach (Boid boidB in GlobalVariables.boidList)
            {
                if (cannotCompare(boidA, boidB)) { continue; }

                total++;
                percievedCOM += boidB.position;
            }

            percievedCOM /= total;

            boidA.velocity += (percievedCOM - boidA.position) / 100;
        }
    }
}
