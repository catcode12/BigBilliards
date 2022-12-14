using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstGravityHandler : MonoBehaviour
{
    public GameObject[] otherBodies;
    public int globalIndex;
    public int[] bodyIDs;
    public GameObject WorldController;
    public float bigG = 0.01f;
    public Vector2 direction;
    public Vector2 Velocity;
    public Vector2 startingVelocity;

    public Vector2 Acceleration;
    // Start is called before the first frame update
    void Start()
    {
        //this.gameObject.GetComponent<Rigidbody2D>().AddForce(startingVelocity, ForceMode2D.Impulse);
        Velocity = startingVelocity;
    }

    // Update is called once per frame
    public void move()
    {
        Debug.Log("test");
        bigG = WorldController.GetComponent<centralGravityController>().worldBigG;
        Acceleration = Vector2.zero;
        foreach (GameObject otherBody in otherBodies)
        {
            Vector2 sunPosition = otherBody.GetComponent<Transform>().position;
            Vector2 planetPosition = this.gameObject.GetComponent<Transform>().position;
            float xDistance = sunPosition.x - planetPosition.x;
            float yDistance = sunPosition.y - planetPosition.y;
            float distance = Mathf.Sqrt(xDistance * xDistance + yDistance * yDistance);
            float forceMultiplier = bigG * otherBody.GetComponent<Rigidbody2D>().mass * this.gameObject.GetComponent<Rigidbody2D>().mass / (distance * distance);
            direction = new Vector2(xDistance / distance, yDistance / distance);
            //this.gameObject.GetComponent<Rigidbody2D>().AddForce(direction * forceMultiplier, ForceMode2D.Impulse);
            Acceleration += direction * forceMultiplier / this.gameObject.GetComponent<Rigidbody2D>().mass; 
        }

        Velocity += Acceleration;
        this.gameObject.GetComponent<Transform>().position =this.gameObject.GetComponent<Transform>().position+ new Vector3 (Velocity.x, Velocity.y, 0);
    }

    public void createIDArray(){
        bodyIDs = new int[128];
        int index=0;
        foreach(GameObject body in otherBodies){
            bodyIDs[index++] = body.GetComponent<FirstGravityHandler>().globalIndex;
        }
        for ( int i = index+1; i < bodyIDs.Length;i++ ) {
            bodyIDs[i] = -1;
        }
    }
}
