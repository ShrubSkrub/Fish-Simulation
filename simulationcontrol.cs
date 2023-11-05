using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class simulationcontrol : MonoBehaviour
{
    public GameObject fishObject;
   
    [SerializeField]public int resolution;
    GameObject[] pa;  // Particle Array 

    [SerializeField] private float spawnRange = 10f;

    [SerializeField] private float strengthOfAttraction = 10f;
    [SerializeField] private float radiusOfAttraction = 5f;

    private int numHeight;
    private int numWidth;
    private float widthBox;
    private float heightBox;

    private int gridSquares;

    
    private Dictionary<int, int[]> neighbors;

    // Start is called before the first frame update
    void Start()
    {
        //Debug.Log(fishObject);
        pa = new GameObject[resolution]; 
        // instantiate objects and store them in an array
        for (int i = 0; i < resolution; i++) {
            var position = new Vector3(UnityEngine.Random.Range(-spawnRange, spawnRange), UnityEngine.Random.Range(-spawnRange, spawnRange), 0);
            GameObject newFish = Instantiate(fishObject, position, Quaternion.identity);
            pa[i] = newFish;
            //Debug.Log(newFish);

            
        }

        Camera camera = Camera.main;
        float height = 2f * camera.orthographicSize;
        float width = height * camera.aspect;
        //Debug.Log("Camera dimensions: " + width + " x " + height);
        gridSquares = (int)(width / radiusOfAttraction);
        numHeight = gridSquares;
        numWidth = gridSquares * 2;

        widthBox = width / numWidth;
        heightBox = height / numHeight;

        //Debug.Log("WidthBox: " + widthBox + ", HeightBox: " + heightBox);

        int totalBoxes = numHeight * numWidth;

        float leftBound = 0 - width/2;
        float lowerBound = 0 - height/2;

        
        // Fill in nearby box dictionary
        neighbors = new Dictionary<int, int[]>();
        for(int i = 0; i < totalBoxes; i++) {
            int[] tempArray = {i, i-1, i+1, i-1-numWidth, i+1-numWidth, i-numWidth, i-1+numWidth, i+1+numWidth, i+numWidth}; 
            neighbors.Add(i,tempArray);
        }
        


        
    }

    // Update is called once per frame
    void Update()
    {

        List<Vector3>[] boxPoints = new List<Vector3>[numHeight*numWidth];


        // Assign points to each box
        for (int i = 0; i < resolution; i++) {
            float xpoint = pa[i].transform.position[0] + widthBox/2;
            float ypoint = pa[i].transform.position[1] + heightBox/2;
            //Debug.Log("Xpoint: " + xpoint + ", Ypoint: " + ypoint);

            int xBox = (int)(xpoint / widthBox) + (gridSquares/2) - 1;
            int yBox = (int)(ypoint / heightBox) + (gridSquares/2) - 1;

            // Debug.Log("xBox: " + xBox + ", yBox: " + yBox);
            // Debug.Log(xBox+(numWidth*yBox));
            // Debug.Log("I: " + i + ", pa[i]: " + pa[i]);
            // Debug.Log("I: " + i);
            // Debug.Log("Point" + (xBox + (numWidth * yBox)));
            // Debug.Log(boxPoints[(xBox + (numWidth * yBox))]);
            if (boxPoints[(xBox + (numWidth * yBox))] == null) {
                boxPoints[(xBox + (numWidth * yBox))] = new List<Vector3>();
            }
                boxPoints[(xBox + (numWidth * yBox))].Add(pa[i].transform.position);
        }

        // Pulls neighbors towards it
        for(int i = 0; i < resolution; i++) {   // For each point
            float xpoint = pa[i].transform.position[0] + widthBox/2;
            float ypoint = pa[i].transform.position[1] + heightBox/2;
            int xBox = (int)(xpoint / widthBox) + (gridSquares/2) - 1;
            int yBox = (int)(ypoint / heightBox) + (gridSquares/2) - 1;
            int currentBox = (xBox + (numWidth * yBox));    // Get current box of point
            int[] boxesToCheck = neighbors[currentBox];     // Find all boxes to check based on current box

            List<int> boxesLeft = new List<int>();
            for(int l = 0; l < 9; l++) {
                if (boxesToCheck[l] >= 0) {
                    boxesLeft.Add(boxesToCheck[l]);
                    // Debug.Log("Test: " + boxesToCheck[l]);
                }
            }

            Debug.Log("Boxes Left Length: " + boxesLeft.Count);

            for(int k = 0; k < boxesLeft.Count; k++) {  // Check all necessary boxes
                
                // for each point in current box j
                //      run naive code

                // points in current iterative box

                // Debug.Log(boxPoints[boxesLeft[0]][0][0]);

                for (int j = 0; j < (boxPoints[boxesLeft[k]]).Count; j++) {


                    var distance = Vector3.Distance(pa[i].transform.position, boxPoints[boxesLeft[j]][0]);
                    Debug.Log("Distance: " + distance);

                    // if (distance <= radiusOfAttraction && distance > 0) {
                    //     Vector3 offset = pa[i].transform.position - boxPoints[boxesLeft[j]][0];
                    //     float distance2 = offset.magnitude;
                    //     float forceMagnitude = strengthOfAttraction / Mathf.Pow(distance2, 2);
                    //     Vector3 force = offset.normalized * forceMagnitude;
                    //     Rigidbody rbi = pa[i].GetComponent<Rigidbody>();
                    //     rbi.velocity -= force;
                    // }
                }
        
            }
        }
    }
}