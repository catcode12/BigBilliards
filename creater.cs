using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class creater : MonoBehaviour
{
    public GameObject planet;
    public GameObject WorldController;
    // Start is called before the first frame update
    void Start()
    {
        
    }


    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {

            Vector3 mousePos = Input.mousePosition;
            mousePos.z = Camera.main.nearClipPlane;
            Vector3 worldPosition = Camera.main.ScreenToWorldPoint(mousePos);

            GameObject instance = GameObject.Instantiate(planet) as GameObject;
            instance.SetActive(true);

            instance.GetComponent<FirstGravityHandler>().startingVelocity = (worldPosition - this.gameObject.transform.position)/50;
            WorldController.GetComponent<centralGravityController>().allObjects.Add(instance);
            instance.GetComponent<FirstGravityHandler>().globalIndex = WorldController.GetComponent<centralGravityController>().allObjects.Count;
            instance.GetComponent<FirstGravityHandler>().createIDArray();
            //WorldController.GetComponent<centralGravityController>().updateBuffer();
            
        }
    }
}
