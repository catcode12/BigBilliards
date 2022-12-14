using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering;


public class FirstComputeHandler: MonoBehaviour
{
    public ComputeShader shader;
    
    public RenderTexture renderTexture;
    public GameObject worldGravityController;
    // Start is called before the first frame update
    void Start()
    {
        renderTexture = new RenderTexture(512,512, 32);
        renderTexture.enableRandomWrite = true;
        renderTexture.depth= 0;
        renderTexture.graphicsFormat=GraphicsFormat.R32G32B32A32_SFloat;
        renderTexture.Create();
    }
    void OnRenderImage(RenderTexture src, RenderTexture dest)
    {
        Vector4 position = new Vector4(Input.mousePosition.x , Input.mousePosition.y, 0, 0);
        Vector4 screen = new Vector4(Screen.width, Screen.height,0,0);
        Vector4 size = new Vector4(renderTexture.width, renderTexture.height, 0, 0);
        planet[] planetArray = worldGravityController.GetComponent<centralGravityController>().currentPlanetBuffer;

        ComputeBuffer planetBuffer = new ComputeBuffer(planetArray.Length, sizeof(planet));
        planetBuffer.setData(planetArray);
        Debug.Log(position.y);
        shader.SetVector("mousePos", position);
        shader.SetVector("size", size);
        shader.SetVector("screen", screen);
        shader.setBuffer("planetBuffer", planetBuffer);
        Graphics.Blit(src, renderTexture);
        shader.SetTexture(0, "Result", renderTexture);
        shader.Dispatch(0, renderTexture.width/8, renderTexture.height/8, 1);
        Graphics.Blit(renderTexture, dest);
    }

 
}
