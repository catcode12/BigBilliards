using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
    public unsafe struct planet {
        public Vector2 position;
        public Vector2 velocity;
        public float mass; 

        public fixed int attachedPlanets[128];
        public planet(Vector2 setposition, Vector2 setVelocity, float setMass, int[] setPlanets){
                position = setposition;
                velocity=setVelocity;
                mass=setMass;
                attachedPlanets=setPlanets;
        }
    }
*/
struct planetBuffer{
    public planet[] planets;
    public float bigG;

    public planetBuffer(planet[] setPlanets, float setBigG){
        planets=setPlanets;
        bigG=setBigG;
    }
}
public class centralGravityController : MonoBehaviour
{
    public float worldBigG = 0.01f;
    public List<GameObject> allObjects = new List<GameObject>();
    public planet[] currentPlanetBuffer;
    // Start is called before the first frame update
    void Start()
    {
        int index=0;
        foreach(GameObject bodyObject in allObjects){
            bodyObject.GetComponent<FirstGravityHandler>().globalIndex=index++;
        }
        foreach(GameObject bodyObject in allObjects){
           bodyObject.GetComponent<FirstGravityHandler>().createIDArray();
        }

        updateBuffer();
    }
    Texture2D createBuffer(){
        planet[] tempPlanetArray = new planet[allObjects.Count];
        int index=0;
        foreach(GameObject bodyObject in allObjects){
            tempPlanetArray[index]= new planet(
                bodyObject.GetComponent<Transform>().position,
                bodyObject.GetComponent<FirstGravityHandler>().Velocity,
                bodyObject.GetComponent<Rigidbody2D>().mass,
                bodyObject.GetComponent<FirstGravityHandler>().bodyIDs
            );
        }
        return tempPlanetArray;
        
    }
    public void updateBuffer(){
        currentPlanetBuffer=createBuffer();
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        foreach(GameObject planet in allObjects)
        {
            planet.GetComponent<FirstGravityHandler>().move();
        }
    }
}
