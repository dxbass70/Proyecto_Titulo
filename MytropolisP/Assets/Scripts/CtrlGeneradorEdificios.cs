using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CtrlGeneradorEdificios : MonoBehaviour
{
    public Camera cam;
    public GameObject EdificioPrefab;
    private Vector3 point;
    private bool Construir = false;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {

        if (Construir == true & Input.GetMouseButtonDown(0)){
            Vector2 mousePos = new Vector2();
            mousePos = Input.mousePosition;
            point = cam.ScreenToWorldPoint(new Vector3(mousePos.x, mousePos.y, 105.0f));
            Invoke("CreateEdificio", 0);
            Construir = false;
        }
        
    }

    void CreateEdificio(){
        Instantiate(EdificioPrefab, point, Quaternion.identity);
    }

    public void invokeEdifico(){
        Construir = true;
    }
}
