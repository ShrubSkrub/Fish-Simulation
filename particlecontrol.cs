using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class particlecontrol : MonoBehaviour
{
    ParticleSystem ps;
    ParticleSystem.Particle[] particlesArray; 
    public int resolution;

    [SerializeField] private float spawnRange = 10f;

    [SerializeField] private float strengthOfAttraction = 10f;
    [SerializeField] private float radiusOfAttraction = 5f;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("This is a debug message.");
        ps = GetComponent<ParticleSystem>();
        particlesArray = new ParticleSystem.Particle[resolution];      
        ps.Emit(resolution);        

        int totalParticles = ps.GetParticles(particlesArray);
        for (int i = 0; i < totalParticles; i++) {
            var position = new Vector3(Random.Range(-spawnRange, spawnRange), 0, Random.Range(-spawnRange, spawnRange));
            particlesArray[i].position = position;
            
            //Debug.Log(position);

        }
        ps.SetParticles(particlesArray, totalParticles);

        //Debug.Log("maxParticles: " + ps.maxParticles);
    }

    // Update is called once per frame
    void Update()
    {

        int totalParticles = ps.GetParticles(particlesArray);
        for (int i = 0; i < totalParticles; i++) {
            //Collider2D[] colliders = Physics2D.OverlapCircleAll(particlesArray[i].position, radiusOfAttraction);
            //Debug.Log("Size of array: " + colliders.Length);
            //Debug.Log("particlesArray[i].position: " + particlesArray[i].position);

            


            //Naive way
            for (int j = 0; j < totalParticles; j++) {
                var distance = Vector3.Distance(particlesArray[i].position, particlesArray[j].position);
                
                if (distance <= radiusOfAttraction && distance > 0) {
                    //Debug.Log("Distance <= 10");
                    Vector3 offset = particlesArray[i].position - particlesArray[j].position;
                    float distance2 = offset.magnitude;
                    float forceMagnitude = strengthOfAttraction / Mathf.Pow(distance2, 2);
                    Vector3 force = offset.normalized * forceMagnitude;

                    Debug.Log("Offset.normalized: " + offset.normalized + " , forceMagnitude: " + forceMagnitude);
                    
                    Debug.Log("Force: " + force + " , Velocity: " + particlesArray[i].velocity);
                    particlesArray[i].velocity -= force;
                }

                if (distance <= (radiusOfAttraction / 4) && distance > 0) {
                    //Debug.Log("Distance <= 10");
                    Vector3 offset = particlesArray[i].position - particlesArray[j].position;
                    float distance2 = offset.magnitude;
                    float forceMagnitude = (2 * strengthOfAttraction) / Mathf.Pow(distance2, 2);
                    Vector3 force = offset.normalized * forceMagnitude;

                    Debug.Log("Offset.normalized: " + offset.normalized + " , forceMagnitude: " + forceMagnitude);
                    
                    Debug.Log("Force: " + force + " , Velocity: " + particlesArray[i].velocity);
                    particlesArray[i].velocity += force;
                }
            }

            // Debug.Log("Velocity: " + particlesArray[i].velocity);
            // particlesArray[i].velocity = Vector3.left;
        }
        ps.SetParticles(particlesArray, totalParticles);
    }
}
