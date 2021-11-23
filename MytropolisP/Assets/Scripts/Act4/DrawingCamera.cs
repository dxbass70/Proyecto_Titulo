using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DrawingCamera : MonoBehaviour
{
    public Camera drawingCamera;
    public RawImage OverlayImage;
    // Start is called before the first frame update
    void Start()
    {
        drawingCamera.targetTexture = new RenderTexture(Screen.width, Screen.height, 24);
        OverlayImage.texture = drawingCamera.targetTexture;
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
