# boids
Real-time Unity flocking simulation: boids that steer by separation, alignment, and cohesion.

Boids pseudocode can be found here (made by Conrad Parker):
https://vergenet.net/~conrad/boids/pseudocode.html
More info on boids:
https://cs.stanford.edu/people/eroberts/courses/soco/projects/2008-09/modeling-natural-systems/boids.html#:~:text=Boids%20is%20an%20artificial%20life,behavior%20of%20flocks%20of%20birds.

Currently added:
Rules 1 - 3
  Cohesion (rule 1)
  Seperation (Rule 2)
  Alignment (Rule 3)
Reaction distance - boids can only react to other boids within this distance allowing multiple flocks to appear
Box / barrier - keeps all the boids together by teleporting boids to the other side of the box when they reach a wall (debugger script helps outline the box)
Paramters - currently only maxSpeed and all the other constants to control the 3 rules

To be added in the future:
Ray casting to allow boids to interact with loads of different environmnts
More of the optional rules which you can see in the pseudocode
Turn it into a compute shader to simulate loads of boids or before getting into that complicated stuff: making the CPU side of things more efficient (jobs + Burst + spatial partitioning)
Hash mapping to increase performance
