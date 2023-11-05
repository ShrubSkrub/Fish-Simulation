# Fish-Simulation

## Inspiration
We were inspired to create this because the schooling behavior of fish is very beautiful.

## What it does
This project attempts to simulate the schooling behavior of fish through particle interactions.

## How we built it
We built it using Unity.  Our first approach used the particle system of Unity to improve performance.  This approach had minimal optimizations, and would begin to struggle around 1000 fish.  Our second approach attempted to use a hashing algorithm to improve the performance of the system greatly, but we did not manage to get it to work.  Projected performance would have been magnitudes greater than the previous implementation.

## Challenges we ran into
We ran into major issues with implementing the improved algorithm.  It was our team's first time using Unity and C#, and also our first time working with large scale simulations.  We had to spend many hours researching algorithms and what could be done to make our code run efficiently.

## Accomplishments that we're proud of
We are proud of sticking through with the project to the end, and for submitting our first ever hackathon project.

## What we learned
We learned that defining scope at the start of the project is very important, especially if you want to meet a close deadline. We also learned so much about efficient algorithms for finding nearby neighbors within a radius.

## What's next for Large Scale Fish Schooling Simulation
Our next steps for this project would be to make the more efficient version of the algorithm work properly, and see how much we can optimize it.
