using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ZoomImagen : MonoBehaviour
{
    public GameObject zoom;
    public GameObject Image;
    // Start is called before the first frame update
    void Start()
    {
        //zoom = GameObject.Find("Canvas/fondoZoom");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void zoomimg(){
        Image.GetComponent<Image>().sprite = GetComponent<Image>().sprite;
        zoom.SetActive(true);
    }
}
