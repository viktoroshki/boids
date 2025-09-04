# boids
Real-time Unity flocking simulation: boids that steer by separation, alignment, and cohesion.


**Boids pseudocode can be found here:**
https://vergenet.net/~conrad/boids/pseudocode.html

**More info on boids:**
https://cs.stanford.edu/people/eroberts/courses/soco/projects/2008-09/modeling-natural-systems/boids.html#:~:text=Boids%20is%20an%20artificial%20life,behavior%20of%20flocks%20of%20birds.

**A bit about this sim**
This simulation uses a compute shader to run most of the calculations (Currently ruels 1, 2 and 3; but will add more stuff to it)
Settings can be changed in the Simulation Settings script

**Things I want to add**
Hash mapping to increase performance
Ray casting to make boids steer away from objects and walls
